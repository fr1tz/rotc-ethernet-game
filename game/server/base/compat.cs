//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// compat.cs
// stuff to enable compatibility with older versions
//------------------------------------------------------------------------------

// Note: Some missing files are transmitted automagically while others need some
//       hacky magic in order to be transmitted to clients that don't have them.

datablock AudioDescription(AudioCompat)
{
	volume	= 1.0;
	isLooping= false;

	is3D	  = false;
	ReferenceDistance= 5.0;
	MaxDistance= 30.0;
	type = $SimAudioType;
	
	isStreaming = true;
};

// *** for graphics files ***
//datablock ParticleData(TextureDummy_Nr)
//{
//	textureName	= "path";
//};

// *** for sound files ***
//datablock AudioProfile(AudioDummy_Nr)
//{
//	filename = "path";
//	description = AudioCompat;
//	preload = true;
//};

// *** for a missing ShapeBaseImageData ***
//datablock ShapeBaseImageData(ImageDummy_Nr)
//{
//	shapeFile = "path";	
//	stateName[0] = "DoNothing";
//};

//------------------------------------------------------------------------------

datablock ParticleData(TextureDummy_1)
{
	textureName	= "share/textures/eth/precipitation1";
};

datablock ParticleData(TextureDummy_2)
{
	textureName	= "share/textures/rotc/screen.damage";
};

datablock ParticleData(TextureDummy_3)
{
	textureName	= "share/textures/rotc/bounce.blue.hit.png";
};

datablock ParticleData(TextureDummy_4)
{
	textureName	= "share/textures/rotc/bounce.blue.miss.png";
};

datablock ParticleData(TextureDummy_5)
{
	textureName	= "share/textures/rotc/bounce.red.hit.png";
};

datablock ParticleData(TextureDummy_6)
{
	textureName	= "share/textures/rotc/bounce.red.miss.png";
};

datablock ParticleData(TextureDummy_7)
{
	textureName	= "share/textures/rotc/laser1.blue.png";
};

datablock ParticleData(TextureDummy_8)
{
	textureName	= "share/textures/rotc/laser1.cyan.png";
};

datablock ParticleData(TextureDummy_9)
{
	textureName	= "share/textures/rotc/laser1.green.png";
};

datablock ParticleData(TextureDummy_10)
{
	textureName	= "share/textures/rotc/laser1.orange.png";
};

datablock ParticleData(TextureDummy_11)
{
	textureName	= "share/textures/rotc/laser1.red.png";
};

datablock ParticleData(TextureDummy_12)
{
	textureName	= "share/textures/rotc/laser1.violet.png";
};

datablock ParticleData(TextureDummy_13)
{
	textureName	= "share/textures/rotc/laser1.white.png";
};

datablock ParticleData(TextureDummy_14)
{
	textureName	= "share/textures/rotc/lasertrail2.blue.png";
};

datablock ParticleData(TextureDummy_15)
{
	textureName	= "share/textures/rotc/lasertrail2.red.png";
};

datablock ParticleData(TextureDummy_16)
{
	textureName	= "share/textures/rotc/smoke3.green.png";
};

datablock ParticleData(TextureDummy_17)
{
	textureName	= "share/textures/rotc/smoke3.orange.png";
};

datablock ParticleData(TextureDummy_18)
{
	textureName	= "share/textures/rotc/smoke3.png";
};

datablock ParticleData(TextureDummy_19)
{
	textureName	= "share/textures/rotc/smoke4.blue.png";
};

datablock ParticleData(TextureDummy_20)
{
	textureName	= "share/textures/rotc/smoke4.red.png";
};

datablock ParticleData(TextureDummy_21)
{
	textureName	= "share/hud/rotc/ch2.png";
};
