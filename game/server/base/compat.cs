//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// compat.cs
// stuff to enable compatibility with older versions
//------------------------------------------------------------------------------

datablock ShapeBaseImageData(ImageDummy_AssaultRifleImage_Orange)
{
	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image2.orange.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 2;
	offset = "0 0 0";
	
	stateName[0] = "DoNothing";
};

datablock ShapeBaseImageData(ImageDummy_AssaultRifleImage_Cyan)
{
	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image2.cyan.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 2;
	offset = "0 0 0";
	
	stateName[0] = "DoNothing";
};

datablock ParticleData(TextureFileDummy_share_shapes_rotc_weapons_assaultrifle_lasertail_orange)
{
	textureName = "share/shapes/rotc/weapons/assaultrifle/lasertail.orange";
};

datablock ParticleData(TextureFileDummy_share_shapes_rotc_weapons_assaultrifle_lasertail_cyan)
{
	textureName = "share/shapes/rotc/weapons/assaultrifle/lasertail.cyan";
};

datablock ParticleData(TextureFileDummy_share_shapes_rotc_weapons_assaultrifle_projectile_orange)
{
	textureName = "share/shapes/rotc/weapons/assaultrifle/projectile.orange";
};

datablock ParticleData(TextureFileDummy_share_shapes_rotc_weapons_assaultrifle_projectile_cyan)
{
	textureName = "share/shapes/rotc/weapons/assaultrifle/projectile.cyan";
};

datablock ParticleData(TextureFileDummy_share_shapes_rotc_weapons_disc_lasertrail2_red)
{
	textureName = "share/shapes/rotc/weapons/disc/lasertrail2.red";
};

datablock ParticleData(TextureFileDummy_share_shapes_rotc_weapons_disc_lasertrail2_blue)
{
	textureName = "share/shapes/rotc/weapons/disc/lasertrail2.blue";
};

datablock ParticleData(TextureFileDummy_share_shapes_rotc_weapons_sniperrifle_lasertrail2_orange)
{
	textureName = "share/shapes/rotc/weapons/sniperrifle/lasertrail2.orange";
};

datablock ParticleData(TextureFileDummy_share_shapes_rotc_weapons_sniperrifle_lasertrail2_cyan)
{
	textureName = "share/shapes/rotc/weapons/sniperrifle/lasertrail2.cyan";
};

datablock ParticleData(TextureFileDummy_share_shapes_rotc_weapons_sniperrifle_lasertrail3_orange)
{
	textureName = "share/shapes/rotc/weapons/sniperrifle/lasertrail3.orange";
};

datablock ParticleData(TextureFileDummy_share_shapes_rotc_weapons_sniperrifle_lasertrail3_cyan)
{
	textureName = "share/shapes/rotc/weapons/sniperrifle/lasertrail3.cyan";
};

datablock ParticleData(TextureFileDummy_share_textures_rotc_armor_white)
{
	textureName = "share/textures/rotc/armor.white";
};

datablock ParticleData(TextureFileDummy_share_textures_rotc_armor_orange)
{
	textureName = "share/textures/rotc/armor.orange";
};

datablock ParticleData(TextureFileDummy_share_textures_rotc_armor_cyan)
{
	textureName = "share/textures/rotc/armor.cyan";
};
