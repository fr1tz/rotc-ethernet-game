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
// Sky colorization
//-----------------------------------------------------------------------------

function SkyColorMsgCallback(%msgType, %msgString, %color)
{
	$sky.changeColor(%color);
}

addMessageCallback('MsgSkyColor', SkyColorMsgCallback);

//-----------------------------------------------------------------------------
// Music
//-----------------------------------------------------------------------------

function clientPlayMusic(%msgType, %msgString, %profileId, %immediately)
{
	echo("clientPlayMusic()" SPC %profileId SPC %immediately);
	%profile = %profileId.getName();
	if(%profile $= "")
	{
		%name = "Music" @ %profileId;
		%profileId.setName(%name);
		%profile = %name;
	}

	if(!alxIsPlaying($CMusic::Handle) || (%immediately && $CMusic::CurrProfile !$= %profile))
	{
		alxStop($CMusic::Handle);
		%len = alxGetWaveLen(%profile.filename);
		if(%len > 0)
		{
			cancel($CMusic::UpdateThread);
			$CMusic::UpdateThread = schedule(%len, 0, "clientUpdateMusic");
			$CMusic::Handle = alxPlay(%profile);
			$CMusic::CurrProfile = %profile;
		}
	}

	$CMusic::NextProfile = %profile;
}

addMessageCallback('MsgMusic', clientPlayMusic);

function clientUpdateMusic()
{
	echo("clientUpdateMusic()" SPC $CMusic::CurrProfile SPC "->" SPC $CMusic::NextProfile);
	cancel($CMusic::UpdateThread);
	if($CMusic::NextProfile !$= $CMusic::CurrProfile)
	{
		alxStop($CMusic::Handle);
		%len = alxGetWaveLen($CMusic::NextProfile.filename);
		if(%len > 0)
		{
			$CMusic::UpdateThread = schedule(%len, 0, "clientUpdateMusic");
			$CMusic::Handle = alxPlay($CMusic::NextProfile);
			$CMusic::CurrProfile = $CMusic::NextProfile;
		}
	}
	else
	{
		%len = alxGetWaveLen($CMusic::CurrProfile.filename);
		if(%len > 0)
			$CMusic::UpdateThread = schedule(%len, 0, "clientUpdateMusic");
	}
}

//-----------------------------------------------------------------------------
// Game balance
//-----------------------------------------------------------------------------

function GameBalanceMsgCallback(%msgType, %msgString, %balance)
{
	HudGameBalance.setText(%balance);
}

addMessageCallback('MsgGameBalance', GameBalanceMsgCallback);
