//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
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
package Game {

function displayHelp()
{
	Parent::displayHelp();
	
	error(
		"Ethernet options:\n"@
		"  -dedicated          Start as dedicated server\n"@
		"  -connect <address>  For non-dedicated: Connect to a game at <address>\n" @
		"  -mis <filename>     For dedicated: Load specified mission\n"
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
			case "-mis":
				$argUsed[%i]++;
				if (%hasNextArg) {
					$misArg = %nextArg;
					$argUsed[%i+1]++;
					%i++;
				}
				else
					error("Error: Missing Command Line argument. Usage: -mis <filename>");

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

	// Start up in either client, or dedicated server mode
	if($Server::Dedicated)
	{
		enableWinConsole(true);
		echo("\n--------- Starting Dedicated Server ---------");
		if($misArg $= "")
		{
			error("No mission file (.mis) specified (use -mis <filename>)");
			$misArg = "game/arenas/rotc-ethernet/eth-pond.mis";
			error("Using default mission file" SPC $misArg);
		}
		initBaseServer(); // The common module provides basic server functionality
		createServer("MultiPlayer", $misArg);
	}
	else
	{
		initClient();
	}
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

}; // package Game

activatePackage(Game);
