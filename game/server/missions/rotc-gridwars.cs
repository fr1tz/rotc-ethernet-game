//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//*************************************************
// Mission init script for ROTC: Grid Wars missions
//*************************************************

exec("./rotc-types.cs");

$Game::GameType = $Game::GridWars;
$Game::GameTypeString = "ROTC: Grid Wars";

$Server::MissionDirectory = strreplace($Server::MissionFile, ".mis", "") @ "/";
$Server::MissionEnvironmentFile = $Server::MissionDirectory @ "mission.env";

function executeMissionScript()
{
	exec($Server::MissionDirectory @ "mission.cs"); 
}

function executeEnvironmentScript()
{
	exec($Server::MissionEnvironmentFile @ ".cs"); 
}

function executeGameScripts()
{
	exec("game/server/base/exec.cs");
	exec("game/server/eth/exec.cs");
	exec("game/server/weapons/exec.cs");	
	exec("game/server/blueprints/exec.cs");	
	exec("game/server/gridwars/exec.cs");
}

function initMission()
{
	executeGameScripts();
	executeMissionScript();
	executeEnvironmentScript();
}

function onMissionLoaded()
{
	// Called by loadMission() once the mission is finished loading.
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



		