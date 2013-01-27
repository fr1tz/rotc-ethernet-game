//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

// Game information used to query the master server
$Client::GameTypeQuery = $GameNameString;
$Client::MissionTypeQuery = "Any";

function printServers()
{
	%sc = getServerCount();
	for (%i = 0; %i < %sc; %i++) {
		setServerInfo(%i);
        %prefix = "#server" SPC $ServerInfo::Address;
		echo(%prefix SPC "arena" SPC $ServerInfo::MissionType SPC
			$ServerInfo::MissionName);
		echo(%prefix SPC "ping" SPC $ServerInfo::Ping);
		echo(%prefix SPC "player_count" SPC $ServerInfo::PlayerCount @ " of " @ $ServerInfo::MaxPlayers);
		echo(%prefix SPC "hoster" SPC $ServerInfo::Name);
		echo(%prefix SPC "description" SPC $ServerInfo::Info);
	}
}

function queryServers()
{
	if($lan)
	{
		queryLANServers(
			28000,		// lanPort for local queries
			0,			 // Query flags
			$Client::GameTypeQuery,		 // gameTypes
			$Client::MissionTypeQuery,	 // missionType
			0,			 // minPlayers
			100,		  // maxPlayers
			0,			 // maxBots
			2,			 // regionMask
			0,			 // maxPing
			100,		  // minCPU
			0			  // filterFlags
		);
	}
	else
	{
		queryMasterServer(
			0,			 // Query flags
			$Client::GameTypeQuery,		 // gameTypes
			$Client::MissionTypeQuery,	 // missionType
			0,			 // minPlayers
			100,		  // maxPlayers
			0,			 // maxBots
			2,			 // regionMask
			0,			 // maxPing
			100,		  // minCPU
			0			  // filterFlags
		);
	}
}

function onServerQueryStatus(%status, %msg, %value)
{
    if(%status $= "done")
    {
        printServers();
        quit();
    }
}

package ServerList {

function displayHelp() {
	Parent::displayHelp();
	error(
		"Serverlist Mod options:\n"@
		"  -lan                Query lan servers\n"
	);
}

function parseArgs()
{
	Parent::parseArgs();

	$lan = false;

	for (%i = 1; %i < $Game::argc ; %i++)
	{
		%arg = $Game::argv[%i];
		%nextArg = $Game::argv[%i+1];
		%hasNextArg = $Game::argc - %i > 1;
	
		switch$ (%arg)
		{
			case "-lan":
				$lan = true;
		}
	}
}

function onStart()
{
    enableWinConsole(true);
    setNetPort(0);
    schedule(0, 0, queryServers);
}

function onExit()
{

}

}; // Client package

activatePackage(ServerList);
