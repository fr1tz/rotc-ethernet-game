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

datablock ParticleData(FileDummy_share_shapes_rotc_effects_explosion5green)
{
	textureName	= "share/shapes/rotc/effects/explosion5.green";
};

datablock ParticleData(FileDummy_share_shapes_rotc_effects_explosion5orange)
{
	textureName	= "share/shapes/rotc/effects/explosion5.orange";
};

datablock ParticleData(FileDummy_share_shapes_rotc_weapons_blaster_projectileimpactgreen)
{
	textureName	= "share/shapes/rotc/weapons/blaster/projectile.impact.green";
};

datablock ParticleData(FileDummy_share_shapes_rotc_weapons_blaster_projectileimpactorange)
{
	textureName	= "share/shapes/rotc/weapons/blaster/projectile.impact.orange";
};

datablock ShapeBaseImageData(FileDummy_share_shapes_rotc_weapons_assaultrifle_image3red)
{
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image3.red.dts";	
	stateName[0] = "DoNothing";
};

datablock ShapeBaseImageData(FileDummy_share_shapes_rotc_weapons_assaultrifle_image3blue)
{
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image3.blue.dts";	
	stateName[0] = "DoNothing";
};

datablock ParticleData(FileDummy_share_shapes_rotc_weapons_assaultrifle_projectilered)
{
	textureName	= "share/shapes/rotc/weapons/assaultrifle/projectile.red";
};

datablock ParticleData(FileDummy_share_shapes_rotc_weapons_assaultrifle_projectileblue)
{
	textureName	= "share/shapes/rotc/weapons/assaultrifle/projectile.blue";
};

datablock ParticleData(FileDummy_share_shapes_rotc_weapons_assaultrifle_lasertailred)
{
	textureName	= "share/shapes/rotc/weapons/assaultrifle/lasertail.red";
};

datablock ParticleData(FileDummy_share_shapes_rotc_weapons_assaultrifle_lasertailblue)
{
	textureName	= "share/shapes/rotc/weapons/assaultrifle/lasertail.blue";
};

datablock ShapeBaseImageData(FileDummy_share_shapes_rotc_weapons_missilelauncher_image2green)
{
	shapeFile = "share/shapes/rotc/weapons/missilelauncher/image2.green.dts";	
	stateName[0] = "DoNothing";
};

datablock ShapeBaseImageData(FileDummy_share_shapes_rotc_weapons_missilelauncher_image2orange)
{
	shapeFile = "share/shapes/rotc/weapons/missilelauncher/image2.orange.dts";	
	stateName[0] = "DoNothing";
};

datablock ParticleData(FileDummy_share_shapes_rotc_weapons_missilelauncher_green)
{
	textureName	= "share/shapes/rotc/weapons/missilelauncher/green";
};

datablock ParticleData(FileDummy_share_shapes_rotc_weapons_missilelauncher_orange)
{
	textureName	= "share/shapes/rotc/weapons/missilelauncher/orange";
};

datablock ParticleData(FileDummy_share_shapes_rotc_weapons_sniperrifle_lasertrail2green)
{
	textureName	= "share/shapes/rotc/weapons/sniperrifle/lasertrail2.green";
};

datablock ParticleData(FileDummy_share_shapes_rotc_weapons_sniperrifle_lasertrail3green)
{
	textureName	= "share/shapes/rotc/weapons/sniperrifle/lasertrail3.green";
};

datablock ParticleData(FileDummy_share_textures_rotc_barriergreen)
{
	textureName	= "share/textures/rotc/barrier.green";
};

datablock ParticleData(FileDummy_share_textures_rotc_barrierorange)
{
	textureName	= "share/textures/rotc/barrier.orange";
};