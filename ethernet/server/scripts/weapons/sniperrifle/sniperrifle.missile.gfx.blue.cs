//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - sniperrifle.gfx.blue.cs
// Eye-candy for the sniper rifle
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(BlueSniperMissileTrail)
{
	//bitmap = "~/data/weapons/sniperrifle/lasertrail_blue";
	bitmapWidth = 0.50;

	blendMode = 1;
	fadeTime = 1000;
};

//-----------------------------------------------------------------------------
// laser tail

datablock LaserBeamData(BlueSniperMissileTail)
{
	hasLine = true;
	lineStartColor	= "0.00 0.00 1.00 0.0";
	lineBetweenColor = "0.00 0.00 1.00 0.5";
	lineEndColor	  = "0.00 0.00 1.00 0.5";
    lineWidth = 2.0;

	hasInner = true;
	innerStartColor	= "0.00 0.00 1.00 0.0";
	innerBetweenColor = "0.00 0.00 1.00 0.5";
	innerEndColor	  = "0.00 0.00 1.00 0.5";
	innerStartWidth	= "0.0";
	innerBetweenWidth = "0.3";
	innerEndWidth	  = "0.1";

	hasOuter = true;
	outerStartColor	= "0.00 0.00 1.00 0.0";
	outerBetweenColor = "0.00 0.00 1.00 0.5";
	outerEndColor	  = "0.00 0.00 1.00 0.5";
	outerStartWidth	= "0.1";
	outerBetweenWidth = "0.4";
	outerEndWidth	  = "0.2";

//	crossBitmap = "~/data/projectiles/team1heavyrepeater/lasertail_cross";
//	crossBitmapWidth = 2;

	betweenFactor = 0.999;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// slow projectile particle emitter

datablock ParticleData(BlueSlowSniperMissileEmitter_Particles)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 0;
	textureName			 = "~/data/particles/smoke_particle";
	colors[0]	  = "0.5 0.5 0.5 1.0";
	colors[1]	  = "0.5 0.5 0.5 0.666";
	colors[2]	  = "0.5 0.5 0.5 0.333";
	colors[3]	  = "0.5 0.5 0.5 0.0";
	sizes[0]		= 1.5;
	sizes[1]		= 1.5;
	sizes[2]		= 1.5;
	sizes[3]		= 1.5;
	times[0]		= 0.0;
	times[1]		= 0.333;
	times[2]		= 0.666;
	times[3]		= 1.0;
	useInvAlpha = true;
	renderDot = true;
};

datablock ParticleEmitterData(BlueSlowSniperMissileEmitter)
{
	ejectionPeriodMS = 50;
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
	particles = "BlueSlowSniperMissileEmitter_Particles";
};


//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(BlueSniperMissileEmitter_Particles)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 0;
	textureName			 = "~/data/particles/small_particle4";
	colors[0]	  = "1.0 0.0 1.0 0.5";
	colors[1]	  = "0.0 0.0 1.0 0.5";
	colors[2]	  = "0.0 1.0 1.0 0.5";
	colors[3]	  = "0.0 5.0 0.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.5;
	sizes[3]		= 0.5;
	times[0]		= 0.0;
	times[1]		= 0.333;
	times[2]		= 0.666;
	times[3]		= 1.0;
	renderDot = true;
};

datablock ParticleEmitterData(BlueSniperMissileEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 1.5;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 45;
	thetaMax			= 45;
	phiReferenceVel  = 75000;
	phiVariance		= 0;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "BlueSniperMissileEmitter_Particles";
};

//-----------------------------------------------------------------------------
// fire particle emitter

datablock ParticleData(BlueSniperRifleFireEmitter_Particles)
{
	dragCoefficient		= 1;
	gravityCoefficient	= -0.5;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 200;
	lifetimeVarianceMS	= 0;
	textureName			 = "~/data/particles/smoke_particle";
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "0.0 0.0 1.0 1.0";
	colors[2]	  = "0.0 0.0 1.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.25;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

};

datablock ParticleEmitterData(BlueSniperRifleFireEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 15.0;
	velocityVariance = 0.2;
	ejectionOffset	= 0;
	thetaMin			= 0;
	thetaMax			= 10;
	phiReferenceVel  = 0;
	phiVariance		= 90;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "BlueSniperRifleFireEmitter_Particles";
};

//-----------------------------------------------------------------------------
// near enemy explosion

datablock ParticleData( BlueSniperNearEnemyExplosionDebrisSmokeParticle )
{
	dragCoeffiecient	  = 1.0;
	gravityCoefficient	= 0.0;
	windCoefficient		= 0.2;
	inheritedVelFactor	= 0.0;

	lifetimeMS			  = 400;
	lifetimeVarianceMS	= 0;

	textureName			 = "~/data/particles/small_particle4";

	//spinRandomMin = -60.0;
	//spinRandomMax = 60.0;

	colors[0]	  = "1.0 0.0 1.0 0.25";
	colors[1]	  = "0.0 0.0 1.0 0.25";
	colors[2]	  = "0.0 1.0 1.0 0.25";
	colors[3]	  = "0.0 5.0 0.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	sizes[3]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.333;
	times[2]		= 0.666;
	times[3]		= 1.0;
};

datablock ParticleEmitterData( BlueSniperNearEnemyExplosionDebrisSmokeEmitter )
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 1;

	ejectionVelocity = 0.0;
	velocityVariance = 0.0;

	thetaMin			= 0.0;
	thetaMax			= 0.0;

	particles = "BlueSniperNearEnemyExplosionDebrisSmokeParticle";
};


datablock DebrisData( BlueSniperNearEnemyExplosionDebris )
{
	emitters[0] = BlueSniperNearEnemyExplosionDebrisSmokeEmitter;

	explodeOnMaxBounce = true;

	elasticity = 0.4;
	friction = 0.2;

	lifetime = 0.15;
	lifetimeVariance = 0.02;

	baseRadius = 0.0;

	numBounces = 1;
};

datablock ParticleData(BlueSniperNearEnemyExplosionCloud)
{
	dragCoefficient		= 1;
	gravityCoefficient	= -0.15; // rise slowly
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 350;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;
	textureName			 = "~/data/particles/particle1";
	colors[0]	  = "1.00 1.00 1.00 0.8";
	colors[1]	  = "0.00 0.00 1.00 0.4";
	colors[2]	  = "0.00 0.00 1.00 0.0";
	sizes[0]		= 3.0;
	sizes[1]		= 1.5;
	sizes[2]		= 0.5;
	times[0]		= 0.0;
	times[1]		= 0.2;
	times[2]		= 1.0;

};

datablock ParticleEmitterData(BlueSniperNearEnemyExplosionCloudEmitter)
{
	ejectionPeriodMS = 4;
	periodVarianceMS = 0;
	ejectionVelocity = 1;
	velocityVariance = 0;
	ejectionOffset	= 0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 100;
	particles = "BlueSniperNearEnemyExplosionCloud";
};

datablock ExplosionData(BlueSniperNearEnemyExplosion)
{
	soundProfile	= SniperNearEnemyExplosionSound;

	faceViewer	  = true;
	explosionScale = "1.0 1.0 1.0";

	debris = BlueSniperNearEnemyExplosionDebris;
	debrisThetaMin = 0;
	debrisThetaMax = 180;
	debrisNum = 15;
	debrisVelocity = 30.0;
	debrisVelocityVariance = 0.0;

	//emitter[0] = BlueSniperNearEnemyExplosionCloudEmitter;

	shakeCamera = true;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;
};

//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(BlueSniperMissileExplosion_Cloud)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 600;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "~/data/particles/corona.png";

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

datablock ParticleEmitterData(BlueSniperMissileExplosion_CloudEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 6.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "BlueSniperMissileExplosion_Cloud";
};

datablock ParticleData(BlueSniperMissileExplosion_Dust)
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
	sizes[0]		= 3.2;
	sizes[1]		= 4.6;
	sizes[2]		= 5.0;
	times[0]		= 0.0;
	times[1]		= 0.7;
	times[2]		= 1.0;
	allowLighting = true;
};

datablock ParticleEmitterData(BlueSniperMissileExplosion_DustEmitter)
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
	particles = "BlueSniperMissileExplosion_Dust";
};


datablock ParticleData(BlueSniperMissileExplosion_Smoke)
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
	sizes[0]		= 2.0;
	sizes[1]		= 6.0;
	sizes[2]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(BlueSniperMissileExplosion_SmokeEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 6.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

	lifetimeMS		 = 250;

	particles = "BlueSniperMissileExplosion_Smoke";
};

datablock ParticleData(BlueSniperMissileExplosion_Sparks)
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

datablock ParticleEmitterData(BlueSniperMissileExplosion_SparksEmitter)
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
	particles = "BlueSniperMissileExplosion_Sparks";
};

datablock DebrisData(BlueSniperMissileExplosion_SmallDebris)
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
	lifetime = 2.0;
	lifetimeVariance = 1.0;
};

datablock MultiNodeLaserBeamData(BlueSniperMissileExplosion_LargeDebris_LaserTrail)
{
	hasLine = true;
	lineColor	= "1.00 1.00 1.00 0.5";

	hasInner = false;
	innerColor = "1.00 1.00 0.00 0.3";
	innerWidth = "0.20";

	hasOuter = true;
	outerColor = "1.00 1.00 1.00 0.2";
	outerWidth = "0.40";

//	bitmap = "~/data/weapons/sniperrifle/explosion.trail";
//	bitmapWidth = 0.25;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 1000;
};

datablock ParticleData(BlueSniperMissileExplosion_LargeDebris_Particles2)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 0;
	textureName			 = "~/data/particles/cross1";
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

datablock ParticleEmitterData(BlueSniperMissileExplosion_LargeDebris_Emitter2)
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
	particles = "BlueSniperMissileExplosion_LargeDebris_Particles2";
};

datablock ParticleData(BlueSniperMissileExplosion_LargeDebris_Particles1)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 100;
	lifetimeVarianceMS	= 0;
	textureName			 = "~/data/particles/cross1";
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

datablock ParticleEmitterData(BlueSniperMissileExplosion_LargeDebris_Emitter1)
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
	particles = "BlueSniperMissileExplosion_LargeDebris_Particles1";
};

datablock ExplosionData(BlueSniperMissileExplosion_LargeDebris_Explosion)
{
	soundProfile	= SniperDebrisSound;

	debris = BlueSniperMissileExplosion_SmallDebris;
	debrisThetaMin = 0;
	debrisThetaMax = 60;
	debrisNum = 5;
	debrisVelocity = 15.0;
	debrisVelocityVariance = 10.0;
};

datablock DebrisData(BlueSniperMissileExplosion_LargeDebris)
{
	// shape...
	shapeFile = "~/data/misc/debris2.white.dts";

	explosion = BlueSniperMissileExplosion_LargeDebris_Explosion;

	//laserTrail = BlueSniperMissileExplosion_LargeDebris_LaserTrail;
	emitters[0] = BlueSniperMissileExplosion_LargeDebris_Emitter2;
	//emitters[1] = BlueSniperMissileExplosion_LargeDebris_Emitter1;

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

datablock ExplosionData(BlueSniperMissileExplosion)
{
	soundProfile	= SniperExplosionSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 200;

	debris = BlueSniperMissileExplosion_LargeDebris;
	debrisThetaMin = 0;
	debrisThetaMax = 60;
	debrisNum = 5;
	debrisVelocity = 30.0;
	debrisVelocityVariance = 10.0;

	particleEmitter = BlueSniperMissileExplosion_CloudEmitter;
	particleDensity = 100;
	particleRadius = 4;

	emitter[0] = BlueSniperMissileExplosion_DustEmitter;
	emitter[1] = BlueSniperMissileExplosion_SmokeEmitter;
	emitter[2] = BlueSniperMissileExplosion_SparksEmitter;

	// Camera shake
	shakeCamera = true;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 3;
	lightEndRadius = 20;
	lightStartColor = "1.0 1.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
};

