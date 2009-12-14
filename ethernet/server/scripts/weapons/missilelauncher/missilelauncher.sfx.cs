//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - missilelauncher.sfx.cs
// Sounds for the missile launcher
//------------------------------------------------------------------------------

datablock AudioProfile(MissileLauncherChargeSound)
{
	filename = "~/data/weapons/missilelauncher/sound.charge.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherChargeAbortedSound)
{
	filename = "~/data/weapons/missilelauncher/sound.noammo.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherFireSound)
{
	filename = "~/data/weapons/missilelauncher/sound.fire.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherExplosionSound)
{
	filename = "~/data/weapons/missilelauncher/sound.explosion.wav";
	description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherDebrisSound)
{
	filename = "~/data/weapons/missilelauncher/sound.debris.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherNearEnemyExplosionSound)
{
	filename = "~/data/weapons/missilelauncher/sound.nearenemyexp.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherProjectileImpactSound)
{
	filename = "~/data/weapons/blaster/sound_impact.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherProjectileMissedEnemySound)
{
	filename = "~/data/weapons/blaster/sound_flyby.wav";
	description = AudioClose3D;
	preload = true;
};


