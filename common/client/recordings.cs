//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Demo playback...

function fastForward(%seconds)
{
    $timeScale = %seconds;
    schedule(%seconds * 1000, 0, "pauseDemoPlayback", 1);
}

function pauseDemoPlayback()
{
    $timeScale = 0;
}

function startDemoPlayback(%file, %skipToSecond)
{
    $DemoFileName = %file;

    if(isObject(ServerConnection))
        ServerConnection.delete();

	new GameConnection(ServerConnection);
	RootGroup.add(ServerConnection);

	if(ServerConnection.playDemo($DemoFileName))
	{
        StartClientReplication();
        StartFoliageReplication();
        computeZoneGrids();
        
		ServerConnection.prepDemoPlayback();
  
        pushActionMap(RecordingActionMap);
        
        if(%skipToSecond > 0)
        {
            $timeScale = %skipToSecond;
            if($timeScale > 25) $timeScale = 25;
            schedule(%skipToSecond * 1000, 0, "pauseDemoPlayback", 1);
        }
	}
	else
	{
		MessageBoxOK("Playback Failed", "Demo playback failed for file '" @ %file @ "'.");
		if(isObject(ServerConnection)) {
			ServerConnection.delete();
		}
	}

}

function StartSelectedDemo()
{
	// first unit is filename
	%sel = RecordingsDlgList.getSelectedId();
	%rowText = RecordingsDlgList.getRowTextById(%sel);

	%file = $lastMod @ "/recordings/" @ getField(%rowText, 0) @ ".rec";

    startDemoPlayback(%file, 0);
}

function demoPlaybackComplete()
{
	disconnect();
    popActionMap(RecordingActionMap);
	Canvas.setContent("Shell");
}

//------------------------------------------------------------------------------
// Demo recording...

function startDemoRecord()
{
	// make sure that current recording stream is stopped
	ServerConnection.stopRecording();

	// make sure we aren't playing a demo
	if(ServerConnection.isDemoPlaying())
		return;

	for(%i = 0; %i < 1000; %i++)
	{
		%num = %i;
		if(%num < 10)
			%num = "0" @ %num;
		if(%num < 100)
			%num = "0" @ %num;

		%file = $lastMod @ "/recordings/"
			@ strreplace($Pref::Player::Name, "/", "")
			@ "-" @ %num @ ".rec";

		if(!isfile(%file))
			break;
	}
	if(%i == 1000)
		return;

	$DemoFileName = %file;

	ChatHud.AddLine( "\c4Recording to file [\c2" @ $DemoFileName @ "\cr].");

	ServerConnection.prepDemoRecord();
	ServerConnection.startRecording($DemoFileName);

	// make sure start worked
	if(!ServerConnection.isDemoRecording())
	{
		deleteFile($DemoFileName);
		ChatHud.AddLine( "\c3 *** Failed to record to file [\c2" @ $DemoFileName @ "\cr].");
		$DemoFileName = "";
	}
}

function stopDemoRecord()
{
	// make sure we are recording
	if(ServerConnection.isDemoRecording())
	{
		ChatHud.AddLine( "\c4Recording file [\c2" @ $DemoFileName @ "\cr] finished.");
		ServerConnection.stopRecording();
	}
}

//------------------------------------------------------------------------------
// GUI stuff...

function RecordingsWindow::onWake()
{
	RecordingsDlgList.clear();
	%i = 0;
	%filespec = $lastMod @ "/recordings/*.rec";
	echo(%filespec);
	for(%file = findFirstFile(%filespec); %file !$= ""; %file = findNextFile(%filespec)) 
	{ 
		%fileName = fileBase(%file);
		if (strStr(%file, "/CVS/") == -1) 
		{
			RecordingsDlgList.addRow(%i++, %fileName);
		}
	}
	RecordingsDlgList.sort(0);
	RecordingsDlgList.setSelectedRow(0);
	RecordingsDlgList.scrollVisible(0);
}





