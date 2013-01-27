//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------


datablock AudioProfile(ScoutDroneEngineSound)
{
   filename = "share/sound/rotc/slide2";
   description = AudioDefaultLooping3D;
	preload = true;
};

datablock AudioProfile(ScoutDroneExplosionSound)
{
   filename = "~/data/vehicles/ScoutDrone/sound_explode.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(ScoutDroneSoftImpactSound)
{
   filename = "~/data/vehicles/tank/sound_softimpact.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(ScoutDroneHardImpactSound)
{
   filename = "~/data/vehicles/tank/sound_hardimpact.wav";
   description = AudioDefault3D;
	preload = true;
};
