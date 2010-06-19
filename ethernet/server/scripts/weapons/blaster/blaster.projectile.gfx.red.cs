//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - blaster.projectile.gfx.red.cs
// Eyecandy for the blaster projectile
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(RedBlasterProjectileParticleEmitter_Particles)
{
	dragCoefficient      = 1;
	gravityCoefficient   = 0.0;
	windCoefficient      = 0.0;
	inheritedVelFactor	 = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			 = 100;
	lifetimeVarianceMS	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;
	textureName			 = "ethernet/data/weapons/blaster/projectile.impact.red";
	colors[0]            = "1.0 0.0 0.0 10.5";
	colors[1]            = "1.0 0.0 0.0 0.0";
	sizes[0]             = 0.25;
	sizes[1]             = 0.0;
	times[0]             = 0.0;
	times[1]             = 1.0;
	useInvAlpha          = false;
	renderDot            = true;
};

datablock ParticleEmitterData(RedBlasterProjectileParticleEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 2;
	ejectionVelocity = 5;
	velocityVariance = 2.5;
	ejectionOffset   = 0.0;
	thetaMin         = 0;
	thetaMax         = 0;
	phiReferenceVel  = 0;
	phiVariance      = 0;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "RedBlasterProjectileParticleEmitter_Particles";
};

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(RedBlasterProjectileLaserTail)
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
	bitmapWidth = 0.20;
//	crossBitmap = "~/data/weapons/blaster/lasertail.red.cross";
//	crossBitmapWidth = 0.10;

	betweenFactor = 0.5;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(RedBlasterProjectileLaserTrailMissed)
{
	hasLine   = true;
	lineColor = "1.00 0.00 0.00 1.0";
	lineWidth = 2.0;

	hasInner = false;
	innerColor = "1.00 0.00 0.00 0.5";
	innerWidth = "0.04";

	hasOuter = false;
	outerColor = "1.00 0.00 1.00 0.1";
	outerWidth = "0.10";

	bitmap = "~/data/weapons/blaster/lasertrail.red";
	bitmapWidth = 0.10;

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

datablock MultiNodeLaserBeamData(RedBlasterProjectileLaserTrailHit)
{
	hasLine   = true;
	lineColor = "1.00 0.00 0.00 1.0";
	lineWidth = 2.0;

	hasInner = false;
	innerColor = "1.00 0.00 0.00 0.5";
	innerWidth = "0.04";

	hasOuter = false;
	outerColor = "1.00 0.00 1.00 0.1";
	outerWidth = "0.10";

	bitmap = "~/data/weapons/blaster/lasertrail.red";
	bitmapWidth = 0.10;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 500;
 
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
// impact...

datablock ParticleData(RedBlasterProjectileImpact_Smoke)
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

datablock ParticleEmitterData(RedBlasterProjectileImpact_SmokeEmitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.1;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "RedBlasterProjectileImpact_Smoke";
};

datablock DebrisData(RedBlasterProjectileImpact_Debris)
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

datablock ExplosionData(RedBlasterProjectileImpact)
{
	soundProfile = BlasterProjectileImpactSound;

	lifetimeMS = 3000;
 
 	// shape...
	explosionShape = "~/data/weapons/blaster/projectile.impact.red.dts";
	faceViewer = false;
	playSpeed = 0.4;
	sizes[0] = "1 1 1";
	sizes[1] = "1 1 1";
	times[0] = 0.0;
	times[1] = 1.0;

	emitter[0] = RedBlasterProjectileImpact_SmokeEmitter;

	debris = RedBlasterProjectileImpact_Debris;
	debrisThetaMin = 0;
	debrisThetaMax = 60;
	debrisNum = 1;
	debrisNumVariance = 1;
	debrisVelocity = 10.0;
	debrisVelocityVariance = 5.0;

	// Dynamic light
	lightStartRadius = 0;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;

	shakeCamera = false;
};

//-----------------------------------------------------------------------------
// hit enemy...

datablock ExplosionData(RedBlasterProjectileHit)
{
	soundProfile = BlasterProjectileImpactSound;

	lifetimeMS = 450;

	// Dynamic light
	lightStartRadius = 0.25;
	lightEndRadius = 3;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
};

//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(RedBlasterProjectileMissedEnemyEffect)
{
	soundProfile = BlasterProjectileMissedEnemySound;

	// shape...
	explosionShape = "~/data/effects/explosion2_white.dts";
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


