//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(OrangeGrenade2_ParticleEmitter_Particles)
{
   dragCoefficient      = 0;
   gravityCoefficient   = 0.0;
   windCoefficient      = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   textureName          = "share/textures/rotc/star3.orange";
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

datablock ParticleEmitterData(OrangeGrenade2_ParticleEmitter)
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
   particles = "OrangeGrenade2_ParticleEmitter_Particles";
};

//-----------------------------------------------------------------------------
// laser trail

//-----------------------------------------------------------------------------
// bounce

datablock ExplosionData(OrangeGrenade2BounceEffect)
{
    soundProfile   = Grenade2BounceSound;
};

//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(OrangeGrenade2Explosion_Cloud)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/corona1.png";

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

datablock ParticleEmitterData(OrangeGrenade2Explosion_CloudEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 6.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "OrangeGrenade2Explosion_Cloud";
};

datablock ExplosionData(OrangeGrenade2Explosion)
{
	soundProfile = Grenade2ExplodeSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 200;

	particleEmitter = OrangeGrenade2Explosion_CloudEmitter;
	particleDensity = 300;
	particleRadius = 15;

	//emitter[0] = OrangeGrenade2Explosion_DustEmitter;
	//emitter[1] = OrangeGrenade2Explosion_SmokeEmitter;
	//emitter[2] = OrangeGrenade2Explosion_SparksEmitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 15;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
};
