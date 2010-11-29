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
		if(%obj.getMountedImage(0).crosshair $= "blaster") // hack hack ;)
			%nr = 2;
		else
			%nr = 1;
	}

	if(%nr == -2)
	{
		%obj.mountImage(%obj.prevWeaponImage, 0, -1, true);
	}
	else if(%nr == -1)
	{
		%obj.prevWeaponImage = %obj.getMountedImage(0);

		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedSniperRifleImage, 0, -1, true);
		else
			%obj.mountImage(BlueSniperRifleImage, 0, -1, true);
	}
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
	else if(false)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedGrenadeLauncherImage, 0, -1, true);
		else
			%obj.mountImage(BlueGrenadeLauncherImage, 0, -1, true);
	}
	else if(false)
	{
		// special weapon!
		if(%obj.specialWeapon == $SpecialWeapon::AssaultRifle)
		{
			if(%obj.getTeamId() == 1)
				%obj.mountImage(RedAssaultRifleImage, 0, -1, true);
			else
				%obj.mountImage(BlueAssaultRifleImage, 0, -1, true);
    
            %validWeapon = true;
		}
		else if(%obj.specialWeapon == $SpecialWeapon::SniperRifle)
		{
			if(%obj.getTeamId() == 1)
				%obj.mountImage(RedSniperRifleImage, 0, -1, true);
			else
				%obj.mountImage(BlueSniperRifleImage, 0, -1, true);
    
            %validWeapon = true;
		}
		else if(%obj.specialWeapon == $SpecialWeapon::MissileLauncher)
		{
			if(%obj.getTeamId() == 1)
				%obj.mountImage(RedMissileLauncherImage, 0, -1, true);
			else
				%obj.mountImage(BlueMissileLauncherImage, 0, -1, true);
    
            %validWeapon = true;
		}
		else if(%obj.specialWeapon == $SpecialWeapon::Chaingun)
		{
			if(%obj.getTeamId() == 1)
				%obj.mountImage(RedChaingunImage, 0, -1, true);
			else
				%obj.mountImage(BlueChaingunImage, 0, -1, true);
    
            %validWeapon = true;
		}
		else if(%obj.specialWeapon == $SpecialWeapon::GrenadeLauncher)
		{
			if(%obj.getTeamId() == 1)
				%obj.mountImage(RedGrenadeLauncherImage, 0, -1, true);
			else
				%obj.mountImage(BlueGrenadeLauncherImage, 0, -1, true);
    
            %validWeapon = true;
		}
		else if(%obj.specialWeapon == $SpecialWeapon::MachineGun)
		{
			if(%obj.getTeamId() == 1)
				%obj.mountImage(RedMachineGunImage, 0, -1, true);
			else
				%obj.mountImage(BlueMachineGunImage, 0, -1, true);
		
			%obj.setImageAmmo(0, true);
    
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
}

