//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - sniperrifle.sfx.cs
// Sounds for the sniper rifle
//------------------------------------------------------------------------------

datablock AudioProfile(SniperRifleFireSound)
{
	filename = "share/sounds/rotc/explosion12.wav";
	description = AudioDefault3D;
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
	filename = "share/sounds/rotc/explosion2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(SniperProjectileMissedEnemySound)
{
	filename = "share/sounds/rotc/impact4.wav";
	description = AudioClose3D;
	preload = true;
};


