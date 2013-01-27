//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock AudioProfile(MachineGunFireSound)
{
	filename = "share/shapes/rotc/weapons/machinegun/sound.fire.wav";
	description = AudioDefaultLooping3D;
	preload = true;
};

datablock AudioProfile(MachineGunProjectileBounceSound)
{
	filename = "share/shapes/rotc/weapons/assaultrifle/sound.bounce.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MachineGunProjectileExplosionSound)
{
	filename = "share/shapes/rotc/weapons/assaultrifle/sound.explosion2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MachineGunProjectileImpactSound)
{
	filename = "share/shapes/rotc/weapons/blaster/sound_impact.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MachineGunProjectileMissedEnemySound)
{
	filename = "share/shapes/rotc/weapons/blaster/sound_flyby.wav";
	description = AudioClose3D;
	preload = true;
};
