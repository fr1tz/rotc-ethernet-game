//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

datablock AudioProfile(Blaster2FireSound)
{
	filename = "share/sounds/rotc/fire1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(Blaster2ProjectileSound)
{
	filename = "share/sounds/rotc/charge5.wav";
	description = AudioCloseLooping3D;
	preload = true;	
};

datablock AudioProfile(Blaster2ProjectileExplosionSound)
{
	filename = "share/sounds/rotc/explosion2.wav";
	description = AudioDefault3D;
	preload = true;	
};

datablock AudioProfile(Blaster2ProjectileImpactSound)
{
	filename = "share/sounds/rotc/explosion2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(Blaster2ProjectileHitSound)
{
	filename = "share/sounds/rotc/explosion2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(Blaster2ProjectileBounceSound)
{
	filename = "share/sounds/rotc/bounce1.wav";
//	alternate[0] = "share/sounds/rotc/impact3-1.wav";
//	alternate[1] = "share/sounds/rotc/impact3-2.wav";
//	alternate[2] = "share/sounds/rotc/impact3-3.wav";
//	alternate[3] = "share/sounds/rotc/ricochet2-1.wav";
//	alternate[4] = "share/sounds/rotc/ricochet2-1.wav";
//	alternate[5] = "share/sounds/rotc/ricochet2-1.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(Blaster2ProjectileMissedEnemySound)
{
	filename = "share/sounds/rotc/flyby1.wav";
	description = AudioClose3D;
	preload = true;
};


