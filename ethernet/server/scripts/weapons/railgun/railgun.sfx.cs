//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - railgun.sfx.cs
// Sound effects for the railgun
//------------------------------------------------------------------------------

datablock AudioProfile(RailgunFireSound)
{
	filename = "~/data/weapons/sniperrifle/sound_fire.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(RailgunProjectileExplosionSound)
{
	filename = "~/data/weapons/disc/sound.explosion.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(RailgunProjectileMissedEnemySound)
{
	filename = "~/data/weapons/blaster/sound_flyby.wav";
	description = AudioClose3D;
	preload = true;
};
