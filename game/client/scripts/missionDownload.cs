//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Mission Loading & Mission Info
// The mission loading server handshaking is handled by the
// common/client/missingLoading.cs.  This portion handles the interface
// with the game GUI.
//----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Pre Phase 1
//----------------------------------------------------------------------------

function onConnectionInitiated()
{
	// Reset all the loading stuff...
	LoadingProgressTxt.setText("WAITING FOR SERVER");
	IngameMenuReturn.setActive(false);
	IngameMenuText.setText("");

	addWindow(MissionWindow);
	//addWindow(ServerMessagesWindow);
	MissionWindow.resizeIdeal();
}

//----------------------------------------------------------------------------
// Loading Phases:
// Phase 1: Download Datablocks
// Phase 2: Download Ghost Objects
// Phase 3: TacticalZones grid computation / Scene Lighting

//----------------------------------------------------------------------------
// Phase 1
//----------------------------------------------------------------------------

function onMissionDownloadPhase1(%missionEnvFile, %musicTrack)
{
	// Close and clear the message hud (in case it's open)
	MessageHud.close();

	// Make sure we're displaying the shell
	Canvas.setContent(Shell);

	// Reset the loading progress controls:
	LoadingProgress.setValue(0);
	LoadingProgressTxt.setValue("LOADING DATABLOCKS");
	IngameMenuReturn.setActive(false);

	addWindow(MissionWindow);
	//addWindow(ServerMessagesWindow);	
}

function onPhase1Progress(%progress)
{
	LoadingProgress.setValue(%progress);
	Canvas.repaint();
}

function onPhase1Complete()
{
}

//----------------------------------------------------------------------------
// Phase 2
//----------------------------------------------------------------------------

function onMissionDownloadPhase2()
{
	// Reset the loading progress controls:
	LoadingProgress.setValue(0);
	LoadingProgressTxt.setValue("LOADING OBJECTS");
	Canvas.repaint();
}

function onPhase2Progress(%progress)
{
	LoadingProgress.setValue(%progress);
	Canvas.repaint();
}

function onPhase2Complete()
{
}	

function onFileChunkReceived(%fileName, %ofs, %size)
{
	LoadingProgress.setValue(%ofs / %size);
	LoadingProgressTxt.setValue("Downloading " @ %fileName @ "...");
}

//----------------------------------------------------------------------------
// Phase 3
//----------------------------------------------------------------------------

function onMissionDownloadPhase3()
{
	LoadingProgress.setValue(0);
	Canvas.repaint();
}

function onPhase3Progress(%subphase_name, %progress)
{
	LoadingProgressTxt.setValue(%subphase_name);
	LoadingProgress.setValue(%progress);
}

function onPhase3Complete()
{
	LoadingProgress.setValue( 1 );
	$lightingMission = false;
}

//----------------------------------------------------------------------------
// Mission loading done!
//----------------------------------------------------------------------------

function onMissionDownloadComplete()
{
	// Client will shortly be dropped into the game, so this is
	// good place for any last minute gui cleanup.
	%mapBitmap = filePath($Client::MissionEnvironmentFile) @ "/map.png";
	MiniMap.setMapBitmap(%mapBitmap);
	BigMap.setMapBitmap(%mapBitmap);

	LoadingProgressTxt.setValue(replaceBindVars("Loading done, press @bind01 to play."));
	LoadingProgress.setValue(0);

	// Enable player to play...
	IngameMenuReturn.setActive(true);
}


//------------------------------------------------------------------------------
// Before downloading a mission, the server transmits the mission
// information through these messages.
//------------------------------------------------------------------------------

addMessageCallback( 'MsgMapInfoBasics', handleMapInfoBasicsMessage );
addMessageCallback( 'MsgMapDesc', handleMapDescMessage );
addMessageCallback( 'MsgMapCopyright', handleMapCopyrightMessage );
addMessageCallback( 'MsgMapLicense', handleMapLicenseMessage );
addMessageCallback( 'MsgMapCredits', handleMapCreditsMessage );
addMessageCallback( 'MsgMapInfoDone', handleMapInfoDoneMessage );

//------------------------------------------------------------------------------

function handleMapInfoBasicsMessage(%msgType, %msgString, %mapName, %mapHomepage)
{
	LOAD_Title.setText($ServerInfo::Name);
	
	MapInfoWindow_MapName.setText(%mapName);
	MapInfoWindow_MapHomepage.setText(
		"<a:" @ %mapHomepage @ ">" @ %mapHomepage @ "</a>");
		
	MapInfoWindow_Button_Desc.setActive(false);
	MapInfoWindow_Button_Copyright.setActive(false);
	MapInfoWindow_Button_License.setActive(false);
	MapInfoWindow_Button_Credits.setActive(false);
	
	MapDescWindow_Text.setText("");
	MapCopyrightWindow_Text.setText("");
	MapLicenseWindow_Text.setText("");
	MapCreditsWindow_Text.setText("");
}

//------------------------------------------------------------------------------

function handleMapDescMessage(%msgType, %msgString, %line)
{
	MapDescWindow_Text.addText(%line, true);
}

function handleMapCopyrightMessage(%msgType, %msgString, %line)
{
	MapCopyrightWindow_Text.addText(%line, true);
}

function handleMapLicenseMessage(%msgType, %msgString, %line)
{
	MapLicenseWindow_Text.addText(%line, true);
}

function handleMapCreditsMessage(%msgType, %msgString, %line)
{
	MapCreditsWindow_Text.addText(%line, true);
}

//------------------------------------------------------------------------------

function handleMapInfoDoneMessage(%msgType, %msgString)
{
	// This will get called after the map info is sent.

	if(MapDescWindow_Text.getText() !$= "")
		MapInfoWindow_Button_Desc.setActive(true);

	if(MapCopyrightWindow_Text.getText() !$= "")
		MapInfoWindow_Button_Copyright.setActive(true);
		
	if(MapLicenseWindow_Text.getText() !$= "")
		MapInfoWindow_Button_License.setActive(true);
		
	if(MapCreditsWindow_Text.getText() !$= "")
		MapInfoWindow_Button_Credits.setActive(true);
}
