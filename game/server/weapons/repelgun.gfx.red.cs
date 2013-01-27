//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(RedRepelGunProjectileParticleEmitter_Particles)
{
	dragCoefficient      = 1;
	gravityCoefficient   = -0.2;
	windCoefficient      = 0.0;
	inheritedVelFactor	 = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			 = 1000;
	lifetimeVarianceMS	 = 0;
	textureName			 = "share/textures/rotc/smoke_particle";
	colors[0]	    = "1.0 1.0 1.0 0.5";
	colors[1]	    = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.5;
	sizes[1]		= 1.5;
	times[0]		= 0.0;
	times[1]		= 1.0;
	useInvAlpha = true;
	renderDot = false;
};

datablock ParticleEmitterData(RedRepelGunProjectileParticleEmitter)
{
	ejectionPeriodMS = 40;
	periodVarianceMS = 10;
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
	particles = "RedRepelGunProjectileParticleEmitter_Particles";
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(RedRepelGunProjectileLaserTrail)
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
    nodeMoveSpeed[2]    = 0.5;
    nodeMoveSpeedAdd[2] = 1.0;

	nodeDistance = 1;
 
	fadeTime = 1000;
};

datablock MultiNodeLaserBeamData(RedRepelGunProjectileLaserTrail2)
{
	hasLine = true;
	lineColor	= "1.00 0.00 0.00 0.5";
	lineWidth = 1;

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = true;
	outerColor = "1.00 0.00 0.00 0.5";
	outerWidth = "0.25";

//	bitmap = "share/shapes/rotc/weapons/assaultrifle/lasertrail";
//	bitmapWidth = 0.25;

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
    nodeMoveMode[2]     = $NodeMoveMode::None;
    nodeMoveSpeed[2]    = 0.5;
    nodeMoveSpeedAdd[2] = 0.5;
 
	fadeTime = 200;
};

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(RedRepelGunProjectileLaserTail)
{
	hasLine = true;
	lineStartColor	= "1.00 1.00 1.00 0.0";
	lineBetweenColor = "1.00 1.00 1.00 0.5";
	lineEndColor	  = "1.00 1.00 1.00 1.0";
 	lineWidth		  = 2.0;

	hasInner = false;
	innerStartColor	= "1.00 1.00 0.00 0.5";
	innerBetweenColor = "1.00 1.00 0.00 0.5";
	innerEndColor	  = "1.00 1.00 0.00 0.5";
	innerStartWidth	= "0.0";
	innerBetweenWidth = "0.05";
	innerEndWidth	  = "0.1";

	hasOuter = false;
	outerStartColor	= "1.00 1.00 0.00 0.0";
	outerBetweenColor = "1.00 1.00 0.00 0.2";
	outerEndColor	  = "1.00 1.00 0.00 0.2";
	outerStartWidth	= "0.0";
	outerBetweenWidth = "0.3";
	outerEndWidth	  = "0.0";

	bitmap = "share/shapes/rotc/weapons/assaultrifle/lasertail";
	bitmapWidth = 0.2;
//	crossBitmap = "share/shapes/rotc/weapons/assaultrifle/lasertail.cross";
//	crossBitmapWidth = 0.25;

	betweenFactor = 0.5;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// bounce

datablock ExplosionData(RedRepelGunProjectileBounceExplosion)
{
	soundProfile	= RepelGunProjectileBounceSound;
};


//-----------------------------------------------------------------------------
// explosion

datablock ExplosionData(RedRepelGunProjectileExplosion)
{
	soundProfile = RepelGunProjectileDebrisSound;
	
	emitter[0] = DefaultMediumWhiteDebrisEmitter;	

	//debris = RedRepelGunProjectileExplosion_SmallDebris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 60;
	//debrisNum = 5;
	//debrisVelocity = 15.0;
	//debrisVelocityVariance = 10.0;
};

//-----------------------------------------------------------------------------
// impact...

datablock ParticleData(RedRepelGunProjectileImpact_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.4;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 200;

	useInvAlpha =  true;

	textureName = "share/textures/rotc/smoke_particle";

	colors[0]	  = "1.0 1.0 1.0 0.5";
	colors[1]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(RedRepelGunProjectileImpact_SmokeEmitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.1;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "RedRepelGunProjectileImpact_Smoke";
};

datablock DebrisData(RedRepelGunProjectileImpact_Debris)
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

datablock ExplosionData(RedRepelGunProjectileImpact)
{
	soundProfile	= RedRepelGunProjectileImpactSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 250;

	emitter[0] = RedRepelGunProjectileImpact_SmokeEmitter;

	debris = RedRepelGunProjectileImpact_Debris;
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



