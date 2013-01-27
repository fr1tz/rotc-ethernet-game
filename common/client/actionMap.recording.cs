//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// binds when playing back a recording
//------------------------------------------------------------------------------

if(isObject(RecordingActionMap)) RecordingActionMap.delete();
new ActionMap(RecordingActionMap);

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

RecordingActionMap.bindCmd(keyboard, "escape", "", "toggleShellDlg();");

RecordingActionMap.bind("keyboard", "h", "demoSetViewHud");
RecordingActionMap.bind("keyboard", "f", "demoSetViewFree");
RecordingActionMap.bind("keyboard", "c", "demoSetViewCmdr");
RecordingActionMap.bind("keyboard", "p", "demoTogglePerspective");

RecordingActionMap.bind("keyboard", "space", "demoTogglePause");

RecordingActionMap.bind("keyboard", "j", "demoJumpTo");

RecordingActionMap.bind("keyboard", "a", "recordingMoveLeft" );
RecordingActionMap.bind("keyboard", "d", "recordingMoveRight" );
RecordingActionMap.bind("keyboard", "w", "recordingMoveForward" );
RecordingActionMap.bind("keyboard", "s", "recordingMoveBackward" );
RecordingActionMap.bind("mouse0", "xaxis", "S", $pref::Input::MouseSensitivity, "recordingYaw");
RecordingActionMap.bind("mouse0", "yaxis", "S", $pref::Input::MouseSensitivity, "recordingPitch");
RecordingActionMap.bind("mouse0", "zaxis", mouseZoom);

RecordingActionMap.bindCmd("mouse", "button0", "demoChangePlaySpeed(1);", "demoResetPlaySpeed();");
RecordingActionMap.bindCmd("mouse", "button1", "demoChangePlaySpeed(2);", "demoResetPlaySpeed();");
RecordingActionMap.bindCmd("mouse", "button2", "demoChangePlaySpeed(3);", "demoResetPlaySpeed();");

RecordingActionMap.bindCmd("keyboard", "1", "demoChangePlaySpeed(1);", "demoResetPlaySpeed();");
RecordingActionMap.bindCmd("keyboard", "2", "demoChangePlaySpeed(2);", "demoResetPlaySpeed();");
RecordingActionMap.bindCmd("keyboard", "3", "demoChangePlaySpeed(3);", "demoResetPlaySpeed();");
RecordingActionMap.bindCmd("keyboard", "4", "demoChangePlaySpeed(4);", "demoResetPlaySpeed();");
RecordingActionMap.bindCmd("keyboard", "5", "demoChangePlaySpeed(5);", "demoResetPlaySpeed();");
RecordingActionMap.bindCmd("keyboard", "6", "demoChangePlaySpeed(6);", "demoResetPlaySpeed();");
