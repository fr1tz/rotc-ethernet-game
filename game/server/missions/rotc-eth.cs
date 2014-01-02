//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//************************************************
// Mission init script for ROTC: Ethernet missions
//************************************************

exec("./rotc-types.cs");

$Game::GameType = $Game::Ethernet;
$Game::GameTypeString = "ROTC: Ethernet";

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
}

function loadManual()
{
	constructManual("game/server/eth/help/index.idx");
	// hack hack hack
	//if($Server::Game.superblaster)
	//{
	//	%p = getManualPage("6.1");
	//	%p.name = "Superblaster";
	//	%p.file = "game/server/weapons/blaster3/blaster3.rml";
	//	updateManualPage(%p);
	//}
}

function loadHints()
{
   constructHints("game/server/eth/help/hints.rml");
}

function initMission()
{
	if(isObject($Server::Game))
		$Server::Game.delete();
	$Server::Game = new ScriptObject();
	$Server::Game.mutators = "";
	$Server::Game.alwaystag = -1;
	$Server::Game.nevertag  = 0;
	$Server::Game.temptag   = 1;
	$Server::Game.tagMode = $Server::Game.alwaystag;
	$Server::Game.slowpokemod = 1.0;
	%recognized = "";
	for(%i = 0; %i < getWordCount($Pref::Server::Mutators); %i++)
	{
		%mutator = getWord($Pref::Server::Mutators, %i);
		if(%mutator $= "temptag")
		{
		 %recognized = %mutator SPC %recognized;
			$Server::Game.tagMode = $Server::Game.temptag;
			$Server::Game.mutators = %mutator SPC $Server::Game.mutators;
		}
		else if(%mutator $= "nevertag")
		{
		%recognized = %mutator SPC %recognized;
			$Server::Game.tagMode = $Server::Game.nevertag;
			$Server::Game.mutators = %mutator SPC $Server::Game.mutators;
		}
		else if(%mutator $= "noshield")
		{
		%recognized = %mutator SPC %recognized;
			$Server::Game.noshield = true;
			$Server::Game.mutators = %mutator SPC $Server::Game.mutators;
		}
		else if(%mutator $= "lowhealth")
		{
		%recognized = %mutator SPC %recognized;
			$Server::Game.lowhealth = true;
			$Server::Game.mutators = %mutator SPC $Server::Game.mutators;
		}
		else if(%mutator $= "slowpoke")
		{
		%recognized = %mutator SPC %recognized;
			$Server::Game.slowpoke = true;
			$Server::Game.slowpokemod = 0.5;
			$Server::Game.mutators = %mutator SPC $Server::Game.mutators;
		}
		else if(%mutator $= "QUICKDEATH")
		{
		%recognized = %mutator SPC %recognized;
			$Server::Game.noshield = true;
			$Server::Game.lowhealth = true;
			$Server::Game.mutators = $Server::Game.mutators SPC
				"noshield" SPC "lowhealth";
		}
	}
	%recognized = trim(%recognized);
	if(getWordCount(%recognized) > 0)
	{
		// We have valid mutators...
		$Server::Game.mutators = trim($Server::Game.mutators);
		if(getWordCount(%recognized) == 1)
			%str = %recognized;
		else
			%str = "VARIANT";
		$Server::MissionType = $Server::MissionType SPC "\c4["@%str@"]\co";
	}
	executeGameScripts();
	executeMissionScript();
	executeEnvironmentScript();
	loadManual();
	loadHints();
}

function onMissionLoaded()
{
	// Called by loadMission() once the mission is finished loading.
	startGame();
}

function onMissionEnded()
{
	// Called by endMission(), right before the mission is destroyed

	if(isObject($Server::Game))
		$Server::Game.delete();

	// Normally the game should be ended first before the next
	// mission is loaded, this is here in case loadMission has been
	// called directly.  The mission will be ended if the server
	// is destroyed, so we only need to cleanup here.
	cancel($Game::Schedule);
	$Game::Running = false;
	$Game::Cycling = false;
}



		
