//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function changeSkyColor(%color)
{
  	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
  	{
  		%client = ClientGroup.getObject( %clientIndex );
		%client.setSkyColor(%color);
  	}
}

function teamDragRaceSkyColorThread()
{
	if($Game::TeamDragRaceSkyColorThread !$= "")
		cancel($Game::TeamDragRaceSkyColorThread);	
	$Game::TeamDragRaceSkyColorThread = schedule(500, MissionEnvironment,
		"teamDragRaceSkyColorThread");

	%teamHealth[1] = 0;
	%teamHealth[2] = 0;
	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
	{
		%client = ClientGroup.getObject(%clientIndex);
		if(isObject(%client.player) && %client.player.isCAT)
		{
			%teamHealth[%client.team.teamId] += %client.player.getDataBlock().maxDamage
				- %client.player.getDamageLevel() + %client.player.getDamageBufferLevel();
		}
	}

	%teamHealth[1] += $Team1.score*100;
	%teamHealth[2] += $Team2.score*100;

	if(%teamHealth[1] > %teamHealth[2])
	{
		%ratio = %teamHealth[2] / %teamHealth[1];
		changeSkyColor("1 0.75" SPC %ratio);
	}
	else
	{
		%ratio = %teamHealth[1] / %teamHealth[2];
		changeSkyColor(%ratio SPC "0.75 1");
	}
}

function startTeamDragRace()
{
	if($Game::TeamDragRaceThread !$= "")
		cancel($Game::TeamDragRaceThread);
	if($Game::TeamDragRaceSkyColorThread !$= "")
		cancel($Game::TeamDragRaceSkyColorThread);	
	$Game::TeamDragRaceState = 0;
	$Team1.score = 0;
	$Team2.score = 0;
	RacingLaneZones_reset();
	changeSkyColor("1 0 0");
	centerPrintAll("Get into formation!", 4);
	$Game::TeamDragRaceThread = 
		schedule(2000, MissionEnvironment, "advanceTeamDragRace");
}

function advanceTeamDragRace()
{
	if($Game::TeamDragRaceThread !$= "")
		cancel($Game::TeamDragRaceThread);
	$Game::TeamDragRaceState++;

	RacingLaneZones_reset();

	switch($Game::TeamDragRaceState)
	{
		case 1:
			changeSkyColor("1 1 0");
			$Game::TeamDragRaceThread =
				schedule(2000, MissionEnvironment, "advanceTeamDragRace");	

		case 2:
			changeSkyColor("0 1 0");
			$Game::TeamDragRaceThread =
				schedule(2000, MissionEnvironment, "advanceTeamDragRace");

		case 3:
			// go go go!
		  	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
		  	{
		  		%client = ClientGroup.getObject( %clientIndex );
		  		if( %client.team == $Team0 )
					continue;
		  
				if(!%client.player.zCurrentZone.startZone)
					continue;
					
		  		%client.togglePlayerForm(true);
				%player = %client.player;
				if(%player.isCAT)
		  			%client.team.numPlayersOnRoundStart++;
		  	}
			teamDragRaceSkyColorThread();
			checkRoundEnd_TeamDragRace();

		case 4:			
			$Game::TeamDragRaceState = "done";
			checkRoundEnd_TeamDragRace();	
	}
}

function checkRoundEnd_TeamDragRace()
{
	if($Team1.numCATs > 0 || $Team2.numCATs > 0)
		return;

	if($Game::TeamDragRaceThread !$= "")
		cancel($Game::TeamDragRaceThread);

	%text = "";
	%text = %text @ "Reds:" SPC ($Team1.score*100) @ "%\n";
	%text = %text @ "Blues:" SPC ($Team2.score*100) @ "%\n";

	%text = "\n\n" @ %text;

	if($Team1.numPlayersOnRoundStart == 0
	&& $Team2.numPlayersOnRoundStart == 0)
	{
		%text = "No racers! Restarting...";
	}
	else if($Team1.score == $Team2.score)
	{
		%text = "Draw!" @ %text;
		serverPlay2D(RedVictorySound);
	}
	else if($Team1.score > $Team2.score)
	{
		%text = "Reds have won!" @ %text;
		serverPlay2D(RedVictorySound);
	}
	else
	{
		%text = "Blues have won!" @ %text;
		serverPlay2D(RedVictorySound);
	}

	centerPrintAll(%text, 3);

	schedule(5000, MissionEnvironment, "startNewRound");
	$Game::RoundRestarting = true;
}
