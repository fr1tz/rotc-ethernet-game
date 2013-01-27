//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock HoverVehicleData(HoverPod)
{
	//======================= Engine ==============================

	// *** ShapeBase ***

	isInvincible = true;

	// 3rd person camera
	 cameraMaxDist = 5;

	// Rigid body
	mass = 100;
	drag = 0.0;
	density = 0.3;

	// Rendering
	shapeFile = "share/shapes/rotc/vehicles/hoverpod/vehicle.dts";
	computeCRC = true;
	debrisShapeName = "share/shapes/rotc/misc/debris1.white.dts";
	debris = ShapeDebris;
	renderWhenDestroyed = false;

	// *** Vehicle ***

	// 3rd person camera
	cameraRoll = true;         // Roll the camera with the vehicle
	cameraOffset = 2.0;        // Vertical offset from camera mount point
	cameraLag = 0.05;           // Velocity lag of camera
	cameraDecay = 0.75;        // Decay per sec. rate of velocity lag

	// Rigid body
	bodyFriction = 0.5;
	bodyRestitution = 0.0;
	// Physics system
	integration = 4;           // # of physics steps per tick
	collisionTol = 0.1;        // Collision distance tolerance
	contactTol = 0.1;          // Contact velocity tolerance

	// Drag
	minDrag = 60; // Linear Drag (eventually slows you down when not thrusting...constant drag)

	// *** HoverVehicle ***
	
	// Drag
	gyroDrag = 36; 
	dragForce = 100*6; // 1.9
	vertFactor = 1; // factor for vertical velocity drag

	normalForce = 20*20;
	restorativeForce = 10*20;

	mainThrustForce = 3000*20;
	reverseThrustForce = 0*20;
	strafeThrustForce = 500*20;
	floatingGravMag = 25; // factor applied to gravity when floating
	floatingThrustFactor = 0.35; // factor applied to thrust force when floating
	turboFactor = 1.5; // factor applied to thrust force when jetting
	steeringForce = 100*20;
	rollForce = 7*20;
	pitchForce = 5*20;

	// braking force  is applied only when not thrusting 
	// and the speed is less than brakingActivationSpeed
	brakingForce = 30*20;
	brakingActivationSpeed = 30; 

	// Stabilizer "spring"
	stabLenMin = 4;
	stabLenMax = 8;
	stabSpringConstant = 25*20;
	stabDampingConstant = 10*20;

	//damageEmitter[0] = AssaultTankEmitter;
	//damageEmitter[1] = AssaultTankEmitter;
	//damageEmitter[2] = AssaultTankEmitter;
	//damageEmitterOffset[0] = "0.0 -1.5 0.5 ";
	//damageLevelTolerance[0] = 0.3;
	//damageLevelTolerance[1] = 0.7;
	//numDmgEmitterAreas = 2;

	//======================= Script ==============================

	category = "Vehicles";
	
	mountPose[0] = scoutRoot;

	numMountPoints = 3;
	isProtectedMountPoint[0] = true;
	//explosion = AssaultTankExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;
	
	lightOnly = 1;
	
	maxDamage = 120;
	destroyedLevel = 120;
	
	isShielded = true;
	rechargeRate = 0.7;
	energyPerDamagePoint = 75;
	maxEnergy = 150;
	minJetEnergy = 15;
	jetEnergyDrain = 1.3;
	
	// Rigid Body



	softImpactSpeed = 20; // Play SoftImpact Sound
	hardImpactSpeed = 28; // Play HardImpact Sound
	
	// Ground Impact Damage (uses DamageType::Ground)
	minImpactSpeed = 29;
	speedDamageScale = 0.010;
	
	// Object Impact Damage (uses DamageType::Impact)
	collDamageThresholdVel = 23;
	collDamageMultiplier = 0.030;
	


	

	
	//dustEmitter = CatSlideContactTrailEmitter;
	triggerDustHeight = 2.5;
	dustHeight = 1.0;
	//dustTrailEmitter = CatSkidTrailEmitter0;
	dustTrailOffset = "0.0 -1.0 0.5";
	triggerTrailHeight = 3.6;
	dustTrailFreqMod = 15.0;
	
	//jetSound = ScoutSqueelSound;
	//engineSound = ScoutEngineSound;
	//floatSound = ScoutThrustSound;
	//softImpactSound = GravSoftImpactSound;
	//hardImpactSound = HardImpactSound;
	//wheelImpactSound = WheelImpactSound;
	
	//
	softSplashSoundVelocity = 10.0;
	mediumSplashSoundVelocity = 20.0;
	hardSplashSoundVelocity = 30.0;
	exitSplashSoundVelocity = 10.0;
	
	//exitingWater = VehicleExitWaterSoftSound;
	//impactWaterEasy = VehicleImpactWaterSoftSound;
	//impactWaterMedium = VehicleImpactWaterSoftSound;
	//impactWaterHard = VehicleImpactWaterMediumSound;
	//waterWakeSound = VehicleWakeSoftSplashSound;
	
	minMountDist = 4;
	

	
	//splashEmitter[0] = VehicleFoamDropletsEmitter;
	//splashEmitter[1] = VehicleFoamEmitter;
	
	//forwardJetEmitter = ContrailEmitter;
	
	//cmdCategory = Tactical;
	//cmdIcon = CMDHoverScoutIcon;
	//cmdMiniIconName = "commander/MiniIcons/com_landscout_grey";
	//targetNameTag = 'WildCat';
	//targetTypeTag = 'Grav Cycle';
	//sensorData = VehiclePulseSensor;
	
	//checkRadius = 1.7785;
	//observeParameters = "1 10 10";
	
	//runningLight[0] = WildcatLight1;
	//runningLight[1] = WildcatLight2;
	//runningLight[2] = WildcatLight3;
	
	//shieldEffectScale = "0.9375 1.125 0.6";
};