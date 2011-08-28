//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./rifle1.sfx.cs");
exec("./rifle1.gfx.cs");
exec("./rifle1.gfx.red.cs");
exec("./rifle1.gfx.blue.cs");

datablock TracerProjectileData(Rifle1PseudoProjectile)
{
	energyDrain = 15;
	lifetime = 1000;
	muzzleVelocity = 1500;
	velInheritFactor = 0.75;
};

function Rifle1PseudoProjectile::onAdd(%this, %obj)
{
	%player = %obj.sourceObject;
	%slot = %obj.sourceSlot;
	%image = %player.getMountedImage(%slot);

	%muzzlePoint = %obj.initialPosition;
	%muzzleVector = %obj.initialVelocity;

	//
	%accuracy = %player.getImageCharge(%slot) / 2;
	if(%accuracy > 1)
		%accuracy = 1;
	%inaccuracy = 0.5 + VectorLen(%player.getVelocity()) / 10;
	%a = 1-%accuracy;
	if(%a < 0) 
		%a = 0;
	error(%inaccuracy);
	%s = 0.05 * %a;
	%p = VectorAdd(%muzzlePoint, %muzzleVector);
	%r = %s * %this.muzzleVelocity;
	for(%i = 0; %i < 3; %i++)
	{
		%rand = getRandom(100)-50;
		if(%rand == 0)
			%newpos[%i] = getWord(%p, %i);
		else
			%newpos[%i] = getWord(%p, %i) + %r*(%rand/50);		
	}
	%muzzleVector = VectorSub(%newpos[0] SPC %newpos[1] SPC %newpos[2], %muzzlePoint);
	%muzzleVector = VectorNormalize(%muzzleVector );

	for(%i = 0; %i < 1; %i++)
	{
		%projectile = %image.fireprojectile[%i];
	
		%position = %muzzlePoint;
		%velocity = VectorScale(%muzzleVector, %this.muzzleVelocity);

		// create the projectile object...
		%p = new Projectile() {
			dataBlock       = %projectile;
			teamId          = %obj.teamId;
			initialVelocity = %velocity;
			initialPosition = %position;
			sourceObject    = %player;
			sourceSlot      = %slot;
			client	        = %obj.client;
		};
		MissionCleanup.add(%p);
	}	

    %player.setSniping(false);
      
	// no need to ghost pseudo projectile to clients...
	%obj.delete();
}

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ProjectileData(RedRifle1Projectile)
{
	stat = "r1";

	// script damage properties...
	impactDamage       = 25;
	impactImpulse      = 500;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = false;
	
	trackingAgility = 0;
	
	explodesNearEnemies			= false;
	explodesNearEnemiesRadius	= 2;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = Rifle1ProjectileSound;
 
//	projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.red.dts";

	explosion             = RedRifle1ProjectileImpact;
//	bounceExplosion       = RedRifle1ProjectileBounceExplosion;
	hitEnemyExplosion     = RedRifle1ProjectileHit;
//	nearEnemyExplosion    = RedRifle1ProjectileExplosion;
	hitTeammateExplosion  = RedRifle1ProjectileHit;
//	hitDeflectorExplosion = DiscDeflectedEffect;

	missEnemyEffectRadius = 10;
	missEnemyEffect = Rifle1ProjectileMissedEnemyEffect;

//   particleEmitter	= Rifle1ProjectileParticleEmitter;
//	laserTrail[0]   = RedRifle1ProjectileLaserTrail;
//	laserTrail[1]   = Rifle1ProjectileRedLaserTrail;
	laserTail	    = RedRifle1ProjectileLaserTail;
	laserTailLen    = 25;

	muzzleVelocity = 0; // Handled by Rifle1PseudoProjectile
	velInheritFactor = 0; // Handled by Rifle1PseudoProjectile
	
	isBallistic			= true;
	gravityMod			 = 5.0;

	armingDelay			= 0;
	lifetime				= 1000*5;
	fadeDelay			  = 5000;
	
	numBounces = 0;

	decals[0] = BulletHoleDecalTwo;
	
	hasLight	 = true;
	lightRadius = 8.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedRifle1Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

//    %src =  %obj.getSourceObject();
//    if(%src)
//        %src.setDiscTarget(%col);
}

//--------------------------------------------------------------------------

datablock ProjectileData(BlueRifle1Projectile : RedRifle1Projectile)
{
//	projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.blue.dts";    
	explosion = BlueRifle1ProjectileImpact;
	hitEnemyExplosion = BlueRifle1ProjectileHit;
	hitTeammateExplosion = BlueRifle1ProjectileHit;	
	laserTail = BlueRifle1ProjectileLaserTail;
	lightColor  = "0.0 0.0 1.0";
};

function BlueRifle1Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedRifle1Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedRifle1Image)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;
	
	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image3.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset		= "0 0 0";
	rotation	 = "0 0 0";
	eyeOffset	= "0.275 0.1 -0.05";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 15;

    // charging...
    minCharge = 0.1;

	projectile = Rifle1PseudoProjectile;

	// light properties...
	lightType = "WeaponFireLight";
	lightColor = "1 0.5 0";
	lightTime = 100;
	lightRadius = 15;
	lightCastsShadows = false;
	lightAffectsShapes = true;

	// script fields...
	iconId = 7;
	mainWeapon = true;
	armThread  = "holdrifle";  // armThread to use when holding this weapon
	crosshair  = "assaultrifle"; // crosshair to display when holding this weapon
	fireprojectile[0] = RedRifle1Projectile;
	fireprojectile[1] = RedRifle1Projectile2;

	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Activate";
		stateTransitionOnNoAmmo[0]		 = "NoAmmo";

		// when mounted...
		stateName[1]                     = "Activate";
		stateTransitionOnTimeout[1]      = "Ready";
		stateTimeoutValue[1]             = 0.5;
		stateSequence[1]                 = "idle";
		stateSpinThread[1]               = "SpinUp";

		// ready to fire, just waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnNoAmmo[2]       = "NoAmmo";
  		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Aim";
		stateArmThread[2]                = "holdrifle";
		stateSpinThread[2]               = "FullSpeed";
		stateSequence[2]                 = "idle";

		stateName[8]                     = "Aim";
		stateTransitionOnTriggerUp[8]    = "Fire";
//		stateTransitionOnNoAmmo[8]       = "NoAmmo";
		stateCharge[8]                   = true;
		stateAllowImageChange[8]         = true;
		stateArmThread[8]                = "aimrifle";
		//stateSequence[8]               = "raisecharge";
		stateSound[8]                    = Rifle1AimingSound;
		
		stateName[3]                     = "Fire";
		stateTransitionOnTimeout[3]      = "KeepAiming";
		stateTimeoutValue[3]             = 0.0;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = Rifle1PseudoProjectile;
//		stateRecoil[3]                 = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimrifle";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = Rifle1FireSound;

		stateName[4]                     = "KeepAiming";
		stateTransitionOnNoAmmo[4]       = "NoAmmo";
		stateTransitionOnNotLoaded[4]    = "Disabled";
		stateTransitionOnTriggerDown[4]  = "Aim";
		stateTransitionOnTimeout[4]      = "Ready";
		stateWaitForTimeout[4]           = false;
		stateTimeoutValue[4]             = 2.00;

        // no ammo...
		stateName[5]                     = "NoAmmo";
		stateTransitionOnAmmo[5]         = "Ready";
        stateTransitionOnTriggerDown[5]  = "DryFire";
		stateTimeoutValue[5]             = 0.50;

        // dry fire...
		stateName[6]                     = "DryFire";
		stateTransitionOnTriggerUp[6]    = "NoAmmo";
		stateSound[6]                    = WeaponEmptySound;
  
		// disabled...
		stateName[7]                     = "Disabled";
		stateTransitionOnLoaded[7]       = "Ready";
		stateAllowImageChange[7]         = false;
	//
	// ...end of image states
	//-------------------------------------------------
};

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueRifle1Image : RedRifle1Image)
{
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image3.blue.dts";
	fireprojectile[0] = BlueRifle1Projectile;
	fireprojectile[1] = BlueRifle1Projectile2;
	lightColor = "0 0.5 1";
	//stateFireProjectile[3] = BlueRifle1Projectile;
	//stateFireProjectile[8] = BlueRifle1Projectile2;
};

