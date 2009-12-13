//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// default input config
//------------------------------------------------------------------------------

if( isObject( MoveMap ) )
	MoveMap.delete();
	
new ActionMap(MoveMap);

//------------------------------------------------------------------------------
// non-remapable binds...
//------------------------------------------------------------------------------

function escapeFromGame()
{
	if ( $Server::ServerType $= "SinglePlayer" )
		MessageBoxYesNo( "Quit Mission", "Exit from this Mission?", "disconnect();", "");
	else
		MessageBoxYesNo( "Disconnect", "Disconnect from the server?", "disconnect();", "");
}

function toggleShellDlg()
{
	if(ShellDlg.isAwake())
		Canvas.popDialog(ShellDlg);
	else
	{
		Canvas.pushDialog(ShellDlg);
		addWindow(IngameMenuWindow);
	}
}

MoveMap.bindCmd(keyboard, "escape", "", "toggleShellDlg();");

MoveMap.bindCmd(keyboard, "shift i", "setTimeScale(2.0);", "");
MoveMap.bindCmd(keyboard, "shift o", "setTimeScale(1.0);", "");
MoveMap.bindCmd(keyboard, "shift p", "setTimeScale(0.2);", "");


//------------------------------------------------------------------------------
// movement...
//------------------------------------------------------------------------------

$movementSpeed = 1; // m/s

function setSpeed(%speed)
{
	if(%speed)
		$movementSpeed = %speed;
}

function moveleft(%val)
{
	$mvLeftAction = %val;
}

function moveright(%val)
{
	$mvRightAction = %val;
}

function moveforward(%val)
{
	$mvForwardAction = %val;
}

function movebackward(%val)
{
	$mvBackwardAction = %val;
}

function moveup(%val)
{
	$mvUpAction = %val;
}

function movedown(%val)
{
	$mvDownAction = %val;
}

function turnLeft( %val )
{
	$mvYawRightSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
}

function turnRight( %val )
{
	$mvYawLeftSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
}

function panUp( %val )
{
	$mvPitchDownSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
}

function panDown( %val )
{
	$mvPitchUpSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
}

function getMouseAdjustAmount(%val)
{
	// based on a default camera fov of 90'
	return(%val * ($cameraFov / 90) * 0.01);
}

function yaw(%val)
{
	$mvYaw += getMouseAdjustAmount(%val);
}

function pitch(%val)
{
	$mvPitch += getMouseAdjustAmount(%val);
}

MoveMap.bind( keyboard, a, moveleft );
MoveMap.bind( keyboard, d, moveright );
MoveMap.bind( keyboard, w, moveforward );
MoveMap.bind( keyboard, s, movebackward );
MoveMap.bind(mouse0, "xaxis", S, $pref::Input::MouseSensitivity, yaw);
MoveMap.bind(mouse0, "yaxis", S, $pref::Input::MouseSensitivity, pitch);


//------------------------------------------------------------------------------
// misc. triggers...
//------------------------------------------------------------------------------

function primaryFire(%val)
{
	$mvTriggerCount0++;
}

function secondaryFire(%val)
{
	$mvTriggerCount1++;
}

function jump(%val)
{
	$mvTriggerCount2++;
}

function tertiaryFire(%val)
{
	$mvTriggerCount3++;
}

function march(%val)
{
	$mvTriggerCount4++;
}

function slide(%val)
{
	$mvTriggerCount5++;
}

function instantGrenadeThrow(%val)
{
    if(%val)
		commandToServer('InstantGrenadeThrow');
}

MoveMap.bind( mouse, button0, primaryFire );
MoveMap.bind( mouse, button1, secondaryFire );
MoveMap.bind( mouse, button2, tertiaryFire );
MoveMap.bind( mouse, "shift button2", instantGrenadeThrow );
MoveMap.bind( keyboard, space, jump );
MoveMap.bind( keyboard, lshift, march );
MoveMap.bind( keyboard, lcontrol, slide );

//------------------------------------------------------------------------------
// zoom and FOV...
//------------------------------------------------------------------------------

function setZoom(%val)
{
	if(%val)
	{
		if( $CurrentZoomValue == 0 )
			$CurrentZoomValue = 5;
		else if ( $CurrentZoomValue == 5 )
			$CurrentZoomValue = 20;
		else if ( $CurrentZoomValue == 20 )
			$CurrentZoomValue = 30;
		else if ( $CurrentZoomValue == 30 )
			$CurrentZoomValue = 40;
		else if ( $CurrentZoomValue == 40 )
			$CurrentZoomValue = 50;
		else if ( $CurrentZoomValue == 50 )
			$CurrentZoomValue = 60;
		else if ( $CurrentZoomValue == 60 )
			$CurrentZoomValue = 70;
		else if ( $CurrentZoomValue == 70 )
			$CurrentZoomValue = 80;
		else if ( $CurrentZoomValue == 80 )
			$CurrentZoomValue = 90;
		else if ( $CurrentZoomValue == 90 )
			$CurrentZoomValue = 5;
			
		toggleZoom(1);
	}
	else
	{
		toggleZoom(0);
	}
}

// zoom to user defined value...
function toggleZoom( %val )
{
	if( $CurrentZoomValue == 0 )
		$CurrentZoomValue = 5;

	if ( %val )
	{
		$ZoomOn = true;
		setFov($CurrentZoomValue);
	}
	else
	{
		$ZoomOn = false;
		setFov($Pref::player::DefaultFov);
	}
}
		
// zoom to fixed value...
function toggleMedZoom( %val )
{
	if ( %val )
	{
		$ZoomOn = true;
		setFov(45);
	}
	else
	{
		$ZoomOn = false;
		setFov( $Pref::Player::DefaultFov );
	}
}

function mouseZoom(%val)
{
	if($CurrentZoomValue == 0)
		$CurrentZoomValue = $Pref::Player::DefaultFov;
		
	%minFov = ServerConnection.getControlObject().getDataBlock().cameraMinFov;
	%step = ($Pref::Player::DefaultFov - %minFov)/$Pref::Player::MouseZoomSteps;

	if(%val > 0)
		$CurrentZoomValue -= %step;
	else
		$CurrentZoomValue += %step;
		
	if($CurrentZoomValue < %minFov)
		$CurrentZoomValue = %minFov;
	else if($CurrentZoomValue > $Pref::Player::DefaultFov)
		$CurrentZoomValue = $Pref::Player::DefaultFov;

	toggleZoom(1);
}

//MoveMap.bind(keyboard, "r", toggleMedZoom);
MoveMap.bind(keyboard, "r", toggleZoom);
MoveMap.bind(keyboard, "f", setZoom);

MoveMap.bind(mouse, zaxis, mouseZoom);


//------------------------------------------------------------------------------
// camera & view...
//------------------------------------------------------------------------------

function toggleFreeLook( %val )
{
	if( %val )
		$mvFreeLook = true;
	else
		$mvFreeLook = false;
}

$firstPerson = true;
function toggleFirstPerson(%val)
{
	if (%val)
	{
		$firstPerson = !$firstPerson;
		ServerConnection.setFirstPerson($firstPerson);
	}
}

function toggleCamera(%val)
{
	if(%val)
		commandToServer('ToggleCamera');
}

function togglePlayerForm(%val)
{
	if(%val)
		commandToServer('TogglePlayerForm');
}

function glanceBack(%val)
{
	if(%val)
		Hud.cameraZRot = 180;
	else
		Hud.cameraZRot = 0;
}

MoveMap.bind(keyboard, lessthan,  toggleFreeLook );
MoveMap.bind(keyboard, "p",		 toggleFirstPerson );
MoveMap.bind(keyboard, "alt c",	toggleCamera);
MoveMap.bind(keyboard, tab,		 togglePlayerForm );
MoveMap.bind(keyboard, "shift x", zoomToControlObject );
//MoveMap.bind(keyboard, c,			glanceBack );

//------------------------------------------------------------------------------
// team & loadout...
//------------------------------------------------------------------------------

function showJoinTeamDlg(%val)
{
	if(%val)
		Canvas.pushDialog(JoinTeamDlg);
}

function showSelectLoadoutDlg(%val)
{
	if( %val )
		Canvas.pushDialog(SelectLoadoutDlg);
}

//MoveMap.bind(keyboard, "m", showJoinTeamDlg);
//MoveMap.bind(keyboard, "n", showSelectLoadoutDlg);

//------------------------------------------------------------------------------
// misc. player...
//------------------------------------------------------------------------------

function showCelAnimDlg(%val)
{
	if( %val )
		Canvas.pushDialog(CelAnimationDlg);
}

function showTeamplayDlg(%val)
{
	if( %val )
		Canvas.pushDialog(TeamplayDlg);
}

function transform(%val)
{
	if( %val )
		commandToServer('Transform');
}

MoveMap.bindCmd(keyboard, "ctrl k", "commandToServer('suicide');", "");

//MoveMap.bind(keyboard, "v", showCelAnimDlg);
//MoveMap.bind(keyboard, "x", showTeamplayDlg);
//MoveMap.bind(keyboard, "t", transform);
//MoveMap.bindCmd(keyboard, "q", "commandToServer('CycleThroughTaggedEnemiesForward');", "");
//MoveMap.bindCmd(keyboard, "shift q", "commandToServer('CycleThroughTaggedEnemiesBackward');", "");
//MoveMap.bindCmd(keyboard, "j", "commandToServer('MountVehicle');", "");
//MoveMap.bindCmd(keyboard, "k", "commandToServer('DismountVehicle');", "");

//------------------------------------------------------------------------------
// weapons...
//------------------------------------------------------------------------------

MoveMap.bindCmd(keyboard, "q", "commandToServer('useWeapon',0);", "");
MoveMap.bindCmd(keyboard, "1", "commandToServer('useWeapon',1);", "");
MoveMap.bindCmd(keyboard, "2", "commandToServer('useWeapon',2);", "");
MoveMap.bindCmd(keyboard, "3", "commandToServer('useWeapon',3);", "");
MoveMap.bindCmd(keyboard, "4", "commandToServer('useWeapon',4);", "");
MoveMap.bindCmd(keyboard, "5", "commandToServer('useWeapon',5);", "");
MoveMap.bindCmd(keyboard, "6", "commandToServer('useWeapon',6);", "");
MoveMap.bindCmd(keyboard, "7", "commandToServer('useWeapon',7);", "");
MoveMap.bindCmd(keyboard, "8", "commandToServer('useWeapon',8);", "");
MoveMap.bindCmd(keyboard, "9", "commandToServer('useWeapon',9);", "");
MoveMap.bindCmd(keyboard, "0", "commandToServer('useWeapon',10);", "");

function mouseWheelWeaponSelect(%val)
{
	if(%val > 0)
		commandToServer('useNextWeapon');
	else
		commandToServer('usePrevWeapon');
}

//MoveMap.bind(mouse, zaxis, mouseWheelWeaponSelect);

//------------------------------------------------------------------------------
// "full control"...
//------------------------------------------------------------------------------

function takeFullControl()
{
	commandToServer('toggleFullControl', true);
}

function relinquishFullControl()
{
	commandToServer('toggleFullControl', false);
}

//MoveMap.bindCmd(keyboard, "shift r", "takeFullControl();", "");
//MoveMap.bindCmd(keyboard, "c", "relinquishFullControl();", "");

//------------------------------------------------------------------------------
// "simple control (tm)"...
//------------------------------------------------------------------------------

function toggleSimpleControl(%slot)
{
	commandToServer('toggleSimpleControl',%slot);
}

function doSimpleControlAction(%slot)
{
	commandToServer('doSimpleControlAction',%slot);
}

//MoveMap.bindCmd(keyboard, "shift 1", "toggleSimpleControl(1);", "");
//MoveMap.bindCmd(keyboard, "shift 2", "toggleSimpleControl(2);", "");
//MoveMap.bindCmd(keyboard, "shift 3", "toggleSimpleControl(3);", "");

//MoveMap.bindCmd(mouse, "shift button0", "doSimpleControlAction(1);", "");
//MoveMap.bindCmd(mouse, "shift button2", "doSimpleControlAction(2);", "");
//MoveMap.bindCmd(mouse, "shift button1", "doSimpleControlAction(3);", "");

//------------------------------------------------------------------------------
// message HUD...
//------------------------------------------------------------------------------

function pageMessageHudUp( %val )
{
	if ( %val )
		pageUpMessageHud();
}

function pageMessageHudDown( %val )
{
	if ( %val )
		pageDownMessageHud();
}

function resizeMessageHud( %val )
{
	if ( %val )
		cycleMessageHudSize();
}

MoveMap.bind(keyboard, "z", toggleMessageHud );
MoveMap.bind(keyboard, "t", teamMessageHud );
MoveMap.bind(keyboard, "shift pageUp", pageMessageHudUp );
MoveMap.bind(keyboard, "shift pageDown", pageMessageHudDown );
MoveMap.bind(keyboard, "i", resizeMessageHud );

//------------------------------------------------------------------------------
// big map...
//------------------------------------------------------------------------------

function toggleBigMap( %val )
{
	BigMap.visible = %val;
}

MoveMap.bind(keyboard, "y", toggleBigMap );

//------------------------------------------------------------------------------
// commander screen...
//------------------------------------------------------------------------------

function activateCmdrScreen(%val)
{
	if(%val)
		Canvas.setContent(CmdrScreen);
}

MoveMap.bind(keyboard, "c", activateCmdrScreen );

//------------------------------------------------------------------------------
// demo & movie recording...
//------------------------------------------------------------------------------

function startRecordingDemo( %val )
{
	if ( %val )
		startDemoRecord();
}

function stopRecordingDemo( %val )
{
	if ( %val )
		stopDemoRecord();
}

MoveMap.bind( keyboard, F3, startRecordingDemo );
MoveMap.bind( keyboard, F4, stopRecordingDemo );

MoveMap.bindCmd( keyboard, F11, "startMovieRecord(\"__movie\",30);","" );
MoveMap.bindCmd( keyboard, F12, "stopMovieRecord();","" );


//------------------------------------------------------------------------------
// helper functions...
//------------------------------------------------------------------------------

function dropCameraAtPlayer(%val)
{
	if (%val)
		commandToServer('DropCameraAtPlayer');
}

function dropPlayerAtCamera(%val)
{
	if (%val)
		commandToServer('DropPlayerAtCamera');
}

MoveMap.bind(keyboard, "F8", dropCameraAtPlayer);
MoveMap.bind(keyboard, "F7", dropPlayerAtCamera);




