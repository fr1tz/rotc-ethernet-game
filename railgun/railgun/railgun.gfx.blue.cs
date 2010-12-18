//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - railgun.gfx.blue.cs
// Eyecandy for the railgun
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(BlueRailgunProjectileLaserTail)
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

	bitmap = "ethernet/data/weapons/blaster/lasertail.blue";
	bitmapWidth = 0.20;
	crossBitmap = "ethernet/data/weapons/blaster/lasertail.blue.cross";
	crossBitmapWidth = 0.50;

	betweenFactor = 0.5;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(BlueRailgunProjectileLaserTrailOne)
{
	hasLine = false;
	lineColor	= "1.00 1.00 1.00 1.0";

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = true;
	outerColor = "0.00 0.00 1.00 1.0";
	outerWidth = "0.05";

//	bitmap = "ethernet/data/weapons/assaultrifle/lasertrail";
//	bitmapWidth = 0.25;

	blendMode = 1;

    windCoefficient = 0.0;

    // node x movement...
    nodeMoveMode[0]     = $NodeMoveMode::DynamicSpeed;
    nodeMoveSpeed[0]    =  -5;
    nodeMoveSpeedAdd[0] =  10;
    // node y movement...
    nodeMoveMode[1]     = $NodeMoveMode::DynamicSpeed;
    nodeMoveSpeed[1]    =  -5;
    nodeMoveSpeedAdd[1] =  10;
    // node z movement...
    nodeMoveMode[2]     = $NodeMoveMode::DynamicSpeed;
    nodeMoveSpeed[2]    =  -5;
    nodeMoveSpeedAdd[2] =  10;

    nodeDistance = 1;

	fadeTime = 300;
};

datablock MultiNodeLaserBeamData(BlueRailgunProjectileLaserTrailTwo)
{
	hasLine = false;
	lineColor	= "1.00 1.00 1.00 1.0";

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = true;
	outerColor = "0.00 0.40 1.00 1.0";
	outerWidth = "0.05";

//	bitmap = "ethernet/data/weapons/assaultrifle/lasertrail";
//	bitmapWidth = 0.25;

	blendMode = 1;

    windCoefficient = 0.0;

    // node x movement...
    nodeMoveMode[0]     = $NodeMoveMode::DynamicSpeed;
    nodeMoveSpeed[0]    =  -5;
    nodeMoveSpeedAdd[0] =  10;
    // node y movement...
    nodeMoveMode[1]     = $NodeMoveMode::DynamicSpeed;
    nodeMoveSpeed[1]    =  -5;
    nodeMoveSpeedAdd[1] =  10;
    // node z movement...
    nodeMoveMode[2]     = $NodeMoveMode::DynamicSpeed;
    nodeMoveSpeed[2]    =  -5;
    nodeMoveSpeedAdd[2] =  10;

    nodeDistance = 1;

	fadeTime = 300;
};

datablock MultiNodeLaserBeamData(BlueRailgunProjectileLaserTrailThree)
{
	hasLine = false;
	lineColor	= "1.00 1.00 1.00 1.0";

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = true;
	outerColor = "0.00 0.80 1.00 1.0";
	outerWidth = "0.05";

//	bitmap = "ethernet/data/weapons/assaultrifle/lasertrail";
//	bitmapWidth = 0.25;

	blendMode = 1;

    windCoefficient = 0.0;

    // node x movement...
    nodeMoveMode[0]     = $NodeMoveMode::DynamicSpeed;
    nodeMoveSpeed[0]    =  -5;
    nodeMoveSpeedAdd[0] =  10;
    // node y movement...
    nodeMoveMode[1]     = $NodeMoveMode::DynamicSpeed;
    nodeMoveSpeed[1]    =  -5;
    nodeMoveSpeedAdd[1] =  10;
    // node z movement...
    nodeMoveMode[2]     = $NodeMoveMode::DynamicSpeed;
    nodeMoveSpeed[2]    =  -5;
    nodeMoveSpeedAdd[2] =  10;

    nodeDistance = 1;

	fadeTime = 300;
};

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(BlueRailgunProjectileParticleEmitter_Particles)
{
	dragCoefficient      = 1;
	gravityCoefficient   = 0.0;
	windCoefficient      = 0.0;
	inheritedVelFactor	 = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			 = 300;
	lifetimeVarianceMS	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;
	textureName			 = "ethernet/data/weapons/blaster/projectile.impact.blue";
	colors[0]            = "0.0 0.0 1.0 1.0";
	colors[1]            = "0.0 0.0 1.0 0.0";
	sizes[0]             = 1.0;
	sizes[1]             = 1.0;
	times[0]             = 0.0;
	times[1]             = 1.0;
	useInvAlpha          = false;
	renderDot            = true;
};

datablock ParticleEmitterData(BlueRailgunProjectileParticleEmitter)
{
	ejectionPeriodMS = 5;
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
	particles = "BlueRailgunProjectileParticleEmitter_Particles";
};

//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(BlueRailgunExplosion_Emitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "0.0 0.2 1.0 1.0";
	colors[1]	  = "0.0 0.2 1.0 1.0";
	colors[2]	  = "0.0 0.2 1.0 0.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "ethernet/data/particles/smoke_particle";
	allowLighting = false;
};

datablock ParticleEmitterData(BlueRailgunExplosion_Emitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 10.0;
	velocityVariance = 4.0;
	ejectionOffset	 = 1.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 100;
	particles = BlueRailgunExplosion_Emitter_Particle;
};

datablock ExplosionData(BlueRailgunProjectileExplosion)
{
	soundProfile = RailgunProjectileExplosionSound;

	lifetimeMS = 600;

 	// shape...
	explosionShape = "ethernet/data/effects/explosion4_blue.dts";
	faceViewer	  = false;
	playSpeed = 3.0;
	sizes[0] = "0.1 0.1 0.1";
	sizes[1] = "0.5 0.5 0.5";
	times[0] = 0.0;
	times[1] = 1.0;

	emitter[0] = BlueRailgunExplosion_Emitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 1;
	lightEndRadius = 10;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
};


//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(BlueRailgunProjectileMissedEnemyEffect)
{
	soundProfile = RailgunProjectileMissedEnemySound;
};

