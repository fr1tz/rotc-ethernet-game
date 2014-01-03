//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(RedBlaster5ProjectileLaserTail)
{
	hasLine = true;
	lineStartColor	= "1.00 0.00 0.00 0.0";
	lineBetweenColor = "1.00 0.00 0.00 0.25";
	lineEndColor	  = "1.00 0.00 0.00 0.5";
 	lineWidth		  = 1.0;

	hasInner = true;
	innerStartColor	= "1.00 0.50 0.50 0.0";
	innerBetweenColor = "1.00 0.50 0.50 0.5";
	innerEndColor	  = "1.00 0.50 0.50 1.0";
	innerStartWidth	= "0.0";
	innerBetweenWidth = "0.2";
	innerEndWidth	  = "0.4";

	hasOuter = false;
	outerStartColor	= "1.00 1.00 0.00 0.0";
	outerBetweenColor = "1.00 1.00 0.00 0.2";
	outerEndColor	  = "1.00 1.00 0.00 0.2";
	outerStartWidth	= "0.0";
	outerBetweenWidth = "0.3";
	outerEndWidth	  = "0.0";

	bitmap = "share/shapes/rotc/weapons/blaster/lasertail.red";
	bitmapWidth = 0.1;
	//crossBitmap = "share/shapes/rotc/weapons/blaster/projectile.impact.green";
	//crossBitmapWidth = 4.25;

	betweenFactor = 0.5;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(RedBlaster5ProjectileHit_Cloud)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 250;
	lifetimeVarianceMS	= 0;

	useInvAlpha = false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/corona3.png";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.5";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 1.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(RedBlaster5ProjectileHit_CloudEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;

	ejectionVelocity = 0.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "RedBlaster5ProjectileHit_Cloud";
};

datablock ParticleData(RedBlaster5ProjectileHit_Dust)
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

datablock ParticleEmitterData(RedBlaster5ProjectileHit_DustEmitter)
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
	particles = "RedBlaster5ProjectileHit_Dust";
};


datablock ParticleData(RedBlaster5ProjectileHit_Smoke)
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
	sizes[1]		= 5.0;
	sizes[2]		= 0.6;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(RedBlaster5ProjectileHit_SmokeEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

	lifetimeMS		 = 250;

	particles = "RedBlaster5ProjectileHit_Smoke";
};

datablock ParticleData(RedBlaster5ProjectileHit_Debris_Smoke)
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

datablock ParticleEmitterData(RedBlaster5ProjectileHit_Debris_SmokeEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;

	ejectionVelocity = 0.0;
	velocityVariance = 0.0;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

   //lifetimeMS		 = 250;

	particles = "RedBlaster5ProjectileHit_Debris_Smoke";
};


datablock DebrisData(RedBlaster5ProjectileHit_Debris)
{
//	shapeFile = "share/shapes/rotc/misc/debris2.white.dts";
 
	emitters[0] = RedBlaster5ProjectileHit_Debris_SmokeEmitter;
//	emitters[1] = RedBlaster5ProjectileHit_Debris_GlowEmitter;

//	laserTrail = RedBlaster5ProjectileHit_Debris_LaserTrail;

	// bounce...
	numBounces = 3;
	explodeOnMaxBounce = false;

	// physics...
	gravModifier = 5.0;
	elasticity = 0.6;
	friction = 0.1;

	lifetime = 0.25;
	lifetimeVariance = 0.05;
};

datablock ExplosionData(RedBlaster5ProjectileHit)
{
	soundProfile = Blaster5ProjectileHitSound;

	lifetimeMS = 250;

 	// shape...
	//explosionShape = "share/shapes/rotc/effects/explosion2_white.dts";
	//faceViewer	  = false;
	//playSpeed = 8.0;
	//sizes[0] = "0.2 0.2 0.2";
	//sizes[1] = "0.2 0.2 0.2";
	//times[0] = 0.0;
	//times[1] = 1.0;

	particleEmitter = RedBlaster5ProjectileHit_CloudEmitter;
	particleDensity = 25;
	particleRadius = 0.1;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 5;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "1.0 0.0 0.0";
};

//-----------------------------------------------------------------------------
// impact

datablock ParticleData(RedBlaster5ProjectileImpact_DebrisParticle)
{
	spinSpeed = 200;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;
	dragCoefficient		= 1;
	gravityCoefficient	= 2.5;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1500;
	lifetimeVarianceMS	= 0;
	textureName = "share/shapes/rotc/misc/debris1.white";
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 1.0";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 0.25;
	sizes[1]		= 0.25;
	sizes[2]		= 0.25;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	useInvAlpha =  false;
	allowLighting = false;
};

datablock ParticleEmitterData(RedBlaster5ProjectileImpact_DebrisEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 10.0;
	velocityVariance = 5.0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 60;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	lifetimeMS		 = 100;
	lifetimeVarianceMS = 0;
	overrideAdvances = false;
	orientParticles  = true;
	particles = "RedBlaster5ProjectileImpact_DebrisParticle";
};

datablock ExplosionData(RedBlaster5ProjectileImpact : RedBlaster5ProjectileHit)
{
	soundProfile = Blaster5ProjectileImpactSound;
	emitter[0] = RedBlaster5ProjectileHit_SmokeEmitter;
	emitter[1] = RedBlaster5ProjectileHit_DustEmitter;
	emitter[2] = RedBlaster5ProjectileImpact_DebrisEmitter;
	debris = RedBlaster5ProjectileHit_Debris;
	debrisThetaMin = 0;
	debrisThetaMax = 80;
	debrisNum = 3;
	debrisNumVariance = 0;
	debrisVelocity = 50.0;
	debrisVelocityVariance = 10.0;
};

//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(RedBlaster5ProjectileMissedEnemyEffect)
{
	soundProfile = Blaster5ProjectileMissedEnemySound;

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
