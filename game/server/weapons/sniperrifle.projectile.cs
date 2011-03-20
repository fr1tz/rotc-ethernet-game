//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// sniperrifle.projectile.cs
// Code for the sniper rifle projectile
//------------------------------------------------------------------------------

exec("./sniperrifle.projectile.gfx.red.cs");
exec("./sniperrifle.projectile.gfx.orange.cs");
exec("./sniperrifle.projectile.gfx.blue.cs");
exec("./sniperrifle.projectile.gfx.cyan.cs");


//-----------------------------------------------------------------------------
// projectile datablock...

datablock ShotgunProjectileData(RedSniperProjectile)
{
	stat = "sniper";

	// script damage properties...
	impactDamage       = 60;
	impactImpulse      = 1000;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = true;	

	energyDrain = 30; // how much energy does firing this projectile drain?

	numBullets = 1; // number of shotgun bullets
	range = 2000; // shotgun range
	muzzleSpreadRadius = 0.0;
	referenceSpreadRadius = 0.0;
	referenceSpreadDistance = 50.0;

	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 4;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

    //sound = SniperProjectileFlybySound;

    //projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion               = OrangeSniperProjectileExplosion;
	hitEnemyExplosion       = OrangeSniperProjectileHit;
    hitTeammateExplosion    = OrangeSniperProjectileHit;
    //nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
    //hitDeflectorExplosion = SeekerDiscBounceEffect;

    //fxLight					= RedSniperProjectileFxLight;

	missEnemyEffect		 = OrangeSniperProjectileMissedEnemyEffect;

	fireExplosion = OrangeSniperProjectileFireExplosion; // script field

    //laserTail	 = RedSniperProjectileLaserTail;
    //laserTailLen = 10.0;

	//laserTrail[0] = OrangeSniperProjectileLaserTrailMissed;
	laserTrail[1] = OrangeSniperProjectileLaserTrailHit;
	laserTrail[2] = OrangeSniperProjectileLaserTrail; 

	//particleEmitter = OrangeSniperProjectileEmitter;

	muzzleVelocity   = 9999;
	velInheritFactor = 0.0;

	isBallistic			= false;
	gravityMod			 = 10.0;

	armingDelay			= 1000*0;
	lifetime				= 5000;
	fadeDelay			  = 5000;

	decals[0] = BulletHoleDecalOne;

	hasLight    = false;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedSniperProjectile::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);
	%vel = %obj.initialVelocity;
	%pos = %obj.initialPosition;
	%pos = VectorAdd(VectorScale(VectorNormalize(%vel),4), %pos);
	%norm = "0 0 1";
	createExplosionOnClients(%this.fireExplosion, %pos, %norm);
}

function RedSniperProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
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

datablock ShotgunProjectileData(BlueSniperProjectile : RedSniperProjectile)
{
    //projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";

	explosion            = CyanSniperProjectileExplosion;
	hitEnemyExplosion    = CyanSniperProjectileHit;
    hitTeammateExplosion = CyanSniperProjectileHit;

	missEnemyEffect    = CyanSniperProjectileMissedEnemyEffect;

	//laserTail          = CyanSniperProjectileLaserTail;

	fireExplosion = CyanSniperProjectileFireExplosion; // script field

	//laserTrail[0] = CyanSniperProjectileLaserTrailMissed;
	laserTrail[1] = CyanSniperProjectileLaserTrailHit;
	laserTrail[2] = CyanSniperProjectileLaserTrail;

	lightColor  = "0.0 0.0 1.0";
};

function BlueSniperProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedSniperProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function BlueSniperProjectile::onAdd(%this, %obj)
{
	RedSniperProjectile::onAdd(%this, %obj);
}
