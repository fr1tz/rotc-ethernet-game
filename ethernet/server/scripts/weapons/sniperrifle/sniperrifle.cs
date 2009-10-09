//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - sniperrifle.cs
// Code for the sniper rifle
//------------------------------------------------------------------------------

exec("./sniperrifle.sfx.cs");
exec("./sniperrifle.gfx.red.cs");
exec("./sniperrifle.gfx.blue.cs");

//------------------------------------------------------------------------------
// red projectile...

datablock ProjectileData(RedSniperProjectile)
{
	// script damage properties...
	impactDamage        = 80;
	impactImpulse       = 2000;
	splashDamage        = 0;
	splashDamageRadius  = 0;
	splashImpulse       = 0;
	
	// simulate the projectile's entire life in one tick?
	hyperSpeed = false;
	
	energyDrain = 40; // how much energy does firing this projectile drain?

	//sound = SniperFlyBySound;
	
	// target-tracking properties...
	maxTrackingAbility = 200;
	trackingAgility = 50;

	explodesNearEnemies		 = false;
	explodesNearEnemiesRadius = 10;
 
    projectileShapeName = "~/data/weapons/blaster/projectile.red.dts";

	explosion				 = RedSniperProjectileExplosion;
	//bounceExplosion		 = SniperRifleBounce;
	//hitEnemyExplosion	  = RedSniperHitEffect;
	//nearEnemyExplosion	 = RedSniperNearEnemyExplosion;
	//hitTeammateExplosion  = RedSniperHitEffect;
	//hitDeflectorExplosion = RedSniperDeflectedEffect;
	
	missEnemyEffect = RedSniperNearEnemyExplosion;
	
    laserTail	 = RedSniperProjectileTail;
    laserTailLen = 5;
	//laserTrail[0] = RedSniperProjectileTrail;

	particleEmitter = RedSniperProjectileEmitter;

	muzzleVelocity   = 400;
	velInheritFactor = 0.0;

	armingDelay = 0;
	lifetime    = 3000;
	fadeDelay   = 0;
	
	decals[0] = ExplosionDecalOne;
};

function RedSniperProjectile::onRemove(%this, %obj)
{
	if(!%obj.hasExploded)
	{
		%p = new Projectile() {
			dataBlock		  = RedSlowSniperProjectile;
			initialVelocity  = VectorScale(%obj.getVelocity(), 0.5);
			initialPosition  = %obj.getPosition();
			sourceObject	  = %obj.sourceObject;
			sourceSlot		 = %obj.sourceSlot;
			client			  = %obj.client;
		};
		MissionCleanup.add(%p);
	}

	Parent::onRemove(%this,%obj);
}

function RedSniperProjectile::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
	%obj.hasExploded = true;
	Parent::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);
}

//------------------------------------------------------------------------------
// red slow projectile...

datablock ProjectileData(RedSlowSniperProjectile : RedSniperProjectile)
{
	hyperSpeed = false;
	//projectileShapeName = "~/data/weapons/sniperrifle/projectile/shape.dts";
	missEnemyEffect = 0;
	particleEmitter = RedSlowSniperProjectileEmitter;
	isBallistic	  = true;
	gravityMod		= 10.0;
	lifetime		  = 10000;
};


//------------------------------------------------------------------------------
// blue projectile...

datablock ProjectileData(BlueSniperProjectile : RedSniperProjectile)
{
    projectileShapeName = "~/data/weapons/blaster/projectile.blue.dts";
	explosion = BlueSniperProjectileExplosion;
	missEnemyEffect = BlueSniperNearEnemyExplosion;
	laserTail = BlueSniperProjectileTail;
	particleEmitter = BlueSniperProjectileEmitter;
};

function BlueSniperProjectile::onRemove(%this, %obj)
{
	if(!%obj.hasExploded)
	{
		%p = new Projectile() {
			dataBlock		  = BlueSlowSniperProjectile;
			initialVelocity  = VectorScale(%obj.getVelocity(), 0.5);
			initialPosition  = %obj.getPosition();
			sourceObject	  = %obj.sourceObject;
			sourceSlot		 = %obj.sourceSlot;
			client			  = %obj.client;
		};
		MissionCleanup.add(%p);
	}

	Parent::onRemove(%this,%obj);
}

function BlueSniperProjectile::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
	%obj.hasExploded = true;
	Parent::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);
}

//------------------------------------------------------------------------------
// blue slow projectile...

datablock ProjectileData(BlueSlowSniperProjectile : BlueSniperProjectile)
{
	hyperSpeed = false;
	//projectileShapeName = "~/data/weapons/sniperrifle/projectile/shape.dts";
	missEnemyEffect = 0;
	particleEmitter = BlueSlowSniperProjectileEmitter;
	isBallistic = true;
	gravityMod = 10.0;
	lifetime = 10000;
};

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(HolsteredSniperRifleImage)
{
	// basic item properties
	shapeFile = "~/data/weapons/sniperrifle/image_holstered.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 2;
	offset = "0 0 0";

	stateName[0] = "DoNothing";
};

//------------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedSniperRifleImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;

	// basic item properties
	shapeFile = "~/data/weapons/sniperrifle/image.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset      = "0 0 0";
	rotation    = "0 0 0";
	eyeOffset   = "0.25 -0.2 -0.2";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	// Use energy for ammo?
	usesEnergy = true;
	minEnergy = 40;
	
	projectile = RedSniperProjectile;
	
	// targeting...
	targetingMask	 = $TypeMasks::ShapeBaseObjectType;
	targetingMaxDist = 10000;

	// script fields...
	iconId = 9;
	mainWeapon = true;
	armThread = "holdrifle";  // armThread to use when holding this weapon
	crosshair = "sniperrifle"; // crosshair to display when holding this weapon

	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Activate";
		stateTransitionOnNoAmmo[0]       = "NoAmmo";
  		stateSequence[0]                 = "Activate";

		// when mounted...
		stateName[1]                     = "Activate";
		stateTransitionOnTimeout[1]      = "Ready";
		stateTimeoutValue[1]             = 0.5;
		stateSequence[1]                 = "Activate";

		// waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Target";
		stateArmThread[2]                = "holdrifle";
		//stateSequence[2]                 = "ready";

		// target...
		stateName[3]                      = "Target";
		stateTransitionOnTriggerUp[3]	  = "Ready";
		stateTransitionOnTargetAquired[3] = "Locked";
		stateTransitionOnNoAmmo[3]		  = "SelectAction";
		stateTransitionOnNotLoaded[3]	  = "Disabled";
		stateTarget[3]                    = true;
		stateEnergyDrain[3]               = 20;
		//stateSequence[3]                = "target";
		stateSound[3]                     = SniperRifleTargetSound;
		stateArmThread[3]                 = "aimrifle";

		// target locked...
		stateName[4]                        = "Locked";
		stateTransitionOnNoTarget[4]        = "Target";
		stateTransitionOnTriggerUp[4]       = "SelectAction";
		stateTransitionOnNoAmmo[4]          = "SelectAction";
		stateTransitionOnNotLoaded[4]       = "Disabled";
		stateTarget[4]                      = true;
		stateEnergyDrain[4]                 = 20;
		//stateSequence[4]                  = "locked";
		stateSound[4]                       = SniperRifleTargetAquiredSound;
  
        // select action...
		stateName[5]                        = "SelectAction";
		stateTransitionOnTimeout[5]         = "PerformAction";
		stateScript[5]                      = "selectAction";
		stateFire[5]                        = true;
  
        // perform action...
		stateName[6]                        = "PerformAction";
		stateTransitionOnAmmo[6]            = "Fire";
		stateTransitionOnNoAmmo[6]          = "PowerDown";

		// fire!
		stateName[7]                     = "Fire";
		stateTransitionOnTimeout[7]      = "Emitter1";
		stateTimeoutValue[7]             = 0.00;
		stateRecoil[7]                   = NoRecoil;
		stateAllowImageChange[7]         = false;
		stateEjectShell[7]               = true;
		stateSequence[7]                 = "fire";
		stateSound[7]                    = SniperRifleFireSound;
		stateScript[7]                   = "onFire";

		// eyecandy...
		stateName[8]                     = "Emitter1";
		stateTransitionOnTimeout[8]      = "Emitter2";
		stateTimeoutValue[8]             = 0.00;
		stateAllowImageChange[8]         = false;
		stateEmitter[8]                  = RedSniperRifleFireEmitter;
		stateEmitterNode[8]              = "emitter1";
		stateEmitterTime[8]              = 0.05;

		// eyecandy...
		stateName[9]                     = "Emitter2";
		stateTransitionOnTimeout[9]      = "Reload";
		stateTimeoutValue[9]             = 0.00;
		stateAllowImageChange[9]         = false;
		stateEmitter[9]                  = RedSniperRifleFireEmitter;
		stateEmitterNode[9]              = "emitter2";
		stateEmitterTime[9]              = 0.05;

		// reload...
		stateName[10]                    = "Reload";
		stateTransitionOnTimeout[10]     = "KeepAiming";
		stateTimeoutValue[10]            = 0.50;
		stateAllowImageChange[10]        = false;

		// keep aiming...
		stateName[11]                    = "KeepAiming";
		stateTimeoutValue[11]            = 2.00;
		stateWaitForTimeout[11]          = false;
		stateTransitionOnTriggerDown[11] = "Target";
		stateTransitionOnTimeout[11]     = "Ready";
		stateTransitionOnNoAmmo[11]      = "NoAmmo";

		// powerdown...
		stateName[12]                    = "PowerDown";
		stateTransitionOnFlag0Set[12]    = "NoAmmo";
		stateSequence[12]                = "Activate";
		stateSound[12]                   = SniperPowerDownSound;

		// no ammo...
		stateName[13]                    = "NoAmmo";
		stateTransitionOnAmmo[13]        = "Ready";
		//stateSequence[13]              = "NoAmmo";
		stateSequence[13]                = "Activate";

		// disabled...
		stateName[14]                    = "Disabled";
		stateTransitionOnLoaded[14]      = "Ready";
		stateAllowImageChange[14]        = false;
	//
	// ...end of image states
	//-------------------------------------------------
};

function RedSniperRifleImage::onUnmount(%this, %obj, %slot)
{
    Parent::onUnmount(%this, %obj, %slot);
    cancel(%obj.sniperPowerUpThread);
}

function RedSniperRifleImage::selectAction(%this, %obj, %slot)
{
    if(%obj.getImageAmmo(%slot) == false)
    {
        %obj.setImageFlag(%slot, 0, false);
        %obj.sniperPowerUpThread = %obj.schedule(1500, "setImageFlag", %slot, 0, true);
    }
}

function RedSniperRifleImage::onFire(%this, %obj, %slot)
{
	Parent::onFire(%this, %obj, %slot);
	
	%projectile = %this.projectile;

	// drain energy...
	%obj.setEnergyLevel( %obj.getEnergyLevel() - %projectile.energyDrain );

	// determine initial projectile velocity based on the
	// gun's muzzle point and the object's current velocity...
	%muzzleVector = %obj.getMuzzleVector(%slot);
	%objectVelocity = %obj.getVelocity();
	%muzzleVelocity = VectorAdd(
		VectorScale(%muzzleVector, %projectile.muzzleVelocity),
		VectorScale(%objectVelocity, %projectile.velInheritFactor));

	// determine muzzle-point...
	%muzzlePoint = %obj.getMuzzlePoint(%slot);

	// create the projectile object...
	%p = new Projectile() {
		dataBlock		  = %projectile;
        teamId            = %obj.teamId;
		initialVelocity  = %muzzleVelocity;
		initialPosition  = %muzzlePoint;
		sourceObject	  = %obj;
		sourceSlot		 = %slot;
		client			  = %obj.client;
	};
	MissionCleanup.add(%p);
	
	// set projectile target...
	%p.setTarget(%obj.getImageTarget(%slot));
	
	// clear out image target...
	%obj.setImageTarget(%slot, 0);

	return %p;
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueSniperRifleImage : RedSniperRifleImage)
{
	shapeFile = "~/data/weapons/sniperrifle/image.blue.dts";

	projectile = BlueSniperProjectile;

	stateEmitter[8] = BlueSniperRifleFireEmitter;
	stateEmitter[9] = BlueSniperRifleFireEmitter;
};

function BlueSniperRifleImage::onUnmount(%this, %obj, %slot)
{
	RedSniperRifleImage::onUnmount(%this, %obj, %slot);
}

function BlueSniperRifleImage::selectAction(%this, %obj, %slot)
{
	RedSniperRifleImage::selectAction(%this, %obj, %slot);
}

function BlueSniperRifleImage::onFire(%this, %obj, %slot)
{
	RedSniperRifleImage::onFire(%this, %obj, %slot);
}

