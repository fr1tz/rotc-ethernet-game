//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
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
	mountPoint  = 1;
	offset		= "0 0 0";
	rotation	 = "0 0 0";
	eyeOffset   = "0 0 0";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	// script fields...
	useGrenadeAmmo = true;
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
		stateName[3]                    = "Charge";
		stateTransitionOnTriggerUp[3]   = "Throw";
		stateCharge[3]                  = true;
		stateSound[3]                   = GrenadeChargeSound;
   		stateAllowImageChange[3]        = false;
		stateScript[3]                  = "onCharge";

        	// throw grenade...
       	stateName[5]                    = "Throw";
       	stateTransitionOnTimeout[5]     = "NoAmmo";
   		stateTimeoutValue[5]            = 0.0;
   		stateAllowImageChange[5]        = false;
		stateFire[5]                    = true;
		stateSound[5]                   = GrenadeThrowSound;
		stateScript[5]                  = "throw";

		// overcharge explosion...
   		stateName[6]                    = "Explode";
   		stateTransitionOnTimeout[6]     = "NoAmmo";
   		stateTimeoutValue[6]            = 0.0;
   		stateAllowImageChange[6]        = false;
		stateScript[6]                  = "explode";

		// no ammo...
		stateName[7]                    = "NoAmmo";
		stateTransitionOnAmmo[7]        = "Ready";
		stateTransitionOnTriggerDown[7] = "DryFire";
		stateSequence[7]                = "Invisible";
		stateScript[7]                  = "onNoAmmo";

		// dry fire...
		stateName[8]                    = "DryFire";
		stateTransitionOnTriggerUp[8]   = "NoAmmo";
		stateSound[8]                   = WeaponEmptySound;
		stateScript[8]                  = "onDryFire";

		// disabled...
		stateName[9]                    = "Disabled";
		stateTransitionOnLoaded[9]      = "Ready";
		stateSequence[9]                = "Invisible";
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
	%obj.fullForceGrenade = false;
	%obj.noGrenade = false;
}

function RedGrenadeImage::onCharge(%this, %obj, %slot)
{

}

function RedGrenadeImage::throw(%this, %obj, %slot)
{
	if(%obj.noGrenade)
		return;

	%obj.setArmThread("look");
	%obj.playThread(0, "throwInterceptor");
	%obj.shapeFxSetActive($PlayerShapeFxSlot::Charge, false, false);

	%projectile = %this.projectile;

	// drain some energy...
	%obj.setEnergyLevel( %obj.getEnergyLevel() - %projectile.energyDrain );

	// %throwForce is based on how long the trigger has been hold down...
	%throwCoefficient = 0;
	if(%obj.fullForceGrenade)
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

	%vec = %obj.getEyeVector();
	%vec = vectorScale(%vec, %throwForce);

	// add a vertical component to give the grenade a better arc
	%verticalForce = %throwForce / 8;
	%dot = vectorDot("0 0 1",%eye);
	if (%dot < 0) %dot = -%dot;
	%vec = vectorAdd(%vec,VectorScale("0 0 " @ %verticalForce,1 - %dot));

	// add velocity inherited from player...
	%vec = vectorAdd( %vec, VectorScale(%obj.getVelocity(), %projectile.velInheritFactor));

	// get initial position...
	%pos = %obj.getWorldBoxCenter();
	%pos = VectorAdd(%pos, "0 0 0.5");

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

	%obj.decGrenadeAmmo(1.0);

	return %grenade;
}

function RedGrenadeImage::onDryFire(%this, %obj, %slot)
{

}

function RedGrenadeImage::onNoAmmo(%this, %obj, %slot)
{

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

function BlueGrenadeImage::onCharge(%this, %obj, %slot)
{
    RedGrenadeImage::onCharge(%this, %obj, %slot);
}

function BlueGrenadeImage::throw(%this, %obj, %slot)
{
	RedGrenadeImage::throw(%this,%obj,%slot);
}

function BlueGrenadeImage::onDryFire(%this, %obj, %slot)
{
	RedGrenadeImage::onDryFire(%this, %obj, %slot);
}

function BlueGrenadeImage::onNoAmmo(%this, %obj, %slot)
{
	RedGrenadeImage::onNoAmmo(%this, %obj, %slot);
}
