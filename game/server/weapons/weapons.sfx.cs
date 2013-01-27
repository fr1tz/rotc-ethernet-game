//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - weapons.sfx.cs
// Sounds for all weapons
//------------------------------------------------------------------------------

datablock AudioProfile(WeaponSwitchSound)
{
	filename = "share/sounds/rotc/weaponSwitch.wav";
	description = AudioClosest3D;
	preload = true;
};

datablock AudioProfile(WeaponEmptySound)
{
	filename = "share/sounds/rotc/weaponEmpty.wav";
	description = AudioClosest3D;
	preload = true;
};

datablock AudioProfile(DefaultProjectileNearEnemyExplosionSound)
{
	filename = "share/shapes/rotc/weapons/sniperrifle/sound.nearenemyexp.wav";
	description = AudioClose3D;
	preload = true;
};

