//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function TeamsMenu_Link(%text, %arg1, %arg2, %arg3, %arg4)
{
   if(%arg1 $= "") %arg1 = 0;
   if(%arg2 $= "") %arg2 = 0;
   if(%arg3 $= "") %arg3 = 0;
   if(%arg4 $= "") %arg4 = 0;
   %arg = %arg1 @"/"@ %arg2 @"/"@ %arg3 @"/"@ %arg4;
   return "[<a:cmd Loadout " @ %arg @ ">" @ %text @ "</a>]";
}

function GameConnection::showTeamsMenu(%this)
{
	%L3 = om_init();
	%L3 = %L3 @ om_head(%this, "Switch Team");

	%joinText = "<just:center>Join Team:\n<spush><font:NovaSquare:24>";
	if(%this.team != $Team1)
		%joinText = %joinText @ "<a:cmd JoinTeam 1>";
	%joinText = %joinText @ "Reds (" @ $Team1.numPlayers @ ")";
	if(%this.team != $Team1)
		%joinText = %joinText @ "</a>";
	%joinText = %joinText @ "    ";
	if(%this.team != $Team2)
		%joinText = %joinText @ "<a:cmd JoinTeam 2>";
	%joinText = %joinText @ "Blues (" @ $Team2.numPlayers @ ")";
	if(%this.team != $Team2)
		%joinText = %joinText @ "</a>";
	%joinText = %joinText @ "    ";
	if(%this.team != $Team0)
		%joinText = %joinText @ "<a:cmd JoinTeam 0>";
	%joinText = %joinText @ "Observers (" @ $Team0.numPlayers @ ")";
	if(%this.team != $Team0)
		%joinText = %joinText @ "</a>";
   %joinText = %joinText @ "<spop>";

   %L3 = %L3 @ %joinText;

	%this.beginMenuText(%this.menu $= "teams");
   if(%L1 !$= "") %this.addMenuText(%L1, 1);
	if(%L2 !$= "") %this.addMenuText(%L2, 2);
	if(%L3 !$= "") %this.addMenuText(%L3, 4);
	if(%L4 !$= "") %this.addMenuText(%L4, 8);
	%this.endMenuText();

   return;

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
               %c3 = %c3 SPC LoadoutMenu_InfoLink(%no, %L3, %c);
               %c4 = LoadoutMenu_Link("Select", %no, "s", %slot, %item);
   		      %L3 = %L3 TAB "" TAB %c2 TAB %c3 TAB %c4 @ "\n";
            }
         }
         else
         {
            %c1 = "Slot #" @ %slot @ ":";
            %c2 = LoadoutMenu_GetIcon(%c);
            %c2 = "<bitmap:share/hud/rotc/icon." @ %c2 @ ".50x15>";
            %c3 = LoadoutMenu_GetWeaponName(%slot);
            %c3 = %c3 SPC LoadoutMenu_InfoLink(%no, %L3, %c);
            %c4 = LoadoutMenu_Link("Change", %no, "e", %slot);
   		   %L3 = %L3 TAB %c1 TAB %c2 TAB %c3  TAB %c4 @ "\n";
         }
         %L3 = %L3 @ "\n";
      }

      %L3 = %L3 @ "CAT Specs:\n";
		%L3 = %L3 @ "<bitmap:share/ui/rotc/cat.blueprint.200x500>\n";
		%L3 = %L3 @ " Hull:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.damper.20x20> Damper " @ LoadoutMenu_InfoLink(%no,%L3,10) @ "\n\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.barrier.20x20> Barrier " @ LoadoutMenu_InfoLink(%no,%L3,10) @ "\n\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.shield.20x20> Shield " @ LoadoutMenu_InfoLink(%no,%L3,10) @ "\n\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.bounce.20x20> B.O.U.N.C.E. " @ LoadoutMenu_InfoLink(%no,%L3,10) @ "\n\n";
		%L3 = %L3 @ " Head:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.repeldisc.20x20> Repel Disc Launcher " @ LoadoutMenu_InfoLink(%no,%L3,10) @ "\n\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.explosivedisc.20x20> Explosive Disc Launche " @ LoadoutMenu_InfoLink(%no,%L3,10) @ "\n\n";
		%L3 = %L3 @ " Arm:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.grenade.20x20> Grenade " @ LoadoutMenu_InfoLink(%no,%L3,10) @ "\n\n";
		%L3 = %L3 @ " Torso:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.vamp.20x20> V-AMP " @ LoadoutMenu_InfoLink(%no,%L3,10) @ "\n\n";
		%L3 = %L3 @ " Pelvis:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.anchor.20x20> Anchor " @ LoadoutMenu_InfoLink(%no,%L3,10) @ "\n\n";
		%L3 = %L3 @ " Feet:\n";
		%L3 = %L3 @ "    <bitmap:share/hud/rotc/icon.permaboard.20x20> Jump Booster " @ LoadoutMenu_InfoLink(%no,%L3,10) @ "\n\n";
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
      %spacers = (%infoPos*16)/14;

      for(%i = 0; %i < %spacers; %i++)
         %L1 = %L1 @ "<bitmap:share/ui/rotc/bg1spc>\n";
      %L1 = %L1 @ om_init();
      %L1 = %L1 @ "<lmargin:350>";
      %L1 = %L1 @ LoadoutMenu_Link("Close", %no);
      %L1 = %L1 @ "\n<lmargin:65>";
      %L1 = %L1 @ "BLAH BLAH BLAH\n";
      %L1 = %L1 @ "BLAH BLAH BLAH\n";
      %L1 = %L1 @ "BLAH BLAH BLAH\n";
      %L1 = %L1 @ "BLAH BLAH BLAH\n";

      for(%i = 0; %i < %spacers; %i++)
         %L2 = %L2 @ "<bitmap:share/ui/rotc/bg1>\n";
      %L2 = %L2 @ "<bitmap:share/ui/rotc/bg1t>\n";
      %n = getRecordCount(%L1) - %spacers - 1;
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



