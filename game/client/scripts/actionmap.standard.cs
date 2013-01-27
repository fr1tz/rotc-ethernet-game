//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// default input config
//------------------------------------------------------------------------------

if( isObject( MoveMap ) )
	MoveMap.delete();
	
new ActionMap(MoveMap);

//------------------------------------------------------------------------------
// In-game shell overlay
//------------------------------------------------------------------------------

MoveMap.bind( keyboard, "escape", toggleShellDlg );

//------------------------------------------------------------------------------
// camera & view...
//------------------------------------------------------------------------------

MoveMap.bind(keyboard, "lessthan", freeLook );
MoveMap.bind(keyboard, "p", toggleFirstPerson );

//------------------------------------------------------------------------------
// movement...
//------------------------------------------------------------------------------

MoveMap.bind( keyboard, "a", moveleft );
MoveMap.bind( keyboard, "d", moveright );
MoveMap.bind( keyboard, "w", moveforward );
MoveMap.bind( keyboard, "s", movebackward );
MoveMap.bind(mouse0, "xaxis", S, $pref::Input::MouseSensitivity, yaw);
MoveMap.bind(mouse0, "yaxis", S, $pref::Input::MouseSensitivity, pitch);

//------------------------------------------------------------------------------
// triggers...
//------------------------------------------------------------------------------

MoveMap.bind( mouse, "button0", trigger0 ); // left button
MoveMap.bind( mouse, "button2", trigger3 ); // middle button
MoveMap.bind( mouse, "button1", trigger1 ); // right button
MoveMap.bind( keyboard, "space", trigger2 );
MoveMap.bind( keyboard, "lshift", trigger4 );
MoveMap.bind( keyboard, "lcontrol", trigger5 );

//------------------------------------------------------------------------------
// Zoom and FOV
//------------------------------------------------------------------------------

MoveMap.bind( keyboard, "shift f", toggleTempZoomLevel );
MoveMap.bind( keyboard, "f", tempZoom );
MoveMap.bind(mouse, "zaxis", mouseZoom);

//------------------------------------------------------------------------------
// Message HUD
//------------------------------------------------------------------------------

MoveMap.bind(keyboard, "t", toggleMessageHud );
MoveMap.bind(keyboard, "shift t", teamMessageHud );
MoveMap.bind(keyboard, "shift pageUp", pageMessageHudUp );
MoveMap.bind(keyboard, "shift pageDown", pageMessageHudDown );
MoveMap.bind(keyboard, "i", resizeMessageHud );

//------------------------------------------------------------------------------
// Misc
//------------------------------------------------------------------------------

MoveMap.bind(keyboard, "y", biggerMiniMap );
MoveMap.bind(keyboard, "c", activateCmdrScreen );
MoveMap.bind(keyboard, "alt r", toggleRecordingDemo);
MoveMap.bind(keyboard, "alt p", takeScreenshot);

//------------------------------------------------------------------------------
// Player actions
//------------------------------------------------------------------------------

MoveMap.bind( keyboard, "tab", action0 );
MoveMap.bind( keyboard, "1", action1 );
MoveMap.bind( keyboard, "2", action2 );
MoveMap.bind( keyboard, "3", action3 );
MoveMap.bind( keyboard, "4", action4 );
MoveMap.bind( keyboard, "5", action5 );
MoveMap.bind( keyboard, "6", action6 );
MoveMap.bind( keyboard, "7", action7 );
MoveMap.bind( keyboard, "8", action8 );
MoveMap.bind( keyboard, "9", action9 );
MoveMap.bind( keyboard, "0", action10);
MoveMap.bind( keyboard, "q", action11);
moveMap.bind( mouse0, "shift button2", action12);
MoveMap.bind( keyboard, "e", action13 );
MoveMap.bind( keyboard, "r", action14 );
MoveMap.bind( keyboard, "o", action15 );
MoveMap.bind( keyboard, "g", action16 );
MoveMap.bind( keyboard, "x", action17 );
MoveMap.bind( keyboard, "v", action18 );
MoveMap.bind( keyboard, "b", action19 );
MoveMap.bind( keyboard, "m", action20 );
MoveMap.bind( keyboard, "shift 1", action21 );
MoveMap.bind( keyboard, "shift 2", action22 );
MoveMap.bind( keyboard, "shift 3", action23 );
MoveMap.bind( keyboard, "shift 4", action24 );
MoveMap.bind( keyboard, "shift 5", action25 );
MoveMap.bind( keyboard, "shift 6", action26 );
MoveMap.bind( keyboard, "shift 7", action27 );
MoveMap.bind( keyboard, "shift 8", action28 );
MoveMap.bind( keyboard, "shift 9", action29 );
MoveMap.bind( keyboard, "shift 0", action30 );
MoveMap.bind( keyboard, "F1", action31 );
MoveMap.bind( keyboard, "F2", action32 );
MoveMap.bind( keyboard, "F3", action33 );
MoveMap.bind( keyboard, "F4", action34 );
MoveMap.bind( keyboard, "F5", action35 );
MoveMap.bind( keyboard, "F6", action36 );
MoveMap.bind( keyboard, "F7", action37 );
MoveMap.bind( keyboard, "F8", action38 );
MoveMap.bind( keyboard, "alt c", action39 );