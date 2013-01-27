//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function OptControlsWindow::onWake(%this)
{
   if(OptControlsWindow.zMeaning $= "")
      OptControlsWindow.zMeaning = 1;

   OptControlsMouseSensitivity.setValue($Pref::Input::MouseSensitivity);
   OptControlsMouseSensitivityNum.setValue($Pref::Input::MouseSensitivity);
   OptControlsMouseInvertYAxis.setValue($Pref::Input::InvertMouse);
}

function OptControlsWindow::onSleep(%this)
{
	// write out the control config into the config.cs file
	moveMap.save( "~/client/config.cs" );
}

function OptControlsWindow::onAddedAsWindow(%this)
{
	OptControlsMeaning.clear();
	OptControlsMeaning.add("Universal", 0);
	OptControlsMeaning.add("ROTC: Ethernet", 1);
	OptControlsMeaning.add("Infantry", 2);
	OptControlsMeaning.setSelected(OptControlsWindow.zMeaning);
}

function OptControlsMeaning::onSelect( %this, %id, %text )
{
   OptControlsWindow.zMeaning = %id;
	OptRemapList.fillList();
}

function restoreDefaultMappings()
{
	moveMap.delete();
	exec( "~/client/scripts/actionmap.standard.cs" );
	OptRemapList.fillList();
}

function getMapDisplayName( %device, %action )
{
	if ( %device $= "keyboard" )
		return( %action );
	else if ( strstr( %device, "mouse" ) != -1 )
	{
		// Substitute "mouse" for "button" in the action string:
		%pos = strstr( %action, "button" );
		if ( %pos != -1 )
		{
			%mods = getSubStr( %action, 0, %pos );
			%object = getSubStr( %action, %pos, 1000 );
			%instance = getSubStr( %object, strlen( "button" ), 1000 );
			return( %mods @ "mouse" @ ( %instance + 1 ) );
		}
		else
			error( "Mouse input object other than button passed to getDisplayMapName!" );
	}
	else if ( strstr( %device, "joystick" ) != -1 )
	{
		// Substitute "joystick" for "button" in the action string:
		%pos = strstr( %action, "button" );
		if ( %pos != -1 )
		{
			%mods = getSubStr( %action, 0, %pos );
			%object = getSubStr( %action, %pos, 1000 );
			%instance = getSubStr( %object, strlen( "button" ), 1000 );
			return( %mods @ "joystick" @ ( %instance + 1 ) );
		}
		else
		{
			%pos = strstr( %action, "pov" );
			if ( %pos != -1 )
			{
				%wordCount = getWordCount( %action );
				%mods = %wordCount > 1 ? getWords( %action, 0, %wordCount - 2 ) @ " " : "";
				%object = getWord( %action, %wordCount - 1 );
				switch$ ( %object )
				{
					case "upov":	%object = "POV1 up";
					case "dpov":	%object = "POV1 down";
					case "lpov":	%object = "POV1 left";
					case "rpov":	%object = "POV1 right";
					case "upov2":  %object = "POV2 up";
					case "dpov2":  %object = "POV2 down";
					case "lpov2":  %object = "POV2 left";
					case "rpov2":  %object = "POV2 right";
					default:		 %object = "??";
				}
				return( %mods @ %object );
			}
			else
				error( "Unsupported Joystick input object passed to getDisplayMapName!" );
		}
	}

	return( "(unbound)" );
}

function buildFullMapString( %index )
{
	%name = getField($RemapName[%index], OptControlsWindow.zMeaning);
   if(%name $= "") %name = getField($RemapName[%index], 0);
   if(%name $= "-") return "";

	%cmd	= $RemapCmd[%index];

	%temp = moveMap.getBinding( %cmd );
	%device = getField( %temp, 0 );
	%object = getField( %temp, 1 );
	if ( %device !$= "" && %object !$= "" )
		%mapString = getMapDisplayName( %device, %object );
	else
		%mapString = "";

	return( %name TAB %mapString );
}

function OptRemapList::fillList( %this )
{
	%this.clear();
	for ( %i = 1; %i < $RemapCount; %i++ )
	{
		if($RemapName[%i] $= "")
         continue;
      %mapString =  buildFullMapString(%i);
      if(%mapString !$= "")
			%this.addRow(%i, %mapString);
	}
}

//------------------------------------------------------------------------------
function OptRemapList::doRemap( %this )
{
	%selId = %this.getSelectedId();
	%name = $RemapName[%selId];

	OptRemapText.setValue( "REMAP \"" @ %name @ "\"" );
	OptRemapInputCtrl.index = %selId;

	deactivateActionMaps();

	Canvas.pushDialog( RemapDlg );
}

//------------------------------------------------------------------------------

function clearMapping(%cmd)
{
	%temp = moveMap.getBinding(%cmd);
	%device = getField( %temp, 0 );
	%action = getField( %temp, 1 );
	moveMap.unbind(%device, %action);
}

function redoMapping( %device, %action, %cmd, %oldIndex, %newIndex )
{
	clearMapping(%cmd);
	moveMap.unbind(%device, %action);
	moveMap.bind( %device, %action, %cmd );
	OptRemapList.setRowById( %oldIndex, buildFullMapString( %oldIndex ) );
	OptRemapList.setRowById( %newIndex, buildFullMapString( %newIndex ) );
}

//------------------------------------------------------------------------------
function findRemapCmdIndex( %command )
{
	for ( %i = 1; %i < $RemapCount; %i++ )
	{
		if ( %command $= $RemapCmd[%i] )
			return( %i );
	}
	return( -1 );
}

function OptRemapInputCtrl::onInputEvent( %this, %device, %action )
{
	//error( "** onInputEvent called - device = " @ %device @ ", action = " @ %action @ " **" );
	Canvas.popDialog( RemapDlg );

	reactivateActionMaps();

	// Test for the reserved keystrokes:
	if ( %device $= "keyboard" )
	{
		// Cancel...
		if ( %action $= "escape" )
		{
			// Do nothing...
			return;
		}
	}

	%cmd  = $RemapCmd[%this.index];
	%name = $RemapName[%this.index];

	// First check to see if the given action is already mapped:
	%prevMap = moveMap.getCommand( %device, %action );
	if ( %prevMap !$= %cmd )
	{
		if ( %prevMap $= "" )
		{
			clearMapping(%cmd);
            moveMap.unbind(%device, %action);
			moveMap.bind(%device, %action, %cmd);
			OptRemapList.setRowById(%this.index, buildFullMapString(%this.index));
		}
		else
		{
			%mapName = getMapDisplayName( %device, %action );
			%prevMapIndex = findRemapCmdIndex( %prevMap );
			if ( %prevMapIndex == -1 )
				MessageBoxOK( "REMAP FAILED", "\"" @ %mapName @ "\" is already bound to a non-remappable command!" );
			else
			{
				%prevCmdName = $RemapName[%prevMapIndex];
				MessageBoxYesNo( "WARNING",
					"\"" @ %mapName @ "\" is already bound to \""
						@ %prevCmdName @ "\"!\nDo you want to undo this mapping?",
					"redoMapping(" @ %device @ ", \"" @ %action @ "\", \"" @ %cmd @ "\", " @ %prevMapIndex @ ", " @ %this.index @ ");", "" );
			}
			return;
		}
	}
}

//------------------------------------------------------------------------------

function OptControlsUpdateMouse()
{
    %oldSensitivity = $Pref::Input::MouseSensitivity;
    %newSensitivity = OptControlsMouseSensitivity.getValue();
    if(%newSensitivity == %oldSensitivity)
        %newSensitivity = OptControlsMouseSensitivityNum.getValue();
        
    $Pref::Input::MouseSensitivity = getSubStr(%newSensitivity, 0, 4);
    $Pref::Input::InvertMouse = OptControlsMouseInvertYAxis.getValue();
    
    MoveMap.bind(
        mouse0,
        "xaxis",
        S,
        $Pref::Input::MouseSensitivity,
        yaw
    );
    
    MoveMap.bind(
        mouse0,
        "yaxis",
        $Pref::Input::InvertMouse ? "SI" : "S",
        $Pref::Input::MouseSensitivity,
        pitch
    );
    
    OptControlsMouseSensitivity.setValue($Pref::Input::MouseSensitivity);
    OptControlsMouseSensitivityNum.setValue($Pref::Input::MouseSensitivity);
}

