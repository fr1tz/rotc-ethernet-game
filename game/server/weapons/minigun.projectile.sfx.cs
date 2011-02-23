//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

datablock AudioProfile(MinigunProjectileImpactSound)
{
	filename = "share/sounds/rotc/impact1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MinigunProjectileFlybySound)
{
	filename = "share/sounds/rotc/flyby1.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(MinigunProjectileMissedEnemySound)
{
	filename = "share/sounds/rotc/flyby1.wav";
	description = AudioClose3D;
	preload = true;
};
