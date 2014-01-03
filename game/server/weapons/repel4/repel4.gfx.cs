//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Red Effects...

datablock ExplosionData(RedRepel4Explosion1)
{
	soundProfile = Repel4HitSound;

	lifetimeMS = 2000;

 	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion4_red.dts";
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

datablock ExplosionData(RedRepel4Explosion2 : RedRepel4Explosion1)
{
	camShakeAmp = "80.0 80.0 80.0";
	camShakeDuration = 0.25;
};

datablock ExplosionData(RedRepel4Explosion3 : RedRepel4Explosion1)
{
	camShakeAmp = "160.0 160.0 160.0";
	camShakeDuration = 0.25;
};

datablock ExplosionData(RedRepel4Explosion4 : RedRepel4Explosion1)
{
	camShakeAmp = "240.0 240.0 240.0";
	camShakeDuration = 0.5;
};

datablock ExplosionData(RedRepel4Explosion5 : RedRepel4Explosion1)
{
	camShakeAmp = "320.0 320.0 320.0";
	camShakeDuration = 0.75;
};

datablock ExplosionData(RedRepel4ProjectileExplosion : RedRepel4Explosion1)
{
	sizes[0] = "0.1 0.1 0.1";
	shakeCamera = false;
};

//------------------------------------------------------------------------------
// Blue Effects...

datablock ExplosionData(BlueRepel4Explosion1 : RedRepel4Explosion1)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_blue.dts";
};

datablock ExplosionData(BlueRepel4Explosion2 : RedRepel4Explosion2)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_blue.dts";
};

datablock ExplosionData(BlueRepel4Explosion3 : RedRepel4Explosion3)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_blue.dts";
};

datablock ExplosionData(BlueRepel4Explosion4 : RedRepel4Explosion4)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_blue.dts";
};

datablock ExplosionData(BlueRepel4Explosion5 : RedRepel4Explosion5)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_blue.dts";
};

datablock ExplosionData(BlueRepel4ProjectileExplosion : RedRepel4Explosion1)
{
	explosionShape = "share/shapes/rotc/effects/explosion4_blue.dts";
	sizes[0] = "0.1 0.1 0.1";
	shakeCamera = false;
};


