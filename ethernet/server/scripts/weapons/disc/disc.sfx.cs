//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - disc.sfx.cs
// Sound effects for the disc
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// 3D Sounds
//------------------------------------------------------------------------------

datablock AudioProfile(DiscProjectileSound)
{
	filename = "~/data/weapons/disc/sound.projectile.wav";
	description = AudioCloseLooping3d;
	preload = true;
};

datablock AudioProfile(DiscChargeSound)
{
	filename = "~/data/weapons/disc/sound.charge.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(DiscTargetSound)
{
	filename = "~/data/weapons/disc/sound.target.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(DiscTargetAquiredSound)
{
	filename = "~/data/weapons/disc/sound.targetaquired.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(DiscThrowSound)
{
	filename = "~/data/weapons/disc/sound.throw.wav";
	description = AudioClose3d;
	preload = true;
};

datablock AudioProfile(DiscDeflectedSound)
{
	filename = "~/data/weapons/disc/sound.deflected.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(DiscHitSound)
{
	filename = "~/data/weapons/disc/sound.hit.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock AudioProfile(DiscExplosionSound)
{
	filename = "~/data/weapons/disc/sound.vanish.wav";
	description = AudioDefault3d;
	preload = true;
};

//------------------------------------------------------------------------------
// 2D Sounds
//------------------------------------------------------------------------------

datablock AudioProfile(DiscSeekerDeniedSound)
{
	filename = "~/data/weapons/disc/sound.denied.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(DiscIncomingSound)
{
	filename = "~/data/weapons/disc/sound.incoming.wav";
	description = AudioCritical2D;
	preload = true;
};

