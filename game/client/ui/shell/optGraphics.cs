//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function OptGraphicsWindow::onWake(%this)
{
    if($pref::OpenGL::textureTrilinear == false)
        OptGraphicsTextureFilteringMenu.setSelected(0);
    else if($pref::OpenGL::textureAnisotropy != 0)
        OptGraphicsTextureFilteringMenu.setSelected(2);
    else
        OptGraphicsTextureFilteringMenu.setSelected(1);
        
	// Player trails...
	OptGraphicsTrailsAmount.setValue($Pref::Player::Trails::Amount);
	OptGraphicsTrailsAmountNum.setValue($Pref::Player::Trails::Amount);        
	OptGraphicsTrailsScale.setValue($Pref::Player::Trails::Scale);
	OptGraphicsTrailsScaleNum.setValue($Pref::Player::Trails::Scale);   	
	OptGraphicsTrailsVisibility.setValue($Pref::Player::Trails::Visibility);
	OptGraphicsTrailsVisibilityNum.setValue($Pref::Player::Trails::Visibility);   	
	OptGraphicsTrailsDetail.setValue($Pref::Player::Trails::Detail);
	OptGraphicsTrailsDetailNum.setValue($Pref::Player::Trails::Detail);   	
}

function OptGraphicsWindow::onAddedAsWindow(%this)
{
    // Graphics driver menu...
	%buffer = getDisplayDeviceList();
	%count = getFieldCount( %buffer );
	OptGraphicsDriverMenu.clear();
	for(%i = 0; %i < %count; %i++)
		OptGraphicsDriverMenu.add(getField(%buffer, %i), %i);
	%selId = OptGraphicsDriverMenu.findText( $pref::Video::displayDevice );
	if ( %selId == -1 )
		%selId = 0; // How did THAT happen?
	OptGraphicsDriverMenu.setSelected( %selId );
	OptGraphicsDriverMenu.onSelect( %selId, "" );
 
    // Zone rendering menu...
	OptGraphicsZoneRenderingMenu.clear();
	OptGraphicsZoneRenderingMenu.add("None", $TacticalZoneRenderMode::None);
	OptGraphicsZoneRenderingMenu.add("Borders Only", $TacticalZoneRenderMode::BordersOnly);
	OptGraphicsZoneRenderingMenu.add("Full", $TacticalZoneRenderMode::Full);
	OptGraphicsZoneRenderingMenu.setSelected($Pref::TacticalZone::RenderMode);
 
    // Texture filtering menu...
	OptGraphicsTextureFilteringMenu.clear();
	OptGraphicsTextureFilteringMenu.add("Bilinear", 0);
	OptGraphicsTextureFilteringMenu.add("Trilinear", 1);
	OptGraphicsTextureFilteringMenu.add("Anisotropic", 2);
}

function OptGraphicsDriverMenu::onSelect( %this, %id, %text )
{
	// Attempt to keep the same res and bpp settings:
	if ( OptGraphicsResolutionMenu.size() > 0 )
		%prevRes = OptGraphicsResolutionMenu.getText();
	else
		%prevRes = getWords( $pref::Video::resolution, 0, 1 );

	// Check if this device is full-screen only:
	if ( isDeviceFullScreenOnly( %this.getText() ) )
	{
		OptGraphicsFullscreenToggle.setValue( true );
		OptGraphicsFullscreenToggle.setActive( false );
		OptGraphicsFullscreenToggle.onAction();
	}
	else
		OptGraphicsFullscreenToggle.setActive( true );

	if ( OptGraphicsFullscreenToggle.getValue() )
	{
		if ( OptGraphicsBPPMenu.size() > 0 )
			%prevBPP = OptGraphicsBPPMenu.getText();
		else
			%prevBPP = getWord( $pref::Video::resolution, 2 );
	}

	// Fill the resolution and bit depth lists:
	OptGraphicsResolutionMenu.init( %this.getText(), OptGraphicsFullscreenToggle.getValue() );
	OptGraphicsBPPMenu.init( %this.getText() );

	// Try to select the previous settings:
	%selId = OptGraphicsResolutionMenu.findText( %prevRes );
	if ( %selId == -1 )
		%selId = 0;
	OptGraphicsResolutionMenu.setSelected( %selId );

	if ( OptGraphicsFullscreenToggle.getValue() )
	{
		%selId = OptGraphicsBPPMenu.findText( %prevBPP );
		if ( %selId == -1 )
			%selId = 0;
		OptGraphicsBPPMenu.setSelected( %selId );
		OptGraphicsBPPMenu.setText( OptGraphicsBPPMenu.getTextById( %selId ) );
	}
	else
		OptGraphicsBPPMenu.setText( "Default" );

}

function OptGraphicsResolutionMenu::init( %this, %device, %fullScreen )
{
	%this.clear();
	%resList = getResolutionList( %device );
	%resCount = getFieldCount( %resList );
	%deskRes = getDesktopResolution();

	%count = 0;
	for ( %i = 0; %i < %resCount; %i++ )
	{
		%res = getWords( getField( %resList, %i ), 0, 1 );

		if ( !%fullScreen )
		{
			if ( firstWord( %res ) >= firstWord( %deskRes ) )
				continue;
			if ( getWord( %res, 1 ) >= getWord( %deskRes, 1 ) )
				continue;
		}

		// Only add to list if it isn't there already:
		if ( %this.findText( %res ) == -1 )
		{
			%this.add( %res, %count );
			%count++;
		}
	}
}

function OptGraphicsFullscreenToggle::onAction(%this)
{
	Parent::onAction();
	%prevRes = OptGraphicsResolutionMenu.getText();

	// Update the resolution menu with the new options
	OptGraphicsResolutionMenu.init( OptGraphicsDriverMenu.getText(), %this.getValue() );

	// Set it back to the previous resolution if the new mode supports it.
	%selId = OptGraphicsResolutionMenu.findText( %prevRes );
	if ( %selId == -1 )
		%selId = 0;
 	OptGraphicsResolutionMenu.setSelected( %selId );
}


function OptGraphicsBPPMenu::init( %this, %device )
{
	%this.clear();

	if ( %device $= "Voodoo2" )
		%this.add( "16", 0 );
	else
	{
		%resList = getResolutionList( %device );
		%resCount = getFieldCount( %resList );
		%count = 0;
		for ( %i = 0; %i < %resCount; %i++ )
		{
			%bpp = getWord( getField( %resList, %i ), 2 );

			// Only add to list if it isn't there already:
			if ( %this.findText( %bpp ) == -1 )
			{
				%this.add( %bpp, %count );
				%count++;
			}
		}
	}
}

function OptGraphicsZoneRenderingMenu::onSelect( %this, %id, %text )
{
    $Pref::TacticalZone::RenderMode = %id;
}

function OptGraphicsTextureFilteringMenu::onSelect( %this, %id, %text )
{
    if(%id == 0) // Bilinear
    {
        $pref::OpenGL::textureTrilinear = false;
        $pref::OpenGL::textureAnisotropy = 0.0;
    }
    else if(%id == 1) // Trilinear
    {
        $pref::OpenGL::textureTrilinear = true;
        $pref::OpenGL::textureAnisotropy = 0.0;
    }
    else if(%id == 2) // Anisotropic
    {
        $pref::OpenGL::textureTrilinear = true;
        $pref::OpenGL::textureAnisotropy = 1.0;
        setOpenGLAnisotropy($pref::OpenGL::textureAnisotropy);
    }
}

function OptGraphicsWindow::applyGraphics( %this )
{
	%newDriver = OptGraphicsDriverMenu.getText();
	%newRes = OptGraphicsResolutionMenu.getText();
	%newBpp = OptGraphicsBPPMenu.getText();
	%newFullScreen = OptGraphicsFullscreenToggle.getValue();

	if ( %newDriver !$= $pref::Video::displayDevice )
	{
		setDisplayDevice( %newDriver, firstWord( %newRes ), getWord( %newRes, 1 ), %newBpp, %newFullScreen );
		//OptionsWindow::deviceDependent( %this );
	}
	else
		setScreenMode( firstWord( %newRes ), getWord( %newRes, 1 ), %newBpp, %newFullScreen );
  
    flushTextureCache();
}

function OptGraphicsWindowUpdateTrails()
{
	%oldVal = $Pref::Player::Trails::Amount;
	%newVal = OptGraphicsTrailsAmount.getValue();
	if(%newVal == %oldVal)
		%newVal = OptGraphicsTrailsAmountNum.getValue();		
	$Pref::Player::Trails::Amount = getSubStr(%newVal, 0, 5);
	OptGraphicsTrailsAmount.setValue($Pref::Player::Trails::Amount);
	OptGraphicsTrailsAmountNum.setValue($Pref::Player::Trails::Amount);	
	
	%oldVal = $Pref::Player::Trails::Scale;
	%newVal = OptGraphicsTrailsScale.getValue();
	if(%newVal == %oldVal)
		%newVal = OptGraphicsTrailsScaleNum.getValue();		
	$Pref::Player::Trails::Scale = getSubStr(%newVal, 0, 5);
	OptGraphicsTrailsScale.setValue($Pref::Player::Trails::Scale);
	OptGraphicsTrailsScaleNum.setValue($Pref::Player::Trails::Scale);		
	
	%oldVal = $Pref::Player::Trails::Visibility;
	%newVal = OptGraphicsTrailsVisibility.getValue();
	if(%newVal == %oldVal)
		%newVal = OptGraphicsTrailsVisibilityNum.getValue();		
	$Pref::Player::Trails::Visibility = getSubStr(%newVal, 0, 5);
	OptGraphicsTrailsVisibility.setValue($Pref::Player::Trails::Visibility);
	OptGraphicsTrailsVisibilityNum.setValue($Pref::Player::Trails::Visibility);	
	
	%oldVal = $Pref::Player::Trails::Detail;
	%newVal = OptGraphicsTrailsDetail.getValue();
	if(%newVal == %oldVal)
		%newVal = OptGraphicsTrailsDetailNum.getValue();		
	$Pref::Player::Trails::Detail = getSubStr(%newVal, 0, 5);
	OptGraphicsTrailsDetail.setValue($Pref::Player::Trails::Detail);
	OptGraphicsTrailsDetailNum.setValue($Pref::Player::Trails::Detail);			 
}
