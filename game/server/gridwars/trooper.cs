//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("share/shapes/infantry/players/lightcat/player.cs");

exec("./trooper.sfx.cs");
exec("./trooper.gfx.cs");

datablock PlayerData(RedTrooper)
{
	className = Trooper;
	
	firstPersonOnly = true;
	
	targetLockTimeMS = 200;
	
	hudImageNameFriendly = "~/client/ui/hud/pixmaps/teammate.cat.png";
	hudImageNameEnemy = "~/client/ui/hud/pixmaps/enemy.cat.png";

	useEyePoint = true;
	renderFirstPerson = true;
	emap = true;

    eyeOffset = "0 -0.2 -0.02";
    cameraDefaultFov = 110.0;
	cameraMinFov	  = 30.0;
	cameraMaxFov	  = 130.0;
	cameraMinDist = 1;
	cameraMaxDist = 5;

	shapeFile = "share/shapes/infantry/players/lightcat/player.red.dts";
 
    //cloakTexture = "share/shapes/rotc/effects/explosion_white.png";
    shapeFxTexture[0] = "share/textures/rotc/heating.png";
    shapeFxTexture[1] = "share/textures/rotc/heat.red.png";
    shapeFxTexture[2] = "share/textures/rotc/locked.png";
    shapeFxTexture[3] = "share/textures/rotc/energy.png";

	computeCRC = true;

	canObserve = true;
	cmdCategory = "Clients";

	renderWhenDestroyed = false;
	debrisShapeName = "share/shapes/rotc/players/standardcat/debris.red.dts";
	debris = TrooperDebris;

	aiAvoidThis = true;

	minLookAngle = -1.5;
	maxLookAngle = 1.5;
	minLookAngleMarching = -1.5;
	maxLookAngleMarching = 1.5;
	minLookAngleCrouched = -1.5;
	maxLookAngleCrouched = 1.5;
	minLookAngleProne = -0.8;
	maxLookAngleProne = 0.8;

	maxFreelookAngle = 3.0;

	mass = 30;
	drag = 0.0;
	density = 10;

	maxDamage = 75;
	damageBuffer = 25;
	maxEnergy = 100;

	repairRate = 0.8;
	damageBufferRechargeRate = 0.15;
	damageBufferDischargeRate = 0.05;
	energyRechargeRate = 0.4;

	flyForce = 0;

	runForce = 15 * 90;
	runEnergyDrain = 0;
	minRunEnergy = 0;
	
	slideForce = 20 * 90;
	slideEnergyDrain = 0;
	minSlideEnergy = 0;

	maxForwardSpeed = 12;
	maxBackwardSpeed = 10;
	maxSideSpeed = 10;
	maxForwardSpeedSliding = 30;
	maxBackwardSpeedSliding = 25;
	maxSideSpeedSliding = 12;
	maxForwardSpeedMarching = 6;
	maxBackwardSpeedMarching = 6;
	maxSideSpeedMarching = 6;
//	maxForwardSpeedCrouched = 15; NOT USED
//	maxBackwardSpeedCrouched = 12; NOT USED
//	maxSideSpeedCrouched = 10; NOT USED
//	maxForwardSpeedProne = 3; NOT USED
//	maxBackwardSpeedProne = 3; NOT USED
//	maxSideSpeedProne = 2; NOT USED

	maxUnderwaterForwardSpeed = 8.4;
	maxUnderwaterBackwardSpeed = 7.8;
	maxUnderwaterSideSpeed = 7.8;
	// [todo: insert values for other body poses here?]

	jumpForce = 3 * 90;  // 12 * 90
	jumpEnergyDrain = 0;
    reJumpForce = 10 * 90; // script field
    reJumpEnergyDrain = 20; // script field
	minJumpEnergy = 20;
	jumpDelay = 0;
	
	recoverDelay = 9;
	recoverRunForceScale = 1.2;

	minImpactSpeed = 30; //
	speedDamageScale = 3.0; // dynamic field: impact damage multiplier

	boundingBox = "1.2 1.1 2.7";
	pickupRadius = 0.75;

	// Controls over slope of runnable/jumpable surfaces
	maxStepHeight = 1.0;
	maxStepHeightMarching = 1.0;
	maxStepHeightCrouched = 1.0;
	maxStepHeightProne = 0.2;
	runSurfaceAngle  = 40;
	runSurfaceAngleMarching  = 40;
	runSurfaceAngleCrouched  = 40;
	runSurfaceAngleProne  = 50;

	jumpSurfaceAngle = 30;

	minJumpSpeed = 20;
	maxJumpSpeed = 30;

	horizMaxSpeed = 200;
	horizResistSpeed = 30;
	horizResistFactor = 0.35;

	upMaxSpeed = 65;
	upResistSpeed = 25;
	upResistFactor = 0.3;

	footstepSplashHeight = 0.35;

	// Damage location details
	boxNormalHeadPercentage		 = 0.83;
	boxNormalTorsoPercentage		= 0.49;
	boxHeadLeftPercentage			= 0;
	boxHeadRightPercentage		  = 1;
	boxHeadBackPercentage			= 0;
	boxHeadFrontPercentage		  = 1;

	// footprints
	decalData	= RedTrooperFootprint;
	decalOffset = 0.25;

	// foot puffs
	footPuffEmitter = TrooperLightPuffEmitter;
	footPuffNumParts = 10;
	footPuffRadius = 0.25;

	// slide emitter
	slideParticleEmitter = RedSlideEmitter;
	
	// damage info eyecandy...
	damageBufferParticleEmitter = RedCatDamageBufferEmitter;
	repairParticleEmitter = RedCatRepairEmitter;
	bufferRepairParticleEmitter = RedCatBufferRepairEmitter;
	damageDebris = RedCatDamageDebris;
	bufferDamageDebris = CatBufferDamageDebris;

	// not implemented in engine...
	// dustEmitter = TrooperLiftoffDustEmitter;

	splash = TrooperSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 0.4;
	splashEmitter[0] = TrooperFoamDropletsEmitter;
	splashEmitter[1] = TrooperFoamEmitter;
	splashEmitter[2] = TrooperBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;
	hardSplashSoundVelocity = 20.0;
	exitSplashSoundVelocity = 5.0;

	//NOTE:  some sounds commented out until wav's are available

	// Footstep Sounds
	LeftFootSoftSound		= TrooperLeftFootSoftSound;
	LeftFootHardSound		= TrooperLeftFootHardSound;
	LeftFootMetalSound	  = TrooperLeftFootMetalSound;
	LeftFootSnowSound		= TrooperLeftFootSnowSound;
	LeftFootShallowSound	= TrooperLeftFootShallowSplashSound;
	LeftFootWadingSound	 = TrooperLeftFootWadingSound;
	RightFootSoftSound	  = TrooperRightFootSoftSound;
	RightFootHardSound	  = TrooperRightFootHardSound;
	RightFootMetalSound	 = TrooperRightFootMetalSound;
	RightFootSnowSound	  = TrooperRightFootSnowSound;
	RightFootShallowSound  = TrooperRightFootShallowSplashSound;
	RightFootWadingSound	= TrooperRightFootWadingSound;
	FootUnderwaterSound	 = TrooperFootUnderwaterSound;
	//FootBubblesSound	  = FootBubblesSound;
	//movingBubblesSound	= ArmorMoveBubblesSound;
	//waterBreathSound	  = WaterBreathMaleSound;

	impactSoftSound		= TrooperImpactSoftSound;
	impactHardSound		= TrooperImpactHardSound;
	impactMetalSound	  = TrooperImpactMetalSound;
	impactSnowSound		= TrooperImpactSnowSound;

	//impactWaterEasy		= ImpactLightWaterEasySound;
	//impactWaterMedium	 = ImpactLightWaterMediumSound;
	//impactWaterHard		= ImpactLightWaterHardSound;

	groundImpactMinSpeed	 = 10.0;
	groundImpactShakeFreq	= "4.0 4.0 4.0";
	groundImpactShakeAmp	 = "1.0 1.0 1.0";
	groundImpactShakeDuration = 0.8;
	groundImpactShakeFalloff = 10.0;

	//exitingWater			= ExitingWaterLightSound;
	slideSound = PlayerSlideSound;
	slideContactSound = PlayerSlideContactSound;

	observeParameters = "0.5 4.5 4.5";
};

datablock PlayerData(BlueTrooper : RedTrooper)
{
	shapeFile = "share/shapes/infantry/players/lightcat/player.blue.dts";
    shapeFxTexture[1] = "share/textures/rotc/heat.blue.png";
	debrisShapeName = "share/shapes/rotc/players/standardcat/debris.blue.dts";
	decalData = BlueTrooperFootprint;
	slideParticleEmitter = BlueSlideEmitter;
	damageBufferParticleEmitter = BlueCatDamageBufferEmitter;
	repairParticleEmitter = BlueCatRepairEmitter;
	bufferRepairParticleEmitter = BlueCatBufferRepairEmitter;
	damageDebris = BlueCatDamageDebris;
	bufferDamageDebris = CatBufferDamageDebris;
};
