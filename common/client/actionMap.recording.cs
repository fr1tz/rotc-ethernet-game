//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// binds when playing back a recording
//------------------------------------------------------------------------------

if(isObject(RecordingActionMap)) RecordingActionMap.delete();
new ActionMap(RecordingActionMap);

function recordingToggleFreelook(%val)
{
    if(!%val) return;
    if(Canvas.getContent() != RecordingFreelookGui.getId())
    {
        $canvasContentStore = Canvas.getContent();
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
    else
    {
        Canvas.setContent($canvasContentStore);
        schedule(0, 0, "pushActionMap", RecordingActionMap);
        ServerConnection.setFirstPerson($firstPerson);
    }
    
}

function recordingTogglePerspective(%val)
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

function recordingTogglePause(%val)
{
    if(!%val) return;
    if($timeScale != 0)
    {
        $timeScale = 0;
        $recordingPaused = true;
    }
    else
    {
        recordingChangePlaySpeed($unpausedTimeScale);
        $recordingPaused = false;
    }
}

function recordingChangePlaySpeed(%scale)
{
    $unpausedTimeScale = %scale;
    $timeScale = %scale;
    RecordingFreelookGui.camMovementSpeed = 40 * %scale;
}

function recordingResetPlaySpeed()
{
    $unpausedTimeScale = 1.0;
    if($recordingPaused)
        $timeScale = 0;
    else
        $timeScale = $unpausedTimeScale;
    RecordingFreelookGui.camMovementSpeed = 40;
}

function recordingMoveleft(%val)
{
	RecordingFreelookGui.camLeftAction = %val;
}

function recordingMoveRight(%val)
{
	RecordingFreelookGui.camRightAction = %val;
}

function recordingMoveForward(%val)
{
	RecordingFreelookGui.camForwardAction = %val;
}

function recordingMoveBackward(%val)
{
	RecordingFreelookGui.camBackwardAction = %val;
}

function recordingGetMouseAdjustAmount(%val)
{
	// based on a default camera fov of 110'
	return(%val * ($cameraFov / 110) * 0.01);
}

function recordingYaw(%val)
{
	RecordingFreelookGui.camYaw += recordingGetMouseAdjustAmount(%val);
}

function recordingPitch(%val)
{
	RecordingFreelookGui.camPitch += recordingGetMouseAdjustAmount(%val);
}

RecordingActionMap.bind("keyboard", "tab", "recordingToggleFreelook");
RecordingActionMap.bind("keyboard", "q", "recordingTogglePerspective");
RecordingActionMap.bind("keyboard", "space", "recordingTogglePause");

RecordingActionMap.bind("keyboard", "a", "recordingMoveLeft" );
RecordingActionMap.bind("keyboard", "d", "recordingMoveRight" );
RecordingActionMap.bind("keyboard", "w", "recordingMoveForward" );
RecordingActionMap.bind("keyboard", "s", "recordingMoveBackward" );
RecordingActionMap.bind("mouse0", "xaxis", "S", $pref::Input::MouseSensitivity, "recordingYaw");
RecordingActionMap.bind("mouse0", "yaxis", "S", $pref::Input::MouseSensitivity, "recordingPitch");

RecordingActionMap.bindCmd("mouse", "button0", "recordingChangePlaySpeed(0.1);", "recordingResetPlaySpeed();");
RecordingActionMap.bindCmd("mouse", "button1", "recordingChangePlaySpeed(0.5);", "recordingResetPlaySpeed();");
RecordingActionMap.bindCmd("mouse", "button2", "recordingChangePlaySpeed(0.25);", "recordingResetPlaySpeed();");

RecordingActionMap.bindCmd("keyboard", "1", "recordingChangePlaySpeed(0.1);", "recordingResetPlaySpeed();");
RecordingActionMap.bindCmd("keyboard", "2", "recordingChangePlaySpeed(0.25);", "recordingResetPlaySpeed();");
RecordingActionMap.bindCmd("keyboard", "3", "recordingChangePlaySpeed(0.5);", "recordingResetPlaySpeed();");
RecordingActionMap.bindCmd("keyboard", "4", "recordingChangePlaySpeed(10);", "recordingResetPlaySpeed();");
RecordingActionMap.bindCmd("keyboard", "5", "recordingChangePlaySpeed(4);", "recordingResetPlaySpeed();");
RecordingActionMap.bindCmd("keyboard", "6", "recordingChangePlaySpeed(2);", "recordingResetPlaySpeed();");
