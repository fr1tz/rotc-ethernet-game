//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - gameconnection.cs
// Sstuff for the objects that represent client connections
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------

function GameConnection::control(%this, %shapebase)
{
	%oldcontrol = %this.getControlObject();
	%newcontrol = %shapebase;
	
	if(%newcontrol.isTransformed)
		%newControl = %newcontrol.transformObj;

	%currTagged = %oldcontrol.getCurrTagged();
	if(%currTagged)
		%newcontrol.setCurrTagged(%currTagged);
	else
	{
		%newcontrol.setCurrTagged(0);
		%newcontrol.setCurrTaggedPos(%oldcontrol.getCurrTaggedPos());
	}
	
	%this.setControlObject(%newcontrol);
}

//------------------------------------------------------------------------------

// *** callback function: called by script code in "common"
function GameConnection::onClientEnterGame(%this)
{
	commandToClient(%this, 'SyncClock', $Sim::Time - $Game::StartTime);
 
    // ScriptObject used to store statistics...
	%this.stats = new ScriptObject()
    {
        healthTaken = 0;
        healthLost = 0;
	};
    %this.score = "- / -";
    %this.updateScoreOnClients();
	
	// "simple control (tm)" info...
	%this.simpleControl = new Array();
	MissionCleanup.add(%this.simpleControl);
	
	//
	// setup observer camera object...
	//
	
	%this.camera = new Camera() {
		dataBlock = ObserverCamera;
	};
	MissionCleanup.add(%this.camera);
	%this.camera.scopeToClient(%this);
		
	%this.camera.setTransform(pickObserverPoint(%this));
	%this.camera.setVelocity("0 0 0");
	
	//
	// join observer team...
	//
	
	%this.joinTeam(0);
}

// *** callback function: called by script code in "common"
function GameConnection::onClientLeaveGame(%this)
{
	%this.team.numPlayers--;

	%this.clearFullControl();
	%this.clearSimpleControl();
 
	if(isObject(%this.stats))
		%this.stats.delete();

	if(isObject(%this.simpleControl))
		%this.simpleControl.delete();
	
	if(isObject(%this.thirdEye))
		%this.thirdEye.delete();

	if(isObject(%this.camera))
		%this.camera.delete();
		
	if(isObject(%this.player))
		%this.player.delete();
}

//------------------------------------------------------------------------------

function GameConnection::joinTeam(%this, %teamId)
{
	if (%teamid > 2 || %teamid < 0)
		return false;

	if( %this.team != 0 && %teamId == %this.team.teamId)
		return false;

	// remove from old team...
	if(%this.team == $Team0)
		$Team0.numPlayers--;
	if(%this.team == $Team1)
		$Team1.numPlayers--;
	if(%this.team == $Team2)
		$Team2.numPlayers--;

	// add client to new team...
	if(%teamId == 0)
	{
		%this.team = $Team0;
		$Team0.numPlayers++;
	}
	if(%teamId == 1)
	{
		%this.team = $Team1;
		$Team1.numPlayers++;
		commandToClient(%this,'SetHudColor',"red");
	}
	if(%teamId == 2)
	{
		%this.team = $Team2;
		$Team2.numPlayers++;
		commandToClient(%this,'SetHudColor',"blue");
	}

	// full and simple control cleanup...
	%this.clearFullControl();
	%this.clearSimpleControl();

	// notify all clients of team change...
	MessageAll('MsgClientJoinTeam', '\c2%1 joined the %2.',
		%this.name,
		%this.team.name,
		%this.team.teamId,
		%this,
		%this.sendGuid,
		%this.score,
		%this.isAiControlled(),
		%this.isAdmin,
		%this.isSuperAdmin);

	%this.spawnPlayer();

	return true;
}

function GameConnection::spawnPlayer(%this)
{
	// remove existing player...
	if(%this.player > 0)
		%this.player.delete();

	// observers have no players...
	if( %this.team == $Team0 )
	{
		%this.setControlObject(%this.camera);
		return;
	}

	%spawnSphere = pickSpawnSphere(%this.team.teamId);
	
	if( %this.team == $Team1 )
		%data = RedEtherform;
	else
		%data = BlueEtherform;

	%obj = new Etherform() {
		dataBlock = %data;
		client = %this;
		teamId = %this.team.teamId;
	};

	// player setup...
	%pos = getRandomHorizontalPos(%spawnSphere.position, %spawnSphere.radius);
	%obj.setTransform(%pos);
	%obj.useWeapon(1);
	%obj.setCurrTagged(0);
	%obj.setCurrTaggedPos("0 0 0");

	// update the client's observer camera to start with the player...
	%this.camera.setMode("Observer");
	%this.camera.setTransform(%this.player.getEyeTransform());

	// give the client control of the player...
	%this.player = %obj;
	%this.setControlObject(%obj);
}

//-----------------------------------------------------------------------------

function GameConnection::togglePlayerForm(%this)
{
	if(!isObject(%this.player))
		return;
		
	%tagged = %this.player.isTagged();
	%pos = %this.player.getWorldBoxCenter();

	if(%this.player.getClassName() $= "Player")
	{
		// CAT -> etherform
	
		if( %this.team == $Team1 )
			%data = RedEtherform;
		else
			%data = BlueEtherform;

		%obj = new Etherform() {
			dataBlock = %data;
			client = %this;
			teamId = %this.team.teamId;
		};
	}
	else
	{
		// etherform -> CAT
		
		if(%this.player.getDamageLevel() > %this.player.getDataBlock().maxDamage*0.75)
		{
			bottomPrint(%this, "You need at least 25% health to manifest!", 3, 1 );
			return;
		}
		
		if(%this.player.getEnergyLevel() < 50)
		{
			bottomPrint(%this, "You need 50% energy to manifest!", 3, 1 );
			return;
		}
		
		%ownTeamId = %this.player.getTeamId();

		%inOwnZone = false;
		%inEnemyZone = false;

		InitContainerRadiusSearch(%pos, 0.0001, $TypeMasks::TacticalZoneObjectType);
		while((%srchObj = containerSearchNext()) != 0)
		{
			%zoneTeamId = %srchObj.getTeamId();

			if(%zoneTeamId != %ownTeamId && %zoneTeamId != 0)
			{
				%inEnemyZone = true;
				break;
			}
			else if(%zoneTeamId == %ownTeamId)
			{
				%inOwnZone = true;
			}
		}

		if(%inEnemyZone)
		{
			bottomPrint(%this, "You can not manifest in an enemy zone!", 3, 1 );
			return;
		}
		else if(!%inOwnZone)
		{
			bottomPrint(%this, "You can only manifest in your team's zones!", 3, 1 );
			return;
		}

		if( %this.team == $Team1 )
			%data = RedStandardCat;
		else
			%data = BlueStandardCat;

		%obj = new Player() {
			dataBlock = %data;
			client = %this;
			teamId = %this.team.teamId;
		};

		$aiTarget = %obj;
	}
	
	MissionCleanup.add(%obj);
	
	%mat = %this.player.getTransform();
	%dmg = %this.player.getDamageLevel();
	%nrg = %this.player.getEnergyLevel();
	%buf = %this.player.getDamageBufferLevel();
	%vel = %this.player.getVelocity();

	%obj.setTransform(%mat);
	%obj.setTransform(%pos);
	%obj.setDamageLevel(%dmg);
	%obj.setDamageBufferLevel(%buf);
	
	if(%tagged)
		%obj.setTagged();

	%this.control(%obj);
	
	if(%obj.getType() & $TypeMasks::PlayerObjectType)
	{
		// etherform -> CAT
		
		// remove any z-velocity...
		%vel = getWord(%vel, 0) SPC getWord(%vel, 1) SPC "0";
	
		%this.player.delete();

		%obj.setEnergyLevel(%nrg);
		%obj.setVelocity(VectorScale(%vel, 0.25));
		
		%obj.startFade(1000,0,false);
		%obj.playAudio(0, CatSpawnSound);
	}
	else
	{
		// CAT -> etherform
	
		if(%this.player.getDamageState() $= "Enabled")
		{
			//if(%this.player.getDataBlock().damageBuffer - %buf > 1)
			//{
			//	%this.player.setDamageState("Disabled");
			//	%this.player.playDeathAnimation(0, 0);
			//}
			//else
			//{
				%this.player.setDamageState("Destroyed");
			//}
		}

		%obj.setEnergyLevel(%nrg - 25);
		%obj.applyImpulse(%pos, VectorScale(%vel,100));
		%obj.playAudio(0, EtherformSpawnSound);
	}

	%this.player = %obj;
}
