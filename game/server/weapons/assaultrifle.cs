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
//exec("./assaultrifle.gfx.Red.cs");
//exec("./assaultrifle.gfx.Blue.cs");
exec("./assaultrifle.gfx.red.cs");
exec("./assaultrifle.gfx.blue.cs");

datablock TracerProjectileData(AssaultRiflePseudoProjectile)
{
	lifetime = 1000;

   // Keep these three in sync with the real projectiles down below!
	energyDrain = 16; 
	muzzleVelocity = 300;
	velInheritFactor = 0.75;
};

function AssaultRiflePseudoProjectile::onAdd(%this, %obj)
{
	%player = %obj.sourceObject;
	%slot = %obj.sourceSlot;
	%image = %player.getMountedImage(%slot);

	%muzzlePoint = %obj.initialPosition;
	%muzzleVector = %obj.initialVelocity;
	%muzzleTransform = createOrientFromDir(VectorNormalize(%muzzleVector));
	
	%pos[0] = "0 0 0";
	%vec[0] = "0 1 0.005";
	%pos[1] = "0 0 0.1";
	%vec[1] = "0 1 0.025";
	
	for(%i = 0; %i < 2; %i++)
	{
		%projectile = %image.fireprojectile[%i];
	
		%position =	VectorAdd(
			%muzzlePoint, 
			MatrixMulVector(%muzzleTransform, %pos[%i])
		);		
		%velocity = VectorScale(MatrixMulVector(%muzzleTransform, %vec[%i]), %this.muzzleVelocity);

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
        
	// no need to ghost pseudo projectile to clients...
	%obj.delete();
}

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ProjectileData(RedAssaultRifleProjectile1)
{
	stat = "br";

	// script damage properties...
	impactDamage       = 0;
	impactImpulse      = 2500;
	splashDamage       = 30;
	splashDamageRadius = 2;
	splashImpulse      = 0;
	bypassDamageBuffer = false;
	
	trackingAgility = 0;
	
	explodesNearEnemies			= true;
	explodesNearEnemiesRadius	= 2;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = AssaultRifleProjectileSound;
 
    projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.red.dts";

	explosion             = RedAssaultRifleProjectileImpact;
	bounceExplosion       = RedAssaultRifleProjectileBounceExplosion;
	hitEnemyExplosion     = RedAssaultRifleProjectileHit;
	nearEnemyExplosion    = RedAssaultRifleProjectileExplosion;
	hitTeammateExplosion  = RedAssaultRifleProjectileHit;
//	hitDeflectorExplosion = DiscDeflectedEffect;

	missEnemyEffectRadius = 10;
	missEnemyEffect = AssaultRifleProjectileMissedEnemyEffect;

	particleEmitter = RedAssaultRifleProjectileParticleEmitter;
	laserTrail[0]   = AssaultRifleProjectileLaserTrail;
//	laserTrail[1]   = RedAssaultRifleProjectileLaserTrail;
	laserTail	    = RedAssaultRifleProjectileLaserTail;
	laserTailLen    = 2;

	posOffset = "0 0 0";
	velOffset = "0 0.005";

   // Keep these three in sync with the pseudo projectile above!
	energyDrain = 16;
	muzzleVelocity	= 300; 
	velInheritFactor = 0.75; 
	
	isBallistic			= true;
	gravityMod			 = 5.0;

	armingDelay			= 0;
	lifetime				= 1000*5;
	fadeDelay			  = 5000;
	
	numBounces = 2;
	
	decals[0] = ExplosionDecalTwo;
	
	bounceDecals[0] = SmashDecalOne;
	bounceDecals[1] = SmashDecalTwo;
	bounceDecals[2] = SmashDecalThree;
	bounceDecals[3] = SmashDecalFour;
	
	hasLight	 = true;
	lightRadius = 8.0;
	lightColor  = "1.0 0.0 0.0";
};

datablock ProjectileData(RedAssaultRifleProjectile2 : RedAssaultRifleProjectile1)
{
	posOffset = "0 0 0.1";
	velOffset = "0 0.025";
	bounceDecals[0] = SmashDecalFive;
	bounceDecals[1] = SmashDecalSix;
	bounceDecals[2] = SmashDecalSeven;
	bounceDecals[3] = SmashDecalEight;	
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
	projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.blue.dts";    
	explosion = BlueAssaultRifleProjectileImpact;
	bounceExplosion = BlueAssaultRifleProjectileBounceExplosion;
	hitEnemyExplosion = BlueAssaultRifleProjectileHit;
	nearEnemyExplosion = BlueAssaultRifleProjectileExplosion;
	hitTeammateExplosion = BlueAssaultRifleProjectileHit;
	particleEmitter = BlueAssaultRifleProjectileParticleEmitter;	
	laserTail = BlueAssaultRifleProjectileLaserTail;
	lightColor  = "0.0 0.0 1.0";
};

datablock ProjectileData(BlueAssaultRifleProjectile2 : RedAssaultRifleProjectile2)
{
	projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.blue.dts";    
	explosion = BlueAssaultRifleProjectileImpact;
	bounceExplosion = BlueAssaultRifleProjectileBounceExplosion;
	hitEnemyExplosion = BlueAssaultRifleProjectileHit;
	nearEnemyExplosion = BlueAssaultRifleProjectileExplosion;
	hitTeammateExplosion = BlueAssaultRifleProjectileHit;
	particleEmitter = BlueAssaultRifleProjectileParticleEmitter;		
	laserTail = BlueAssaultRifleProjectileLaserTail;
	lightColor  = "0.0 0.0 1.0";	
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
	minEnergy = 16;

	projectile = AssaultRiflePseudoProjectile;

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
	fireprojectile[0] = RedAssaultRifleProjectile1;
	fireprojectile[1] = RedAssaultRifleProjectile2;

	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Activate";
		stateTransitionOnNoAmmo[0]		 = "NoAmmo";
		stateTimeoutValue[0]             = 0.25;
		stateSequence[0]                 = "idle";

		// when mounted...
		stateName[1]                     = "Activate";
		stateTransitionOnTimeout[1]      = "Ready";
		stateTimeoutValue[1]             = 0.25;
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
		stateFireProjectile[3]           = AssaultRiflePseudoProjectile;
		stateRecoil[3]                   = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimrifle";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = AssaultRifleFireSound;

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

function RedAssaultRifleImage::onMount(%this, %obj, %slot)
{
   Parent::onMount(%this, %obj, %slot);

   // Set up recoil
   %obj.setImageRecoilEnabled(%slot, true);
   %obj.setImageCurrentRecoil(%slot, 80);
   %obj.setImageMaxRecoil(%slot, 80);
   %obj.setImageRecoilAdd(%slot, 0);
   %obj.setImageRecoilDelta(%slot, -0);
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueAssaultRifleImage : RedAssaultRifleImage)
{
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image3.blue.dts";
	fireprojectile[0] = BlueAssaultRifleProjectile1;
	fireprojectile[1] = BlueAssaultRifleProjectile2;
	lightColor = "0 0.5 1";
	//stateFireProjectile[3] = BlueAssaultRifleProjectile1;
	//stateFireProjectile[8] = BlueAssaultRifleProjectile2;
};

function BlueAssaultRifleImage::onMount(%this, %obj, %slot)
{
    RedAssaultRifleImage::onMount(%this, %obj, %slot);
}

