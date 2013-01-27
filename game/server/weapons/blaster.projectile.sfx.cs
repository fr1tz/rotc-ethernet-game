//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - blaster.projectile.sfx.cs
// Sound effects for the blaster projectile
//------------------------------------------------------------------------------

datablock AudioProfile(BlasterProjectileImpactSound)
{
	filename = "share/sounds/rotc/impact1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(BlasterProjectileFlybySound)
{
	filename = "share/sounds/rotc/flyby1.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(BlasterProjectileMissedEnemySound)
{
	filename = "share/sounds/rotc/flyby1.wav";
	description = AudioClose3D;
	preload = true;
};
