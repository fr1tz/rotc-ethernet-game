//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - disc.offense.cs
// Code for the offsensive disc image
//------------------------------------------------------------------------------

exec("./disc.seeker.cs");
exec("./disc.grenade.cs");

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedOffensiveDiscImage)
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

    // charging...
    minCharge = 0.3;

	// script fields...
	iconId  = 2;
	seeker  = RedSeekerDisc;
    grenade = RedDiscGrenade;

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
		stateTransitionOnTriggerDown[2] = "SilentCharge";
		stateSequence[2]                = "Invisible";

		// silent charge...
		stateName[3]                    = "SilentCharge";
		stateTransitionOnTriggerUp[3]   = "SelectAction";
		stateTransitionOnCharged[3]     = "AudibleCharge";
		stateCharge[3]                  = true;

		// silent charge...
		stateName[4]                    = "AudibleCharge";
		stateTransitionOnTriggerUp[4]   = "SelectAction";
		stateCharge[4]                  = true;
		stateSound[4]                   = DiscChargeSound;

        // select action...
		stateName[5]                    = "SelectAction";
		stateTransitionOnTimeout[5]     = "SelectAttack";
		stateScript[5]                  = "selectAction";
		stateFire[5]                    = true;

        // attacks... ----------------------------------------------------------
		stateName[6]                    = "SelectAttack";
		stateTransitionOnCharged[6]     = "GrenadeAttackStart";
		stateTransitionOnNotCharged[6]  = "SeekerAttackCheck";
  
             // grenade attack... ----------------------------------------
        	stateName[7]                    = "GrenadeAttackStart";
        	stateTransitionOnTimeout[7]     = "GrenadeAttackFire";
       		stateTimeoutValue[7]            = 0.10;
       		stateAllowImageChange[7]        = false;
       		stateSequence[7]                = "AllVisible";
            stateSound[7]                   = DiscGrenadeThrowSound;
       		stateScript[7]                  = "grenadeAttackStart";

       		stateName[8]                    = "GrenadeAttackFire";
       		stateTransitionOnTimeout[8]     = "AfterThrow";
       		stateFire[8]                    = true;
       		stateTimeoutValue[8]            = 0.25;
       		stateAllowImageChange[8]        = false;
            stateSequence[8]                = "Invisible";
       		stateScript[8]                  = "grenadeAttackFire";

            // seeker attack... ------------------------------------------------
    		stateName[9]                    = "SeekerAttackCheck";
    		stateTransitionOnFlag0Set[9]    = "SeekerAttackStart";
    		stateTransitionOnFlag0NotSet[9] = "SeekerAttackDenied";

                // seeker attack GO!... ----------------------------------------
        		stateName[10]                   = "SeekerAttackStart";
        		stateTransitionOnTimeout[10]    = "SeekerAttackFire";
        		stateTimeoutValue[10]           = 0.10;
        		stateAllowImageChange[10]       = false;
        		stateSequence[10]               = "AllVisible";
                stateSound[10]                  = DiscThrowSound;
        		stateScript[10]                 = "seekerAttackStart";

        		stateName[11]                   = "SeekerAttackFire";
        		stateTransitionOnTimeout[11]    = "AfterThrow";
        		stateFire[11]                   = true;
        		stateTimeoutValue[11]           = 0.25;
        		stateAllowImageChange[11]       = false;
                stateSequence[11]               = "Invisible";
        		stateScript[11]                 = "seekerAttackFire";
          
                // seeker attack denied!... ----------------------------------------
        		stateName[12]                   = "SeekerAttackDenied";
        		stateTransitionOnTimeout[12]    = "AfterSeekerAttackDenied";
        		stateTimeoutValue[12]           = 0.3;
        		stateAllowImageChange[12]       = false;
          
        		stateName[13]                   = "AfterSeekerAttackDenied";
        		stateTransitionOnTimeout[13]    = "Ready";
        		stateTimeoutValue[13]           = 0.00;
        		stateAllowImageChange[13]       = false;
        		stateScript[13]                 = "afterSeekerAttackDenied";

        // ---------------------------------------------------------------------
		stateName[14]                    = "AfterThrow";
		stateTransitionOnTimeout[14]     = "NoAmmo";
		stateTimeoutValue[14]            = 0.0;
		stateAllowImageChange[14]        = false;
		stateSequence[14]                = "Invisible";
		stateScript[14]                  = "afterThrow";

		stateName[15]                    = "NoAmmo";
		stateTransitionOnAmmo[15]        = "Ready";
		stateTransitionOnTriggerDown[15] = "DryFire";
		stateSequence[15]                = "Invisible";

		stateName[16]                    = "DryFire";
		stateTransitionOnTriggerUp[16]   = "NoAmmo";
		stateSound[16]                   = WeaponEmptySound;

		stateName[17]                    = "Disabled";
		stateTransitionOnLoaded[17]      = "Ready";
		stateSequence[17]                = "Invisible";
	//
	// ...end of image states
	//-------------------------------------------------
};

function RedOffensiveDiscImage::onMount(%this, %obj, %slot)
{
	Parent::onMount(%this, %obj, %slot);
}

function RedOffensiveDiscImage::onUnmount(%this, %obj, %slot)
{
	Parent::onUnmount(%this, %obj, %slot);
}

function RedOffensiveDiscImage::selectAction(%this, %obj, %slot)
{
    // charged = throw grenade
    // not charged, flag 0 set = throw seeker
    // not charged, flag 0 not set = unable to throw seeker

    //echo(%obj.getImageCharge(%slot) SPC   %this.minCharge );
    if(%obj.getImageCharge(%slot) >= %this.minCharge)
    {

    }
    else
    {
        %target = %obj.getCurrTarget();
        //echo(%target);
        %obj.setImageFlag(%slot, 0, %target != 0);
        %obj.seekerTarget = %target;

        if(%target == 0)
            %obj.client.play2D(DiscSeekerDeniedSound);
        else if(%target.client)
            %target.client.play2D(DiscIncomingSound);
	}
}

function RedOffensiveDiscImage::grenadeAttackStart(%this, %obj, %slot)
{
	// disable main weapon until we fired the disc...
	%obj.setImageLoaded(0, false);

	// play throw animation...
	%obj.setArmThread("look");
	//%obj.playThread(0, "throwSidearm");
    %obj.playThread(0, "throwInterceptor");
}

function RedOffensiveDiscImage::grenadeAttackFire(%this, %obj, %slot)
{
	%projectile = %this.grenade;

	// drain some energy...
	%obj.setEnergyLevel( %obj.getEnergyLevel() - %projectile.energyDrain );

    // %throwForce is based on how long the trigger has been hold down...
    %throwCoefficient = %obj.getImageCharge(%slot);
    if( %throwCoefficient > 1 ) %throwCoefficient = 1;
    //%throwCoefficient = %throwCoefficient/2;
    %throwForce = %projectile.muzzleVelocity * %throwCoefficient;

    %vec = %obj.getMuzzleVector(%slot);
    %vec = vectorScale(%vec, %throwForce);

    // add a vertical component to give the grenade a better arc
    %verticalForce = %throwForce / 8;
    %dot = vectorDot("0 0 1",%eye);
    if (%dot < 0) %dot = -%dot;
    %vec = vectorAdd(%vec,VectorScale("0 0 " @ %verticalForce,1 - %dot));

    // add velocity inherited from player...
    %vec = vectorAdd( %vec, VectorScale(%obj.getVelocity(), %projectile.velInheritFactor));

    // get initial position...
    %pos = %obj.getMuzzlePoint(%slot);

	// create the disc...
	%disc = new (Projectile)() {
		dataBlock        = %projectile;
        teamId           = %obj.teamId;
		initialVelocity  = %vec;
		initialPosition  = %pos;
		sourceObject     = %obj;
		sourceSlot       = %slot;
		client           = %obj.client;
	};
	MissionCleanup.add(%disc);

    //%disc.schedule(2000,"explode");

	%obj.decDiscs();

	return %disc;
}

function RedOffensiveDiscImage::seekerAttackStart(%this, %obj, %slot)
{
	// disable main weapon until we fired the disc...
	%obj.setImageLoaded(0, false);

	// set shape target...
	//%target = %obj.seekerTarget;
	//%pos = %target.getWorldBoxCenter();
	//%obj.setCurrTarget(%target, %pos);
 
	// play throw animation...
	%obj.setArmThread("look");
	//%obj.playThread(0, "throwSidearm");
    %obj.playThread(0, "throwInterceptor");

	// target is being attacked by disc...
//	%target.attackedByDisc = true;

	// inform target he's about to get attacked...
//	if(isObject(%target.client) && %target == %target.client.player)
//		%target.client.play2D(SeekerDiscLockedSound);
}

function RedOffensiveDiscImage::seekerAttackFire(%this, %obj, %slot)
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

function RedOffensiveDiscImage::afterSeekerAttackDenied(%this, %obj, %slot)
{
	// ensure disc is marked as loaded...
	%obj.setImageLoaded(%slot, true);
}

function RedOffensiveDiscImage::afterThrow(%this, %obj, %slot)
{
	// ensure disc is marked as loaded...
	%obj.setImageLoaded(%slot, true);

	// re-enable main weapon...
	%obj.setImageLoaded(0, true);
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueOffensiveDiscImage : RedOffensiveDiscImage)
{
	// basic item properties
	shapeFile = "~/data/weapons/disc/image.blue.dts";
	emap = true;

	// script fields...
	seeker  = BlueSeekerDisc;
    grenade = BlueDiscGrenade;
};

function BlueOffensiveDiscImage::onMount(%this, %obj, %slot)
{
	RedOffensiveDiscImage::onMount(%this,%obj,%slot);
}

function BlueOffensiveDiscImage::onUnmount(%this, %obj, %slot)
{
	RedOffensiveDiscImage::onUnmount(%this,%obj,%slot);
}

function BlueOffensiveDiscImage::selectAction(%this, %obj, %slot)
{
	RedOffensiveDiscImage::selectAction(%this,%obj,%slot);
}

function BlueOffensiveDiscImage::grenadeAttackStart(%this, %obj, %slot)
{
	RedOffensiveDiscImage::grenadeAttackStart(%this,%obj,%slot);
}

function BlueOffensiveDiscImage::grenadeAttackFire(%this, %obj, %slot)
{
	RedOffensiveDiscImage::grenadeAttackFire(%this, %obj, %slot);
}

function BlueOffensiveDiscImage::seekerAttackStart(%this, %obj, %slot)
{
	RedOffensiveDiscImage::seekerAttackStart(%this, %obj, %slot);
}

function BlueOffensiveDiscImage::seekerAttackFire(%this, %obj, %slot)
{
	RedOffensiveDiscImage::seekerAttackFire(%this, %obj, %slot);
}

function BlueOffensiveDiscImage::afterSeekerAttackDenied(%this, %obj, %slot)
{
	RedOffensiveDiscImage::afterSeekerAttackDenied(%this, %obj, %slot);
}

function BlueOffensiveDiscImage::afterThrow(%this, %obj, %slot)
{
	RedOffensiveDiscImage::afterThrow(%this, %obj, %slot);
}

