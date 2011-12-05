//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function serverCmdTextInput(%client, %text)
{
   if(%client.textInputTargetName $= "LoadoutName")
   {
      %loadout = %client.textInputTargetArg[0];
      %client.loadoutName[%loadout] = %text;
      %client.displayInventory();
      serverCmdLoadout(%client, %loadout);
   }
   %client.textInputTargetName = "";
}

function serverCmdLoadout(%client, %str)
{
	error(%str);

	%str = strreplace(%str, "/", " ");
	%arg1 = getWord(%str, 0);
	%arg2 = getWord(%str, 1);
	%arg3 = getWord(%str, 2);
	%arg4 = getWord(%str, 3);

   %loadout = %arg1;
   if(%loadout $= "")
   {
      %loadout = 1;
      %client.loadoutMenuExpandedSlot = 0;
   }
   else if(!(%loadout > 0 && %loadout <= 10))
      return;

   %showInfo = 0;
   %infoPos = 0;

   if(%arg2 $= "n")
   {
      %client.textInputTargetName = "LoadoutName";
      %client.textInputTargetArg[0] = %loadout;
      commandToClient(%client, 'RequestTextInput', "New name", %client.loadoutName[%loadout]);
   }
   else if(%arg2 $= "e")
   {
      %slot = %arg3;
      if(%slot >= 1 && %slot <= 3)
         %client.loadoutMenuExpandedSlot = %slot;
      else
         %client.loadoutMenuExpandedSlot = 0;
   }
   else if(%arg2 $= "s")
   {
      %slot = %arg3;
      %item = %arg4;
      if(%slot >= 1 && %slot <= 3)
      {
         if(%item >= 1 && %item <= 7)
         {
            %newCode = "";
            for(%i = 1; %i <= 3; %i++)
            {
               if(%i == %slot)
                  %c = %item;
               else
                  %c = getSubStr(%client.loadoutCode[%loadout], %i-1, 1);
               %newCode = %newCode @ %c;
            }
            %client.loadoutCode[%loadout] = %newCode;
            %client.sendCookie("ETH_LCODE" @ %loadout, %newCode);
            %client.loadoutMenuExpandedSlot = 0;
         }
      }
   }
   else if(%arg2 $= "i")
   {
      %showInfo = %arg3;
      %infoPos = %arg4;
   }

   %client.showLoadout(%loadout, %client.loadoutMenuExpandedSlot,
      %showInfo, %infoPos);

	%client.menu = "loadout";
}

function serverCmdTeams(%client)
{
   %client.showTeamsMenu();
	%client.menu = "teams";
}


function serverCmdHowToPlay(%client, %page)
{
	%newtxt = om_init();
	%client.beginMenuText();

	if(%page $= "")
		%page = 1;

	if($Game::GameType == $Game::mEthMatch)
		%help = "help-tdm";
	else
		%help = "help";	

	%newtxt = %newtxt @ 
		om_head(%client, "Manual") @
		"<just:center><spush><font:NovaSquare:20>How to play " @ $Game::GameTypeString @ "?<spop>\n\n";

	if($Game::GameType == $Game::mEthMatch)
	{
		%n = 0;
		%pageNumber[%n] = "0"; 
		%pageTitle[%n] = "Index";
		%pageFile[%n] = "index";
		%n++;
		%pageNumber[%n] = "1"; 
		%pageTitle[%n] = "ROTC: Team Deathmatch in a nutshell";
		%pageFile[%n] = "summary";
		%n++;
		%pageNumber[%n] = "2"; 
		%pageTitle[%n] = "Basics";
		%pageFile[%n] = "basics";
		%n++;
		%pageNumber[%n] = "3"; 
		%pageTitle[%n] = "Playing as Etherform";
		%pageFile[%n] = "etherform";
		%n++;
		%pageNumber[%n] = "4"; 
		%pageTitle[%n] = "Playing in CAT form";
		%pageFile[%n] = "cat";
		%n++;
		%pageNumber[%n] = "5"; 
		%pageTitle[%n] = "Basic CAT Weapons";
		%pageFile[%n] = "basicweapons";
		%n++;
		%pageNumber[%n] = "5.1"; 
		%pageTitle[%n] = "Discs";
		%pageFile[%n] = "discs";
		%n++;
		%pageNumber[%n] = "5.2"; 
		%pageTitle[%n] = "Grenades";
		%pageFile[%n] = "grenades";
		%n++;
		%pageNumber[%n] = "6"; 
		%pageTitle[%n] = "Red/Blue (buffer) CAT Weapons";
		%pageFile[%n] = "bufferweapons";
		%n++;
		%pageNumber[%n] = "6.1"; 
		%pageTitle[%n] = "Blaster";
		%pageFile[%n] = "blaster";
		%n++;
		%pageNumber[%n] = "6.2"; 
		%pageTitle[%n] = "Battle Rifle";
		%pageFile[%n] = "battlerifle";
		%n++;
		%pageNumber[%n] = "7"; 
		%pageTitle[%n] = "Other CAT equipment";
		%pageFile[%n] = "catequipment";
		%n++;	
		%pageNumber[%n] = "7.1"; 
		%pageTitle[%n] = "The Etherboard";
		%pageFile[%n] = "etherboard";
		%n++;
		%pageNumber[%n] = "C"; 
		%pageTitle[%n] = "Controls reference";
		%pageFile[%n] = "controls";
		%n++;
		%pageNumber[%n] = "H"; 
		%pageTitle[%n] = "Handicap";
		%pageFile[%n] = "handicap";
		%n++;
		%pageNumber[%n] = "X"; 
		%pageTitle[%n] = "Bots, Mapping & other neat stuff";
		%pageFile[%n] = "other";
		%n++;
	}
	else
	{
		%n = 0;
		%pageNumber[%n] = "0"; 
		%pageTitle[%n] = "Index";
		%pageFile[%n] = "index";
		%n++;
		%pageNumber[%n] = "1"; 
		%pageTitle[%n] = "ROTC: Ethernet in a nutshell";
		%pageFile[%n] = "summary";
		%n++;
		%pageNumber[%n] = "2"; 
		%pageTitle[%n] = "Basics";
		%pageFile[%n] = "basics";
		%n++;
		%pageNumber[%n] = "3"; 
		%pageTitle[%n] = "Playing as Etherform";
		%pageFile[%n] = "etherform";
		%n++;
		%pageNumber[%n] = "4"; 
		%pageTitle[%n] = "Playing in CAT form";
		%pageFile[%n] = "cat";
		%n++;
		%pageNumber[%n] = "5"; 
		%pageTitle[%n] = "Basic CAT Weapons";
		%pageFile[%n] = "basicweapons";
		%n++;
		%pageNumber[%n] = "5.1"; 
		%pageTitle[%n] = "Discs";
		%pageFile[%n] = "discs";
		%n++;
		%pageNumber[%n] = "5.2"; 
		%pageTitle[%n] = "Grenades";
		%pageFile[%n] = "grenades";
		%n++;
		%pageNumber[%n] = "5.3";
		%pageTitle[%n] = "<bitmap:share/hud/rotc/icon.bounce.20x20>B.O.U.N.C.E.";
		%pageFile[%n] = "bounce";
		%n++;
		%pageNumber[%n] = "6"; 
		%pageTitle[%n] = "Red/Blue (buffer) CAT Weapons";
		%pageFile[%n] = "bufferweapons";
		%n++;
		%pageNumber[%n] = "6.1"; 
		%pageTitle[%n] = "Blaster";
		%pageFile[%n] = "blaster";
		%n++;
		%pageNumber[%n] = "6.2"; 
		%pageTitle[%n] = "Battle Rifle";
		%pageFile[%n] = "battlerifle";
		%n++;
		%pageNumber[%n] = "6.3"; 
		%pageTitle[%n] = "Minigun";
		%pageFile[%n] = "minigun";
		%n++;
		%pageNumber[%n] = "7"; 
		%pageTitle[%n] = "Green/Orange (barrier) CAT Weapons";
		%pageFile[%n] = "barrierweapons";
		%n++;
		%pageNumber[%n] = "7.1"; 
		%pageTitle[%n] = "Sniper ROFL";
		%pageFile[%n] = "sniperrofl";
		%n++;	
		%pageNumber[%n] = "8"; 
		%pageTitle[%n] = "Other CAT equipment";
		%pageFile[%n] = "catequipment";
		%n++;	
		%pageNumber[%n] = "8.1"; 
		%pageTitle[%n] = "The Etherboard";
		%pageFile[%n] = "etherboard";
		%n++;
		%pageNumber[%n] = "8.2"; 
		%pageTitle[%n] = "Regeneration modules";
		%pageFile[%n] = "regeneration";
		%n++;
		%pageNumber[%n] = "8.3"; 
		%pageTitle[%n] = "Mine Launcher";
		%pageFile[%n] = "minelauncher";
		%n++;
		%pageNumber[%n] = "C"; 
		%pageTitle[%n] = "Controls reference";
		%pageFile[%n] = "controls";
		%n++;
		%pageNumber[%n] = "H"; 
		%pageTitle[%n] = "Handicap";
		%pageFile[%n] = "handicap";
		%n++;
		%pageNumber[%n] = "X"; 
		%pageTitle[%n] = "Bots, Mapping & other neat stuff";
		%pageFile[%n] = "other";
		%n++;
	}

	for(%i = 0; %i < %n; %i++)
	{
		if(%page $= %pageNumber[%i])
		{
			%prev =	%i-1;
			%next = %i+1;
			%file = %pageFile[%i];
			%title = %pageNumber[%i] @ ". " @ %pageTitle[%i];
			break;
		}
	}

	if(%file $= "")
	{
		%title = "0. Index";
		%prev = "";
		%next = "<a:cmd HowToPlay 1>" @ %gametype @ " in a nutshell</a>";
		%fileName = "game/server/eth/" @ %help @ "/index.rml";
	}
	else
	{
		if(%prev >= 0)
		{
			%prev = "<a:cmd HowToPlay" SPC %pageNumber[%prev] @ ">" @ 
				%pageNumber[%prev] SPC %pageTitle[%prev] @ "</a>";
		}
		else
			%prev = "";

		if(%next < %n)
		{
			%next = "<a:cmd HowToPlay" SPC %pageNumber[%next] @ ">" @ 
				%pageNumber[%next] SPC %pageTitle[%next] @ "</a>";
		}
		else
			%next = "";

		%fileName = "game/server/eth/" @ %help @ "/" @ %file @ ".rml";
	}

	%nav = "<spush><font:NovaSquare:13>";
	if(%prev !$= "")
		%nav = %nav @ "Prev:" SPC %prev SPC "|";
	%nav = %nav SPC "<a:cmd HowToPlay 0>Index</a>";
	if(%next !$= "")
		%nav = %nav SPC "| Next:" SPC %next;
	%nav = %nav @ "<spop>";

	%newtxt = %newtxt @ %nav @ "\n\n" @
		"<spush><font:NovaSquare:24>" @ %title @ "<spop><just:left>\n\n";

	%file = new FileObject();
	%file.openForRead(%fileName);
	while(!%file.isEOF())
		%newtxt = %newtxt @ strreplace(%file.readLine(), "<br>", "\n") @ "\n";
	%file.delete();

	%newtxt = %newtxt @ "\n<just:center>" @ %nav @ "\n";

	%client.addMenuText(%newtxt);
	%client.endMenuText(%newtxt);

	%client.menu = "howtoplay" @ %page;
}
