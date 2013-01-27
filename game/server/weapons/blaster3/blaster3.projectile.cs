//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./blaster3.projectile.sfx.cs");
exec("./blaster3.projectile.gfx.red.cs");
exec("./blaster3.projectile.gfx.blue.cs");

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ShotgunProjectileData(RedBlaster3Projectile)
{
	stat = "blaster";

	// script damage properties...
	impactDamage       = 10;
	impactImpulse      = 400;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;

	energyDrain = 3; // how much energy does firing this projectile drain?

	numBullets = 9; // number of shotgun bullets

	range = 500; // shotgun range
	muzzleSpreadRadius = 0.5;
	referenceSpreadRadius = 1.0;
	referenceSpreadDistance = 25;

	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 4;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

	//sound = Blaster3ProjectileFlybySound;

	//projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion               = RedBlaster3ProjectileImpact;
	hitEnemyExplosion       = RedBlaster3ProjectileHit;
	hitTeammateExplosion    = RedBlaster3ProjectileHit;
	//nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
	//hitDeflectorExplosion = SeekerDiscBounceEffect;

	//fxLight					= RedBlaster3ProjectileFxLight;

	missEnemyEffect		 = RedBlaster3ProjectileMissedEnemyEffect;

	//laserTail				 = RedBlaster3ProjectileLaserTail;
	//laserTailLen			 = 10.0;

	laserTrail[0]			= RedBlaster3ProjectileLaserTrail;
	laserTrail[1]			= RedBlaster3ProjectileLaserTrail;
	smoothLaserTrail = false;

	//particleEmitter	  = RedBlaster3ProjectileParticleEmitter;

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

function RedBlaster3Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

    %src =  %obj.getSourceObject();
    if(!%src)
        return;

    %currTime = getSimTime();

	// FIXME: strange linux version bug:
	//        after the game has been running a long time
	//        (%currTime == %obj.hitTime)
	//        often evaluates to false even if the
	//        values appear to be equal.
	//        (%currTime - %obj.hitTime) evaluates to 1
	//        in those cases.
    //if(%currTime == %obj.hitTime)
	if(%currTime - %obj.hitTime <= 1)
    {
        %col.numBlaster3BulletHits += 1;
        if(%col.numBlaster3BulletHits == 4)
            %src.setDiscTarget(%col);
    }
    else
    {
        %obj.hitTime = %currTime;
        %col.numBlaster3BulletHits = 1;
    }
}

//-----------------------------------------------------------------------------

datablock ShotgunProjectileData(BlueBlaster3Projectile : RedBlaster3Projectile)
{
	//projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";

	explosion            = BlueBlaster3ProjectileImpact;
	hitEnemyExplosion    = BlueBlaster3ProjectileHit;
	hitTeammateExplosion = BlueBlaster3ProjectileHit;

	missEnemyEffect    = BlueBlaster3ProjectileMissedEnemyEffect;

	//laserTail          = BlueBlaster3ProjectileLaserTail;

	laserTrail[0]      = BlueBlaster3ProjectileLaserTrail;
	laserTrail[1]      = BlueBlaster3ProjectileLaserTrail;

	lightColor  = "0.0 0.0 1.0";
};

function BlueBlaster3Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedBlaster3Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}
