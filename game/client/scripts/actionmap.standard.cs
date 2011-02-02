//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
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

MoveMap.bind(mouse, "zaxis", mouseZoom);

//------------------------------------------------------------------------------
// Message HUD
//------------------------------------------------------------------------------

MoveMap.bind(keyboard, "z", toggleMessageHud );
MoveMap.bind(keyboard, "t", teamMessageHud );
MoveMap.bind(keyboard, "shift pageUp", pageMessageHudUp );
MoveMap.bind(keyboard, "shift pageDown", pageMessageHudDown );
MoveMap.bind(keyboard, "i", resizeMessageHud );

//------------------------------------------------------------------------------
// Misc
//------------------------------------------------------------------------------

MoveMap.bind(keyboard, "y", biggerMiniMap );
MoveMap.bind(keyboard, "c", activateCmdrScreen );
MoveMap.bind( keyboard, "F3", startRecordingDemo );
MoveMap.bind( keyboard, "F4", stopRecordingDemo );

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
MoveMap.bind( keyboard, "alt c", action39 );