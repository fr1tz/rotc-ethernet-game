//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - blaster.projectile.sfx.cs
// Sound effects for the blaster projectile
//------------------------------------------------------------------------------

datablock AudioProfile(BlasterProjectileImpactSound)
{
	filename = "~/data/sfx/impact1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(BlasterProjectileFlybySound)
{
	filename = "~/data/sfx/flyby1.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(BlasterProjectileMissedEnemySound)
{
	filename = "~/data/sfx/flyby1.wav";
	description = AudioClose3D;
	preload = true;
};
