//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - sniperrifle.sfx.cs
// Sounds for the sniper rifle
//------------------------------------------------------------------------------

datablock AudioProfile(SniperRifleFireSound)
{
	filename = "~/data/sfx/explosion3.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(SniperExplosionSound)
{
	filename = "~/data/sfx/explosion5.wav";
	description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(SniperDebrisSound)
{
	filename = "~/data/sfx/debris1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(SniperPowerUpSound)
{
	filename = "~/data/sfx/charge2.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(SniperProjectileImpactSound)
{
	filename = "~/data/sfx/explosion5.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(SniperProjectileMissedEnemySound)
{
	filename = "~/data/sfx/flyby1.wav";
	description = AudioClose3D;
	preload = true;
};


