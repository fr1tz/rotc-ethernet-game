//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - cats.cs
// Code for all CATs
//------------------------------------------------------------------------------

function executeCatScripts()
{
	echo(" ----- executing cat scripts ----- ");
	
	exec("./cats.sfx.cs");
	exec("./cats.gfx.cs");

	// partytime!
	exec("./cats/partysounds.cs");
	exec("./cats/partyanims.cs");

	// CATs...
	exec("./cats/standard/standard.cs");
}

executeCatScripts();

//------------------------------------------------------------------------------

function StandardCat::useWeapon(%this, %obj, %nr)
{
	%client = %obj.client;

	// discs...
	if(%nr == 6)
	{
		launchExplosiveDisc(%obj);
		return;
	}
	else if(%nr == 7)
	{
		launchRepelDisc(%obj);
		return;
	}
	
	if(%client.numWeapons == 0)
		return;

	if(%nr == 0)
		%obj.currWeapon++;
	else
		%obj.currWeapon = %nr;
	
	if(%obj.currWeapon > %client.numWeapons)
		%obj.currWeapon = 1;	

	%nr = %client.weapons[%obj.currWeapon-1];

	if(%nr == 1)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedBlasterImage, 0, -1, true);
		else
			%obj.mountImage(BlueBlasterImage, 0, -1, true);
	}
	else if(%nr == 2)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedAssaultRifleImage, 0, -1, true);
		else
			%obj.mountImage(BlueAssaultRifleImage, 0, -1, true);
	}
	else if(%nr == 3)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedSniperRifleImage, 0, -1, true);
		else
			%obj.mountImage(BlueSniperRifleImage, 0, -1, true);
	}
	else if(%nr == 4)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedMinigunImage, 0, -1, true);
		else
			%obj.mountImage(BlueMinigunImage, 0, -1, true);
	}
	else if(%nr == 5)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedRepelGunImage, 0, -1, true);
		else
			%obj.mountImage(BlueRepelGunImage, 0, -1, true);
	}
}

