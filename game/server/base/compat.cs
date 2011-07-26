//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
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
//datablock ParticleData(FileDummy_Path)
//{
//	textureName	= "path";
//};

// *** for sound files ***
//datablock AudioProfile(FileDummy_Path)
//{
//	filename = "path";
//	description = AudioCompat;
//	preload = true;
//};

// *** for a missing ShapeBaseImageData ***
//datablock ShapeBaseImageData(FileDummy_Path)
//{
//	shapeFile = "path";	
//	stateName[0] = "DoNothing";
//};

//------------------------------------------------------------------------------

datablock ParticleData(TextureFileDummy1)
{
	textureName	= "share/textures/rotc/rainbow1.png";
};

datablock ParticleData(TextureFileDummy2)
{
	textureName	= "share/shapes/rotc/effects/explosion4_white.png";
};

datablock ParticleData(TextureFileDummy3)
{
	textureName	= "share/textures/rotc/zone.lane.png";
};

datablock ParticleData(TextureFileDummy4)
{
	textureName	= "share/hud/rotc/icon.anchor.20x20.png";
};

datablock ParticleData(TextureFileDummy5)
{
	textureName	= "share/hud/rotc/icon.explosivedisc.20x20.png";
};

datablock ParticleData(TextureFileDummy6)
{
	textureName	= "share/hud/rotc/icon.grenade.20x20.png";
};

datablock ParticleData(TextureFileDummy7)
{
	textureName	= "share/hud/rotc/icon.repeldisc.20x20.png";
};

datablock ParticleData(TextureFileDummy8)
{
	textureName	= "share/hud/rotc/icon.slasherdisc.20x20.png";
};

datablock ParticleData(TextureFileDummy9)
{
	textureName	= "share/hud/rotc/icon.stabilizer.20x20.png";
};

datablock ParticleData(TextureFileDummy10)
{
	textureName	= "share/hud/rotc/icon.permaboard.20x20.png";
};

datablock ParticleData(TextureFileDummy11)
{
	textureName	= "share/hud/rotc/icon.vamp.20x20.png";
};



