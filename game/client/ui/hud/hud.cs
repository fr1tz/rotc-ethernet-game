//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - hud.cs
// Code for the TSControl through which the game is viewed
//------------------------------------------------------------------------------

function clientCmdSetHudColor(%darkcolor, %lightcolor)
{
	Hud.setColor(%darkcolor, %lightcolor);
}

function clientCmdSetHudBackground(%slot, %bitmap, %color, %repeat, %alpha, %alphaDt)
{
	//error("slot:" SPC %slot);
	//error("bitmap:" SPC %bitmap);
	//error("color:" SPC %color);
	//error("repeat:" SPC %repeat);
	//error("alpha:" SPC %alpha);
	//error("alphaDt:" SPC %alphaDt);

	switch(%slot) {
		case 1:
			%ctrl = HudBackground1;
			%profile = HudBackgroundProfile1;
		case 2:
			%ctrl = HudBackground2;
			%profile = HudBackgroundProfile2;
		case 3:
			%ctrl = HudBackground3;
			%profile = HudBackgroundProfile3;
		default:
			return;
	}

	if(%bitmap !$= "")
		%ctrl.bitmap = %bitmap;
	if(%color !$= "") 
		HUD.zBackgroundColor[%slot] = %color;
	if(%repeat !$= "")
		%ctrl.wrap = %repeat;
	HUD.zBackgroundAlpha[%slot] = %alpha;
	if(%alphaDt !$= "")
		HUD.zBackgroundAlphaDt[%slot] = %alphaDt;
	%profile.fillColor = %color SPC %alpha;
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
	if(!ServerConnection.isDemoPlaying())
      pushActionMap(MoveMap);

	// hack city - these controls are floating around and need to be clamped
	schedule(0, 0, "refreshCenterTextCtrl");
	schedule(0, 0, "refreshBottomTextCtrl");

	%this.flashWarnings();
	%this.animThread();
}

function Hud::onSleep(%this)
{
	Canvas.popDialog( MainChatHud  );

	// pop the keymaps
	popActionMap(MoveMap);

	cancel(%this.flashWarningsThread);
	cancel(%this.zAnimThread);
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

function Hud::animThread(%this)
{
	HudFpsGraph.setVisible($Pref::Hud::ShowFPSGraph);
	
	%txt = "";
	if($Pref::Hud::ShowPing && isObject(ServerConnection))
		%txt = %txt @ "PING:" SPC ServerConnection.getPing() @ "   ";
	if($Pref::Hud::ShowPacketloss && isObject(ServerConnection))
		%txt = %txt @ "PACKETLOSS:" SPC ServerConnection.getPacketloss() @ "   ";
	if($Pref::Hud::ShowFPS)
		%txt = %txt @ "FPS:" SPC ($FPS::Real);
		
	HudMetrics.setText(%txt);

	for(%slot = 1; %slot <= 3; %slot++)
	{
		switch(%slot) {
			case 1:
				%ctrl = HudBackground1;
				%profile = HudBackgroundProfile1;
			case 2:
				%ctrl = HudBackground2;
				%profile = HudBackgroundProfile2;
			case 3:
				%ctrl = HudBackground3;
				%profile = HudBackgroundProfile3;
		}

		HUD.zBackgroundAlpha[%slot] += HUD.zBackgroundAlphaDt[%slot];
		if(HUD.zBackgroundAlpha[%slot] < 0)
			HUD.zBackgroundAlpha[%slot] = 0;

		%profile.fillColor = 
			HUD.zBackgroundColor[%slot] SPC HUD.zBackgroundAlpha[%slot];
	}
	
	%this.zAnimThread = %this.schedule(50, "animThread");
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
	HudMediumTextProfile.fillColor = %dark SPC "200";
	HudMediumTextProfile.fontColor = %light SPC "200";

   HudMenuT.forceReflow();
   HudMenuL.forceReflow();
   HudMenuR.forceReflow();
   HudMenuC.forceReflow();
}

function Hud::setCrosshair(%this, %option, %arg1, %arg2, %arg3, %arg4, %arg5)
{
   //error(%option SPC %arg1 SPC %arg2 SPC %arg3 SPC %arg4 SPC %arg5);
   if(%option == 0)
   {
      CrosshairStatic.setVisible(false);
      CrosshairStatic.setBitmap("");
      Crosshair.setVisible(false);
      Crosshair.drawCircle = false;
      Crosshair.drawCross  = false;
      Crosshair.drawSquare = false;
      Crosshair.drawBitmap = false;
      Crosshair.circleSegments = 36;
      Crosshair.circleLineWidth = 2;
      Crosshair.circleAngle = 0;
      Crosshair.crossLineWidth = 2;
      Crosshair.crossLineLength = 100;
      Crosshair.crossParts = 15;
      Crosshair.setBitmap("");
   }
   else if(%option == 1)
   {
      Crosshair.setVisible(true);
      if(CrosshairStatic.bitmap !$= "")
         CrosshairStatic.setVisible(true);
   }
   else if(%option == 2)
   {
      Crosshair.drawCircle = true;
      if(%arg1 !$= "") Crosshair.circleLineWidth = %arg1;
      if(%arg2 !$= "") Crosshair.circleSegments  = %arg2;
      if(%arg3 !$= "") Crosshair.circleAngle  = %arg3;
   }
   else if(%option == 3)
   {
      Crosshair.drawCross = true;
      if(%arg1 !$= "") Crosshair.crossLineWidth = %arg1;
      if(%arg2 !$= "") Crosshair.crossLineLength = %arg2;
      if(%arg3 !$= "") Crosshair.crossParts = %arg3;
   }
   else if(%option == 4)
   {
      // reserved for box stuff
   }
   else if(%option == 5)
   {
      Crosshair.drawBitmap = true;
      if(getSubStr(%arg1, 0, 2) $= "./")
         Crosshair.setBitmap("share/hud/" @ %arg1);
      else
         Crosshair.setBitmap(%arg1);
   }
   else if(%option == 6)
   {
      if(getSubStr(%arg1, 0, 2) $= "./")
         %bmp = "share/hud/" @ %arg1;
      else
         %bmp = %arg1;
      %width = %arg2;
      %height = %arg2;
      if(%arg3 !$= "")
         %height = %arg3;
      CrosshairStatic.setBitmap(%bmp);
      CrosshairStatic.setExtent(%width, %height);
   }
}


