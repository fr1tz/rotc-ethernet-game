//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - weapons.sfx.cs
// Sounds for all weapons
//------------------------------------------------------------------------------

datablock AudioProfile(WeaponSwitchSound)
{
	filename = "~/data/sound/weaponSwitch.wav";
	description = AudioClosest3D;
	preload = true;
};

datablock AudioProfile(WeaponEmptySound)
{
	filename = "~/data/sound/weaponEmpty.wav";
	description = AudioClosest3D;
	preload = true;
};

datablock AudioProfile(DefaultProjectileNearEnemyExplosionSound)
{
	filename = "~/data/weapons/sniperrifle/sound.nearenemyexp.wav";
	description = AudioClose3D;
	preload = true;
};

