//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//******************************************************
// Mission init script for ROTC: Ethernet Light missions
//******************************************************

exec("./rotc-types.cs");

$Server::MissionFile = strreplace($Server::MissionFile, 
	"/rotc-ethernet-light/", "/rotc-ethernet/");

exec("./rotc-eth.cs");

$Server::MissionFile = strreplace($Server::MissionFile, 
	"/rotc-ethernet/", "/rotc-ethernet-light/");

$ROTC::GameType = $ROTC::EthernetLight;




		
