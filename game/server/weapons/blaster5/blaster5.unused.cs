//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------


datablock ParticleData(RedBlaster5ProjectileParticleEmitter_Particles)
{
	dragCoefficient      = 1;
	gravityCoefficient   = 0.0;
	windCoefficient      = 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			   = 1000;
	lifetimeVarianceMS	= 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;
	textureName			   = "share/textures/rotc/ring";
	colors[0]            = "1.0 0.0 0.0 0.2";
	colors[1]            = "1.0 0.0 0.0 0.1";
	colors[2]            = "1.0 0.0 0.0 0.0";
	sizes[0]             = 0.0;
	sizes[1]             = 1.0;
	sizes[2]             = 2.0;
	times[0]             = 0.0;
	times[1]             = 0.5;
	times[2]             = 1.0;
	useInvAlpha          = false;
	renderDot            = false;
};

datablock ParticleEmitterData(RedBlaster5ProjectileParticleEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 2.5;
	ejectionOffset   = 0.0;
	thetaMin         = 0;
	thetaMax         = 0;
	phiReferenceVel  = 0;
	phiVariance      = 0;
	overrideAdvances = false;
	orientParticles  = false;
	//lifetimeMS		 = 1000;
	particles = "RedBlaster5ProjectileParticleEmitter_Particles";
};


