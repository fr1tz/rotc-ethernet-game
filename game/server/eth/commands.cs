//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function serverCmdHowToPlay(%client, %page)
{
	%newtxt = "";
	%client.clearMenuText();

	if(%page $= "")
		%page = 1;

	%newtxt = %newtxt @ 
		"<< <a:cmd MainMenu>Main Menu</a>\n\n" @
		"<spush><font:Arial:24>How to play ROTC: Ethernet?<spop>\n\n";

	switch(%page)
	{
		case 1:
			%title = "1. Basics";
			%prev = "";
			%next = "<a:cmd HowToPlay 2>Life as Etherform</a>";
			%fileName = "game/server/eth/help/basics.rml";

		case 2:
			%title = "Playing as Etherform";
			%prev = "<a:cmd HowToPlay 1>Basics</a>";
			%next = "<a:cmd HowToPlay 3>Playing in CAT form</a>";
			%fileName = "game/server/eth/help/etherform.rml";
			break;

		case 3:
			%title = "3. Playing in CAT form";
			%prev = "<a:cmd HowToPlay 2>Playing as Etherform</a>";
			%next = "<a:cmd HowToPlay 4>Weapons</a>";
			%fileName = "game/server/eth/help/cat.rml";
			break;

		case 4:
			%title = "4. Weapons";
			%prev = "<a:cmd HowToPlay 3>Playing in CAT form</a>";
			%next = "";
			%fileName = "game/server/eth/help/weapons.rml";
			break;

		default:
			%title = "0. Index";
			%prev = "";
			%next = "<a:cmd HowToPlay 1>Basics</a>";
			%fileName = "game/server/eth/help/index.rml";
			break;
	}

	%nav = "";
	if(%prev !$= "")
		%nav = %nav @ "Prev:" SPC %prev SPC "|";
	%nav = %nav SPC "<a:cmd HowToPlay 0>Index</a>";
	if(%next !$= "")
		%nav = %nav SPC "| Next:" SPC %next;

	%newtxt = %newtxt @ "<just:center><spush><font:Arial:22>" @ %title @ "<spop>\n" @ %nav @ "<just:left>\n\n";

	%file = new FileObject();
	%file.openForRead(%fileName);
	while(!%file.isEOF())
		%newtxt = %newtxt @ strreplace(%file.readLine(), "<br>", "\n") @ "\n";
	%file.delete();

	%newtxt = %newtxt @ "\n<just:center>" @ %nav @ "\n";

	%client.addMenuText(%newtxt);

	%client.menu = "howtoplay" @ %page;
}
