//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
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

function ShapeBase::impulse(%this, %position, %impulseVec, %src)
{
	// All impulses applied by one object to another should go through this method. 
	return %this.getDataBlock().impulse(%this, %position, %impulseVec, %src);
}

//-----------------------------------------------------------------------------

function ShapeBase::setDamageDt(%this, %damageAmount, %damageType)
{
	// This function is used to apply damage over time.  The damage
	// is applied at a fixed rate (50 ms).  Damage could be applied
	// over time using the built in ShapBase C++ repair functions
	// (using a neg. repair), but this has the advantage of going
	// through the normal script channels.
	if( %this.damageSchedule !$= "" )
		cancel(%this.damageSchedule);
	%this.damageSchedule = %this.schedule(0, "applyDamageDt", %damageAmount, %damageType);
}

function ShapeBase::applyDamageDt(%this, %damageAmount, %damageType)
{
	if( %this.damageSchedule !$= "" )
		cancel(%this.damageSchedule);
	if(%this.getDamageState() $= "Enabled")
		%this.damage(0, "0 0 0", %damageAmount, %damageType);
	%this.damageSchedule = %this.schedule(50, "applyDamageDt", %damageAmount, %damageType);
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

function ShapeBase::checkTaggedThread(%this)
{
	cancel(%this.checkTaggedThread);
	%this.checkTaggedThread = %this.schedule(500,"checkTaggedThread");

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
    if(%this == %target)
        return;
        
    if(!%target.isCAT)
        return;

    if(%target.getTeamId() == %this.getTeamId())
        return;

    cancel(%this.discTargetThread);
	%pos = %target.getWorldBoxCenter();
    %this.setCurrTarget(%target, %pos);
    %this.inflictedDamageSoundLocked = true;
    
    %this.discTargetThread = %this.schedule(2000, "clearDiscTarget");
}

function ShapeBase::clearDiscTarget(%this)
{
    cancel(%this.discTargetThread);
    %this.setCurrTarget(0, "0 0 0");
}

//-----------------------------------------------------------------------------

function ShapeBase::setInflictedDamageSoundPitch(%this, %pitch)
{
    %this.inflictedDamageSoundPitch = %pitch;

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
    %this.inflictedDamageSoundLocked = false;
}

//-----------------------------------------------------------------------------

function ShapeBase::setShieldLevel(%this, %val)
{
	%this.setDamageBufferLevel(%val);
	%this.getDataBlock().updateShieldFx(%this);
}

//-----------------------------------------------------------------------------

function ShapeBase::hasBarrier(%this)
{
	return false;
}

//-----------------------------------------------------------------------------
// ShapeBase datablock
//-----------------------------------------------------------------------------

// *** callback function: called by engine
function ShapeBaseData::onAdd(%this,%obj)
{
	%this.updateShapeName(%obj);
	
	// Default dynamic armor stats...
	%obj.setDamageBufferRechargeRate(%this.damageBufferRechargeRate);
	%obj.setDamageBufferDischargeRate(%this.damageBufferDischargeRate);
	%obj.setEnergyRechargeRate(%this.energyRechargeRate);
	%obj.setRepairRate(0);

	// Barrier...
	%obj.barrier = 0;

	// Start threads...
	%obj.checkTaggedThread();
}

// *** callback function: called by engine
function ShapeBaseData::onRemove(%this, %obj)
{
	%obj.beingRemoved = true;

	if(%obj.damageSchedule !$= "")
		cancel(%obj.damageSchedule);
	
	// relieve obj from simple control if needed...
	if(%obj.isUnderSimpleControl())
		%obj.removeFromSimpleControl();
	
	// if obj was created by a spawn, inform it so it can spawn a new one
	if(isObject(%obj.spawn))
		%obj.spawn.getDataBlock().respawn(%obj.spawn);
		
	if(isObject(%obj.ssc))
		%obj.ssc.delete();
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
	%dmgstor = %damage;
	
	if(%damageType == $DamageType::Force)
		%n = "Force";
	else if(isObject(%sourceObject) && isObject(%sourceObject.getDataBlock()))
		%n = %sourceObject.getDataBlock().stat;  
	else
		%n = "Other";

	// get the real source object
	%realSourceObject = 0;
	if(isObject(%sourceObject))
	{
		if(%sourceObject.getType() & $TypeMasks::ProjectileObjectType)
			%realSourceObject = %sourceObject.getSourceObject();
		else if(%sourceObject.getType() & $TypeMasks::ShapeBaseObjectType)
			%realSourceObject = %sourceObject.client.player;
	}
	
	// reduce damage based on energy level...
	if(%obj.client.hasDamper)
	{
		%energyScale = %obj.getEnergyLevel() / %obj.getDataBlock().maxEnergy;
		%damage -= %damage * %energyScale * 0.50;	
	}

	// Handicap, we do damage dampening depending on the handicap value of the object
	if (isObject(%obj.client))
		%dstHand = %obj.client.handicap;
    else
        %dstHand = 1;

	if (isObject(%realSourceObject) && isObject(%realSourceObject.client))
		%srcHand = %realSourceObject.client.handicap;
    else
        %srcHand = 1;

	// The default handicap is 1, and it can only be in [0,1]
	if (! (0 <= %dstHand && %dstHand <= 1)) %dstHand = 1;
	if (! (0 <= %srcHand && %srcHand <= 1)) %srcHand = 1;

	// If the source handicap is lower than the target handicap, we lower the damage
	// If the difference is 0.5, we lower the damage by 50%. If it's 0.1, we lower it by 10%.
	if (%dstHand > %srcHand)
		%damage = %damage * (1 - (%dstHand - %srcHand));
	// If the the source handicap is higher, we leave the damage as it is, so dealing damage
	// feels like normal when playing against a better player.

   %bypassDamageBuffer = false;
   if(isObject(%sourceObject) && %sourceObject.getDataBlock().bypassDamageBuffer)
      %bypassDamageBuffer = true;
   if(%damageType == $DamageType::BOUNCE)
      %bypassDamageBuffer = true;
	if(%bypassDamageBuffer)
	{
		if(%obj.hasBarrier())
		{
			%healthDamageDealt = 0;
			%bufDamageDealt = 0;
		}
		else
		{
			%maxDamage = %obj.getDataBlock().maxDamage;
			%dmgLevel = %obj.getDamageLevel();
			if(%dmgLevel + %damage > %maxDamage)
				%damage = %maxDamage - %dmgLevel;
			%obj.setDamageLevel(%dmgLevel + %damage);
         if(%damageType == $DamageType::BOUNCE)
            %barriertime = 1.0;
         else
            %barriertime = %sourceObject.getDataBlock().impactDamage/10;
			%obj.activateBarrier(%barriertime);
			%healthDamageDealt = %damage;
			%bufDamageDealt = 0;
		}
	}
	else
	{
		%damageBufStore = %obj.getDamageBufferLevel();
		%healthDamageDealt = %obj.applyDamage(%damage);
		%bufDamageDealt = %damageBufStore - %obj.getDamageBufferLevel();
		%this.updateShieldFx(%obj);		
	}
	
	if(%realSourceObject != 0
	&& %realSourceObject.teamId != %obj.teamId
	&& %realSourceObject.getDamageState() $= "Enabled")
	{
		%stor = %realSourceObject.getDamageLevel();
		
        %this.onHitEnemy(
            %realSourceObject,
            %obj,
            %healthDamageDealt,
            %bufDamageDealt
        );

    	if(%realSourceObject.client)
  		{	
			%healthRestored = %stor - %realSourceObject.getDamageLevel();
			%a = %realSourceObject.client.stats.healthRegained;
			arrayChangeElement(%a, "All", arrayGetValue(%a, "All") + %healthRestored);
			arrayChangeElement(%a, %n, arrayGetValue(%a, %n) + %healthRestored);
		}
	}
	
	// eyecandy: ain't got time to bleed?...
   if(%bypassDamageBuffer && %healthDamageDealt == 0)
   {
      // no bleed
   }
	else
   {
      %bleed = %this.getBleed(%obj, %healthDamageDealt, %sourceObject);
      if(isObject(%bleed))
      {
         %norm = VectorNormalize(VectorSub(%pos, %obj.getWorldBoxCenter()));
         %bpos = %damageType == $DamageType::Impact ? %pos : %obj.getWorldBoxCenter();
         createExplosion(%bleed, %bpos, %norm);
      }
   }

    if(%obj.client)
    {
		%a = %obj.client.stats.dmgReceivedApplied;
		arrayChangeElement(%a, "All", arrayGetValue(%a, "All") + %dmgstor);
		arrayChangeElement(%a, %n, arrayGetValue(%a, %n) + %dmgstor);
		%a =%obj.client.stats.dmgReceivedCaused;
		arrayChangeElement(%a, "All", arrayGetValue(%a, "All") + %healthDamageDealt+%bufDamageDealt);
		arrayChangeElement(%a, %n, arrayGetValue(%a, %n) + %healthDamageDealt+%bufDamageDealt);
		%a =%obj.client.stats.healthLost;
		arrayChangeElement(%a, "All", arrayGetValue(%a, "All") + %healthDamageDealt);
		arrayChangeElement(%a, %n, arrayGetValue(%a, %n) + %healthDamageDealt);
    }
    if(%realSourceObject.client)
    {
		%a = %realSourceObject.client.stats.dmgDealtApplied;
		arrayChangeElement(%a, "All", arrayGetValue(%a, "All") + %dmgstor);
		arrayChangeElement(%a, %n, arrayGetValue(%a, %n) + %dmgstor);
		%a = %realSourceObject.client.stats.dmgDealtCaused;
		arrayChangeElement(%a, "All", arrayGetValue(%a, "All") + %healthDamageDealt+%bufDamageDealt);
		arrayChangeElement(%a, %n, arrayGetValue(%a, %n) + %healthDamageDealt+%bufDamageDealt);
    }

	return %healthDamageDealt;
}

// called by ShapeBase::impulse()
function ShapeBaseData::impulse(%this, %obj, %position, %impulseVec, %src)
{
	if(isObject(%src) && %src.getDataBlock().bypassDamageBuffer)
		if(%obj.hasBarrier())
			return;

	%impulseVec = VectorScale(%impulseVec, 1-0.75*%obj.gridConnection);
	%obj.applyImpulse(%position, %impulseVec);
}

// script function called by territory zone code
function ShapeBaseData::updateZone(%this, %obj, %enterZone, %leftZone)
{
	%ownTeamId = %obj.getTeamId();

	%inZone = false;
	%inOwnZone = false;
	%inEnemyZone = false;
    %zoneTeamId = -1;
    
	%obj.zCurrentZone = -1;    
    
	%pos = %obj.getPosition();
	InitContainerRadiusSearch(%pos, 0.0001, $TypeMasks::TacticalZoneObjectType);
	while( (%srchObj = %enterZone) != 0 || (%srchObj = containerSearchNext()) != 0)
	{
		%inSrchZone = false;
		if(%srchObj == %enterZone)
		{
			%inSrchZone = true;
		}
		else if(%srchObj != %leftZone)
		{
			// object actually in this zone?
			for(%i = 0; %i < %srchObj.getNumObjects(); %i++)
			{
				if(%srchObj.getObject(%i) == %obj)
				{
					%inSrchZone = true;
					break;
				}
			}
		}

		%enterZone = 0;	

		if(!%inSrchZone)
			continue;
	
		%inZone = true;
		%obj.zCurrentZone = %srchObj;		

		%zoneTeamId = %srchObj.getTeamId();

		if(%zoneTeamId != %ownTeamId && %zoneTeamId != 0)
		{
			%inEnemyZone = true;
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
	}
	else
	{
		//echo(" not in zone");
        %this.onLeaveMissionArea(%obj);  
   }

	// save these...
	%obj.zInZone = %inZone;
	%obj.zInOwnZone = %inOwnZone;
	%obj.zInEnemyZone = %inEnemyZone;
}

// called by script code...
function ShapeBaseData::onHitEnemy(%this, %obj, %enemy, %healthDmg, %bufDmg)
{
	%client = %obj.client;
	if(!isObject(%client))
		return;

   if(%healthDmg <= 0 && %bufDmg <= 0)
      return;

    // health takeback...
	if(%client.numVAMPs > 0)
	{
		%healthTakeback = %healthDmg * 0.5 * %client.numVAMPs;
		%newSrcDamage = %obj.getDamageLevel() - %healthTakeback;
		%obj.setDamageLevel(%newSrcDamage);
		if(%newSrcDamage < 0)
			%obj.setShieldLevel(%obj.getDamageBufferLevel() - %newSrcDamage);
	}

    // tagging...
    %enemy.setTagged();
    %obj.setCurrTagged(%enemy);
    
    %obj.incGrenadeAmmo((%healthDmg+%bufDmg)/250);

    %obj.setInflictedDamageSoundPitch(%enemy.getDamagePercent());

	$Music::LastHitTime = getSimTime();
	serverUpdateMusic(); // might have to change music immediately
}

// called by script code...
function ShapeBaseData::updateShapeName(%this, %obj)
{
	%client = %obj.client;

	if(%this.name)
	{
		%obj.setShapeName(%this.name);
	}
	else if(%client)
	{
		%handicap = %client.handicap;
		if(%handicap == 1)
			%handicap = "1.0";
		%name = getTaggedString(%client.name) @ "-" @ %handicap;
		%obj.setShapeName(%name);
		%obj.getHudInfo().markAsControlled(%obj.client, 0);
	}
}
