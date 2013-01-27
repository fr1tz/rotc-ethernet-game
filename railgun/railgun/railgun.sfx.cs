//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - railgun.sfx.cs
// Sound effects for the railgun
//------------------------------------------------------------------------------

datablock AudioProfile(RailgunFireSound)
{
	filename = "ethernet/data/weapons/sniperrifle/sound_fire.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(RailgunProjectileExplosionSound)
{
	filename = "ethernet/data/weapons/disc/sound.explosion.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(RailgunProjectileMissedEnemySound)
{
	filename = "ethernet/data/weapons/blaster/sound_flyby.wav";
	description = AudioClose3D;
	preload = true;
};
