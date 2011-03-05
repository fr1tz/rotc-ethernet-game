//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - weapons.gfx.cs
// Eye-candy for all weapons
//------------------------------------------------------------------------------

datablock DecalData(SlashDecalOne)
{
	sizeX = "1.00";
	sizeY = "1.00";
	textureName = "share/textures/rotc/slashdecal1";
	SelfIlluminated = false;
};

datablock DecalData(BulletHoleDecalOne)
{
	sizeX = "0.15";
	sizeY = "0.15";
	textureName = "share/textures/rotc/bullethole1";
	SelfIlluminated = false;
};

datablock DecalData(ExplosionDecalOne)
{
	sizeX = "2.0";
	sizeY = "2.0";
	textureName = "share/textures/rotc/explosiondecal1";
	SelfIlluminated = false;
};

datablock DecalData(ExplosionDecalTwo)
{
	sizeX = "1.0";
	sizeY = "1.0";
	textureName = "share/textures/rotc/explosiondecal1";
	SelfIlluminated = false;
};

//------------------------------------------------------------------------------
// Default Medium Debris (Particles)

datablock ParticleData(DefaultMediumWhiteDebrisParticle)
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

datablock ParticleEmitterData(DefaultMediumWhiteDebrisEmitter)
{
	ejectionPeriodMS = 50;
	periodVarianceMS = 0;
	ejectionVelocity = 10.0;
	velocityVariance = 5.0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 60;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	lifetimeMS		 = 200;
	lifetimeVarianceMS = 0;
	overrideAdvances = false;
	orientParticles  = true;
	particles = "DefaultMediumWhiteDebrisParticle";
};

//------------------------------------------------------------------------------
// Default Small Debris (Particles)

datablock ParticleData(DefaultSmallWhiteDebrisParticle)
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

datablock ParticleEmitterData(DefaultSmallWhiteDebrisEmitter)
{
	ejectionPeriodMS = 50;
	periodVarianceMS = 0;
	ejectionVelocity = 10.0;
	velocityVariance = 5.0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 60;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	lifetimeMS		 = 5;
	lifetimeVarianceMS = 0;
	overrideAdvances = false;
	orientParticles  = true;
	particles = "DefaultSmallWhiteDebrisParticle";
};

datablock ParticleData(DefaultSmallRedDebrisParticle : DefaultSmallWhiteDebrisParticle)
{
	textureName = "share/shapes/rotc/misc/debris1.red";
};

datablock ParticleEmitterData(DefaultSmallRedDebrisEmitter : DefaultSmallWhiteDebrisEmitter)
{
	particles = "DefaultSmallRedDebrisParticle";
};

datablock ParticleData(DefaultSmallBlueDebrisParticle : DefaultSmallWhiteDebrisParticle)
{
	textureName = "share/shapes/rotc/misc/debris1.blue";
};

datablock ParticleEmitterData(DefaultSmallBlueDebrisEmitter : DefaultSmallWhiteDebrisEmitter)
{
	particles = "DefaultSmallBlueDebrisParticle";
};

//------------------------------------------------------------------------------

datablock ParticleData(DefaultProjectileNearEnemyExplosion_ExplosionCloud)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 1;
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 350;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;
	textureName			 = "share/textures/rotc/particle1";
	colors[0]	  = "1.00 1.00 1.00 0.8";
	colors[1]	  = "0.90 0.90 0.00 0.4";
	colors[2]	  = "0.90 0.90 0.00 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.2;
	times[2]		= 1.0;

};

datablock ParticleEmitterData(DefaultProjectileNearEnemyExplosion_CloudEmitter)
{
	ejectionPeriodMS = 4;
	periodVarianceMS = 0;
	ejectionVelocity = 5;
	velocityVariance = 0;
	ejectionOffset	= 0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 100;
	particles = "DefaultProjectileNearEnemyExplosion_ExplosionCloud";
};

datablock ExplosionData(DefaultProjectileNearEnemyExplosion)
{
	soundProfile = DefaultProjectileNearEnemyExplosionSound;

	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion_white.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.1 0.1 0.1";
	sizes[1] = "0.0 0.0 0.0";
	times[0] = 0.0;
	times[1] = 1.0;
	
	// particle emitters...
	//emitter[0] = DefaultProjectileNearEnemyExplosion_CloudEmitter;
	
	// dynamic light...
	lightStartRadius = 8;
	lightEndRadius = 2;
	lightStartColor = "1.0 1.0 1.0 1.0";
	lightEndColor = "1.0 1.0 1.0 0.3";
};


