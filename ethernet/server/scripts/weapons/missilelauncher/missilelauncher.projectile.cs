//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// missilelauncher.projectile.cs
// Code for the missile launcher projectile
//------------------------------------------------------------------------------

exec("./missilelauncher.projectile.gfx.red.cs");
exec("./missilelauncher.projectile.gfx.blue.cs");

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ShotgunProjectileData(RedMissileLauncherProjectile)
{
	// script damage properties...
	impactDamage       = 50;
	impactImpulse      = 1000;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;

	energyDrain = 35; // how much energy does firing this projectile drain?

    numBullets = 1; // number of shotgun bullets
    range = 2000; // shotgun range
    spread = 0.0; // shotgun spread in degrees

	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 4;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

    //sound = MissileLauncherProjectileFlybySound;

    //projectileShapeName = "~/data/weapons/blaster/projectile.red.dts";

	explosion               = RedMissileLauncherProjectileImpact;
	hitEnemyExplosion       = RedMissileLauncherProjectileHit;
    hitTeammateExplosion    = RedMissileLauncherProjectileHit;
    //nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
    //hitDeflectorExplosion = SeekerDiscBounceEffect;

    //fxLight					= RedMissileLauncherProjectileFxLight;

	missEnemyEffect		 = RedMissileLauncherProjectileMissedEnemyEffect;

    //laserTail				 = RedMissileLauncherProjectileLaserTail;
    //laserTailLen			 = 10.0;

    laserTrail[0]			= RedMissileLauncherProjectileLaserTrail;
    //laserTrail[1]		 = Team1StingerProjectileLaserTrail2;

    //particleEmitter	  = RedMissileLauncherProjectileParticleEmitter;

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

function RedMissileLauncherProjectile::onAdd(%this, %obj)
{
    Parent::onAdd(%this, %obj);
    
    %src =  %obj.getSourceObject();
    if(!%src)
        return;

    %slot = %obj.sourceSlot;
    %image = %src.getMountedImage(%slot);
    if(%image.projectile.getId() == %this)
        %obj.missile = %image.fireMissile(%src, %slot);
}

function RedMissileLauncherProjectile::onRemove(%this, %obj)
{
    Parent::onRemove(%this, %obj);
}

function RedMissileLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
    
    %target = "";
	if( %col.getType() & $TypeMasks::ShapeBaseObjectType )
        %target = %col;

    if(%obj.missile !$= "")
    	%obj.missile.setTarget(%target);
}

//-----------------------------------------------------------------------------

datablock ShotgunProjectileData(BlueMissileLauncherProjectile : RedMissileLauncherProjectile)
{
    //projectileShapeName = "~/data/weapons/blaster/projectile.blue.dts";

	explosion            = BlueMissileLauncherProjectileImpact;
	hitEnemyExplosion    = BlueMissileLauncherProjectileHit;
    hitTeammateExplosion = BlueMissileLauncherProjectileHit;

	missEnemyEffect    = BlueMissileLauncherProjectileMissedEnemyEffect;

	//laserTail          = BlueMissileLauncherProjectileLaserTail;

    laserTrail[0]      = BlueMissileLauncherProjectileLaserTrail;

	lightColor  = "0.0 0.0 1.0";
};

function BlueMissileLauncherProjectile::onAdd(%this, %obj)
{
    RedMissileLauncherProjectile::onAdd(%this, %obj);
}

function BlueMissileLauncherProjectile::onRemove(%this, %obj)
{
    RedMissileLauncherProjectile::onRemove(%this, %obj);
}

function BlueMissileLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedMissileLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}
