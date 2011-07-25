//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Game duration in secs, no limit if the duration is set to 0
$Game::Duration = 0;

// When a client score reaches this value, the game is ended.
$Game::EndGameScore = 99999;

// Pause while looking over the end game screen (in secs)
$Game::EndGamePause = 10;

//-----------------------------------------------------------------------------
//  Functions that implement game-play
//-----------------------------------------------------------------------------


//-----------------------------------------------------------------------------

function startGame()
{
	if ($Game::Running) {
		error("startGame: End the game first!");
		return;
	}

	// GameStartTime is the sim time the game started. Used to calculated
	// game elapsed time.
	$Game::StartTime = $Sim::Time;
	
	// Start the game timer
	if ($Game::Duration)
		$Game::Schedule = schedule($Game::Duration * 1000, 0, "onGameDurationEnd" );
		
	// create team objects...
	createTeams();

	// Start the first round...
	startNewRound();

	// Inform the client we're starting up
	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
		%cl = ClientGroup.getObject( %clientIndex );
		commandToClient(%cl, 'GameStart');

		// Other client specific setup..
		%cl.score = 0;
	}

	$Game::Running = true;
}

function endGame()
{
	if (!$Game::Running)  {
		error("endGame: No game running!");
		return;
	}

	// Stop any game timers
	cancel($Game::Schedule);

	// Inform the client the game is over
	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
		%cl = ClientGroup.getObject( %clientIndex );
		commandToClient(%cl, 'GameEnd');
	}

	// Delete all the temporary mission objects
	resetMission();
	$Game::Running = false;
}

function onGameDurationEnd()
{
	// This "redirect" is here so that we can abort the game cycle if
	// the $Game::Duration variable has been cleared, without having
	// to have a function to cancel the schedule.
	if ($Game::Duration && !isObject(EditorGui))
		cycleGame();
}


//------------------------------------------------------------------------------

function cycleGame()
{
	// This is setup as a schedule so that this function can be called
	// directly from object callbacks.  Object callbacks have to be
	// carefull about invoking server functions that could cause
	// their object to be deleted.
	if (!$Game::Cycling) {
		$Game::Cycling = true;
		$Game::Schedule = schedule(0, 0, "onCycleExec");
	}
}

function onCycleExec()
{
	// End the current game and start another one, we'll pause for a little
	// so the end game victory screen can be examined by the clients.
	endGame();
	$Game::Schedule = schedule($Game::EndGamePause * 1000, 0, "onCyclePauseEnd");
}

function onCyclePauseEnd()
{
	$Game::Cycling = false;

	// Just cycle through the missions for now.
	%search = $Server::MissionFileSpec;
	for (%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
		if (%file $= $Server::MissionFile) {
			// Get the next one, back to the first if there
			// is no next.
			%file = findNextFile(%search);
			if (%file $= "")
			  %file = findFirstFile(%search);
			break;
		}
	}
	loadMission(%file);
}

//------------------------------------------------------------------------------

function serverUpdateTeamJoustClock()
{
	cancel($Game::UpdateTeamJoustClockThread);

	$Game::TeamJoustClock -= 1;

	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
	{
		%client = ClientGroup.getObject(%clientIndex);
		%client.topHudMenu = "teamjoustclock";
		%client.updateTopHudMenuThread();
		if($Game::TeamJoustClock > 0)
		{
			%pitch = ($Game::TeamJoustClock < 4 ? 1.5 : 0.5);
			%client.play2D(ClockTickSound, %pitch);
		}
	}

	if($Game::TeamJoustClock == 0)
	{
		checkRoundEnd();
		return;
	}

}

function serverStartTeamJoust()
{
	if($Game::TeamJoustThread !$= "")
		cancel($Game::TeamJoustThread);
	$Game::TeamJoustState = 0;
	$Team1.score = 0;
	$Team2.score = 0;
	TerritoryZones_reset();
	centerPrintAll("Get into formation!", 4);
	$Game::TeamJoustThread = 
		schedule(2000, MissionEnvironment, "serverAdvanceTeamJoust");
}

function serverAdvanceTeamJoust()
{
	if($Game::TeamJoustThread !$= "")
		cancel($Game::TeamJoustThread);
	$Game::TeamJoustState++;

	TerritoryZones_reset();

	if($Game::TeamJoustState == 4)
	{
		$Game::TeamJoustState = "done";
		checkRoundEnd();		
	}
	else if($Game::TeamJoustState == 3)
	{
		// go go go!
	  	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
	  	{
	  		%client = ClientGroup.getObject( %clientIndex );
	  		if( %client.team == $Team1 || %client.team == $Team2 )
	  		{
	  			%client.togglePlayerForm();
				%player = %client.player;
				if(%player.isCAT)
				{
	  				%client.team.numPlayersOnRoundStart++;
					%player.setBodyPose($PlayerBodyPose::Sliding);
				}
	  		}
	  	}

		schedule(10000, MissionEnvironment, "serverAdvanceTeamJoust");
	}
	else
	{
		schedule(2000, MissionEnvironment, "serverAdvanceTeamJoust");
	}
}


// this function starts a new round...
function startNewRound()
{
	// cleanup...
	for( %idx = MissionCleanup.getCount()-1; %idx >= 0; %idx-- )
	{
		%obj = MissionCleanup.getObject(%idx);
		if(%obj.getType() & $TypeMasks::ProjectileObjectType
		|| %obj.getType() & $TypeMasks::PlayerObjectType
		|| %obj.getType() & $TypeMasks::CorpseObjectType)
			%obj.delete();
	}

	$Team1.numPlayersOnRoundStart = 0;
	$Team2.numPlayersOnRoundStart = 0;

	if($ROTC::GameType == $ROTC::TeamJoust)
	{
		serverStartTeamJoust();
	}
	
//	if( $Server::MissionType $= "har" )
//	{
//		resetCheckpoints();
		TerritoryZones_reset();
//	}

	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
	{
		%client = ClientGroup.getObject( %clientIndex );

		// do not respawn observers...
		if( %client.team == $Team1 || %client.team == $Team2 )
			%client.spawnPlayer();
	}

	serverUpdateMusic();
	serverUpdateGameStatus();
	
	$Game::RoundRestarting = false;
}

// this function checks if the round has ended...
function checkRoundEnd()
{
	if($Game::RoundRestarting)
		return;

	if($ROTC::GameType == $ROTC::TeamJoust)
	{
		if($Game::TeamJoustState !$= "done")
			return;

		if($Team1.score == $Team2.score)
		{
			%text = "Draw!";
			serverPlay2D(RedVictorySound);
		}
		else if($Team1.score > $Team2.score)
		{
			%text = "Reds have won!";
			serverPlay2D(RedVictorySound);
		}
		else
		{
			%text = "Blues have won!";
			serverPlay2D(RedVictorySound);
		}

		%text = %text @ "\n\n";
		%text = %text @ "Reds:" SPC ($Team1.score*100) @ "%\n";
		%text = %text @ "Blues:" SPC ($Team2.score*100) @ "%\n";

		centerPrintAll(%text, 3);

		schedule(5000,0,"startNewRound");
		$Game::RoundRestarting = true;
	}
	else
	{
		if($Team1.numTerritoryZones == 0 && $Team1.numCATs == 0)
		{
			centerPrintAll($Team2.name @ " have won!",3);
			serverPlay2D(BlueVictorySound);
			schedule(5000,0,"startNewRound");
			$Game::RoundRestarting = true;
		}
		else if($Team2.numTerritoryZones == 0 && $Team2.numCATs == 0)
		{
			centerPrintAll($Team1.name @ " have won!",3);
			serverPlay2D(RedVictorySound);
			schedule(5000,0,"startNewRound");
			$Game::RoundRestarting = true;
		}
	}
}
