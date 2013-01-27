//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - grenadelauncher.cs
// Code for the grenadelauncher
//------------------------------------------------------------------------------

exec("./grenadelauncher.sfx.cs");
exec("./grenadelauncher.gfx.red.cs");
exec("./grenadelauncher.gfx.blue.cs");

//-----------------------------------------------------------------------------

datablock ShapeBaseImageData(HolsteredGrenadeLauncherImage)
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

datablock TracerProjectileData(RedGrenadeLauncherProjectile)
{
	// script damage properties...
	impactDamage        = 0;
	impactImpulse       = 1000;
	splashDamage        = 40;
	splashDamageRadius  = 18;
	splashDamageFalloff = $SplashDamageFalloff::Linear;
	splashImpulse       = 0;
	
	// how much energy does firing this projectile drain?...
	energyDrain = 10;

	trackingAgility = 0;
	
	explodesNearEnemies        = false;
	explodesNearEnemiesRadius  = 10;
	explodesNearEnemiesMask    = $TypeMasks::PlayerObjectType;

	sound = GrenadeLauncherProjectileSound;
 
	projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion             = RedGrenadeLauncherProjectileExplosion;
	bounceExplosion		  = RedGrenadeLauncherProjectileBounceExplosion;
//	hitEnemyExplosion     = GrenadeLauncherProjectileImpact;
//	nearEnemyExplosion    = GrenadeLauncherProjectileExplosion;
//	hitTeammateExplosion  = GrenadeLauncherProjectileImpact;
//	hitDeflectorExplosion = DiscDeflectedEffect;

//  particleEmitter	= RedGrenadeLauncherProjectileParticleEmitter;
	laserTrail[0]   = RedGrenadeLauncherProjectileLaserTrail;
//	laserTrail[1]   = RedGrenadeLauncherProjectileLaserTrail2;
//	laserTail	    = RedGrenadeLauncherProjectileLaserTail;
//	laserTailLen    = 2;

	muzzleVelocity		= 100;
	velInheritFactor	 = 1.0;
	
	isBallistic = true;
	gravityMod  = 10.0;
	bounceElasticity = 0.5;
	bounceFriction   = 0.5;

	armingDelay	= 500;
	lifetime    = 1000*10;
	fadeDelay   = 5000;
	
	decals[0]	= ExplosionDecalTwo;
	
	hasLight	 = true;
	lightRadius = 6.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedGrenadeLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------

datablock TracerProjectileData(BlueGrenadeLauncherProjectile : RedGrenadeLauncherProjectile)
{
	projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";
	explosion = BlueGrenadeLauncherProjectileExplosion;
	bounceExplosion = BlueGrenadeLauncherProjectileBounceExplosion;
	laserTrail[0]   = BlueGrenadeLauncherProjectileLaserTrail;
//	laserTrail[0]   = BlueGrenadeLauncherProjectileLaserTrail2;
	lightColor  = "0.0 0.0 1.0";
};

function BlueGrenadeLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedGrenadeLauncherProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(RedGrenadeLauncherImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;
	
	// basic item properties
	shapeFile = "share/shapes/rotc/misc/nothing.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset		= "0 0 0";
	rotation	 = "0 0 0";
	eyeOffset   = "0.275 -0.25 -0.2";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 15;

	projectile = RedGrenadeLauncherProjectile;

	// script fields...
	iconId = 7;
	specialWeapon = true;
	armThread  = "holdrifle";  // armThread to use when holding this weapon
	crosshair  = "assaultrifle"; // crosshair to display when holding this weapon
    bigGrenade = RedGrenade;

	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Ready";
		stateTransitionOnNoAmmo[0]		 = "NoAmmo";

		// when mounted...
		stateName[1]                     = "Activate";
		stateTransitionOnTimeout[1]      = "Ready";
		stateTimeoutValue[1]             = 0.5;
		stateSpinThread[1]               = "Stop";
		stateSequence[1]                 = "Activate";

		// ready to fire, just waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnNoAmmo[2]       = "NoAmmo";
  		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Fire1";
        stateArmThread[2]                = "holdrifle";
		stateSpinThread[2]               = "Stop";

		stateName[3]                     = "Fire1";
		stateTransitionOnTimeout[3]      = "Fire2";
		stateTransitionOnTriggerUp[3]    = "Cooldown";
		stateTransitionOnNoAmmo[3]       = "Cooldown";
		stateTimeoutValue[3]             = 0.17;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = RedGrenadeLauncherProjectile;
		stateAllowImageChange[3]         = false;
		stateArmThread[3]                = "aimrifle";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = GrenadeLauncherFireSound;
		stateScript[3]                   = "onFire";

		stateName[4]                     = "Fire2";
		stateTransitionOnTimeout[4]      = "Fire3";
		stateTransitionOnTriggerUp[4]    = "Cooldown";
		stateTransitionOnNoAmmo[4]       = "Cooldown";
		stateTimeoutValue[4]             = 0.17;
		stateSpinThread[4]               = "FullSpeed";
		stateFireProjectile[4]           = RedGrenadeLauncherProjectile;
		stateAllowImageChange[4]         = false;
		stateSequence[4]                 = "Fire";
		stateSound[4]                    = GrenadeLauncherFireSound;

		stateName[5]                     = "Fire3";
		stateTransitionOnTimeout[5]      = "Fire4";
		stateTransitionOnTriggerUp[5]    = "Cooldown";
		stateTransitionOnNoAmmo[5]       = "Cooldown";
		stateTimeoutValue[5]             = 0.17;
		stateSpinThread[5]               = "FullSpeed";
		stateFireProjectile[5]           = RedGrenadeLauncherProjectile;
		stateAllowImageChange[5]         = false;
		stateSequence[5]                 = "Fire";
		stateSound[5]                    = GrenadeLauncherFireSound;

		stateName[6]                     = "Fire4";
		stateTransitionOnTimeout[6]      = "Cooldown";
		stateTransitionOnNoAmmo[6]       = "Cooldown";
		stateTimeoutValue[6]             = 0.17;
		stateSpinThread[6]               = "FullSpeed";
		stateFireProjectile[6]           = RedGrenadeLauncherProjectile;
		stateAllowImageChange[6]         = false;
		stateSequence[6]                 = "Fire";
		stateSound[6]                    = GrenadeLauncherFireSound;
		
		stateName[7]                     = "Cooldown";
		stateTransitionOnTimeout[7]      = "KeepAiming";
		stateTimeoutValue[7]             = 1.0;
		stateSpinThread[7]               = "SpinDown";
		stateArmThread[7]                = "aimrifle";
		
		stateName[8]                     = "KeepAiming";
		stateTransitionOnNoAmmo[8]       = "NoAmmo";
		stateTransitionOnNotLoaded[8]    = "Disabled";
		stateTransitionOnTriggerDown[8]  = "Fire1";
		stateTransitionOnTimeout[8]      = "Ready";
		stateWaitForTimeout[8]           = false;
		stateTimeoutValue[8]             = 2.00;
		stateSpinThread[8]               = "Stop";

        // no ammo...
		stateName[9]                     = "NoAmmo";
		stateTransitionOnAmmo[9]         = "Ready";
        stateTransitionOnTriggerDown[9]  = "DryFire";
		stateTimeoutValue[9]             = 0.50;
		stateSequence[9]                 = "NoAmmo";
  
        // dry fire...
		stateName[10]                    = "DryFire";
		stateTransitionOnTriggerUp[10]   = "NoAmmo";
		stateSound[10]                   = WeaponEmptySound;
  
		// disabled...
		stateName[11]                    = "Disabled";
		stateTransitionOnLoaded[11]      = "Ready";
		stateAllowImageChange[11]        = false;
	//
	// ...end of image states
	//-------------------------------------------------
};

function RedGrenadeLauncherImage::onFire(%this, %obj, %slot)
{
	%obj.throwGrenade = false;
}

function RedGrenadeLauncherImage::fireBigGrenade(%this, %obj, %slot)
{
	%projectile = %this.bigGrenade;

	// drain some energy...
	%obj.setEnergyLevel( %obj.getEnergyLevel() - %projectile.energyDrain );

    // %throwForce is based on how long the trigger has been hold down...
    %throwCoefficient = 0;
    if(false)
    {
        %throwCoefficient = 1;
    }
    else
    {
        %throwCoefficient = (getSimTime() - %obj.grenadeStart) / 1000;
		error(%throwCoefficient);
        if( %throwCoefficient > 1 )
            %throwCoefficient = 1;
    }
    //%throwCoefficient = %throwCoefficient/2;
    %throwForce = %projectile.muzzleVelocity * %throwCoefficient;

    %vec = %obj.getMuzzleVector(%slot);
    %vec = vectorScale(%vec, %throwForce);

    // add a vertical component to give the grenade a better arc
    %verticalForce = %throwForce / 8;
    %dot = vectorDot("0 0 1",%eye);
    if (%dot < 0) %dot = -%dot;
    %vec = vectorAdd(%vec,VectorScale("0 0 " @ %verticalForce,1 - %dot));

    // add velocity inherited from player...
    %vec = vectorAdd( %vec, VectorScale(%obj.getVelocity(), %projectile.velInheritFactor));

    // get initial position...
    %pos = %obj.getMuzzlePoint(%slot);

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

    //%disc.schedule(2000,"explode");

	%obj.decGrenadeAmmo(1.0);

	return %grenade;
}

//------------------------------------------------------------------------------

datablock ShapeBaseImageData(BlueGrenadeLauncherImage : RedGrenadeLauncherImage)
{
	projectile = BlueGrenadeLauncherProjectile;
    stateFireProjectile[3] = BlueGrenadeLauncherProjectile;
	stateFireProjectile[4] = BlueGrenadeLauncherProjectile;
    stateFireProjectile[5] = BlueGrenadeLauncherProjectile;
	stateFireProjectile[6] = BlueGrenadeLauncherProjectile;
};

