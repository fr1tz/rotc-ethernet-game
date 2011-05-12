//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

datablock AudioProfile(RepelGunFireSound)
{
	filename = "share/sounds/rotc/fire3.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(RepelGunProjectileBounceSound)
{
	filename = "share/sounds/rotc/bounce1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(RepelGunProjectileExplosionSound)
{
	filename = "share/sounds/rotc/explosion1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(RepelGunProjectileDebrisSound)
{
	filename = "share/sounds/rotc/debris1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(RepelGunMineExplosionSound)
{
	filename = "share/sounds/rotc/explosion10.wav";
	description = AudioFar3D;
	preload = true;
};


datablock AudioProfile(RepelGunProjectileSound)
{
	filename = "share/sounds/rotc/missile1.wav";
	description = AudioCloseLooping3d;
	preload = true;
};
