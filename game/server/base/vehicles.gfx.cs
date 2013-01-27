//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - vehicles.gfx.cs
// eye-candy for the vehicles
//------------------------------------------------------------------------------

datablock ParticleData(HugeVehicleParticleEmitter_Particle)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 350;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "0.5 0.5 0.5 1.0";
	colors[2]	  = "0.5 0.5 0.5 0.5";
	colors[3]	  = "0.5 0.5 0.5 0.0";
	sizes[0]		= 0.8;
	sizes[1]		= 0.8;
	sizes[2]		= 0.8;
	sizes[3]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.3;
	times[2]		= 0.6;
	times[4]		= 1.0;
	textureName	= "share/textures/rotc/smoke_particle";
};

datablock ParticleEmitterData(HugeVehicleParticleEmitter)
{
	ejectionPeriodMS = 20;
	periodVarianceMS = 5;
	ejectionVelocity = 0;
	velocityVariance = 0;
	ejectionOffset	= 0;
	thetaMin			= 0;
	thetaMax			= 10;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = HugeVehicleParticleEmitter_Particle;
};

//------------------------------------------------------------------------------

datablock ParticleData(HugeVehicleExplosion_Dust)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= -0.01;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1500;
	lifetimeVarianceMS	= 100;
	spinRandomMin		  = -90.0;
	spinRandomMax		  = 500.0;
	textureName			 = "share/textures/rotc/smoke_particle.png";
	colors[0]	  = "0.3 0.3 0.3 0.5";
	colors[1]	  = "0.3 0.3 0.3 0.5";
	colors[2]	  = "0.3 0.3 0.3 0.0";
	sizes[0]		= 3.2;
	sizes[1]		= 4.6;
	sizes[2]		= 5.0;
	times[0]		= 0.0;
	times[1]		= 0.7;
	times[2]		= 1.0;
	allowLighting = true;
};

datablock ParticleEmitterData(HugeVehicleExplosion_DustEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 15.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 85;
	thetaMax			= 85;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	lifetimeMS		 = 250;
	particles = "HugeVehicleExplosion_Dust";
};


datablock ParticleData(HugeVehicleExplosion_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.5;	// rises slowly
	inheritedVelFactor	= 0.025;
	lifetimeMS			  = 1250;
	lifetimeVarianceMS	= 0;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;
	textureName = "share/textures/rotc/smoke_particle.png";
	colors[0]	  = "0.7 0.7 0.7 1.0";
	colors[1]	  = "0.2 0.2 0.2 1.0";
	colors[2]	  = "0.1 0.1 0.1 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 6.0;
	sizes[2]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	allowLighting = true;
};

datablock ParticleEmitterData(HugeVehicleExplosion_SmokeEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 6.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 250;

	particles = "HugeVehicleExplosion_Smoke";
};



datablock ParticleData(HugeVehicleExplosion_Sparks)
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

datablock ParticleEmitterData(HugeVehicleExplosion_SparksEmitter)
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
	particles = "HugeVehicleExplosion_Sparks";
};

datablock ExplosionData(HugeVehicleSubExplosion)
{
	soundProfile	= HugeVehicleExplosionSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";
	
	delayMS = 0;
	delayVariance = 0;
	offset = 3;

	emitter[0] = HugeVehicleExplosion_DustEmitter;
	emitter[1] = HugeVehicleExplosion_SmokeEmitter;
	emitter[2] = HugeVehicleExplosion_SparksEmitter;

	shakeCamera = true;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 15;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 0.0 1.0";
	lightEndColor = "1.0 1.0 0.0 0.0";
};

datablock ParticleData(HugeVehicleExplosion_Debris_Emitter_Particles)
{
	dragCoefficient		= 1;
	gravityCoefficient	= -0.5;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 200;
	lifetimeVarianceMS	= 0;
	textureName			 = "share/textures/rotc/smoke_particle";
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 0.0 1.0";
	colors[2]	  = "1.0 1.0 0.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

};

datablock ParticleEmitterData(HugeVehicleExplosion_Debris_Emitter)
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
	particles = "HugeVehicleExplosion_Debris_Emitter_Particles";
};

datablock MultiNodeLaserBeamData(HugeVehicleExplosion_Debris_LaserTrail)
{
	hasLine = false;
	lineColor	= "1.00 1.00 0.00 0.5";

	hasInner = false;
	innerColor = "1.00 1.00 1.00 0.3";
	innerWidth = "0.50";

	hasOuter = true;
	outerColor = "1.00 1.00 0.00 0.3";
	outerWidth = "0.4";

	//bitmap = "~/data/projectiles/Disc/lasertrail";
	//bitmapWidth = 1.00;

	blendMode = 1;
	//renderMode = $MultiNodeLaserBeamRenderMode::Horizontal;
	fadeTime = 400;
};

datablock DebrisData(HugeVehicleExplosion_Debris)
{
//	shapeFile = "share/shapes/rotc/weapons/hegrenade/grenade.dts";
	emitters[0] = HugeVehicleExplosion_Debris_Emitter;

	laserTrail = HugeVehicleExplosion_Debris_LaserTrail;

	// bounce...
	numBounces = 3;
	explodeOnMaxBounce = true;

	// physics...
	gravModifier = 3.0;
	elasticity = 0.6;
	friction = 0.1;

	lifetime = 8.0;
	lifetimeVariance = 0.02;
};

datablock ExplosionData(HugeVehicleExplosion)
{
	soundProfile	= HeGrenadeExplodeSound;

	faceViewer	  = true;

	debris = HugeVehicleExplosion_Debris;
	debrisThetaMin = 0;
	debrisThetaMax = 50;
	debrisNum = 6;
	debrisVelocity = 26.0;
	debrisVelocityVariance = 7.0;
	
	subExplosion[0] = HugeVehicleSubExplosion;
	subExplosion[1] = HugeVehicleSubExplosion;
	subExplosion[2] = HugeVehicleSubExplosion;

	emitter[0] = HugeVehicleExplosion_DustEmitter;
	emitter[1] = HugeVehicleExplosion_SmokeEmitter;
	emitter[2] = HugeVehicleExplosion_SparksEmitter;

	shakeCamera = true;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;
	
	// Dynamic light
	lightStartRadius = 15;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 0.0 1.0";
	lightEndColor = "1.0 1.0 0.0 0.0";
};

//------------------------------------------------------------------------------

datablock ExplosionData(MediumVehicleExplosion)
{
	soundProfile	= HeGrenadeExplodeSound;

	faceViewer	  = true;

	debris = HugeVehicleExplosion_Debris;
	debrisThetaMin = 0;
	debrisThetaMax = 50;
	debrisNum = 4;
	debrisVelocity = 26.0;
	debrisVelocityVariance = 7.0;

	emitter[0] = HugeVehicleExplosion_DustEmitter;
	emitter[1] = HugeVehicleExplosion_SmokeEmitter;
	emitter[2] = HugeVehicleExplosion_SparksEmitter;

	shakeCamera = true;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 15;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 0.0 1.0";
	lightEndColor = "1.0 1.0 0.0 0.0";
};

//------------------------------------------------------------------------------

datablock ParticleData(NoDamageVehicleExplosion_Sparks)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 400;
	lifetimeVarianceMS	= 50;
	textureName			 = "share/textures/rotc/spark01";
	colors[0]	  = "1.0 1.0 0.0 1.0";
	colors[1]	  = "1.0 1.0 0.0 1.0";
	colors[2]	  = "1.0 1.0 0.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

};

datablock ParticleEmitterData(NoDamageVehicleExplosion_SparksEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 10.0;
	velocityVariance = 0.2;
	ejectionOffset	= 0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 100;
	particles = "NoDamageVehicleExplosion_Sparks";
};

datablock ExplosionData(NoDamageVehicleExplosion)
{
	soundProfile	= AssaultRifleProjectileImpactSound;
	
	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion_white.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.0 0.0 0.0";
	sizes[1] = "0.1 0.1 0.1";
	times[0] = 0.0;
	times[1] = 1.0;

	lifetimeMS = 250;

	emitter[0] = NoDamageVehicleExplosion_SparksEmitter;

	// Dynamic light
	lightStartRadius = 1;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 1.0 1.0";
	lightEndColor = "1.0 1.0 1.0 0.3";

	shakeCamera = false;
};
