//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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
		
	$ShellDlgActive = !$ShellDlgActive;

	updateShellDlg();
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
	// hack hack hack: players should really be able
    // to configure arbitrary chords.
	if($triggerDown[3] && %val)
	{
		// chord initiated
		$chording[0] = true;
		action12(1);
	}

	if($chording[0])
	{
		if(!%val)
		{
			// chord finished
			$chording[0] = false;
			action12(0);
		}			
	}
	else
	{
		$mvTriggerCount0++;
	}
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
	$triggerDown[3] = %val;
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

function toggleTempZoomLevel(%val)
{
	//echo("toggleTempZoomLevel(): %val =" SPC %val);

	if(%val)
	{
		%minFov = ServerConnection.getControlObject().getDataBlock().cameraMinFov;
		%step = ($Pref::Player::DefaultFov - %minFov)/$Pref::Player::MouseZoomSteps;

		$TempZoomValue += %step;

		if($TempZoomValue > $Pref::Player::DefaultFov)
			$TempZoomValue = %minFov;
						
		if($TempZoomOn)
			setFov($TempZoomValue);	
	}
}

// zoom to user defined value...
function tempZoom( %val )
{
	//echo("tempZoom(): %val =" SPC %val);

	if($TempZoomValue == 0)
		$TempZoomValue = 1;

	if(%val)
	{
		$TempZoomOn = true;
		setFov($TempZoomValue);
	}
	else
	{
		$TempZoomOn = false;
		setFov($MouseZoomValue == 0 ? $Pref::Player::DefaultFov : $MouseZoomValue);
	}
}

function mouseZoom(%val)
{
	if(Canvas.isCursorOn())
		return;

	if($MouseZoomValue == 0)
		$MouseZoomValue = $Pref::Player::DefaultFov;
		
	%minFov = ServerConnection.getControlObject().getDataBlock().cameraMinFov;
	%step = ($Pref::Player::DefaultFov - %minFov)/$Pref::Player::MouseZoomSteps;

	if(%val > 0)
		$MouseZoomValue -= %step;
	else
		$MouseZoomValue += %step;
		
	if($MouseZoomValue < %minFov)
		$MouseZoomValue = %minFov;
	else if($MouseZoomValue > $Pref::Player::DefaultFov)
		$MouseZoomValue = $Pref::Player::DefaultFov;

	setFov($MouseZoomValue);
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

function toggleRecordingDemo( %val )
{
	if ( %val )
		toggleDemoRecord();
}

function takeScreenshot( %val )
{
	if ( %val )
		doScreenshot(1);
}

//------------------------------------------------------------------------------
// Player actions
//------------------------------------------------------------------------------

function action0(%val)
{
	commandToServer('PlayerAction', 0, %val);
}


function action1(%val)
{
	commandToServer('PlayerAction', 1, %val);
}


function action2(%val)
{
	commandToServer('PlayerAction', 2, %val);
}


function action3(%val)
{
	commandToServer('PlayerAction', 3, %val);
}


function action4(%val)
{
	commandToServer('PlayerAction', 4, %val);
}


function action5(%val)
{
	commandToServer('PlayerAction', 5, %val);
}


function action6(%val)
{
	commandToServer('PlayerAction', 6, %val);
}


function action7(%val)
{
	commandToServer('PlayerAction', 7, %val);
}


function action8(%val)
{
	commandToServer('PlayerAction', 8, %val);
}


function action9(%val)
{
	commandToServer('PlayerAction', 9, %val);
}


function action10(%val)
{
	commandToServer('PlayerAction', 10, %val);
}


function action11(%val)
{
	commandToServer('PlayerAction', 11, %val);
}


function action12(%val)
{
	commandToServer('PlayerAction', 12, %val);
}


function action13(%val)
{
	commandToServer('PlayerAction', 13, %val);
}


function action14(%val)
{
	commandToServer('PlayerAction', 14, %val);
}


function action15(%val)
{
	commandToServer('PlayerAction', 15, %val);
}


function action16(%val)
{
	commandToServer('PlayerAction', 16, %val);
}


function action17(%val)
{
	commandToServer('PlayerAction', 17, %val);
}


function action18(%val)
{
	commandToServer('PlayerAction', 18, %val);
}


function action19(%val)
{
	commandToServer('PlayerAction', 19, %val);
}


function action20(%val)
{
	commandToServer('PlayerAction', 20, %val);
}


function action21(%val)
{
	commandToServer('PlayerAction', 21, %val);
}


function action22(%val)
{
	commandToServer('PlayerAction', 22, %val);
}


function action23(%val)
{
	commandToServer('PlayerAction', 23, %val);
}


function action24(%val)
{
	commandToServer('PlayerAction', 24, %val);
}


function action25(%val)
{
	commandToServer('PlayerAction', 25, %val);
}


function action26(%val)
{
	commandToServer('PlayerAction', 26, %val);
}


function action27(%val)
{
	commandToServer('PlayerAction', 27, %val);
}


function action28(%val)
{
	commandToServer('PlayerAction', 28, %val);
}


function action29(%val)
{
	commandToServer('PlayerAction', 29, %val);
}


function action30(%val)
{
	commandToServer('PlayerAction', 30, %val);
}


function action31(%val)
{
	commandToServer('PlayerAction', 31, %val);
}


function action32(%val)
{
	commandToServer('PlayerAction', 32, %val);
}


function action33(%val)
{
	commandToServer('PlayerAction', 33, %val);
}


function action34(%val)
{
	commandToServer('PlayerAction', 34, %val);
}


function action35(%val)
{
	commandToServer('PlayerAction', 35, %val);
}


function action36(%val)
{
	commandToServer('PlayerAction', 36, %val);
}


function action37(%val)
{
	commandToServer('PlayerAction', 37, %val);
}


function action38(%val)
{
	commandToServer('PlayerAction', 38, %val);
}


function action39(%val)
{
	commandToServer('PlayerAction', 39, %val);
}


