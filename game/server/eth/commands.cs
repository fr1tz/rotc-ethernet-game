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

	%newtxt = %newtxt @ 
		om_head(%client, "Online Help") @
		"<just:center><spush><font:NovaSquare:20>How to play ROTC: Ethernet?<spop>\n\n";

	switch(%page)
	{
		case 1:
			%title = "1. ROTC: Ethernet in a nutshell";
			%prev = "";
			%next = "<a:cmd HowToPlay 2>Basics</a>";
			%fileName = "game/server/eth/help/summary.rml";

		case 2:
			%title = "2. Basics";
			%prev = "<a:cmd HowToPlay 1>ROTC: Ethernet in a nutshell</a>";
			%next = "<a:cmd HowToPlay 3>Playing as Etherform</a>";
			%fileName = "game/server/eth/help/basics.rml";

		case 3:
			%title = "3. Playing as Etherform";
			%prev = "<a:cmd HowToPlay 2>Basics</a>";
			%next = "<a:cmd HowToPlay 4>Playing in CAT form</a>";
			%fileName = "game/server/eth/help/etherform.rml";

		case 4:
			%title = "4. Playing in CAT form";
			%prev = "<a:cmd HowToPlay 3>Playing as Etherform</a>";
			%next = "<a:cmd HowToPlay 5>Weapons</a>";
			%fileName = "game/server/eth/help/cat.rml";

		case 5:
			%title = "5. Weapons";
			%prev = "<a:cmd HowToPlay 4>Playing in CAT form</a>";
			%next = "<a:cmd HowToPlay 6>Controls reference</a>";
			%fileName = "game/server/eth/help/weapons.rml";

		case 6:
			%title = "6. Controls reference";
			%prev = "<a:cmd HowToPlay 5>Weapons</a>";
			%next = "<a:cmd HowToPlay 7>Handicap</a>";
			%fileName = "game/server/eth/help/controls.rml";
			
		case 7:
			%title = "7. Handicap";
			%prev = "<a:cmd HowToPlay 6>Controls reference</a>";
			%next = "";
			%fileName = "game/server/eth/help/handicap.rml";			

		default:
			%title = "0. Index";
			%prev = "";
			%next = "<a:cmd HowToPlay 1>ROTC: Ethernet in a nutshell</a>";
			%fileName = "game/server/eth/help/index.rml";
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
