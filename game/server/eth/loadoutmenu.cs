//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function LoadoutMenu_GetIcon(%nr)
{
   %icon = "[?]";
   switch(%nr)
   {
		case 1: %icon = "blaster";
		case 2: %icon = "rifle";
		case 3: %icon = "sniper";
		case 4: %icon = "minigun";
		case 5: %icon = "grenadelauncher";
		case 6: %icon = "etherboard";
		case 7: %icon = "regen";
   }
   return %icon;
}

function LoadoutMenu_GetWeaponName(%nr)
{
   %name = "[?]";
   switch(%nr)
   {
		case 1: %name = "Blaster";
		case 2: %name = "Battle Rifle";
		case 3: %name = "Sniper ROFL";
		case 4: %name = "Minigun";
		case 5: %name = "Bubblegun";
		case 6: %name = "Etherboard";
		case 7: %name = "Regeneration";
   }
   return %name;
}

function LoadoutMenu_Link(%text, %arg1, %arg2, %arg3, %arg4)
{
   if(%arg1 $= "") %arg1 = 0;
   if(%arg2 $= "") %arg2 = 0;
   if(%arg3 $= "") %arg3 = 0;
   if(%arg4 $= "") %arg4 = 0;
   %arg = %arg1 @"/"@ %arg2 @"/"@ %arg3 @"/"@ %arg4;
   return "[<a:cmd Loadout " @ %arg @ ">" @ %text @ "</a>]";
}

function LoadoutMenu_InfoLink(%loadout, %L3, %item)
{
   %arg1 = %loadout;
   %arg2 = "i";
   %arg3 = %item;
   %arg4 = getRecordCount(%L3);
   %arg = %arg1 @"/"@ %arg2 @"/"@ %arg3 @"/"@ %arg4;
   return "[<a:cmd Loadout " @ %arg @ ">" @ "?" @ "</a>]";
}

function LoadoutMenu_WeaponInfoLink(%loadout, %L3, %nr)
{
   %info = "";
   switch(%nr)
   {
		case 1: %info = "6.1";
		case 2: %info = "6.2";
		case 3: %info = "7.1";
		case 4: %info = "6.3";
		case 5: %info = "7.3";
		case 6: %info = "8.1";
		case 7: %info = "8.2";
   }
   LoadoutMenu_InfoLink(%loadout, %L3, %info);
}

function GameConnection::showLoadout(%this, %no, %expandslot, %showInfo, %infoPos)
{
	%L3 = om_init();
	%L3 = %L3 @ om_head(%this, "Edit Loadouts");

	%L3 = %L3 @ "<just:center>Select Loadout:\n";
	for(%i = 1; %i <= 10; %i++)
	{
		if(%i != %no)
			%L3 = %L3 @ "<a:cmd Loadout" SPC %i @ ">";
      %name = %this.loadoutName[%i];
      if(%name $= "")
         %name = %i;
		%L3 = %L3 @ %name;
		if(%i != %no)
			%L3 = %L3 @ "</a>";
		%L3 = %L3 @ "   ";
	}
 	%L3 = %L3 @ "<bitmap:share/misc/ui/sep>\n\n";

   %name = %this.loadoutName[%no];

  	%L3 = %L3 @ "<just:left><lmargin:10>";
	%L3 = %L3 @ "Loadout #" @ %no @ ": " @ %name;
   if(%name $= "")
   	%L3 = %L3 SPC LoadoutMenu_Link("Enable", %no, "n");
   else
   	%L3 = %L3 SPC LoadoutMenu_Link("Rename", %no, "n");
	%L3 = %L3 @ "<spop>\n";

	%code = %this.loadoutCode[%no];
	if(%code $= "")
	{
		%L3 = %newtext @ "An error occured! Sorry about that :(";
	}
	else
	{
      %L3 = %L3 @ "<tab:75,125,185,300>\n";
      for(%i = 0; %i < 3; %i++)
      {
         %slot = %i + 1;
         %c = getSubStr(%this.loadoutCode[%no], %i, 1);
         if(%slot == %expandslot)
         {
            %c1 = "Slot #" @ %slot @ ":";
            %c2 = "   ???";
            %c3 = "Choose one:";
            %c4 = LoadoutMenu_Link("Cancel", %no, "e", 0);
   		   %L3 = %L3 TAB %c1 TAB %c2 TAB %c3  TAB %c4 @ "\n";
            for(%item = 1; %item <= 7; %item++)
            {
               %c2 = LoadoutMenu_GetIcon(%item);
               %c2 = "<bitmap:share/hud/rotc/icon." @ %c2 @ ".50x15>";
               %c3 = LoadoutMenu_GetWeaponName(%item);
               %c3 = %c3 SPC LoadoutMenu_WeaponInfoLink(%no, %L3, %item);
               %c4 = LoadoutMenu_Link("Select", %no, "s", %slot, %item);
   		      %L3 = %L3 TAB "" TAB %c2 TAB %c3 TAB %c4 @ "\n";
            }
         }
         else
         {
            %c1 = "Slot #" @ %slot @ ":";
            %c2 = LoadoutMenu_GetIcon(%c);
            %c2 = "<bitmap:share/hud/rotc/icon." @ %c2 @ ".50x15>";
            %c3 = LoadoutMenu_GetWeaponName(%c);
            %c3 = %c3 SPC LoadoutMenu_WeaponInfoLink(%no, %L3, %c);
            %c4 = LoadoutMenu_Link("Change", %no, "e", %slot);
   		   %L3 = %L3 TAB %c1 TAB %c2 TAB %c3  TAB %c4 @ "\n";
         }
         %L3 = %L3 @ "\n";
      }

      %L3 = %L3 @ "CAT Specs:\n";
		%L3 = %L3 @ "<bitmap:share/ui/rotc/cat.blueprint.200x500>\n";
		%L3 = %L3 @ " Hull:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.damper.20x20> Damper " @ LoadoutMenu_InfoLink(%no,%L3,"4.1") @ "\n\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.shield.20x20> Shield " @ LoadoutMenu_InfoLink(%no,%L3,"4.2") @ "\n\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.barrier.20x20> Barrier " @ LoadoutMenu_InfoLink(%no,%L3,"4.3") @ "\n\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.bounce.20x20> B.O.U.N.C.E. " @ LoadoutMenu_InfoLink(%no,%L3,"7.2") @ "\n\n";
		%L3 = %L3 @ " Head:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.repeldisc.20x20> Repel Disc Launcher " @ LoadoutMenu_InfoLink(%no,%L3,"5.1") @ "\n\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.explosivedisc.20x20> Explosive Disc Launcher " @ LoadoutMenu_InfoLink(%no,%L3,"5.2") @ "\n\n";
		%L3 = %L3 @ " Arm:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.grenade.20x20> Grenade " @ LoadoutMenu_InfoLink(%no,%L3,"6.4") @ "\n\n";
		%L3 = %L3 @ " Torso:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.vamp.20x20> V-AMP " @ LoadoutMenu_InfoLink(%no,%L3,"4.5") @ "\n\n";
		%L3 = %L3 @ " Pelvis:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.anchor.20x20> Anchor " @ LoadoutMenu_InfoLink(%no,%L3,"4.4") @ "\n\n";
		%L3 = %L3 @ " Feet:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.booster.20x20> Jump Booster " @ LoadoutMenu_InfoLink(%no,%L3,"8.3") @ "\n\n";
	}

   %L4 = om_init();
   if(%expandslot != 0)
      %L4 = %L4 @ "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
   else
      %L4 = %L4 @ "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
   %L4 = %L4 @ "<tab:70,125>";
   %L4 = %L4 @ "\tHealth:\t75\n";
   %L4 = %L4 @ "\tShield:\t25\n";
   %L4 = %L4 @ "\tEnergy:\t100\n";

   if(%showInfo > 0)
   {
	loadManual();

      %page = getManualPage(%showInfo);
      %spacers = (%infoPos*16)/14;

      for(%i = 0; %i < %spacers; %i++)
         %L1 = %L1 @ "<bitmap:share/ui/rotc/bg1spc>\n";
      %L1 = %L1 @ om_init();
      %L1 = %L1 @ "<just:center>";
      %L1 = %L1 @ "<spush><font:NovaSquare:24>" @ %page.name @ "<spop>\n\n";
      %L1 = %L1 @ LoadoutMenu_Link("Done", %no);
      %L1 = %L1 @ "\n\n<just:left><lmargin:5><rmargin:480>";
      %L1 = %L1 @ %page.text;
      %L1 = %L1 @ "\n<just:center>";
      %L1 = %L1 @ LoadoutMenu_Link("Done", %no);

      for(%i = 0; %i < %spacers; %i++)
         %L2 = %L2 @ "<bitmap:share/ui/rotc/bg1>\n";
      %L2 = %L2 @ "<bitmap:share/ui/rotc/bg1t>\n";
      %n = %page.size + 6;
      while(%n > 0)
      {
         %L2 = %L2 @ "<bitmap:share/ui/rotc/bg1m>\n";
         %n--;
      }
      %L2 = %L2 @ "<bitmap:share/ui/rotc/bg1b>\n";
      if(%expandslot != 0)
         %n = 50;
      else
         %n = 45;
      for(%j = 0; %j < %n-%i; %j++)
         %L2 = %L2 @ "<bitmap:share/ui/rotc/bg1>\n";
   }

	%this.beginMenuText(%this.menu $= "loadout");
   if(%L1 !$= "") %this.addMenuText(%L1, 1);
	if(%L2 !$= "") %this.addMenuText(%L2, 2);
	if(%L3 !$= "") %this.addMenuText(%L3, 4);
	if(%L4 !$= "") %this.addMenuText(%L4, 8);
	%this.endMenuText();
}



