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

 
	if(%nr == 0)
	{
		if(%obj.currWeapon == 1)
			%nr = 2;
		else
			%nr = 1;
	}

	%obj.currWeapon = %nr;

	%nr = %client.weapon[%nr];

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
			%obj.mountImage(RedGrenadeLauncherImage, 0, -1, true);
		else
			%obj.mountImage(BlueGrenadeLauncherImage, 0, -1, true);
	}
}

