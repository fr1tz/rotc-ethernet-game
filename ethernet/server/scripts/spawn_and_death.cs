//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - spawn_and_death.cs
// Code which deals with spawning and death of players
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Support functions
//-----------------------------------------------------------------------------

function createDeathExplosion(%obj,%position)
{
	%x = getWord(%position,0);
	%y = getWord(%position,1);
	%z = getWord(%obj.getPosition(),2);
	%pushPos = %x @ " " @ %y @ " " @ %z;

	radiusDamage(%sourceObject,%pushPos,1,0,"DeathExplosion_NoDamage",3000);
	if(%client.team == $Team1)
		createExplosion(Team1DeathExplosion,%position);
	else
		createExplosion(Team2DeathExplosion,%position);
}

function pickSpawnSphere(%teamId)
{
	 if( $Server::MissionType $= "ffa" )
	 { // deathmatch == no teams...
		 // FIXME: todo -mg
	 }
	 else // team-based gametype...
	 {
		 if( %teamId == 1 ) %groupName = "MissionGroup/RedSpawnPoints";
		 if( %teamId == 2 ) %groupName = "MissionGroup/BlueSpawnPoints";
	 }
	%group = nameToID(%groupName);

	if (%group != -1)
	{
		%count = %group.getCount();
		if (%count != 0)
		{
			%index = getRandom(%count-1);
			%spawn = %group.getObject(%index);

			return %spawn;
		}
		else
			error("no spawn points found in " @ %groupName);
	}
	else
		error("missing spawn points group " @ %groupName);

	return -1;
}

function pickObserverPoint(%client)
{
	%groupName = "MissionGroup/ObserverPoints";
	%group = nameToID(%groupName);

	if (%group != -1) {
		%count = %group.getCount();
		if (%count != 0) {
			%index = getRandom(%count-1);
			%spawn = %group.getObject(%index);
			return %spawn.getTransform();
		}
		else
			error("no observer spawn points found in " @ %groupName);
	}
	else
		error("missing observer spawn points group " @ %groupName);

	// could be no spawn points, in which case we'll stick the
	// player at the center of the world.
	return "0 0 300 1 0 0 0";
}


function getRandomHorizontalPos(%pos,%rad)
{
	while(%retries < 500)
	{
		%x = getWord(%pos, 0) + mFloor(getRandom(%rad * 2) - %rad);
		%y = getWord(%pos, 1) + mFloor(getRandom(%rad * 2) - %rad);
		%z = getWord(%pos, 2);

		%position = %x @ " " @ %y @ " " @ %z;

		%mask = ($TypeMasks::VehicleObjectType |
			$TypeMasks::MoveableObjectType |
			$TypeMasks::StaticShapeObjectType |
			$TypeMasks::ForceFieldObjectType |
			$TypeMasks::InteriorObjectType |
			$TypeMasks::ItemObjectType);

		if(ContainerBoxEmpty(%mask,%position,1.0))
		{
//			error ("Spawn Position Is Good");
			return %position;
		}
		else
			%retries++;
	}
 
	return "0 0 300 1 0 0 0";
}

function getGroundLevel(%pos)
{
	%x = getWord(%pos, 0);
	%y = getWord(%pos, 1);
	%z = getWord(%pos, 2);

	%start 	= %x @ " " @ %y @ " 5000";
	%end 		= %x @ " " @ %y @ " -1";
	%ground 	= containerRayCast(%start, %end, $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::TurretObjectType, 0);
	%z 		= getWord(%ground, 3);

	%position = %x @ " " @ %y @ " " @ %z;
	return %position;
}

function getTerrainLevel(%pos)
{
	%x = getWord(%pos, 0);
	%y = getWord(%pos, 1);
	%z = getWord(%pos, 2);

	%start 	= %x @ " " @ %y @ " 5000";
	%end 		= %x @ " " @ %y @ " -1";
	%ground 	= containerRayCast(%start, %end, $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType , 0);
	%z 		= getWord(%ground, 3);

	%position = %x @ " " @ %y @ " " @ %z;
	return %position;
}
