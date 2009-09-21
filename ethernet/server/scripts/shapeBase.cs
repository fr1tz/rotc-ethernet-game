//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - shapebase.cs
// This file contains ShapeBase methods used by all the derived classes
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// ShapeBase object
//-----------------------------------------------------------------------------

function ShapeBase::damage(%this, %sourceObject, %position, %damage, %damageType)
{
	// All damage applied by one object to another should go through this
	// method. This function is provided to allow objects some chance of
	// overriding or processing damage values and types.  As opposed to
	// having weapons call ShapeBase::applyDamage directly.
	// Damage is redirected to the datablock, this is standard proceedure
	// for many built in callbacks.
	return %this.getDataBlock().damage(%this, %sourceObject, %position, %damage, %damageType);
}

//-----------------------------------------------------------------------------

function ShapeBase::setDamageDt(%this, %damageAmount, %damageType)
{
	// This function is used to apply damage over time.  The damage
	// is applied at a fixed rate (50 ms).  Damage could be applied
	// over time using the built in ShapBase C++ repair functions
	// (using a neg. repair), but this has the advantage of going
	// through the normal script channels.
	if (%this.getState() !$= "Dead") {
		%this.damage(0, "0 0 0", %damageAmount, %damageType);
		%this.damageSchedule = %this.schedule(50, "setDamageDt", %damageAmount, %damageType);
	}
	else
		%this.damageSchedule = "";
}

function ShapeBase::clearDamageDt(%this)
{
	if( %this.damageSchedule !$= "" )
	{
		cancel(%this.damageSchedule);
		%this.damageSchedule = "";
	}
}

//-----------------------------------------------------------------------------

function ShapeBase::getTeamObject(%this)
{
	switch(%this.getTeamId())
	{
		case 0: return $Team0;
		case 1: return $Team1;
		case 2: return $Team2;
	}
	
	return -1;
}

function ShapeBase::setTeam(%this, %teamId)
{
	%this.setTeamId(%teamId);
	for(%i = 0; %i < 8; %i++)
	{
		%mount = %this.getMountedObject(%i);
		if( isObject(%mount) )
			%mount.setTeam(%teamId);
	}
}

function ShapeBase::getTeam(%this)
{
	return %this.getTeamId();
}

//-----------------------------------------------------------------------------

function ShapeBase::incExperience(%this, %value)
{
	%this.experience += %value;
}

//-----------------------------------------------------------------------------

function ShapeBase::useWeapon(%this, %nr)
{
	%this.getDataBlock().useWeapon(%this, %nr);
}

//-----------------------------------------------------------------------------

function ShapeBase::checkTagged(%this, %tagger)
{
	%this.checkTaggedThread = %this.schedule(500,"checkTagged");

	%pos = %this.getPosition();
	%sky = nameToID("Sky");
	if(%sky != -1)
	{
		%radius = %sky.visibleDistance;
	}
	else
	{
		error("ShapeBase::checkTagged(): 'Sky' not found to get visible distance from");
		return;
	}
	%type = $TypeMasks::PlayerObjectType
			| $TypeMasks::VehicleObjectType
			| $TypeMasks::TurretObjectType;

	InitContainerRadiusSearch(%pos, %radius, %type);
	while( (%targetObject = containerSearchNext()) != 0 )
	{
		// don't evaluate teammates...
		if( %targetObject.teamId == %this.teamId )
				continue;
				
		// don't evaluate non-team objects
		if( %targetObject.teamId == 0 )
				continue;
				
		// don't evaluate dead players
		if( %targetObject.getDamageState() !$= "Enabled" )
				continue;

		%start = %this.getWorldBoxCenter();
		%end	= %targetObject.getEyePoint();
		
		%ground = containerRayCast(%start, %end, $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType , %this);
		
		if( %ground == 0 )
		{
			//echo("tagger found");
			return;
		}
	}

	//
	// no tagger which can still see us found...
	//
	
	for( %idx = 0; %idx < ClientGroup.getCount(); %idx++ )
	{
		%client = ClientGroup.getObject( %idx );
		if(isObject(%client.player) && %client.player.getCurrTagged() == %this)
			%client.player.setCurrTagged(0);
	}

	%this.clearTagged();
}

//-----------------------------------------------------------------------------

function ShapeBase::setDiscTarget(%this, %target)
{
    cancel(%this.discTargetThread);
	%pos = %target.getWorldBoxCenter();
    %this.setCurrTarget(%target, %pos);
    %this.discTargetThread = %this.schedule(2000, "clearDiscTarget");
}

function ShapeBase::clearDiscTarget(%this)
{
    cancel(%this.discTargetThread);
    %this.setCurrTarget(0, "0 0 0");
}

//-----------------------------------------------------------------------------

function ShapeBase::setInflictedDamageSoundPitch(%this, %pitch, %locked)
{
    %this.inflictedDamageSoundPitch = %pitch;
    %this.inflictedDamageSoundLocked = %locked;

    cancel(%this.inflictedDamageThread);
    %this.inflictedDamageThread = %this.schedule(0, "playInflictedDamageSound");
}

function ShapeBase::playInflictedDamageSound(%this)
{
    if(%this.client)
    {
        %pitch = 0.9 + %this.inflictedDamageSoundPitch / 2;
        if(%this.inflictedDamageSoundLocked)
            %this.client.play2D(DamageSoundTwo, %pitch);
        else
            %this.client.play2D(DamageSoundOne, %pitch);
    }

    %this.inflictedDamageSoundPitch = 0;
}

//-----------------------------------------------------------------------------
// ShapeBase datablock
//-----------------------------------------------------------------------------

// *** callback function: called by engine
function ShapeBaseData::onAdd(%this,%obj)
{
	if(%this.name)
	{
		%obj.setShapeName(%this.name);
	}
	else if(%obj.client)
	{
		%obj.setShapeName(%obj.client.name);
		%obj.getHudInfo().markAsControlled(%obj.client, 0);
	}
	
	// Default dynamic armor stats...
	%obj.setDamageBufferRechargeRate(%this.damageBufferRechargeRate);
	%obj.setDamageBufferDischargeRate(%this.damageBufferDischargeRate);
	%obj.setEnergyRechargeRate(%this.energyRechargeRate);
	%obj.setRepairRate(0);
	
	%obj.checkTaggedThread = %obj.schedule(500,"checkTagged");
}

// *** callback function: called by engine
function ShapeBaseData::onRemove(%this, %obj)
{
	%obj.beingRemoved = true;

	cancel(%obj.checkTaggedThread);
	cancel(%obj.damageSchedule);
	
	// relieve obj from simple control if needed...
	if(%obj.isUnderSimpleControl())
		%obj.removeFromSimpleControl();
	
	// if obj was created by a spawn, inform it so it can spawn a new one
	if(isObject(%obj.spawn))
		%obj.spawn.getDataBlock().respawn(%obj.spawn);
}

// callback function: called by engine
function ShapeBaseData::onNewDataBlock(%this,%obj)
{
	// avoid console spam
}

// *** callback function: called by engine
function ShapeBaseData::onLeaveMissionArea(%this,%obj)
{
	//echo("left mission area");
	%obj.setDamageDt(10,"LeftMissionArea");
}

// *** callback function: called by engine
function ShapeBaseData::onEnterMissionArea(%this,%obj)
{
	//echo("entered mission area");
	%obj.clearDamageDt();
}

// *** callback function: called by engine
function ShapeBaseData::onCurrTaggedSelected(%this, %obj, %target)
{
	// tag enemies...
	if( %target.teamId > 0 && %target.teamId != %obj.teamId )
		%target.setTagged();
}

// *** callback function: called by engine
function ShapeBaseData::onCurrTaggedDeSelected(%this, %obj, %target)
{

}

// *** Callback function: called by engine
function ShapeBaseData::onCollision(%this,%obj,%col,%vec,%vecLen)
{
//	error("Collision:" SPC %obj.getShapeName() SPC "with" SPC
//		%col.getShapeName() SPC "with speed" SPC %vecLen);
}

// default ShapeBaseData::getBleed() method...
// called by ShapeBaseData::damage()
function ShapeBaseData::getBleed(%this, %obj, %dmg)
{
	return 0;
}

// called by ShapeBase::damage()
function ShapeBaseData::damage(%this, %obj, %sourceObject, %pos, %damage, %damageType)
{
	// reduce damage based on energy level...
	%energyScale = %obj.getEnergyLevel() / %obj.getDataBlock().maxEnergy;
	%damage -= %damage * %energyScale * 0.50;

	%damageBufStore = %obj.getDamageBufferLevel();

	%damageDealt = %obj.applyDamage(%damage);
    %bufDamageDealt = %damageBufStore - %obj.getDamageBufferLevel();

	%realSourceObject = 0;
	if(%sourceObject.getType() & $TypeMasks::ProjectileObjectType)
		%realSourceObject = %sourceObject.getSourceObject();
	else if(%sourceObject.getType() & $TypeMasks::ShapeBaseObjectType)
		%realSourceObject = %sourceObject.client.player;
	if(%realSourceObject != 0
	&& %realSourceObject.teamId != %obj.teamId
	&& %realSourceObject.getDamageState() $= "Enabled")
	{
        %this.onHitEnemy(
            %realSourceObject,
            %obj,
            %damage,
            %damageDealt+%bufDamageDealt
        );
	}
	
	// eyecandy: ain't got time to bleed?...
	%bleed = %this.getBleed(%obj, %damageDealt);
	if(isObject(%bleed))
	{
		%norm = VectorNormalize(VectorSub(%pos, %obj.getWorldBoxCenter()));
		%bpos = %damageType == $DamageType::Impact ? %pos : %obj.getWorldBoxCenter();
		createExplosionOnClients(%bleed, %bpos, %norm);
	}
 
    // Update stats...
    if(%obj.client)
    {
        %obj.client.stats.healthLost += %damageDealt;
        %obj.client.updateStats();
    }
    if(%realSourceObject.client)
    {
        %realSourceObject.client.stats.healthTaken += %damageDealt;
        %realSourceObject.client.updateStats();
    }

	return %damageDealt;
}

// script function called by territory zone code
function ShapeBaseData::updateZone(%this, %obj, %newZone)
{
	%ownTeamId = %obj.getTeamId();

	%inZone = false;
	%inOwnZone = false;
	%inEnemyZone = false;
    %zoneTeamId = -1;
    
	%pos = %obj.getPosition();
	InitContainerRadiusSearch(%pos, 0.0001, $TypeMasks::TacticalZoneObjectType);
	while( (%srchObj = %newZone) != 0 || (%srchObj = containerSearchNext()) != 0)
	{
		%newZone = 0;

		%inZone = true;

		%zoneTeamId = %srchObj.getTeamId();

		if(%zoneTeamId != %ownTeamId && %zoneTeamId != 0)
		{
			%inEnemyZone = true;
			break;
		}
		else if(%zoneTeamId == %ownTeamId)
		{
			%inOwnZone = true;
		}
	}

	if(%obj.beingRemoved
	|| %obj.isCAT
	|| !%inOwnZone
	|| %inEnemyZone
	|| %obj.getDamageLevel() == 0
	)
		TerritoryZones_disableRepair(%obj);
	else
		TerritoryZones_enableRepair(%obj);
 
	if(%inZone)
	{
		//echo(" in zone");
        %this.onEnterMissionArea(%obj);

		%datablock = WhiteZoneLightImage;

		if(%inEnemyZone)
		{
			//echo(" in enemy zone");
			if(%ownTeamId == 1)
				%datablock = BlueZoneLightImage;
			else
				%datablock = RedZoneLightImage;
		}
		else if(%inOwnZone)
		{
			//echo(" in own zone");
			if(%ownTeamId == 1)
				%datablock = RedZoneLightImage;
			else
				%datablock = BlueZoneLightImage;
		}

		%obj.mountImage(%datablock, 3);
	}
	else
	{
		//echo(" not in zone");
        %this.onLeaveMissionArea(%obj);
		%obj.unmountImage(3); // remove light
	}
 
    if(%obj.client)
        messageClient(%obj.client, 'MsgCurrentZone', "", %zoneTeamId);
}

// called by script code...
function ShapeBaseData::onHitEnemy(%this, %obj, %enemy, %dmg, %actualdmg)
{
    %currTime = getSimTime();

    %healthTakeback = %actualdmg * 0.5;

    %newSrcDamage = %obj.getDamageLevel() - %healthTakeback;
    %obj.setDamageLevel(%newSrcDamage);
    if(%newSrcDamage < 0)
        %obj.setDamageBufferLevel(%obj.getDamageBufferLevel() - %newSrcDamage);

    %enemy.setTagged();
    %obj.setCurrTagged(%enemy);
    
    if(%enemy != %obj.lastHitEnemy || %currTime >= %obj.lastHitClearTime)
        %obj.discTargetDamageAmount = 0;
        
    %obj.discTargetDamageAmount += %dmg;

    if(%obj.discTargetDamageAmount >= 60)
    {
        %obj.setDiscTarget(%enemy);
        %obj.discTargetDamageAmount = 0;
    }

    %locked = %obj.getCurrTarget() == %enemy;
    %obj.setInflictedDamageSoundPitch(%obj.getDamagePercent(), %locked);
    
    %obj.lastHitEnemy = %enemy;
    %obj.lastHitClearTime = %currTime + 1000;
}

