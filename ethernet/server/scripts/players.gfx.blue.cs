//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - players.gfx.blue.cs
// player related eyecandy
//------------------------------------------------------------------------------

datablock ParticleData(BluePlayerBleedEffect_Cloud)
{
	dragCoeffiecient   = 0.4;
	gravityCoefficient = 0;
	inheritedVelFactor = 0;

	lifetimeMS         = 200;
	lifetimeVarianceMS = 0;

	useInvAlpha   =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "~/data/particles/corona.png";

	colors[0]	  = "0.0 0.2 1.0 1.0";
	colors[1]	  = "0.0 0.2 1.0 0.5";
	colors[2]	  = "0.0 0.2 1.0 0.0";
	sizes[0]		= 3.0;
	sizes[1]		= 3.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(BluePlayerBleedEffect_CloudEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0;
	thetaMin         = 0.0;
	thetaMax         = 90.0;
	lifetimeMS		 = 100;
	particles = "BluePlayerBleedEffect_Cloud";
};

datablock ExplosionData(BluePlayerBleedEffect_Heavy)
{
	// shape...
	explosionShape = "~/data/effects/explosion_blue.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.1 0.1 0.1";
	sizes[1] = "0.8 0.8 0.8";
	times[0] = 0.0;
	times[1] = 1.0;

	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 8;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;

    // particles...
	particleEmitter = BluePlayerBleedEffect_CloudEmitter;
	particleDensity = 40;
	particleRadius = 4;

	//emitter[0] = BluePlayerBleedEffect_HeavyEmitter;
};

datablock ExplosionData(BluePlayerBleedEffect_Medium)
{
	// shape...
	explosionShape = "~/data/effects/explosion_blue.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.1 0.1 0.1";
	sizes[1] = "0.6 0.6 0.6";
	times[0] = 0.0;
	times[1] = 1.0;

	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 6;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;

    // particles...
	particleEmitter = BluePlayerBleedEffect_CloudEmitter;
	particleDensity = 30;
	particleRadius = 3;

	//emitter[0] = BluePlayerBleedEffect_MediumEmitter;
};

datablock ExplosionData(BluePlayerBleedEffect_Light)
{
	// shape...
	explosionShape = "~/data/effects/explosion_blue.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.0 0.0 0.0";
	sizes[1] = "0.4 0.4 0.4";
	times[0] = 0.0;
	times[1] = 1.0;

	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 4;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;

    // particles...
	particleEmitter = BluePlayerBleedEffect_CloudEmitter;
	particleDensity = 20;
	particleRadius = 2;

	//emitter[0] = BluePlayerBleedEffect_LightEmitter;
};

datablock ExplosionData(BluePlayerBleedEffect_Sting)
{
	// shape...
	explosionShape = "~/data/effects/explosion_blue.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.0 0.0 0.0";
	sizes[1] = "0.2 0.2 0.2";
	times[0] = 0.0;
	times[1] = 1.0;

	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 2;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;

    // particles...
	particleEmitter = BluePlayerBleedEffect_CloudEmitter;
	particleDensity = 10;
	particleRadius = 1;

	//emitter[0] = BluePlayerBleedEffect_StingEmitter;
};

datablock ExplosionData(BluePlayerBleedEffect_Buffer)
{
	// shape...
	explosionShape = "~/data/effects/explosion_white.dts";
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
