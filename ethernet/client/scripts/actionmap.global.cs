//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

// The global action map is created by the engine and always active.

//------------------------------------------------------------------------------
// misc...
//------------------------------------------------------------------------------

function InputGrab_Reactivate(%control, %window)
{
	for(%i = 0; %i < %control.getCount(); %i++)
	{
		%obj = %control.getObject(%i);
		if(%obj == $window)
			continue;
		if(%obj.getCount() > 0)
			InputGrab_Reactivate(%obj);
		%obj.setActive(!%obj.wasInactiveBeforeInputGrab);
	}
}

function InputGrab_Deactivate(%control, %window)
{
	for(%i = 0; %i < %control.getCount(); %i++)
	{
		%obj = %control.getObject(%i);
		if(%obj == $window)
			continue;
		if(%obj.getCount() > 0)
			InputGrab_Deactivate(%obj);
		%obj.wasInactiveBeforeInputGrab = !%obj.isActive();
		%obj.setActive(false);
	}
}

function activateWindowInputGrab(%window)
{
	cursorOff();
	InputGrab_Deactivate(Canvas, %window);
}

function cancelInputGrab(%val)
{
	if(%val)
	{
		cursorOn();
		InputGrab_Reactivate(Canvas, %window);
	}
}

GlobalActionMap.bind(keyboard, "escape", cancelInputGrab);
GlobalActionMap.bind(keyboard, "tilde", toggleConsole);
GlobalActionMap.bind(keyboard, "F12", toggleConsole);
GlobalActionMap.bindCmd(keyboard, "alt enter", "", "toggleFullScreen();");
//GlobalActionMap.bindCmd(keyboard, "F1", "", "contextHelp();");

//------------------------------------------------------------------------------
// debugging...
//------------------------------------------------------------------------------

$MFDebugRenderMode = 0;
function cycleDebugRenderMode(%val)
{
	if (!%val)
		return;
	if($MFDebugRenderMode == 0)
	{
		// Outline mode, including fonts so no stats
		$MFDebugRenderMode = 1;
		GLEnableOutline(true);
	}
	else if ($MFDebugRenderMode == 1)
	{
		// Interior debug mode
		$MFDebugRenderMode = 2;
		GLEnableOutline(false);
		setInteriorRenderMode(7);
		showInterior();
	}
	else if ($MFDebugRenderMode == 2)
	{
		// Back to normal
		$MFDebugRenderMode = 0;
		setInteriorRenderMode(0);
		GLEnableOutline(false);
		show();
	}
}

GlobalActionMap.bind(keyboard, "F9", cycleDebugRenderMode);
