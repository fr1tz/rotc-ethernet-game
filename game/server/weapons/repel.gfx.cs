//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

datablock ExplosionData(RepelExplosion1)
{
	lifetimeMS = 200;

 	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion2_white.dts";
	faceViewer	  = true;
	playSpeed = 3.0;
	sizes[0] = "0.1 0.1 0.1";
	sizes[1] = "0.0 0.0 0.0";
	times[0] = 0.0;
	times[1] = 1.0;
 
	//emitter[0] = RedSeekerDiscExplosion_Emitter;

	// Camera shake
	shakeCamera = true;
	camShakeFreq = "10.0 6.0 12.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.25;
	camShakeRadius = 5.0;

	// Dynamic light
	lightStartRadius = 3;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
};

datablock ExplosionData(RepelExplosion2 : RepelExplosion1)
{
	sizes[0] = "0.2 0.2 0.2";
	lightStartRadius = 6;
	camShakeAmp = "80.0 80.0 80.0";
	camShakeDuration = 0.25;
	//lightStartColor = "0.0 1.0 0.0";
};


datablock ExplosionData(RepelExplosion3 : RepelExplosion1)
{
	sizes[0] = "0.4 0.4 0.4";
	lightStartRadius = 12;
	camShakeAmp = "160.0 160.0 160.0";
	camShakeDuration = 0.25;
	//lightStartColor = "0.0 0.0 3.0";
};


datablock ExplosionData(RepelExplosion4 : RepelExplosion1)
{
	sizes[0] = "0.6 0.6 0.6";
	lightStartRadius = 16;
	camShakeAmp = "240.0 240.0 240.0";
	camShakeDuration = 0.5;
	//lightStartColor = "1.0 1.0 0.0";
};

datablock ExplosionData(RepelExplosion5 : RepelExplosion1)
{
	sizes[0] = "0.8 0.8 0.8";
	lightStartRadius = 20;
	camShakeAmp = "320.0 320.0 320.0";
	camShakeDuration = 0.75;
	//lightStartColor = "1.0 0.0 1.0";
};


datablock ExplosionData(RepelSourceExplosion)
{
	soundProfile = RepelExplosionSound;

	lifetimeMS = 200;

 	// shape...
	//explosionShape = "share/shapes/rotc/effects/explosion2_white.dts";
	//faceViewer	  = true;
	//playSpeed = 3.0;
	//sizes[0] = "1.0 1.0 1.0";
	//sizes[1] = "0.0 0.0 0.0";
	//times[0] = 0.0;
	//times[1] = 1.0;
 
	//emitter[0] = RedSeekerDiscExplosion_Emitter;

	// Camera shake
	shakeCamera = true;
	camShakeFreq = "10.0 6.0 12.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.25;
	camShakeRadius = 2.0;

	// Dynamic light
	lightStartRadius = 20;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
};

