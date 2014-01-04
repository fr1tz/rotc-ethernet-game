//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./blaster5.sfx.cs");
exec("./blaster5.gfx.cs");
exec("./blaster5.gfx.red.cs");
exec("./blaster5.gfx.blue.cs");

datablock TracerProjectileData(Blaster5PseudoProjectile)
{
	energyDrain = 36;
	lifetime = 1000;
	muzzleVelocity = 200 * $Server::Game.slowpokemod;
	velInheritFactor = 0.5 * $Server::Game.slowpokemod;
};

function Blaster5PseudoProjectile::onAdd(%this, %obj)
{
	%player = %obj.sourceObject;
	%slot = %obj.sourceSlot;
	%image = %player.getMountedImage(%slot);

	%muzzlePoint = %obj.initialPosition;
	%muzzleVector = %obj.initialVelocity;

   %projectile = %image.fireprojectile[0];

   // create the projectile object...
   %p = new Projectile() {
      dataBlock       = %projectile;
      teamId          = %obj.teamId;
      initialVelocity = %muzzleVector;
      initialPosition = %muzzlePoint;
      sourceObject    = %player;
      sourceSlot      = %slot;
      client          = %player.client;
   };
   MissionCleanup.add(%p);

	// no need to ghost pseudo projectile to clients...
	%obj.delete();
}

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ProjectileData(RedBlaster5Projectile)
{
	stat = "blaster5";

	// script damage properties...
	impactDamage       = 120;
	impactImpulse      = 5000;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = false;

	trackingAgility = 0;

	explodesNearEnemies			= false;
	explodesNearEnemiesRadius	= 2;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = Blaster5ProjectileSound;

   projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.red.dts";

	explosion             = RedBlaster5ProjectileImpact;
//	bounceExplosion       = RedBlaster5ProjectileBounceExplosion;
	hitEnemyExplosion     = RedBlaster5ProjectileHit;
//	nearEnemyExplosion    = RedBlaster5ProjectileExplosion;
	hitTeammateExplosion  = RedBlaster5ProjectileHit;
//	hitDeflectorExplosion = DiscDeflectedEffect;

	missEnemyEffectRadius = 10;
	missEnemyEffect = RedBlaster5ProjectileMissedEnemyEffect;

//	particleEmitter = RedBlaster5ProjectileParticleEmitter;
//	laserTrail[0]   = Blaster5ProjectileLaserTrail;
//	laserTrail[1]   = RedBlaster5ProjectileLaserTrail;
	laserTail	    = RedBlaster5ProjectileLaserTail;
	laserTailLen    = 1;

	muzzleVelocity	= 0; // Handled by Blaster5PseudoProjectile
	velInheritFactor = 0; // Handled by Blaster5PseudoProjectile

	isBallistic			= true;
	gravityMod			 = 3.0 * $Server::Game.slowpokemod;

	armingDelay    = 0;
	lifetime			= 5000;
	fadeDelay		= 5000;

	numBounces = 0;

	decals[0] = BulletHoleDecalTwo;

	bounceDecals[0] = SmashDecalOne;
	bounceDecals[1] = SmashDecalTwo;
	bounceDecals[2] = SmashDecalThree;
	bounceDecals[3] = SmashDecalFour;

	hasLight	 = true;
	lightRadius = 8.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedBlaster5Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

    %src =  %obj.getSourceObject();
    if(!%src)
        return;
        
   %src.setDiscTarget(%col);
}

//--------------------------------------------------------------------------

datablock ProjectileData(BlueBlaster5Projectile : RedBlaster5Projectile)
{
   projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.blue.dts";
	explosion = BlueBlaster5ProjectileImpact;
	hitEnemyExplosion = BlueBlaster5ProjectileHit;
	hitTeammateExplosion = BlueBlaster5ProjectileHit;
	missEnemyEffect = BlueBlaster5ProjectileMissedEnemyEffect;
	//particleEmitter = BlueBlaster5ProjectileParticleEmitter;
	laserTail = BlueBlaster5ProjectileLaserTail;
	lightColor  = "0.0 0.0 1.0";
};

function BlueBlaster5Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedBlaster5Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedBlaster5Image)
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

	projectile = Blaster5PseudoProjectile;

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
	armThread  = "aimrifle";  // armThread to use when holding this weapon
	crosshair  = "assaultrifle"; // crosshair to display when holding this weapon
	fireprojectile[0] = RedBlaster5Projectile;

	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Ready";
		stateTransitionOnNoAmmo[0]       = "NoAmmo";
      stateArmThread[0]                = "aimrifle";

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
		stateSpinThread[2]               = "FullSpeed";
		stateSequence[2]                 = "idle";

		stateName[3]                     = "Fire";
		stateTimeoutValue[3]             = 0.0;
		stateTransitionOnTimeout[3]      = "PumpDelay";
		stateFire[3]                     = true;
		stateFireProjectile[3]           = Blaster5PseudoProjectile;
		stateRecoil[3]                   = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = Blaster5FireSound;
  
		stateName[8]                     = "PumpDelay";
		stateTimeoutValue[8]             = 0.4;
		stateTransitionOnTimeout[8]      = "Pump";
		stateAllowImageChange[8]         = false;

		stateName[9]                     = "Pump";
		stateTimeoutValue[9]             = 0.6;
		stateTransitionOnTimeout[9]      = "AfterFire";
		stateAllowImageChange[9]         = false;
		stateEjectShell[9]               = true;
		stateSound[9]                    = Blaster5PumpSound;

		// after fire...
		stateName[10]                    = "AfterFire";
		stateTransitionOnTriggerUp[10]   = "KeepAiming";

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

function RedBlaster5Image::onMount(%this, %obj, %slot)
{
   Parent::onMount(%this, %obj, %slot);

   //%client = %obj.client;
   //if(isObject(%client))
   //{
   //   %obj.zLastFov = %client.getControlCameraFov();
   //   %client.setControlCameraFov(90);
   //   %client.lockControlCameraFov(true);
   //}

   // Set up recoil
   %obj.setImageRecoilEnabled(%slot, true);
   %obj.setImageCurrentRecoil(%slot, 60);
   %obj.setImageMaxRecoil(%slot, 60);
   %obj.setImageRecoilAdd(%slot, 0);
   %obj.setImageRecoilDelta(%slot, -0);
}

function RedBlaster5Image::onUnmount(%this, %obj, %slot)
{
   Parent::onUnmount(%this, %obj, %slot);

   //%client = %obj.client;
   //if(isObject(%client))
   //{
   //   %client.setControlCameraFov(%obj.zLastFov);
   //   %client.lockControlCameraFov(false);
   //}
}

function RedBlaster5Image::setupHud(%this, %obj, %slot)
{
   %client = %obj.client;
   if(!isObject(%client))
      return;
      
   commandToClient(%client, 'Crosshair', 0);
   //commandToClient(%client, 'Crosshair', 2, 2);
   commandToClient(%client, 'Crosshair', 3, 2, 10);
   commandToClient(%client, 'Crosshair', 5, "./rotc/ch2");
   //commandToClient(%client, 'Crosshair', 6, "./rotc/ch.static.1", 128, 128);
   commandToClient(%client, 'Crosshair', 1);

   return;

   %client.setHudBackground(
      1,
      "share/hud/rotc/bg.white",
      "255 255 255",
      false,
      255,
      -32
   );

   %client.setHudBackground(
      2,
      "share/hud/rotc/bg.blaster5",
      "0 255 0",
      false,
      255,
      0
   );

   %client.setHudBackground(
      3,
      "share/hud/rotc/bg.scanlines",
      "0 255 0",
      true,
      32,
      0
   );
}

function RedBlaster5Image::getBulletSpread(%this, %obj)
{
   return 0.04;
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueBlaster5Image : RedBlaster5Image)
{
	shapeFile = "share/shapes/rotc/weapons/blaster/image2.blue.dts";
	fireprojectile[0] = BlueBlaster5Projectile;
	lightColor = "0 0.5 1";
	//stateFireProjectile[3] = BlueAssaultRifleProjectile1;
	//stateFireProjectile[8] = BlueAssaultRifleProjectile2;
};

function BlueBlaster5Image::onMount(%this, %obj, %slot)
{
   RedBlaster5Image::onMount(%this, %obj, %slot);
}

function BlueBlaster5Image::setupHud(%this, %obj)
{
   RedBlaster5Image::setupHud(%this, %obj);
}

function BlueBlaster5Image::getBulletSpread(%this, %obj)
{
   RedBlaster5Image::getBulletSpread(%this, %obj);
}

