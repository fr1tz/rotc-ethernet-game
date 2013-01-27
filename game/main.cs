//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Load up common script base
loadDir("common");

// Defaults console values
exec("./client/defaults.cs");

// Preferences (overide defaults)
exec("./client/prefs.cs");

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
		"  -srv <filename>     For dedicated: Use specified server settings file\n" @
		"                      (Default: game/server/default.srv)\n" @
		"  -mis <filename>     For dedicated: Load specified mission file instead\n" @
		"                      of the one specified in the server settings file\n"
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
			case "-srv":
				$argUsed[%i]++;
				if (%hasNextArg) {
					$srvArg = %nextArg;
					$argUsed[%i+1]++;
					%i++;
				}
				else
					error("Error: Missing Command Line argument. Usage: -srv <filename>");				

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
		if($srvArg !$= "")
			$Server::SettingsFile = $srvArg;
		else
			$Server::SettingsFile = "./server/default.srv";
		exec($Server::SettingsFile);			

		if($misArg $= "")
		{
			if($Server::MissionFile !$= "")
				$misArg = $Server::MissionFile;
			else
				$misArg = "game/arenas/rotc-ethernet/eth-pond.mis";			
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

	BanList::Export("./server/banlist.cs");

	Parent::onExit();
}

}; // package Game

activatePackage(Game);
