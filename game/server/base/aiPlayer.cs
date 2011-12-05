//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - aiPlayer.cs
// code for (currently stupid) practice ai opponents
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// AIPlayer callbacks
// The AIPlayer class implements the following callbacks:
//
//	 PlayerData::onStuck(%this,%obj)
//	 PlayerData::onUnStuck(%this,%obj)
//	 PlayerData::onStop(%this,%obj)
//	 PlayerData::onMove(%this,%obj)
//	 PlayerData::onReachDestination(%this,%obj)
//	 PlayerData::onTargetEnterLOS(%this,%obj)
//	 PlayerData::onTargetExitLOS(%this,%obj)
//	 PlayerData::onAdd(%this,%obj)
//
// Since the AIPlayer doesn't implement it's own datablock, these callbacks
// all take place in the PlayerData namespace.
//-----------------------------------------------------------------------------

function aiAdd(%teamid, %weaponNum)
{
	if( !isObject($aiPlayers) )
	{
		$aiPlayers = new Array();
		MissionCleanup.add($aiPlayers);
	}
	
	if( !isObject($aiPlayersPseudoClient) )
	{
		$aiPlayersPseudoClient = new ScriptObject();
		$aiPlayersPseudoClient.handicap = 1.0;
		GameConnection::defaultLoadout($aiPlayersPseudoClient);
		GameConnection::updateLoadout($aiPlayersPseudoClient);
		MissionCleanup.add($aiPlayersPseudoClient);
	}
	

	%nameadd = "_" @ $aiPlayers.count();
	if(isObject($aiPlayers)) {
		%nameadd = "_" @ $aiPlayers.count();
	}

	%spawnSphere = pickSpawnSphere(%teamid);
	
	if($Game::GameType == $Game::GridWars)
	{
		if(%teamid == 1)
			%playerData = RedInfantryCat;
		else
			%playerData = BlueInfantryCat;
	}
	else if($Game::GameType == $Game::Infantry)
	{
		if(%teamid == 1)
			%playerData = RedInfantryCat;
		else
			%playerData = BlueInfantryCat;
	}
	else
	{
		if(%teamid == 1)
			%playerData = RedStandardCat;
		else
			%playerData = BlueStandardCat;
	}

	%player = new AiPlayer() {
		dataBlock = %playerData;
		client = $aiPlayersPseudoClient;
		path = "";
		teamId = %teamid;
	};
	MissionCleanup.add(%player);

	%pos = getRandomHorizontalPos(%spawnSphere.position,%spawnSphere.radius);
	%player.setShapeName("wayne" @ %nameadd);
	%player.setTransform(%pos);

	%player.aiWeapon = %weaponNum;
   if(%weaponNum == 3 || %weaponNum == 4)
   	%player.aiCharge = 1100;
   else
   	%player.aiCharge = 100;

	$aiPlayers.push_back("",%player);
	$aiPlayersPseudoClient.weapons[0] = %weaponNum;
	$aiPlayersPseudoClient.numWeapons = 1;

	%player.useWeapon(1);

	return %player;
}

//-----
// called by user
//-----

function aiAddRed(%weaponNum)
{
	%player = aiAdd(1, %weaponNum);
	return %player;
}

function aiAddBlue(%weaponNum)
{
	%player = aiAdd(2, %weaponNum);
	return %player;
}

function aiStartMove() {
	for( %i = 0; %i < $aiPlayers.count(); %i++ ) {
		xxx_aiStartMove($aiPlayers.getValue(%i));
	}
}
function aiStartFire() {
	for( %i = 0; %i < $aiPlayers.count(); %i++ ) {
		xxx_aiStartFire($aiPlayers.getValue(%i));
	}
}
function aiStartFight() {
	for( %i = 0; %i < $aiPlayers.count(); %i++ ) {
		xxx_aiStartMove($aiPlayers.getValue(%i));
		xxx_aiStartFire($aiPlayers.getValue(%i));
	}
}
function aiKill() {
	for( %i = 0; %i < $aiPlayers.count(); %i++ ) {
		%player = $aiPlayers.getValue(%i);
		%player.kill();
	}
	$aiPlayers.empty();
}

function aiWayneStopFire()
{

}

function aiTransformInto(%what)
{
	for( %i = 0; %i < $aiPlayers.count(); %i++ )
	{
		%player = $aiPlayers.getValue(%i);
		Team2Scout::transformInto(0,%player,%what);
	}
}


//-----
// called non-interactively
//-----

function xxx_aiStartMove(%player)
{
	%player.updateMove = schedule(1000,%player,"xxx_aiUpdateMove",%player);
}

function xxx_aiStartFire(%player)
{
	%player.setImageLoaded(0,true);
	%player.targetUpdateThread = schedule(100,%player,"xxx_aiUpdateTarget",%player);
	%player.fireThread = schedule((getRandom(3)+1)*1000,%player,"xxx_aiFire",%player);
}

function xxx_aiFire(%player)
{
	%target = %player.getAimObject();
	if(isObject(%target))
	{
		%x = 0;
		%y = 0;
		%z = 0;

	  %enemypos = %player.getAimLocation();
	  %mypos = %player.getPosition();
	  %dist = VectorDist(%mypos, %enemypos);

	  %z = %dist/30;

		%offset = %x SPC %y SPC %z;

		%player.setAimObject(%target, %offset);
		%player.setImageTrigger(0,true);
	}
	
	%player.fireReleaseThread = schedule(%player.aiCharge,%player,xxx_aiFireRelease,%player);
}

function xxx_aiFireRelease(%player)
{
	%player.setImageTrigger(0,false);
	%player.fireThread = schedule(getRandom(1000),%player,xxx_aiFire,%player);

    // try to throw disc
	%player.setImageTrigger(3, true);
    %player.fireReleaseThread = %player.schedule(0, setImageTrigger, 3, false);
}

function xxx_aiUpdateTarget(%player)
{
	%target = 0; //$aiTarget;

	%position = %player.getPosition();
	%radius = 500;

	InitContainerRadiusSearch(%position, %radius, $TypeMasks::ShapeBaseObjectType);
	while ((%targetObject = containerSearchNext()) != 0)
	{
		if( %targetObject.teamId > 0
		 && %targetObject.getDamageState() $= "Enabled"
		 && %targetObject.teamId != %player.teamId )
		{
			%target = %targetObject;
			break;
		}
	}
	
	if(%target != 0)
		%player.setAimObject(%target, "0 0 1");
	
	%player.targetUpdateThread = schedule(2500,%player,xxx_aiUpdateTarget,%player);
}

function xxx_aiUpdateMove(%player)
{
	%pos = %player.getPosition();
	%dest = getTerrainLevel(getRandomHorizontalPos(%pos, 50));

	//%heightdiff = getTerrainLevel(%pos) - getTerrainHeight(%desc);
	%heightdiff = getTerrainHeight(getTerrainLevel(%pos)) - getTerrainHeight(%dest);
	%dmg = %heightdiff * %player.getDataBlock().speedDamageScale;
	%dmgbuff = %player.getDataBlock().damageBuffer;

//	if (%heightdiff < 0) {
//		%player.jump();
//	}
	if (%dmg < %dmgbuff) {
		%player.setMoveDestination(%dest);
	}
	%player.moveThread = schedule((getRandom(1)+1)*1000,%player,xxx_aiUpdateMove,%player);
}

