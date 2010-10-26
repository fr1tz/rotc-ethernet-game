//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - missilelauncher.cs
// Code for the missile launcher
//------------------------------------------------------------------------------

exec("./missilelauncher.sfx.cs");
exec("./missilelauncher.projectile.cs");
exec("./missilelauncher.missile.cs");

//------------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedMissileLauncherImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;

	// basic item properties
	shapeFile = "~/data/weapons/missilelauncher/image.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset      = "0 0 0";
	rotation    = "0 0 0";
	eyeOffset   = "0.4 -0.2 -0.2";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	// Use energy for ammo?
	usesEnergy = true;
	minEnergy = 45;
	
    // charging...
    minCharge = 0.4;
    
    // NOTE: The Projectile is not actually used, but is needed to
    // put the image into "client fire mode".
	projectile = RedMissileLauncherProjectile;

	// targeting...
    targetingMask = $TargetingMask::Heat;
	targetingMaxDist = 10000;

	// script fields...
	iconId = 9;
	specialWeapon = true;
	armThread = "holdrifle";  // armThread to use when holding this weapon
	crosshair = "missilelauncher"; // crosshair to display when holding this weapon
    missile = RedMissileLauncherMissile;
 
	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Activate";
		stateTransitionOnNoAmmo[0]       = "NoAmmo";

		// when mounted...
		stateName[1]                     = "Activate";
		stateTransitionOnTimeout[1]      = "Ready";
		stateTimeoutValue[1]             = 0.5;
		stateSequence[1]                 = "idle";

		// ready, just waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnMovement[2]     = "Moving";
		stateTransitionOnNoAmmo[2]       = "NoAmmo";
		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Charge";
		stateArmThread[2]                = "holdrifle";
		stateSequence[2]                 = "idle";
		stateScript[2]                   = "onReady";
  
		// charge...
		stateName[3]                     = "Charge";
		stateTransitionOnTriggerUp[3]    = "CheckFire";
		stateTransitionOnMovement[3]     = "ChargeAborted";
		stateTransitionOnNoAmmo[3]       = "ChargeAborted";
		stateTarget[3]                   = true;
		stateCharge[3]                   = true;
		stateAllowImageChange[3]         = false;
		stateArmThread[3]                = "aimrifle";
		stateSound[3]                    = MissileLauncherChargeSound;
		stateSequence[3]                 = "charge";
		stateScript[3]                   = "onCharge";
  
		// check fire...
		stateName[4]                     = "CheckFire";
		stateTransitionOnCharged[4]      = "Fire";
		stateTransitionOnNotCharged[4]   = "Ready";
		stateFire[4]                     = true;
		stateTarget[4]                   = true;

		// fire!...
		stateName[5]                     = "Fire";
		stateTransitionOnTimeout[5]      = "Emitter1";
		stateTimeoutValue[5]             = 0.00;
		stateRecoil[5]                   = NoRecoil;
		stateAllowImageChange[5]         = false;
		stateEjectShell[5]               = true;
		stateArmThread[5]                = "aimrifle";
		stateSequence[5]                 = "fire";
		stateSound[5]                    = MissileLauncherFireSound;
		stateScript[5]                   = "onFire";
  
        stateName[6]                     = "Emitter1";
        stateTransitionOnTimeout[6]      = "Emitter2";
        stateTimeoutValue[6]             = 0.00;
        stateAllowImageChange[6]         = false;
        stateEmitter[6]                  = RedMissileLauncherFireEmitter;
        stateEmitterNode[6]              = "emitter1";
        stateEmitterTime[6]              = 0.05;

        stateName[7]                     = "Emitter2";
        stateTransitionOnTimeout[7]      = "AfterFire";
        stateTimeoutValue[7]             = 0.00;
        stateAllowImageChange[7]         = false;
        stateEmitter[7]                  = RedMissileLauncherFireEmitter;
        stateEmitterNode[7]              = "emitter2";
        stateEmitterTime[7]              = 0.05;

		// after fire...
		stateName[8]                     = "AfterFire";
		stateTransitionOnTriggerUp[8]    = "KeepAiming";

		// keep aiming...
		stateName[9]                     = "KeepAiming";
		stateTransitionOnNoAmmo[9]       = "NoAmmo";
		stateTransitionOnTriggerDown[9]  = "Ready";
		stateTransitionOnTimeout[9]      = "Ready";
		stateTransitionOnNotLoaded[9]    = "Disabled";
		stateWaitForTimeout[9]           = false;
		stateTimeoutValue[9]             = 2.00;

		// no ammo...
		stateName[10]                    = "NoAmmo";
        stateTransitionOnTriggerDown[10] = "DryFire";
		stateTransitionOnAmmo[10]        = "Ready";
		stateSequence[10]                = "idle";
		stateScript[10]                  = "onNoAmmo";

        // dry fire...
		stateName[11]                    = "DryFire";
		stateTransitionOnTriggerUp[11]   = "NoAmmo";
		stateTransitionOnAmmo[11]        = "Ready";
		stateSound[11]                   = WeaponEmptySound;
		stateSequence[11]                = "idle";
  
        // charge aborted...
		stateName[12]                    = "ChargeAborted";
		stateTransitionOnTriggerUp[12]   = "Ready";
		stateTransitionOnNoMovement[12]  = "Ready";
		stateSound[12]                   = MissileLauncherChargeAbortedSound;
		stateSequence[12]                = "nocharge";
  
        // moving...
		stateName[13]                    = "Moving";
		stateTransitionOnTriggerDown[13] = "MovingTriggerDown";
		stateTransitionOnNoMovement[13]  = "Ready";
		stateSequence[13]                = "idle";
  
        // moving with trigger...
		stateName[14]                    = "MovingTriggerDown";
		stateTransitionOnTriggerUp[14]   = "Moving";
		stateTransitionOnNoMovement[14]  = "Ready";
		stateSequence[14]                = "nocharge";

		// disabled...
		stateName[15]                    = "Disabled";
		stateTransitionOnLoaded[15]      = "Ready";
		stateAllowImageChange[15]        = false;
		stateSequence[15]                = "idle";
	//
	// ...end of image states
	//-------------------------------------------------
};

function RedMissileLauncherImage::onMount(%this, %obj, %slot)
{
    Parent::onMount(%this, %obj, %slot);
}

function RedMissileLauncherImage::onUnmount(%this, %obj, %slot)
{
    Parent::onUnmount(%this, %obj, %slot);
}

function RedMissileLauncherImage::onReady(%this, %obj, %slot)
{

}

function RedMissileLauncherImage::onCharge(%this, %obj, %slot)
{

}

function RedMissileLauncherImage::onFire(%this, %obj, %slot)
{
	%projectile = %this.missile;

	// determine initial projectile velocity based on the
	// gun's muzzle point and the object's current velocity...
	%muzzleVector = %obj.getMuzzleVector(%slot);
	%objectVelocity = %obj.getVelocity();
	%muzzleVelocity = VectorAdd(
		VectorScale(%muzzleVector, %projectile.muzzleVelocity),
		VectorScale(%objectVelocity, %projectile.velInheritFactor));

	// determine muzzle-point...
	%muzzlePoint = %obj.getMuzzlePoint(%slot);
    %muzzlePoint = VectorAdd(%muzzlePoint, "0 0 0.25");

	// create the projectile object...
	%p = new Projectile() {
		dataBlock       = %projectile;
        teamId          = %obj.teamId;
		initialVelocity = %muzzleVelocity;
		initialPosition = %muzzlePoint;
		sourceObject    = %obj;
		sourceSlot      = %slot;
		client			= %obj.client;
	};
	MissionCleanup.add(%p);
 
    %target = %obj.getImageTarget(%slot);
    if(isObject(%target))
        %p.setTarget(%target);
        
    return %p;
}

function RedMissileLauncherImage::onNoAmmo(%this, %obj, %slot)
{

}

function RedMissileLauncherImage::fireMissile(%this, %obj, %slot)
{

}


//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueMissileLauncherImage : RedMissileLauncherImage)
{
	shapeFile = "~/data/weapons/missilelauncher/image.blue.dts";

	projectile = BlueMissileLauncherProjectile;
    missile = BlueMissileLauncherMissile;

	stateEmitter[6] = BlueMissileLauncherFireEmitter;
	stateEmitter[7] = BlueMissileLauncherFireEmitter;
};

function BlueMissileLauncherImage::onMount(%this, %obj, %slot)
{
	RedMissileLauncherImage::onMount(%this, %obj, %slot);
}

function BlueMissileLauncherImage::onUnmount(%this, %obj, %slot)
{
	RedMissileLauncherImage::onUnmount(%this, %obj, %slot);
}

function BlueMissileLauncherImage::onReady(%this, %obj, %slot)
{
    RedMissileLauncherImage::onReady(%this, %obj, %slot);
}

function BlueMissileLauncherImage::onCharge(%this, %obj, %slot)
{
    RedMissileLauncherImage::onCharge(%this, %obj, %slot);
}

function BlueMissileLauncherImage::onFire(%this, %obj, %slot)
{
	RedMissileLauncherImage::onFire(%this, %obj, %slot);
}

function BlueMissileLauncherImage::onNoAmmo(%this, %obj, %slot)
{
    RedMissileLauncherImage::onNoAmmo(%this, %obj, %slot);
}

function BlueMissileLauncherImage::fireMissile(%this, %obj, %slot)
{
   RedMissileLauncherImage::fireMissile(%this, %obj, %slot);
}

