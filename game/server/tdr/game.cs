//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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

function pushDown(%obj)
{
	return;

	%start = %obj.getWorldBoxCenter();
	%end	= VectorAdd(%start, "0 0 -3");	
	%c = containerRayCast(%start, %end, $TypeMasks::TerrainObjectType |
		$TypeMasks::InteriorObjectType , %obj);
	if(%c)
	{
		%normal = getWord(%c,4) SPC getWord(%c,5) SPC getWord(%c,6);
		%vec = VectorScale(%normal, -10000);
		%obj.impulse(%start, %vec, 0);
	}

	echo(%contact);

}

function teamDragRaceUglyThread()
{
	if($Game::TeamDragRaceUglyThread !$= "")
		cancel($Game::TeamDragRaceUglyThread);	
	$Game::TeamDragRaceUglyThread = schedule(100, MissionEnvironment,
		"teamDragRaceUglyThread");

	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
	{
		%client = ClientGroup.getObject(%clientIndex);
		%player = %client.palyer;
		if(!isObject(%player))
			continue;
		if(isObject(%client.player) && %client.player.isCAT)
			pushDown(%client.player);
	}
}

function startTeamDragRace()
{
	if($Game::TeamDragRaceThread !$= "")
		cancel($Game::TeamDragRaceThread);
	if($Game::TeamDragRaceSkyColorThread !$= "")
		cancel($Game::TeamDragRaceSkyColorThread);	
	if($Game::TeamDragRaceUglyThread !$= "")
		cancel($Game::TeamDragRaceUglyThread);
	if(!isObject($Game::RedCats))
	{
		$Game::RedCats = new SimSet();
		MissionCleanup.add($Game::RedCats);
	}
	if(!isObject($Game::BlueCats))
	{
		$Game::BlueCats = new SimSet();
		MissionCleanup.add($Game::BlueCats);
	}
	$Game::TeamDragRaceState = 0;
	$Team1.score = 0;
	$Team2.score = 0;
	RacingLaneZones_reset();
	WindZones_reset();
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
				{
		  			%client.team.numPlayersOnRoundStart++;
					if(%player.getTeamId() == 1)
						$Game::RedCats.add(%player);
					else if(%player.getTeamId() == 2)
						$Game::BlueCats.add(%player);
					//%pod = new HoverVehicle() {
					//	dataBlock = HoverPod;
					//	client = %client;
					//};
					//%pod = new Etherform() {
					//	dataBlock = Etherpod;
					//	client = %client;
					//};
					//%pod = new FlyingVehicle() {
					//	dataBlock = Flyerpod;
					//	client = %client;
					//};
					%pod = new Player() {
						dataBlock = PlayerPod;
						client = %client;
						teamId = %client.team.teamId;
					};
					MissionCleanup.add(%pod);
					%pod.ssc = new HoverPodController() {
						client = %client;
					};
					MissionCleanup.add(%pod.ssc);
					%pod.useServerSideController(%pod.ssc);
					%pod.setTransform(%player.getTransform());
					%pod.mountObject(%player, 0);
				}
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
	error($Team1.numCATs SPC $Team2.numCATs);
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
