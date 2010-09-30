//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
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
	filename = "~/data/sfx/spin1.wav";
	description = AudioCloseLooping3d;
	preload = true;
};

datablock AudioProfile(DiscTargetSound)
{
	filename = "~/data/sfx/target1.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(DiscTargetAquiredSound)
{
	filename = "~/data/sfx/target2.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(DiscThrowSound)
{
	filename = "~/data/sfx/throw1.wav";
	description = AudioClose3d;
	preload = true;
};

datablock AudioProfile(DiscDeflectedSound)
{
	filename = "~/data/sfx/impact2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(DiscHitSound)
{
	filename = "~/data/sfx/slice1.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock AudioProfile(DiscExplosionSound)
{
	filename = "~/data/sfx/explosion6.wav";
	description = AudioDefault3d;
	preload = true;
};

//------------------------------------------------------------------------------
// 2D Sounds
//------------------------------------------------------------------------------

datablock AudioProfile(DiscSeekerDeniedSound)
{
	filename = "~/data/sfx/denied1.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(DiscIncomingSound)
{
	filename = "~/data/sfx/alert1.wav";
	description = AudioCritical2D;
	preload = true;
};

