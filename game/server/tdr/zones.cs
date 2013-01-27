//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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
	RacingLaneZones_resetNamed("RedLane", 1);
	RacingLaneZones_resetNamed("BlueLane", 2);
	RacingLaneZones_repairTick();
}

function RacingLaneZones_resetNamed(%name, %teamId)
{
	%group = nameToID(%name);
	if (%group != -1)
		RacingLaneZones_resetZones(%group, %teamId);
	else
		error("RacingLaneZones_resetNamed(): group" SPC %name SPC "not found!");
}

function RacingLaneZones_resetZones(%group, %teamId)
{
	%count = %group.getCount();
	if (%count != 0)
	{
		for (%i = 0; %i < %count; %i++)
		{
			%obj = %group.getObject(%i);
			if(%obj.getClassName() $= "SimGroup")
				RacingLaneZones_resetZones(%obj, %teamId);
			else if(%obj.getClassName() $= "TacticalZone")
				%obj.getDataBlock().reset(%obj, %teamId);
		}
	}
	else
	{
		error("RacingLaneZones_resetZones():" SPC
			"no TacticalZones found in" SPC %name SPC "group!");
	}
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

	colors[1]  = "1 1 1 0.2";  // end zone
	colors[2]  = "1 0 0 0.1";   // red start zone
	colors[3]  = "0 0 1 0.1";   // blue start zone

	colors[6]  = "1 1 1 0.75";  // white flash
	colors[7]  = "1 0 0 0.75";   // red lane
	colors[8]  = "0 0 1 0.75"; // blue lane

	colors[11] = "0 1 0 0.75";   // bright green
	colors[12] = "1 1 0 0.75";   // bright yellow
	colors[13] = "1 0 0 0.75";   // bright red

	colors[15] = "1 1 1 0.1";   // invisible 

    texture = "share/textures/rotc/zone";
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

	if(%zone.endZone)
    {
		%team = %obj.getTeamId() == 1 ? $Team1 : $Team2;
		%val = 1 - %obj.getDamagePercent();
		%val = %val / %team.numPlayersOnRoundStart;
		%team.score += %val;
		%zone.setColor(1, 6 + %zone.getTeamId(), %team.score);
		%obj.kill("LeftMissionArea");
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

function RacingLaneZone::reset(%this, %zone, %teamId)
{
	if($Game::TeamDragRaceState == 0)
	{
		%zone.setTeamId(%teamId);
		%zone.isProtected = !(%zone.startZone || %zone.freeZone);

		if(%zone.startZone)
			%color = 1 + %zone.getTeamId();
		else if(%zone.endZone)
			%color = 1;
		else
			%color = 13;
		%zone.setColor(%color, %color, 1);
	}
	else if($Game::TeamDragRaceState == 1)
	{
		if(!%zone.startZone && !%zone.endZone)
			%zone.setColor(12, 12, 1);
	}
	else if($Game::TeamDragRaceState == 2)
	{
		if(!%zone.startZone && !%zone.endZone)
			%zone.setColor(11, 11, 1);
	}
	else if($Game::TeamDragRaceState == 3)
	{
		if(!%zone.startZone && !%zone.endZone)
		{
			if(%zone.getTeamId() == 0)
				%color = 13;
			else 
				%color = 6 + %zone.getTeamId();
			%zone.setColor(%color, %color, 1);
		}
	}

	if(%zone.borderTop == 0)
		%zone.setColor(15, 15, 0);

	if($Game::TeamDragRaceState < 4)
	{
  		if(!%zone.startZone)
  			%zone.flash(15, 15, 1);
	}
}

