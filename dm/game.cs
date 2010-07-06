//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function aiAdd(%teamid, %weaponNum)
{
	if( !isObject($aiPlayers) )
	{
		$aiPlayers = new Array();
		MissionCleanup.add($aiPlayers);
	}

	%nameadd = "_" @ $aiPlayers.count();
	if(isObject($aiPlayers)) {
		%nameadd = "_" @ $aiPlayers.count();
	}

	%spawnSphere = pickSpawnSphere(%teamid);

	if(%spawnSphere.spawnLight)
	{
		if(%teamid == 1)
			%playerData = RedLightCat;
		else
			%playerData = BlueLightCat;
	}
	else
	{
		if(%teamid == 1)
			%playerData = RedStandardCat;
		else
			%playerData = BlueStandardCat;
	}

	%player = new AiPlayer() {
		dataBlock = %playerData;
		path = "";
		teamId = $aiPlayers.count();
	};
    %player.setTeamId(%player);
	MissionCleanup.add(%player);

	%pos = getRandomHorizontalPos(%spawnSphere.position,%spawnSphere.radius);
	%player.setShapeName("wayne" @ %nameadd);
	%player.setTransform(%pos);

	%player.weapon = %weaponNum;
	%player.charge = 100;

	$aiPlayers.push_back("",%player);

	return %player;
}


//------------------------------------------------------------------------------
// GameConnection

function GameConnection::togglePlayerForm(%this)
{
	if(!isObject(%this.player))
		return;
		
	%tagged = %this.player.isTagged();
	%pos = %this.player.getWorldBoxCenter();

	if(%this.player.getClassName() $= "Player")
	{
		// CAT -> etherform
	
		if( %this.team == $Team1 )
			%data = RedEtherform;
		else
			%data = BlueEtherform;

		%obj = new Etherform() {
			dataBlock = %data;
			client = %this;
			teamId = %this.team.teamId;
		};
	}
	else
	{
		// etherform -> CAT
		
		if(%this.player.getDamageLevel() >= %this.player.getDataBlock().maxDamage)
		{
			bottomPrint(%this, "You need health to manifest!", 3, 1 );
			return;
		}
		
		InitContainerRadiusSearch(%pos, 0.0001, $TypeMasks::TacticalZoneObjectType);
		if(containerSearchNext() == 0)
		{
			bottomPrint(%this, "Can only manifest inside zones!", 3, 1 );
			return;
		}

		if( %this.team == $Team1 )
			%data = RedStandardCat;
		else
			%data = BlueStandardCat;

		%obj = new Player() {
			dataBlock = %data;
			client = %this;
			teamId = %this.team.teamId;
		};

		$aiTarget = %obj;
	}
	
	MissionCleanup.add(%obj);
	
	%mat = %this.player.getTransform();
	%dmg = %this.player.getDamageLevel();
	%nrg = %this.player.getEnergyLevel();
	%buf = %this.player.getDamageBufferLevel();
	%vel = %this.player.getVelocity();

	%obj.setTransform(%mat);
	%obj.setTransform(%pos);
	%obj.setDamageLevel(%dmg);
	%obj.setDamageBufferLevel(%buf);
	
	if(%tagged)
		%obj.setTagged();

	%this.control(%obj);
	
	if(%obj.getType() & $TypeMasks::PlayerObjectType)
	{
		// etherform -> CAT
		
		// remove any z-velocity...
		%vel = getWord(%vel, 0) SPC getWord(%vel, 1) SPC "0";
	
		%this.player.delete();

		%obj.setEnergyLevel(%nrg);
		%obj.setVelocity(VectorScale(%vel, 0.25));
		
		%obj.startFade(1000,0,false);
		%obj.playAudio(0, CatSpawnSound);
	}
	else
	{
		// CAT -> etherform
	
		if(%this.player.getDamageState() $= "Enabled")
		{
			//if(%this.player.getDataBlock().damageBuffer - %buf > 1)
			//{
			//	%this.player.setDamageState("Disabled");
			//	%this.player.playDeathAnimation(0, 0);
			//}
			//else
			//{
				%this.player.setDamageState("Destroyed");
			//}
		}

		%obj.setEnergyLevel(%nrg);
		%obj.applyImpulse(%pos, VectorScale(%vel,100));
		%obj.playAudio(0, EtherformSpawnSound);
	}

	%this.player = %obj;
}

//------------------------------------------------------------------------------
// Etherform

function Etherform::fade(%this)
{
    %this.getDataBlock().fade(%this);
}

function EtherformData::fade(%this, %obj)
{
    %fadeAmount = 0.1;
    %obj.setDamageLevel(%obj.getDamageLevel() + %fadeAmount);
    %obj.fadeThread = %obj.schedule(50, "fade");
}

function Etherform::reset(%this)
{
    %this.getDataBlock().reset(%this);
}

function EtherformData::reset(%this, %obj)
{
    %obj.setDamageLevel(0);
    %obj.setDamageBufferLevel(%this.damageBuffer);
    %obj.setEnergyLevel(%this.maxEnergy);
}

function EtherformData::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);

	// start singing...
	%obj.playAudio(1, EtherformSingSound);
}

function EtherformData::onDamage(%this, %obj, %delta)
{
	%totalDamage = %obj.getDamageLevel();
	if(%totalDamage >= %this.maxDamage)
	{
        %obj.setRepairRate(0.5);
    }
}

//------------------------------------------------------------------------------
// ShapeBase

function ShapeBase::useWeapon(%this, %nr)
{
    %teamIdStore = %this.teamId;
    
    if(%this.client)
        %this.teamId = %this.client.team.teamId;

	%this.getDataBlock().useWeapon(%this, %nr);
 
    %this.teamId = %teamIdStore;
}

//------------------------------------------------------------------------------
// Zones

function TerritoryZone::updateOwner(%this, %zone)
{
	if(%zone.numReds != 0 && %zone.numBlues == 0)
	{
		%this.setZoneOwner(%zone, 1);
	}
	else if(%zone.numBlues != 0 && %zone.numReds == 0)
	{
		%this.setZoneOwner(%zone, 2);
	}
	else
	{
		%this.setZoneOwner(%zone, 0);
	}
}

function TerritoryZones_enableRepair(%shape)
{
    return; // No zone repair in deathmatch
}

function TerritoryZones_disableRepair(%shape)
{
    return; // No zone repair in deathmatch
}

function TerritoryZones_repairTick()
{
    return; // No zone repair in deathmatch
}

//------------------------------------------------------------------------------
// Misc

function checkRoundEnd()
{
    return; // No rounds in deathmatch
}
