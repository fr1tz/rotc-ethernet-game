//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - messages.cs
// code to handle misc. server messages
//------------------------------------------------------------------------------

$WeaponIcons[0] = "icon.assaultrifle.png";
$WeaponIcons[1] = "icon.sniperrifle.png";

//------------------------------------------------------------------------------

function NumDiscsMessageCallback(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10)
{
	Hud.numDiscs = %a1;
	Hud.updateDiscIcons();
}

addMessageCallback('MsgNumDiscs', NumDiscsMessageCallback);

//------------------------------------------------------------------------------

function GrenadeAmmoMessageCallback(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10)
{
    GrenadeAmmo.setValue(%a1);
}

addMessageCallback('MsgGrenadeAmmo', GrenadeAmmoMessageCallback);

function GrenadeAmmoDtMessageCallback(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10)
{
    %interval = %a1;
    %delta = %a2;

    GrenadeAmmo.interval = %interval;
    GrenadeAmmo.dt = %delta;
    Hud.updateGrenadeAmmo();
}

addMessageCallback('MsgGrenadeAmmoDt', GrenadeAmmoDtMessageCallback);

//------------------------------------------------------------------------------

function HeatMessageCallback(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10)
{
    Heat.setValue(%a1);
    Heat.dt = 0;
    Hud.updateHeat();
    Heat.dt = %a2;
}

addMessageCallback('MsgHeat', HeatMessageCallback);

//------------------------------------------------------------------------------

function WeaponSelectedMessageCallback(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10)
{
	%pixmap = "game/client/ui/hud/pixmaps/" @ $WeaponIcons[%a1];

	PrimaryWeaponIcon.setBitmap(%pixmap);
}

addMessageCallback('MsgWeaponUsed', WeaponSelectedMessageCallback);

//------------------------------------------------------------------------------

function WarningMessageCallback(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10)
{
	%slot = %a1;
	%string = %a2;
	%visible = %a3;

	switch(%slot)
	{
		case 1:
			if(%string !$= "") HudWarning1.setText(%string);
			if(%visible !$= "") HudWarning1.setVisible(%visible);

		case 2:
			if(%string !$= "") HudWarning2.setText(%string);
			if(%visible !$= "") HudWarning2.setVisible(%visible);

		case 3:
			if(%string !$= "") HudWarning3.setText(%string);
			if(%visible !$= "") HudWarning3.setVisible(%visible);

		case 4:
			if(%string !$= "") HudWarning4.setText(%string);
			if(%visible !$= "") HudWarning4.setVisible(%visible);

		case 5:
			if(%string !$= "") HudWarning5.setText(%string);
			if(%visible !$= "") HudWarning5.setVisible(%visible);

		case 6:
			if(%string !$= "") HudWarning6.setText(%string);
			if(%visible !$= "") HudWarning6.setVisible(%visible);
	}
}

addMessageCallback('MsgWarning', WarningMessageCallback);

//------------------------------------------------------------------------------
