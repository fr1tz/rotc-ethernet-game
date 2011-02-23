//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Server mission loading
//-----------------------------------------------------------------------------

// On every mission load except the first, there is a pause after
// the initial mission info is downloaded to the client.
$MissionLoadPause = 5000;

//-----------------------------------------------------------------------------

function loadMission(%missionFile, %isFirstMission)
{
	endMission();
	
	echo("*** LOADING MISSION: " @ %missionFile);
	echo("*** Stage 1 load");
	
	// Reset all of these
	clearCenterPrintAll();
	clearBottomPrintAll();

	// increment the mission sequence (used for ghost sequencing)
	$missionSequence++;
	$missionRunning = false;

	MissionInfo::load(%missionFile);
	
	// set server vars...
	$Server::MissionFile = %missionFile;
	if(getSubStr($MissionInfo::EnvFile, 0, 2) $= "./")
		$Server::MissionEnvironmentFile = filePath($MissionInfo::File) @ "/" @ $MissionInfo::EnvFile;
	else
		$Server::MissionEnvironmentFile = $MissionInfo::EnvFile;
	$Server::MissionName = $MissionInfo::Name;
	$Server::MissionType = $MissionInfo::Type;

	exec($MissionInfo::ScriptFile);

	initMission();
	
	// if this isn't the first mission, allow some time for the server
	// to transmit information to the clients:
	if( %isFirstMission || $Server::ServerType $= "SinglePlayer" )
		loadMissionStage2();
	else
		schedule( $MissionLoadPause, ServerGroup, loadMissionStage2 );
}

//-----------------------------------------------------------------------------

function loadMissionStage2() 
{
	// Create the mission group off the ServerGroup
	echo("*** Stage 2 load");
	$instantGroup = ServerGroup;

	// Make sure the mission environment exists
	%file = $Server::MissionEnvironmentFile;
	echo("Loading mission environment file:" SPC %file);
	if( !isFile( %file ) ) {
		error( "Could not find mission environment " @ %file );
		return;
	}

	// Calculate the mission CRC.  The CRC is used by the clients
	// to caching mission lighting.
	$missionCRC = getFileCRC( %file );

	// Exec the mission environment, objects are added to the ServerGroup
	exec(%file);
	
	// If there was a problem with the load, let's try another mission
	if( !isObject(MissionEnvironment) ) {
		error( "No 'MissionEnvironment' found in file \"" @ %file @ "\"." );
		schedule( 3000, ServerGroup, CycleMissions );
		return;
	}

	// Mission cleanup group
	new SimGroup( MissionCleanup );
	$instantGroup = MissionCleanup;
	
	// Construct MOD paths
	pathOnMissionLoadDone();

	// Mission loading done...
	echo("*** Mission loaded");
	
	// Start all the clients in the mission
	$missionRunning = true;
	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
		ClientGroup.getObject(%clientIndex).loadMission();

	// Go ahead and launch the game
	onMissionLoaded();
	purgeResources();
}


//-----------------------------------------------------------------------------

function endMission()
{
	if (!isObject(MissionEnvironment))
		return;

	echo("*** ENDING MISSION");
	
	// Inform the game code we're done.
	onMissionEnded();

	// Inform the clients
	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
		// clear ghosts and paths from all clients
		%cl = ClientGroup.getObject( %clientIndex );
		%cl.endMission();
		%cl.resetGhosting();
		%cl.clearPaths();
	}
	
	// Delete everything
	MissionEnvironment.delete();
	MissionCleanup.delete();

	$ServerGroup.delete();
	$ServerGroup = new SimGroup(ServerGroup);
}


//-----------------------------------------------------------------------------

function resetMission()
{
	echo("*** MISSION RESET");

	// Remove any temporary mission objects
	MissionCleanup.delete();
	$instantGroup = ServerGroup;
	new SimGroup( MissionCleanup );
	$instantGroup = MissionCleanup;

	//
	onMissionReset();
}
