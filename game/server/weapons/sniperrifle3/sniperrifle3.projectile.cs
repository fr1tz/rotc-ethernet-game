//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./sniperrifle3.projectile.gfx.cs");
exec("./sniperrifle3.projectile.gfx.red.cs");
exec("./sniperrifle3.projectile.gfx.blue.cs");


//-----------------------------------------------------------------------------
// projectile datablock...

datablock ShotgunProjectileData(RedSniperRifle3Projectile)
{
	stat = "sniper";

	// script damage properties...
	impactDamage       = 120;
	impactImpulse      = 6000;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = false;

	energyDrain = 50; // how much energy does firing this projectile drain?

	numBullets = 1; // number of shotgun bullets
	range = 2000; // shotgun range
	muzzleSpreadRadius = 0.0;
	referenceSpreadRadius = 0.0;
	referenceSpreadDistance = 50.0;

	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 4;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

    //sound = SniperRifle3ProjectileFlybySound;

    //projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion               = RedSniperRifle3ProjectileExplosion;
	hitEnemyExplosion       = RedSniperRifle3ProjectileHit;
    hitTeammateExplosion    = RedSniperRifle3ProjectileHit;
    //nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
    //hitDeflectorExplosion = SeekerDiscBounceEffect;

    //fxLight					= RedSniperRifle3ProjectileFxLight;

	missEnemyEffect		 = RedSniperRifle3ProjectileMissedEnemyEffect;

	fireExplosion = RedSniperRifle3ProjectileFireExplosion; // script field

    //laserTail	 = RedSniperRifle3ProjectileLaserTail;
    //laserTailLen = 10.0;

	laserTrail[0] = RedSniperRifle3ProjectileLaserTrailOne;
	laserTrail[1] = RedSniperRifle3ProjectileLaserTrailOne;
	laserTrail[2] = RedSniperRifle3ProjectileLaserTrailThree;

	//particleEmitter = RedSniperRifle3ProjectileEmitter;

	muzzleVelocity   = 9999;
	velInheritFactor = 0.0;

	isBallistic			= false;
	gravityMod			 = 10.0;

	armingDelay			= 1000*0;
	lifetime				= 5000;
	fadeDelay			  = 5000;

	decals[0] = BulletHoleDecalTwo;

	hasLight    = false;
	lightRadius = 10.0;
	lightColor  = "0.0 1.0 0.0";
};

function RedSniperRifle3Projectile::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);
	%vel = %obj.initialVelocity;
	%pos = %obj.initialPosition;
	%pos = VectorAdd(VectorScale(VectorNormalize(%vel),4), %pos);
	%norm = "0 0 1";
	createExplosion(%this.fireExplosion, %pos, %norm);
}

function RedSniperRifle3Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
    
	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

    %src =  %obj.getSourceObject();
    if(!%src)
        return;
        
    %src.sniperTarget = %col;
}

//-----------------------------------------------------------------------------

datablock ShotgunProjectileData(BlueSniperRifle3Projectile : RedSniperRifle3Projectile)
{
    //projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";

	explosion               = BlueSniperRifle3ProjectileExplosion;
	hitEnemyExplosion       = BlueSniperRifle3ProjectileHit;
    hitTeammateExplosion    = BlueSniperRifle3ProjectileHit;

	missEnemyEffect    = BlueSniperRifle3ProjectileMissedEnemyEffect;

	//laserTail          = BlueSniperRifle3ProjectileLaserTail;

	fireExplosion = BlueSniperRifle3ProjectileFireExplosion; // script field

	laserTrail[0] = BlueSniperRifle3ProjectileLaserTrailOne;
	laserTrail[1] = BlueSniperRifle3ProjectileLaserTrailOne;
	laserTrail[2] = BlueSniperRifle3ProjectileLaserTrailThree;

	lightColor  = "1.0 0.5 0.0";
};

function BlueSniperRifle3Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedSniperRifle3Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function BlueSniperRifle3Projectile::onAdd(%this, %obj)
{
	RedSniperRifle3Projectile::onAdd(%this, %obj);
}
