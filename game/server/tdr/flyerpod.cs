//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//exec("./ScoutDrone.blaster.cs");

//exec("./scoutdrone.blueprint.cs");
//exec("./scoutdrone.wreck.cs");
//exec("./scoutdrone.sfx.cs");
//exec("./scoutdrone.gfx.cs");

//-----------------------------------------------------------------------------
// FlyingVehicleData

datablock FlyingVehicleData(FlyerPod)
{
   // @name dynamic fields, needed for certain in-script checks -mag
   // @{
   isAircraft = true;
   class = "ScoutDrone";
   // @}

//   category = "Vehicles"; don't appear in mission editor
   shapeFile = "share/shapes/rotc/vehicles/hoverpod/vehicle.dts";
   emap = true;
   
   renderWhenDestroyed = false;
   //explosion = ScoutDroneExplosion;
   //defunctEffect = ScoutDroneDefunctEffect;
   //debris = BomberDebris;
   //debrisShapeName = "~/data/vehicles/bomber/vehicle.dts";

   drag    = 0.15;
   density = 1.0;

   // 3rd person camera settings...
   cameraRoll = true;         // Roll the camera with the vehicle
   cameraMaxDist = 5;         // Far distance from vehicle
   cameraOffset = 0.0;        // Vertical offset from camera mount point
   cameraLag = 0.05;           // Velocity lag of camera
   cameraDecay = 0.75;        // Decay per sec. rate of velocity lag
   
   maxDamage = 75;
   maxEnergy = 100;           // Afterburner and any energy weapon pool
   energyRechargeRate = 0.4;

   // drag...
   minDrag = 60;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 10;         // Angular Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   // autostabilizer...
   maxAutoSpeed = 15;            // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 500;  // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 500;   // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.95;      // Dampen control input so you don't` whack out at very slow speeds

   // maneuvering...
   maxSteeringAngle = 6;          // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 10;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 8;      // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 8000;  // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 250*5;      // Steering jets (force applied when you move the mouse)
   steeringRollForce = 750; // Steering jets (how much you heel over when you turn)
   rollForce = 0;                 // Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 3;               // Height off the ground at rest
   createHoverHeight = 3;         // Height off the ground when created

   // turbo jet...
   jetForce = 3000;              // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 0;             // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 0.0;         // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 2.0;

   // Rigid body
   mass = 90;
   massCenter = "0 0 0";   // Center of mass for rigid body
   massBox = "0 0 0";      // Size of box used for moment of inertia, \
                           // if zero it defaults to object bounding box
   bodyFriction = 0.0;     // Don't mess with this.
   bodyRestitution = 0.1;  // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;       // Don't mess with this.
   softImpactSpeed = 14;   // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;   // Sound hooks. This is the hard hit.
   
   // physics system...
   integration = 4;           // # of physics steps per tick
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.1;          // Contact velocity tolerance

   // impact damage...
   minImpactSpeed = 1;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 100.0;   // Dynamic field: impact damage multiplier

   // contrail...
   minTrailSpeed = 10;      // The speed your contrail shows up at
   trailEmitter = RedScoutDrone_ContrailEmitter;
   
   // laser trail...
   //laserTrail = RedScoutDrone_LaserTrail;
   
   // various emitters...
   //forwardJetEmitter = ScoutDroneJetEmitter;
   //downJetEmitter = ScoutDroneJetEmitter;

   //
//   jetSound = Team1ScoutScoutDroneThrustSound;
//   engineSound = ScoutDroneEngineSound;
//   softImpactSound = SoftImpactSound;
//   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 10.0;
   mediumSplashSoundVelocity = 15.0;
   hardSplashSoundVelocity = 20.0;
   exitSplashSoundVelocity = 10.0;

//   exitingWater      = VehicleExitWaterMediumSound;
//   impactWaterEasy   = VehicleImpactWaterSoftSound;
//   impactWaterMedium = VehicleImpactWaterMediumSound;
//   impactWaterHard   = VehicleImpactWaterMediumSound;
//   waterWakeSound    = VehicleWakeMediumSplashSound;

//   dustEmitter = VehicleLiftoffDustEmitter;
   triggerDustHeight = 4.0;
   dustHeight = 1.0;

//   damageEmitter[0] = LightDamageSmoke;
//   damageEmitter[1] = HeavyDamageSmoke;
//   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "0.0 -3.0 0.0 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 1;

//   splashEmitter[0] = VehicleFoamDropletsEmitter;
//   splashEmitter[1] = VehicleFoamEmitter;

   //
   // dynamic fields for in-script vehicle-implementation...
   //

   mountable = false;
   mountPose[0] = "ScoutDrone";
};

