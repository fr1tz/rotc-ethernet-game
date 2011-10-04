//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Mission Loading
// Server download handshaking.  This produces a number of onPhaseX
// calls so the game scripts can update the game's GUI.
//
// Loading Phases:
// Phase 1: Download Datablocks
// Phase 2: Download Ghost Objects
// Phase 3: TacticalZones grid computation / Scene Lighting
//----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Phase 1 
//----------------------------------------------------------------------------

function clientCmdMissionStartPhase1(%seq, %missionEnvFile, %musicTrack)
{
	$Client::MissionEnvironmentFile = %missionEnvFile;

	// These need to come after the cls.
	echo ("*** New Mission: " @ %missionEnvFile);
	echo ("*** Phase 1: Download Datablocks & Targets");
	onMissionDownloadPhase1(%missionEnvFile, %musicTrack);
	commandToServer('MissionStartPhase1Ack', %seq);
}

function onDataBlockObjectReceived(%index, %total)
{
	onPhase1Progress(%index / %total);
}

//----------------------------------------------------------------------------
// Phase 2
//----------------------------------------------------------------------------

function clientCmdMissionStartPhase2(%seq,%missionName)
{
	onPhase1Complete();
	echo ("*** Phase 2: Download Ghost Objects");
	purgeResources();
	onMissionDownloadPhase2(%missionName);
	commandToServer('MissionStartPhase2Ack', %seq);
}

function onGhostAlwaysStarted(%ghostCount)
{
	$ghostCount = %ghostCount;
	$ghostsRecvd = 0;
}

function onGhostAlwaysObjectReceived()
{
	$ghostsRecvd++;
	onPhase2Progress($ghostsRecvd / $ghostCount);

    // HACK HACK HACK: The client should really have a cleaner
    // way of accessing the sky object.
    $sky = 0;
    for(%idx = 0; %idx < ServerConnection.getCount(); %idx++)
    {
		%obj = ServerConnection.getObject(%idx);
        if(%obj.getClassName() $= "Sky")
        {
            $sky = %obj;
            break;
        }
    }
}

//----------------------------------------------------------------------------
// Phase 3
//----------------------------------------------------------------------------

function clientCmdMissionStartPhase3(%seq,%missionName)
{
	onPhase2Complete();
 
 	echo ("*** Phase 3: Shape replication");
  	onPhase3Progress("SHAPE REPLICATION", 0);
	StartClientReplication();
  	onPhase3Progress("SHAPE REPLICATION", 1);
   
 	echo ("*** Phase 3: Foliage replication");
   	onPhase3Progress("FOLIAGE REPLICATION", 0);
	StartFoliageReplication();
   	onPhase3Progress("FOLIAGE REPLICATION", 1);
    
 	echo ("*** Phase 3: TacticalZone grid computation");
   	onPhase3Progress("ZONE GRID COMPUTATION", 0);
    computeZoneGrids("updateZoneGridProgress");
   	onPhase3Progress("ZONE GRID COMPUTATION", 1);
  
	echo ("*** Phase 3: Mission Lighting");
	$MSeq = %seq;

	// Need to light the mission before we are ready.
	// The sceneLightingComplete function will complete the handshake 
	// once the scene lighting is done.
	if (lightScene("sceneLightingComplete", ""))
	{
		error("Lighting mission....");
		schedule(1, 0, "updateLightingProgress");
		onMissionDownloadPhase3(%missionName);
		$lightingMission = true;
	}
}

function updateZoneGridProgress(%progress)
{
    onPhase3Progress("ZONE GRID COMPUTATION", %progress);
    Canvas.repaint();
}

function updateLightingProgress()
{
	onPhase3Progress("LIGHTING MISSION", $SceneLighting::lightingProgress);
	if ($lightingMission)
		$lightingProgressThread = schedule(1, 0, "updateLightingProgress");
}

function sceneLightingComplete()
{
	echo("Mission lighting done");
	onPhase3Complete();
	
	// The is also the end of the mission load cycle.
	onMissionDownloadComplete();
	commandToServer('MissionStartPhase3Ack', $MSeq);
}

//----------------------------------------------------------------------------
// Helper functions
//----------------------------------------------------------------------------

function connect(%server)
{
   disconnect();
	%conn = new GameConnection();
	%conn.setConnectArgs($GameNameString, $GameVersionString, $Pref::Player::Name);
	%conn.setJoinPassword($Client::Password);
	%conn.connect(%server);
	onConnectionInitiated();
}
