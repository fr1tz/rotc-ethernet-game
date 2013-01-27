//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - players.gfx.red.cs
// Player related eyecandy
//------------------------------------------------------------------------------

datablock ParticleData(RedPlayerBleedEffect_Cloud)
{
	dragCoeffiecient   = 0.4;
	gravityCoefficient = 0;
	inheritedVelFactor = 0;

	lifetimeMS         = 200;
	lifetimeVarianceMS = 0;

	useInvAlpha   =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/corona.png";

	colors[0]	  = "1.0 0.2 0.0 1.0";
	colors[1]	  = "1.0 0.2 0.0 0.5";
	colors[2]	  = "1.0 0.2 0.0 0.0";
	sizes[0]		= 3.0;
	sizes[1]		= 3.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(RedPlayerBleedEffect_CloudEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0;
	thetaMin         = 0.0;
	thetaMax         = 90.0;
	lifetimeMS		 = 100;
	particles = "RedPlayerBleedEffect_Cloud";
};

datablock ExplosionData(RedPlayerBleedEffect_Heavy)
{
	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion_red.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.1 0.1 0.1";
	sizes[1] = "0.6 0.6 0.6";
	times[0] = 0.0;
	times[1] = 1.0;
 
	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 8;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
    
    // particles...
	particleEmitter = RedPlayerBleedEffect_CloudEmitter;
	particleDensity = 40;
	particleRadius = 4;

	//emitter[0] = RedPlayerBleedEffect_HeavyEmitter;
};

datablock ExplosionData(RedPlayerBleedEffect_Medium)
{
	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion_red.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.1 0.1 0.1";
	sizes[1] = "0.4 0.4 0.4";
	times[0] = 0.0;
	times[1] = 1.0;
 
	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 6;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
    
    // particles...
	particleEmitter = RedPlayerBleedEffect_CloudEmitter;
	particleDensity = 30;
	particleRadius = 3;

	//emitter[0] = RedPlayerBleedEffect_MediumEmitter;
};

datablock ExplosionData(RedPlayerBleedEffect_Light)
{
	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion_red.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.0 0.0 0.0";
	sizes[1] = "0.2 0.2 0.2";
	times[0] = 0.0;
	times[1] = 1.0;
 
	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 4;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
    
    // particles...
	particleEmitter = RedPlayerBleedEffect_CloudEmitter;
	particleDensity = 20;
	particleRadius = 2;

	//emitter[0] = RedPlayerBleedEffect_LightEmitter;
};

datablock ExplosionData(RedPlayerBleedEffect_Sting)
{
	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion_red.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.0 0.0 0.0";
	sizes[1] = "0.1 0.1 0.1";
	times[0] = 0.0;
	times[1] = 1.0;
 
	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 2;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
    
    // particles...
	particleEmitter = RedPlayerBleedEffect_CloudEmitter;
	particleDensity = 10;
	particleRadius = 1;
	
	//emitter[0] = RedPlayerBleedEffect_StingEmitter;
};

datablock ExplosionData(RedPlayerBleedEffect_Buffer)
{
	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion_white.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.0 0.0 0.0";
	sizes[1] = "0.1 0.1 0.1";
	times[0] = 0.0;
	times[1] = 1.0;
 
	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 2;
	lightStartColor = "0.5 0.5 0.5";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
};

datablock ExplosionData(RedPlayerBleedEffect_Barrier)
{
	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion5.green.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.1 0.1 0.1";
	sizes[1] = "0.0 0.0 0.0";
	times[0] = 0.0;
	times[1] = 1.0;
 
	// dynamic light...
	lightStartRadius = 5;
	lightEndRadius = 0;
	lightStartColor = "0.0 1.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
};
