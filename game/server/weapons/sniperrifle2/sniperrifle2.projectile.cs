//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./sniperrifle2.projectile.gfx.red.cs");
exec("./sniperrifle2.projectile.gfx.blue.cs");


//-----------------------------------------------------------------------------
// projectile datablock...

datablock ShotgunProjectileData(RedSniperRifle2Projectile)
{
	stat = "sniper2";

	// script damage properties...
	impactDamage       = 30;
	impactImpulse      = 250;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = false;

	energyDrain = 20; // how much energy does firing this projectile drain?

	numBullets = 1; // number of shotgun bullets
	range = 2000; // shotgun range
	muzzleSpreadRadius = 0.0;
	referenceSpreadRadius = 0.0;
	referenceSpreadDistance = 50.0;

	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 4;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

    //sound = SniperRifle2ProjectileFlybySound;

    //projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion               = RedSniperRifle2ProjectileExplosion;
	hitEnemyExplosion       = RedSniperRifle2ProjectileHit;
    hitTeammateExplosion    = RedSniperRifle2ProjectileHit;
    //nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
    //hitDeflectorExplosion = SeekerDiscBounceEffect;

    //fxLight					= RedSniperRifle2ProjectileFxLight;

	missEnemyEffect		 = RedSniperRifle2ProjectileMissedEnemyEffect;

	fireExplosion = RedSniperRifle2ProjectileFireExplosion; // script field

    //laserTail	 = RedSniperRifle2ProjectileLaserTail;
    //laserTailLen = 10.0;

	//laserTrail[0] = RedSniperRifle2ProjectileLaserTrailOne;
	laserTrail[1] = RedSniperRifle2ProjectileLaserTrailHit;
	laserTrail[2] = RedSniperRifle2ProjectileLaserTrail;

	//particleEmitter = RedSniperRifle2ProjectileEmitter;

	muzzleVelocity   = 9999;
	velInheritFactor = 0.0;

	isBallistic			= false;
	gravityMod			 = 10.0;

	armingDelay			= 1000*0;
	lifetime				= 5000;
	fadeDelay			  = 5000;

	//decals[0] = BulletHoleDecalOne;
	decals[0] = SmashDecalOne;
	decals[1] = SmashDecalTwo;
	decals[2] = SmashDecalThree;
	decals[3] = SmashDecalFour;

	hasLight    = false;
	lightRadius = 10.0;
	lightColor  = "0.0 1.0 0.0";
};

function RedSniperRifle2Projectile::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);
	%vel = %obj.initialVelocity;
	%pos = %obj.initialPosition;
	%pos = VectorAdd(VectorScale(VectorNormalize(%vel),4), %pos);
	%norm = "0 0 1";
	createExplosion(%this.fireExplosion, %pos, %norm);
}

function RedSniperRifle2Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
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

datablock ShotgunProjectileData(BlueSniperRifle2Projectile : RedSniperRifle2Projectile)
{
    //projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";

	explosion               = BlueSniperRifle2ProjectileExplosion;
	hitEnemyExplosion       = BlueSniperRifle2ProjectileHit;
    hitTeammateExplosion    = BlueSniperRifle2ProjectileHit;

	missEnemyEffect    = BlueSniperRifle2ProjectileMissedEnemyEffect;

	//laserTail          = BlueSniperRifle2ProjectileLaserTail;

	fireExplosion = BlueSniperRifle2ProjectileFireExplosion; // script field

	//laserTrail[0] = BlueSniperRifle2ProjectileLaserTrailMissed;
	laserTrail[1] = BlueSniperRifle2ProjectileLaserTrailHit;
	laserTrail[2] = BlueSniperRifle2ProjectileLaserTrail;

	lightColor  = "1.0 0.5 0.0";
};

function BlueSniperRifle2Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedSniperRifle2Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function BlueSniperRifle2Projectile::onAdd(%this, %obj)
{
	RedSniperRifle2Projectile::onAdd(%this, %obj);
}
