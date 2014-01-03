//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - cats.cs
// Code for all CATs
//------------------------------------------------------------------------------

function executeCatScripts()
{
	echo(" ----- executing cat scripts ----- ");

	%i = 1;
	// Weapons...
	$CatEquipment::Blaster         = %i; %i++;
	$CatEquipment::BattleRifle     = %i; %i++;
	$CatEquipment::SniperRifle     = %i; %i++;
	$CatEquipment::MiniGun         = %i; %i++;
	$CatEquipment::RepelGun        = %i; %i++;
	$CatEquipment::GrenadeLauncher = %i; %i++;
	// Discs...
	$CatEquipment::SlasherDisc     = %i; %i++;
	$CatEquipment::RepelDisc       = %i; %i++;
	$CatEquipment::ExplosiveDisc   = %i; %i++;
	// Other...
	$CatEquipment::Damper          = %i; %i++;
	$CatEquipment::VAMP            = %i; %i++;
	$CatEquipment::Anchor          = %i; %i++;
	$CatEquipment::Stabilizer      = %i; %i++;
	$CatEquipment::Grenade         = %i; %i++;
	$CatEquipment::Bounce          = %i; %i++;
	$CatEquipment::Etherboard      = %i; %i++;
	$CatEquipment::Permaboard      = %i; %i++;
	$CatEquipment::Regeneration    = %i; %i++;
	
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

	if(%nr == 4)
	{
		dropGreen(%obj);
		return;
	}

   if(%nr == -17)
   {
		if(%client.hasBounce)
			deployRepel4(%obj);
      return;
   }

   
   // Switch weapon mode.
   if(%nr == 7)
   {
      %image = %obj.getMountedImage(0);
      // Blaster...
      if(%image.getId() == RedBlaster4Image.getId())
	      %obj.mountImage(RedBlaster5Image, 0, -1, true);
      else if(%image.getId() == BlueBlaster4Image.getId())
	      %obj.mountImage(BlueBlaster5Image, 0, -1, true);
      else if(%image.getId() == RedBlaster5Image.getId())
	      %obj.mountImage(RedBlaster4Image, 0, -1, true);
      else if(%image.getId() == BlueBlaster5Image.getId())
	      %obj.mountImage(BlueBlaster4Image, 0, -1, true);
      // Sniper Rifle...
      else if(%image.getId() == RedSniperRifleImage.getId())
	      %obj.mountImage(RedSniperRifle2Image, 0, -1, true);
      else if(%image.getId() == BlueSniperRifleImage.getId())
	      %obj.mountImage(BlueSniperRifle2Image, 0, -1, true);
      else if(%image.getId() == RedSniperRifle2Image.getId())
	      %obj.mountImage(RedSniperRifleImage, 0, -1, true);
      else if(%image.getId() == BlueSniperRifle2Image.getId())
	      %obj.mountImage(BlueSniperRifleImage, 0, -1, true);
   }

	// discs...
	if($Game::GameType == $Game::Ethernet)
	{
		if(%nr == 6)
		{
			launchExplosiveDisc(%obj);
			return;
		}
	}
	
	if(%client.numWeapons == 0)
		return;

	if(%nr > %client.numWeapons)
		return;

	if(%nr == 0)
		%obj.currWeapon++;
	else
		%obj.currWeapon = %nr;
	
	if(%obj.currWeapon > %client.numWeapons)
		%obj.currWeapon = 1;	

	%wpn = %client.weapons[%obj.currWeapon-1];

	if(%wpn == 1)
	{
	   if(%obj.getTeamId() == $CatEquipment::Blaster)
	      %obj.mountImage(RedBlaster4Image, 0, -1, true);
	   else
	      %obj.mountImage(BlueBlaster4Image, 0, -1, true);
	}
	else if(%wpn == $CatEquipment::BattleRifle)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedAssaultRifleImage, 0, -1, true);
		else
			%obj.mountImage(BlueAssaultRifleImage, 0, -1, true);
	}
	else if(%wpn == $CatEquipment::SniperRifle)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedSniperRifleImage, 0, -1, true);
		else
			%obj.mountImage(BlueSniperRifleImage, 0, -1, true);
	}
	else if(%wpn == $CatEquipment::MiniGun)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedMinigunImage, 0, -1, true);
		else
			%obj.mountImage(BlueMinigunImage, 0, -1, true);
	}
	else if(%wpn == $CatEquipment::RepelGun)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedRepelGunImage, 0, -1, true);
		else
			%obj.mountImage(BlueRepelGunImage, 0, -1, true);
	}
	else if(%wpn == $CatEquipment::GrenadeLauncher)
	{
		if(%obj.getTeamId() == 1)
			%obj.mountImage(RedGrenadeLauncherImage, 0, -1, true);
		else
			%obj.mountImage(BlueGrenadeLauncherImage, 0, -1, true);
	}
}

