//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - cats.sfx.cs
// Sounds for all CATs
//------------------------------------------------------------------------------

datablock AudioProfile(CatSpawnSound)
{
	filename	 = "~/data/players/shared/sound.spawn.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(PlayerSlideSound)
{
	filename	 = "~/data/players/standardcat/slide.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(PlayerSlideContactSound)
{
	filename	 = "~/data/players/standardcat/slidecontact.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(CatJumpExplosionSound)
{
	filename = "~/data/weapons/sniperrifle/sound.explosion.wav";
	description = AudioFar3D;
	preload = true;
};

//datablock AudioProfile(ExitingWaterLightSound)
//{
//	filename	 = "~/data/sound/replaceme.wav";
//	description = AudioClose3d;
//	preload = true;
//};

//datablock AudioProfile(DeathCrySound)
//{
//	fileName = "";
//	description = AudioClose3d;
//	preload = true;
//};

//datablock AudioProfile(PainCrySound)
//{
//	fileName = "";
//	description = AudioClose3d;
//	preload = true;
//};

//datablock AudioProfile(PlayerSharedMoveBubblesSound)
//{
//	filename	 = "~/data/sound/replaceme.wav";
//	description = AudioCloseLooping3d;
//	preload = true;
//};

//datablock AudioProfile(WaterBreathMaleSound)
//{
//	filename	 = "~/data/sound/replaceme.wav";
//	description = AudioClosestLooping3d;
//	preload = true;
//};
