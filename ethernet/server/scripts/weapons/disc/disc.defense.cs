//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - disc.defense.cs
// Code for the defensive disc image
//------------------------------------------------------------------------------

exec("./disc.interceptor.cs");

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedDefensiveDiscImage)
{
	// add the WeaponImage namespace as a parent...
	className = WeaponImage;

	// basic item properties
	shapeFile = "~/data/weapons/disc/image.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint = 1;
	offset = "0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	// targeting...
    targetingMask = $TypeMasks::ProjectileObjectType;
	targetingMaxDist = 250;
 
    // charging...
    minCharge = 0.4;

	// script fields...
	iconId  = 2;
	interceptor = RedInterceptorDisc;

	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                    = "Preactivate";
		stateTransitionOnLoaded[0]      = "Ready";
		stateTransitionOnNotLoaded[0]   = "NoAmmo";

		// activate...
		stateName[1]                    = "Activate";
		stateTransitionOnTimeout[1]     = "Ready";
		stateTimeoutValue[1]            = 0.3;
		stateSequence[1]                = "Invisible";
  
		// waiting for the trigger...
		stateName[2]                    = "Ready";
		stateTransitionOnNotLoaded[2]   = "Disabled";
		stateTransitionOnTarget[2]      = "Target";
		stateTarget[2]                  = true;
		stateSequence[2]                = "Invisible";

		// target...
		stateName[3]                      = "Target";
		//stateTransitionOnTriggerUp[3]     = "Ready";
		stateTransitionOnTargetAquired[3] = "Locked";
		stateTransitionOnNoTarget[3]      = "Ready";
		stateTarget[3]                    = true;
		stateSound[3]                     = DiscTargetSound;

		// target locked...
		stateName[4]                      = "Locked";
		stateTransitionOnTriggerDown[4]   = "SelectAction";
		stateTransitionOnNoTarget[4]      = "Ready";
		stateTarget[4]                    = true;
		stateSound[4]                     = DiscTargetAquiredSound;
  
        // select action...
		stateName[5]                    = "SelectAction";
		stateTransitionOnTimeout[5]     = "SelectDefense";
		stateScript[5]                  = "selectAction";
		stateFire[5]                    = true;

        // defend... ---------------------------------------------------------
		stateName[6]                    = "SelectDefense";
		stateTransitionOnFlag0Set[6]    = "DefensiveThrow";
		stateTransitionOnFlag0NotSet[6] = "Deflect";
  
            // throw interceptor... --------------------------------------------
    		stateName[7]                    = "DefensiveThrow";
    		stateTransitionOnTimeout[7]     = "AfterThrow";
    		stateTimeoutValue[7]            = 0.25;
    		stateAllowImageChange[7]        = false;
    		stateSequence[7]                = "Invisible";
    		stateSound[7]                   = DiscThrowSound;
    		stateScript[7]                  = "defensiveThrow";
      
            // deflect... ------------------------------------------------------
    		stateName[8]                    = "Deflect";
    		stateTransitionOnTimeout[8]     = "AfterDeflect";
    		stateTimeoutValue[8]            = 0.25;
    		stateAllowImageChange[8]        = false;
    		stateSound[8]                   = DiscThrowSound;
    		stateSequence[8]                = "AllVisible";
    		stateScript[8]                  = "deflect";

    		stateName[9]                    = "AfterDeflect";
    		stateTransitionOnTimeout[9]     = "Ready";
    		stateTimeoutValue[9]            = 0.00;
    		stateAllowImageChange[9]        = false;
    		stateSequence[9]                = "Invisible";
    		stateScript[9]                  = "afterDeflect";

        // ---------------------------------------------------------------------
		stateName[10]                    = "AfterThrow";
		stateTransitionOnTimeout[10]     = "NoAmmo";
		stateTimeoutValue[10]            = 0.0;
		stateAllowImageChange[10]        = false;
		stateSequence[10]                = "Invisible";
		stateScript[10]                  = "afterThrow";

		stateName[11]                    = "NoAmmo";
		stateTransitionOnAmmo[11]        = "Ready";
		stateTransitionOnTriggerDown[11] = "DryFire";
		stateSequence[11]                = "Invisible";

		stateName[12]                    = "DryFire";
		stateTransitionOnTriggerUp[12]   = "NoAmmo";
		stateSound[12]                   = WeaponEmptySound;

		stateName[13]                    = "Disabled";
		stateTransitionOnLoaded[13]      = "Ready";
		stateSequence[13]                = "Invisible";
	//
	// ...end of image states
	//-------------------------------------------------
};

function RedDefensiveDiscImage::onMount(%this, %obj, %slot)
{
	Parent::onMount(%this, %obj, %slot);
}

function RedDefensiveDiscImage::onUnmount(%this, %obj, %slot)
{
	Parent::onUnmount(%this, %obj, %slot);
}

function RedDefensiveDiscImage::selectAction(%this, %obj, %slot)
{
    // flag 0 set = throw interceptor
    // flag 0 not set = deflect

	%target = %obj.getImageTarget(%slot);

	// use world box center to calculate distance...
    %targetPos = %target.getWorldBoxCenter();
   	%playerPos = %obj.getWorldBoxCenter();
   	%vec = VectorSub(%targetPos, %playerPos);
   	%dist = VectorLen(%vec);
   	if(%dist < 12 && %target.getType() & $TypeMasks::ProjectileObjectType)
   	{
        // deflect!
        %obj.setImageFlag(%slot, 0, false);

        %force = 50;
        %vec = VectorScale(VectorNormalize(%vec), %force);
        %target.setDeflected(%vec);
    }
    else
    {
        // throw interceptor!
        %obj.interceptorTarget = %target;
        %obj.setImageFlag(%slot, 0, true);
    }
}

function RedDefensiveDiscImage::defensiveThrow(%this, %obj, %slot)
{
	// disable main weapon until we fired the disc...
	%obj.setImageLoaded(0, false);

	// play throw animation...
	%obj.setArmThread("look");
	%obj.playThread(0, "throwInterceptor");

	%projectile = %this.interceptor;

	// drain some energy...
	%obj.setEnergyLevel( %obj.getEnergyLevel() - %projectile.energyDrain );

	// determine muzzle-point...
	%muzzlePoint = %obj.getMuzzlePoint(%slot);

	// determine initial projectile velocity...
	%muzzleSpeed = %projectile.muzzleVelocity;

	%muzzleVector = %obj.getMuzzleVector(%slot);
	%objectVelocity = %obj.getVelocity();
	%muzzleVelocity = VectorAdd(
		VectorScale(%muzzleVector,  %muzzleSpeed),
		VectorScale(%objectVelocity, %projectile.velInheritFactor));

	// create the disc...
	%disc = new (NortDisc)() {
		dataBlock        = %projectile;
        teamId           = %obj.teamId;
		initialVelocity  = %muzzleVelocity;
		initialPosition  = %muzzlePoint;
		sourceObject     = %obj;
		sourceSlot       = %slot;
		client           = %obj.client;
	};
	MissionCleanup.add(%disc);

	// set disc target...
	%disc.setTarget(%obj.interceptorTarget);

	// clear out image target...
	%obj.setImageTarget(%slot, 0);

	%obj.decDiscs();

	return %disc;
}

function RedDefensiveDiscImage::deflect(%this, %obj, %slot)
{
	%obj.setImageLoaded(0, false); // disable main weapon

	// play deflect animation...
	%obj.setArmThread("discdeflect_left_base");
	%obj.playThread(0,"discdeflect_left_anim");

	// clear out image target...
	%obj.setImageTarget(%slot, 0);
}

function RedDefensiveDiscImage::afterDeflect(%this, %obj, %slot)
{
	// ensure disc is marked as loaded...
	%obj.setImageLoaded(%slot, true);

	// re-enable main weapon...
	%obj.setImageLoaded(0, true);
}

function RedDefensiveDiscImage::afterThrow(%this, %obj, %slot)
{
	// ensure disc is marked as loaded...
	%obj.setImageLoaded(%slot, true);

	// re-enable main weapon...
	%obj.setImageLoaded(0, true);
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueDefensiveDiscImage : RedDefensiveDiscImage)
{
	// basic item properties
	shapeFile = "~/data/weapons/disc/image.blue.dts";
	emap = true;

	// script fields...
	interceptor = BlueInterceptorDisc;
};

function BlueDefensiveDiscImage::onMount(%this, %obj, %slot)
{
	RedDefensiveDiscImage::onMount(%this,%obj,%slot);
}

function BlueDefensiveDiscImage::onUnmount(%this, %obj, %slot)
{
	RedDefensiveDiscImage::onUnmount(%this,%obj,%slot);
}

function BlueDefensiveDiscImage::selectAction(%this, %obj, %slot)
{
	RedDefensiveDiscImage::selectAction(%this,%obj,%slot);
}

function BlueDefensiveDiscImage::defensiveThrow(%this, %obj, %slot)
{
	RedDefensiveDiscImage::defensiveThrow(%this,%obj,%slot);
}

function BlueDefensiveDiscImage::deflect(%this, %obj, %slot)
{
	RedDefensiveDiscImage::deflect(%this,%obj,%slot);
}

function BlueDefensiveDiscImage::afterDeflect(%this, %obj, %slot)
{
	RedDefensiveDiscImage::afterDeflect(%this,%obj,%slot);
}

function BlueDefensiveDiscImage::afterThrow(%this, %obj, %slot)
{
	RedDefensiveDiscImage::afterThrow(%this, %obj, %slot);
}

