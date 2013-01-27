//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - grenade.cs
// Code for the grenade image
//------------------------------------------------------------------------------

exec("./grenade.sfx.cs");
exec("./grenade.projectile.cs");

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedGrenadeImage)
{
	// add the WeaponImage namespace as a parent...
	className = WeaponImage;

	// basic item properties
	shapeFile = "share/shapes/rotc/misc/nothing.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint = 1;
	offset = "0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	// script fields...
    projectile = RedGrenade;
	iconId  = 2;

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
		stateTransitionOnTriggerDown[2] = "Charge";
		stateSequence[2]                = "Invisible";
		stateScript[2]                  = "onReady";

		// charge...
		stateName[4]                    = "Charge";
		stateTransitionOnTriggerUp[4]   = "GrenadeAttackStart";
		stateCharge[4]                  = true;
		stateSound[4]                   = GrenadeChargeSound;

        // grenade attack...
       	stateName[7]                    = "GrenadeAttackStart";
       	stateTransitionOnTimeout[7]     = "GrenadeAttackFire";
   		stateTimeoutValue[7]            = 0.10;
   		stateAllowImageChange[7]        = false;
   		stateSequence[7]                = "AllVisible";
        stateSound[7]                   = GrenadeThrowSound;
        stateScript[7]                  = "grenadeAttackStart";
        stateFire[7]                    = true;

   		stateName[8]                    = "GrenadeAttackFire";
   		stateTransitionOnTimeout[8]     = "AfterThrow";
   		stateFire[8]                    = true;
   		stateTimeoutValue[8]            = 0.25;
   		stateAllowImageChange[8]        = false;
        stateSequence[8]                = "Invisible";
        stateScript[8]                  = "grenadeAttackFire";

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

function RedGrenadeImage::onMount(%this, %obj, %slot)
{
	Parent::onMount(%this, %obj, %slot);
}

function RedGrenadeImage::onUnmount(%this, %obj, %slot)
{
	Parent::onUnmount(%this, %obj, %slot);
}

function RedGrenadeImage::onReady(%this, %obj, %slot)
{
    %obj.doInstantGrenadeThrow = false;
}

function RedGrenadeImage::grenadeAttackStart(%this, %obj, %slot)
{
	// disable main weapon until we threw the grenade...
	%obj.setImageLoaded(0, false);

	// play throw animation...
	%obj.setArmThread("look");
	//%obj.playThread(0, "throwSidearm");
    %obj.playThread(0, "throwInterceptor");
}

function RedGrenadeImage::grenadeAttackFire(%this, %obj, %slot)
{
	%projectile = %this.projectile;

	// drain some energy...
	%obj.setEnergyLevel( %obj.getEnergyLevel() - %projectile.energyDrain );

    // %throwForce is based on how long the trigger has been hold down...
    %throwCoefficient = 0;
    if(%obj.doInstantGrenadeThrow)
    {
        %throwCoefficient = 1;
    }
    else
    {
        %throwCoefficient = %obj.getImageCharge(%slot);
        if( %throwCoefficient > 1 )
            %throwCoefficient = 1;
    }
    //%throwCoefficient = %throwCoefficient/2;
    %throwForce = %projectile.muzzleVelocity * %throwCoefficient;

    %vec = %obj.getMuzzleVector(0);
    %vec = vectorScale(%vec, %throwForce);

    // add a vertical component to give the grenade a better arc
    %verticalForce = %throwForce / 8;
    %dot = vectorDot("0 0 1",%eye);
    if (%dot < 0) %dot = -%dot;
    %vec = vectorAdd(%vec,VectorScale("0 0 " @ %verticalForce,1 - %dot));

    // add velocity inherited from player...
    %vec = vectorAdd( %vec, VectorScale(%obj.getVelocity(), %projectile.velInheritFactor));

    // get initial position...
    %pos = %obj.getMuzzlePoint(0);

	// create the grenade...
	%grenade = new (Projectile)() {
		dataBlock        = %projectile;
        teamId           = %obj.teamId;
		initialVelocity  = %vec;
		initialPosition  = %pos;
		sourceObject     = %obj;
		sourceSlot       = %slot;
		client           = %obj.client;
	};
	MissionCleanup.add(%grenade);

    //%disc.schedule(2000,"explode");

	%obj.decGrenadeAmmo(1.0);

	return %grenade;
}

function RedGrenadeImage::afterThrow(%this, %obj, %slot)
{
	// ensure image is marked as loaded...
	%obj.setImageLoaded(%slot, true);

	// re-enable main weapon...
	%obj.setImageLoaded(0, true);
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueGrenadeImage : RedGrenadeImage)
{
    projectile = BlueGrenade;
};

function BlueGrenadeImage::onMount(%this, %obj, %slot)
{
	RedGrenadeImage::onMount(%this,%obj,%slot);
}

function BlueGrenadeImage::onUnmount(%this, %obj, %slot)
{
	RedGrenadeImage::onUnmount(%this,%obj,%slot);
}

function BlueGrenadeImage::onReady(%this, %obj, %slot)
{
    RedGrenadeImage::onReady(%this, %obj, %slot);
}

function BlueGrenadeImage::grenadeAttackStart(%this, %obj, %slot)
{
	RedGrenadeImage::grenadeAttackStart(%this,%obj,%slot);
}

function BlueGrenadeImage::grenadeAttackFire(%this, %obj, %slot)
{
	RedGrenadeImage::grenadeAttackFire(%this, %obj, %slot);
}

function BlueGrenadeImage::afterThrow(%this, %obj, %slot)
{
	RedGrenadeImage::afterThrow(%this, %obj, %slot);
}

