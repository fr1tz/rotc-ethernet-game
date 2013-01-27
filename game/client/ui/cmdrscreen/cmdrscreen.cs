//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function CmdrScreen::onAdd(%this)
{
   %this.mouseZoom = 1;
   %this.zoom(%this.mouseZoom);
}

function CmdrScreen::onWake(%this)
{
	// Turn off any shell sounds...
	// alxStop( ... );

	$enableDirectInput = "1";
	activateDirectInput();
	
	// "steal" ScannerHud
	%this.add(ScannerHud);

	// add message hud dialog...
	Canvas.pushDialog(MainChatHud);
	chatHud.attach(HudMessageVector);

   //
   %this.panSpeed = $Pref::Commander::PanSpeed;
   %this.zoomSpeed = $Pref::Commander::ZoomSpeed;

	// activate commander screen controls...
   pushActionMap(CmdrScreenActionMap);
	
	// warp to curr position...
	//%this.pan(%this.currPanX, %this.currPanY);
	//%this.zoom(%this.currZoom);
}

function CmdrScreen::onSleep(%this)
{
	Canvas.popDialog(MainChatHud);
	Canvas.popDialog(ScannerHud);

	// deactivate commander screen controls...
	popActionMap(CmdrScreenActionMap);
}



