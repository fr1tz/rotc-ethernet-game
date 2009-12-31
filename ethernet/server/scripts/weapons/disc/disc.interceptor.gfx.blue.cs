//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - disc.interceptor.gfx.blue.cs
// Eye-candy for the disc
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(BlueInterceptorDiscLaserTail)
{
	hasLine = true;
	lineStartColor	= "0.00 0.00 1.00 0.0";
	lineBetweenColor = "0.00 0.00 1.00 0.25";
	lineEndColor	  = "0.00 0.00 1.00 0.5";
	lineWidth		  = 2.0;

	hasInner = false;
	innerStartColor = "0.00 0.00 0.90 0.5";
	innerBetweenColor = "0.50 0.00 0.90 0.9";
	innerEndColor = "1.00 1.00 1.00 0.9";
	innerStartWidth = "0.05";
	innerBetweenWidth = "0.05";
	innerEndWidth = "0.05";

	hasOuter = false;
	outerStartColor = "0.00 0.00 0.90 0.0";
	outerBetweenColor = "0.50 0.00 0.90 0.8";
	outerEndColor = "1.00 1.00 1.00 0.8";
	outerStartWidth = "0.3";
	outerBetweenWidth = "0.25";
	outerEndWidth = "0.1";

	bitmap = "~/data/weapons/blaster/lasertail.blue";
	bitmapWidth = 0.40;
//	crossBitmap = "~/data/weapons/blaster/lasertail.blue.cross";
//	crossBitmapWidth = 0.10;

	betweenFactor = 0.5;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(BlueInterceptorDiscInnerLaserTrail)
{
	hasLine = false;
	lineColor	= "0.00 0.00 1.00 0.3";

	hasInner = false;
	innerColor = "1.00 0.00 1.00 0.3";
	innerWidth = "0.05";

	hasOuter = false;
	outerColor = "1.00 0.00 1.00 0.1";
	outerWidth = "0.10";

	bitmap = "~/data/weapons/disc/lasertrail.blue";
	bitmapWidth = 0.50;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 200;
};

datablock MultiNodeLaserBeamData(BlueInterceptorDiscOuterLaserTrail)
{
	hasLine = true;
	lineColor	= "0.00 0.00 1.00 0.5";

	hasInner = true;
	innerColor = "0.00 0.00 1.00 0.3";
	innerWidth = "0.50";

	hasOuter = false;
	outerColor = "1.00 0.00 1.00 0.1";
	outerWidth = "0.10";

	//bitmap = "~/data/projectiles/Disc/lasertrail";
	//bitmapWidth = 1.00;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 200;
};

//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(BlueInterceptorDiscExplosion_Emitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 220;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "0.0 0.0 1.0 1.0";
	colors[1]	  = "0.0 0.0 1.0 1.0";
	colors[2]	  = "0.0 0.0 1.0 0.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "~/data/particles/smoke_particle";
	allowLighting = false;
};

datablock ParticleEmitterData(BlueInterceptorDiscExplosion_Emitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = -5.0;
	velocityVariance = 0.0;
	ejectionOffset	 = 1.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 100;
	particles = BlueInterceptorDiscExplosion_Emitter_Particle;
};

datablock ExplosionData(BlueInterceptorDiscExplosion)
{
	soundProfile = DiscExplosionSound;

	lifetimeMS = 200;

 	// shape...
	explosionShape = "~/data/effects/explosion4_blue.dts";
	faceViewer	  = false;
	playSpeed = 3.0;
	sizes[0] = "0.2 0.2 0.2";
	sizes[1] = "0.0 0.0 0.0";
	times[0] = 0.0;
	times[1] = 1.0;

	emitter[0] = BlueInterceptorDiscExplosion_Emitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 4;
	lightEndRadius = 0;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
};

//-----------------------------------------------------------------------------
// deflected effect

datablock ExplosionData(BlueInterceptorDiscDeflectedEffect)
{
	soundProfile	= DiscDeflectedSound;

	explosionShape = "~/data/effects/explosion_white.dts";
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

datablock ExplosionData(BlueInterceptorDiscBounceEffect)
{
	soundProfile	= DiscBounceSound;

	// shape...
	explosionShape = "~/data/effects/explosion_white.dts";
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

datablock DebrisData(BlueInterceptorDiscHit_Debris)
{
	// shape...
	shapeFile = "~/data/misc/debris1.white.dts";

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

datablock ExplosionData(BlueInterceptorDiscHit)
{
	soundProfile	= DiscHitSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 250;

	debris = BlueInterceptorDiscHit_Debris;
	debrisThetaMin = 0;
	debrisThetaMax = 60;
	debrisNum = 3;
	debrisNumVariance = 1;
	debrisVelocity = 10.0;
	debrisVelocityVariance = 5.0;

	// Dynamic light
	lightStartRadius = 1;
	lightEndRadius = 5;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";

	shakeCamera = false;
};

//-----------------------------------------------------------------------------
// hit enemy...

datablock ExplosionData(BlueInterceptorDiscHitEnemy)
{
	soundProfile = DiscHitSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 250;

	// Dynamic light
	lightStartRadius = 1;
	lightEndRadius = 5;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";

	shakeCamera = false;
};



