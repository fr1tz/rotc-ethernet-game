//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

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
		
		if(%this.player.getDamageLevel() >= %this.player.getDataBlock().maxDamage)
		{
			bottomPrint(%this, "You need health to manifest!", 3, 1 );
			return;
		}
		
		InitContainerRadiusSearch(%pos, 0.0001, $TypeMasks::TacticalZoneObjectType);
		if(containerSearchNext() == 0)
		{
			bottomPrint(%this, "Can only manifest inside zones!", 3, 1 );
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

		%obj.setEnergyLevel(%nrg - 50);
		%obj.applyImpulse(%pos, VectorScale(%vel,100));
		%obj.playAudio(0, EtherformSpawnSound);
	}

	%this.player = %obj;
}

function TerritoryZone::updateOwner(%this, %zone)
{
	if(%zone.numReds != 0 && %zone.numBlues == 0)
	{
		%this.setZoneOwner(%zone, 1);
	}
	else if(%zone.numBlues != 0 && %zone.numReds == 0)
	{
		%this.setZoneOwner(%zone, 2);
	}
	else
	{
		%this.setZoneOwner(%zone, 0);
	}
}

function EtherformData::damage(%this, %obj, %sourceObject, %pos, %damage, %damageType)
{
    if(%damageType $= "EtherformFade")
        Parent::damage(%this, %obj, %sourceObject, %pos, %damage, %damageType);
}

function EtherformData::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);

	// start singing your swan song...
	%obj.playAudio(1, EtherformSingSound);
    %obj.setDamageDt(1, "EtherformFade");
}

function checkRoundEnd()
{
	if($Game::RoundRestarting)
		return;

    return;

	if($Team1.numTerritoryZones == 0 && $Team1.numCATs == 0)
	{
		centerPrintAll($Team2.name @ " have won!",3);
		serverPlay2D(BlueVictorySound);
		schedule(5000,0,"startNewRound");
		$Game::RoundRestarting = true;
	}
	else if($Team2.numTerritoryZones == 0 && $Team2.numCATs == 0)
	{
		centerPrintAll($Team1.name @ " have won!",3);
		serverPlay2D(RedVictorySound);
		schedule(5000,0,"startNewRound");
		$Game::RoundRestarting = true;
	}
}
