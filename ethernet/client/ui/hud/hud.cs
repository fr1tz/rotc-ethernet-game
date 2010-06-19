//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - hud.cs
// Code for the TSControl through which the game is viewed
//------------------------------------------------------------------------------

function clientCmdSetHudColor(%color)
{
	Hud.setColor(%color);
}

function refreshBottomTextCtrl()
{
	BottomPrintText.position = "0 0";
}

function refreshCenterTextCtrl()
{
	CenterPrintText.position = "0 0";
}

//-----------------------------------------------------------------------------

function Hud::onWake(%this)
{
	// Turn off any shell sounds...
	// alxStop( ... );

	$enableDirectInput = "1";
	activateDirectInput();

	// reclaim ScannerHud
	%this.add(ScannerHud);
	%this.bringToFront(ScannerHud);
	%this.bringToFront(Scanlines);

	// add message hud dialog...
	Canvas.pushDialog(MainChatHud);
	chatHud.attach(HudMessageVector);

	// just update the action map here
    pushActionMap(MoveMap);

	// hack city - these controls are floating around and need to be clamped
	schedule(0, 0, "refreshCenterTextCtrl");
	schedule(0, 0, "refreshBottomTextCtrl");
}

function Hud::onSleep(%this)
{
	Canvas.popDialog( MainChatHud  );

	// pop the keymaps
	popActionMap(MoveMap);
}

function Hud::matchControlObject(%this, %obj)
{
	%data = %obj.getDataBlock().getName();

	Scanlines.setVisible(false);
	DiscIcon1.setVisible(false);
	DiscIcon2.setVisible(false);
	DiscIcon3.setVisible(false);
 
    GrenadeIcon.setVisible(false);
    GrenadeAmmo.setVisible(false);
    
    Heat.setVisible(false);

	if(%obj.getType() & $TypeMasks::CameraObjectType)
	{
		PrimaryWeaponIcon.setVisible(false);

		HealthMeter.setVisible(false);
		EnergyMeter.setVisible(false);

		%this.showDiscIcons = false;
	}
	else
	{
		PrimaryWeaponIcon.setVisible(true);

		HealthMeter.setVisible(true);
		EnergyMeter.setVisible(true);

		if(%obj.getType() & $TypeMasks::PlayerObjectType)
		{
			%this.showDiscIcons = true;
			%this.updateDiscIcons();
   
            GrenadeIcon.setVisible(true);
            GrenadeAmmo.setVisible(true);
            Heat.setVisible(true);
		}
	}

	if(%data $= "PlayerThirdEye")
		Scanlines.setVisible(true);
}

function Hud::updateDiscIcons(%this)
{
	if(!%this.showDiscIcons)
	{
		DiscIcon1.setVisible(false);
		DiscIcon2.setVisible(false);
		DiscIcon3.setVisible(false);
		return;
	}

	if(%this.numDiscs $= "")
		%this.numDiscs = 2;

	switch(%this.numDiscs)
	{
		case 1:
			DiscIcon1.setVisible(true);
			DiscIcon2.setVisible(false);
			DiscIcon3.setVisible(false);
			break;

		case 2:
			DiscIcon1.setVisible(true);
			DiscIcon2.setVisible(true);
			DiscIcon3.setVisible(false);
			break;

		case 3:
			DiscIcon1.setVisible(true);
			DiscIcon2.setVisible(true);
			DiscIcon3.setVisible(true);
			break;

		default:
			DiscIcon1.setVisible(false);
			DiscIcon2.setVisible(false);
			DiscIcon3.setVisible(false);
	}
}

function Hud::updateGrenadeAmmo(%this)
{
    if(%this.grenadeAmmoThread !$= "")
        cancel(%this.grenadeAmmoThread);
        
    %dt = ((GrenadeAmmo.dt/1000)*GrenadeAmmo.interval)
        / (GrenadeAmmo.interval/50);
    
    GrenadeAmmo.setValue(GrenadeAmmo.getValue() + %dt);
    
    %this.grenadeAmmoThread = %this.schedule(50, "updateGrenadeAmmo");
}

function Hud::updateHeat(%this)
{
    if(%this.heatThread !$= "")
        cancel(%this.heatThread);
        
    Heat.setValue(Heat.getValue() + Heat.dt);
        
    %this.heatThread = %this.schedule(50, "updateHeat");
}

function Hud::setColor(%this, %color)
{
	if(%color $= "red")
	{
		Scanlines.color = "255 50 50 50";

		HudDefaultProfile.fillColor = "255 0 0 100";
		HudDefaultProfile.fillColorHL = "255 0 0 100";
		HudDefaultProfile.fillColorNA = "255 0 0 100";

		HudDefaultProfile.borderColor	= "255 0 0 200";
		HudDefaultProfile.borderColorHL = "255 0 0 200";
		HudDefaultProfile.borderColorNA = "255 0 0 200";

		HudDefaultProfile.fontColor	= "255 200 200 255";
		HudDefaultProfile.fontColorHL = "255 200 200 255";
		HudDefaultProfile.fontColorNA = "255 200 200 255";
		HudDefaultProfile.fontColorSEL= "200 200 200";

		HudButtonProfile.fillColor = "255 0 0 100";
		HudButtonProfile.fillColorHL = "255 0 0 100";
		HudButtonProfile.fillColorNA = "255 0 0 100";

		HudButtonProfile.borderColor	= "255 0 0 200";
		HudButtonProfile.borderColorHL = "255 0 0 200";
		HudButtonProfile.borderColorNA = "255 0 0 200";

		HudButtonProfile.fontColor	= "255 0 0 200";
		HudButtonProfile.fontColorHL = "255 0 0 200";
		HudButtonProfile.fontColorNA = "255 0 0 200";
		HudButtonProfile.fontColorSEL= "200 200 200";

		HudMediumTextProfile.fillColor = "255 0 0 100";
		HudMediumTextProfile.fillColorHL = "255 0 0 100";
		HudMediumTextProfile.fillColorNA = "255 0 0 100";
  
		HudProgressProfile.fillColor = "255 0 0 100";
		HudProgressProfile.fillColorHL = "255 0 0 100";
		HudProgressProfile.fillColorNA = "255 0 0 100";

		HudProgressProfile.borderColor	= "255 0 0 200";
		HudProgressProfile.borderColorHL = "255 0 0 200";
		HudProgressProfile.borderColorNA = "255 0 0 200";
	}

	if(%color $= "blue")
	{
		Scanlines.color = "50 50 255 50";

		HudDefaultProfile.fillColor = "0 100 255 100";
		HudDefaultProfile.fillColorHL = "0 100 255 100";
		HudDefaultProfile.fillColorNA = "0 100 255 100";

		HudDefaultProfile.borderColor	= "0 100 255 200";
		HudDefaultProfile.borderColorHL = "0 100 255 200";
		HudDefaultProfile.borderColorNA = "0 100 255 200";

		HudDefaultProfile.fontColor	= "200 200 255 255";
		HudDefaultProfile.fontColorHL = "200 200 255 255";
		HudDefaultProfile.fontColorNA = "200 200 255 255";
		HudDefaultProfile.fontColorSEL= "200 200 200";

		HudButtonProfile.fillColor = "0 100 255 100";
		HudButtonProfile.fillColorHL = "0 100 255 100";
		HudButtonProfile.fillColorNA = "0 100 255 100";

		HudButtonProfile.borderColor	= "0 100 255 200";
		HudButtonProfile.borderColorHL = "0 100 255 200";
		HudButtonProfile.borderColorNA = "0 100 255 200";

		HudButtonProfile.fontColor	= "0 100 255 200";
		HudButtonProfile.fontColorHL = "0 100 255 200";
		HudButtonProfile.fontColorNA = "0 100 255 200";
		HudButtonProfile.fontColorSEL= "200 200 200";

		HudMediumTextProfile.fillColor = "0 100 255 100";
		HudMediumTextProfile.fillColorHL = "0 100 255 100";
		HudMediumTextProfile.fillColorNA = "0 100 255 100";
  
		HudProgressProfile.fillColor = "0 100 255 100";
		HudProgressProfile.fillColorHL = "0 100 255 100";
		HudProgressProfile.fillColorNA = "0 100 255 100";

		HudProgressProfile.borderColor	= "0 100 255 200";
		HudProgressProfile.borderColorHL = "0 100 255 200";
		HudProgressProfile.borderColorNA = "0 100 255 200";
	}
}


