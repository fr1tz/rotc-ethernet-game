//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
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
 
    if($Pref::Server::InstaGibMode)
    {
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedRailgunImage, 0, -1, true);
		else
			%obj.mountImage(BlueRailgunImage, 0, -1, true);
   
        return;
    }
	
	if(%nr == 0)
	{
		if(%obj.getMountedImage(0).mainWeapon)
			%nr = 1;
		else
			%nr = 2;
	}
	
	if(%nr == 1)
	{
		// blaster!
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedBlasterImage, 0, -1, true);
		else
			%obj.mountImage(BlueBlasterImage, 0, -1, true);
	}
	else if(%nr == 2)
	{
		// main weapon!
		if(%obj.mainWeapon == $MainWeapon::AssaultRifle)
		{
			if(%obj.getTeamId() == 1)
				%obj.mountImage(RedAssaultRifleImage, 0, -1, true);
			else
				%obj.mountImage(BlueAssaultRifleImage, 0, -1, true);
		}
		else
		{
			if(%obj.getTeamId() == 1)
				%obj.mountImage(RedSniperRifleImage, 0, -1, true);
			else
				%obj.mountImage(BlueSniperRifleImage, 0, -1, true);
		}
	}
	else if(%nr == 4)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedStilettoImage, 0, -1, true);
		else
			%obj.mountImage(BlueStilettoImage, 0, -1, true);
	}
 
    if(isObject(%obj.client))
        %obj.client.lastCATWeapon = %nr;
}

