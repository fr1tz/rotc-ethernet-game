//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

function executeShellScripts()
{
	exec("./shell/shell.cs");
	exec("./shell/shell.gui");
	exec("./shell/rootMenu.cs");
	exec("./shell/rootMenu.gui");
	exec("./shell/startServer.cs");
	exec("./shell/startServer.gui");
	exec("./shell/joinServer.cs");
	exec("./shell/joinServer.gui");
	exec("./shell/options.cs");
	exec("./shell/options.gui");
	exec("./shell/optPlayer.cs");
	exec("./shell/optPlayer.gui");
	exec("./shell/optGraphics.cs");
	exec("./shell/optGraphics.gui");
	exec("./shell/optAudio.cs");
	exec("./shell/optAudio.gui");
	exec("./shell/optNetwork.cs");
	exec("./shell/optNetwork.gui");
	exec("./shell/optControls.cs");
	exec("./shell/optControls.gui");
	exec("./shell/recordings.cs");
	exec("./shell/recordings.gui");
	exec("./shell/missileCommand.cs");
	exec("./shell/missileCommand.gui");
	exec("./shell/about.cs");
	exec("./shell/about.gui");
	exec("./shell/remapDlg.cs");
	exec("./shell/remapDlg.gui");
	exec("./shell/endGameGui.cs");
	exec("./shell/endGameGui.gui");
	exec("./shell/loadMap.cs");
	exec("./shell/loadMap.gui");
	exec("./shell/ingameMenu.cs");
	exec("./shell/ingameMenu.gui");
	exec("./shell/joinTeam.cs");
	exec("./shell/joinTeam.gui");
	exec("./shell/playerList.cs");
	exec("./shell/playerList.gui");
	exec("./shell/mapInfo.cs");
	exec("./shell/mapInfo.gui");
	exec("./shell/mapDesc.cs");
	exec("./shell/mapDesc.gui");
	exec("./shell/mapCopyright.cs");
	exec("./shell/mapCopyright.gui");
	exec("./shell/mapLicense.cs");
	exec("./shell/mapLicense.gui");
	exec("./shell/mapCredits.cs");
	exec("./shell/mapCredits.gui");
}

executeShellScripts();

function executeHudScripts()
{
	exec("./hud/hud.cs");
	exec("./hud/hud.gui");
	exec("./hud/chatHud.cs");
	exec("./hud/chatHud.gui");
	exec("./hud/messages.cs");
	exec("./hud/messageHud.cs");
	exec("./hud/simpleControl.cs");
	exec("./hud/animationListDlg.gui");
	exec("./hud/transformDlg.gui");
	exec("./hud/celAnimationDlg.gui");
	exec("./hud/teamplayDlg.gui");
	exec("./hud/selectLoadoutDlg.gui");
	exec("./hud/centerPrint.cs");
	exec("./hud/shellDlg.gui");
}

executeHudScripts();

function executeCmdrScreenScripts()
{
	exec("./cmdrscreen/cmdrscreen.cs");
	exec("./cmdrscreen/cmdrscreen.gui");
}

executeCmdrScreenScripts();


