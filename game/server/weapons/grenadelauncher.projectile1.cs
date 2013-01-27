//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

// *** called from the grenade launcher image's "onFire" script function 
function spawnGrenadeLauncherProjectile(%datablock, %srcObj, %slot, %muzzlePoint, %muzzleVelocity)
{
	//error("spawnGrenadeLauncherProjectile();");

	%p = new Projectile() {
		dataBlock       = %datablock;
		teamId          = %srcObj.teamId;
		initialVelocity = %muzzleVelocity;
		initialPosition = %muzzlePoint;
		sourceObject    = %srcObj;
		sourceSlot      = %slot;
		client          = %srcObj.client;
	};
	
	return %p;
}

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ProjectileData(RedGrenadeLauncherProjectile)
{
	stat = "gl";

	// script damage properties...
	impactDamage        = 0;
	impactImpulse       = 1000;
	splashDamage        = 40;
	splashDamageRadius  = 18;
	splashDamageFalloff = $SplashDamageFalloff::Linear;
	splashImpulse       = 0;
	
	// how much energy does firing this projectile drain?...
	energyDrain = 8;

	trackingAgility = 0;
	
	explodesNearEnemies        = false;
	explodesNearEnemiesRadius  = 10;
	explodesNearEnemiesMask    = $TypeMasks::PlayerObjectType;

	sound = GrenadeLauncherProjectileSound;
 
	projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion             = RedGrenadeLauncherProjectileExplosion;
	bounceExplosion		 = RedGrenadeLauncherProjectileBounceExplosion;
//	hitEnemyExplosion     = GrenadeLauncherProjectileImpact;
//	nearEnemyExplosion    = GrenadeLauncherProjectileExplosion;
//	hitTeammateExplosion  = GrenadeLauncherProjectileImpact;
//	hitDeflectorExplosion = DiscDeflectedEffect;

//  particleEmitter	= RedGrenadeLauncherProjectileParticleEmitter;
	laserTrail[0]   = RedGrenadeLauncherProjectileLaserTrail;
//	laserTrail[1]   = RedGrenadeLauncherProjectileLaserTrail2;
//	laserTail	    = RedGrenadeLauncherProjectileLaserTail;
//	laserTailLen    = 2;

	muzzleVelocity		= 100;
	velInheritFactor	 = 1.0;
	
	isBallistic = true;
	gravityMod  = 10.0;
	bounceElasticity = 0;
	bounceFriction   = 0;

	armingDelay	= 1000;
	lifetime    = 1000*10;
	fadeDelay   = 5000;
	
	decals[0]	= ExplosionDecalTwo;
	
	hasLight	 = true;
	lightRadius = 18.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedGrenadeLauncherProjectile::onBounce(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	Parent::onBounce(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if(!%obj.playedBounceSound)
	{
		serverPlay3D(GrenadeLauncherProjectileBounceSound, %pos);
		%obj.playedBounceSound = true;
	}	
}

function RedGrenadeLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	serverPlay3D(GrenadeLauncherProjectileBounceSound, %pos);
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

function BlueGrenadeLauncherProjectile::onBounce(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedGrenadeLauncherProjectile::onBounce(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function BlueGrenadeLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedGrenadeLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

