//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - assaultrifle.gfx.red.cs
// Eyecandy for the assault rifle
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(RedAssaultRifleProjectileParticleEmitter_Particles)
{
	dragCoefficient      = 1;
	gravityCoefficient   = -0.2;
	windCoefficient      = 0.0;
	inheritedVelFactor	 = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			 = 1000;
	lifetimeVarianceMS	 = 0;
	textureName			 = "~/data/particles/smoke_particle";
	colors[0]	    = "1.0 1.0 1.0 0.2";
	colors[1]	    = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.5;
	sizes[1]		= 1.5;
	times[0]		= 0.0;
	times[1]		= 1.0;
	useInvAlpha = true;
	renderDot = false;
};

datablock ParticleEmitterData(RedAssaultRifleProjectileParticleEmitter)
{
	ejectionPeriodMS = 200;
	periodVarianceMS = 100;
	ejectionVelocity = 0.0;
	velocityVariance = 0.0;
	ejectionOffset	 = 0.0;
	thetaMin		 = 0;
	thetaMax		 = 0;
	phiReferenceVel  = 0;
	phiVariance		 = 0;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "RedAssaultRifleProjectileParticleEmitter_Particles";
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(RedAssaultRifleProjectileLaserTrail)
{
	hasLine   = true;
	lineColor = "1.00 0.00 0.00 1.0";
    lineWidth = 2.0;

	hasInner = false;
	innerColor = "1.00 0.00 1.00 0.3";
	innerWidth = "0.05";

	hasOuter = false;
	outerColor = "1.00 0.00 1.00 0.1";
	outerWidth = "0.10";

	bitmap = "~/data/weapons/blaster/lasertrail.red";
	bitmapWidth = 0.50;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 100;
 
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
    nodeMoveMode[2]     = $NodeMoveMode::DynamicSpeed;
    nodeMoveSpeed[2]    = 3.0;
    nodeMoveSpeedAdd[2] = -6.0;
    
    nodeDistance = 5;
};

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(RedAssaultRifleProjectileLaserTail)
{
	hasLine = true;
	lineStartColor	= "1.00 0.00 0.00 0.0";
	lineBetweenColor = "1.00 0.00 0.00 0.25";
	lineEndColor	  = "1.00 0.00 0.00 0.5";
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
	
	bitmap = "~/data/weapons/blaster/lasertail.red";
	bitmapWidth = 0.30;
	//crossBitmap = "~/data/weapons/blaster/lasertail.red.cross";
	//crossBitmapWidth = 0.50;

	betweenFactor = 0.5;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// bounce

datablock ExplosionData(RedAssaultRifleProjectileBounceExplosion)
{
	soundProfile	= RedAssaultRifleProjectileBounceSound;
	
	lifetimeMS = 200;
	
	// Dynamic light
	lightStartRadius = 6;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.8 0.2 1.0";
	lightEndColor = "1.0 0.8 0.2 0.3";
};


//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(RedAssaultRifleProjectileExplosion_Cloud)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 600;
	lifetimeVarianceMS	= 0;

	useInvAlpha = false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "~/data/particles/corona.png";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.0";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.5;
	times[0]		= 0.0;
	times[1]		= 0.2;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(RedAssaultRifleProjectileExplosion_CloudEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;

	ejectionVelocity = 0.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "RedAssaultRifleProjectileExplosion_Cloud";
};

datablock ParticleData(RedAssaultRifleProjectileExplosion_Dust)
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
	textureName			 = "~/data/particles/smoke_particle.png";
	colors[0]	  = "0.9 0.9 0.9 0.5";
	colors[1]	  = "0.9 0.9 0.9 0.5";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 0.9;
	sizes[1]		= 1.5;
	sizes[2]		= 1.6;
	times[0]		= 0.0;
	times[1]		= 0.7;
	times[2]		= 1.0;
	allowLighting = true;
};

datablock ParticleEmitterData(RedAssaultRifleProjectileExplosion_DustEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 2.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	lifetimeMS		 = 50;
	particles = "RedAssaultRifleProjectileExplosion_Dust";
};


datablock ParticleData(RedAssaultRifleProjectileExplosion_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.5;	// rises slowly
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 1250;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  true;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "~/data/particles/smoke_particle.png";

	colors[0]	  = "0.9 0.9 0.9 0.4";
	colors[1]	  = "0.9 0.9 0.9 0.2";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 0.6;
	sizes[1]		= 2.0;
	sizes[2]		= 0.6;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(RedAssaultRifleProjectileExplosion_SmokeEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

	lifetimeMS		 = 250;

	particles = "RedAssaultRifleProjectileExplosion_Smoke";
};

datablock ParticleData(RedAssaultRifleProjectileExplosion_Sparks)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 350;
	textureName			 = "~/data/particles/particle1.png";
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

datablock ParticleEmitterData(RedAssaultRifleProjectileExplosion_SparksEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 4;
	velocityVariance = 1;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 60;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 100;
	particles = "RedAssaultRifleProjectileExplosion_Sparks";
};

datablock MultiNodeLaserBeamData(RedAssaultRifleProjectileExplosion_Debris_LaserTrail)
{
	hasLine = true;
	lineColor	= "1.00 1.00 0.00 0.5";

	hasInner = false;
	innerColor = "1.00 1.00 0.00 0.3";
	innerWidth = "0.20";

	hasOuter = false;
	outerColor = "1.00 1.00 0.00 0.3";
	outerWidth = "0.40";

//	bitmap = "~/data/weapons/hegrenade/lasertrail";
//	bitmapWidth = 0.1;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 500;
};

datablock DebrisData(RedAssaultRifleProjectileExplosion_Debris)
{
//	shapeFile = "~/data/weapons/hegrenade/grenade.dts";
//	emitters[0] = GrenadeLauncherParticleEmitter;

	laserTrail = RedAssaultRifleProjectileExplosion_Debris_LaserTrail;

	// bounce...
	numBounces = 3;
	explodeOnMaxBounce = true;

	// physics...
	gravModifier = 5.0;
	elasticity = 0.6;
	friction = 0.1;

	lifetime = 5.0;
	lifetimeVariance = 0.02;
};

datablock ExplosionData(RedAssaultRifleProjectileExplosion)
{
	soundProfile = AssaultRifleProjectileExplosionSound;

	lifetimeMS = 200;
 
 	// shape...
	explosionShape = "~/data/effects/explosion2_white.dts";
	faceViewer	  = false;
	playSpeed = 8.0;
	sizes[0] = "0.2 0.2 0.2";
	sizes[1] = "0.2 0.2 0.2";
	times[0] = 0.0;
	times[1] = 1.0;

	debris = 0; //RedAssaultRifleProjectileExplosion_Debris;
	debrisThetaMin = 0;
	debrisThetaMax = 180;
	debrisNum = 3;
	debrisVelocity = 50.0;
	debrisVelocityVariance = 10.0;
	
	particleEmitter = RedAssaultRifleProjectileExplosion_CloudEmitter;
	particleDensity = 100;
	particleRadius = 1.5;

	emitter[0] = RedAssaultRifleProjectileExplosion_DustEmitter;
	emitter[1] = 0; // RedAssaultRifleProjectileExplosion_SmokeEmitter;
	emitter[2] = 0; // RedAssaultRifleProjectileExplosion_SparksEmitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 6;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
};

//-----------------------------------------------------------------------------
// impact...

datablock ParticleData(RedAssaultRifleProjectileImpact_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.4;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 200;

	useInvAlpha =  true;

	textureName = "~/data/particles/smoke_particle";

	colors[0]	  = "1.0 1.0 1.0 0.5";
	colors[1]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(RedAssaultRifleProjectileImpact_SmokeEmitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.1;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "RedAssaultRifleProjectileImpact_Smoke";
};

datablock DebrisData(RedAssaultRifleProjectileImpact_Debris)
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

datablock ExplosionData(RedAssaultRifleProjectileImpact)
{
	soundProfile	= RedAssaultRifleProjectileImpactSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 250;

	emitter[0] = RedAssaultRifleProjectileImpact_SmokeEmitter;

	debris = RedAssaultRifleProjectileImpact_Debris;
	debrisThetaMin = 0;
	debrisThetaMax = 60;
	debrisNum = 2;
	debrisNumVariance = 1;
	debrisVelocity = 10.0;
	debrisVelocityVariance = 5.0;

	// Dynamic light
	lightStartRadius = 0.25;
	lightEndRadius = 4;
	lightStartColor = "1.0 0.8 0.2";
	lightEndColor = "0.0 0.0 0.0";

	shakeCamera = false;
};



