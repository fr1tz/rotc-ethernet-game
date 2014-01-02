//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(BlueAssaultRifleProjectileParticleEmitter_Particles)
{
	dragCoeffiecient	  = 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;

	lifetimeMS			  = 200;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  true;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/smoke_particle.png";

	colors[0]	  = "0.9 0.9 0.9 0.6";
	colors[1]	  = "0.9 0.9 0.9 0.3";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 0.6;
	sizes[1]		= 0.3;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(BlueAssaultRifleProjectileParticleEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
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
	particles = "BlueAssaultRifleProjectileParticleEmitter_Particles";
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(BlueAssaultRifleProjectileLaserTrail)
{
	hasLine = true;
	lineColor	= "0.00 0.00 1.00 1.00";
	lineWidth = 1;

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = false;
	outerColor = "1.00 0.00 0.00 0.02";
	outerWidth = "0.10";

	bitmap = "share/shapes/rotc/weapons/disc/lasertrail2.blue";
	bitmapWidth = 0.25;

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

datablock LaserBeamData(BlueAssaultRifleProjectileLaserTail)
{
	hasLine = true;
	lineStartColor	= "0.00 0.00 1.00 0.0";
	lineBetweenColor = "0.00 0.00 1.00 0.5";
	lineEndColor	  = "0.00 0.00 1.00 1.0";
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

	bitmap = "share/shapes/rotc/weapons/blaster/lasertail.blue";
	bitmapWidth = 0.1;
	//crossBitmap = "share/shapes/rotc/weapons/blaster/projectile.impact.green";
	//crossBitmapWidth = 4.25;

	betweenFactor = 0.5;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// bounce

datablock ParticleData(BlueAssaultRifleProjectileBounceExplosion_Smoke)
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

datablock ParticleEmitterData(BlueAssaultRifleProjectileBounceExplosion_SmokeEmitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.1;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "BlueAssaultRifleProjectileBounceExplosion_Smoke";
};

datablock ExplosionData(BlueAssaultRifleProjectileBounceExplosion)
{
	soundProfile = AssaultRifleProjectileBounceSound;

	lifetimeMS = 50;

	emitter[0] = BlueAssaultRifleProjectileBounceExplosion_SmokeEmitter;

	// Dynamic light
	lightStartRadius = 10;
	lightEndRadius = 0;
	lightStartColor = "1 1 1";
	lightEndColor = "1 1 1";
};


//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(BlueAssaultRifleProjectileExplosion_Cloud)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 250;
	lifetimeVarianceMS	= 0;

	useInvAlpha = false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/corona4.png";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.5";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 7.0;
	sizes[1]		= 2.5;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(BlueAssaultRifleProjectileExplosion_CloudEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;

	ejectionVelocity = 0.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "BlueAssaultRifleProjectileExplosion_Cloud";
};

datablock ParticleData(BlueAssaultRifleProjectileExplosion_Dust)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= -0.01;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 2000;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = true;
	spinRandomMin		  = -90.0;
	spinRandomMax		  = 500.0;
	textureName			 = "share/textures/rotc/smoke_particle.png";
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

datablock ParticleEmitterData(BlueAssaultRifleProjectileExplosion_DustEmitter)
{
	ejectionPeriodMS = 5;
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
	particles = "BlueAssaultRifleProjectileExplosion_Dust";
};


datablock ParticleData(BlueAssaultRifleProjectileExplosion_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.1;	// rises slowly
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 2000;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  true;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/smoke_particle.png";

	colors[0]	  = "0.4 0.4 0.4 0.4";
	colors[1]	  = "0.4 0.4 0.4 0.2";
	colors[2]	  = "0.4 0.4 0.4 0.0";
	sizes[0]		= 0.0;
	sizes[1]		= 6.0;
	sizes[2]		= 0.6;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(BlueAssaultRifleProjectileExplosion_SmokeEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

	lifetimeMS		 = 50;

	particles = "BlueAssaultRifleProjectileExplosion_Smoke";
};

datablock ParticleData(BlueAssaultRifleProjectileExplosion_Shrapnel)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	windCoefficient	= 0.0;
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 0;
	textureName			 = "share/textures/rotc/corona4.png";
	colors[0]	  = "1 1 1 1.0";
	colors[1]	  = "1 1 1 0.5";
	colors[2]	  = "1 1 1 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.5;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	allowLighting = false;
	renderDot = false;
};

datablock ParticleEmitterData(BlueAssaultRifleProjectileExplosion_ShrapnelEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;
	ejectionVelocity = 5;
	velocityVariance = 0;
	ejectionOffset	= 1.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 50;
	particles = "BlueAssaultRifleProjectileExplosion_Shrapnel";
};

datablock ParticleData(BlueAssaultRifleProjectileExplosion_Sparks)
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

datablock ParticleEmitterData(BlueAssaultRifleProjectileExplosion_SparksEmitter)
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
	particles = "BlueAssaultRifleProjectileExplosion_Sparks";
};

datablock MultiNodeLaserBeamData(BlueAssaultRifleProjectileExplosion_Debris_LaserTrail)
{
	hasLine = true;
	lineColor	= "1.00 1.00 1.00 0.1";

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = true;
	outerColor = "1.00 1.00 1.00 0.1";
	outerWidth = "0.1";

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

	//nodeDistance = 2;

	fadeTime = 1000;
};

datablock ParticleData(BlueAssaultRifleProjectileExplosion_Debris_Particle)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.1;	// rises slowly
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
	sizes[0]		= 0.6;
	sizes[1]		= 2.0;
	sizes[2]		= 0.6;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(BlueAssaultRifleProjectileExplosion_Debris_ParticleEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;

	ejectionVelocity = 0.0;
	velocityVariance = 0.0;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

	//lifetimeMS		 = 250;

	particles = "BlueAssaultRifleProjectileExplosion_Debris_Particle";
};

datablock DebrisData(BlueAssaultRifleProjectileExplosion_Debris)
{
//	shapeFile = "~/data/weapons/hegrenade/grenade.dts";
	emitters[0] = BlueAssaultRifleProjectileExplosion_Debris_ParticleEmitter;

//	laserTrail = BlueAssaultRifleProjectileExplosion_Debris_LaserTrail;

	// bounce...
	numBounces = 3;
	explodeOnMaxBounce = true;

	// physics...
	gravModifier = 5.0;
	elasticity = 0.6;
	friction = 0.1;

	lifetime = 0.1;
	lifetimeVariance = 0.05;
};

datablock ExplosionData(BlueAssaultRifleProjectileExplosion)
{
	soundProfile = AssaultRifleProjectileExplosionSound;

	lifetimeMS = 200;

 	// shape...
	//explosionShape = "share/shapes/rotc/effects/explosion2_white.dts";
	//faceViewer	  = false;
	//playSpeed = 8.0;
	//sizes[0] = "0.2 0.2 0.2";
	//sizes[1] = "0.2 0.2 0.2";
	//times[0] = 0.0;
	//times[1] = 1.0;

	//debris = BlueAssaultRifleProjectileExplosion_Debris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 180;
	//debrisNum = 3;
	//debrisNumVariance = 0;
	//debrisVelocity = 50.0;
	//debrisVelocityVariance = 10.0;

	particleEmitter = BlueAssaultRifleProjectileExplosion_CloudEmitter;
	particleDensity = 25;
	particleRadius = 0.5;

	emitter[0] = BlueAssaultRifleProjectileExplosion_SmokeEmitter;
	emitter[1] = BlueAssaultRifleProjectileExplosion_ShrapnelEmitter;
//	emitter[1] = BlueAssaultRifleProjectileExplosion_DustEmitter;
//	emitter[2] = BlueAssaultRifleProjectileExplosion_SparksEmitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 7;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
};

//-----------------------------------------------------------------------------
// impact

datablock ExplosionData(BlueAssaultRifleProjectileImpact : BlueAssaultRifleProjectileExplosion)
{
	soundProfile = AssaultRifleProjectileImpactSound;
	debrisThetaMax = 60;
	emitter[2] = DefaultMediumWhiteDebrisEmitter;
};

