//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./ff3.sfx.cs");
exec("./ff3.gfx.cs");
exec("./ff3.gfx.red.cs");
exec("./ff3.gfx.blue.cs");

datablock TracerProjectileData(FF3PseudoProjectile)
{
	energyDrain = 30;
	lifetime = 1000;
	muzzleVelocity = 300;
	velInheritFactor = 0.0;
};

function FF3PseudoProjectile::onAdd(%this, %obj)
{
	%player = %obj.sourceObject;
	%slot = %obj.sourceSlot;
	%image = %player.getMountedImage(%slot);

	%muzzlePoint = %obj.initialPosition;
	%muzzleVector = %obj.initialVelocity;
	%muzzleTransform = createOrientFromDir(VectorNormalize(%muzzleVector));

   %spread = %image.getBulletSpread(%player);

   %randX = %spread * ((getRandom(1000)-500)/1000);
   %randZ = %spread * ((getRandom(1000)-500)/1000);

   %pos[0] = "0 0 0";
   %vec[0] = %randX SPC "1" SPC %randZ;

	%position =	VectorAdd(
		%muzzlePoint,
		MatrixMulVector(%muzzleTransform, %pos[0])
	);
	%muzzleVec = MatrixMulVector(%muzzleTransform, %vec[0]);

   %projectile = %image.fireprojectile[0];

	for(%i = 0; %i < 8; %i++)
	{
      %spread = 0.05;
      %randX = %spread * ((getRandom(1000)-500)/1000);
      %randZ = %spread * ((getRandom(1000)-500)/1000);
      %vec = %randX SPC "1" SPC %randZ;
      %mat = createOrientFromDir(VectorNormalize(%muzzleVec));
      %vel = VectorScale(MatrixMulVector(%mat, %vec), %this.muzzleVelocity);

		// create the projectile object...
		%p = new Projectile() {
			dataBlock       = %projectile;
			teamId          = %obj.teamId;
			initialVelocity = %vel;
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

datablock ProjectileData(RedFF3Projectile)
{
	stat = "ff3";

	// script damage properties...
	impactDamage       = 15;
	impactImpulse      = 250;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = false;

	trackingAgility = 0;

	explodesNearEnemies			= false;
	explodesNearEnemiesRadius	= 2;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = FF3ProjectileSound;

   projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.red.dts";

	explosion             = RedFF3ProjectileExplosion;
//	bounceExplosion       = RedFF3ProjectileBounceExplosion;
	hitEnemyExplosion     = RedFF3ProjectileHit;
//	nearEnemyExplosion    = RedFF3ProjectileExplosion;
	hitTeammateExplosion  = RedFF3ProjectileHit;
//	hitDeflectorExplosion = DiscDeflectedEffect;

	missEnemyEffectRadius = 10;
	missEnemyEffect = FF3ProjectileMissedEnemyEffect;

//	particleEmitter = RedFF3ProjectileParticleEmitter;
//	laserTrail[0]   = FF3ProjectileLaserTrail;
//	laserTrail[1]   = RedFF3ProjectileLaserTrail;
	laserTail	    = RedFF3ProjectileLaserTail;
	laserTailLen    = 15;

	muzzleVelocity	= 0; // Handled by FF3PseudoProjectile
	velInheritFactor = 0; // Handled by FF3PseudoProjectile

	isBallistic			= true;
	gravityMod			 = 5.0;

	armingDelay			= 0;
	lifetime				= 750;
	fadeDelay			= 0;

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

function RedFF3Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

   //%col.leak += 0.05;
}

//--------------------------------------------------------------------------

datablock ProjectileData(BlueFF3Projectile : RedFF3Projectile)
{
   projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.blue.dts";
	explosion = BlueFF3ProjectileExplosion;
	hitEnemyExplosion = BlueFF3ProjectileHit;
	hitTeammateExplosion = BlueFF3ProjectileHit;
	//particleEmitter = BlueFF3ProjectileParticleEmitter;
	laserTail = BlueFF3ProjectileLaserTail;
	lightColor  = "0.0 0.0 1.0";
};

function BlueFF3Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedFF3Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedFF3Image)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;

	// basic item properties
	shapeFile = "share/shapes/inf/ff3-0.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset      = "0 0 0";
	rotation    = "0 0 0";
	eyeOffset	= "0.15 0.1 -0.1";
	eyeRotation = "0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 30;

	projectile = FF3PseudoProjectile;

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
	fireprojectile[0] = RedFF3Projectile;

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
		stateTimeoutValue[3]             = 0.00;
		stateTransitionOnTimeout[3]      = "AfterFire";
		stateFire[3]                     = true;
		stateFireProjectile[3]           = FF3PseudoProjectile;
		stateRecoil[3]                   = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimblaster";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = FF3FireSound;

		// after fire...
		stateName[8]                     = "AfterFire";
		stateTransitionOnTriggerUp[8]    = "KeepAiming";

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

function RedFF3Image::getBulletSpread(%this, %obj)
{
   %vel = VectorLen(%obj.getVelocity());
   %a = %vel > 22 ? (%vel-22)*0.02 : 0;
   if(%a > 0.3) %a = 0.3;
   return 0.01+%a;
}

function RedFF3Image::onMount(%this, %obj, %slot)
{
    Parent::onMount(%this, %obj, %slot);

   // Set up recoil
   %obj.setImageRecoilEnabled(%slot, true);
   %obj.setImageCurrentRecoil(%slot, 100);
   %obj.setImageMaxRecoil(%slot, 100);
   %obj.setImageRecoilAdd(%slot, 0);
   %obj.setImageRecoilDelta(%slot, -0);

   // Set up crosshair
   %client = %obj.client;
   if(!isObject(%client)) return;
   commandToClient(%client, 'Crosshair', 0);
   commandToClient(%client, 'Crosshair', 2, 6);
   commandToClient(%client, 'Crosshair', 1);
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueFF3Image : RedFF3Image)
{
	shapeFile = "share/shapes/inf/ff3-0.blu.dts";
	fireprojectile[0] = BlueFF3Projectile;
	lightColor = "0 0.5 1";
	//stateFireProjectile[3] = BlueAssaultRifleProjectile1;
	//stateFireProjectile[8] = BlueAssaultRifleProjectile2;
};

function BlueFF3Image::getBulletSpread(%this, %obj)
{
   RedFF3Image::getBulletSpread(%this, %obj);
}

function BlueFF3Image::onMount(%this, %obj, %slot)
{
   RedFF3Image::onMount(%this, %obj, %slot);
}

