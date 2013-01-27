//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - missilelauncher.sfx.cs
// Sounds for the missile launcher
//------------------------------------------------------------------------------

datablock AudioProfile(MissileLauncherChargeSound)
{
	filename = "share/shapes/rotc/weapons/missilelauncher/sound.charge.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherChargeAbortedSound)
{
	filename = "share/shapes/rotc/weapons/missilelauncher/sound.noammo.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherFireSound)
{
	filename = "share/shapes/rotc/weapons/missilelauncher/sound.fire.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherExplosionSound)
{
	filename = "share/shapes/rotc/weapons/missilelauncher/sound.explosion.wav";
	description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherDebrisSound)
{
	filename = "share/shapes/rotc/weapons/missilelauncher/sound.debris.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherNearEnemyExplosionSound)
{
	filename = "share/shapes/rotc/weapons/missilelauncher/sound.nearenemyexp.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherProjectileImpactSound)
{
	filename = "share/shapes/rotc/weapons/blaster/sound_impact.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherProjectileMissedEnemySound)
{
	filename = "share/shapes/rotc/weapons/blaster/sound_flyby.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(MissileLauncherMissileSound)
{
	filename = "share/shapes/rotc/weapons/missilelauncher/sound.missile.wav";
	description = AudioDefaultLooping3d;
	preload = true;
};



