//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./disc.grenade.sfx.cs");
exec("./disc.grenade.gfx.red.cs");
exec("./disc.grenade.gfx.blue.cs");

datablock ProjectileData(RedDiscGrenade)
{
	// script damage properties...
	impactDamage       = 0;
 	impactImpulse      = 0;
	splashDamage       = 150;
	splashDamageRadius = 20;
	splashImpulse      = 2500;

	energyDrain = 0; // how much energy does firing this projectile drain?

	//sound = DiscProjectileSound;

	muzzleVelocity   = 100;
	velInheritFactor = 0.0;

	armingDelay  = 1000;
	lifetime     = 10000;
	fadeDelay    = 0;

	isBallistic      = true;
	gravityMod       = 8.0;
	bounceElasticity = 0.7;
	bounceFriction   = 0.5;
    //numBounces           = 3;

	projectileShapeName = "~/data/weapons/disc/projectile.red.dts";

	explosion             = RedDiscGrenadeExplosion;
//	hitEnemyExplosion     = RedDiscHitEnemy;
// nearEnemyExplosion	 = ThisDoesNotExist;
//	hitTeammateExplosion  = RedDiscHitEnemy;
//	hitDeflectorExplosion = RedDiscDeflectedEffect;
	bounceExplosion       = RedDiscGrenadeBounceEffect;
 
    particleEmitter = RedDiscGrenade_ParticleEmitter;

	laserTrail[0]		= RedDiscInnerLaserTrail;
	laserTrail[1]		= RedDiscOuterLaserTrail;

	decals[0] = ExplosionDecalOne;

	hasLight	 = true;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedDiscGrenade::onAdd(%this, %obj)
{

}

function RedDiscGrenade::onRemove(%this, %obj)
{
    %source = %obj.sourceObject;
    %source.incDiscs();
}

//------------------------------------------------------------------------------

datablock ProjectileData(BlueDiscGrenade : RedDiscGrenade)
{
	projectileShapeName = "~/data/weapons/disc/projectile.blue.dts";
	explosion = BlueDiscGrenadeExplosion;
	bounceExplosion = BlueDiscGrenadeBounceEffect;
    particleEmitter = BlueDiscGrenade_ParticleEmitter;
	laserTrail[0] = BlueDiscInnerLaserTrail;
	laserTrail[1] = BlueDiscOuterLaserTrail;
	lightColor = "0.0 0.0 1.0";
};

function BlueDiscGrenade::onAdd(%this, %obj)
{
    RedDiscGrenade::onAdd(%this, %obj);
}

function BlueDiscGrenade::onRemove(%this, %obj)
{
    RedDiscGrenade::onRemove(%this, %obj);
}
