//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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
   CmdrScreen.camLeftAction = %val;
}

function cmdrscreen_panRight(%val)
{
   CmdrScreen.camRightAction = %val;
}

function cmdrscreen_panUp(%val)
{
   CmdrScreen.camUpAction = %val;
}

function cmdrscreen_panDown(%val)
{
   CmdrScreen.camDownAction = %val;
}

CmdrScreenActionMap.bind( keyboard, a, cmdrscreen_panLeft );
CmdrScreenActionMap.bind( keyboard, d, cmdrscreen_panRight );
CmdrScreenActionMap.bind( keyboard, w, cmdrscreen_panUp );
CmdrScreenActionMap.bind( keyboard, s, cmdrscreen_panDown );

//------------------------------------------------------------------------------
// zoom...
//------------------------------------------------------------------------------

function cmdrscreen_mouseZoom(%val)
{
   %min = 0.1;
   %max = 2.0;

	%step = (%max - %min)/$Pref::Player::MouseZoomSteps;

	if(%val > 0)
		CmdrScreen.mouseZoom -= %step;
	else
		CmdrScreen.mouseZoom += %step;

	if(CmdrScreen.mouseZoom < %min)
		CmdrScreen.mouseZoom = %min;
	else if(CmdrScreen.mouseZoom > %max)
		CmdrScreen.mouseZoom = %max;

   CmdrScreen.zoom(CmdrScreen.mouseZoom);
}

function cmdrscreen_zoomIn(%val)
{
   CmdrScreen.camForwardAction = %val;
}

function cmdrscreen_zoomOut(%val)
{
   CmdrScreen.camBackwardAction = %val;
}

CmdrScreenActionMap.bind(mouse, zaxis, cmdrscreen_mouseZoom);
CmdrScreenActionMap.bind(keyboard, e, cmdrscreen_zoomIn);
CmdrScreenActionMap.bind(keyboard, q, cmdrscreen_zoomOut);


