//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Server side of the scripted mission info system.
//------------------------------------------------------------------------------

function MissionInfo::load(%missionFile)
{
	$MissionInfo::File = %missionFile;

	$MissionInfo::Name        = "";
	$MissionInfo::Desc        = "";
	$MissionInfo::Type        = "";
	$MissionInfo::TypeDesc    = "";
	$MissionInfo::MutatorDesc = "";
	$MissionInfo::InitScript  = "";

	exec(%missionFile);
}

