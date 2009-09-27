//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Load up common script base
loadDir("common");

// Defaults console values
exec("./client/defaults.cs");
exec("./server/defaults.cs");

// Preferences (overide defaults)
exec("./client/prefs.cs");
exec("./server/prefs.cs");

//-----------------------------------------------------------------------------
// Package overrides to initialize the mod.
package ethernet {

function displayHelp()
{
	Parent::displayHelp();
	
	error(
		"Ethernet options:\n"@
		"  -dedicated          Start as dedicated server\n"@
		"  -connect <address>  For non-dedicated: Connect to a game at <address>\n" @
		"  -map <filename>     For dedicated: Load specified map\n"
	);
}

function parseArgs()
{
	Parent::parseArgs();

	// Arguments, which override everything else.
	for (%i = 1; %i < $Game::argc ; %i++)
	{
		%arg = $Game::argv[%i];
		%nextArg = $Game::argv[%i+1];
		%hasNextArg = $Game::argc - %i > 1;
	
		switch$ (%arg)
		{
			//--------------------
			case "-dedicated":
				$Server::Dedicated = true;
				enableWinConsole(true);
				$argUsed[%i]++;

			//--------------------
			case "-map":
				$argUsed[%i]++;
				if (%hasNextArg) {
					$mapArg = %nextArg;
					$argUsed[%i+1]++;
					%i++;
				}
				else
					error("Error: Missing Command Line argument. Usage: -mission <filename>");

			//--------------------
			case "-connect":
				$argUsed[%i]++;
				if (%hasNextArg) {
					$JoinGameAddress = %nextArg;
					$argUsed[%i+1]++;
					%i++;
				}
				else
					error("Error: Missing Command Line argument. Usage: -connect <ip_address>");
		}
	}
}

function onStart()
{
	Parent::onStart();
	
	echo("\n--------- Initializing MOD: Ethernet ---------");

	// Load the scripts that start it all...
	exec("./client/init.cs");
	exec("./server/init.cs");
	exec("./data/init.cs");

	// Server gets loaded for all sessions, since clients
	// can host in-game servers.
	initServer();

	// Start up in either client, or dedicated server mode
	if ($Server::Dedicated)
		initDedicated();
	else
		initClient();
}

function onExit()
{
	echo("Exporting client prefs");
	export("$pref::*", "./client/prefs.cs", False);
	
	echo("Exporting client config");
	if (isObject(moveMap))
		moveMap.save("./client/config.cs", false);

	echo("Exporting server prefs");
	export("$Pref::Server::*", "./server/prefs.cs", False);
	BanList::Export("./server/banlist.cs");

	Parent::onExit();
}

}; // Client package
activatePackage(ethernet);
