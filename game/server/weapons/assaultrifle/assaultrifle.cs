//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - assaultrifle.cs
// Code for the assault rifle
//------------------------------------------------------------------------------

exec("./assaultrifle.sfx.cs");
exec("./assaultrifle.gfx.red.cs");
exec("./assaultrifle.gfx.blue.cs");

//-----------------------------------------------------------------------------

datablock ShapeBaseImageData(HolsteredAssaultRifleImage)
{
	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image_holstered.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 2;
	offset = "0 0 0";
	
	stateName[0] = "DoNothing";
};

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ShotgunProjectileData(RedAssaultRifleProjectile)
{
	// script damage properties...
	impactDamage        = 15;
	impactImpulse       = 250;
	splashDamage        = 0;
	splashDamageRadius  = 0;
	splashDamageFalloff = $SplashDamageFalloff::Exponential;
	splashImpulse       = 0;
	
	// how much energy does firing this projectile drain?...
	energyDrain = 3;

	numBullets = 1; // number of shotgun bullets

	range = 500; // shotgun range
	muzzleSpreadRadius = 0.0;
	referenceSpreadRadius = 0.0;
	referenceSpreadDistance = 40;

	trackingAgility = 0;
	
	explodesNearEnemies			= false;
	explodesNearEnemiesRadius	= 5;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = AssaultRifleProjectileFlybySound;
 
//  projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion             = RedAssaultRifleProjectileExplosion;
//	bounceExplosion		  = RedAssaultRifleProjectileBounceExplosion;
//	hitEnemyExplosion     = AssaultRifleProjectileImpact;
//	nearEnemyExplosion    = AssaultRifleProjectileExplosion;
//	hitTeammateExplosion  = AssaultRifleProjectileImpact;
//	hitDeflectorExplosion = DiscDeflectedEffect;

//   particleEmitter	= AssaultRifleProjectileParticleEmitter;
	laserTrail[0]   = RedAssaultRifleProjectileLaserTrail;
	laserTrail[1]   = RedAssaultRifleProjectileLaserTrail;
//	laserTail	    = RedAssaultRifleProjectileLaserTail;
//	laserTailLen    = 10;

	muzzleVelocity   = 9999;
	velInheritFactor = 0.0;
	
	isBallistic = false;
	gravityMod	= 7.5;

	armingDelay	= 0;
	lifetime    = 1000*10;
	fadeDelay   = 5000;
	
	decals[0] = BulletHoleDecalOne;
	
	hasLight	 = false;
	lightRadius = 6.0;
	lightColor  = "1.0 0.8 0.0";
};

function RedAssaultRifleProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

//	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
//		return;
//
//    %src =  %obj.getSourceObject();
//    if(%src)
//        %src.setDiscTarget(%col);
}

//--------------------------------------------------------------------------

datablock ShotgunProjectileData(BlueAssaultRifleProjectile : RedAssaultRifleProjectile)
{
//	projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";
//	explosion = BlueAssaultRifleProjectileExplosion;
//	bounceExplosion = BlueAssaultRifleProjectileBounceExplosion;
	laserTrail[0]   = BlueAssaultRifleProjectileLaserTrail;
//	laserTail = BlueAssaultRifleProjectileLaserTail;
//	lightColor  = "0.0 0.0 1.0";
};

function BlueAssaultRifleProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedAssaultRifleProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
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
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset		= "0 0 0";
	rotation	 = "0 0 0";
	eyeOffset	= "0.325 0.4 -0.35";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 3;

	projectile = RedAssaultRifleProjectile;

	// script fields...
	iconId = 7;
	specialWeapon = true;
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
		stateSequence[1]                 = "Activate";

		// ready to fire, just waiting for the trigger...
		stateName[2]                     = "Ready";
  		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Fire";
        stateArmThread[2]                = "holdrifle";
		
		stateName[3]                     = "Fire";
		stateTransitionOnNoAmmo[3]       = "Reload";
		stateTransitionOnTimeout[3]      = "Fire";
		stateTransitionOnTriggerUp[3]    = "Ready";
		stateTimeoutValue[3]             = 0.1;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = RedAssaultRifleProjectile;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		//stateRecoil[3]                   = "LightRecoil";
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
		stateSequence[5]                 = "NoAmmo";
  
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

datablock ShapeBaseImageData(BlueAssaultRifleImage : RedAssaultRifleImage)
{
	projectile = BlueAssaultRifleProjectile;
    stateFireProjectile[3] = BlueAssaultRifleProjectile;
};

