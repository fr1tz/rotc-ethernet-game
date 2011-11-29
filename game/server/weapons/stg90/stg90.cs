//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./stg90.sfx.cs");
exec("./stg90.gfx.cs");
exec("./stg90.gfx.red.cs");
exec("./stg90.gfx.blue.cs");

datablock TracerProjectileData(STG90PseudoProjectile)
{
	energyDrain = 7;
	lifetime = 1000;
	muzzleVelocity = 1000;
	velInheritFactor = 0.0;
};

function STG90PseudoProjectile::onAdd(%this, %obj)
{
	%player = %obj.sourceObject;
	%slot = %obj.sourceSlot;
	%image = %player.getMountedImage(%slot);

	%muzzlePoint = %obj.initialPosition;
	%muzzleVector = %obj.initialVelocity;
	%muzzleTransform = createOrientFromDir(VectorNormalize(%muzzleVector));

   %spread = %image.getBulletSpread(%player);

   %player.firingInaccuracy += 5;
   if(%player.firingInaccuracy > 10)
      %player.firingInaccuracy = 10;

   %randX = %spread * ((getRandom(1000)-500)/1000);
   %randZ = %spread * ((getRandom(1000)-500)/1000);

	%pos[0] = "0 0 0";
	%vec[0] = %randX SPC "1" SPC %randZ;
	
	for(%i = 0; %i < 1; %i++)
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

datablock ProjectileData(RedSTG90Projectile)
{
	stat = "stg90";

	// script damage properties...
	impactDamage       = 25;
	impactImpulse      = 250;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = false;
	
	trackingAgility = 0;
	
	explodesNearEnemies			= false;
	explodesNearEnemiesRadius	= 2;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = STG90ProjectileSound;
 
   //projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.red.dts";

	explosion             = RedSTG90ProjectileImpact;
//	bounceExplosion       = RedSTG90ProjectileBounceExplosion;
	hitEnemyExplosion     = RedSTG90ProjectileHit;
//	nearEnemyExplosion    = RedSTG90ProjectileExplosion;
	hitTeammateExplosion  = RedSTG90ProjectileHit;
//	hitDeflectorExplosion = DiscDeflectedEffect;

	missEnemyEffectRadius = 10;
	missEnemyEffect = STG90ProjectileMissedEnemyEffect;

//	particleEmitter = RedSTG90ProjectileParticleEmitter;
//	laserTrail[0]   = STG90ProjectileLaserTrail;
//	laserTrail[1]   = RedSTG90ProjectileLaserTrail;
	laserTail	    = RedSTG90ProjectileLaserTail;
	laserTailLen    = 40;

	muzzleVelocity	= 0; // Handled by STG90PseudoProjectile
	velInheritFactor = 0; // Handled by STG90PseudoProjectile
	
	isBallistic			= false;
	gravityMod			 = 5.0;

	armingDelay			= 0;
	lifetime				= 1000;
	fadeDelay			= 500;
	
	numBounces = 0;
	
	decals[0] = BulletHoleDecalOne;
	
	bounceDecals[0] = SmashDecalOne;
	bounceDecals[1] = SmashDecalTwo;
	bounceDecals[2] = SmashDecalThree;
	bounceDecals[3] = SmashDecalFour;
	
	hasLight	 = true;
	lightRadius = 8.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedSTG90Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

   //%col.leak += 0.05;
}

//--------------------------------------------------------------------------

datablock ProjectileData(BlueSTG90Projectile : RedSTG90Projectile)
{
	//projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.blue.dts";
	explosion = BlueSTG90ProjectileImpact;
	hitEnemyExplosion = BlueSTG90ProjectileHit;
	hitTeammateExplosion = BlueSTG90ProjectileHit;
	laserTail = BlueSTG90ProjectileLaserTail;
	lightColor  = "0.0 0.0 1.0";
};

function BlueSTG90Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedSTG90Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedSTG90Image)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;
	
	// basic item properties
	shapeFile = "share/shapes/inf/stg90-0.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset		= "0 0 0";
	rotation	 = "0 0 0";
	eyeOffset	= "0.0 0.1 -0.12";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 7;

	projectile = STG90PseudoProjectile;

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
	fireprojectile[0] = RedSTG90Projectile;

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
		stateTransitionOnTimeout[3]      = "Cycle";
		stateTimeoutValue[3]             = 0.0;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = STG90PseudoProjectile;
		stateRecoil[3]                   = LightRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimrifle";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = STG90FireSound;

		stateName[8]                     = "Cycle";
		stateTransitionOnTriggerUp[3]    = "KeepAiming";
		stateTransitionOnNoAmmo[8]       = "NoAmmo";
		stateTransitionOnNotLoaded[8]    = "Disabled";
		stateTransitionOnTimeout[8]      = "Fire";
		stateWaitForTimeout[8]           = false;
		stateTimeoutValue[8]             = 0.12;

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
		stateSequence[5]                 = "idle";
  
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

function RedSTG90Image::getBulletSpread(%this, %obj)
{
   %a = VectorLen(%obj.getVelocity())*0.002;
   %b = %obj.firingInaccuracy/150;
   return 0.01+%a+%b;
}

function RedSTG90Image::onMount(%this, %obj, %slot)
{
   Parent::onMount(%this, %obj, %slot);

   // Set up recoil
   %obj.setImageRecoilEnabled(%slot, true);
   %obj.setImageCurrentRecoil(%slot, 0);
   %obj.setImageMaxRecoil(%slot, 100);
   %obj.setImageRecoilAdd(%slot, 5);
   %obj.setImageRecoilDelta(%slot, -50);

   // Set up crosshair
   %client = %obj.client;
   if(!isObject(%client)) return;
   commandToClient(%client, 'Crosshair', 0);
   commandToClient(%client, 'Crosshair', 2, 1);
   commandToClient(%client, 'Crosshair', 3, 2, 20);
   commandToClient(%client, 'Crosshair', 5, "./rotc/ch1");
   commandToClient(%client, 'Crosshair', 1);
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueSTG90Image : RedSTG90Image)
{
	shapeFile = "share/shapes/inf/stg90-0.blu.dts";
	fireprojectile[0] = BlueSTG90Projectile;
	lightColor = "0 0.5 1";
	//stateFireProjectile[3] = BlueSTG90Projectile1;
	//stateFireProjectile[8] = BlueSTG90Projectile2;
};

function BlueSTG90Image::getBulletSpread(%this, %obj)
{
   RedSTG90Image::getBulletSpread(%this, %obj);
}

function BlueSTG90Image::onMount(%this, %obj, %slot)
{
    RedSTG90Image::onMount(%this, %obj, %slot);
}


