//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - sniperrifle.sfx.cs
// Sounds for the sniper rifle
//------------------------------------------------------------------------------

datablock AudioProfile(SniperRifleTargetSound)
{
	filename = "~/data/weapons/sniperrifle/sound.target.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(SniperRifleTargetAquiredSound)
{
	filename = "~/data/weapons/sniperrifle/sound.targetaquired.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(SniperRifleFireSound)
{
	filename = "~/data/weapons/sniperrifle/sound_fire.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(SniperExplosionSound)
{
	filename = "~/data/weapons/sniperrifle/sound.explosion.wav";
	description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(SniperDebrisSound)
{
	filename = "~/data/weapons/sniperrifle/sound.debris.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(SniperNearEnemyExplosionSound)
{
	filename = "~/data/weapons/sniperrifle/sound.nearenemyexp.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(SniperPowerUpSound)
{
	filename = "~/data/weapons/sniperrifle/sound.charge.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(SniperPowerDownSound)
{
	filename = "~/data/weapons/sniperrifle/sound.noammo.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(SniperProjectileImpactSound)
{
	filename = "~/data/weapons/blaster/sound_impact.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(SniperProjectileMissedEnemySound)
{
	filename = "~/data/weapons/blaster/sound_flyby.wav";
	description = AudioClose3D;
	preload = true;
};


