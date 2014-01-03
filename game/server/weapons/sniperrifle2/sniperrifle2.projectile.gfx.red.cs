//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// fire explosion

datablock ExplosionData(RedSniperRifle2ProjectileFireExplosion)
{
	soundProfile = SniperRifle2FireSound;

//	particleEmitter = RedSniperRifle2ProjectileFireExplosion_CloudEmitter;
//	particleDensity = 5;
//	particleRadius = 0.1;
};

//-----------------------------------------------------------------------------
// projectile particle emitter

//-----------------------------------------------------------------------------
// laser tail...

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(RedSniperRifle2ProjectileLaserTrail)
{
	hasLine   = false;
	lineColor = "1.00 0.50 0.50 0.5";
	lineWidth = 2.0;

	hasInner = false;
	innerColor = "1.00 0.50 0.50 0.5";
	innerWidth = "0.08";

	hasOuter = false;
	outerColor = "1.00 0.00 0.00 0.75";
	outerWidth = "0.20";

	bitmap = "share/textures/rotc/smoke4.red";
	bitmapWidth = 0.15;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 1250;
 
    windCoefficient = 0.0;
    
    // node x movement...
    nodeMoveMode[0]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[0]    = -0.25;
    nodeMoveSpeedAdd[0] =  0.5;
    // node y movement...
    nodeMoveMode[1]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[1]    = -0.25;
    nodeMoveSpeedAdd[1] =  0.5;
    // node z movement...
    nodeMoveMode[2]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[2]    = -0.25;
    nodeMoveSpeedAdd[2] =  0.5;
    
    nodeDistance = 4;
};

datablock MultiNodeLaserBeamData(RedSniperRifle2ProjectileLaserTrailHit)
{
	hasLine   = true;
	lineColor = "1.00 0.00 0.00 1.0";
    lineWidth = 2.0;

	hasInner = false;
	innerColor = "1.00 0.50 0.00 1.0";
	innerWidth = "0.08";

	hasOuter = false;
	outerColor = "1.00 0.00 0.00 0.75";
	outerWidth = "0.20";

	bitmap = "share/textures/rotc/lasertrail2.red";
	bitmapWidth = 2.0;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 450;
 
    windCoefficient = 0.0;
    
    // node x movement...
    nodeMoveMode[0]     = $NodeMoveMode::None;
    nodeMoveSpeed[0]    = -2;
    nodeMoveSpeedAdd[0] =  4;
    // node y movement...
    nodeMoveMode[1]     = $NodeMoveMode::None;
    nodeMoveSpeed[1]    = -2;
    nodeMoveSpeedAdd[1] =  4;
    // node z movement...
    nodeMoveMode[2]     = $NodeMoveMode::None;
    nodeMoveSpeed[2]    = -2.0;
    nodeMoveSpeedAdd[2] =  4.0;
    
    nodeDistance = 4;
};

//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(RedSniperRifle2ProjectileMissedEnemyEffect)
{
	soundProfile = SniperRifle2ProjectileMissedEnemySound;

	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion2_white.dts";
	faceViewer	  = true;
	playSpeed = 8.0;
	sizes[0] = "0.07 0.07 0.07";
	sizes[1] = "0.01 0.01 0.01";
	times[0] = 0.0;
	times[1] = 1.0;

	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 2;
	lightStartColor = "0.5 0.5 0.5";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
};

//-----------------------------------------------------------------------------
// explosion...

datablock ParticleData(RedSniperRifle2ProjectileExplosion_SparksParticle)
{
	dragCoeffiecient	  = 1.0;
	gravityCoefficient	= -0.0;
	inheritedVelFactor	= 0.0;

	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 200;

	useInvAlpha =  false;

	textureName = "share/textures/rotc/star3.red";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 0.0 0.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 1.0;

	allowLighting = false;
   renderDot = false;
};

datablock ParticleEmitterData(RedSniperRifle2ProjectileExplosion_SparksEmitter)
{
	ejectionPeriodMS = 3;
	periodVarianceMS = 0;
	ejectionVelocity = 3;
	velocityVariance = 0;
	ejectionOffset	= 0.0;
	thetaMin			= 80;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 50;
	particles = "RedSniperRifle2ProjectileExplosion_SparksParticle";
};

datablock ExplosionData(RedSniperRifle2ProjectileHit)
{
	soundProfile = SniperRifle2ProjectileImpactSound;

	lifetimeMS = 300;

	//particleEmitter = RedSniperRifle2ProjectileExplosion_CloudEmitter;
	//particleDensity = 15;
	//particleRadius = 1;

	//emitter[0] = RedSniperRifle2ProjectileExplosion_SparksEmitter;
	//emitter[2] = RedSniperRifle2ProjectileExplosion_DustEmitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 2;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
};

datablock ExplosionData(RedSniperRifle2ProjectileExplosion : RedSniperRifle2ProjectileHit)
{
 	// shape...
	//explosionShape = "share/shapes/rotc/weapons/blaster/projectile.impact.orange.dts";
	//faceViewer = false;
	//playSpeed = 0.4;
	//sizes[0] = "1 1 1";
	//sizes[1] = "1 1 1";
	//times[0] = 0.0;
	//times[1] = 1.0;

	lifetimeMS = 300;

	//debris = RedSniperRifle2ProjectileExplosion_LargeDebris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 60;
	//debrisNum = 3;
   //debrisNumVariance = 0;
	//debrisVelocity = 60.0;
	//debrisVelocityVariance = 10.0;

	//particleEmitter = RedSniperRifle2ProjectileExplosion_CloudEmitter;
	//particleDensity = 15;
	//particleRadius = 1;

	emitter[0] = DefaultSmallWhiteDebrisEmitter;
	emitter[1] = RedSniperRifle2ProjectileExplosion_SparksEmitter;
 
	// Dynamic light
	lightStartRadius = 2;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
};


