//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./sniperrifle3.sfx.cs");
exec("./sniperrifle3.projectile.cs");

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(HolsteredSniperRifle3Image)
{
	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/sniperrifle/image_holstered.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 2;
	offset = "0 0 0";

	stateName[0] = "DoNothing";
};

//------------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedSniperRifle3Image)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;

	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/missilelauncher/image2.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset      = "0 0 0";
	rotation    = "0 0 0";
    eyeOffset   = "0.5 -0.2 -0.2";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	// Use energy for ammo?
	usesEnergy = true;
	minEnergy = 30;
	
    // charging...
    minCharge = 0.4;
    
	projectile = RedSniperRifle3Projectile;
    missile = RedSniperMissile;
    
	// targeting...
    targetingMask = $TargetingMask::Heat;
	targetingMaxDist = 10000;
    followTarget = true;
	
	// script fields...
	iconId = 9;
	specialWeapon = true;
	armThread = "holdrifle";  // armThread to use when holding this weapon
	crosshair = "sniperrifle"; // crosshair to display when holding this weapon
 
	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Activate";
		stateTransitionOnNoAmmo[0]       = "NoAmmo";
		stateTimeoutValue[0]             = 0.25;
		stateSequence[0]                 = "lowered";

		// when mounted...
		stateName[1]                     = "Activate";
		stateTransitionOnTimeout[1]      = "Ready";
		stateTimeoutValue[1]             = 0.25;
		stateSequence[1]                 = "lowered";

		// ready, just waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnNoAmmo[2]       = "NoAmmo";
		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "RaiseCharge";
		stateArmThread[2]                = "holdrifle";
		stateSequence[2]                 = "lowered";
		stateScript[2]                   = "onReady";
		stateSpin[2]                     = "FullSpin";
  
		// raise & charge...
		stateName[3]                     = "RaiseCharge";
		stateTransitionOnTriggerUp[3]    = "CheckFire";
		//stateTransitionOnNoAmmo[3]       = "NoAmmo";
		stateTarget[3]                   = true;
		stateCharge[3]                   = true;
		stateAllowImageChange[3]         = true;
		stateArmThread[3]                = "aimrifle";
		stateSound[3]                    = SniperRifle3PowerUpSound;
		stateSequence[3]                 = "raisecharge";
		stateScript[3]                   = "onCharge";
  
		// check fire...
		stateName[4]                     = "CheckFire";
		stateTransitionOnCharged[4]      = "Fire";
		stateTransitionOnNotCharged[4]   = "Ready";
		//stateFire[4]                     = true;
		stateTarget[4]                   = true;
		stateAllowImageChange[4]         = false;

		// fire!...
		stateName[5]                     = "Fire";
		stateTransitionOnTimeout[5]      = "AfterFire";
		stateTimeoutValue[5]             = 0;
		stateFire[5]                     = true;
		stateFireProjectile[5]           = RedSniperRifle3Projectile;
		stateRecoil[5]                   = NoRecoil;
		stateAllowImageChange[5]         = false;
		stateEjectShell[5]               = true;
		stateArmThread[5]                = "aimrifle";
		stateSequence[5]                 = "fire";
		stateScript[5]                   = "onFire";

		// after fire...
		stateName[8]                     = "AfterFire";
		stateTransitionOnTriggerUp[8]    = "KeepAiming";

		// keep aiming...
		stateName[9]                     = "KeepAiming";
		stateTransitionOnNoAmmo[9]       = "NoAmmo";
		stateTransitionOnTriggerDown[9]  = "Charge";
		stateTransitionOnTimeout[9]      = "Ready";
		stateTransitionOnNotLoaded[9]    = "Disabled";
		stateWaitForTimeout[9]           = false;
		stateTimeoutValue[9]             = 2.00;
		
		// charge...
		stateName[13]                     = "Charge";
		stateTransitionOnTriggerUp[13]    = "CheckFire";
		//stateTransitionOnNoAmmo[13]       = "NoAmmo";
		stateTarget[13]                   = true;
		stateCharge[13]                   = true;
		stateAllowImageChange[13]         = true;
		stateArmThread[13]                = "aimrifle";
		stateSound[13]                    = SniperRifle3PowerUpSound;
		stateSequence[13]                 = "charge";
		stateScript[13]                   = "onCharge";		

		// no ammo...
		stateName[10]                    = "NoAmmo";
        stateTransitionOnTriggerDown[10] = "DryFire";
		stateTransitionOnAmmo[10]        = "Ready";
		stateSequence[10]                = "lowered";
		stateTimeoutValue[10]            = 0.50;
		stateScript[10]                  = "onNoAmmo";

        // dry fire...
		stateName[11]                    = "DryFire";
		stateTransitionOnTriggerUp[11]   = "NoAmmo";
		stateTransitionOnAmmo[11]        = "Ready";
		stateSound[11]                   = WeaponEmptySound;
		stateSequence[11]                = "idle";

		// disabled...
		stateName[12]                    = "Disabled";
		stateTransitionOnLoaded[12]      = "Ready";
		stateAllowImageChange[12]        = false;
		stateSequence[12]                = "idle";
	//
	// ...end of image states
	//-------------------------------------------------
};

function RedSniperRifle3Image::onMount(%this, %obj, %slot)
{
    Parent::onMount(%this, %obj, %slot);
}

function RedSniperRifle3Image::onUnmount(%this, %obj, %slot)
{
    Parent::onUnmount(%this, %obj, %slot);
    //%obj.setSniping(false);
}

function RedSniperRifle3Image::setupHud(%this, %obj, %slot)
{
   %client = %obj.client;
   if(!isObject(%client))
      return;

   commandToClient(%client, 'Crosshair', 0);
   //commandToClient(%client, 'Crosshair', 2, 2);
   commandToClient(%client, 'Crosshair', 3, 8, 8);
   commandToClient(%client, 'Crosshair', 5, "./rotc/ch2");
   //commandToClient(%client, 'Crosshair', 6, "./rotc/ch.static.2a", 128, 128);
   commandToClient(%client, 'Crosshair', 1);
   
   %client.setHudMenuC(0, "<bitmap:share/hud/>", 1, true);
}

function RedSniperRifle3Image::getBulletSpread(%this, %obj)
{
   return 0.12;
}

function RedSniperRifle3Image::onReady(%this, %obj, %slot)
{
	//error("onReady");
    %obj.setSniping(false);
}

function RedSniperRifle3Image::onCharge(%this, %obj, %slot)
{
	//error("onCharge");
    %obj.sniperTarget = "";
    //%obj.setSniping(true);
}

function RedSniperRifle3Image::onFire(%this, %obj, %slot)
{
	//error("onFire");
    //%obj.setSniping(false);
}

function RedSniperRifle3Image::onNoAmmo(%this, %obj, %slot)
{
	//error("onNoAmmo");
    //%obj.setSniping(false);
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueSniperRifle3Image : RedSniperRifle3Image)
{
	shapeFile = "share/shapes/rotc/weapons/missilelauncher/image2.blue.dts";

	projectile = BlueSniperRifle3Projectile;
    missile = BlueSniperMissile;

	stateFireProjectile[5] = BlueSniperRifle3Projectile;
	stateEmitter[6] = BlueMissileLauncherFireEmitter;
	stateEmitter[7] = BlueMissileLauncherFireEmitter;
};

function BlueSniperRifle3Image::onMount(%this, %obj, %slot)
{
	RedSniperRifle3Image::onMount(%this, %obj, %slot);
}

function BlueSniperRifle3Image::setupHud(%this, %obj)
{
   RedSniperRifle3Image::setupHud(%this, %obj);
}

function BlueSniperRifle3Image::getBulletSpread(%this, %obj)
{
   RedSniperRifle3Image::getBulletSpread(%this, %obj);
}

function BlueSniperRifle3Image::onUnmount(%this, %obj, %slot)
{
	RedSniperRifle3Image::onUnmount(%this, %obj, %slot);
}

function BlueSniperRifle3Image::onReady(%this, %obj, %slot)
{
    RedSniperRifle3Image::onReady(%this, %obj, %slot);
}

function BlueSniperRifle3Image::onCharge(%this, %obj, %slot)
{
    RedSniperRifle3Image::onCharge(%this, %obj, %slot);
}

function BlueSniperRifle3Image::onFire(%this, %obj, %slot)
{
	RedSniperRifle3Image::onFire(%this, %obj, %slot);
}

function BlueSniperRifle3Image::onNoAmmo(%this, %obj, %slot)
{
    RedSniperRifle3Image::onNoAmmo(%this, %obj, %slot);
}

