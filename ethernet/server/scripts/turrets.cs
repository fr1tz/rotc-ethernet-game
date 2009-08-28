//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - turrets.cs
// Code for all turrets
//------------------------------------------------------------------------------

function executeTurretScripts()
{
	 // no turrets in ethernet
	 //echo(" ----- executing turret scripts ----- ");
}

executeTurretScripts();

//-----------------------------------------------------------------------------

function TurretData::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type.  For the mission editor
	%obj = new Turret() {
		dataBlock = %data;
	};
	%obj.mountable = true;

	return %obj;
}

function TurretData::onTrigger(%this, %obj, %triggerNum, %val)
{
	//--------------------------------------------------------------------------
	// third weapon...
	//--------------------------------------------------------------------------
	if( %triggerNum == 2 )
	{
		%obj.setImageTrigger(2,%val);
	}

	//--------------------------------------------------------------------------
	// tagging...
	//--------------------------------------------------------------------------
	if( %triggerNum == 3 )
	{
		%obj.isTagging = %val;
	}

}

function TurretData::onAdd(%this,%obj)
{
	Parent::onAdd(%this,%obj);

//	 echo("TurretData::onAdd()");
	%obj.setEnergyLevel(%this.maxEnergy);
	 // mount the barrel if one is specified
	 if (%this.barrel1 !$= "")
	 {
		 // echo("TurretData::onAdd() - Mounted Default Barrel on Turret");
		 %obj.mountImage(%this.barrel,%this.barrelSlot);
	 }

	// create base if a base is defined...
	if( %this.base !$= "" )
	{
		%transform = %obj.getTransform();

		%base = new WheeledVehicle() {
			dataBlock = %this.base;
		};
		%obj.base = %base;

		%base.mountObject(%obj,0);
		%base.setTransform(%transform);
	}

	// don't mount NoWeapon, just clear rosshair...
	//commandToClient(%obj.client,'setCrosshair',"crosshair_noweapon");
}

function TurretData::onRemove(%this,%obj)
{
	//Parent::onRemove(%this,%obj);
	echo("TurretData::onRemove");
}

function TurretData::onNewDatablock(%this,%obj)
{
	//Parent::onNewDatablock(%this,%obj);
	echo("TurretData::onNewDataBlock");
}

function TurretData::onDamage(%this,%obj,%amount)
{
	//called when damage level is changed
	//echo("TurretData::onDamage()");
	%damage = %obj.getDamageLevel();
	//

	%healthLeft = %this.maxDamage - %damage;
	//echo("TurretData::onDamage() - The Turret's health is " @ %healthLeft);
  //if (%damage >= %this.destroyedLevel)
  //GUI should show maxDamage - damageLevel
	 if(%damage >= %this.maxDamage)
	{
		if(%obj.getDamageState() !$= "Destroyed")
		{
			%obj.setDamageState(Destroyed);
		}
	}
	else
	{
		if(%obj.getDamageState() !$= "Enabled")
			%obj.setDamageState(Enabled);
	}
}


function TurretData::onDestroyed(%data, %obj, %prevState)
{
	%obj.schedule(2000, "delete");
	
	if(%obj.getControllingClient())
		%obj.getControllingClient().toggleFullControl(false);
}

function TurretData::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
{
	Parent::damage(%this, %obj, %sourceObject, %position, %damage, %damageType);

	%location = "Body";

	%client = %obj.client;
	%sourceClient = %sourceObject ? %sourceObject.client : 0;

	if( %obj.getDamageLevel() >= %this.maxDamage )
		%client.onDeath(%sourceObject, %sourceClient, %damageType, %location);
}

function TurretData::playerMounted(%data, %obj, %player, %node)
{
	echo("TurretData::playerMounted()");
}

function TurretData::playerDismounted(%data, %obj, %player, %node)
{
	echo("TurretData::playerDismounted()");

	%player.unmount();

	// we are dismounting so deactivate
	%obj.setActive(false);
}

//------------------------------------------------------------------------------

// phdana turrets ->

//-----------------------------------------------------------------------------
// Hook into the mission editor.

function AITurretData::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type.
	%obj = new AITurret() {
		dataBlock = %data;
	};
	return %obj;
}

// dummy datablock needed to create a trigger
// the values in this datablock are not used
datablock TriggerData(AITurretTriggerData)
{
	// values not used
	tickPeriodMS = 100;
};

function AITurretData::onAdd(%this,%obj)
{
	Parent::onAdd(%this,%obj);
	echo("AITurretData::onAdd");

	%size = %this.triggerRadius;
	%scale = VectorScale(%obj.scale,%size*2);
	%sizeVec = %size @" "@ %size @" "@ %size;
	%pos = VectorAdd(%obj.position,%sizeVec);

	// now we have to give the static shape a
	// controlling trigger of the right size and location
	%trigger = new Trigger()
	{
		dataBlock = AITurretTriggerData;
		position = %pos;
		rotation = %obj.rotation;
		scale = %scale;
		polyhedron = "0.000000 1.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
	};
	%trigger.setOwner(%obj);
	%obj.setTrigger(%trigger);
}

function AITurretData::onRemove(%this,%obj)
{
	// remove our trigger
	%trigger = %obj.getTrigger();
	if (isObject(%trigger))
	{
		%trigger.schedule(300,"delete");
		MissionCleanup.Add(%trigger);
	}
	Parent::onRemove(%this,%obj);
	echo("AITurretData::onRemove");
}

function AITurretData::onNewDatablock(%this,%obj)
{
	Parent::onNewDatablock(%this,%obj);
	echo("AITurretData::onNewDataBlock");
}

function AITurretData::onTargetDeleted(%this,%obj)
{
	// the aim object just got deleted
	//echo("AITurretData::onTargetDeleted()");
}

function AITurretData::onThink(%this,%obj)
{
	//echo("AITurretData::onThink()");
}

// trigger zone thinking...

function AITurretData::onEnterTrigger(%this,%turret,%obj)
{
	// if this is not the first object to enter do nothing
	if (%turret.getNumObjects() > 1)
		return;

	echo("AITurretData::onEnterTrigger(FirstObject!)");

	if (!%turret.isActivated())
	{
		echo("AITurretData::onEnterTrigger() - Activating...");
		%turret.setActive(true);
	}
}

function AITurretData::onTickTrigger(%this,%turret)
{
	// if we are not yet fully activated...then do nothing
	if (!%turret.isActivated())
		return;

	//echo("AITurretData::onTickTrigger() - activated ");

	// if we dont have a target...pick one
	if (!%turret.hasTarget())
	{
		// pick a target and setup for first firing
		%this.chooseTarget(%turret);
	}
	else
	{
		// the time has come to fire again!
		if (%turret.nextFireTime > 0 && $Sim::Time >= %turret.nextFireTime)
			%this.FireGun(%turret);
	}
}

function AITurretData::onTargetAquired(%this,%turret, %target)
{
	//echo("AITurretData::onTargetAquired()");
}

function AITurretData::onTargetChanged(%this,%turret, %oldTarget, %newTarget)
{
	//echo("AITurretData::onTargetChanged()");
}

function AITurretData::onTargetLost(%this,%turret, %target)
{
	//echo("AITurretData::onTargetLost()");

	// no more firing unless we find another target
	%turret.nextFireTime = 0;

	// if there are no more in zone
	if (%turret.getNumObjects() < 1)
		return;

	// try to pick another
	%this.chooseTarget(%turret);
}

function AITurretData::onLeaveTrigger(%this,%turret,%obj)
{
	// if this is not the last object to leave do nothing
	if (%turret.getNumObjects() > 0)
		return;

	echo("AITurretData::onLeaveTrigger(LastObject!)");

	// stop targeting if we are
	%turret.clearTarget();

	// stop firing
	%turret.nextFireTime = 0;

	// deactivate if we are not already
	if (%turret.isActivated())
		%turret.setActive(false);
}

function AITurretData::onTargetEnterLOS(%this,%turret)
{
	echo("AITurretData::onTargetEnterLOS");
}

function AITurretData::onTargetExitLOS(%this,%turret)
{
	echo("AITurretData::onTargetExitLOS");
}

// --------------------------------------------------------------------------
// SCRIPT only methods
//

// setup to fire again fireRate seconds later
function AITurretData::setupNextFire(%this,%turret,%rate,%variance)
{
	// setup next fire time
	%ranValue = (1000 - getRandom(2000)) / 1000;
	%offset = %ranValue * %variance;
	%delay =  %rate + %offset;
	%turret.nextFireTime = $Sim::Time + %delay;

	//echo("next firing at " @ %turret.nextFireTime);
}

// fire turret gun and setup to fire again fireRate seconds later
function AITurretData::fireGun(%this,%turret)
{
	// fire gun
	%turret.fire(0);

	//echo("FIRING NOW: at " @ $Sim::Time);

	// setup next fire time
	%this.setupNextFire(%turret,%this.fireRate,%this.fireRateVariance);
}

// pick a target and setup for first firing
function AITurretData::ChooseTarget(%this,%turret)
{
  if (%turret.pickTarget())
  {
	  // setup to fire the first shot after picking a target
	  %this.setupNextFire(%turret,%this.firstFireDelay,%this.firstFireDelayVariance);
  }
  else
  {
	  echo("failed to pick a target");
  }
}



