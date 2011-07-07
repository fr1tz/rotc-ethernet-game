//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function serverCmdHowToPlay(%client, %page)
{
	%newtxt = om_init();
	%client.beginMenuText();

	if(%page $= "")
		%page = 1;

	if($ROTC::GameType == $ROTC::EthernetLight)
	{
		%gametype = "ROTC: Ethernet Light";
		%help = "help-light";
	}
	else
	{
		%gametype = "ROTC: Ethernet";
		%help = "help";	
	}	

	%newtxt = %newtxt @ 
		om_head(%client, "Manual") @
		"<just:center><spush><font:NovaSquare:20>How to play " @ %gametype @ "?<spop>\n\n";

	if($ROTC::GameType == $ROTC::EthernetLight)
	{
		%n = 0;
		%pageNumber[%n] = "0"; 
		%pageTitle[%n] = "Index";
		%pageFile[%n] = "index";
		%n++;
		%pageNumber[%n] = "1"; 
		%pageTitle[%n] = "ROTC: Ethernet Light in a nutshell";
		%pageFile[%n] = "summary";
		%n++;
		%pageNumber[%n] = "2"; 
		%pageTitle[%n] = "Ethernet basics";
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
		%pageTitle[%n] = "Ethernet basics";
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
		%pageTitle[%n] = "B.O.U.N.C.E.";
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
