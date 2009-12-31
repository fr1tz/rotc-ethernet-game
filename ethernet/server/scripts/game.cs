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

function executeServerScripts()
{
	// to get the Lighting Editor to work, common/server/lightingSystem.cs
	// must be loaded. no idea why common doesn't load it since both
	// $sgLightEditor::lightDBPath and $sgLightEditor::filterDBPath are set
	// within it. -mag
	exec("common/server/lightingsystem.cs");

	exec("./audiodescriptions.cs");
	exec("./etherform.cs");
	exec("./gameconnection.cs");
	exec("./globals.cs");
	exec("./party.cs");
	exec("./booze.cs");
	exec("./cats.cs");
	exec("./fullcontrol.cs");
	exec("./waypointdata.cs");
	exec("./misc.cs");
	exec("./camera.cs");
	exec("./markers.cs");
	exec("./triggers.cs");
	exec("./tacticalzones.cs");
	exec("./shapebase.cs");
	exec("./simplecontrol.cs");
	exec("./staticshape.cs");
	exec("./radiusdamage.cs");
	exec("./aiplayer.cs");
	exec("./teams.cs");
	exec("./loadout.cs");
	exec("./spawn_and_death.cs");
	exec("./teamplay.cs");
	exec("./weapons.cs");
	exec("./players.cs");
	exec("./vehicles.cs");
	exec("./turrets.cs");
	exec("./objectspawn.cs");
	exec("./stats.cs");
}

function onServerCreated()
{
	// Server::GameType is sent to the master server.
	$Server::GameType = $GameNameString;
	$Server::GameVersion = $GameVersionString;
    $Server::ModString = "-";

	// GameStartTime is the sim time the game started. Used to calculated
	// game elapsed time.
	$Game::StartTime = 0;
	
	// load standard server scripts...
	executeServerScripts();
	
	// Keep track of when the game started
	$Game::StartTime = $Sim::Time;
}

function onServerDestroyed()
{
	// This function is called as part of a server shutdown.
}


//-----------------------------------------------------------------------------

function onMissionLoaded()
{
	// Called by loadMission() once the mission is finished loading.

	// start gameplay...
	startGame();
}

function onMissionEnded()
{
	// Called by endMission(), right before the mission is destroyed

	// Normally the game should be ended first before the next
	// mission is loaded, this is here in case loadMission has been
	// called directly.  The mission will be ended if the server
	// is destroyed, so we only need to cleanup here.
	cancel($Game::Schedule);
	$Game::Running = false;
	$Game::Cycling = false;
}


//-----------------------------------------------------------------------------

function startGame()
{
	if ($Game::Running) {
		error("startGame: End the game first!");
		return;
	}
	
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
	%search = $Server::MapInfoFileSpec;
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
	
	$Game::RoundRestarting = false;
}

// this function checks if the round has ended...
function checkRoundEnd()
{
	if($Game::RoundRestarting)
		return;

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
