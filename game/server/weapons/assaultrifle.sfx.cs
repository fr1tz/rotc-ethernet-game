//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - assaultrifle.sfx.cs
// Sound effects for the assault rifle
//------------------------------------------------------------------------------

datablock AudioProfile(AssaultRifleFireSound)
{
	filename = "share/sounds/rotc/fire2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(AssaultRifleProjectileSound)
{
	filename = "share/sounds/rotc/charge5.wav";
	description = AudioCloseLooping3D;
	preload = true;	
};

datablock AudioProfile(AssaultRifleProjectileExplosionSound)
{
	filename = "share/sounds/rotc/explosion2.wav";
	description = AudioDefault3D;
	preload = true;	
};

datablock AudioProfile(AssaultRifleProjectileImpactSound)
{
	filename = "share/sounds/rotc/explosion2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(AssaultRifleProjectileHitSound)
{
	filename = "share/sounds/rotc/explosion2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(AssaultRifleProjectileBounceSound)
{
	filename = "share/sounds/rotc/ricochet2-1.wav";
	alternate[0] = "share/sounds/rotc/ricochet2-1.wav";
	alternate[1] = "share/sounds/rotc/ricochet2-2.wav";
	alternate[2] = "share/sounds/rotc/ricochet2-3.wav";
//	alternate[3] = "share/sounds/rotc/ricochet2-1.wav";
//	alternate[4] = "share/sounds/rotc/ricochet2-1.wav";
//	alternate[5] = "share/sounds/rotc/ricochet2-1.wav";
	description = AudioClose3D;
	preload = true;
};

