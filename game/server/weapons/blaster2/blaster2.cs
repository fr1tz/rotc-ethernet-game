//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./blaster2.sfx.cs");
exec("./blaster2.gfx.cs");
exec("./blaster2.gfx.red.cs");
exec("./blaster2.gfx.blue.cs");

datablock TracerProjectileData(Blaster2PseudoProjectile)
{
	energyDrain = 36;
	lifetime = 1000;
	muzzleVelocity = 200 * $Server::Game.slowpokemod;
	velInheritFactor = 0.5 * $Server::Game.slowpokemod;
};

function Blaster2PseudoProjectile::onAdd(%this, %obj)
{
	%player = %obj.sourceObject;
	%slot = %obj.sourceSlot;
	%image = %player.getMountedImage(%slot);

	%muzzlePoint = %obj.initialPosition;
	%muzzleVector = %obj.initialVelocity;
	%muzzleTransform = createOrientFromDir(VectorNormalize(%muzzleVector));

	%spread = 0;
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

	for(%i = 0; %i < 9; %i++)
	{
      %spread = %image.getBulletSpread(%player);
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

datablock ProjectileData(RedBlaster2Projectile)
{
	stat = "blaster";

	// script damage properties...
	impactDamage       = 10;
	impactImpulse      = 400;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = false;

	trackingAgility = 0;

	explodesNearEnemies			= false;
	explodesNearEnemiesRadius	= 2;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = Blaster2ProjectileSound;

   projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.red.dts";

	explosion             = RedBlasterProjectileImpact;
//	bounceExplosion       = RedBlasterProjectileBounceExplosion;
	hitEnemyExplosion     = RedBlasterProjectileHit;
//	nearEnemyExplosion    = RedBlasterProjectileExplosion;
	hitTeammateExplosion  = RedBlasterProjectileHit;
//	hitDeflectorExplosion = DiscDeflectedEffect;

	missEnemyEffectRadius = 10;
	missEnemyEffect = RedBlasterProjectileMissedEnemyEffect;

//	particleEmitter = RedBlaster2ProjectileParticleEmitter;
//	laserTrail[0]   = Blaster2ProjectileLaserTrail;
//	laserTrail[1]   = RedBlaster2ProjectileLaserTrail;
	laserTail	    = RedBlaster2ProjectileLaserTail;
	laserTailLen    = 15;

	muzzleVelocity	= 0; // Handled by Blaster2PseudoProjectile
	velInheritFactor = 0; // Handled by Blaster2PseudoProjectile

	isBallistic			= true;
	gravityMod			 = 1.0 * $Server::Game.slowpokemod;

	armingDelay			= 0;
	lifetime			= 750;
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

function RedBlaster2Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

    %src =  %obj.getSourceObject();
    if(!%src)
        return;

    %currTime = getSimTime();

	if(%col.blasterHitSrc == %src
	&& (%currTime - %col.blasterHitTime <= 250))
    {
        %col.blasterHitNum += 1;
        if(%col.blasterHitNum == 4)
            %src.setDiscTarget(%col);
    }
    else
    {
		%col.blasterHitSrc = %src;
        %col.blasterHitTime = %currTime;
        %col.blasterHitNum = 1;
    }
}

//--------------------------------------------------------------------------

datablock ProjectileData(BlueBlaster2Projectile : RedBlaster2Projectile)
{
   projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.blue.dts";
	explosion = BlueBlasterProjectileImpact;
	hitEnemyExplosion = BlueBlasterProjectileHit;
	hitTeammateExplosion = BlueBlasterProjectileHit;
	//particleEmitter = BlueBlaster2ProjectileParticleEmitter;
	laserTail = BlueBlaster2ProjectileLaserTail;
	lightColor  = "0.0 0.0 1.0";
};

function BlueBlaster2Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedBlaster2Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedBlaster2Image)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;

	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/blaster/image2.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset      = "0 0 0";
	rotation    = "0 0 0";
	eyeOffset	= "0.3 -0.025 -0.15";
	eyeRotation = "0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 30;

	projectile = Blaster2PseudoProjectile;

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
	fireprojectile[0] = RedBlaster2Projectile;

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
		stateFireProjectile[3]           = Blaster2PseudoProjectile;
		stateRecoil[3]                   = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimblaster";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = Blaster2FireSound;

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

function RedBlaster2Image::getBulletSpread(%this, %obj)
{
   return 0.05;
}

function RedBlaster2Image::onMount(%this, %obj, %slot)
{
    Parent::onMount(%this, %obj, %slot);

   // Set up recoil
   %obj.setImageRecoilEnabled(%slot, true);
   %obj.setImageCurrentRecoil(%slot, 150);
   %obj.setImageMaxRecoil(%slot, 150);
   %obj.setImageRecoilAdd(%slot, 0);
   %obj.setImageRecoilDelta(%slot, -0);
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueBlaster2Image : RedBlaster2Image)
{
	shapeFile = "share/shapes/rotc/weapons/blaster/image2.blue.dts";
	fireprojectile[0] = BlueBlaster2Projectile;
	lightColor = "0 0.5 1";
	//stateFireProjectile[3] = BlueAssaultRifleProjectile1;
	//stateFireProjectile[8] = BlueAssaultRifleProjectile2;
};

function BlueBlaster2Image::getBulletSpread(%this, %obj)
{
   RedBlaster2Image::getBulletSpread(%this, %obj);
}

function BlueBlaster2Image::onMount(%this, %obj, %slot)
{
   RedBlaster2Image::onMount(%this, %obj, %slot);
}

