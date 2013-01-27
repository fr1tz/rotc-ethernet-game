//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock AudioProfile(GrenadeLauncherFireSound)
{
	filename = "share/sounds/rotc/fire3.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeLauncherProjectileBounceSound)
{
	filename = "share/sounds/rotc/bounce1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeLauncherProjectileExplosionSound)
{
	filename = "share/sounds/rotc/explosion1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeLauncherProjectileSound)
{
	filename = "share/sounds/rotc/missile1.wav";
	description = AudioCloseLooping3d;
	preload = true;
};
