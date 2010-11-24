//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - tacticalZones.cs
// Script-stuff for tactical zones
//------------------------------------------------------------------------------

//
// the following dynamic fields can be added to the TacticalZone *objects*...
//	 initialOwner:  team id set on reset
//	 initiallyProtected: protected on reset?
//

function TacticalZoneData::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type.  For the mission editor
	%obj = new TacticalZone() {
		dataBlock = %data;
		initialOwner = 0;
		initiallyProtected = false;
	};
	return %obj;
}

//------------------------------------------------------------------------------
// Territory Zones

function TerritoryZone_find(%name)
{
	%group = nameToID("TerritoryZones");

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
			error("TerritoryZones_call():" SPC
				"no TacticalZones found in TerritoryZones group!");
	}
	else
		error("TerritoryZones_call(): missing TerritoryZones group!");

	return -1;
}

//------------------------------------------------------------------------------

function TerritoryZones_enableRepair(%shape)
{
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

function TerritoryZones_disableRepair(%shape)
{
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

function TerritoryZones_repairTick()
{
	cancel($TerritoryZones_repairTickThread);
 
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

	$TerritoryZones_repairTickThread =
		schedule(100, 0, "TerritoryZones_repairTick");
}

//-----------------------------------------------------------------------------

// to reset all the territory zones...
function TerritoryZones_reset()
{
	%group = nameToID("TerritoryZones");

	if (%group != -1)
	{
		%count = %group.getCount();
		if (%count != 0)
		{
				for (%i = 0; %i < %count; %i++)
				{
					%zone = %group.getObject(%i);
					%zone.getDataBlock().reset(%zone);
				}
				
				TerritoryZones_repairTick();
		}
		else
			error("TerritoryZones_reset():" SPC
				"no TacticalZones found in TerritoryZones group!");
	}
	else
		error("TerritoryZones_reset(): missing TerritoryZones group!");
}

//-----------------------------------------------------------------------------

function TerritoryZones_call(%func)
{
	%group = nameToID("TerritoryZones");

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
			error("TerritoryZones_call():" SPC
				"no TacticalZones found in TerritoryZones group!");
	}
	else
		error("TerritoryZones_call(): missing TerritoryZones group!");
}

//-----------------------------------------------------------------------------
// Territory Zone Sounds

datablock AudioProfile(ZoneAquiredSound)
{
	filename = "share/sounds/rotc/events/zone.aquired.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(ZoneAttackedSound)
{
	filename = "share/sounds/rotc/events/zone.attacked.wav";
	description = AudioCritical2D;
	preload = true;
};


//-----------------------------------------------------------------------------
// Territory Zone

datablock TacticalZoneData(TerritoryZone)
{
	category = "Tactical Zones"; // for the mission editor

	// The period is value is used to control how often the console
	// onTick callback is called while there are any objects
	// in the zone.  The default value is 100 MS.
	tickPeriodMS = 200;

	colorChangeTimeMS = 200;

	colors[1]  = "1 1 1 0.05";  // neutral
	colors[2]  = "1 0 0 0.1";   // red
	colors[3]  = "0 0 1 0.1";   // blue
	colors[4]  = "1 0.5 0 0.1";   // red blocked
	colors[5]  = "0 0.5 1 0.1";   // blue blocked

	colors[6]  = "1 1 1 0.75";  // white flash
	colors[7]  = "1 0 0 0.75";   // red flash
	colors[8]  = "0 0 1 0.75";   // blue flash
	colors[9]  = "1 0.5 0 0.75";   // red blocked flash
	colors[10] = "0 0.5 1 0.75";   // blue blocked flash

	colors[14] = "0 1 0 0.4";   // protected
	colors[15] = "1 1 1 1"; 

    texture = "share/textures/rotc/zone";
};

function TerritoryZone::onAdd(%this, %zone)
{
	%zone.teamId = 0;

	%zone.numReds = 0;
	%zone.numBlues = 0;
}

function TerritoryZone::onEnter(%this,%zone,%obj)
{
	//echo("entered territory zone");
	
	if(!%obj.getType() & $TypeMasks::ShapeBaseObjectType)
		return;
	
	%this.onTick(%zone);
	
	%obj.getDataBlock().updateZone(%obj, %zone);
}

function TerritoryZone::onLeave(%this,%zone,%obj)
{
	// note that this function does not get called immediately when an
	// object leaves the zone but when the zone registers the object's
	// absence when the zone ticks.

	//echo("left territory zone");
	
	if(!%obj.getType() & $TypeMasks::ShapeBaseObjectType)
		return;
		
	%this.onTick(%zone);

	%obj.getDataBlock().updateZone(%obj, 0);
}

function TerritoryZone::onTick(%this, %zone)
{
	%zone.numReds = 0;
	%zone.numBlues = 0;
	
	for(%i = 0; %i < %zone.getNumObjects(); %i++)
	{
		%obj = %zone.getObject(%i);
		if(%obj.getType() & $TypeMasks::PlayerObjectType
		&& %obj.isCAT)
		{
            if(%zone.protected && %obj.getTeamId() != %zone.getTeamId())
                %obj.kill();
            else if(%obj.getTeamId() == $Team1.teamId)
				%zone.numReds++;
			else if(%obj.getTeamId() == $Team2.teamId)
				%zone.numBlues++;
		}
	}

	%this.updateOwner(%zone);
}

function TerritoryZone::updateOwner(%this, %zone)
{
	%connectedToRed = false;
	%connectedToBlue = false;

	for(%i = 0; %i < 4; %i++)
	{
		%z = %zone.neighbour[%i];
		if(isObject(%z))
		{
			if(%z.getTeamId() == 1)
				%connectedToRed = true;
			if(%z.getTeamId() == 2)
				%connectedToBlue = true;
		}
	}

	if(%zone.numReds != 0 && %zone.numBlues == 0 && %connectedToRed)
	{
		%this.setZoneOwner(%zone, 1);
	}
	else if(%zone.numBlues != 0 && %zone.numReds == 0 && %connectedToBlue)
	{
		%this.setZoneOwner(%zone, 2);
	}
	else if(%zone.numReds != 0 && %zone.numBlues != 0 && %connectedToRed && %connectedToBlue)
	{
		%this.setZoneOwner(%zone, 0);
	}

	%zone.blocked = false;

	%color = 1;
	if(%zone.getTeamId() == 2 && %zone.numReds != 0)
	{
		%zone.blocked = true;
		%color = 5;
	}
	else if(%zone.getTeamId() == 1 && %zone.numBlues != 0)
	{
		%zone.blocked = true;
		%color = 4;
	}
	else if(%zone.getTeamId() == 2)
		%color = 3;
	else if(%zone.getTeamId() == 1)
		%color = 2;

	%zone.setColor(%color, %color, 1);

	if(%color != %zone.color)
		%zone.flash(%color + 5, %color + 5, 1);

	%zone.color = %color;
}

function TerritoryZone::setZoneOwner(%this, %zone, %teamId)
{
	%oldTeamId = %zone.getTeamId();
	
	if(%teamId == %oldTeamId)
		return;
		
	if(%oldTeamId == 1)
		$Team1.numTerritoryZones--;
	else if(%oldTeamId == 2)
		$Team2.numTerritoryZones--;
		
	%zone.setTeamId(%teamId);
	
	if(%teamId == 1)
		$Team1.numTerritoryZones++;
	else if(%teamId == 2)
		$Team2.numTerritoryZones++;

	for(%idx = 0; %idx < ClientGroup.getCount(); %idx++)
	{
		%client = ClientGroup.getObject(%idx);
	
		%sound = 0;

		if(%client.team == $Team1)
		{
			if(%teamId == 1)
				%sound = ZoneAquiredSound;
			else if(%teamId == 2)
				%sound = ZoneAttackedSound;
		}
		else if(%client.team == $Team2)
		{
			if(%teamId == 2)
				%sound = ZoneAquiredSound;
			else if(%teamId == 1)
				%sound = ZoneAttackedSound;
		}

		if(isObject(%sound))
			%client.play2D(%sound);
	}
	
	for(%i = 0; %i < %zone.getNumObjects(); %i++)
	{
		%obj = %zone.getObject(%i);
		if(%obj.getType() & $TypeMasks::ShapeBaseObjectType)
			%obj.getDataBlock().updateZone(%obj, 0);
	}
		
	echo("Number of zones:" SPC
		$Team1.numTerritoryZones SPC "red /" SPC
		$Team2.numTerritoryZones SPC "blue");
		
	checkRoundEnd();
}

function TerritoryZone::reset(%this, %zone)
{
	for(%i = 0; %i < 4; %i++)
	{	
		%z = TerritoryZone_find(%zone.connection[%i]);
		if(isObject(%z))
			%zone.neighbour[%i] = %z;
		else
			%zone.neighbour[%i] = -1;
	}

	if( %zone.initialOwner != 0 )
		%this.setZoneOwner(%zone, %zone.initialOwner);
	else
		%this.setZoneOwner(%zone, 0);

	%zone.protected = %zone.initiallyProtected;

	%this.updateOwner(%zone);
}


//-----------------------------------------------------------------------------
// AirfieldZone

datablock TacticalZoneData(AirfieldZone)
{
	category = "Tactical Zones"; // for the mission editor

	tickPeriodMS = 250;
	neutralColor			 = "0.0 0.5 0.0 0.3";
};

function AirfieldZone::onEnter(%this,%zone,%obj)
{
	echo("entered airfield-tactical zone");

	if(%obj.getDataBlock().isAircraft)
		%obj.setHoverMode();
}

function AirfieldZone::onLeave(%this,%zone,%obj)
{
	echo("left airfield-tactical zone");
	
	if(%obj.getDataBlock().isAircraft)
		%obj.setFlyMode();
}

function AirfieldZone::onTick(%this,%trigger)
{

}


