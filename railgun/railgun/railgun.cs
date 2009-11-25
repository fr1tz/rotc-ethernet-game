//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - assaultrifle.cs
// Code for the assault rifle
//------------------------------------------------------------------------------

exec("./railgun.sfx.cs");
exec("./railgun.gfx.red.cs");
exec("./railgun.gfx.blue.cs");

//-----------------------------------------------------------------------------

datablock ShapeBaseImageData(HolsteredRailgunImage)
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
// projectile datablock...

datablock ShotgunProjectileData(RedRailgunProjectile)
{
	// script damage properties...
	impactDamage       = 200;
	impactImpulse      = 0;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	
	// how much energy does firing this projectile drain?...
	energyDrain = 0;
 
    numBullets = 1;
    spread = 0;
    range = 1000;

	trackingAgility = 0;
	
	explodesNearEnemies		  = false;
	explodesNearEnemiesRadius = 8;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

	//sound = RailgunProjectileFlybySound;
 
    //projectileShapeName = "ethernet/data/weapons/assaultrifle/projectile.dts";

	explosion             = RedRailgunProjectileExplosion;
	//bounceExplosion		  = RailgunProjectileBounceExplosion;
	//hitEnemyExplosion     = RailgunProjectileImpact;
	//nearEnemyExplosion    = RedRailgunProjectileMissedEnemyEffect;
    //hitTeammateExplosion  = RailgunProjectileImpact;
    //hitDeflectorExplosion = DiscDeflectedEffect;
    
    missEnemyEffect = RedRailgunProjectileMissedEnemyEffect;

    //particleEmitter	= RedRailgunProjectileParticleEmitter;
	//laserTrail[0]   = RedRailgunProjectileLaserTrailOne;
    //laserTrail[1]   = RedRailgunProjectileLaserTrailTwo;
    //laserTrail[2]   = RedRailgunProjectileLaserTrailThree;
	laserTail = RedRailgunProjectileLaserTail;
	laserTailLen = 20;

	muzzleVelocity   = 500;
	velInheritFactor = 0.0;
	
	isBallistic = false;
	gravityMod  = 7.5;

	armingDelay	 = 75;
	lifetime     = 1000*5;
	fadeDelay    = 0;
	
	decals[0]	= ExplosionDecalTwo;
	
	hasLight	= false;
	lightRadius = 8.0;
	lightColor  = "1.0 0.8 0.2";
};

//--------------------------------------------------------------------------

datablock ShotgunProjectileData(BlueRailgunProjectile : RedRailgunProjectile)
{
	explosion = BlueRailgunProjectileExplosion;
    missEnemyEffect = BlueRailgunProjectileMissedEnemyEffect;
    //particleEmitter	= BlueRailgunProjectileParticleEmitter;
	//laserTrail[0]   = BlueRailgunProjectileLaserTrailOne;
    //laserTrail[1]   = BlueRailgunProjectileLaserTrailTwo;
    //laserTrail[2]   = BlueRailgunProjectileLaserTrailThree;
	laserTail = BlueRailgunProjectileLaserTail;
};

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedRailgunImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;
	
	// basic item properties
	shapeFile = "ethernet/data/weapons/sniperrifle/image.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset		= "0 0 0";
	rotation	 = "0 0 0";
	eyeOffset   = "0.25 -0.2 -0.2";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = false;

	usesEnergy = true;
	minEnergy = 15;

	projectile = RedRailgunProjectile;

	// script fields...
	iconId = 7;
	mainWeapon = true;
	armThread  = "holdrifle";  // armThread to use when holding this weapon
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
        stateArmThread[2]                = "holdrifle";

        // fire...
		stateName[3]                     = "Fire";
		stateTransitionOnTimeout[3]      = "Emitter1";
		stateTimeoutValue[3]             = 0.0;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = RedRailgunProjectile;
		stateRecoil[3]                   = NoRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimrifle";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = RailgunFireSound;
		stateScript[3]                   = "onFire";
  
		// eyecandy...
		stateName[4]                     = "Emitter1";
		stateTransitionOnTimeout[4]      = "Emitter2";
		stateTimeoutValue[4]             = 0.00;
		stateAllowImageChange[4]         = false;
		//stateEmitter[4]                  = RedSniperRifleFireEmitter;
		stateEmitterNode[4]              = "emitter1";
		stateEmitterTime[4]              = 0.1;

		// eyecandy...
		stateName[5]                     = "Emitter2";
		stateTransitionOnTimeout[5]      = "Reload";
		stateTimeoutValue[5]             = 0.00;
		stateAllowImageChange[5]         = false;
		//stateEmitter[5]                  = RedSniperRifleFireEmitter;
		stateEmitterNode[5]              = "emitter2";
		stateEmitterTime[5]              = 0.1;

		// reload...
		stateName[6]                     = "Reload";
		stateTransitionOnTimeout[6]      = "KeepAiming";
		stateTimeoutValue[6]             = 1.00;
		stateAllowImageChange[6]         = false;

        // keep aiming...
		stateName[7]                     = "KeepAiming";
		stateTransitionOnNoAmmo[7]       = "NoAmmo";
		stateTransitionOnNotLoaded[7]    = "Disabled";
		stateTransitionOnTriggerDown[7]  = "Fire";
		stateTransitionOnTimeout[7]      = "Ready";
		stateWaitForTimeout[7]           = false;
		stateTimeoutValue[7]             = 2.00;

        // no ammo...
		stateName[8]                     = "NoAmmo";
		stateTransitionOnAmmo[8]         = "Ready";
        stateTransitionOnTriggerDown[8]  = "DryFire";
		stateSequence[8]                 = "NoAmmo";

        // dry fire...
		stateName[9]                     = "DryFire";
		stateTransitionOnTriggerUp[9]    = "NoAmmo";
		stateSound[9]                    = WeaponEmptySound;

		// disabled...
		stateName[10]                    = "Disabled";
		stateTransitionOnLoaded[10]      = "Ready";
		stateAllowImageChange[10]        = false;
	//
	// ...end of image states
	//-------------------------------------------------
};

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueRailgunImage : RedRailgunImage)
{
	shapeFile = "ethernet/data/weapons/sniperrifle/image.blue.dts";
	projectile = BlueRailgunProjectile;
    stateFireProjectile[3] = BlueRailgunProjectile;
	//stateEmitter[4] = BlueSniperRifleFireEmitter;
	//stateEmitter[5] = BlueSniperRifleFireEmitter;
};

