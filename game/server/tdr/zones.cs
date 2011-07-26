//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function RacingLaneZone_find(%name)
{
	%group = nameToID("RacingLaneZones");

	if (%group != -1)
	{
		%count = %group.getCount();
		if(%count != 0)
		{
				for (%i = 0; %i < %count; %i++)
				{
					%zone = %group.getObject(%i);
					if(%zone.getName() $= %name)
						return %zone;
				}
		}
		else
			error("RacingLaneZones_call():" SPC
				"no TacticalZones found in RacingLaneZones group!");
	}
	else
		error("RacingLaneZones_call(): missing RacingLaneZones group!");

	return -1;
}

//------------------------------------------------------------------------------

function RacingLaneZones_enableRepair(%shape)
{
	if(%shape.isCAT)
		return;

	if(%shape.getTeamId() == $Team1.teamId)
	{
		if(!$Team1.repairObjects.isMember(%shape))
			$Team1.repairObjects.add(%shape);
	}
	else if(%shape.getTeamId() == $Team2.teamId)
	{
		if(!$Team2.repairObjects.isMember(%shape))
			$Team2.repairObjects.add(%shape);
	}
}

function RacingLaneZones_disableRepair(%shape)
{
	if(%shape.isCAT)
		return;

	if(%shape.getTeamId() == $Team1.teamId)
	{
		if($Team1.repairObjects.isMember(%shape))
			$Team1.repairObjects.remove(%shape);
	}
	else if(%shape.getTeamId() == $Team2.teamId)
	{
		if($Team2.repairObjects.isMember(%shape))
			$Team2.repairObjects.remove(%shape);
	}
	
	%shape.setRepairRate(0);
}

//------------------------------------------------------------------------------

function RacingLaneZones_repairTick()
{
	cancel($RacingLaneZones_repairTickThread);
 
    if(!isObject($Team1.repairObjects) || !isObject($Team2.repairObjects))
        return;

	%count = $Team1.repairObjects.getCount();
	if(%count != 0)
	{
		%repair = $Team1.repairSpeed / %count;
		for (%i = 0; %i < %count; %i++)
		{
			%obj = $Team1.repairObjects.getObject(%i);
			%obj.setRepairRate(%repair);
		}
	}

	%count = $Team2.repairObjects.getCount();
	if(%count != 0)
	{
		%repair = $Team2.repairSpeed / %count;
		for (%i = 0; %i < %count; %i++)
		{
			%obj = $Team2.repairObjects.getObject(%i);
			%obj.setRepairRate(%repair);
		}
	}

	$RacingLaneZones_repairTickThread =
		schedule(100, 0, "RacingLaneZones_repairTick");
}

//-----------------------------------------------------------------------------

// to reset all the zones...
function RacingLaneZones_reset()
{
	RacingLaneZones_resetNamed("RedLane");
	RacingLaneZones_resetNamed("BlueLane");
}

function RacingLaneZones_resetNamed(%name)
{
	%group = nameToID(%name);

	if (%group != -1)
	{
		%count = %group.getCount();
		if (%count != 0)
		{
				for (%i = 0; %i < %count; %i++)
				{
					%zone = %group.getObject(%i);
					%zone.getDataBlock().reset(%zone, %name);
				}
				
				RacingLaneZones_repairTick();
		}
		else
			error("RacingLaneZones_resetNamed():" SPC
				"no TacticalZones found in" SPC %name SPC "group!");
	}
	else
		error("RacingLaneZones_reset(): group" SPC %name SPC "not found!");
}

//-----------------------------------------------------------------------------

function RacingLaneZones_call(%func)
{
	%group = nameToID("RacingLaneZones");

	if (%group != -1)
	{
		%count = %group.getCount();
		if (%count != 0)
		{
				for (%i = 0; %i < %count; %i++)
				{
					%zone = %group.getObject(%i);
					call(%func, %zone);
				}
		}
		else
			error("RacingLaneZones_call():" SPC
				"no TacticalZones found in RacingLaneZones group!");
	}
	else
		error("RacingLaneZones_call(): missing RacingLaneZones group!");
}

//-----------------------------------------------------------------------------

datablock TacticalZoneData(RacingLaneZone)
{
	category = "Tactical Zones"; // for the mission editor

	// The period is value is used to control how often the console
	// onTick callback is called while there are any objects
	// in the zone.  The default value is 100 MS.
	tickPeriodMS = 200;

	colorChangeTimeMS = 200;

	colors[1]  = "1 1 1 0.05";  // neutral
	colors[2]  = "1 0 0 0.1";   // red start zone
	colors[3]  = "0 0 1 0.1";   // blue start zone

	colors[6]  = "1 1 1 0.75";  // white flash
	colors[7]  = "0 0.25 0 0.75";   // red lane
	colors[8]  = "0.5 0.25 0 0.75"; // blue lane

	colors[11] = "0 1 0 0.75";   // bright green
	colors[12] = "1 1 0 0.75";   // bright yellow
	colors[13] = "1 0 0 0.75";   // bright red


    texture = "share/textures/rotc/zone.lane";
};

function RacingLaneZone::onAdd(%this, %zone)
{
	%zone.teamId = 0;

	%zone.zNumReds = 0;
	%zone.zNumBlues = 0;
}

function RacingLaneZone::onEnter(%this,%zone,%obj)
{
	//echo("entered territory zone");
	
	if(!%obj.getType() & $TypeMasks::ShapeBaseObjectType)
		return;

	if(false)
    {
		%team = %obj.getTeamId() == 1 ? $Team1 : $Team2;
		%val = 1 - %obj.getDamagePercent();
		%val = %val / %team.numPlayersOnRoundStart;
		%team.score += %val;
		%zone.zValue -= %val;
		%zone.setColor(1, 8 + %zone.getTeamId(), %zone.zValue);
		%zone.zBlocked = true;
		%obj.kill();
	}

	%obj.getDataBlock().updateRacingLaneZone(%obj, %zone, 0);
}

function RacingLaneZone::onLeave(%this, %zone, %obj)
{
	%obj.getDataBlock().updateRacingLaneZone(%obj, 0, %zone);
}

function RacingLaneZone::onTick(%this, %zone)
{

}

function RacingLaneZone::reset(%this, %zone, %parentName)
{
	if($Game::TeamDragRaceState == 0)
	{
		%team = 0;
		if(%parentName $= "RedLane")
			%team = 1;
		else if(%parentName $= "BlueLane")
			%team = 2;

		%zone.setTeamId(%team);
		%zone.isProtected = !(%zone.startZone || %zone.freeZone);

		if(!%zone.startZone)
			%color = 13;
		else
			%color = 1 + %zone.getTeamId();
		%zone.setColor(%color, %color, 1);
	}
	else if($Game::TeamDragRaceState == 1)
	{
		if(!%zone.startZone)
			%zone.setColor(12, 12, 1);
	}
	else if($Game::TeamDragRaceState == 2)
	{
		if(!%zone.startZone)
			%zone.setColor(11, 11, 1);
	}
	else if($Game::TeamDragRaceState == 3)
	{
		if(!%zone.startZone)
		{
			if(%zone.getTeamId() == 0)
				%color = 13;
			else 
				%color = 6 + %zone.getTeamId();
			%zone.setColor(%color, %color, 1);
		}
	}

	if($Game::TeamDragRaceState < 4)
	{
  		if(!%zone.startZone)
  			%zone.flash(15, 15, 1);
	}
}

