//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

if(isObject(CmdrScreenActionMap))
	CmdrScreenActionMap.delete();
	
new ActionMap(CmdrScreenActionMap);

//------------------------------------------------------------------------------

function cmdrscreen_deactivate(%val)
{
	if(%val)
		Canvas.setContent(Hud);
}

CmdrScreenActionMap.bind(keyboard, "c", cmdrscreen_deactivate );
CmdrScreenActionMap.bindCmd(keyboard, "escape", "", "toggleShellDlg();");

//------------------------------------------------------------------------------
// pan...
//------------------------------------------------------------------------------

function cmdrscreen_panLeft(%val)
{
	CmdrScreen.panLeft(%val);
}

function cmdrscreen_panRight(%val)
{
	CmdrScreen.panRight(%val);
}

function cmdrscreen_panUp(%val)
{
	CmdrScreen.panUp(%val);}

function cmdrscreen_panDown(%val)
{
	CmdrScreen.panDown(%val);
}

CmdrScreenActionMap.bind( keyboard, a, cmdrscreen_panLeft );
CmdrScreenActionMap.bind( keyboard, d, cmdrscreen_panRight );
CmdrScreenActionMap.bind( keyboard, w, cmdrscreen_panUp );
CmdrScreenActionMap.bind( keyboard, s, cmdrscreen_panDown );

//------------------------------------------------------------------------------
// zoom...
//------------------------------------------------------------------------------

function cmdrscreen_zoom(%val)
{
	CmdrScreen.zoom(%val);
}

function cmdrscreen_zoomIn(%val)
{
	CmdrScreen.zoomIn(%val);
}

function cmdrscreen_zoomOut(%val)
{
	CmdrScreen.zoomOut(%val);
}

CmdrScreenActionMap.bind(mouse, zaxis, cmdrscreen_zoom);
CmdrScreenActionMap.bind(keyboard, e, cmdrscreen_zoomIn);
CmdrScreenActionMap.bind(keyboard, q, cmdrscreen_zoomOut);

//------------------------------------------------------------------------------
// message HUD...
//------------------------------------------------------------------------------

CmdrScreenActionMap.bind(keyboard, "z", toggleMessageHud );
CmdrScreenActionMap.bind(keyboard, "t", teamMessageHud );
CmdrScreenActionMap.bind(keyboard, "shift pageUp", pageMessageHudUp );
CmdrScreenActionMap.bind(keyboard, "shift pageDown", pageMessageHudDown );
CmdrScreenActionMap.bind(keyboard, "i", resizeMessageHud );


