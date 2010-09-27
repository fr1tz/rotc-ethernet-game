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
   filename = "~/data/sfx/fire2.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeBounceSound)
{
   filename = "~/data/sfx/bounce1.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeExplodeSound)
{
   filename = "~/data/sfx/explosion5.wav";
   description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(GrenadeProjectileSound)
{
	filename = "~/data/sfx/missile1.wav";
	description = AudioCloseLooping3d;
	preload = true;
};

datablock AudioProfile(GrenadeChargeSound)
{
	filename = "~/data/sfx/charge3.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

