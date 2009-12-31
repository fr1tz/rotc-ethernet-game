//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - misc.cs
// client-side functions which are too small to jusitfy an own file for them
//------------------------------------------------------------------------------

function clientCmdSetTimeScale(%x)
{
	$timeScale = %x;
}

//-----------------------------------------------------------------------------
// weapons stuff...
//-----------------------------------------------------------------------------

function clientCmdSetCrosshair(%crosshair)
{
// TODO: I think this is not needed anymore - mag
	//Crosshair.setBitmap("base/client/ui/hud/pixmaps/crosshairs/" @ %crosshair);
}

//-----------------------------------------------------------------------------
// transform stuff...
//-----------------------------------------------------------------------------

// TODO: I think this is not needed anymore... - mag
function clientCmdShowTransformDlg(%data)
{
	TransformDlgText.setText("");
	for(%i=0; %data.transform[%i] !$= ""; %i++)
		TransformDlgText.addText(%data.transform[%i] SPC "\n",true);
	Canvas.pushDialog(TransformDlg);
}

//-----------------------------------------------------------------------------
// progressHud stuff...
//-----------------------------------------------------------------------------

function updateProgressHud()
{
	%delta = 1/ProgressHud.maxTime*50;
	%value = ProgressHud.getValue()+%delta;
	if(%value > 1)
		%value = 1;
	ProgressHud.setValue(%value);
	ProgressHudTxt.setValue(%value);
	$ProgressHudThread = schedule(50,0,"updateProgressHud");
}

function clientCmdStartProgressHud(%maxTime)
{
	cancel($ProgressHudThread);
	ProgressHud.setValue(0);
	ProgressHudTxt.setValue(0);
	ProgressHud.maxTime = %maxTime;
	$ProgressHudThread = schedule(50,0,"updateProgressHud");
	ProgressHud.visible = true;
	ProgressHudTxt.visible = true;
}

function clientCmdStopProgressHud()
{
	ProgressHud.setValue(0);
	ProgressHudTxt.setValue("");
	cancel($ProgressHudThread);
	ProgressHud.visible = false;
	ProgressHudTxt.visible = false;
}

//-----------------------------------------------------------------------------
// loadout stuff...
//-----------------------------------------------------------------------------

function selectMainWeapon(%weapon)
{
	commandToServer('SelectMainWeapon',%weapon);
	Canvas.popDialog(SelectLoadoutDlg);
}

//-----------------------------------------------------------------------------
// Eyecandy stuff...
//-----------------------------------------------------------------------------

function CurrentZoneMessageCallback(%msgType, %msgString, %zoneTeamId)
{
	switch(%zoneTeamId)
	{
		case 0:  $sky.changeColor("1 1 1");
		case 1:  $sky.changeColor("1 0 0");
		case 2:  $sky.changeColor("0 0 1");
		default: $sky.changeColor("1 1 1");
	}
}

addMessageCallback('MsgCurrentZone', CurrentZoneMessageCallback);
