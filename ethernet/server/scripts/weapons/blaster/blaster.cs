//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - blaster.cs
// Code for the blaster image
//------------------------------------------------------------------------------

exec("./blaster.projectile.cs");

exec("./blaster.sfx.cs");

//------------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedBlasterImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;

	// basic item properties
	shapeFile = "~/data/weapons/blaster/image.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset		= "0 0 0";
	rotation	 = "0 0 0";
	eyeOffset	= "0.3 -0.34 -0.5";
	eyeRotation = "0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 30;

	projectile = RedBlasterProjectile;

	// script fields...
	iconId = 5;
	armThread = "holdblaster";  // armThread to use when holding this weapon
	crosshair = "blaster"; // crosshair to display when holding this weapon
	
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
		stateSequence[1]                 = "activate";

		// ready to fire, just waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnNoAmmo[2]       = "NoAmmo";
		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Fire";
		stateArmThread[2]                = "holdblaster";

		// fire!...
		stateName[3]                     = "Fire";
		stateTransitionOnTimeout[3]      = "AfterFire";
		stateTimeoutValue[3]             = 0.00;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = RedBlasterProjectile;
		stateRecoil[3]                   = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimblaster";
		stateSequence[3]                 = "fire";
		stateSound[3]                    = BlasterFireSound;
		stateScript[3]                   = "onFire";
		
		// after fire...
		stateName[4]                     = "AfterFire";
		stateTransitionOnTriggerUp[4]    = "KeepAiming";

		// keep aiming...
		stateName[5]                     = "KeepAiming";
		stateTransitionOnNoAmmo[5]       = "NoAmmo";
		stateTransitionOnTriggerDown[5]  = "Fire";
		stateTransitionOnTimeout[5]      = "Ready";
		stateTransitionOnNotLoaded[5]    = "Disabled";
		stateWaitForTimeout[5]           = false;
		stateTimeoutValue[5]             = 2.00;

		// no ammo...
		stateName[6]                     = "NoAmmo";
        stateTransitionOnTriggerDown[6]  = "DryFire";
		stateTransitionOnAmmo[6]         = "Ready";
  
        // dry fire...
		stateName[7]                     = "DryFire";
		stateTransitionOnTriggerUp[7]    = "NoAmmo";
		stateSound[7]                    = WeaponEmptySound;

		// disabled...
		stateName[8]                     = "Disabled";
		stateTransitionOnLoaded[8]       = "Ready";
		stateAllowImageChange[8]         = false;
	//
	// ...end of image states
	//-------------------------------------------------
};

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueBlasterImage : RedBlasterImage)
{
	shapeFile = "~/data/weapons/blaster/image.blue.dts";
	projectile = BlueBlasterProjectile;
	stateFireProjectile[3] = BlueBlasterProjectile;
};

