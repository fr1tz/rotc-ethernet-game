//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function showMainMenu(%client)
{
	%newtxt = om_init();

	%bg = "";

	%fg = %newtxt @
		om_head(%client, "Arena Info") @
		"<spush><font:NovaSquare:20>" @
		"Welcome to" SPC $Pref::Server::Name @
		"<spop>\n\n" @
		"Game:" SPC $Server::MissionType SPC 
		"\nChangelog: " @
		"https://github.com/fr1tz/rotc-ethernet-game/commits/infantry\n\n" @
//		"Environment:" SPC $Server::MissionName @ "\n\n" @
		"<spush>" @ $Pref::Server::Info @ "<spop>\n\n" @
		"";

	if(%client.loadingMission || %client.menu $= "mainmenu")
	{
		%fg = %fg @
			"If you're playing this arena for the first time, loading" SPC
			"might take\nsome time while the game downloads needed" SPC
			"art from the server.\nConsider using the time to read" SPC
			"the <a:cmd Help>Help section</a>.\n" @
			"";
	}

	%client.beginMenuText(%client.menu $= "mainmenu");
	%client.addMenuText(%fg, 1);
	%client.addMenuText(%bg, 2);
	%client.endMenuText();
}