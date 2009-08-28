//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

// The global action map is created by the engine and always active.

//------------------------------------------------------------------------------
// misc...
//------------------------------------------------------------------------------

GlobalActionMap.bind(keyboard, "tilde", toggleConsole);
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
