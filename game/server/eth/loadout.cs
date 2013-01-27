//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function GameConnection::loadDefaultLoadout(%this, %no)
{
   switch(%no)
   {
      case 1:
         %this.loadoutName[%no] = "Commando";
         %this.loadoutCode[%no] = "216";
      case 2:
         %this.loadoutName[%no] = "Ranger";
         %this.loadoutCode[%no] = "236";
      case 3:
         %this.loadoutName[%no] = "Skirmisher";
         %this.loadoutCode[%no] = "276";
      case 4:
         %this.loadoutName[%no] = "Protector";
         %this.loadoutCode[%no] = "275";
      case 5:
         %this.loadoutName[%no] = "Backup";
         %this.loadoutCode[%no] = "477";
      default:
         %this.loadoutName[%no] = "";
         %this.loadoutCode[%no] = "216";
   }
}

//------------------------------------------------------------------------------

function GameConnection::defaultLoadout(%this)
{
	for(%i = 1; %i <= 9; %i++)
		this.loadout[%i] = "";

	if($Game::GameType == $Game::Ethernet)
	{
		%this.loadout[1] = $CatEquipment::BattleRifle;
		%this.loadout[2] = $CatEquipment::Blaster;
		%this.loadout[3] = $CatEquipment::Etherboard;
		%this.loadout[4] = $CatEquipment::Damper;
		%this.loadout[5] = $CatEquipment::VAMP;
		%this.loadout[6] = $CatEquipment::Anchor;
		%this.loadout[7] = $CatEquipment::Grenade;
		%this.loadout[8] = $CatEquipment::Bounce;
		%this.loadout[9] = $CatEquipment::RepelDisc;
		%this.loadout[10] = $CatEquipment::ExplosiveDisc;
	}
	else if($Game::GameType == $Game::Infantry)
	{
		%this.loadout[1] = $CatEquipment::Blaster;
		%this.loadout[2] = $CatEquipment::BattleRifle;
		%this.loadout[3] = $CatEquipment::Etherboard;
	}
	else if($Game::GameType == $Game::TeamJoust)
	{
		%this.loadout[1] = $CatEquipment::Blaster;
		%this.loadout[2] = $CatEquipment::BattleRifle;
		%this.loadout[3] = $CatEquipment::GrenadeLauncher;
		%this.loadout[4] = $CatEquipment::Damper;
		%this.loadout[5] = $CatEquipment::VAMP;
		%this.loadout[6] = $CatEquipment::Stabilizer;
		%this.loadout[7] = $CatEquipment::Grenade;
		%this.loadout[8] = $CatEquipment::Permaboard;
		%this.loadout[9] = $CatEquipment::SlasherDisc;
	}
	else if($Game::GameType == $Game::TeamDragRace)
	{
		%this.loadout[1] = $CatEquipment::Blaster;
		%this.loadout[2] = $CatEquipment::BattleRifle;
		%this.loadout[3] = $CatEquipment::GrenadeLauncher;
		%this.loadout[4] = $CatEquipment::Damper;
		%this.loadout[5] = $CatEquipment::VAMP;
		%this.loadout[6] = $CatEquipment::Stabilizer;
		%this.loadout[7] = $CatEquipment::Grenade;
		%this.loadout[8] = $CatEquipment::SlasherDisc;
	}
	else if($Game::GameType == $Game::GridWars)
	{
		%this.loadout[1] = $CatEquipment::BattleRifle;
		%this.loadout[2] = $CatEquipment::SniperRifle;
		%this.loadout[3] = $CatEquipment::Etherboard;
		%this.loadout[4] = $CatEquipment::Grenade;
		%this.loadout[5] = $CatEquipment::Bounce;
		%this.loadout[6] = $CatEquipment::RepelDisc;
		%this.loadout[7] = $CatEquipment::ExplosiveDisc;
	}
	else
	{
		%this.loadout[1] = $CatEquipment::Blaster;
		%this.loadout[2] = $CatEquipment::BattleRifle;
		%this.loadout[3] = $CatEquipment::Etherboard;
		%this.loadout[4] = $CatEquipment::Damper;
		%this.loadout[5] = $CatEquipment::VAMP;
		%this.loadout[6] = $CatEquipment::Stabilizer;
		%this.loadout[7] = $CatEquipment::Grenade;
		%this.loadout[8] = $CatEquipment::SlasherDisc;
	}
}

function GameConnection::updateLoadout(%this)
{
	%this.numWeapons = 0;
	%this.hasDamper = false;
	%this.hasAnchor = false;
	%this.hasStabilizer = false;
	%this.hasSlasherDisc = false;
	%this.hasRepelDisc = false;
	%this.hasExplosiveDisc = false;
	%this.hasGrenade = false;
	%this.hasBounce = false;
	%this.hasEtherboard = false;
	%this.hasPermaboard = false;
	%this.numVAMPs = 0;
	%this.numRegenerators = 0;
	for(%i = 1; %i <= 9; %i++)
	{
		if(%this.loadout[%i] $= "")
			continue;

		if(%this.loadout[%i] == $CatEquipment::Damper)
		{
			%this.hasDamper = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Anchor)
		{
			%this.hasAnchor = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Stabilizer)
		{
			%this.hasStabilizer = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::SlasherDisc)
		{
			%this.hasSlasherDisc = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::RepelDisc)
		{
			%this.hasRepelDisc = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::ExplosiveDisc)
		{
			%this.hasExplosiveDisc = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Grenade)
		{
			%this.hasGrenade = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Bounce)
		{
			%this.hasBounce = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Etherboard)
		{
			%this.hasEtherboard = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Permaboard)
		{
			%this.hasPermaboard = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::VAMP)
		{
			%this.numVAMPs++;
		}
		else if(%this.loadout[%i] == $CatEquipment::Regeneration)
		{
			%this.numRegenerators++;
		}
		else if(%this.loadout[%i] < $CatEquipment::SlasherDisc)
		{
			%this.weapons[%this.numWeapons] = %this.loadout[%i];
			%this.numWeapons++;
		}
	}
}

function GameConnection::displayInventory(%this, %obj)
{
   if(%this.team == $Team0)
   {
      %this.setHudMenuL("*", " ", 1, 0);
      %this.setHudMenuR("*", " ", 1, 0);
      return;
   }

	%iconname[$CatEquipment::Blaster] = "blaster";
	%iconname[$CatEquipment::BattleRifle] = "rifle";
	%iconname[$CatEquipment::SniperRifle] = "sniper";
	%iconname[$CatEquipment::MiniGun] = "minigun";
	%iconname[$CatEquipment::RepelGun] = "grenadelauncher";
	%iconname[$CatEquipment::GrenadeLauncher] = "grenadelauncher";
	%iconname[$CatEquipment::SlasherDisc] = "slasherdisc";
	%iconname[$CatEquipment::RepelDisc] = "repeldisc";
	%iconname[$CatEquipment::ExplosiveDisc] = "explosivedisc";
	%iconname[$CatEquipment::Damper] = "damper";
	%iconname[$CatEquipment::Shield] = "shield";
	%iconname[$CatEquipment::Barrier] = "barrier";
	%iconname[$CatEquipment::VAMP] = "vamp";
	%iconname[$CatEquipment::Anchor] = "anchor";
	%iconname[$CatEquipment::Stabilizer] = "stabilizer";
	%iconname[$CatEquipment::Permaboard] = "permaboard";
	%iconname[$CatEquipment::Grenade] = "grenade";
	%iconname[$CatEquipment::Bounce] = "bounce";
	%iconname[$CatEquipment::Etherboard] = "etherboard";
	%iconname[$CatEquipment::Regeneration] = "regen";

	%item[1] = $CatEquipment::Blaster;
	%item[2] = $CatEquipment::BattleRifle;
	%item[3] = $CatEquipment::SniperRifle;
	%item[4] = $CatEquipment::Minigun;
	if($Game::GameType == $Game::Ethernet)
		%item[5] = $CatEquipment::RepelGun;
	else
		%item[5] = $CatEquipment::GrenadeLauncher;
	%item[6] = $CatEquipment::Etherboard;
	%item[7] = $CatEquipment::Regeneration;
	%numItems = $Game::GameType == $Game::Ethernet ? 7 : 5;

	%itemname[1] = "Blaster";
	%itemname[2] = "Battle Rifle";
	%itemname[3] = "Sniper ROFL";
	%itemname[4] = "Minigun";
	%itemname[5] = $Game::GameType == $Game::Ethernet ? "Bubblegun" : "Gren. Launcher";
	%itemname[6] = "Etherboard";
	%itemname[7] = "Regeneration";

	%fixed = false;
	if($Game::GameType == $Game::mEthMatch)
		%fixed = true;

	if(%this.inventoryMode $= "showicon")
	{
		%margin = "\n\n\n\n\n\n\n\n\n\n\n\n";

		%numDiscs = %obj.numDiscs;
		%this.setHudMenuL(0, %margin, 1, 1);
		%this.setHudMenuL(1, "<bitmap:share/hud/rotc/icon.disc.png><sbreak>", %numDiscs, 1);
		for(%i = 2; %i < 10; %i++)
			%this.setHudMenuL(%i, "", 1, 0);
	}
	else if(%this.inventoryMode $= "show")
	{
		%this.setHudMenuL(0, "\n", 8, 1);
		%this.setHudMenuL(1, "<lmargin:100><font:NovaSquare:18><tab:120,175,200>" @
         "Select Loadout:\n\n", 1, 1);

		%slot = 2;
		%prefix = "<bitmap:share/hud/rotc/icon.";
		%suffix = ".50x15> ";
		for(%i = 1; %i <= 3; %i++)
		{
		   for(%j = 1; %j <= %numItems; %j++)
		   {
				if(%this.loadout[%i] == %item[%j])
				{
					%icon = %iconname[%item[%j]];
					%this.setHudMenuL(%slot, %prefix @ %icon @ %suffix, 1, 1);
					%slot++;
		 	   }
		   }
		}
		%this.setHudMenuL(%slot, "<sbreak><font:NovaSquare:14>" @
         "<bitmap:share/hud/rotc/icon.quickswitch.50x15>" @
         "Press @bind51 to exchange\n\nLoad:\n", 1, 1);
      %slot++;

      %tmp = "";
		for(%i = 1; %i <= 10; %i++)
		{
         if(%this.loadoutName[%i] !$= "")
            %tmp = %tmp @ "@bind" @ 34+%i @ ":" TAB %this.loadoutName[%i];
         %tmp = %tmp @ "\n";
		}
      %tmp = %tmp @ "\n";
      %l = strlen(%tmp); %n = 0;
   	while(%n < %l)
	   {
		   %chunk = getSubStr(%tmp, %n, 255);
   		%this.setHudMenuL(%slot, %tmp, 1, 1);
		   %n += 255;
         %slot++;
	   }

      if(%this.loadout[1] == 0)
      {
         %this.setHudMenuL(%slot, "!!! ILLEGAL LOADOUT !!!", 1, 1);
         return;
      }

		for(%i = %slot; %i < 10; %i++)
			%this.setHudMenuL(%i, "", 1, 0);
	}
	else if(%this.inventoryMode $= "oldshow")
	{
		for(%i = 1; %i <= 3; %i++)
			%icon[%i] = %iconname[%this.loadout[%i]];

		%this.setHudMenuL(0, "<font:NovaSquare:12>" @ %margin, 1, 1);

		%this.setHudMenuL(1, "Slot #1:\n", 1, 1);
		%this.setHudMenuL(2, "<bitmap:share/hud/rotc/icon." @ %icon[1] @ ".50x15>", 1, 1);
		if(%fixed)
			%this.setHudMenuL(3, "<sbreak>(FIXED)", 1, 1);
		else
			%this.setHudMenuL(3, "<sbreak>(Press @bind35 to change)", 1, 1);

		%this.setHudMenuL(4, "\n\n\n\n\n\Slot #2:\n", 1, 1);
		%this.setHudMenuL(5, "<bitmap:share/hud/rotc/icon." @ %icon[2] @ ".50x15>", 1, 1);
		if(%fixed)
			%this.setHudMenuL(6, "<sbreak>(FIXED)", 1, 1);
		else
			%this.setHudMenuL(6, "<sbreak>(Press @bind36 to change)", 1, 1);

		%this.setHudMenuL(7, "\n\n\n\n\n\Slot #3:\n", 1, 1);
		%this.setHudMenuL(8, "<bitmap:share/hud/rotc/icon." @ %icon[3] @ ".50x15>", 1, 1);
		if(%fixed)
			%this.setHudMenuL(9, "<sbreak>(FIXED)", 1, 1);
		else
			%this.setHudMenuL(9, "<sbreak>(Press @bind37 to change)", 1, 1);
	}
	else if(%this.inventoryMode $= "fill")
	{
		if(%this.gameVersionString $= "p.4-testing")
			%margin = "\n\n";
		else
			%margin = "\n\n\n\n\n\n\n\n\n\n\n\n";

		%this.setHudMenuL(0, "<font:NovaSquare:12>" @ %margin, 1, 1);
		%slot = 1;
		for(%i = 1; %i <= %numItems; %i++)
		{
			%amount = (%i == 7 ? 3 : 1);
			for(%j = 0; %j < %this.inventoryMode[1]; %j++)
			{
				if(%this.loadout[%j] == %item[%i])
					%amount--;
			}

			%text = "\n<sbreak>@bind" @ (%i < 6 ? 34 : 41) + %i @ ": " @ %itemname[%i];
			%icon = "<bitmap:share/hud/rotc/icon." @ %iconname[%item[%i]] @ ".50x15>";
			if(%amount == 1)
			{
				%this.setHudMenuL(%slot, %text @ "\n" @ " " @ %icon, 1, 1);
				%slot++;
			}
			else if(%amount > 1)
			{
				%this.setHudMenuL(%slot, %text @ "\n" @ " ", 1, 1);
				%this.setHudMenuL(%slot+1, %icon, %amount, 1);
				%slot += 2;
			}
		}

		for(%i = %slot; %i <= 9; %i++)
			%this.setHudMenuL(%i, "", 1, 0);
	}
	else if(%this.inventoryMode $= "select")
	{
		%item[1] = $CatEquipment::Blaster;
		%item[2] = $CatEquipment::BattleRifle;
		%item[3] = $CatEquipment::SniperRifle;
		%item[4] = $CatEquipment::Minigun;
		if($Game::GameType == $Game::Ethernet)
			%item[5] = $CatEquipment::RepelGun;
		else
			%item[5] = $CatEquipment::GrenadeLauncher;
		%item[6] = $CatEquipment::Etherboard;
		%item[7] = $CatEquipment::Regeneration;

		%itemname[1] = "Blaster";
		%itemname[2] = "Battle Rifle";
		%itemname[3] = "Sniper ROFL";
		%itemname[4] = "Minigun";
		%itemname[5] = $Game::GameType == $Game::Ethernet ? "Bubblegun" : "Gren. Launcher";
		%itemname[6] = "Etherboard";
		%itemname[7] = "Regeneration";

		%numItems = $Game::GameType == $Game::Ethernet ? 7 : 5;

		%this.setHudMenuL(0, "\n", 8, 1);
		%this.setHudMenuL(1, "<lmargin:100><font:NovaSquare:12>Select slot #" @ %this.inventoryMode[1] @ ":\n\n", 1, 1);
		for(%i = 1; %i <= %numItems; %i++)
			%this.setHudMenuL(%i+1, "@bind" @ (%i < 6 ? 34 : 41) + %i @ ": " @ %itemname[%i]  @  "\n" @
				"   <bitmap:share/hud/rotc/icon." @ %iconname[%item[%i]] @ ".50x15>" @ "<sbreak>", 1, 1);
		for(%i = %numItems + 2; %i <= 9; %i++)
			%this.setHudMenuL(%i, "", 1, 0);
	}

   return;

	%i = -1;
	if($Game::GameType == $Game::Ethernet)
	{
		%this.setHudMenuR(0, "<just:right>\n\n\n\n", 1, 1);
		%i++; %icon[%i] = "damper";
		%i++; %icon[%i] = "-";
		%i++; %icon[%i] = "barrier";
		%i++; %icon[%i] = "shield";
		%i++; %icon[%i] = "-";
		%i++; %icon[%i] = "anchor";
		%i++; %icon[%i] = "vamp";
		%i++; %icon[%i] = "--";
		%i++; %icon[%i] = "explosivedisc";
		%i++; %icon[%i] = "repeldisc";
		%i++; %icon[%i] = "--";
		%i++; %icon[%i] = "bounce";
		%i++; %icon[%i] = "grenade";
		%i++; %icon[%i] = "-";
	}
	else
	{
		%this.setHudMenuR(0, "<just:right>\n\n\n\n", 1, 1);
		%i++; %icon[%i] = "slasherdisc";
		%i++; %icon[%i] = "repeldisc";
		%i++; %icon[%i] = "explosivedisc";
		%i++; %icon[%i] = "damper";
		%i++; %icon[%i] = "shield";
		%i++; %icon[%i] = "barrier";
		%i++; %icon[%i] = "vamp";
		%i++; %icon[%i] = "anchor";
		%i++; %icon[%i] = "stabilizer";
		%i++; %icon[%i] = "permaboard";
		%i++; %icon[%i] = "grenade";
		%i++; %icon[%i] = "bounce";
		%i++; %icon[%i] = "-";
	}

	if(%this.gameVersionString $= "p.4-testing")
		%margin = "";
	else
		%margin = "\n\n\n\n\n\n";

	%slot = 1;
	%text = %margin;
	%max = %i;
	for(%i = 0; %i <= %max; %i++)
	{
		%string = %icon[%i];
		if(%icon[%i] $= "-")
		{
			%text = %text @ "<sbreak>";
			%this.setHudMenuR(%slot, %text, 1, 1);
			%slot++;
			%text = "";
		}
		else if(%icon[%i] $= "--")
		{
			%text = %text @ "<sbreak><bitmap:share/hud/rotc/sep.48x5>\n";
			%this.setHudMenuR(%slot, %text, 1, 1);
			%slot++;
			%text = "";
		}
		else
		{
			%text = %text @ "<bitmap:share/hud/rotc/icon." @ %icon[%i] @ ".20x20>";
			%text = %text @ "<bitmap:share/hud/rotc/spacer.8x20>";
		}
	}
}

function GameConnection::changeInventory(%this, %nr)
{
	if($Game::GameType == $Game::mEthMatch)
		return;

	if(%this.inventoryMode $= "show")
	{
      if(%nr == -17)
      {
			%this.inventoryMode = "select";
			%this.inventoryMode[1] = 1;
      }
		else if(%nr >= 0 && %nr <= 10)
		{
         if(%this.loadoutName[%nr] $= "")
            return;

         for(%i = 0; %i < 3; %i++)
         {
            %c = getSubStr(%this.loadoutCode[%nr], %i, 1);
            if(%c < 1 || %c > 7)
            {
               %this.loadout[1] = 0;
              	%this.play2D(BeepMessageSound);
               %this.displayInventory();
               return;
            }
 			   switch(%c)
			   {
				   case 1: %equipment = $CatEquipment::Blaster;
				   case 2: %equipment = $CatEquipment::BattleRifle;
				   case 3: %equipment = $CatEquipment::SniperRifle;
				   case 4: %equipment = $CatEquipment::MiniGun;
				   case 5: %equipment = $CatEquipment::RepelGun;
				   case 6: %equipment = $CatEquipment::Etherboard;
				   case 7: %equipment = $CatEquipment::Regeneration;
			   }
            %this.loadout[%i+1] = %equipment;
         }
			%this.updateLoadout();
		}
		else if(%nr == 7)
		{
			%this.inventoryMode = "fill";
			%this.inventoryMode[1] = 1;
		}
      else
      {
         return;
      }
		%this.displayInventory();
	}
	else if(%this.inventoryMode $= "fill")
	{
		if($Game::GameType == $Game::TeamJoust
		|| $Game::GameType == $Game::TeamDragRace)
		{
			if(%nr < 1 || %nr > 5)
				return;

			switch(%nr)
			{
				case 1: %equipment = $CatEquipment::Blaster;
				case 2: %equipment = $CatEquipment::BattleRifle;
				case 3: %equipment = $CatEquipment::SniperRifle;
				case 4: %equipment = $CatEquipment::MiniGun;
				case 5: %equipment = $CatEquipment::GrenadeLauncher;
			}
		}
		else
		{
			if(%nr < 1 || %nr > 7)
				return;

			switch(%nr)
			{
				case 1: %equipment = $CatEquipment::Blaster;
				case 2: %equipment = $CatEquipment::BattleRifle;
				case 3: %equipment = $CatEquipment::SniperRifle;
				case 4: %equipment = $CatEquipment::MiniGun;
				case 5: %equipment = $CatEquipment::RepelGun;
				case 6: %equipment = $CatEquipment::Etherboard;
				case 7: %equipment = $CatEquipment::Regeneration;
			}
		}

		if(%equipment != $CatEquipment::Regeneration)
		{
			for(%i = 0; %i < %this.inventoryMode[1]; %i++)
			{
				if(%this.loadout[%i] == %equipment)
					return;
			}
		}

		%this.loadout[%this.inventoryMode[1]] = %equipment;

		if(%this.inventoryMode[1] == 3)
		{
			%this.updateLoadout();
			%this.inventoryMode = "show";
		}
		else
		{
			%this.inventoryMode[1]++;
		}
		%this.displayInventory(0);
	}
	else if(%this.inventoryMode $= "select")
	{
		if($Game::GameType == $Game::TeamJoust
		|| $Game::GameType == $Game::TeamDragRace)
		{
			if(%nr < 1 || %nr > 5)
				return;

			switch(%nr)
			{
				case 1: %equipment = $CatEquipment::Blaster;
				case 2: %equipment = $CatEquipment::BattleRifle;
				case 3: %equipment = $CatEquipment::SniperRifle;
				case 4: %equipment = $CatEquipment::MiniGun;
				case 5: %equipment = $CatEquipment::GrenadeLauncher;
			}
		}
		else
		{
			if(%nr < 1 || %nr > 7)
				return;

			switch(%nr)
			{
				case 1: %equipment = $CatEquipment::Blaster;
				case 2: %equipment = $CatEquipment::BattleRifle;
				case 3: %equipment = $CatEquipment::SniperRifle;
				case 4: %equipment = $CatEquipment::MiniGun;
				case 5: %equipment = $CatEquipment::RepelGun;
				case 6: %equipment = $CatEquipment::Etherboard;
				case 7: %equipment = $CatEquipment::Regeneration;
			}
		}

		%this.loadout[%this.inventoryMode[1]] = %equipment;
		%this.updateLoadout();

		%this.inventoryMode = "show";
		%this.displayInventory(0);
	}
  	%this.play2D(BipMessageSound);
}

//------------------------------------------------------------------------------

