//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - zapper.cs
// Code for the zapper
//------------------------------------------------------------------------------

exec("./zapper.sfx.cs");
exec("./zapper.gfx.red.cs");
exec("./zapper.gfx.blue.cs");

//-----------------------------------------------------------------------------

datablock ShapeBaseImageData(HolsteredZapperImage)
{
	// basic item properties
	shapeFile = "ethernet/data/weapons/assaultrifle/image_holstered.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 2;
	offset = "0 0 0";
	
	stateName[0] = "DoNothing";
};

//-----------------------------------------------------------------------------

datablock DecalData(ZapperExplosionDecal)
{
	sizeX = "1.0";
	sizeY = "1.0";
	textureName = "ethernet/data/textures/explosiondecal1";
	SelfIlluminated = false;
};

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ShotgunProjectileData(RedZapperProjectile)
{
	// script damage properties...
	impactDamage       = 25;
	impactImpulse      = 0;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	
	// how much energy does firing this projectile drain?...
	energyDrain = 10;

    numBullets = 1; // number of shotgun bullets
    range = 1000; // shotgun range
    spread = 0.0; // shotgun spread in degrees
	
	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 4;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

	//sound = ZapperProjectileFlybySound;
 
    //projectileShapeName = "ethernet/data/weapons/assaultrifle/projectile.dts";

	explosion               = RedZapperProjectileImpact;
	hitEnemyExplosion       = RedZapperProjectileHit;
    hitTeammateExplosion    = RedZapperProjectileHit;
    //nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
    //hitDeflectorExplosion = SeekerDiscBounceEffect;
    
	missEnemyEffect		 = RedZapperProjectileMissedEnemyEffect;

    //particleEmitter	= ZapperProjectileParticleEmitter;
	laserTrail[0]   = RedZapperProjectileLaserTrail;
	laserTail	    = RedZapperProjectileLaserTail;
	laserTailLen    = 20;

	muzzleVelocity    = 9999;
	velInheritFactor  = 0.0;
	
	isBallistic	= false;
	gravityMod	= 7.5;

	armingDelay			= 0;
	lifetime				= 1000*10;
	fadeDelay			  = 5000;
	
    decals[0] = ZapperExplosionDecal;
	
	hasLight	= false;
	lightRadius = 8.0;
	lightColor  = "1.0 0.8 0.2";
};

function RedZapperProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------

datablock ShotgunProjectileData(BlueZapperProjectile : RedZapperProjectile)
{
	explosion               = BlueZapperProjectileImpact;
	hitEnemyExplosion       = BlueZapperProjectileHit;
    hitTeammateExplosion    = BlueZapperProjectileHit;
	missEnemyEffect		    = BlueZapperProjectileMissedEnemyEffect;
	laserTrail[0]           = BlueZapperProjectileLaserTrail;
	laserTail	            = BlueZapperProjectileLaserTail;
};

function BlueZapperProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedZapperProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedZapperImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;
	
	// basic item properties
	shapeFile = "ethernet/data/weapons/blaster/image.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset      = "0 0 0";
	rotation    = "0 0 0";
	eyeOffset   = "0.275 -0.25 -0.2";
	eyeRotation = "0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 15;

	projectile = RedZapperProjectile;

	// script fields...
	iconId = 7;
	mainWeapon = true;
	armThread  = "holdblaster";  // armThread to use when holding this weapon
	crosshair  = "assaultrifle"; // crosshair to display when holding this weapon

	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Activate";
		stateTransitionOnNoAmmo[0]		 = "NoAmmo";

		// when mounted...
		stateName[1]                     = "Activate";
		stateTransitionOnTimeout[1]      = "Ready";
		stateTimeoutValue[1]             = 0.5;
		stateSequence[1]                 = "Activate";

		// ready to fire, just waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnNoAmmo[2]       = "NoAmmo";
  		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Fire";
        stateArmThread[2]                = "holdblaster";
		
		stateName[3]                     = "Fire";
		stateTransitionOnTimeout[3]      = "KeepAiming";
		stateTimeoutValue[3]             = 0.1;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = RedZapperProjectile;
		stateRecoil[3]                   = LightRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimblaster";
		stateSequence[3]                 = "Fire";
		stateEmitter[3]                  = RedZapperFireEmitter;
		stateEmitterNode[3]              = "fireparticles";
		stateEmitterTime[3]              = 0.1;
		stateSound[3]                    = ZapperFireSound;
		stateScript[3]                   = "onFire";
		
		stateName[4]                     = "KeepAiming";
		stateTransitionOnNoAmmo[4]       = "NoAmmo";
		stateTransitionOnNotLoaded[4]    = "Disabled";
		stateTransitionOnTriggerDown[4]  = "Fire";
		stateTransitionOnTimeout[4]      = "Ready";
		stateWaitForTimeout[4]           = false;
		stateTimeoutValue[4]             = 2.00;

        // no ammo...
		stateName[5]                     = "NoAmmo";
		stateTransitionOnAmmo[5]         = "Ready";
        stateTransitionOnTriggerDown[5]  = "DryFire";
		stateSequence[5]                 = "NoAmmo";
  
        // dry fire...
		stateName[6]                     = "DryFire";
		stateTransitionOnTriggerUp[6]    = "NoAmmo";
		stateSound[6]                    = WeaponEmptySound;

		// disabled...
		stateName[7]                     = "Disabled";
		stateTransitionOnLoaded[7]       = "Ready";
		stateAllowImageChange[7]         = false;
	//
	// ...end of image states
	//-------------------------------------------------
};

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueZapperImage : RedZapperImage)
{
	shapeFile = "ethernet/data/weapons/blaster/image.blue.dts";
	projectile = BlueZapperProjectile;
    stateFireProjectile[3] = BlueZapperProjectile;
    stateEmitter[3] = BlueZapperFireEmitter;
};

