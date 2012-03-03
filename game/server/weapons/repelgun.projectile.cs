//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

// *** called from the repel gun image's "onFire" script function 
function spawnRepelGunProjectile(%datablock, %srcObj, %slot, %muzzlePoint, %muzzleVelocity)
{
	//error("spawnRepelGunProjectile();");

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

datablock ProjectileData(RedRepelGunProjectile)
{
	stat = "repelgun";

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

	sound = RepelGunProjectileSound;
 
	projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion             = RedRepelGunProjectileExplosion;
	bounceExplosion		 = RedRepelGunProjectileBounceExplosion;
//	hitEnemyExplosion     = RepelGunProjectileImpact;
//	nearEnemyExplosion    = RepelGunProjectileExplosion;
//	hitTeammateExplosion  = RepelGunProjectileImpact;
//	hitDeflectorExplosion = DiscDeflectedEffect;

//  particleEmitter	= RedRepelGunProjectileParticleEmitter;
	laserTrail[0]   = RedRepelGunProjectileLaserTrail;
//	laserTrail[1]   = RedRepelGunProjectileLaserTrail2;
//	laserTail	    = RedRepelGunProjectileLaserTail;
//	laserTailLen    = 2;

	muzzleVelocity		= 100;
	velInheritFactor	 = 1.0;
	
	isBallistic = true;
	gravityMod  = 10.0;
	bounceElasticity = 0.0;
	bounceFriction   = 0.0;

	armingDelay	= 1000*1;
	lifetime    = 1000*10;
	fadeDelay   = 5000;
	
	//decals[0]	= ExplosionDecalTwo;
	
	hasLight	 = true;
	lightRadius = 6.0;
	lightColor  = "1.0 0.0 0.0";
	
	// other script fields...
	mine = RedRepelGunMine;
};

function RedRepelGunProjectile::onBounce(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	Parent::onBounce(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function RedRepelGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	//Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	%p = new StaticShape() {
		dataBlock       = %this.mine;
		teamId          = %obj.teamId;
		client          = %obj.client;
	};

	%pos = VectorAdd(%pos, VectorScale(%normal,0.1));
	%rot = getWords(MatrixCreateFromEuler(%normal),3,6);
	%t = MatrixCreate(%pos, %rot);
	%p.setTransform(%t);
	%p.zNormal = %normal;
}

function RedRepelGunProjectile::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
	//Parent::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);
	
		
}

//--------------------------------------------------------------------------

datablock ProjectileData(BlueRepelGunProjectile : RedRepelGunProjectile)
{
	projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";
	explosion = BlueRepelGunProjectileExplosion;
	bounceExplosion = BlueRepelGunProjectileBounceExplosion;
	laserTrail[0]   = BlueRepelGunProjectileLaserTrail;
//	laserTrail[0]   = BlueRepelGunProjectileLaserTrail2;
	lightColor  = "0.0 0.0 1.0";
	mine = BlueRepelGunMine;	
};

function BlueRepelGunProjectile::onBounce(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedRepelGunProjectile::onBounce(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function BlueRepelGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedRepelGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function BlueRepelGunProjectile::onExplode(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedRepelGunProjectile::onExplode(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

