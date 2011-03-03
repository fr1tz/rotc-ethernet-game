//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - hud.cs
// Code for the TSControl through which the game is viewed
//------------------------------------------------------------------------------

function clientCmdSetHudColor(%darkcolor, %lightcolor)
{
	Hud.setColor(%darkcolor, %lightcolor);
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

	%this.flashWarnings();
	%this.updateMetrics();
}

function Hud::onSleep(%this)
{
	Canvas.popDialog( MainChatHud  );

	// pop the keymaps
	popActionMap(MoveMap);

	cancel(%this.flashWarningsThread);
}

function Hud::flashWarnings(%this)
{
	cancel(%this.flashWarningsThread);

	%this.zWarningsFlash = !%this.zWarningsFlash;
	%profile = %this.zWarningsFlash ? HudWarningFlashProfile : HudWarningProfile;
	HudWarning1.setProfile(%profile);
	HudWarning2.setProfile(%profile);
	HudWarning3.setProfile(%profile);
	HudWarning4.setProfile(%profile);
	HudWarning5.setProfile(%profile);
	HudWarning6.setProfile(%profile);

	%this.flashWarningsThread = %this.schedule(500, "flashWarnings");
}

function Hud::updateMetrics(%this)
{
	cancel(%this.zUpdateMetricsThread);

	HudFpsGraph.setVisible($Pref::Hud::ShowFPSGraph);
	
	%txt = "";
	if($Pref::Hud::ShowPing)
		%txt = %txt @ "PING:" SPC ServerConnection.getPing() @ "   ";
	if($Pref::Hud::ShowPacketloss)
		%txt = %txt @ "PACKETLOSS:" SPC ServerConnection.getPacketloss() @ "   ";
	if($Pref::Hud::ShowFPS)
		%txt = %txt @ "FPS:" SPC ($FPS::Real);
		
	HudMetrics.setText(%txt);
	
	%this.zUpdateMetricsThread = %this.schedule(50, "updateMetrics");
}


function Hud::matchControlObject(%this, %obj)
{
	%data = %obj.getDataBlock().getName();

	Scanlines.setVisible(false);
     
    Heat.setVisible(false);

	if(%obj.getType() & $TypeMasks::CameraObjectType)
	{
		PrimaryWeaponIcon.setVisible(false);

		HealthIcon.setVisible(false);
		HealthMeter.setVisible(false);
		EnergyIcon.setVisible(false);
		EnergyMeter.setVisible(false);
	}
	else
	{
		HealthIcon.setVisible(true);
		HealthMeter.setVisible(true);
		EnergyIcon.setVisible(true);
		EnergyMeter.setVisible(true);
	}

	if(%data $= "PlayerThirdEye")
		Scanlines.setVisible(true);
}

function Hud::updateGrenadeAmmo(%this)
{
    if(%this.grenadeAmmoThread !$= "")
        cancel(%this.grenadeAmmoThread);
        
    %dt = ((GrenadeAmmo.dt/1000)*GrenadeAmmo.interval)
        / (GrenadeAmmo.interval/50);
    
    GrenadeAmmo.setValue(GrenadeAmmo.getValue() + %dt);

    GrenadeAmmo.setVisible(GrenadeAmmo.getValue() < 1);   
    
    %this.grenadeAmmoThread = %this.schedule(50, "updateGrenadeAmmo");
}

function Hud::updateHeat(%this)
{
    if(%this.heatThread !$= "")
        cancel(%this.heatThread);
        
    Heat.setValue(Heat.getValue() + Heat.dt);
        
    %this.heatThread = %this.schedule(50, "updateHeat");
}

function Hud::setColor(%this, %dark, %light)
{
	HudDefaultProfile.fillColor = %dark SPC "100";
	HudDefaultProfile.borderColor	= %dark SPC "200";
	HudDefaultProfile.fontColor	= %light SPC "255";
	HudButtonProfile.fillColor = %dark SPC "100";
	HudButtonProfile.borderColor	= %dark SPC "200";
	HudButtonProfile.fontColor	= %dark SPC "200";
	HudWarningFlashProfile.fontColor = %dark SPC "255";
}


