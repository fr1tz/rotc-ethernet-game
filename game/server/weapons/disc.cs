//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - disc.cs
// Code for the disc
//------------------------------------------------------------------------------

exec("./disc.sfx.cs");
exec("./disc.seeker.cs");
exec("./disc.interceptor.cs");

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedDiscImage)
{
	// add the WeaponImage namespace as a parent...
	className = WeaponImage;

	// basic item properties
    shapeFile = "~/data/weapons/disc/image.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint = 3;
	offset = "-0.15 -0.22 -0.05";
    rotation = "1 0 0 -12";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	// targeting...
    targetingMask = $TargetingMask::Disc;
	targetingMaxDist = 250;

	// script fields...
	iconId  = 2;
	interceptor = RedInterceptorDisc;
    seeker = RedSeekerDisc;

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
		stateTransitionOnTriggerDown[2] = "SelectAction";
		stateTransitionOnTarget[2]      = "Target";
		stateTarget[2]                  = true;
		stateSequence[2]                = "Invisible";

		// target...
		stateName[3]                      = "Target";
		stateTransitionOnTargetAquired[3] = "Locked";
		stateTransitionOnNoTarget[3]      = "Ready";
		stateTransitionOnTriggerDown[3]   = "SelectAction";
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
		stateTransitionOnTimeout[5]     = "SelectMode";
		stateScript[5]                  = "selectAction";
		stateFire[5]                    = true;
  
        // select mode...
		stateName[6]                    = "SelectMode";
		stateTransitionOnFlag0Set[6]    = "SelectOffense";
		stateTransitionOnFlag0NotSet[6] = "SelectDefense";
  
        // attack... ---------------------------------------------------------
		stateName[7]                    = "SelectOffense";
		stateTransitionOnFlag1Set[7]    = "SeekerAttackFire";
		stateTransitionOnFlag1NotSet[7] = "SeekerAttackDenied";
  
            // seeker attack GO!... ----------------------------------------
       		stateName[8]                   = "SeekerAttackStart";
       		stateTransitionOnTimeout[8]    = "SeekerAttackFire";
       		stateTimeoutValue[8]           = 0.10;
       		stateAllowImageChange[8]       = false;
            stateSequence[8]               = "AllVisible";
            stateSound[8]                  = DiscThrowSound;
       		stateScript[8]                 = "seekerAttackStart";

            stateName[9]                   = "SeekerAttackFire";
       		stateTransitionOnTimeout[9]    = "AfterThrow";
       		stateFire[9]                   = true;
       		stateTimeoutValue[9]           = 0.25;
       		stateAllowImageChange[9]       = false;
            stateSequence[9]               = "Invisible";
            stateScript[9]                 = "seekerAttackFire";

            // seeker attack denied!... ----------------------------------------
            stateName[10]                   = "SeekerAttackDenied";
            stateTransitionOnTimeout[10]    = "AfterSeekerAttackDenied";
            stateTimeoutValue[10]           = 0.3;
            stateAllowImageChange[10]       = false;

            stateName[11]                   = "AfterSeekerAttackDenied";
            stateTransitionOnTimeout[11]    = "Ready";
            stateTimeoutValue[11]           = 0.00;
            stateAllowImageChange[11]       = false;
            stateScript[11]                 = "afterSeekerAttackDenied";

        // defend... ---------------------------------------------------------
		stateName[12]                    = "SelectDefense";
		stateTransitionOnFlag1Set[12]    = "ThrowInterceptor";
		stateTransitionOnFlag1NotSet[12] = "Deflect";

            // throw interceptor... --------------------------------------------
    		stateName[13]                    = "ThrowInterceptor";
    		stateTransitionOnTimeout[13]     = "AfterThrow";
    		stateTimeoutValue[13]            = 0.25;
    		stateAllowImageChange[13]        = false;
    		stateSequence[13]                = "Invisible";
    		stateSound[13]                   = DiscThrowSound;
    		stateScript[13]                  = "ThrowInterceptor";

            // deflect... ------------------------------------------------------
    		stateName[14]                    = "Deflect";
    		stateTransitionOnTimeout[14]     = "AfterDeflect";
    		stateTimeoutValue[14]            = 0.25;
    		stateAllowImageChange[14]        = false;
    		stateSound[14]                   = DiscThrowSound;
    		stateSequence[14]                = "AllVisible";
    		stateScript[14]                  = "deflect";

    		stateName[15]                    = "AfterDeflect";
    		stateTransitionOnTimeout[15]     = "Ready";
    		stateTimeoutValue[15]            = 0.00;
    		stateAllowImageChange[15]        = false;
    		stateSequence[15]                = "Invisible";
    		stateScript[15]                  = "afterDeflect";

        // ---------------------------------------------------------------------
		stateName[16]                    = "AfterThrow";
		stateTransitionOnTimeout[16]     = "NoAmmo";
		stateTimeoutValue[16]            = 0.0;
		stateAllowImageChange[16]        = false;
		stateSequence[16]                = "Invisible";
		stateScript[16]                  = "afterThrow";

		stateName[17]                    = "NoAmmo";
		stateTransitionOnAmmo[17]        = "Ready";
		stateTransitionOnTriggerDown[17] = "DryFire";
		stateSequence[17]                = "Invisible";

		stateName[18]                    = "DryFire";
		stateTransitionOnTriggerUp[18]   = "NoAmmo";
		stateSound[18]                   = WeaponEmptySound;

		stateName[19]                    = "Disabled";
		stateTransitionOnLoaded[19]      = "Ready";
		stateSequence[19]                = "Invisible";
	//
	// ...end of image states
	//-------------------------------------------------
};

function RedDiscImage::onMount(%this, %obj, %slot)
{
	Parent::onMount(%this, %obj, %slot);
}

function RedDiscImage::onUnmount(%this, %obj, %slot)
{
	Parent::onUnmount(%this, %obj, %slot);
}

function RedDiscImage::selectAction(%this, %obj, %slot)
{
    // flag 0 set = attack:
        // flag 1 set = do attack!
        // flag 1 not set = attack denied
    // flag 0 not set = defend:
        // flag 1 set = throw interceptor
        // flag 1 not set = deflect

	%target = %obj.getImageTarget(%slot);
 
    if(isObject(%target))
    {
        %obj.setImageFlag(%slot, 0, false); // defend
    
    	// use world box center to calculate distance...
        %targetPos = %target.getWorldBoxCenter();
       	%playerPos = %obj.getWorldBoxCenter();
       	%vec = VectorSub(%targetPos, %playerPos);
       	%dist = VectorLen(%vec);
       	if(false) //%dist < 12 && %target.getType() & $TypeMasks::ProjectileObjectType)
       	{
            // deflect!
            %obj.setImageFlag(%slot, 1, false);

            %force = 50;
            %vec = VectorScale(VectorNormalize(%vec), %force);
            %target.setDeflected(%vec);
        }
        else
        {
            // throw interceptor!
            %obj.interceptorTarget = %target;
            %obj.setImageFlag(%slot, 1, true);
        }
    }
    else
    {
        %obj.setImageFlag(%slot, 0, true); // attack
    
        %target = %obj.getCurrTarget();

        if(%target != 0
        && %target.numAttackingDiscs() == 0
        && !%target.inNoDiscGracePeriod())
        {
            %obj.seekerTarget = %target;
        
            %target.addAttackingDisc(%obj);
            %obj.setImageFlag(%slot, 1, true);

            if(%obj.seekerTarget.client)
                %obj.seekerTarget.client.play2D(DiscIncomingSound);
        }
        else
        {
            %obj.setImageFlag(%slot, 1, false);
            if(%obj.client)
                %obj.client.play2D(DiscSeekerDeniedSound);
        }
    }
}

function RedDiscImage::seekerAttackStart(%this, %obj, %slot)
{

}

function RedDiscImage::seekerAttackFire(%this, %obj, %slot)
{
	%projectile = %this.seeker;

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
		dataBlock		  = %projectile;
        teamId            = %obj.teamId;
		initialVelocity  = %muzzleVelocity;
		initialPosition  = %muzzlePoint;
		sourceObject	  = %obj;
		sourceSlot		 = %slot;
		client			  = %obj.client;
	};
	MissionCleanup.add(%disc);

	// set disc target...

	%disc.setTarget(%obj.seekerTarget);

	// clear out disc target...
	%obj.clearDiscTarget();

	%obj.decDiscs();

	return %disc;
}

function RedDiscImage::afterSeekerAttackDenied(%this, %obj, %slot)
{
	// ensure disc is marked as loaded...
	%obj.setImageLoaded(%slot, true);
}

function RedDiscImage::ThrowInterceptor(%this, %obj, %slot)
{
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

function RedDiscImage::deflect(%this, %obj, %slot)
{
	// clear out image target...
	%obj.setImageTarget(%slot, 0);
}

function RedDiscImage::afterDeflect(%this, %obj, %slot)
{
	// ensure disc is marked as loaded...
	%obj.setImageLoaded(%slot, true);
}

function RedDiscImage::afterThrow(%this, %obj, %slot)
{
	// ensure disc is marked as loaded...
	%obj.setImageLoaded(%slot, true);
}



//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueDiscImage : RedDiscImage)
{
	// basic item properties
	shapeFile = "~/data/weapons/disc/image.blue.dts";
	emap = true;

	// script fields...
	interceptor = BlueInterceptorDisc;
    seeker = BlueSeekerDisc;
};

function BlueDiscImage::onMount(%this, %obj, %slot)
{
	RedDiscImage::onMount(%this,%obj,%slot);
}

function BlueDiscImage::onUnmount(%this, %obj, %slot)
{
	RedDiscImage::onUnmount(%this,%obj,%slot);
}

function BlueDiscImage::selectAction(%this, %obj, %slot)
{
	RedDiscImage::selectAction(%this,%obj,%slot);
}

function BlueDiscImage::seekerAttackStart(%this, %obj, %slot)
{
	RedDiscImage::seekerAttackStart(%this, %obj, %slot);
}

function BlueDiscImage::seekerAttackFire(%this, %obj, %slot)
{
	RedDiscImage::seekerAttackFire(%this, %obj, %slot);
}

function BlueDiscImage::afterSeekerAttackDenied(%this, %obj, %slot)
{
	RedDiscImage::afterSeekerAttackDenied(%this, %obj, %slot);
}

function BlueDiscImage::ThrowInterceptor(%this, %obj, %slot)
{
	RedDiscImage::ThrowInterceptor(%this,%obj,%slot);
}

function BlueDiscImage::deflect(%this, %obj, %slot)
{
	RedDiscImage::deflect(%this,%obj,%slot);
}

function BlueDiscImage::afterDeflect(%this, %obj, %slot)
{
	RedDiscImage::afterDeflect(%this,%obj,%slot);
}

function BlueDiscImage::afterThrow(%this, %obj, %slot)
{
	RedDiscImage::afterThrow(%this, %obj, %slot);
}





