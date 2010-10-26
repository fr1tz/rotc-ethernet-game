//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

datablock AudioProfile(MachineGunFireSound)
{
	filename = "~/data/weapons/machinegun/sound.fire.wav";
	description = AudioDefaultLooping3D;
	preload = true;
};

datablock AudioProfile(MachineGunProjectileBounceSound)
{
	filename = "~/data/weapons/assaultrifle/sound.bounce.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MachineGunProjectileExplosionSound)
{
	filename = "~/data/weapons/assaultrifle/sound.explosion2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MachineGunProjectileImpactSound)
{
	filename = "~/data/weapons/blaster/sound_impact.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MachineGunProjectileMissedEnemySound)
{
	filename = "~/data/weapons/blaster/sound_flyby.wav";
	description = AudioClose3D;
	preload = true;
};
