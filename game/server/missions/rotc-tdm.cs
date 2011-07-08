//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//*******************************************************
// Mission init script for ROTC: Team Deathmatch missions
//*******************************************************

exec("./rotc-types.cs");

$Server::MissionFile = strreplace($Server::MissionFile, 
	"/rotc-team-deathmatch/", "/rotc-ethernet/");

exec("./rotc-eth.cs");

$Server::MissionFile = strreplace($Server::MissionFile, 
	"/rotc-ethernet/", "/rotc-team-deathmatch/");

$ROTC::GameType = $ROTC::TeamDM;
$ROTC::GameTypeString = "ROTC: Team Deathmatch";





		
