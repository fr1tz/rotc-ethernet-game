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
   filename = "share/sounds/rotc/throw1.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeBounceSound)
{
   filename = "share/sounds/rotc/bounce1.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeExplodeSound)
{
   filename = "share/sounds/rotc/explosion7.wav";
   description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(GrenadeProjectileSound)
{
	filename = "share/sounds/rotc/missile1.wav";
	description = AudioDefaultLooping3d;
	preload = true;
};

datablock AudioProfile(GrenadeChargeSound)
{
	filename = "share/sounds/rotc/charge3.wav";
	description = AudioCloseLooping3D;
	preload = true;
};