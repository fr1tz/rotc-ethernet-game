//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// fire explosion

datablock ParticleData(GreenSniperProjectileFireExplosion_CloudParticles)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 1250;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/corona.png";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 0.5 0.0 1.0";
	colors[2]	  = "1.0 0.5 0.0 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 2.0;
	sizes[2]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(GreenSniperProjectileFireExplosion_CloudEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 0;
	velocityVariance = 0;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "GreenSniperProjectileFireExplosion_CloudParticles";
};

datablock ExplosionData(GreenSniperProjectileFireExplosion)
{
	soundProfile = SniperRifleFireSound;

//	particleEmitter = GreenSniperProjectileFireExplosion_CloudEmitter;
//	particleDensity = 5;
//	particleRadius = 0.1;
};

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(GreenSniperProjectileEmitter_Particles)
{
	dragCoeffiecient    = 0.4;
	gravityCoefficient	= -0.5;	// rises slowly
	inheritedVelFactor	= 0.0;

	lifetimeMS         = 2500;
	lifetimeVarianceMS = 0;

	useInvAlpha =  true;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/smoke_particle.png";

	colors[0]	  = "0.9 0.9 0.9 0.8";
	colors[1]	  = "0.9 0.9 0.9 0.4";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.5;
	sizes[2]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(GreenSniperProjectileEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 3;
	ejectionVelocity = 0.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin		= 0;
	thetaMax		= 10;
	phiReferenceVel  = 360;
	phiVariance		= 0;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "GreenSniperProjectileEmitter_Particles";
};

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(GreenSniperProjectileLaserTail)
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
	
	bitmap = "share/shapes/rotc/weapons/blaster/lasertail.red";
	bitmapWidth = 0.20;
//	crossBitmap = "share/shapes/rotc/weapons/blaster/lasertail.red.cross";
//	crossBitmapWidth = 0.10;

	betweenFactor = 0.5;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(GreenSniperProjectileLaserTrail)
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

	bitmap = "share/shapes/rotc/weapons/sniperrifle/lasertrail3.green";
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

datablock MultiNodeLaserBeamData(GreenSniperProjectileLaserTrailMissed)
{
	hasLine   = true;
	lineColor = "0.50 1.00 1.00 0.5";
	lineWidth = 2.0;

	hasInner = false;
	innerColor = "1.00 0.50 0.00 0.5";
	innerWidth = "0.08";

	hasOuter = false;
	outerColor = "1.00 0.00 0.00 0.75";
	outerWidth = "0.20";

	bitmap = "share/shapes/rotc/weapons/sniperrifle/lasertrail2.green.miss";
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

datablock MultiNodeLaserBeamData(GreenSniperProjectileLaserTrailHit)
{
	hasLine   = true;
	lineColor = "0.00 1.00 0.00 1.0";
    lineWidth = 2.0;

	hasInner = false;
	innerColor = "1.00 0.50 0.00 1.0";
	innerWidth = "0.08";

	hasOuter = false;
	outerColor = "1.00 0.00 0.00 0.75";
	outerWidth = "0.20";

	bitmap = "share/shapes/rotc/weapons/sniperrifle/lasertrail2.green";
	bitmapWidth = 0.15;

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
// impact...

datablock ParticleData(GreenSniperProjectileImpact_Smoke)
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

datablock ParticleEmitterData(GreenSniperProjectileImpact_SmokeEmitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.1;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "GreenSniperProjectileImpact_Smoke";
};

datablock DebrisData(GreenSniperProjectileImpact_Debris)
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

datablock ExplosionData(GreenSniperProjectileImpact)
{
	soundProfile = SniperProjectileImpactSound;

	lifetimeMS = 3000;
 
 	// shape...
	explosionShape = "share/shapes/rotc/weapons/blaster/projectile.impact.green.dts";
	faceViewer = false;
	playSpeed = 0.4;
	sizes[0] = "1 1 1";
	sizes[1] = "1 1 1";
	times[0] = 0.0;
	times[1] = 1.0;

	emitter[0] = DefaultSmallWhiteDebrisEmitter;
	emitter[1] = GreenSniperProjectileImpact_SmokeEmitter;

	//debris = GreenSniperProjectileImpact_Debris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 60;
	//debrisNum = 1;
	//debrisNumVariance = 1;
	//debrisVelocity = 10.0;
	//debrisVelocityVariance = 5.0;

	// Dynamic light
	lightStartRadius = 0;
	lightEndRadius = 0;
	lightStartColor = "0.0 1.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;

	shakeCamera = false;
};

//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(GreenSniperProjectileMissedEnemyEffect)
{
	soundProfile = SniperProjectileMissedEnemySound;

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

datablock ParticleData(GreenSniperProjectileExplosion_Cloud)
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
	colors[1]	  = "0.0 1.0 0.0 1.0";
	colors[2]	  = "0.0 1.0 0.0 0.0";
	sizes[0]		= 3.0;
	sizes[1]		= 3.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.2;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(GreenSniperProjectileExplosion_CloudEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 6.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "GreenSniperProjectileExplosion_Cloud";
};

datablock ParticleData(GreenSniperProjectileExplosion_Dust)
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

datablock ParticleEmitterData(GreenSniperProjectileExplosion_DustEmitter)
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
	particles = "GreenSniperProjectileExplosion_Dust";
};


datablock ParticleData(GreenSniperProjectileExplosion_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.5;	// rises slowly
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/smoke_particle.png";

	colors[0]	  = "0.0 0.9 0.0 1.0";
	colors[1]	  = "0.9 0.9 0.9 0.5";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 2.0;
	sizes[2]		= 8.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(GreenSniperProjectileExplosion_SmokeEmitter)
{
	ejectionPeriodMS = 200;
	periodVarianceMS = 0;

	ejectionVelocity = 0;
	velocityVariance = 0;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

	lifetimeMS		 = 2000;

	particles = "GreenSniperProjectileExplosion_Smoke";
};

datablock ParticleData(GreenSniperProjectileExplosion_Sparks)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 350;
	textureName			 = "share/textures/rotc/corona.png";
	colors[0]	  = "0.0 1.0 0.0 1.0";
	colors[1]	  = "0.0 1.0 0.0 1.0";
	colors[2]	  = "0.0 1.0 0.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.75;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	allowLighting = false;
};

datablock ParticleEmitterData(GreenSniperProjectileExplosion_SparksEmitter)
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
	orientParticles  = false;
	lifetimeMS		 = 100;
	particles = "GreenSniperProjectileExplosion_Sparks";
};

datablock DebrisData(GreenSniperProjectileExplosion_SmallDebris)
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

datablock MultiNodeLaserBeamData(GreenSniperProjectileExplosion_LargeDebris_LaserTrail)
{
	hasLine = false;
	lineColor	= "1.00 1.00 1.00 0.05";

	hasInner = false;
	innerColor = "1.00 1.00 0.00 0.3";
	innerWidth = "0.20";

	hasOuter = true;
	outerColor = "1.00 1.00 1.00 0.05";
	outerWidth = "0.40";

//	bitmap = "share/shapes/rotc/weapons/sniperrifle/explosion.trail";
//	bitmapWidth = 0.25;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 1000;
};

datablock ParticleData(GreenSniperProjectileExplosion_LargeDebris_Particles2)
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

datablock ParticleEmitterData(GreenSniperProjectileExplosion_LargeDebris_Emitter2)
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
	particles = "GreenSniperProjectileExplosion_LargeDebris_Particles2";
};

datablock ParticleData(GreenSniperProjectileExplosion_LargeDebris_Particles1)
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

datablock ParticleEmitterData(GreenSniperProjectileExplosion_LargeDebris_Emitter1)
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
	particles = "GreenSniperProjectileExplosion_LargeDebris_Particles1";
};

datablock ExplosionData(GreenSniperProjectileExplosion_LargeDebris_Explosion)
{
	soundProfile	= SniperDebrisSound;
	
	emitter[0] = DefaultMediumWhiteDebrisEmitter;	

	//debris = GreenSniperProjectileExplosion_SmallDebris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 60;
	//debrisNum = 5;
	//debrisVelocity = 15.0;
	//debrisVelocityVariance = 10.0;
};

datablock DebrisData(GreenSniperProjectileExplosion_LargeDebris)
{
	// shape...
	shapeFile = "share/shapes/rotc/misc/debris2.white.dts";

	explosion = GreenSniperProjectileExplosion_LargeDebris_Explosion;

	laserTrail = GreenSniperProjectileExplosion_LargeDebris_LaserTrail;
	//emitters[0] = GreenSniperProjectileExplosion_LargeDebris_Emitter2;
	//emitters[1] = GreenSniperProjectileExplosion_LargeDebris_Emitter1;

	// bounce...
	numBounces = 0;
	explodeOnMaxBounce = true;

	// physics...
	gravModifier = 10.0;
	elasticity = 0.6;
	friction = 0.1;

	// spin...
	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	// lifetime...
	lifetime = 20.0;
	lifetimeVariance = 0.0;
};

datablock ExplosionData(GreenSniperProjectileHit)
{
	soundProfile	= SniperExplosionSound;

	lifetimeMS = 2000;

	//particleEmitter = GreenSniperProjectileExplosion_CloudEmitter;
	//particleDensity = 15;
	//particleRadius = 1;

	//emitter[0] = GreenSniperProjectileExplosion_SparksEmitter;
	//emitter[2] = GreenSniperProjectileExplosion_DustEmitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 0;
	lightEndRadius = 0;
	lightStartColor = "0.0 1.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
};

datablock ExplosionData(GreenSniperProjectileExplosion : GreenSniperProjectileHit)
{
 	// shape...
	explosionShape = "share/shapes/rotc/weapons/blaster/projectile.impact.green.dts";
	faceViewer = false;
	playSpeed = 0.4;
	sizes[0] = "1 1 1";
	sizes[1] = "1 1 1";
	times[0] = 0.0;
	times[1] = 1.0;

	lifetimeMS = 2000;

	debris = GreenSniperProjectileExplosion_LargeDebris;
	debrisThetaMin = 0;
	debrisThetaMax = 60;
	debrisNum = 3;
    debrisNumVariance = 0;
	debrisVelocity = 60.0;
	debrisVelocityVariance = 10.0;

	//particleEmitter = GreenSniperProjectileExplosion_CloudEmitter;
	//particleDensity = 15;
	//particleRadius = 1;

	emitter[0] = GreenSniperProjectileExplosion_SparksEmitter;
	emitter[1] = GreenSniperProjectileExplosion_SmokeEmitter;
};


