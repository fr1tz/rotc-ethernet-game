//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function StandardCat::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);
	%obj.setDiscs(0);
}

function StandardCat::useWeapon(%this, %obj, %nr)
{
	%client = %obj.client;

	if(%nr == 0)
		%obj.currWeapon++;
	else
		%obj.currWeapon = %nr;
	
	if(%obj.currWeapon > 2)
		%obj.currWeapon = 1;	

	%wpn =	%obj.currWeapon;

	if(%wpn == 1)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedSTG90Image, 0, -1, true);
		else
			%obj.mountImage(RedSTG90Image, 0, -1, true);
	}
	else if(%wpn == 2)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedFF3Image, 0, -1, true);
		else
			%obj.mountImage(BlueFF3Image, 0, -1, true);
	}
}



datablock TSShapeConstructor(RedSoldierCatDts)
{
	baseShape = "share/shapes/inf/soldier-0.red.dts";
	sequenceBaseDir = "share/shapes/rotc/players/a/";

	// movement when standing...
	sequence0  = "tl/root.dsq root";
	sequence1  = "nm/run.dsq run";
	sequence2  = "nm/back.dsq back";
	sequence3  = "nm/side.dsq side";

	// movement when marching...
	sequence4  = "tl/root.dsq rootMarching";
	sequence5  = "nm/run.dsq runMarching";
	sequence6  = "nm/back.dsq backMarching";
	sequence7  = "nm/side.dsq sideMarching";

	// movement when crouched...
	sequence8  = "tl/rootcrouched.dsq rootCrouched";
	sequence9  = "nm/rootcrouched.dsq runCrouched";
	sequence10  = "nm/rootcrouched.dsq backCrouched";
	sequence11  = "nm/rootcrouched.dsq sideCrouched";

	// movement when prone...
	sequence12 = "fb/rootprone.dsq rootProne";
	sequence13 = "fb/rootprone.dsq rootProne";
	sequence14 = "fb/rootprone.dsq rootProne";
	sequence15 = "fb/rootprone.dsq rootProne";

	// arm threads...
	sequence16 = "a/holdnoweapon.dsq look";
	sequence17 = "a/discdeflect_left_base.dsq discdeflect_left_base";
	sequence18 = "a/holdgun_onehand.dsq look2";
	sequence52 = "a/holdblaster.dsq holdblaster";
	sequence53 = "ub/aimblaster.dsq aimblaster";
	sequence19 = "ub/holdrifle.dsq holdrifle";
	sequence51 = "ub/aimrifle.dsq aimrifle";
	sequence20 = "ub/holdshield.dsq holdshield";
	sequence46 = "a/holdspear.dsq holdspear";
	sequence47 = "a/holdaimspear.dsq holdaimspear";

	// other...
	sequence21 = "nm/diehead.dsq death1";
	sequence22 = "nm/diechest.dsq death2";
	sequence23 = "nm/dieback.dsq death3";
	sequence24 = "nm/diesidelf.dsq death4";
	sequence25 = "nm/diesidert.dsq death5";
	sequence26 = "nm/dieleglf.dsq death6";
	sequence27 = "nm/dielegrt.dsq death7";
	sequence28 = "nm/dieslump.dsq death8";
	sequence29 = "nm/dieknees.dsq death9";
	sequence30 = "nm/dieforward.dsq death10";
	sequence31 = "nm/diespin.dsq death11";

	sequence32 = "nm/headside.dsq headside";
	sequence33 = "nm/recoilde.dsq light_recoil";
	sequence34 = "nm/sitting.dsq sitting";
	sequence35 = "fb/cel_headbang.dsq celsalute";
	sequence36 = "nm/tauntbest.dsq celwave";
	sequence37 = "nm/standjump.dsq standjump";

	sequence38 = "nm/head.dsq head";
	sequence39 = "nm/fall.dsq fall";
	sequence40 = "nm/land.dsq land";
	sequence41 = "nm/jump.dsq jump";

	sequence42 = "fb/cel_hail.dsq celhail";

	sequence43 = "ub/throwsidearm.dsq throwSidearm";
	sequence44 = "ub/firearmcannon.dsq fireArmcannon";
	sequence48 = "ub/aimspear.dsq aimSpear";
	sequence49 = "ub/throwSpear.dsq throwSpear";
	sequence50 = "ub/discdeflect_left_anim.dsq discdeflect_left_anim";
	sequence54 = "ub/throwinterceptor.dsq throwInterceptor";

	sequence45 = "fb/flyer.dsq flyer";

	sequence55  = "b/slide.dsq slide";
};

datablock TSShapeConstructor(BlueSoldierCatDts : RedSoldierCatDts)
{
	baseShape = "share/shapes/inf/soldier-0.blu.dts";
};

exec("./infantrycat.sfx.cs");
exec("./infantrycat.gfx.cs");

datablock PlayerData(RedInfantryCat)
{
	className = StandardCat;
	
	firstPersonOnly = false;
	
	targetLockTimeMS = 200;
	
	hudImageNameFriendly = "~/client/ui/hud/pixmaps/teammate.cat.png";
	hudImageNameEnemy = "~/client/ui/hud/pixmaps/enemy.cat.png";

	useEyePoint = true;
	renderFirstPerson = true;
	emap = true;

   eyeOffset = "0 -0.2 -0.02";
   cameraDefaultFov = 110.0;
	cameraMinFov	  = 70.0;
	cameraMaxFov	  = 130.0;
	cameraMinDist = 1;
	cameraMaxDist = 5;

	shapeFile = "share/shapes/inf/soldier-0.red.dts";
 
	//cloakTexture = "share/shapes/rotc/effects/explosion_white.png";
	shapeFxTexture[0] = "share/textures/rotc/white.png";
	shapeFxTexture[1] = "share/textures/rotc/connection.png";
	shapeFxTexture[2] = "share/textures/rotc/barrier.green.png";
	shapeFxTexture[3] = "share/textures/rotc/armor.white.png";
	shapeFxTexture[4] = "share/textures/rotc/armor.orange.png";

	shapeFxColor[0] = "1.0 1.0 1.0 1.0";  
	shapeFxColor[1] = "1.0 0.0 0.0 1.0";
	shapeFxColor[2] = "1.0 0.5 0.0 1.0";  
	shapeFxColor[3] = "0.0 0.5 1.0 1.0";  
	shapeFxColor[4] = "1.0 0.0 0.0 1.0"; // repel hit
	shapeFxColor[5] = "1.0 0.5 0.5 1.0"; // repel missed
	shapeFxColor[6] = "0.0 0.5 0.0 1.0"; // permanently neutral zone

	computeCRC = true;

	canObserve = true;
	cmdCategory = "Clients";

	renderWhenDestroyed = false;
	debrisShapeName = "share/shapes/rotc/players/standardcat/debris.red.dts";
	debris = StandardCatDebris;

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

	mass = 90;
	drag = 0.0;
	density = 10;
	gravityMod = 1.0;

	maxDamage = 100;
	damageBuffer = 0;
	maxEnergy = 100;

	repairRate = 0.8;
	damageBufferRechargeRate = 0.0;
	damageBufferDischargeRate = 0.0;
	energyRechargeRate = 0.4;

	skidSpeed = 20;
	skidFactor = 0.4;

	flyForce = 0;

	runForce = 50 * 90; // formerly 48 * 90
	runEnergyDrain = 0;
	minRunEnergy = 0;
	
	slideForce = 20 * 90;
	slideEnergyDrain = 0;
	minSlideEnergy = 0;

	maxForwardSpeed = 10;
	maxBackwardSpeed = 8;
	maxSideSpeed = 8;
	maxForwardSpeedSliding = 30;
	maxBackwardSpeedSliding = 25;
	maxSideSpeedSliding = 12;
	maxForwardSpeedMarching = 8;
	maxBackwardSpeedMarching = 8;
	maxSideSpeedMarching = 8;
	maxForwardSpeedCrouched = 3;
	maxBackwardSpeedCrouched = 3;
	maxSideSpeedCrouched = 3;
//	maxForwardSpeedProne = 3; NOT USED
//	maxBackwardSpeedProne = 3; NOT USED
//	maxSideSpeedProne = 2; NOT USED

	maxUnderwaterForwardSpeed = 8.4;
	maxUnderwaterBackwardSpeed = 7.8;
	maxUnderwaterSideSpeed = 7.8;
	// [todo: insert values for other body poses here?]

	jumpForce = 8 * 90;  // 12 * 90
	jumpEnergyDrain = 0;
   reJumpForce = 10 * 90; // script field
   reJumpEnergyDrain = 20; // script field
	minJumpEnergy = 0;
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
	runSurfaceAngle  = 35;
	runSurfaceAngleMarching  = 35;
	runSurfaceAngleCrouched  = 35;
	runSurfaceAngleProne  = 35;

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
	decalData	= SoldierFootprint;
	decalOffset = 0.25;

	// foot puffs
	footPuffEmitter = SoldierFootPuffEmitter;
	footPuffNumParts = 10;
	footPuffRadius = 0.5;
	
	// ground connection beam
	groundConnectionBeam = InfantryCatGroundConnectionBeam;

   numShapeTrails = 0;

	// slide emitters
	//slideContactParticleFootEmitter = RedSlideEmitter;
	slideContactParticleTrailEmitter[0] = CatSlideContactTrailEmitter;
	//slideParticleFootEmitter = RedCatSlideFootEmitter;
	//slideParticleTrailEmitter[0] = BlueSlideEmitter;
	skidParticleFootEmitter = CatSkidFootEmitter;
	skidParticleTrailEmitter[0] = CatSkidTrailEmitter0;
	skidParticleTrailEmitter[1] = CatSkidTrailEmitter1;
	
	// damage info eyecandy...
	damageBufferParticleEmitter = RedCatDamageBufferEmitter;
	repairParticleEmitter = RedCatRepairEmitter;
	bufferRepairParticleEmitter = RedCatBufferRepairEmitter;
	damageParticleEmitter = RedCatDamageEmitter;
	//bufferDamageParticleEmitter = RedCatBufferDamageEmitter;
	//damageDebris = RedCatDamageDebris;
	//bufferDamageDebris = CatBufferDamageDebris;

	// not implemented in engine...
	// dustEmitter = StandardCatLiftoffDustEmitter;

	splash = StandardCatSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 0.4;
	splashEmitter[0] = StandardCatFoamDropletsEmitter;
	splashEmitter[1] = StandardCatFoamEmitter;
	splashEmitter[2] = StandardCatBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;
	hardSplashSoundVelocity = 20.0;
	exitSplashSoundVelocity = 5.0;

	//NOTE:  some sounds commented out until wav's are available

	// Footstep Sounds
	LeftFootSoftSound		= InfantryCatLeftFootSoftSound;
	LeftFootHardSound		= InfantryCatLeftFootHardSound;
	LeftFootMetalSound	  = InfantryCatLeftFootMetalSound;
	LeftFootSnowSound		= InfantryCatLeftFootSnowSound;
	LeftFootShallowSound	= InfantryCatLeftFootShallowSplashSound;
	LeftFootWadingSound	 = InfantryCatLeftFootWadingSound;
	RightFootSoftSound	  = InfantryCatRightFootSoftSound;
	RightFootHardSound	  = InfantryCatRightFootHardSound;
	RightFootMetalSound	 = InfantryCatRightFootMetalSound;
	RightFootSnowSound	  = InfantryCatRightFootSnowSound;
	RightFootShallowSound  = InfantryCatRightFootShallowSplashSound;
	RightFootWadingSound	= InfantryCatRightFootWadingSound;
	FootUnderwaterSound	 = InfantryCatFootUnderwaterSound;
	//FootBubblesSound	  = FootBubblesSound;
	//movingBubblesSound	= ArmorMoveBubblesSound;
	//waterBreathSound	  = WaterBreathMaleSound;

	impactSoftSound		= InfantryCatImpactSoftSound;
	impactHardSound		= InfantryCatImpactHardSound;
	impactMetalSound	  = InfantryCatImpactMetalSound;
	impactSnowSound		= InfantryCatImpactSnowSound;

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
	skidSound = PlayerSkidSound;

	observeParameters = "0.5 4.5 4.5";
};

datablock PlayerData(RedInfantryCatSliding : RedInfantryCat)
{
   numShapeTrails = 10;
	flyForce = 15 * 90;
   skidSpeed = 30;
	maxForwardSpeed = 18;
	maxBackwardSpeed = 16;
	maxSideSpeed = 12;
};

datablock PlayerData(RedInfantryCatCrouched : RedInfantryCat)
{
	boundingBox = "1.2 1.1 1.5";
};

datablock PlayerData(RedInfantryCatOrb : RedInfantryCat)
{
	shapeFile = "share/shapes/inf/soldierorb-0.red.dts";
	boundingBox = "1.1 1.1 1.1";
	slideForce = 15 * 90;
	maxForwardSpeedSliding = 1;
	maxBackwardSpeedSliding = 1;
	maxSideSpeedSliding = 1;
	jumpForce = 0;
	jumpEnergyDrain = 0;
   reJumpForce = 0; // script field
   reJumpEnergyDrain = 0; // script field
	minJumpEnergy = 0;
	jumpDelay = 0;
};

//------------------------------------------------------------------------------

datablock PlayerData(BlueInfantryCat : RedInfantryCat)
{
	shapeFile = "share/shapes/inf/soldier-0.blu.dts";
	shapeFxTexture[2] = "share/textures/rotc/barrier.orange.png";
	shapeFxTexture[4] = "share/textures/rotc/armor.cyan.png";
	shapeFxColor[0] = "1.0 1.0 1.0 1.0";
	shapeFxColor[1] = "0.0 0.0 1.0 1.0";
	shapeFxColor[2] = "0.0 0.5 1.0 1.0";
	shapeFxColor[3] = "1.0 0.5 0.0 1.0";
	shapeFxColor[4] = "0.0 0.0 1.0 1.0";
	shapeFxColor[5] = "0.5 0.5 1.0 1.0";
	debrisShapeName = "share/shapes/rotc/players/standardcat/debris.blue.dts";
	decalData = BlueStandardCatFootprint;
//	slideParticleFootEmitter = BlueCatSlideFootEmitter;
	damageBufferParticleEmitter = BlueCatDamageBufferEmitter;
	repairParticleEmitter = BlueCatRepairEmitter;
	bufferRepairParticleEmitter = BlueCatBufferRepairEmitter;
	damageParticleEmitter = BlueCatDamageEmitter;
//	bufferDamageParticleEmitter = BlueCatBufferDamageEmitter;
//	damageDebris = BlueCatDamageDebris;
//	bufferDamageDebris = CatBufferDamageDebris;
};

datablock PlayerData(BlueInfantryCatSliding : BlueInfantryCat)
{
   numShapeTrails = 10;
	flyForce = 15 * 90;
   skidSpeed = 30;
	maxForwardSpeed = 20;
	maxBackwardSpeed = 16;
	maxSideSpeed = 16;
};

datablock PlayerData(BlueInfantryCatCrouched : BlueInfantryCat)
{
	boundingBox = "1.2 1.1 1.5";
};

datablock PlayerData(BlueInfantryCatOrb : BlueInfantryCat)
{
	shapeFile = "share/shapes/inf/soldierorb-0.blu.dts";
	boundingBox = "1.1 1.1 1.1";
	slideForce = 15 * 90;
	maxForwardSpeedSliding = 1;
	maxBackwardSpeedSliding = 1;
	maxSideSpeedSliding = 1;
	jumpForce = 0;
	jumpEnergyDrain = 0;
   reJumpForce = 0; // script field
   reJumpEnergyDrain = 0; // script field
	minJumpEnergy = 0;
	jumpDelay = 0;
   numShapeTrails = 0;
};




