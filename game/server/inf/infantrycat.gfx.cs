//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Foot prints
//----------------------------------------------------------------------------

datablock DecalData(SoldierFootprint)
{
	sizeX = "0.18";
	sizeY = "0.27";
	textureName = "share/textures/inf/footprint";
	SelfIlluminated = false;
};

//----------------------------------------------------------------------------
// Foot puffs
//----------------------------------------------------------------------------

datablock ParticleData(SoldierFootPuff)
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
	sizes[0]		= 1.0;
	sizes[1]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 1.0;
	textureName	= "share/textures/rotc/dustParticle";
};

datablock ParticleEmitterData(SoldierFootPuffEmitter)
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
	particles = SoldierFootPuff;
};
