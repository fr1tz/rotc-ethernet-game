//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// particle emitter

datablock ParticleData(RedExplosiveDisc_ParticleEmitter_Particle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 300;
	lifetimeVarianceMS	= 100;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 0.0 0.0 0.5";
	colors[2]	  = "1.0 0.0 0.0 0.0";
	sizes[0]		= 0.9;
	sizes[1]		= 0.9;
	sizes[2]		= 0.5;
	times[0]		= 0.0;
	times[1]		= 0.2;
	times[2]		= 1.0;
	textureName	= "share/textures/rotc/dustParticle";
	allowLighting = false;
};

datablock ParticleEmitterData(RedExplosiveDisc_ParticleEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 5;
	velocityVariance = 0;
	ejectionOffset   = 0;
	thetaMin         = 20;
	thetaMax         = 30;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS       = 0; // forever
	particles        = RedExplosiveDisc_ParticleEmitter_Particle;
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(RedExplosiveDisc_LaserTrail)
{
	hasLine = false;
	lineColor	= "1.00 1.00 1.00 0.02";

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = false;
	outerColor = "1.00 1.00 1.00 0.02";
	outerWidth = "0.05";

	bitmap = "share/textures/rotc/smoke_particle";
	bitmapWidth = 0.5;

	blendMode = 1;
 
    windCoefficient = 0.0;

    // node x movement...
    nodeMoveMode[0]     = $NodeMoveMode::None;
    nodeMoveSpeed[0]    = -0.002;
    nodeMoveSpeedAdd[0] =  0.004;
    // node y movement...
    nodeMoveMode[1]     = $NodeMoveMode::None;
    nodeMoveSpeed[1]    = -0.002;
    nodeMoveSpeedAdd[1] =  0.004;
    // node z movement...
    nodeMoveMode[2]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[2]    = 0.2;
    nodeMoveSpeedAdd[2] = 0.5;

	//nodeDistance = 1;
 
	fadeTime = 2000;
};

//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(RedExplosiveDiscExplosion_Cloud)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 600;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/corona.png";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.0";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 6.0;
	sizes[1]		= 6.0;
	sizes[2]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 0.2;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(RedExplosiveDiscExplosion_CloudEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 6.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "RedExplosiveDiscExplosion_Cloud";
};

datablock ParticleData(RedExplosiveDiscExplosion_Dust)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= -0.01;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = true;
	spinRandomMin		  = -90.0;
	spinRandomMax		  = 500.0;
	textureName			 = "share/textures/rotc/smoke_particle.png";
	colors[0]	  = "0.9 0.9 0.9 0.5";
	colors[1]	  = "0.9 0.9 0.9 0.5";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 3.2;
	sizes[1]		= 4.6;
	sizes[2]		= 5.0;
	times[0]		= 0.0;
	times[1]		= 0.7;
	times[2]		= 1.0;
	allowLighting = true;
};

datablock ParticleEmitterData(RedExplosiveDiscExplosion_DustEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 15.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	lifetimeMS		 = 250;
	particles = "RedExplosiveDiscExplosion_Dust";
};


datablock ParticleData(RedExplosiveDiscExplosion_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.5;	// rises slowly
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 1250;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  true;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/smoke_particle.png";

	colors[0]	  = "0.9 0.9 0.9 0.4";
	colors[1]	  = "0.9 0.9 0.9 0.2";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 6.0;
	sizes[2]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(RedExplosiveDiscExplosion_SmokeEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 6.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

	lifetimeMS		 = 250;

	particles = "RedExplosiveDiscExplosion_Smoke";
};

datablock ParticleData(RedExplosiveDiscExplosion_Sparks)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 350;
	textureName			 = "share/textures/rotc/particle1.png";
	colors[0]	  = "0.56 0.36 0.26 1.0";
	colors[1]	  = "0.56 0.36 0.26 1.0";
	colors[2]	  = "1.0 0.36 0.26 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.75;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	allowLighting = false;
};

datablock ParticleEmitterData(RedExplosiveDiscExplosion_SparksEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 12;
	velocityVariance = 6.75;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 60;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 100;
	particles = "RedExplosiveDiscExplosion_Sparks";
};

datablock DebrisData(RedExplosiveDiscExplosion_SmallDebris)
{
	// shape...
	shapeFile = "share/shapes/rotc/misc/debris1.white.dts";

	// bounce...
	staticOnMaxBounce = true;
	numBounces = 5;

	// physics...
	gravModifier = 2.0;
	elasticity = 0.6;
	friction = 0.1;

	// spin...
	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	// lifetime...
	lifetime = 2.0;
	lifetimeVariance = 1.0;
};

datablock MultiNodeLaserBeamData(RedExplosiveDiscExplosion_LargeDebris_LaserTrail)
{
	hasLine = true;
	lineColor	= "1.00 1.00 1.00 0.5";

	hasInner = false;
	innerColor = "1.00 1.00 0.00 0.3";
	innerWidth = "0.20";

	hasOuter = true;
	outerColor = "1.00 1.00 1.00 0.2";
	outerWidth = "0.40";

//	bitmap = "share/shapes/rotc/weapons/missilelauncher/explosion.trail";
//	bitmapWidth = 0.25;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 1000;
};

datablock ParticleData(RedExplosiveDiscExplosion_LargeDebris_Particles2)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 0;
	textureName			 = "share/textures/rotc/cross1";
	colors[0]	  = "1.0 1.0 1.0 0.6";
	colors[1]	  = "1.0 1.0 1.0 0.4";
	colors[2]	  = "1.0 1.0 1.0 0.2";
	colors[3]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	sizes[3]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.333;
	times[2]		= 0.666;
	times[3]		= 1.0;
};

datablock ParticleEmitterData(RedExplosiveDiscExplosion_LargeDebris_Emitter2)
{
	ejectionPeriodMS = 30;
	periodVarianceMS = 0;
	ejectionVelocity = 2.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 45;
	thetaMax			= 45;
	phiReferenceVel  = 75000;
	phiVariance		= 0;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "RedExplosiveDiscExplosion_LargeDebris_Particles2";
};

datablock ParticleData(RedExplosiveDiscExplosion_LargeDebris_Particles1)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 100;
	lifetimeVarianceMS	= 0;
	textureName			 = "share/textures/rotc/cross1";
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 1.0";
	colors[2]	  = "1.0 1.0 1.0 0.5";
	colors[3]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 2.0;
	sizes[2]		= 2.0;
	sizes[3]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 0.333;
	times[2]		= 0.666;
	times[3]		= 1.0;
};

datablock ParticleEmitterData(RedExplosiveDiscExplosion_LargeDebris_Emitter1)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 10.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "RedExplosiveDiscExplosion_LargeDebris_Particles1";
};

datablock ExplosionData(RedExplosiveDiscExplosion_LargeDebris_Explosion)
{
	soundProfile	= MissileLauncherDebrisSound;
	
	debris = RedExplosiveDiscExplosion_SmallDebris;
	debrisThetaMin = 0;
	debrisThetaMax = 60;
	debrisNum = 5;
	debrisVelocity = 15.0;
	debrisVelocityVariance = 10.0;
};

datablock DebrisData(RedExplosiveDiscExplosion_LargeDebris)
{
	// shape...
	shapeFile = "share/shapes/rotc/misc/debris2.white.dts";

	explosion = RedExplosiveDiscExplosion_LargeDebris_Explosion;

	//laserTrail = RedExplosiveDiscExplosion_LargeDebris_LaserTrail;
	emitters[0] = RedExplosiveDiscExplosion_LargeDebris_Emitter2;
	//emitters[1] = RedExplosiveDiscExplosion_LargeDebris_Emitter1;

	// bounce...
	numBounces = 0;
	explodeOnMaxBounce = true;

	// physics...
	gravModifier = 2.0;
	elasticity = 0.6;
	friction = 0.1;

	// spin...
	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	// lifetime...
	lifetime = 20.0;
	lifetimeVariance = 0.0;
};

datablock ExplosionData(RedExplosiveDiscExplosion)
{
	soundProfile = ExplosiveDiscExplosionSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";
	
	lifetimeMS = 200;

//	debris = RedExplosiveDiscExplosion_LargeDebris;
//	debrisThetaMin = 0;
//	debrisThetaMax = 60;
//	debrisNum = 5;
//	debrisVelocity = 30.0;
//	debrisVelocityVariance = 10.0;

//	particleEmitter = RedExplosiveDiscExplosion_CloudEmitter;
//	particleDensity = 100;
//	particleRadius = 4;

	emitter[0] = RedExplosiveDiscExplosion_DustEmitter;
	emitter[1] = RedExplosiveDiscExplosion_SmokeEmitter;
	emitter[2] = RedExplosiveDiscExplosion_SparksEmitter;

	// Camera shake
	shakeCamera = true;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;
	
	// Dynamic light
	lightStartRadius = 10;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
};


//-----------------------------------------------------------------------------
// deflected effect

datablock ExplosionData(RedExplosiveDiscDeflectedEffect)
{
	soundProfile	= DiscDeflectedSound;

	explosionShape = "share/shapes/rotc/effects/explosion_white.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.2 0.2 0.2";
	sizes[1] = "1.0 1.0 1.0";
	times[0] = 0.0;
	times[1] = 1.0;

//	debris = Team1AntiairNearEnemyExplosionDebris;
//	debrisThetaMin = 0;
//	debrisThetaMax = 180;
//	debrisNum = 15;
//	debrisVelocity = 20.0;
//	debrisVelocityVariance = 15.0;

//	emitter[0] = Team1AntiairNearEnemyExplosion_CloudEmitter;
//	emitter[0] = Team1AntiairNearEnemyExplosion_Cloud2Emitter;

	// Camera Shaking
	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 20;
	lightEndRadius = 2;
	lightStartColor = "1.0 1.0 1.0 1.0";
	lightEndColor = "1.0 1.0 1.0 0.3";
};

//-----------------------------------------------------------------------------
// bounce effect

datablock ExplosionData(RedExplosiveDiscBounceEffect)
{
	soundProfile	= DiscBounceSound;

	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion_white.dts";
	faceViewer	  = false;
	playSpeed = 4.0;
	sizes[0] = "0.2 0.2 0.2";
	sizes[1] = "1.0 1.0 1.0";
	times[0] = 0.0;
	times[1] = 1.0;

//	debris = Team1AntiairNearEnemyExplosionDebris;
//	debrisThetaMin = 0;
//	debrisThetaMax = 180;
//	debrisNum = 15;
//	debrisVelocity = 20.0;
//	debrisVelocityVariance = 15.0;

//	emitter[0] = Team1AntiairNearEnemyExplosion_CloudEmitter;
//	emitter[0] = Team1AntiairNearEnemyExplosion_Cloud2Emitter;

	// Camera Shaking
	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 20;
	lightEndRadius = 2;
	lightStartColor = "1.0 1.0 1.0 1.0";
	lightEndColor = "1.0 1.0 1.0 0.3";
};

//-----------------------------------------------------------------------------
// hit effect

datablock DebrisData(RedExplosiveDiscHit_Debris)
{
	// shape...
	shapeFile = "share/shapes/rotc/misc/debris1.white.dts";

	// bounce...
	staticOnMaxBounce = true;
	numBounces = 5;

	// physics...
	gravModifier = 2.0;
	elasticity = 0.6;
	friction = 0.1;

	// spin...
	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	// lifetime...
	lifetime = 4.0;
	lifetimeVariance = 1.0;
};

datablock ExplosionData(RedExplosiveDiscHit)
{
	soundProfile	= ExplosiveDiscHitSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 250;
	
	//emitter[0] = DefaultSmallWhiteDebrisEmitter;

	//debris = RedExplosiveDiscHit_Debris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 60;
	//debrisNum = 3;
	//debrisNumVariance = 1;
	//debrisVelocity = 10.0;
	//debrisVelocityVariance = 5.0;

	// Dynamic light
	lightStartRadius = 1;
	lightEndRadius = 5;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";

	shakeCamera = false;
};

//-----------------------------------------------------------------------------
// hit enemy...

datablock ExplosionData(RedExplosiveDiscHitEnemy)
{
	soundProfile = DiscHitSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 250;

	// Dynamic light
	lightStartRadius = 1;
	lightEndRadius = 5;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";

	shakeCamera = false;
};



