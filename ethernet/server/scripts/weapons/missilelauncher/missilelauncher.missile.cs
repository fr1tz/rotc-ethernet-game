//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - missilelauncher.missile.cs
// Code for the missile launcher missile
//------------------------------------------------------------------------------

exec("./missilelauncher.missile.gfx.red.cs");
exec("./missilelauncher.missile.gfx.blue.cs");

//------------------------------------------------------------------------------
// red projectile...

datablock ProjectileData(RedMissileLauncherMissile)
{
	// script damage properties...
	impactDamage       = 0;
	impactImpulse      = 0;
	splashDamage       = 150;
	splashDamageRadius = 15;
	splashImpulse      = 2500;
	
	// simulate the projectile's entire life in one tick?
	hyperSpeed = false;
	
	energyDrain = 0; // how much energy does firing this projectile drain?

	sound = MissileLauncherMissileSound;
	
	// target-tracking properties...
	maxTrackingAbility = 200;
	trackingAgility = 50;

	explodesNearEnemies		 = false;
	explodesNearEnemiesRadius = 10;
 
    projectileShapeName = "~/data/weapons/missilelauncher/missile/shape.dts";

	explosion				 = RedMissileLauncherMissileExplosion;
	//bounceExplosion		 = MissileLauncherRifleBounce;
	//hitEnemyExplosion	  = RedMissileLauncherHitEffect;
	//nearEnemyExplosion	 = RedMissileLauncherNearEnemyExplosion;
	//hitTeammateExplosion  = RedMissileLauncherHitEffect;
	//hitDeflectorExplosion = RedMissileLauncherDeflectedEffect;
	
	missEnemyEffect = RedMissileLauncherNearEnemyExplosion;
	
    laserTail	 = RedMissileLauncherMissileTail;
    laserTailLen = 5;
	//laserTrail[0] = RedMissileLauncherMissileTrail;

	particleEmitter = RedMissileLauncherMissileEmitter;

	muzzleVelocity   = 300;
	velInheritFactor = 0.0;

	armingDelay = 0;
	lifetime    = 3000;
	fadeDelay   = 3000;
	
	decals[0] = ExplosionDecalOne;
 
	hasLight    = true;
	lightRadius = 15.0;
	lightColor  = "1.0 0.0 1.0";
};

function RedMissileLauncherMissile::onRemove(%this, %obj)
{
	if(!%obj.hasExploded)
	{
		%p = new Projectile() {
			dataBlock		  = RedSlowMissileLauncherMissile;
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

function RedMissileLauncherMissile::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
	%obj.hasExploded = true;
	Parent::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);
}

//------------------------------------------------------------------------------
// red slow projectile...

datablock ProjectileData(RedSlowMissileLauncherMissile : RedMissileLauncherMissile)
{
	hyperSpeed = false;
	//projectileShapeName = "~/data/weapons/missilelauncher/projectile/shape.dts";
	missEnemyEffect = 0;
	particleEmitter = RedSlowMissileLauncherMissileEmitter;
    laserTail = 0;
	isBallistic = true;
	gravityMod = 7.5;
	lifetime = 10000;
	hasLight = false;
};


//------------------------------------------------------------------------------
// blue projectile...

datablock ProjectileData(BlueMissileLauncherMissile : RedMissileLauncherMissile)
{
    //projectileShapeName = "~/data/weapons/blaster/projectile.blue.dts";
	explosion = BlueMissileLauncherMissileExplosion;
	missEnemyEffect = BlueMissileLauncherNearEnemyExplosion;
	laserTail = BlueMissileLauncherMissileTail;
	particleEmitter = BlueMissileLauncherMissileEmitter;
};

function BlueMissileLauncherMissile::onRemove(%this, %obj)
{
	if(!%obj.hasExploded)
	{
		%p = new Projectile() {
			dataBlock		  = BlueSlowMissileLauncherMissile;
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

function BlueMissileLauncherMissile::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
	%obj.hasExploded = true;
	Parent::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);
}

//------------------------------------------------------------------------------
// blue slow projectile...

datablock ProjectileData(BlueSlowMissileLauncherMissile : BlueMissileLauncherMissile)
{
	hyperSpeed = false;
	//projectileShapeName = "~/data/weapons/missilelauncher/projectile/shape.dts";
	missEnemyEffect = 0;
	particleEmitter = BlueSlowMissileLauncherMissileEmitter;
	isBallistic = true;
	gravityMod = 10.0;
	lifetime = 10000;
};


