//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//------------------------------------------------------------------------------
// RecordingsWindow GUI stuff...

function RecordingsWindow::onWake(%this)
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

//------------------------------------------------------------------------------
// RecordingControlsWindow GUI stuff...

function RecordingControlsWindow::onWake(%this)
{
	%this.updateDemoCurrentPosition();
}

function RecordingControlsWindow::onSleep(%this)
{
	cancel(%this.demoPositionThread);
}

function RecordingControlsWindow::updateDemoCurrentPosition(%this)
{
	updateDemoCurrentPosition();
	%this.demoPositionThread = %this.schedule(50, "updateDemoCurrentPosition");		
}

//------------------------------------------------------------------------------
// RecordingSettingsWindow GUI stuff...

function RecordingControlsWindow::onWake(%this)
{

}

function RecordingSettingsWindow::onSleep(%this)
{
    RecordingFreelookGui.camMovementSpeed =
		$Pref::Recording::FreelookMoveSpeed;
}

//------------------------------------------------------------------------------
// Demo playback...

function demoSetViewHud(%val)
{
    if(!%val || Canvas.getContent() == HUD.getId())
		return;

	Canvas.setContent(HUD);
	ServerConnection.setFirstPerson($firstPerson); 
}

function demoSetViewFree(%val)
{
    if(!%val || Canvas.getContent() == RecordingFreelookGui.getId())
		return;

	Canvas.setContent(RecordingFreelookGui);
        
	if(RecordingFreelookGui.getCameraMode() == 0)
	{
		RecordingFreelookGui.camZoomToPlayer();
 		ServerConnection.setFirstPerson(true);
	}
	else
	{
		ServerConnection.setFirstPerson(false);
	}   
}

function demoSetViewCmdr(%val)
{
    if(!%val || Canvas.getContent() == CmdrScreen.getId())
		return;

	Canvas.setContent(CmdrScreen);
}

function demoTogglePerspective(%val)
{
    if(!%val) return;
    
    if(Canvas.getContent() == RecordingFreelookGui.getId())
    {
        RecordingFreelookGui.camZoomToPlayer();
    
        if(RecordingFreelookGui.getCameraMode() == 0)
        {
            RecordingFreelookGui.setCameraFlyMode();
            ServerConnection.setFirstPerson(false);
        }
        else
        {
            RecordingFreelookGui.setCameraPlayerMode();
            ServerConnection.setFirstPerson(true);
        }
    }
    else
    {
        toggleFirstPerson(1);
    }
}

function demoTogglePause(%val)
{
    if(!%val) return;
    if($timeScale != 0)
    {
        $timeScale = 0;
        $DemoPaused = true;
		$DemoStatus = "PAUSED";
    }
    else
    {
        $DemoPaused = false;
		$DemoStatus = "PLAYING";
		demoSetPlaySpeed($DemoSpeed);
    }

	updateDemoCurrentPosition();
}

function demoSetPlaySpeed(%scale)
{
	$DemoSpeed = %scale;
	RecordingFreelookGui.camMovementSpeed = 
		$Pref::Recording::FreelookMoveSpeed * %scale;
	$timeScale = %scale;
}

function demoChangePlaySpeed(%preset, %honorPause)
{
	%scale = $Pref::Recording::SpeedPreset[%preset];
	$DemoSpeed = %scale;
	RecordingFreelookGui.camMovementSpeed = 
		$Pref::Recording::FreelookMoveSpeed * %scale;
	$timeScale = %scale;
	if(!$DemoPaused || !%honorPause)
		$timeScale = %scale;
}

function demoResetPlaySpeed()
{
    $DemoSpeed = 1.0;
    if($DemoPaused)
        $timeScale = 0;
    else
        $timeScale = $DemoSpeed;
    RecordingFreelookGui.camMovementSpeed =
		$Pref::Recording::FreelookMoveSpeed;
}

function demoJumpTo(%val)
{
    if(!%val || $DemoFileName $= "") 
		return;

	%position = recordingJumpTo.getText();
	startDemoPlayback($DemoFileName, %position);
}


function updateDemoCurrentPosition()
{
	$DemoCurrentPosition = getSimTime() - $DemoStartTime;
}

function onDemoLoaded()
{
	$timeScale = 0;
	$DemoPaused = true;
	$DemoStatus = "PAUSED";

	$ShellDlgActive = true;
	ServerConnection.prepDemoPlayback();
    pushActionMap(RecordingActionMap);
}

function demoSkipToTargetPosition()
{
	cancel($DemoSkipThread);

	updateDemoCurrentPosition();
	%delta = $DemoTargetPosition - $DemoCurrentPosition;

	%n = 25;	
	while(%n > 1)
	{
		if(%delta > 2 * %n * 1000)
		{
			$timeScale = %n;
			$DemoSkipThread = schedule(%delta / $timeScale, 0, "demoSkipToTargetPosition");
			return;
		}
		%n--;
	}

	$timeScale = 1;
	$DemoSkipThread = schedule(%delta / $timeScale, 0, "onDemoLoaded");
}

function startDemoPlayback(%file, %position)
{
    $DemoFileName = %file;

	disconnect();

    if(isObject(ServerConnection))
        ServerConnection.delete();

	new GameConnection(ServerConnection);
	RootGroup.add(ServerConnection);

	$timeScale = 0;
	$DemoStartTime = 0;
	$DemoCurrentPosition = 0;
	$DemoTargetPosition = %position;
	$DemoSpeed = 1;
	$DemoPaused = true;
	$DemoStatus = "LOADING...";

	RecordingJumpTo.setText($DemoTargetPosition);
	addWindow(RecordingControlsWindow);

	if(ServerConnection.playDemo($DemoFileName))
	{	
		$DemoStartTime = getSimTime();

        StartClientReplication();
        StartFoliageReplication();
        computeZoneGrids();
        
        if($DemoTargetPosition > 0)
        {
			demoSkipToTargetPosition();
        }
		else
		{
			onDemoLoaded();
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

    startDemoPlayback(%file, DR_JumpToPosition.getText());
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

function toggleDemoRecord()
{
	if(ServerConnection.isDemoRecording())
		stopDemoRecord();
	else
		startDemoRecord();
}







