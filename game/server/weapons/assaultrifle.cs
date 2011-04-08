//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - assaultrifle.cs
// Code for the assault rifle
//------------------------------------------------------------------------------

exec("./assaultrifle.sfx.cs");
exec("./assaultrifle.gfx.cs");
exec("./assaultrifle.gfx.orange.cs");
exec("./assaultrifle.gfx.cyan.cs");

//-----------------------------------------------------------------------------

// This is not used in-game but makes sure that clients have the image shape
// file downloaded and ready when they try to load the actual image.
datablock ShapeBaseImageData(HolsteredAssaultRifleImage)
{
	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image2.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 2;
	offset = "0 0 0";
	
	stateName[0] = "DoNothing";
};

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ProjectileData(RedAssaultRifleProjectile1)
{
	stat = "br";

	// script damage properties...
	impactDamage       = 0;
	impactImpulse      = 2500;
	splashDamage       = 25;
	splashDamageRadius = 2;
	splashImpulse      = 0;
	bypassDamageBuffer = true;
	
	// how much energy does firing this projectile drain?...
	energyDrain = 8;

	trackingAgility = 0;
	
	explodesNearEnemies			= true;
	explodesNearEnemiesRadius	= 2;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = AssaultRifleProjectileFlybySound;
 
    projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.orange.dts";

	explosion             = OrangeAssaultRifleProjectileImpact;
	bounceExplosion       = OrangeAssaultRifleProjectileBounceExplosion;
	hitEnemyExplosion     = OrangeAssaultRifleProjectileHit;
	nearEnemyExplosion    = OrangeAssaultRifleProjectileExplosion;
	hitTeammateExplosion  = OrangeAssaultRifleProjectileHit;
//	hitDeflectorExplosion = DiscDeflectedEffect;

//   particleEmitter	= AssaultRifleProjectileParticleEmitter;
//	laserTrail[0]   = AssaultRifleProjectileLaserTrail;
//	laserTrail[1]   = AssaultRifleProjectileRedLaserTrail;
	laserTail	    = OrangeAssaultRifleProjectileLaserTail;
	laserTailLen    = 10;

	posOffset = "0 0 0";
	velOffset = "0 0.005";

	muzzleVelocity		= 300;
	velInheritFactor	 = 0.75;
	
	isBallistic			= true;
	gravityMod			 = 5.0;

	armingDelay			= 0;
	lifetime				= 1000*10;
	fadeDelay			  = 5000;
	
	numBounces = 2;
	
	decals[0]	= ExplosionDecalTwo;
	
	hasLight	 = true;
	lightRadius = 4.0;
	lightColor  = "1.0 0.8 0.2";
};

datablock ProjectileData(RedAssaultRifleProjectile2 : RedAssaultRifleProjectile1)
{
	posOffset = "0 0 0.1";
	velOffset = "0 0.025";
};

function RedAssaultRifleProjectile1::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

    %src =  %obj.getSourceObject();
    if(%src)
        %src.setDiscTarget(%col);
}

function RedAssaultRifleProjectile2::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedAssaultRifleProjectile1::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------

datablock ProjectileData(BlueAssaultRifleProjectile1 : RedAssaultRifleProjectile1)
{
	projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.cyan.dts";    
	explosion = CyanAssaultRifleProjectileImpact;
	bounceExplosion = CyanAssaultRifleProjectileBounceExplosion;
	hitEnemyExplosion = CyanAssaultRifleProjectileHit;
	nearEnemyExplosion = CyanAssaultRifleProjectileExplosion;
	hitTeammateExplosion = CyanAssaultRifleProjectileHit;	
	laserTail = CyanAssaultRifleProjectileLaserTail;
	lightColor  = "0.0 0.8 1.0";
};

datablock ProjectileData(BlueAssaultRifleProjectile2 : RedAssaultRifleProjectile2)
{
	projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.cyan.dts";    
	explosion = CyanAssaultRifleProjectileImpact;
	bounceExplosion = CyanAssaultRifleProjectileBounceExplosion;
	hitEnemyExplosion = CyanAssaultRifleProjectileHit;
	nearEnemyExplosion = CyanAssaultRifleProjectileExplosion;
	hitTeammateExplosion = CyanAssaultRifleProjectileHit;	
	laserTail = CyanAssaultRifleProjectileLaserTail;	
	lightColor  = "0.0 0.8 1.0";	
};

function BlueAssaultRifleProjectile1::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedAssaultRifleProjectile1::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function BlueAssaultRifleProjectile2::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedAssaultRifleProjectile1::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedAssaultRifleImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;
	
	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image3.orange.dts";
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

	projectile = RedAssaultRifleProjectile1;

	// light properties...
	lightType = "WeaponFireLight";
	lightColor = "1 0.5 0";
	lightTime = 100;
	lightRadius = 8;
	lightCastsShadows = false;
	lightAffectsShapes = true;

	// script fields...
	iconId = 7;
	mainWeapon = true;
	armThread  = "holdrifle";  // armThread to use when holding this weapon
	crosshair  = "assaultrifle"; // crosshair to display when holding this weapon

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
		stateTransitionOnTriggerDown[2]  = "Fire";
      stateArmThread[2]                = "holdrifle";
		stateSpinThread[2]               = "FullSpeed";
		stateSequence[2]                 = "idle";
		
		stateName[3]                     = "Fire";
		stateTransitionOnTriggerUp[3]    = "KeepAiming";
		//stateTimeoutValue[3]             = 0.0;
		stateFire[3]                     = true;
		//stateFireProjectile[3]           = RedAssaultRifleProjectile1;
		stateRecoil[3]                   = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimrifle";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = AssaultRifleFireSound;
		stateScript[3]                   = "onFire";

		stateName[8]                     = "Fire2";
		stateTransitionOnTriggerUp[8]    = "KeepAiming";
		stateTimeoutValue[8]             = 0.10;
		stateFireProjectile[8]           = RedAssaultRifleProjectile2;
		stateAllowImageChange[8]         = false;
		
		stateName[4]                     = "KeepAiming";
		stateTransitionOnNoAmmo[4]       = "NoAmmo";
		stateTransitionOnNotLoaded[4]    = "Disabled";
		stateTransitionOnTriggerDown[4]  = "Fire";
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

function RedAssaultRifleImage::onFire(%this, %obj, %slot)
{
	%projectile = %this.projectile;

	%objectVelocity = %obj.getVelocity();
	%muzzlePoint = %obj.getMuzzlePoint(%slot);
	%muzzleVector = %obj.getMuzzleVector(%slot);	
	%muzzleTransform = createOrientFromDir(%muzzleVector);
	
	%pos[0] = "0 0 0";
	%vec[0] = "0 1 0.005";
	%pos[1] = "0 0 0.1";
	%vec[1] = "0 1 0.025";
	
	for(%i = 0; %i < 2; %i++)
	{
      %position =	VectorAdd(
			%muzzlePoint, 
			MatrixMulVector(%muzzleTransform, %pos[%i])
		);		
		%velocity = VectorAdd(
			VectorScale(MatrixMulVector(%muzzleTransform, %vec[%i]), %projectile.muzzleVelocity),
			VectorScale(%objectVelocity, %projectile.velInheritFactor)
		);	      

		// create the projectile object...
		%p = new Projectile() {
			dataBlock       = %projectile;
			teamId          = %obj.teamId;
			initialVelocity = %velocity;
			initialPosition = %position;
			sourceObject    = %obj;
			sourceSlot      = %slot;
			client	    = %obj.client;
		};
		MissionCleanup.add(%p);

		%obj.setEnergyLevel(%obj.getEnergyLevel() - %projectile.energyDrain);
	 
		%target = %obj.getImageTarget(%slot);
		if(isObject(%target))
			%p.setTarget(%target);	
	}	
        
	return %p;
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueAssaultRifleImage : RedAssaultRifleImage)
{
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image3.cyan.dts";
	projectile = BlueAssaultRifleProjectile1;
	lightColor = "0 0.5 1";
	//stateFireProjectile[3] = BlueAssaultRifleProjectile1;
	//stateFireProjectile[8] = BlueAssaultRifleProjectile2;
};

function BlueAssaultRifleImage::onFire(%this, %obj, %slot)
{
	RedAssaultRifleImage::onFire(%this, %obj, %slot);
}

