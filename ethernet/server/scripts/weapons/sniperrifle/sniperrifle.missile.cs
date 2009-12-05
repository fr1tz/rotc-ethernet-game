//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - sniperrifle.missile.cs
// Code for the sniper rifle missile
//------------------------------------------------------------------------------

exec("./sniperrifle.missile.gfx.red.cs");
exec("./sniperrifle.missile.gfx.blue.cs");

//------------------------------------------------------------------------------
// red projectile...

datablock ProjectileData(RedSniperMissile)
{
	// script damage properties...
	impactDamage        = 100;
	impactImpulse       = 2000;
	splashDamage        = 0;
	splashDamageRadius  = 0;
	splashImpulse       = 0;
	
	// simulate the projectile's entire life in one tick?
	hyperSpeed = false;
	
	energyDrain = 0; // how much energy does firing this projectile drain?

	//sound = SniperFlyBySound;
	
	// target-tracking properties...
	maxTrackingAbility = 200;
	trackingAgility = 50;

	explodesNearEnemies		 = false;
	explodesNearEnemiesRadius = 10;
 
    projectileShapeName = "~/data/weapons/blaster/projectile.red.dts";

	explosion				 = RedSniperMissileExplosion;
	//bounceExplosion		 = SniperRifleBounce;
	//hitEnemyExplosion	  = RedSniperHitEffect;
	//nearEnemyExplosion	 = RedSniperNearEnemyExplosion;
	//hitTeammateExplosion  = RedSniperHitEffect;
	//hitDeflectorExplosion = RedSniperDeflectedEffect;
	
	missEnemyEffect = RedSniperNearEnemyExplosion;
	
    laserTail	 = RedSniperMissileTail;
    laserTailLen = 5;
	//laserTrail[0] = RedSniperMissileTrail;

	particleEmitter = RedSniperMissileEmitter;

	muzzleVelocity   = 400;
	velInheritFactor = 0.0;

	armingDelay = 0;
	lifetime    = 3000;
	fadeDelay   = 0;
	
	decals[0] = ExplosionDecalOne;
};

function RedSniperMissile::onRemove(%this, %obj)
{
	if(!%obj.hasExploded)
	{
		%p = new Projectile() {
			dataBlock		  = RedSlowSniperMissile;
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

function RedSniperMissile::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
	%obj.hasExploded = true;
	Parent::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);
}

//------------------------------------------------------------------------------
// red slow projectile...

datablock ProjectileData(RedSlowSniperMissile : RedSniperMissile)
{
	hyperSpeed = false;
	//projectileShapeName = "~/data/weapons/sniperrifle/projectile/shape.dts";
	missEnemyEffect = 0;
	particleEmitter = RedSlowSniperMissileEmitter;
	isBallistic	  = true;
	gravityMod		= 10.0;
	lifetime		  = 10000;
};


//------------------------------------------------------------------------------
// blue projectile...

datablock ProjectileData(BlueSniperMissile : RedSniperMissile)
{
    projectileShapeName = "~/data/weapons/blaster/projectile.blue.dts";
	explosion = BlueSniperMissileExplosion;
	missEnemyEffect = BlueSniperNearEnemyExplosion;
	laserTail = BlueSniperMissileTail;
	particleEmitter = BlueSniperMissileEmitter;
};

function BlueSniperMissile::onRemove(%this, %obj)
{
	if(!%obj.hasExploded)
	{
		%p = new Projectile() {
			dataBlock		  = BlueSlowSniperMissile;
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

function BlueSniperMissile::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
	%obj.hasExploded = true;
	Parent::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);
}

//------------------------------------------------------------------------------
// blue slow projectile...

datablock ProjectileData(BlueSlowSniperMissile : BlueSniperMissile)
{
	hyperSpeed = false;
	//projectileShapeName = "~/data/weapons/sniperrifle/projectile/shape.dts";
	missEnemyEffect = 0;
	particleEmitter = BlueSlowSniperMissileEmitter;
	isBallistic = true;
	gravityMod = 10.0;
	lifetime = 10000;
};


