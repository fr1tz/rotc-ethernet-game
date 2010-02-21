//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - blaster.projectile.cs
// Code for the blaster projectile
//------------------------------------------------------------------------------

exec("./blaster.projectile.sfx.cs");
exec("./blaster.projectile.gfx.red.cs");
exec("./blaster.projectile.gfx.blue.cs");

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ShotgunProjectileData(RedBlasterProjectile)
{
	// script damage properties...
	impactDamage       = 10;
	impactImpulse      = 150;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;

	energyDrain = 2; // how much energy does firing this projectile drain?

    numBullets = 9; // number of shotgun bullets

    range = 500; // shotgun range
    spread = 0.5; // shotgun spread in degrees

	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 4;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

	//sound = BlasterProjectileFlybySound;

	//projectileShapeName = "~/data/weapons/blaster/projectile.red.dts";

	explosion               = RedBlasterProjectileImpact;
	hitEnemyExplosion       = RedBlasterProjectileHit;
	hitTeammateExplosion    = RedBlasterProjectileHit;
	//nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
	//hitDeflectorExplosion = SeekerDiscBounceEffect;

	//fxLight					= RedBlasterProjectileFxLight;

	missEnemyEffect		 = RedBlasterProjectileMissedEnemyEffect;

	//laserTail				 = RedBlasterProjectileLaserTail;
	//laserTailLen			 = 10.0;

	laserTrail[0]			= RedBlasterProjectileLaserTrailMissed;
	laserTrail[1]			= RedBlasterProjectileLaserTrailHit;

	//particleEmitter	  = RedBlasterProjectileParticleEmitter;

	muzzleVelocity   = 9999;
	velInheritFactor = 0.0;

	isBallistic			= false;
	gravityMod			 = 10.0;

	armingDelay			= 1000*0;
	lifetime				= 3000;
	fadeDelay			  = 5000;

	decals[0] = BulletHoleDecalOne;

	hasLight    = false;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedBlasterProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

    %src =  %obj.getSourceObject();
    if(!%src)
        return;

    %currTime = getSimTime();

    if(%currTime == %obj.hitTime)
    {
        %col.numBlasterBulletHits += 1;
        if(%col.numBlasterBulletHits == 3)
            %src.setDiscTarget(%col);
    }
    else
    {
        %obj.hitTime = %currTime;
        %col.numBlasterBulletHits = 1;
    }
}

//-----------------------------------------------------------------------------

datablock ShotgunProjectileData(BlueBlasterProjectile : RedBlasterProjectile)
{
	//projectileShapeName = "~/data/weapons/blaster/projectile.blue.dts";

	explosion            = BlueBlasterProjectileImpact;
	hitEnemyExplosion    = BlueBlasterProjectileHit;
	hitTeammateExplosion = BlueBlasterProjectileHit;

	missEnemyEffect    = BlueBlasterProjectileMissedEnemyEffect;

	//laserTail          = BlueBlasterProjectileLaserTail;

	laserTrail[0]      = BlueBlasterProjectileLaserTrailMissed;
	laserTrail[1]      = BlueBlasterProjectileLaserTrailHit;

	lightColor  = "0.0 0.0 1.0";
};

function BlueBlasterProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedBlasterProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}
