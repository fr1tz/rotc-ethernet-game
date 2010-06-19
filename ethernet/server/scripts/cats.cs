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
 
    %validWeapon = false;
 
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
   
        %validWeapon = true;
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
    
            %validWeapon = true;
		}
		else if(%obj.mainWeapon == $MainWeapon::SniperRifle)
		{
			if(%obj.getTeamId() == 1)
				%obj.mountImage(RedSniperRifleImage, 0, -1, true);
			else
				%obj.mountImage(BlueSniperRifleImage, 0, -1, true);
    
            %validWeapon = true;
		}
		else if(false) //%obj.mainWeapon == $MainWeapon::MissileLauncher)
		{
			if(%obj.getTeamId() == 1)
				%obj.mountImage(RedMissileLauncherImage, 0, -1, true);
			else
				%obj.mountImage(BlueMissileLauncherImage, 0, -1, true);
    
            %validWeapon = true;
		}
	}
	else if(false) //%nr == 4)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedStilettoImage, 0, -1, true);
		else
			%obj.mountImage(BlueStilettoImage, 0, -1, true);
	}
 
    if(%validWeapon && isObject(%obj.client))
        %obj.client.lastCATWeapon = %nr;
}

