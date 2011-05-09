//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

// *** called from the grenade launcher image's "onFire" script function 
function spawnGrenadeLauncherProjectile(%datablock, %srcObj, %slot, %muzzlePoint, %muzzleVelocity)
{
	error("spawnGrenadeLauncherProjectile();");

	%p = new Grenade() {
		dataBlock       = %datablock;
		teamId          = %srcObj.teamId;
		client          = %srcObj.client;
	};
	
	%p.setCollisionTimeout(%srcObj);
	%p.setTransform(%muzzlePoint);
	%p.applyImpulse(%muzzlePoint, %muzzleVelocity);
	
	%p.schedule(%datablock.lifetime, "delete");
	
	return %p;
}

//-----------------------------------------------------------------------------
// projectile datablock...

datablock GrenadeData(RedGrenadeLauncherProjectile)
{
	stat = "gl";

	shapeFile = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	detonateExplosion = RedGrenadeLauncherProjectileExplosion;
	bounceExplosion	= RedGrenadeLauncherProjectileBounceExplosion;

	laserTrail = RedGrenadeLauncherProjectileLaserTrail;

	friction   = 0;	
	elasticity = 0;
	sticky     = true;
	gravityMod  = 0.5; //10.0;
	
	lightType = "PulsingLight";
	lightColor = "1.0 0.0 0.0";
	lightTime = 250;
	lightRadius = 18;
	
	// script damage properties...
	impactDamage        = 0;
	impactImpulse       = 1000;
	splashDamage        = 40;
	splashDamageRadius  = 18;
	splashDamageFalloff = $SplashDamageFalloff::Linear;
	splashImpulse       = 0;
	
	// for GrenadeData these are just script fields...
	energyDrain = 8;	
	muzzleVelocity		= 100;
	velInheritFactor	 = 1.0;
	armingDelay	= 1000;
	lifetime    = 1000*10;
	decals[0]	= ExplosionDecalTwo;	
};

function RedGrenadeLauncherProjectile::onAdd(%this, %obj)
{
	%obj.playAudio(0, GrenadeLauncherProjectileSound);
}

function RedGrenadeLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------

datablock ProjectileData(BlueGrenadeLauncherProjectile : RedGrenadeLauncherProjectile)
{
	projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";
	explosion = BlueGrenadeLauncherProjectileExplosion;
	bounceExplosion = BlueGrenadeLauncherProjectileBounceExplosion;
	laserTrail[0]   = BlueGrenadeLauncherProjectileLaserTrail;
//	laserTrail[0]   = BlueGrenadeLauncherProjectileLaserTrail2;
	lightColor  = "0.0 0.0 1.0";
};

function BlueGrenadeLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedGrenadeLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

