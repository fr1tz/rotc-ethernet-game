//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./exampleturret.blueprint.cs");
exec("./exampleturret.sfx.cs");
exec("./exampleturret.gfx.cs");

//-----------------------------------------------------------------------------
// the turret's projectile...

datablock ProjectileData(ExampleTurretProjectile)
{
   explosion             = ExampleTurretProjectileExplosion;
// bounceExplosion       = ExampleTurretProjectileExplosion;
   hitEnemyExplosion     = ExampleTurretProjectileExplosion;
   nearEnemyExplosion    = ExampleTurretProjectileExplosion;
   hitTeammateExplosion  = ExampleTurretProjectileExplosion;
   hitDeflectorExplosion = ExampleTurretProjectileExplosion;

   particleEmitter       = ExampleTurretProjectileParticleEmitter;

   muzzleVelocity      = 200;
   velInheritFactor    = 1.0;

   isBallistic         = true;
   gravityMod          = 1.0;
   bounceElasticity    = 0.3;
   bounceFriction      = 0.5;

   armingDelay         = 0;
   lifetime            = 20*1000;
   fadeDelay           = 5000;
};

function ExampleTurretProjectile::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{

}

function ExampleTurretProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
   if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) ) return;

   // if the collision object is an enemy...
   if( true )//%col.teamId != %obj.getTeamId() )
   {
      %energy = %col.getEnergyLevel();
      %energyMax = %col.getDataBlock().maxEnergy;
      %energyDiff = %energyMax - %energy;

      %damage = 500 + 500*(1-(%energy/%energyMax));

      echo("    damage:" SPC %damage);

      // apply impulse...
      %impulseVec = VectorScale(%normal,-1);
      %impulseVec = VectorNormalize(%impulseVec);
      %impulseVec = VectorScale(%impulseVec, 2500);
      %col.applyImpulse(%pos, %impulseVec);

      // apply damage...
      %col.damage(%obj,%pos,%damage,"ExampleTurretProjectile");
   }
   else // collision with teammate...
   {
      //
   }
}

//-----------------------------------------------------------------------------
// the turret...

datablock TurretData(ExampleTurret)
{
	shapeFile = "share/shapes/rotc/turrets/exampleturret/turret.dts";
   emap = true;
   
   moveSound = ExampleTurretMoveSound;
   
   renderWhenDestroyed = false;

   cameraDefaultFov = 110.0;
   cameraMinFov     = 80.0;
   cameraMaxFov     = 130.0;

	cameraMaxDist = 10;
	cameraOffset  = 2.5;
	maxEnergy	  = 100;
	energyRechargeRate  = 0.4;

   // turret specific stuff
   maxPitchSpeed = 90;
   maxYawSpeed = 90;
   minPitch = -180;
   maxPitch = 20;
   minYaw = -180.0;
   maxYaw = 180;
};

function ExampleTurret::onAdd(%this, %obj)
{
   Parent::onAdd(%this, %obj);
   %obj.mountImage(ExampleTurretWeaponImage,0);
}

function ExampleTurret::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
{
   // pass the damage on to the tank...
   %obj.getObjectMount().damage(%sourceObject, %position, %damage, %damageType);
}

//-----------------------------------------------------------------------------
// the turret weapon image...

datablock ShapeBaseImageData(ExampleTurretWeaponImage)
{
   // basic item properties
   shapeFile = "share/shapes/rotc/misc/nothing.dts";
   emap = true;

   // mount point & mount offset...
   mountPoint  = 0;
   offset      = "0 0 0";
   rotation    = "0 0 0";
   //eyeOffset   = "0.2 0.8 -0.4";
   //eyeRotation = "0 0 0";

   // correct the muzzleVecotr to always point at the crosshair...
   correctMuzzleVector = true;

   usesEnergy = true;
   minEnergy = 40;

   //-------------------------------------------------
   // image states...
   //
      // preactivation...
      stateName[0]                     = "Preactivate";
      stateTransitionOnLoaded[0]       = "Activate";
      stateTransitionOnNoAmmo[0]       = "NoAmmo";

      // when mounted...
      stateName[1]                     = "Activate";
      stateTransitionOnTimeout[1]      = "Ready";
      stateTimeoutValue[1]             = 0.5;
      stateSequence[1]                 = "Activate";

      // ready to fire, just waiting for the trigger...
      stateName[2]                     = "Ready";
      stateTransitionOnNoAmmo[2]       = "NoAmmo";
      stateTransitionOnTriggerDown[2]  = "Fire";

      stateName[3]                     = "Fire";
      stateTransitionOnTimeout[3]      = "Fire";
      stateTransitionOnTriggerUp[3]    = "Ready";
      stateTransitionOnNoAmmo[3]       = "NoAmmo";
      stateTimeoutValue[3]             = 1.00;
      stateFire[3]                     = true;
      stateAllowImageChange[3]         = false;
      stateEjectShell[3]               = true;
      stateSequence[3]                 = "Fire";
      stateScript[3]                   = "fire";

      stateName[4]                     = "NoAmmo";
      stateTransitionOnAmmo[4]         = "Ready";
      stateSequence[4]                 = "NoAmmo";
   //
   // ...end of image states
   //-------------------------------------------------
};

function ExampleTurretWeaponImage::fire(%this, %obj, %slot)
{
   %projectile = ExampleTurretProjectile;

   // drain some energy...
   %obj.setEnergyLevel( %obj.getEnergyLevel() - %this.minEnergy );

   // determine initial projectile velocity based on the
   // gun's muzzle point and the object's current velocity...
   %muzzleVector = %obj.getMuzzleVector(%slot);
   %objectVelocity = %obj.getVelocity();
   %muzzleVelocity = VectorAdd(
      VectorScale(%muzzleVector, %projectile.muzzleVelocity),
      VectorScale(%objectVelocity, %projectile.velInheritFactor));

   // determine muzzle-point...
   %muzzlePoint = %obj.getMuzzlePoint(%slot);
   
   // create the projectile object...
   %p = new Projectile() {
      dataBlock        = %projectile;
      initialVelocity  = %muzzleVelocity;
      initialPosition  = %muzzlePoint;
      sourceObject     = %obj;
      sourceSlot       = %slot;
      client           = %obj.client;
   };
   MissionCleanup.add(%p);
   
   // apply recoil to the tank...
   %tank = %obj.getObjectMount();
   %impulseVec = VectorSub(%obj.getWorldBoxCenter(), %muzzlePoint);
   %impulseVec = VectorNormalize(%impulseVec);
   %impulseVec = VectorScale(%impulseVec, 500*1000);
   %tank.applyImpulse(%muzzlePoint, %impulseVec);
   
   // create a fire explosion...
   createExplosionOnClients(TankFireExplosion,%muzzlePoint,
      VectorNormalize(%muzzleVector));
   
   return %p;
}


