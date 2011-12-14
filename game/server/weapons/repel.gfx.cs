//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Orange Effects...

datablock ExplosionData(OrangeRepelExplosion1)
{
	soundProfile = RepelHitSound;

	lifetimeMS = 2000;

 	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion4_orange.dts";
	faceViewer	  = true;
	playSpeed = 3.0;
	sizes[0] = "0.4 0.4 0.4";
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

datablock ExplosionData(OrangeRepelExplosion2 : OrangeRepelExplosion1)
{
	camShakeAmp = "80.0 80.0 80.0";
	camShakeDuration = 0.25;
};

datablock ExplosionData(OrangeRepelExplosion3 : OrangeRepelExplosion1)
{
	camShakeAmp = "160.0 160.0 160.0";
	camShakeDuration = 0.25;
};

datablock ExplosionData(OrangeRepelExplosion4 : OrangeRepelExplosion1)
{
	camShakeAmp = "240.0 240.0 240.0";
	camShakeDuration = 0.5;
};

datablock ExplosionData(OrangeRepelExplosion5 : OrangeRepelExplosion1)
{
	camShakeAmp = "320.0 320.0 320.0";
	camShakeDuration = 0.75;
};

datablock ExplosionData(OrangeRepelProjectileExplosion : OrangeRepelExplosion1)
{
	sizes[0] = "0.1 0.1 0.1";
	shakeCamera = false;
};

//------------------------------------------------------------------------------
// Green Effects...

datablock ExplosionData(GreenRepelExplosion1 : OrangeRepelExplosion1)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_green.dts";
};

datablock ExplosionData(GreenRepelExplosion2 : OrangeRepelExplosion2)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_green.dts";
};

datablock ExplosionData(GreenRepelExplosion3 : OrangeRepelExplosion3)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_green.dts";
};

datablock ExplosionData(GreenRepelExplosion4 : OrangeRepelExplosion4)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_green.dts";
};

datablock ExplosionData(GreenRepelExplosion5 : OrangeRepelExplosion5)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_green.dts";
};

datablock ExplosionData(GreenRepelProjectileExplosion : OrangeRepelExplosion1)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_green.dts";
	sizes[0] = "0.1 0.1 0.1";
	shakeCamera = false;
};

//------------------------------------------------------------------------------
// Source Effect...

datablock ExplosionData(RepelSourceExplosion)
{
	soundProfile = OrangeRepelExplosionSound;

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

