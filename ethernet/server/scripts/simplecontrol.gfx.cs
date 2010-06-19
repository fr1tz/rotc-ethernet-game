//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - simplecontrol.gfx.cs
// standard effects for ROTC's "simple control (tm)"
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// move effect

datablock ParticleData(SimpleControlMoveEffect_Spark)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 400;
	lifetimeVarianceMS	= 0;
	textureName			 = "~/data/particles/spark01.png";
	colors[0]	  = "0.0 1.0 0.0 1.0";
	colors[1]	  = "0.0 1.0 0.0 1.0";
	colors[2]	  = "0.0 1.0 0.0 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 2.0;
	sizes[2]		= 1.5;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

};

datablock ParticleEmitterData(SimpleControlMoveEffect_SparkEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 6;
	velocityVariance = 0;
	ejectionOffset	= 0.0;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 100;
	particles = SimpleControlMoveEffect_Spark;
};

datablock ExplosionData(SimpleControlMoveEffect)
{
// soundProfile	= SniperImpactSound;
	emitter[0] = SimpleControlMoveEffect_SparkEmitter;
};

//-----------------------------------------------------------------------------
// attack effect

datablock ParticleData(SimpleControlAttackEffect_Spark)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 400;
	lifetimeVarianceMS	= 0;
	textureName			 = "~/data/particles/spark01.png";
	colors[0]	  = "1.0 0.0 0.0 1.0";
	colors[1]	  = "1.0 0.0 0.0 1.0";
	colors[2]	  = "1.0 0.0 0.0 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 2.0;
	sizes[2]		= 1.5;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

};

datablock ParticleEmitterData(SimpleControlAttackEffect_SparkEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 6;
	velocityVariance = 0;
	ejectionOffset	= 0.0;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 100;
	particles = SimpleControlAttackEffect_Spark;
};

datablock ExplosionData(SimpleControlAttackEffect)
{
// soundProfile	= SniperImpactSound;
	emitter[0] = SimpleControlAttackEffect_SparkEmitter;
};

//-----------------------------------------------------------------------------
// follow effect

datablock ParticleData(SimpleControlFollowEffect_Spark)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 400;
	lifetimeVarianceMS	= 0;
	textureName			 = "~/data/particles/spark01.png";
	colors[0]	  = "0.0 1.0 1.0 1.0";
	colors[1]	  = "0.0 1.0 1.0 1.0";
	colors[2]	  = "0.0 1.0 1.0 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 2.0;
	sizes[2]		= 1.5;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

};

datablock ParticleEmitterData(SimpleControlFollowEffect_SparkEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 6;
	velocityVariance = 0;
	ejectionOffset	= 0.0;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 100;
	particles = SimpleControlFollowEffect_Spark;
};

datablock ExplosionData(SimpleControlFollowEffect)
{
// soundProfile	= SniperImpactSound;
	emitter[0] = SimpleControlFollowEffect_SparkEmitter;
};

