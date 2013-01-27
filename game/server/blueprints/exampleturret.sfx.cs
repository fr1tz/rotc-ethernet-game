//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------


datablock AudioProfile(ExampleTurretFireSound)
{
   filename = "~/data/vehicles/tank/sound_fire.wav";
   description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(ExampleTurretProjectileExplosionSound)
{
   filename = "~/data/vehicles/tank/sound_grenadeimpact.wav";
   description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(ExampleTurretMoveSound)
{
   filename = "~/data/vehicles/tank/sound_turretmove.wav";
   description = AudioDefaultLooping3D;
	preload = true;
};
