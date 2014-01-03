//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(GreenGrenade2_ParticleEmitter_Particles)
{
   dragCoefficient      = 0;
   gravityCoefficient   = 0.0;
   windCoefficient      = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   textureName          = "share/textures/rotc/star3.green";
   colors[0]     = "1.0 1.0 1.0 1.0";
   colors[1]     = "1.0 1.0 1.0 0.5";
   colors[2]     = "1.0 1.0 1.0 0.0";
   sizes[0]      = 0.90;
   sizes[1]      = 0.70;
   sizes[2]      = 0.50;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(GreenGrenade2_ParticleEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 2;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = false;
   lifetimeMS       = 0;
   particles = "GreenGrenade2_ParticleEmitter_Particles";
};

//-----------------------------------------------------------------------------
// laser trail

//-----------------------------------------------------------------------------
// bounce

datablock ExplosionData(GreenGrenade2BounceEffect)
{
    soundProfile   = Grenade2BounceSound;
};

//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(GreenGrenade2Explosion_Cloud)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/corona5.png";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.0";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 12.0;
	sizes[1]		= 9.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.25;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(GreenGrenade2Explosion_CloudEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 6.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "GreenGrenade2Explosion_Cloud";
};

datablock ExplosionData(GreenGrenade2Explosion)
{
	soundProfile = Grenade2ExplodeSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 200;

	particleEmitter = GreenGrenade2Explosion_CloudEmitter;
	particleDensity = 300;
	particleRadius = 15;

	//emitter[0] = GreenGrenade2Explosion_DustEmitter;
	//emitter[1] = GreenGrenade2Explosion_SmokeEmitter;
	//emitter[2] = GreenGrenade2Explosion_SparksEmitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 15;
	lightEndRadius = 0;
	lightStartColor = "0.0 1.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
};
