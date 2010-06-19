//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// missilelauncher.projectile.gfx.blue.cs
// Eyecandy for the missile launcher projectile
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(BlueMissileLauncherProjectileLaserTail)
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
	bitmapWidth = 0.20;
//	crossBitmap = "~/data/weapons/blaster/lasertail.blue.cross";
//	crossBitmapWidth = 0.10;

	betweenFactor = 0.5;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(BlueMissileLauncherProjectileLaserTrail)
{
	hasLine = true;
	lineColor	= "0.50 0.50 1.00 0.2";

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = true;
	outerColor = "0.50 0.50 1.00 0.2";
	outerWidth = "0.05";

	//bitmap = "~/data/weapons/missilelauncher/lasertrail.blue";
	//bitmapWidth = 0.25;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::Horizontal;
	fadeTime = 1000;

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
    nodeMoveSpeed[2]    = 0.5;
    nodeMoveSpeedAdd[2] = 0.5;

    nodeDistance = 15;
};

//-----------------------------------------------------------------------------
// impact...

datablock ParticleData(BlueMissileLauncherProjectileImpact_Smoke)
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

datablock ParticleEmitterData(BlueMissileLauncherProjectileImpact_SmokeEmitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.1;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "BlueMissileLauncherProjectileImpact_Smoke";
};

datablock DebrisData(BlueMissileLauncherProjectileImpact_Debris)
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

datablock ExplosionData(BlueMissileLauncherProjectileImpact)
{
	soundProfile	= MissileLauncherProjectileImpactSound;

	lifetimeMS = 3000;

 	// shape...
	explosionShape = "~/data/weapons/blaster/projectile.impact.blue.dts";
	faceViewer = false;
	playSpeed = 0.4;
	sizes[0] = "1 1 1";
	sizes[1] = "1 1 1";
	times[0] = 0.0;
	times[1] = 1.0;

	emitter[0] = BlueMissileLauncherProjectileImpact_SmokeEmitter;

	debris = BlueMissileLauncherProjectileImpact_Debris;
	debrisThetaMin = 0;
	debrisThetaMax = 60;
	debrisNum = 1;
	debrisNumVariance = 1;
	debrisVelocity = 10.0;
	debrisVelocityVariance = 5.0;

	// Dynamic light
	lightStartRadius = 0;
	lightEndRadius = 0;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;

	shakeCamera = false;
};

//-----------------------------------------------------------------------------
// hit enemy...

datablock ExplosionData(BlueMissileLauncherProjectileHit)
{
	soundProfile = MissileLauncherProjectileImpactSound;

	lifetimeMS = 450;

	// Dynamic light
	lightStartRadius = 0.25;
	lightEndRadius = 3;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
};

//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(BlueMissileLauncherProjectileMissedEnemyEffect)
{
	soundProfile = MissileLauncherProjectileMissedEnemySound;

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

