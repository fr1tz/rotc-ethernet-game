//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./grenade2.projectile.gfx.orange.cs");
exec("./grenade2.projectile.gfx.green.cs");

datablock ProjectileData(RedGrenade2)
{
	stat = "grenade";

	// script damage properties...
	impactDamage        = 30; // only used to determine time for barrier
	impactImpulse       = 0;
	splashDamage        = 40;
	splashDamageRadius  = 15;
	splashImpulse       = 0;
	splashDamageFalloff = $SplashDamageFalloff::None;
	bypassDamageBuffer  = true;

	energyDrain = 0; // how much energy does firing this projectile drain?

	sound = Grenade2ProjectileSound;

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

    projectileShapeName = "share/shapes/rotc/weapons/disc/projectile.red.dts";

	explosion             = OrangeGrenade2Explosion;
//	hitEnemyExplosion     = OrangeDiscHitEnemy;
// nearEnemyExplosion	 = ThisDoesNotExist;
//	hitTeammateExplosion  = OrangeDiscHitEnemy;
//	hitDeflectorExplosion = OrangeDiscDeflectedEffect;
	bounceExplosion       = OrangeGrenade2BounceEffect;
 
    particleEmitter = OrangeGrenade2_ParticleEmitter;

//	laserTrail[0]		= RedGrenade2_Lasertrail;

	decals[0] = ExplosionDecalOne;

	hasLight	 = true;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedGrenade2::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);
}

function RedGrenade2::onRemove(%this, %obj)
{	
	Parent::onRemove(%this, %obj);
}

function RedGrenade2::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
	%radius = %this.splashDamageRadius;
	%sourceObject = %obj.getSourceObject();

	InitContainerRadiusSearch(%pos, %radius, $TypeMasks::ShapeBaseObjectType);
	%halfRadius = %radius / 2;
	while( (%targetObject = containerSearchNext()) != 0 )
	{
        // the observer cameras are ShapeBases; ignore them...
        if(%targetObject.getType() & $TypeMasks::CameraObjectType)
    	   continue;

		// ignore shapes with a barrier...
		if(%targetObject.hasBarrier())
			continue;

		%coverage = calcExplosionCoverage(%pos, %targetObject,
			$TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
			$TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType |
			$TypeMasks::TurretObjectType);

		if (%coverage == 0)
			continue;

        %sourceObject.setDiscTarget(%targetObject);
	}

    Parent::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);
}

//------------------------------------------------------------------------------

datablock ProjectileData(BlueGrenade2 : RedGrenade2)
{
    projectileShapeName = "share/shapes/rotc/weapons/disc/projectile.blue.dts";
	explosion = GreenGrenade2Explosion;
	bounceExplosion = GreenGrenade2BounceEffect;
    particleEmitter = GreenGrenade2_ParticleEmitter;
//	laserTrail[0] = GreenGrenade2_Lasertrail;
	lightColor = "0.0 0.0 1.0";
};

function BlueGrenade2::onAdd(%this, %obj)
{
    RedGrenade2::onAdd(%this, %obj);
}

function BlueGrenade2::onRemove(%this, %obj)
{
    RedGrenade2::onRemove(%this, %obj);
}

function BlueGrenade2::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
    RedGrenade2::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);
}
