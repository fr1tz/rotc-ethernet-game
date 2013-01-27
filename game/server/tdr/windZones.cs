//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function WindZone_find(%name)
{
	%group = nameToID("WindZones");

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
			error("WindZones_call():" SPC
				"no TacticalZones found in WindZones group!");
	}
	else
		error("WindZones_call(): missing WindZones group!");

	return -1;
}

//------------------------------------------------------------------------------

function WindZones_enableRepair(%shape)
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

function WindZones_disableRepair(%shape)
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

function WindZones_repairTick()
{
	cancel($WindZones_repairTickThread);
 
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

	$WindZones_repairTickThread =
		schedule(100, 0, "WindZones_repairTick");
}

//-----------------------------------------------------------------------------

// to reset all the zones...
function WindZones_reset()
{
	WindZones_resetNamed("RedWind", 1);
	WindZones_resetNamed("BlueWind", 2);
}

function WindZones_resetNamed(%name, %teamId)
{
	%group = nameToID(%name);
	if (%group != -1)
		WindZones_resetZones(%group, %teamId);
	else
		error("WindZones_resetNamed(): group" SPC %name SPC "not found!");
}

function WindZones_resetZones(%group, %teamId)
{
	%count = %group.getCount();
	if (%count != 0)
	{
		for (%i = 0; %i < %count; %i++)
		{
			%obj = %group.getObject(%i);
			if(%obj.getClassName() $= "SimGroup")
				WindZones_resetZones(%obj, %teamId);
			else if(%obj.getClassName() $= "TacticalZone")
				%obj.getDataBlock().reset(%obj, %teamId);
		}
	}
	else
	{
		error("WindZones_resetZones():" SPC
			"no TacticalZones found in" SPC %name SPC "group!");
	}
}

//-----------------------------------------------------------------------------

function WindZones_call(%func)
{
	%group = nameToID("WindZones");

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
			error("WindZones_call():" SPC
				"no TacticalZones found in WindZones group!");
	}
	else
		error("WindZones_call(): missing WindZones group!");
}

//-----------------------------------------------------------------------------

datablock TacticalZoneData(WindZone)
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

	colors[15] = "0 1 0 0.1";   // invisible 

    texture = "share/textures/rotc/zone";
};

function WindZone::onAdd(%this, %zone)
{
	%zone.teamId = 0;

	%zone.zNumReds = 0;
	%zone.zNumBlues = 0;
}

function WindZone::onEnter(%this,%zone,%obj)
{
	//echo("entered territory zone");
	
	if(!%obj.getType() & $TypeMasks::ShapeBaseObjectType)
		return;

//	if(%obj.isCAT)
//	{
//		%obj.zWindZone = %zone;
//		updateWind(%obj);
//	}
}

function WindZone::onLeave(%this, %zone, %obj)
{

}

function WindZone::onTick(%this, %zone)
{

}

function WindZone::reset(%this, %zone, %teamId)
{
	%zone.setColor(15, 15, 0);

	if(isObject(%zone.zPhysicalZone))
		%zone.zPhysicalZone.delete();

	return;

	%p = new PhysicalZone() {
		polyhedron = "0 0 0 1 0 0 0 -1 0 0 0 1";
		locked = true;
	};
	MissionCleanup.add(%p);

	%p.zWindZone = %zone;
	%zone.zPhysicalZone = %p;

	%t = %zone.getTransform();
	%p.setScale(VectorScale(%zone.getScale(),2));
	if(MatrixGetColumn(%t, 2) < 0)
	{
		%b = %zone.getWorldBox();
		%t = setWord(%t, 0, getWord(%b,3));
		%t = setWord(%t, 1, getWord(%b,4));
		%t = setWord(%t, 2, getWord(%b,5));
		%p.setTransform(%t);
	}
	else
	{
		if(false)
		{
			%b = %zone.getWorldBox();
			%t = setWord(%t, 0, getWord(%b,0));
			%t = setWord(%t, 1, getWord(%b,1));
			%t = setWord(%t, 2, getWord(%b,2));
			%p.setTransform(%t);
		}
		else
		{
			%p.setTransform(%t);
			%mat1 = %p.getTransform();
			%pos1 = %zone.getWorldBoxCenter();
			%pos2 = %p.getWorldBoxCenter();
			%vec = VectorSub(%pos1, %pos2);
			%mat2 =  MatrixCreate(%vec, "0 0 0");
			%mat = MatrixMultiply(%mat1, %mat2);
			%p.setTransform(%mat);
		}
	}
	updateWindStrength(%zone, %zone.wind);
}

//-----------------------------------------------------------------------------

function updateWindStrength(%windZone, %newStrength)
{
	%physicalZone = %windZone.zPhysicalZone;
	if(!isObject(%physicalZone))
	{
		error("Wind zone" SPC %windZone.getId() SPC "has no physical zone!");
		return;
	}
	if(%physicalZone.zWindStrength != %newStrength)
	{
		%physicalZone.zWindStrength = %newStrength;
		%windDir = MatrixGetColumn(%windZone.getTransform(), 1);
		%windVec = VectorScale(%windDir, 7500 * %physicalZone.zWindStrength);
		%physicalZone.appliedForce = %windVec;
		%physicalZone.setTransform(%physicalZone.getTransform());
		//error(%physicalZone.appliedForce);
	}
}

// how far into the track is a pos?
function trackPosition(%pos)
{
	// KISS: just use y component
	return getWord(%pos, 1);
}

function updateWind(%obj)
{
	%windZone = %obj.zWindZone;
	%pos1 = trackPosition(%obj.getPosition());
	error("updating wind for" SPC %obj.getShapeName());

	if(%obj.getTeamId() == 1)
		%opponents = $Game::BlueCats;
	else if(%obj.getTeamId() == 2)
		%opponents = $Game::RedCats;

	%maxdist = 0;
	%count = %opponents.getCount();
	for(%i = 0; %i < %count; %i++)
	{
		%player = %opponents.getObject(%i);
	
		%pos2 = trackPosition(%player.getPosition());
		error(" " @ %pos1 SPC "<->" SPC %pos2);

		%dist = %pos2 - %pos1;

		if(mAbs(%dist) > %maxdist)
			%maxdist = %dist;
	}

	if(mAbs(%maxdist) > 50)
	{
		%strength = %maxdist/50;
	}
	else		
		%strength = 1;

	if(%strength > 2)
		%strength = 2;

	error(%maxdist SPC "->" SPC %strength);

	updateWindStrength(%windZone, %strength);	
}

function updateWinds()
{
	if(getSimTime() == $Game::LastWindUpdateTime)
		return;

	$Game::LastWindUpdateTime = getSimTime();
}

