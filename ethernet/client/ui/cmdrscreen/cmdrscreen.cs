//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

function CmdrScreen::onAdd(%this)
{
	// start at center...
	%this.currPanX = 0;
	%this.currPanY = 0;
	%this.currZoom = 1.0;

	// variables used to control pan and zoom...
	%this.changePanX = 0;
	%this.changePanY = 0;
	%this.changeZoom = 0;
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

	// activate commander screen controls...
    pushActionMap(CmdrScreenActionMap);
	%this.controlThread = %this.schedule(0, "updatePanAndZoom");
	
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
	cancel(%this.controlThread);
}

function CmdrScreen::updatePanAndZoom(%this)
{
	%damp = %this.currZoom < 1 ? %this.currZoom : 1;

	%speed = $Pref::Commander::PanSpeed * %damp;
	%this.currPanX += %this.changePanX * %speed;
	%this.currPanY += %this.changePanY * %speed;
	%this.panTo(%this.currPanX, %this.currPanY);

	%this.currZoom += %this.changeZoom * $Pref::Commander::ZoomSpeed * %damp;
	%this.clampZoom();
	%this.zoomTo(%this.currZoom);

	%this.controlThread = %this.schedule(25, "updatePanAndZoom");
}

function CmdrScreen::panLeft(%this, %val)
{
	if(%val)
		%this.changePanX--;
	else
		%this.changePanX++;
}

function CmdrScreen::panRight(%this, %val)
{
	if(%val)
		%this.changePanX++;
	else
		%this.changePanX--;
}

function CmdrScreen::panUp(%this, %val)
{
	if(%val)
		%this.changePanY++;
	else
		%this.changePanY--;
}

function CmdrScreen::panDown(%this, %val)
{
	if(%val)
		%this.changePanY--;
	else
		%this.changePanY++;
}

function CmdrScreen::zoom(%this, %val)
{
	%this.currZoom += $Pref::Commander::ZoomSpeed * (%val > 0 ? -1 : 1);
	%this.clampZoom();
	%this.zoomTo(%this.currZoom);
}

function CmdrScreen::zoomIn(%this, %val)
{
	if(%val)
		%this.changeZoom--;
	else
		%this.changeZoom++;
}

function CmdrScreen::zoomOut(%this, %val)
{
	if(%val)
		%this.changeZoom++;
	else
		%this.changeZoom--;
}

function CmdrScreen::clampZoom(%this)
{
	if(%this.currZoom < 0.01)
		%this.currZoom = 0.01;
	else if(%this.currZoom > 3)
		%this.currZoom = 3;
}



