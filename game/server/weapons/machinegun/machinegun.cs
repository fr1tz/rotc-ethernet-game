//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - machinegun.cs
// Code for the machine gun
//------------------------------------------------------------------------------

exec("./machinegun.sfx.cs");
exec("./machinegun.gfx.red.cs");
exec("./machinegun.gfx.blue.cs");

//-----------------------------------------------------------------------------

datablock ShapeBaseImageData(HolsteredMachineGunImage)
{
	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image_holstered.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 2;
	offset = "0 0 0";
	
	stateName[0] = "DoNothing";
};

//-----------------------------------------------------------------------------
// projectile datablock...

datablock TracerProjectileData(RedMachineGunProjectile)
{
	// script damage properties...
	impactDamage        = 5;
	impactImpulse       = 0;
	splashDamage        = 0;
	splashDamageRadius  = 0;
	splashDamageFalloff = $SplashDamageFalloff::Exponential;
	splashImpulse       = 0;
	
	// how much energy does firing this projectile drain?...
	energyDrain = 0;

	trackingAgility = 0;
	
	explodesNearEnemies			= false;
	explodesNearEnemiesRadius	= 5;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = MachineGunProjectileFlybySound;
 
//  projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion             = RedMachineGunProjectileImpact;
//	bounceExplosion		  = RedMachineGunProjectileBounceExplosion;
//	hitEnemyExplosion     = MachineGunProjectileImpact;
//	nearEnemyExplosion    = RedMachineGunProjectileExplosion;
//	hitTeammateExplosion  = MachineGunProjectileImpact;
//	hitDeflectorExplosion = DiscDeflectedEffect;

	missEnemyEffect		 = RedMachineGunProjectileMissedEnemyEffect;

//   particleEmitter	= MachineGunProjectileParticleEmitter;
//	laserTrail[0]   = RedMachineGunProjectileLaserTrail;
//	laserTrail[1]   = MachineGunProjectileRedLaserTrail;
	laserTail	    = RedMachineGunProjectileLaserTail;
	laserTailLen    = 10;

	muzzleVelocity   = 600;
	velInheritFactor = 1.0;
	
	isBallistic = false;
	gravityMod	= 7.5;

	armingDelay	= 0;
	lifetime    = 1000*2;
	fadeDelay   = 5000;
	
	decals[0]	= BulletHoleDecalOne;
	
	hasLight	 = true;
	lightRadius = 6.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedMachineGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------

datablock TracerProjectileData(BlueMachineGunProjectile : RedMachineGunProjectile)
{
//	projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";
	explosion = RedMachineGunProjectileImpact;
//	bounceExplosion = BlueMachineGunProjectileBounceExplosion;
	nearEnemyExplosion    = BlueMachineGunProjectileExplosion;
//	laserTrail[0]   = BlueMachineGunProjectileLaserTrail;
	laserTail = BlueMachineGunProjectileLaserTail;
	lightColor  = "0.0 0.0 1.0";
};

function BlueMachineGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedMachineGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedMachineGunImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;
	
	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/machinegun/image.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset		= "0 0 0";
	rotation	 = "0 0 0";
	eyeOffset	= "0.175 -1.75 -0.175";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = false;
	minEnergy = 0;

	projectile = RedMachineGunProjectile;

	// script fields...
	iconId = 7;
	specialWeapon = true;
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
		stateSequence[1]                 = "idle";

		// ready to fire, just waiting for the trigger...
		stateName[2]                     = "Ready";
  		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Fire";
        stateArmThread[2]                = "holdrifle";
		stateScript[2]                   = "onReady";
		
		stateName[3]                     = "Fire";
		stateTransitionOnNoAmmo[3]       = "Reload";
		stateTransitionOnTimeout[3]      = "Fire";
		stateTransitionOnTriggerUp[3]    = "KeepAiming";
		stateTimeoutValue[3]             = 0.05;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = RedMachineGunProjectile;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateRecoil[3]                   = "NoRecoil";
		stateArmThread[3]                = "aimrifle";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = MachineGunFireSound;
		stateScript[3]                   = "onFire";
		
		stateName[4]                     = "KeepAiming";
		stateTransitionOnNoAmmo[4]       = "NoAmmo";
		stateTransitionOnNotLoaded[4]    = "Disabled";
		stateTransitionOnTriggerDown[4]  = "Fire";
		stateTransitionOnTimeout[4]      = "Ready";
		stateWaitForTimeout[4]           = false;
		stateTimeoutValue[4]             = 2.00;
		stateScript[4]                   = "onKeepAiming";

        // no ammo...
		stateName[5]                     = "NoAmmo";
		stateTransitionOnAmmo[5]         = "Ready";
        stateTransitionOnTriggerDown[5]  = "DryFire";
		stateSequence[5]                 = "idle";
		stateScript[5]                   = "onNoAmmo";
  
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

function RedMachineGunImage::onUnmount(%this, %obj, %slot)
{
    Parent::onUnmount(%this, %obj, %slot);
    %obj.setSniping(false);
}

function RedMachineGunImage::onReady(%this, %obj, %slot)
{
//	error("onReady");
}

function RedMachineGunImage::onFire(%this, %obj, %slot)
{
//	error("onFire");
    %obj.setSniping(true);
}

function RedMachineGunImage::onKeepAiming(%this, %obj, %slot)
{
//	error("onKeepAiming");
    %obj.setSniping(false);
}

function RedMachineGunImage::onNoAmmo(%this, %obj, %slot)
{
//	error("onNoAmmo");
	%obj.setSniping(false);
}


//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueMachineGunImage : RedMachineGunImage)
{
	projectile = BlueMachineGunProjectile;
    stateFireProjectile[3] = BlueMachineGunProjectile;
};

function BlueMachineGunImage::onUnmount(%this, %obj, %slot)
{
	RedMachineGunImage::onUnmount(%this, %obj, %slot);
}

function BlueMachineGunImage::onReady(%this, %obj, %slot)
{
	RedMachineGunImage::onReady(%this, %obj, %slot);
}

function BlueMachineGunImage::onFire(%this, %obj, %slot)
{
	RedMachineGunImage::onFire(%this, %obj, %slot);
}

function BlueMachineGunImage::onKeepAiming(%this, %obj, %slot)
{
	RedMachineGunImage::onKeepAiming(%this, %obj, %slot);
}

function BlueMachineGunImage::onNoAmmo(%this, %obj, %slot)
{
	RedMachineGunImage::onNoAmmo(%this, %obj, %slot);
}

