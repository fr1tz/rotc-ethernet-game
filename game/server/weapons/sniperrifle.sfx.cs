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
	filename = "share/sounds/rotc/fire6.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(SniperExplosionSound)
{
	filename = "share/sounds/rotc/explosion5.wav";
	description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(SniperDebrisSound)
{
	filename = "share/sounds/rotc/debris1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(SniperPowerUpSound)
{
	filename = "share/sounds/rotc/charge2.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(SniperProjectileImpactSound)
{
	filename = "share/sounds/rotc/explosion5.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(SniperProjectileMissedEnemySound)
{
	filename = "share/sounds/rotc/flyby1.wav";
	description = AudioClose3D;
	preload = true;
};


