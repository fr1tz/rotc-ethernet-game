//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./minigun.projectile.sfx.cs");
exec("./minigun.projectile.gfx.red.cs");
exec("./minigun.projectile.gfx.blue.cs");

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ProjectileData(RedMinigunProjectile)
{
	stat = "minigun";

	// script damage properties...
	impactDamage       = 20;
	impactImpulse      = 150;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;

	energyDrain = 2; // how much energy does firing this projectile drain?

	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 4;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

	//sound = MinigunProjectileFlybySound;

	//projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion               = RedMinigunProjectileImpact;
	hitEnemyExplosion       = RedMinigunProjectileHit;
	hitTeammateExplosion    = RedMinigunProjectileHit;
	//nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
	//hitDeflectorExplosion = SeekerDiscBounceEffect;

	//fxLight					= RedMinigunProjectileFxLight;

	missEnemyEffect = RedMinigunProjectileMissedEnemyEffect;

	laserTail    = RedMinigunProjectileLaserTail;
	laserTailLen = 8.0;

	laserTrail[0] = RedMinigunProjectileLaserTrail;

	//particleEmitter	  = RedMinigunProjectileParticleEmitter;

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

function RedMinigunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;
}

//-----------------------------------------------------------------------------

datablock ProjectileData(BlueMinigunProjectile : RedMinigunProjectile)
{
	//projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";

	explosion            = BlueMinigunProjectileImpact;
	hitEnemyExplosion    = BlueMinigunProjectileHit;
	hitTeammateExplosion = BlueMinigunProjectileHit;

	missEnemyEffect    = BlueMinigunProjectileMissedEnemyEffect;

	laserTail          = BlueMinigunProjectileLaserTail;

	laserTrail[0]      = BlueMinigunProjectileLaserTrail;

	lightColor  = "0.0 0.0 1.0";
};

function BlueMinigunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedMinigunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}
