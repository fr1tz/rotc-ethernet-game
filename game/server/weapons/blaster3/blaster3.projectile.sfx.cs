//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock AudioProfile(Blaster3ProjectileImpactSound)
{
	filename = "share/sounds/rotc/impact1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(Blaster3ProjectileFlybySound)
{
	filename = "share/sounds/rotc/flyby1.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(Blaster3ProjectileMissedEnemySound)
{
	filename = "share/sounds/rotc/flyby1.wav";
	description = AudioClose3D;
	preload = true;
};
