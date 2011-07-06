//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - disc.sfx.cs
// Sound effects for the discs
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// 3D Sounds
//------------------------------------------------------------------------------

datablock AudioProfile(DiscProjectileSound)
{
	filename = "share/sounds/rotc/spin1.wav";
	description = AudioCloseLooping3d;
	preload = true;
};

datablock AudioProfile(ExplosiveDiscProjectileSound)
{
	filename = "share/sounds/rotc/missile1.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(DiscTargetSound)
{
	filename = "share/sounds/rotc/target1.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(DiscTargetAquiredSound)
{
	filename = "share/sounds/rotc/target2.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(DiscThrowSound)
{
	filename = "share/sounds/rotc/throw1.wav";
	description = AudioClose3d;
	preload = true;
};

datablock AudioProfile(DiscDeflectedSound)
{
	filename = "share/sounds/rotc/impact2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(DiscHitSound)
{
	filename = "share/sounds/rotc/slice1.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock AudioProfile(ExplosiveDiscHitSound)
{
	filename = "share/sounds/rotc/bounce1.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock AudioProfile(DiscExplosionSound)
{
	filename = "share/sounds/rotc/explosion6.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock AudioProfile(ExplosiveDiscExplosionSound)
{
	filename = "share/sounds/rotc/explosion1.wav";
	description = AudioDefault3d;
	preload = true;
};

//------------------------------------------------------------------------------
// 2D Sounds
//------------------------------------------------------------------------------

datablock AudioProfile(DiscSeekerDeniedSound)
{
	filename = "share/sounds/rotc/denied1.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(DiscIncomingSound)
{
	filename = "share/sounds/rotc/alert1.wav";
	description = AudioCritical2D;
	preload = true;
};

