//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

// These are the functions that can be remapped to different controls
// using the "Controls" option dialog.

//------------------------------------------------------------------------------
// In-game shell overlay
//------------------------------------------------------------------------------

function toggleShellDlg(%val)
{
	if(%val)
		return;

	if(ShellDlg.isAwake())
		Canvas.popDialog(ShellDlg);
	else
	{
		Canvas.pushDialog(ShellDlg);
		addWindow(MissionWindow);
	}
}

//------------------------------------------------------------------------------
// Camera
//------------------------------------------------------------------------------

function freeLook( %val )
{
	$mvFreeLook = %val;
}

function toggleFirstPerson(%val)
{
	if (%val)
	{
		$firstPerson = !$firstPerson;
		ServerConnection.setFirstPerson($firstPerson);
	}
}

//------------------------------------------------------------------------------
// Movement
//------------------------------------------------------------------------------

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

function yaw(%val)
{
	$mvYaw += getMouseAdjustAmount(%val);
}

function pitch(%val)
{
	$mvPitch += getMouseAdjustAmount(%val);
}

//------------------------------------------------------------------------------
// Triggers
//------------------------------------------------------------------------------

function trigger0(%val)
{
	$mvTriggerCount0++;
}

function trigger1(%val)
{
	$mvTriggerCount1++;
}

function trigger2(%val)
{
	$mvTriggerCount2++;
}

function trigger3(%val)
{
	$mvTriggerCount3++;
}

function trigger4(%val)
{
	$mvTriggerCount4++;
}

function trigger5(%val)
{
	$mvTriggerCount5++;
}

//------------------------------------------------------------------------------
// Zoom and FOV
//------------------------------------------------------------------------------

function toggleZoomLevel(%val)
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
	}
}

// zoom to user defined value...
function zoom( %val )
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

	zoom(1);
}

//------------------------------------------------------------------------------
// Message HUD
//------------------------------------------------------------------------------

function toggleMessageHud(%val)
{
	if(%val)
	{
		MessageHud.isTeamMsg = false;
		MessageHud.toggleState();
	}
}

function teamMessageHud(%val)
{
	if(%val)
	{
		MessageHud.isTeamMsg = true;
		MessageHud.toggleState();
	}
}

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

//------------------------------------------------------------------------------
// Misc
//------------------------------------------------------------------------------

function biggerMiniMap( %val )
{
	BigMap.visible = %val;
}

function activateCmdrScreen(%val)
{
	if(%val)
		Canvas.setContent(CmdrScreen);
}

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

//------------------------------------------------------------------------------
// Player actions
//------------------------------------------------------------------------------

function action0(%val)
{
	if(%val)
		commandToServer('PlayerAction', 0);
}

function action1(%val)
{
	if(%val)
		commandToServer('PlayerAction', 1);
}

function action2(%val)
{
	if(%val)
		commandToServer('PlayerAction', 2);
}

function action3(%val)
{
	if(%val)
		commandToServer('PlayerAction', 3);
}

function action4(%val)
{
	if(%val)
		commandToServer('PlayerAction', 4);
}

function action5(%val)
{
	if(%val)
		commandToServer('PlayerAction', 5);
}

function action6(%val)
{
	if(%val)
		commandToServer('PlayerAction', 6);
}

function action7(%val)
{
	if(%val)
		commandToServer('PlayerAction', 7);
}

function action8(%val)
{
	if(%val)
		commandToServer('PlayerAction', 8);
}

function action9(%val)
{
	if(%val)
		commandToServer('PlayerAction', 9);
}

function action10(%val)
{
	if(%val)
		commandToServer('PlayerAction', 10);
}

function action11(%val)
{
	if(%val)
		commandToServer('PlayerAction', 11);
}

function action12(%val)
{
	if(%val)
		commandToServer('PlayerAction', 12);
}

function action13(%val)
{
	if(%val)
		commandToServer('PlayerAction', 13);
}

function action14(%val)
{
	if(%val)
		commandToServer('PlayerAction', 14);
}

function action15(%val)
{
	if(%val)
		commandToServer('PlayerAction', 15);
}

function action16(%val)
{
	if(%val)
		commandToServer('PlayerAction', 16);
}

function action17(%val)
{
	if(%val)
		commandToServer('PlayerAction', 17);
}

function action18(%val)
{
	if(%val)
		commandToServer('PlayerAction', 18);
}

function action19(%val)
{
	if(%val)
		commandToServer('PlayerAction', 19);
}

function action20(%val)
{
	if(%val)
		commandToServer('PlayerAction', 20);
}

function action21(%val)
{
	if(%val)
		commandToServer('PlayerAction', 21);
}

function action22(%val)
{
	if(%val)
		commandToServer('PlayerAction', 22);
}

function action23(%val)
{
	if(%val)
		commandToServer('PlayerAction', 23);
}

function action24(%val)
{
	if(%val)
		commandToServer('PlayerAction', 24);
}

function action25(%val)
{
	if(%val)
		commandToServer('PlayerAction', 25);
}

function action26(%val)
{
	if(%val)
		commandToServer('PlayerAction', 26);
}

function action27(%val)
{
	if(%val)
		commandToServer('PlayerAction', 27);
}

function action28(%val)
{
	if(%val)
		commandToServer('PlayerAction', 28);
}

function action29(%val)
{
	if(%val)
		commandToServer('PlayerAction', 29);
}

function action30(%val)
{
	if(%val)
		commandToServer('PlayerAction', 30);
}

function action31(%val)
{
	if(%val)
		commandToServer('PlayerAction', 31);
}

function action32(%val)
{
	if(%val)
		commandToServer('PlayerAction', 32);
}

function action33(%val)
{
	if(%val)
		commandToServer('PlayerAction', 33);
}

function action34(%val)
{
	if(%val)
		commandToServer('PlayerAction', 34);
}

function action35(%val)
{
	if(%val)
		commandToServer('PlayerAction', 35);
}

function action36(%val)
{
	if(%val)
		commandToServer('PlayerAction', 36);
}

function action37(%val)
{
	if(%val)
		commandToServer('PlayerAction', 37);
}

function action38(%val)
{
	if(%val)
		commandToServer('PlayerAction', 38);
}

function action39(%val)
{
	if(%val)
		commandToServer('PlayerAction', 39);
}

