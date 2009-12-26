//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
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
	%pixmap = "ethernet/client/ui/hud/pixmaps/" @ $WeaponIcons[%a1];

	PrimaryWeaponIcon.setBitmap(%pixmap);
}

addMessageCallback('MsgWeaponUsed', WeaponSelectedMessageCallback);

//------------------------------------------------------------------------------


