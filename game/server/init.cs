//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

// Variables used by server scripts & code.  The ones marked with (c)
// are accessed from code.  Variables preceeded by Pref:: are server
// preferences and stored automatically in the ServerPrefs.cs file
// in between server sessions.
//
//	 (c) Server::ServerType                    {SinglePlayer, MultiPlayer}
//	 (c) Server::GameType                      Unique game name
//	 (c) Server::Dedicated                     Bool
//	 ( ) Server::MissionFile                   Mission .mis file name
//	 ( ) Server::MissionEnvironmentFile        Mission .env file name
//	 (c) Server::MissionName                   Mission name
//	 (c) Server::MissionType                   Mission type
//	 (c) Server::PlayerCount                   Current player count
//	 (c) Server::GuidList                      Player GUID (record list?)
//	 (c) Server::Status                        Current server status
//
//	 (c) Pref::Server::Name	                   Server Name
//	 (c) Pref::Server::Password                Password for client connections
//	 ( ) Pref::Server::AdminPassword           Password for client admins
//	 (c) Pref::Server::Info                    Server description
//	 (c) Pref::Server::MaxPlayers              Max allowed players
//	 (c) Pref::Server::RegionMask              Registers this mask with master server
//	 ( ) Pref::Server::BanTime                 Duration of a player ban
//	 ( ) Pref::Server::KickBanTime             Duration of a player kick & ban
//	 ( ) Pref::Server::MaxChatLen              Max chat message len
//	 ( ) Pref::Server::FloodProtectionEnabled  Bool

//-----------------------------------------------------------------------------

// Specify where the map info files are...
$Server::MissionFileSpec = "*/arenas/*.mis";

//-----------------------------------------------------------------------------

function onServerCreated()
{
	$Server::GameType = $GameNameString; // Server::GameType is sent to the master server.
	$Server::NewbieHelp = true;	
}

function onServerDestroyed()
{
	// nothing here right now
}


