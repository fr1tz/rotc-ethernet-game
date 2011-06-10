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

//------------------------------------------------------------------------------

datablock TracerProjectileData(SeekerDiscPseudoProjectile)
{
	lifetime = 1000;
};

function SeekerDiscPseudoProjectile::onAdd(%this, %obj)
{
	%player = %obj.sourceObject;
	%slot = %obj.sourceSlot;

	%projectile = %player.getMountedImage(%slot).seeker;

	%target = %player.getCurrTarget();

	if(%target != 0
	&& %target.numAttackingDiscs() == 0
	&& !%target.hasBarrier())
	{
		// determine muzzle-point...
		%muzzlePoint = %player.getMuzzlePoint(%slot);

		// determine initial projectile velocity...
		%muzzleSpeed = %projectile.muzzleVelocity;

		%muzzleVector = %player.getMuzzleVector(%slot);
		%objectVelocity = %player.getVelocity();
		%muzzleVelocity = VectorAdd(
			VectorScale(%muzzleVector,  %muzzleSpeed),
			VectorScale(%objectVelocity, %projectile.velInheritFactor));

		// create the disc...
		%disc = new (NortDisc)() {
			dataBlock       = %projectile;
			teamId          = %player.teamId;
			initialVelocity = %muzzleVelocity;
			initialPosition = %muzzlePoint;
			sourceObject    = %player;
			sourceSlot      = %slot;
			client          = %player.client;
		};
		MissionCleanup.add(%disc);

		// set disc target...
		%disc.setTarget(%target);

		// clear out disc target...
		%player.clearDiscTarget();

		%player.decDiscs();
        
            %target.addAttackingDisc(%player);

		%player.playAudio(0, DiscThrowSound);

            if(%target.client)
                %target.client.play2D(DiscIncomingSound);
	}
	else
	{
		if(%player.client)
			%player.client.play2D(DiscSeekerDeniedSound);
	}

	// no need to ghost pseudo projectile to clients...
	%obj.delete();
}

//------------------------------------------------------------------------------

datablock TracerProjectileData(InterceptorDiscPseudoProjectile)
{
	lifetime = 1000;
};

function InterceptorDiscPseudoProjectile::onAdd(%this, %obj)
{
	%player = %obj.sourceObject;
	%slot = %obj.sourceSlot;

	%projectile = %player.getMountedImage(%slot).interceptor;

	// determine muzzle-point...
	%muzzlePoint = %player.getMuzzlePoint(%slot);

	// determine initial projectile velocity...
	%muzzleSpeed = %projectile.muzzleVelocity;

	%muzzleVector = %player.getMuzzleVector(%slot);
	%objectVelocity = %player.getVelocity();
	%muzzleVelocity = VectorAdd(
		VectorScale(%muzzleVector,  %muzzleSpeed),
		VectorScale(%objectVelocity, %projectile.velInheritFactor));

	// create the disc...
	%disc = new (NortDisc)() {
		dataBlock        = %projectile;
	      teamId           = %player.teamId;
		initialVelocity  = %muzzleVelocity;
		initialPosition  = %muzzlePoint;
		sourceObject     = %player;
		sourceSlot       = %slot;
		client           = %player.client;
	};
	MissionCleanup.add(%disc);

	// set disc target...
	%disc.setTarget(%obj.getTarget());

	// clear out image target...
	%player.setImageTarget(%slot, 0);

	%player.decDiscs();

	// no need to ghost pseudo projectile to clients...
	%obj.delete();
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedDiscImage)
{
	// add the WeaponImage namespace as a parent...
	className = WeaponImage;

	// basic item properties
    	shapeFile = "share/shapes/rotc/weapons/disc/image2.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint = 3;
	offset = "-0.15 -0.10 -0.05";
	rotation = "1 0 0 -12";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	projectile = InterceptorDiscPseudoProjectile;

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
		stateTransitionOnAmmo[0]        = "Ready";
		stateTransitionOnNoAmmo[0]      = "NoAmmo";

		// waiting for the trigger...
		stateName[1]                    = "Ready";
		stateTransitionOnNoAmmo[1]      = "NoAmmo";
		stateTransitionOnTriggerDown[1] = "SelectAction";
		stateTransitionOnTarget[1]      = "Target";
		stateTarget[1]                  = true;

		// target...
		stateName[2]                      = "Target";
		stateTransitionOnNoAmmo[2]        = "NoAmmo";
		stateTransitionOnTargetAquired[2] = "Locked";
		stateTransitionOnNoTarget[2]      = "Ready";
		stateTransitionOnTriggerDown[2]   = "SelectAction";
		stateTarget[2]                    = true;
		stateSound[2]                     = DiscTargetSound;

		// target locked...
		stateName[3]                      = "Locked";
		stateTransitionOnNoAmmo[3]        = "NoAmmo";
		stateTransitionOnTriggerDown[3]   = "SelectAction";
		stateTransitionOnNoTarget[3]      = "Ready";
		stateTarget[3]                    = true;
		stateSound[3]                     = DiscTargetAquiredSound;

		// select action...
		stateName[4]                    = "SelectAction";
		stateTransitionOnTarget[4]      = "Intercept";
		stateTransitionOnNoTarget[4]    = "Attack";
		stateFire[4]                    = true;

		// intercept...
    		stateName[5]                    = "Intercept";
    		stateTransitionOnTimeout[5]     = "Release";
    		stateTimeoutValue[5]            = 0.0;
		stateFireProjectile[5]          = InterceptorDiscPseudoProjectile;
    		stateSound[5]                   = DiscThrowSound;

		// attack...
    		stateName[6]                    = "Attack";
    		stateTransitionOnTimeout[6]     = "Release";
		stateFireProjectile[6]          = SeekerDiscPseudoProjectile;
    		stateTimeoutValue[6]            = 0.25;

		// release...
    		stateName[7]                    = "Release";
		stateTransitionOnTriggerUp[7]   = "Ready";
		stateTarget[7]                  = false;

		// no ammo...
		stateName[8]                    = "NoAmmo";
		stateTransitionOnAmmo[8]        = "Ready";
		stateTransitionOnTriggerDown[8] = "DryFire";
		stateTarget[8]                  = false;

		// dry fire...
		stateName[9]                    = "DryFire";
		stateTransitionOnTriggerUp[9]   = "NoAmmo";
		stateSound[9]                  = WeaponEmptySound;
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

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueDiscImage : RedDiscImage)
{
	// basic item properties
	//shapeFile = "share/shapes/rotc/weapons/disc/image.blue.dts";

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







