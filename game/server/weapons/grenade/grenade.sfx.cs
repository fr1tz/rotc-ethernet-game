//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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
   filename = "share/shapes/rotc/weapons/disc/sound.throw.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeBounceSound)
{
   filename = "share/shapes/rotc/weapons/assaultrifle/sound.bounce.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeExplodeSound)
{
   filename = "share/shapes/rotc/weapons/disc/sound.explosion.wav";
   description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(GrenadeProjectileSound)
{
	filename = "share/shapes/rotc/weapons/disc/sound.projectile.wav";
	description = AudioCloseLooping3d;
	preload = true;
};

datablock AudioProfile(GrenadeChargeSound)
{
	filename = "share/shapes/rotc/weapons/disc/sound.charge.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(GrenadeTargetSound)
{
	filename = "share/shapes/rotc/weapons/disc/sound.target.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(GrenadeTargetAquiredSound)
{
	filename = "share/shapes/rotc/weapons/disc/sound.targetaquired.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(GrenadeThrowSound)
{
	filename = "share/shapes/rotc/weapons/disc/sound.throw.wav";
	description = AudioClose3d;
	preload = true;
};

datablock AudioProfile(GrenadeDeflectedSound)
{
	filename = "share/shapes/rotc/weapons/disc/sound.deflected.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(GrenadeHitSound)
{
	filename = "share/shapes/rotc/weapons/disc/sound.hit.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock AudioProfile(GrenadeExplosionSound)
{
	filename = "share/shapes/rotc/weapons/disc/sound.vanish.wav";
	description = AudioDefault3d;
	preload = true;
};



