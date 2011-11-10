//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------

datablock ParticleData(WhiteSlideEmitter_Particle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 350;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.5";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	textureName	= "share/textures/rotc/dustParticle";
	allowLighting = false;
};

datablock ParticleEmitterData(WhiteSlideEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 2;
	velocityVariance = 1;
	ejectionOffset	= 0.75;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = WhiteSlideEmitter_Particle;
};

//------------------------------------------------------------------------------

datablock ParticleData(RedSlideEmitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 250;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 0.0 1.0 1.0";
	colors[1]	  = "1.0 0.0 0.0 1.0";
	colors[2]	  = "1.0 1.0 0.0 1.0";
	colors[3]	  = "0.0 5.0 0.0 0.0";
	sizes[0]		= 0.2;
	sizes[1]		= 0.2;
	sizes[2]		= 0.2;
	sizes[3]		= 0.2;
	times[0]		= 0.0;
	times[1]		= 0.333;
	times[2]		= 0.666;
	times[3]		= 1.0;
	textureName	= "share/textures/rotc/small_particle4";
	allowLighting = false;
};

datablock ParticleEmitterData(RedSlideEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 10;
	velocityVariance = 3;
	ejectionOffset	= 0.5;
	thetaMin			= 60;
	thetaMax			= 85;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = RedSlideEmitter_Particle;
};

//------------------------------------------------------------------------------

datablock ParticleData(BlueSlideEmitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 250;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 0.0 1.0 1.0";
	colors[1]	  = "0.0 0.0 1.0 1.0";
	colors[2]	  = "0.0 1.0 1.0 1.0";
	colors[3]	  = "0.0 5.0 0.0 0.0";
	sizes[0]		= 0.2;
	sizes[1]		= 0.2;
	sizes[2]		= 0.2;
	sizes[3]		= 0.2;
	times[0]		= 0.0;
	times[1]		= 0.333;
	times[2]		= 0.666;
	times[3]		= 1.0;
	textureName	= "share/textures/rotc/small_particle4";
	allowLighting = false;
};

datablock ParticleEmitterData(BlueSlideEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 10;
	velocityVariance = 3;
	ejectionOffset	= 0.5;
	thetaMin			= 60;
	thetaMax			= 85;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = BlueSlideEmitter_Particle;
};

//------------------------------------------------------------------------------

datablock DebrisData(TrooperDebris)
{
	explodeOnMaxBounce = false;

	elasticity = 0.35;
	friction = 0.5;

	lifetime = 4.0;
	lifetimeVariance = 1.0;

	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	numBounces = 5;
	bounceVariance = 0;

	staticOnMaxBounce = true;
	gravModifier = 1.0;

	useRadiusMass = true;
	baseRadius = 1;

	velocity = 18.0;
	velocityVariance = 12.0;
};

//----------------------------------------------------------------------------
// Splash
//----------------------------------------------------------------------------

datablock ParticleData(TrooperSplashMist)
{
	dragCoefficient		= 2.0;
	gravityCoefficient	= -0.05;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 400;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = false;
	spinRandomMin		  = -90.0;
	spinRandomMax		  = 500.0;
	textureName			 = "share/shapes/rotc/players/shared/splash";
	colors[0]	  = "0.7 0.8 1.0 1.0";
	colors[1]	  = "0.7 0.8 1.0 0.5";
	colors[2]	  = "0.7 0.8 1.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.8;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
};

datablock ParticleEmitterData(TrooperSplashMistEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 3.0;
	velocityVariance = 2.0;
	ejectionOffset	= 0.0;
	thetaMin			= 85;
	thetaMax			= 85;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	lifetimeMS		 = 250;
	particles = TrooperSplashMist;
};


datablock ParticleData(TrooperBubbleParticle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= -0.50;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 400;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = false;
	textureName			 = "share/shapes/rotc/players/shared/splash";
	colors[0]	  = "0.7 0.8 1.0 0.4";
	colors[1]	  = "0.7 0.8 1.0 0.4";
	colors[2]	  = "0.7 0.8 1.0 0.0";
	sizes[0]		= 0.1;
	sizes[1]		= 0.3;
	sizes[2]		= 0.3;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
};

datablock ParticleEmitterData(TrooperBubbleEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;
	ejectionVelocity = 2.0;
	ejectionOffset	= 0.5;
	velocityVariance = 0.5;
	thetaMin			= 0;
	thetaMax			= 80;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	particles = TrooperBubbleParticle;
};

datablock ParticleData(TrooperFoamParticle)
{
	dragCoefficient		= 2.0;
	gravityCoefficient	= -0.05;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 400;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = false;
	spinRandomMin		  = -90.0;
	spinRandomMax		  = 500.0;
	textureName			 = "share/shapes/rotc/players/shared/splash";
	colors[0]	  = "0.7 0.8 1.0 0.20";
	colors[1]	  = "0.7 0.8 1.0 0.20";
	colors[2]	  = "0.7 0.8 1.0 0.00";
	sizes[0]		= 0.2;
	sizes[1]		= 0.4;
	sizes[2]		= 1.6;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
};

datablock ParticleEmitterData(TrooperFoamEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 3.0;
	velocityVariance = 1.0;
	ejectionOffset	= 0.0;
	thetaMin			= 85;
	thetaMax			= 85;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	particles = TrooperFoamParticle;
};


datablock ParticleData(TrooperFoamDropletsParticle)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.2;
	inheritedVelFactor	= 0.2;
	constantAcceleration = -0.0;
	lifetimeMS			  = 600;
	lifetimeVarianceMS	= 0;
	textureName			 = "share/shapes/rotc/players/shared/splash";
	colors[0]	  = "0.7 0.8 1.0 1.0";
	colors[1]	  = "0.7 0.8 1.0 0.5";
	colors[2]	  = "0.7 0.8 1.0 0.0";
	sizes[0]		= 0.8;
	sizes[1]		= 0.3;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
};

datablock ParticleEmitterData(TrooperFoamDropletsEmitter)
{
	ejectionPeriodMS = 7;
	periodVarianceMS = 0;
	ejectionVelocity = 2;
	velocityVariance = 1.0;
	ejectionOffset	= 0.0;
	thetaMin			= 60;
	thetaMax			= 80;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	particles = TrooperFoamDropletsParticle;
};


datablock ParticleData(TrooperSplashParticle)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.2;
	inheritedVelFactor	= 0.2;
	constantAcceleration = -0.0;
	lifetimeMS			  = 600;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "0.7 0.8 1.0 1.0";
	colors[1]	  = "0.7 0.8 1.0 0.5";
	colors[2]	  = "0.7 0.8 1.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.5;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
};

datablock ParticleEmitterData(TrooperSplashEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;
	ejectionVelocity = 3;
	velocityVariance = 1.0;
	ejectionOffset	= 0.0;
	thetaMin			= 60;
	thetaMax			= 80;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 100;
	particles = TrooperSplashParticle;
};

datablock SplashData(TrooperSplash)
{
	numSegments = 15;
	ejectionFreq = 15;
	ejectionAngle = 40;
	ringLifetime = 0.5;
	lifetimeMS = 300;
	velocity = 4.0;
	startRadius = 0.0;
	acceleration = -3.0;
	texWrap = 5.0;

	texture = "share/shapes/rotc/players/shared/splash";

	emitter[0] = TrooperSplashEmitter;
	emitter[1] = TrooperSplashMistEmitter;

	colors[0] = "0.7 0.8 1.0 0.0";
	colors[1] = "0.7 0.8 1.0 0.3";
	colors[2] = "0.7 0.8 1.0 0.7";
	colors[3] = "0.7 0.8 1.0 0.0";
	times[0] = 0.0;
	times[1] = 0.4;
	times[2] = 0.8;
	times[3] = 1.0;
};

//----------------------------------------------------------------------------
// Foot prints
//----------------------------------------------------------------------------

datablock DecalData(RedTrooperFootprint)
{
	sizeX = "0.60";
	sizeY = "0.60";
	textureName = "share/textures/rotc/footprint.red";
	SelfIlluminated = true;
};

datablock DecalData(BlueTrooperFootprint)
{
	sizeX = "0.60";
	sizeY = "0.60";
	textureName = "share/textures/rotc/footprint.blue";
	SelfIlluminated = true;
};

//----------------------------------------------------------------------------
// Foot puffs
//----------------------------------------------------------------------------

datablock ParticleData(TrooperLightPuff)
{
	dragCoefficient		= 2.0;
	gravityCoefficient	= -0.01;
	inheritedVelFactor	= 0.6;
	constantAcceleration = 0.0;
	lifetimeMS			  = 800;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = true;
	spinRandomMin		  = -35.0;
	spinRandomMax		  = 35.0;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 0.1;
	sizes[1]		= 0.8;
	times[0]		= 0.3;
	times[1]		= 1.0;
	textureName	= "share/textures/rotc/dustParticle";
};

datablock ParticleEmitterData(TrooperLightPuffEmitter)
{
	ejectionPeriodMS = 35;
	periodVarianceMS = 10;
	ejectionVelocity = 0.2;
	velocityVariance = 0.1;
	ejectionOffset	= 0.0;
	thetaMin			= 20;
	thetaMax			= 60;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	useEmitterColors = true;
	particles = TrooperLightPuff;
};

//----------------------------------------------------------------------------
// Liftoff dust
//----------------------------------------------------------------------------

datablock ParticleData(TrooperLiftoffDust)
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
	colors[0]	  = "1.0 1.0 1.0 1.0";
	sizes[0]		= 1.0;
	times[0]		= 1.0;
	textureName	= "share/textures/rotc/dustParticle";
};

datablock ParticleEmitterData(TrooperLiftoffDustEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 2.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	useEmitterColors = true;
	particles = TrooperLiftoffDust;
};
