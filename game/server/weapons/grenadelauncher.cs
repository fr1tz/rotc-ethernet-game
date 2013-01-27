//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - grenadelauncher.cs
// Code for the grenadelauncher
//------------------------------------------------------------------------------
                                         
exec("./grenadelauncher.sfx.cs");
exec("./grenadelauncher.gfx.red.cs");
exec("./grenadelauncher.gfx.blue.cs");

// Projectile implementation (uncomment only one exec!)
// Using Projectile class:
// Work on for now, despite the projectiles not *really* being sticky
exec("./grenadelauncher.projectile1.cs"); 
// Using Grenade class:
// (does not work b/c Grenade class is currently fubar'd)
//exec("./grenadelauncher.projectile2.cs"); 

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedGrenadeLauncherImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;
	
	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/grenadelauncher/image2.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset      = "0 0 0";
	rotation    = "0 0 0";
    eyeOffset   = "0.25 -0.4 -0.2";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 15;

	// Can't set this if RedGrenadeLauncherProjectile is GrenadeData
	//projectile = RedGrenadeLauncherProjectile;

	// script fields...
	iconId = 7;
	specialWeapon = true;
	armThread  = "holdrifle";  // armThread to use when holding this weapon
	crosshair  = "assaultrifle"; // crosshair to display when holding this weapon
	grenade = RedGrenadeLauncherProjectile;

	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Ready";
		stateTransitionOnNoAmmo[0]		 = "NoAmmo";

		// when mounted...
		stateName[1]                     = "Activate";
		stateTransitionOnTimeout[1]      = "Ready";
		stateTimeoutValue[1]             = 0.5;
		stateSpinThread[1]               = "Stop";
		stateSequence[1]                 = "Activate";

		// ready to fire, just waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnNoAmmo[2]       = "NoAmmo";
  		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Fire1";
        stateArmThread[2]                = "holdrifle";
		stateSpinThread[2]               = "Stop";

		stateName[3]                     = "Fire1";
		stateTransitionOnTimeout[3]      = "Fire2";
		stateTransitionOnTriggerUp[3]    = "Cooldown";
		stateTransitionOnNoAmmo[3]       = "Cooldown";
		stateTimeoutValue[3]             = 0.17;
		stateSpinThread[3]               = "FullSpeed";
		stateFire[3]                     = true;
		//stateFireProjectile[3]           = RedGrenadeLauncherProjectile;
		stateAllowImageChange[3]         = false;
		stateArmThread[3]                = "aimrifle";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = GrenadeLauncherFireSound;
		stateScript[3]                   = "onFire";

		stateName[4]                     = "Fire2";
		stateTransitionOnTimeout[4]      = "Fire3";
		stateTransitionOnTriggerUp[4]    = "Cooldown";
		stateTransitionOnNoAmmo[4]       = "Cooldown";
		stateTimeoutValue[4]             = 0.17;
		//stateFireProjectile[4]           = RedGrenadeLauncherProjectile;
		stateAllowImageChange[4]         = false;
		stateSequence[4]                 = "Fire";
		stateSound[4]                    = GrenadeLauncherFireSound;
		stateScript[4]                   = "onFire";

		stateName[5]                     = "Fire3";
		stateTransitionOnTimeout[5]      = "Fire4";
		stateTransitionOnTriggerUp[5]    = "Cooldown";
		stateTransitionOnNoAmmo[5]       = "Cooldown";
		stateTimeoutValue[5]             = 0.17;
		stateSpinThread[5]               = "FullSpeed";
		//stateFireProjectile[5]           = RedGrenadeLauncherProjectile;
		stateAllowImageChange[5]         = false;
		stateSequence[5]                 = "Fire";
		stateSound[5]                    = GrenadeLauncherFireSound;
		stateScript[5]                   = "onFire";

		stateName[6]                     = "Fire4";
		stateTransitionOnTimeout[6]      = "Cooldown";
		stateTransitionOnNoAmmo[6]       = "Cooldown";
		stateTimeoutValue[6]             = 0.17;
		stateSpinThread[6]               = "FullSpeed";
		//stateFireProjectile[6]           = RedGrenadeLauncherProjectile;
		stateAllowImageChange[6]         = false;
		stateSequence[6]                 = "Fire";
		stateSound[6]                    = GrenadeLauncherFireSound;
		stateScript[6]                   = "onFire";
		
		stateName[7]                     = "Cooldown";
		stateTransitionOnTimeout[7]      = "KeepAiming";
		stateTimeoutValue[7]             = 1.0;
		stateSpinThread[7]               = "SpinDown";
		stateArmThread[7]                = "aimrifle";
		
		stateName[8]                     = "KeepAiming";
		stateTransitionOnNoAmmo[8]       = "NoAmmo";
		stateTransitionOnNotLoaded[8]    = "Disabled";
		stateTransitionOnTriggerDown[8]  = "Fire1";
		stateTransitionOnTimeout[8]      = "Ready";
		stateWaitForTimeout[8]           = false;
		stateTimeoutValue[8]             = 2.00;
		stateSpinThread[8]               = "Stop";

        // no ammo...
		stateName[9]                     = "NoAmmo";
		stateTransitionOnAmmo[9]         = "Ready";
        stateTransitionOnTriggerDown[9]  = "DryFire";
		stateTimeoutValue[9]             = 0.50;
		stateSequence[9]                 = "NoAmmo";
  
        // dry fire...
		stateName[10]                    = "DryFire";
		stateTransitionOnTriggerUp[10]   = "NoAmmo";
		stateSound[10]                   = WeaponEmptySound;
  
		// disabled...
		stateName[11]                    = "Disabled";
		stateTransitionOnLoaded[11]      = "Ready";
		stateAllowImageChange[11]        = false;
	//
	// ...end of image states
	//-------------------------------------------------
};

function RedGrenadeLauncherImage::onFire(%this, %obj, %slot)
{
	%projectile = %this.grenade;

	// determine muzzle-point...
	%muzzlePoint = %obj.getMuzzlePoint(%slot);

	// determine initial projectile velocity...
	%muzzleVector = %obj.getMuzzleVector(%slot);

	%objectVelocity = %obj.getVelocity();
	%muzzleVelocity = VectorAdd(
		VectorScale(%muzzleVector, %projectile.muzzleVelocity),
		VectorScale(%objectVelocity, %projectile.velInheritFactor));
		
	%p = spawnGrenadeLauncherProjectile(%projectile, %obj, %slot, %muzzlePoint, %muzzleVelocity);
	MissionCleanup.add(%p);

	%obj.setEnergyLevel(%obj.getEnergyLevel() - %projectile.energyDrain);
 
	%target = %obj.getImageTarget(%slot);
	if(isObject(%target))
		%p.setTarget(%target);
        
	return %p;
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueGrenadeLauncherImage : RedGrenadeLauncherImage)
{
	grenade = BlueGrenadeLauncherProjectile;
	//stateFireProjectile[3] = BlueGrenadeLauncherProjectile;
	//stateFireProjectile[4] = BlueGrenadeLauncherProjectile;
	//stateFireProjectile[5] = BlueGrenadeLauncherProjectile;
	//stateFireProjectile[6] = BlueGrenadeLauncherProjectile;
};

function BlueGrenadeLauncherImage::onFire(%this, %obj, %slot)
{
	RedGrenadeLauncherImage::onFire(%this, %obj, %slot);
}

