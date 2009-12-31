//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - grenade.sfx.cs
// Sound effects for the grenade
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// 3D Sounds
//------------------------------------------------------------------------------

datablock AudioProfile(GrenadeThrowSound)
{
   filename = "~/data/weapons/disc/sound.throw.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeBounceSound)
{
   filename = "~/data/weapons/assaultrifle/sound.bounce.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeExplodeSound)
{
   filename = "~/data/weapons/disc/sound.explosion.wav";
   description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(GrenadeProjectileSound)
{
	filename = "~/data/weapons/disc/sound.projectile.wav";
	description = AudioCloseLooping3d;
	preload = true;
};

datablock AudioProfile(GrenadeChargeSound)
{
	filename = "~/data/weapons/disc/sound.charge.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(GrenadeTargetSound)
{
	filename = "~/data/weapons/disc/sound.target.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(GrenadeTargetAquiredSound)
{
	filename = "~/data/weapons/disc/sound.targetaquired.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(GrenadeThrowSound)
{
	filename = "~/data/weapons/disc/sound.throw.wav";
	description = AudioClose3d;
	preload = true;
};

datablock AudioProfile(GrenadeDeflectedSound)
{
	filename = "~/data/weapons/disc/sound.deflected.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeHitSound)
{
	filename = "~/data/weapons/disc/sound.hit.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock AudioProfile(GrenadeExplosionSound)
{
	filename = "~/data/weapons/disc/sound.vanish.wav";
	description = AudioDefault3d;
	preload = true;
};



