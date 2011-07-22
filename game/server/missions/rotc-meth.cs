//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//*******************************************************
// Mission init script for ROTC: mEthMatch missions
//*******************************************************

exec("./rotc-types.cs");

$Server::MissionFile = strreplace($Server::MissionFile, 
	"/rotc-methmatch/", "/rotc-ethernet/");

exec("./rotc-eth.cs");

$Server::MissionFile = strreplace($Server::MissionFile, 
	"/rotc-ethernet/", "/rotc-methmatch/");

$ROTC::GameType = $ROTC::mEthMatch;
$ROTC::GameTypeString = "ROTC: mEthMatch";





		
