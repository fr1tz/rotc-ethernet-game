//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./heavyminigun.projectile.sfx.cs");
exec("./heavyminigun.projectile.gfx.red.cs");
exec("./heavyminigun.projectile.gfx.blue.cs");

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ProjectileData(RedHeavyMinigunProjectile)
{
	stat = "minigun";

	// script damage properties...
	impactDamage       = 20;
	impactImpulse      = 500;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;

	energyDrain = 2; // how much energy does firing this projectile drain?

	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 10;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

	//sound = HeavyMinigunProjectileFlybySound;

	//projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion               = RedHeavyMinigunProjectileImpact;
	hitEnemyExplosion       = RedHeavyMinigunProjectileHit;
	hitTeammateExplosion    = RedHeavyMinigunProjectileHit;
	//nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
	//hitDeflectorExplosion = SeekerDiscBounceEffect;

	//fxLight					= RedHeavyMinigunProjectileFxLight;

	missEnemyEffect = RedHeavyMinigunProjectileMissedEnemyEffect;

	laserTail    = RedHeavyMinigunProjectileLaserTail;
	laserTailLen = 8.0;

	laserTrail[0] = RedHeavyMinigunProjectileLaserTrail;

	//particleEmitter	  = RedHeavyMinigunProjectileParticleEmitter;

	muzzleVelocity   = 400;
	velInheritFactor = 1.0;

	isBallistic = false;
	gravityMod  = 4.0;

	armingDelay	= 1000*0;
	lifetime	= 3000;
	fadeDelay	= 5000;

	decals[0] = BulletHoleDecalOne;

	hasLight    = false;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedHeavyMinigunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;
}

//-----------------------------------------------------------------------------

datablock ProjectileData(BlueHeavyMinigunProjectile : RedHeavyMinigunProjectile)
{
	//projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";

	explosion            = BlueHeavyMinigunProjectileImpact;
	hitEnemyExplosion    = BlueHeavyMinigunProjectileHit;
	hitTeammateExplosion = BlueHeavyMinigunProjectileHit;

	missEnemyEffect    = BlueHeavyMinigunProjectileMissedEnemyEffect;

	laserTail          = BlueHeavyMinigunProjectileLaserTail;

	laserTrail[0]      = BlueHeavyMinigunProjectileLaserTrail;

	lightColor  = "0.0 0.0 1.0";
};

function BlueHeavyMinigunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedHeavyMinigunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}
