//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

// Game information used to query the master server
$Client::GameTypeQuery = $GameNameString;
$Client::MissionTypeQuery = "Any";

$Pref::ServerWatcher::UpdateDelay = 10000;

function printServers()
{
	%sc = getServerCount();
	for (%i = 0; %i < %sc; %i++) {
		setServerInfo(%i);
        %prefix = "#server" SPC $ServerInfo::Address;
		echo(%prefix SPC "game_version" SPC $ServerInfo::GameVersion);
		echo(%prefix SPC "name" SPC $ServerInfo::Name);
		echo(%prefix SPC "ping" SPC $ServerInfo::Ping);
		echo(%prefix SPC "player_count" SPC $ServerInfo::PlayerCount @ " of " @ $ServerInfo::MaxPlayers);
		echo(%prefix SPC "map_name" SPC $ServerInfo::MissionName);
		echo(%prefix SPC "map_homepage" SPC $ServerInfo::MissionHomepage);
		echo(%prefix SPC "info" SPC $ServerInfo::Info);
	}
}

function queryServers()
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

    schedule($Pref::ServerWatcher::UpdateDelay, 0, queryServers);
}

function onServerQueryStatus(%status, %msg, %value)
{
    if(%status $= "done")
        printServers();
}

package ServerWatcher {

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

activatePackage(ServerWatcher);
