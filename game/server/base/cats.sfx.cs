//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - cats.sfx.cs
// Sounds for all CATs
//------------------------------------------------------------------------------

datablock AudioProfile(CatSpawnSound)
{
	filename	 = "share/sounds/rotc/deploy1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(PlayerSlideSound)
{
	filename	 = "share/sounds/rotc/slide2.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(PlayerSlideContactSound)
{
	filename	 = "share/sounds/rotc/slide1.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(PlayerSkidSound)
{
	filename	 = "share/shapes/rotc/players/standardcat/slidecontact.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(CatJumpExplosionSound)
{
	filename = "share/sounds/rotc/explosion3.wav";
	description = AudioFar3D;
	preload = true;
};

//datablock AudioProfile(ExitingWaterLightSound)
//{
//	filename	 = "share/sounds/rotc/replaceme.wav";
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
//	filename	 = "share/sounds/rotc/replaceme.wav";
//	description = AudioCloseLooping3d;
//	preload = true;
//};

//datablock AudioProfile(WaterBreathMaleSound)
//{
//	filename	 = "share/sounds/rotc/replaceme.wav";
//	description = AudioClosestLooping3d;
//	preload = true;
//};
