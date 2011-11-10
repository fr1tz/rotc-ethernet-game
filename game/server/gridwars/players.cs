//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function PlayerData::onTrigger(%this, %obj, %triggerNum, %val)
{
	// This method is invoked when the player receives a trigger
	// move event.  The player automatically triggers slot 0 and
	// slot 1 off of triggers # 0 & 1.  Trigger # 2 is also used
	// as the jump key.
	
	//--------------------------------------------------------------------------
	// Primary fire
	//--------------------------------------------------------------------------
	if( %triggerNum == 0 )
	{

	}

	//--------------------------------------------------------------------------
	// Jump
	//--------------------------------------------------------------------------
	if( %triggerNum == 2 )
	{
        if(%val)
        {
            %obj.schedule(0, "checkReJump");
        }
	}
	
	//--------------------------------------------------------------------------
	// Grenade
	//--------------------------------------------------------------------------
	if( %triggerNum == 3 )
	{
		%obj.setImageTrigger(2, %val);
	}

	//--------------------------------------------------------------------------
	// Marching & Sliding
	//--------------------------------------------------------------------------
	if( %triggerNum == 4 || %triggerNum == 5 )
	{
		if(%val)
        {
            if(%triggerNum == 4)
    			%obj.nonSnipingBodyPose = $PlayerBodyPose::Marching;
            else if(%triggerNum == 5)
    			%obj.nonSnipingBodyPose = $PlayerBodyPose::Crouched;
        }
		else
			%obj.nonSnipingBodyPose = $PlayerBodyPose::Standard;
 
        if(!%obj.isSniping)
            %obj.setBodyPose(%obj.nonSnipingBodyPose);
	}
}

//-----------------------------------------------------------------------------

function Player::setSniping(%this, %sniping)
{
    %this.isSniping = %sniping;
    if(%this.isSniping)
        %this.setBodyPose($PlayerBodyPose::Marching);
    else
        %this.setBodyPose(%this.nonSnipingBodyPose);
}

//-----------------------------------------------------------------------------

function Player::updateSliding(%this)
{
    %dtTime = 50;
    
    //%warnLevel = 0.15;
    //%hotLevel = 0.5;

    %slidingingDt = -0.01;
    %coolingDt = 0.02;
    %cooldownDelay = 1.0;

    if(%this.slidingThread !$= "")
        cancel(%this.slidingThread);
        
    %store   = %this.sliding;
    %storeDt = %this.slidingDt;

    if(%this.getBodyPose() == $PlayerBodyPose::Sliding)
    {
        %this.slidingDt = %slidingingDt;
        %this.slidingCooldownTime = %cooldownDelay;
    }
    else
    {
        %this.slidingCooldownTime -= %dtTime / 1000;
        if(%this.slidingCooldownTime >= 0)
            %this.slidingDt = 0;
        else
            %this.slidingDt = %coolingDt;
    }
    
    %this.sliding += %this.slidingDt;
    if(%this.sliding > 1)
    {
        %this.sliding = 1;
        %this.slidingDt = 0;
    }
    else if(%this.sliding <= 0)
    {
        %this.sliding = 0;
        %this.slidingDt = 0;
        %this.setBodyPose($PlayerBodyPose::Normal);
    }
    
    if(%this.client && %this.slidingDt !$= %storeDt)
       	messageClient(%this.client, 'MsgHeat', "", %this.sliding, %this.slidingDt);

    %this.slidingThread = %this.schedule(%dtTime, "updateSliding");
}

//-----------------------------------------------------------------------------

function Player::numAttackingDiscs(%this)
{
    return %this.attackingDiscs;
}

function Player::addAttackingDisc(%this, %disc)
{
    %this.attackingDiscs += 1;
    
    %this.shapeFxSetTexture($PlayerShapeFxSlot::NoDisc, 2);
    %this.shapeFxSetBalloon($PlayerShapeFxSlot::NoDisc, 1.10);
    %this.shapeFxSetActive($PlayerShapeFxSlot::NoDisc, true, true);
}

function Player::removeAttackingDisc(%this, %disc)
{
    %this.attackingDiscs -= 1;
    
    if(%this.attackingDiscs == 0)
        %this.shapeFxSetActive($PlayerShapeFxSlot::NoDisc, false, false);
}

//-----------------------------------------------------------------------------

function Player::inNoDiscGracePeriod(%this)
{
    return %this.noDiscGracePeriod;
}

function Player::startNoDiscGracePeriod(%this)
{
    if(%this.endDiscGracePeriodThread !$= "")
        cancel(%this.endDiscGracePeriodThread);

    %gracePeriodTime = 3.0;

    %this.noDiscGracePeriod = true;

    %this.shapeFxSetTexture($PlayerShapeFxSlot::NoDisc, 2);
    %this.shapeFxSetFade($PlayerShapeFxSlot::NoDisc, 1.0, -1/%gracePeriodTime);
    %this.shapeFxSetActive($PlayerShapeFxSlot::NoDisc, true, true);

    %this.endDiscGracePeriodThread =
        %this.schedule(%gracePeriodTime*1000, "endNoDiscGracePeriod");
}

function Player::endNoDiscGracePeriod(%this)
{
    %this.noDiscGracePeriod = false;
    %this.shapeFxSetActive($PlayerShapeFxSlot::NoDisc, false, false);
}

//-----------------------------------------------------------------------------

function Player::checkReJump(%this)
{
    %this.getDataBlock().checkReJump(%this);
}

function PlayerData::checkReJump(%this, %obj)
{
    if(%obj.lastJumpTime != getSimTime() && %obj.getEnergyLevel() > %this.reJumpEnergyDrain)
    {
       	createExplosion(CatJumpExplosion, %obj.getPosition(), "0 0 1");
  		%impulseVec = VectorScale("0 0 1", %this.reJumpForce);
  		%obj.applyImpulse(%pos, %impulseVec);
        %obj.setEnergyLevel( %obj.getEnergyLevel() -  %this.reJumpEnergyDrain);
    }
}

//-----------------------------------------------------------------------------

function PlayerData::onJump(%this, %obj)
{
    %obj.lastJumpTime = getSimTime();
}

//-----------------------------------------------------------------------------

function PlayerData::transformInto(%this,%obj,%into)
{
	%client = %obj.client;
	%success = false;

	//
	// transform into flyer
	//
	if( %into == 1 )
	{
		// need some energy in order to transform...
		if( %obj.getEnergyLevel() < 75 )
			return false;

		%pos = %obj.getTransform();
		%vel = %obj.getVelocity();
		%dmg = %obj.getDamageLevel();

		//%obj.setInvisible(true);

		%data = RedFlyer;

		%newObj = new FlyingVehicle()
		{
			datablock = %data;
			client = %client;
			teamId = %client.team.teamId;
		};

		%effect = new FXShape()
		{
			datablock = Team1ScoutToFlyerEffect;
		};
		%effect.linkToObject(%newObj);

		%newObj.setEnergyLevel($PLAYER_MAX_ENERGY/2);
		%newObj.setDamageLevel(%dmg);
		%newObj.setShapeName(%client.name);

//		$aiTarget = %newObj;

		%newObj.setTransform(%pos);
		%newObj.applyImpulse(%pos, VectorScale(%vel,100));

		MissionCleanup.add(%newObj);

		%obj.setControlObject(%newObj);
//		%client.player = %newObj;

		// create a cool(tm) effect...
		%newObj.startFade(100,900,false);
		%effect.schedule(1000,"delete");
		%newObj.playAudio(0,TransformSound);

		%success = true;
	}

	return %success;
}

//-----------------------------------------------------------------------------
// Player object methods
//-----------------------------------------------------------------------------

function Player::hasDisc(%this)
{
	return %this.numDiscs > 0;
}

function Player::setDiscs(%this, %numDiscs)
{
	%this.numDiscs = %numDiscs;

	if(isObject(%this.client))
		messageClient(%this.client, 'MsgNumDiscs', "", %this.numDiscs);

	%hasDisc = %this.hasDisc();
	%this.setImageLoaded(1, %hasDisc);
	%this.setImageAmmo(1, %hasDisc);
}

function Player::incDiscs(%this)
{
	%this.numDiscs++;
	%this.setDiscs(%this.numDiscs);
}

function Player::decDiscs(%this)
{
	%this.numDiscs--;
	%this.setDiscs(%this.numDiscs);
}

function Player::hasGrenade(%this)
{
	return %this.grenadeAmmo >= 1;
}

function Player::incGrenadeAmmo(%this, %amount)
{
    %this.setGrenadeAmmo(%this.grenadeAmmo + %amount);
}

function Player::decGrenadeAmmo(%this, %amount)
{
    %this.setGrenadeAmmo(%this.grenadeAmmo - %amount);
}

function Player::setGrenadeAmmo(%this, %amount)
{
    %store = %this.grenadeAmmo;

    if(%amount < 0)
        %this.grenadeAmmo = 0;
    else if(%amount > 1)
        %this.grenadeAmmo = 1;
    else
        %this.grenadeAmmo = %amount;

    if(%this.grenadeAmmo != %store)
    {
        if(isObject(%this.client))
            messageClient(%this.client, 'MsgGrenadeAmmo', "", %this.grenadeAmmo);
    }

	%this.setImageAmmo(2, %this.hasGrenade());
}

function Player::setGrenadeAmmoDt(%this, %amount)
{
    %this.grenadeAmmoDt = %amount;

    if(isObject(%this.client))
        messageClient(%this.client, 'MsgGrenadeAmmoDt', "", 250, %amount);

    %this.updateGrenadeAmmo();
}

function Player::updateGrenadeAmmo(%this)
{
	if(%this.grenadeAmmoThread !$= "")
    {
        cancel(%this.grenadeAmmoThread);
		%this.grenadeAmmoThread = "";
	}
 
    %this.grenadeAmmo += %this.grenadeAmmoDt/1000 * 250;
    
    if(%this.grenadeAmmo < 0)
        %this.grenadeAmmo = 0;
    else if(%this.grenadeAmmo > 1)
        %this.grenadeAmmo = 1;

	%this.setImageAmmo(2, %this.hasGrenade());
 
    %this.grenadeAmmoThread = %this.schedule(250, "updateGrenadeAmmo");
}

function Player::mountVehicle(%this, %vehicle)
{
	%vehicleData = %vehicle.getDataBlock();

	// Is the vehicle mountable?
	if(%vehicleData.mountable == false)
	{
		echo("Sorry, the vehicle is not mountable.");
		return;
	}

	// Check the speed of the vehicle. If it's moving, we can't mount it.
	// Note there is a threshold that determines if the vehicle is moving.
	%vel = %vehicle.getVelocity();
	%speed = vectorLen(%vel);

	if(%speed <= %vehicleData.maxMountSpeed)
	{
		// Find an empty seat
		%seat = %vehicle.findEmptySeat();
		if(%seat == -1)
		{
			echo("No free seat available");
			return;
		}

		%this.mVehicle = %col;
		%this.mSeat = %seat;
		%this.isMounted = true;

		echo("Mounting vehicle in seat " @ %seat);

		// Now mount the vehicle.
		%vehicle.mountObject(%this, %seat);
		%this.setTransform(%vehicle.getSlotTransform(%seat));
	}
	else
	{
		echo("You cannot mount a moving vehicle.");
	}
}

function Player::dismountVehicle(%this)
{
	if(!%this.isMounted())
		return;

	%vehicle = %this.getObjectMount();

	%speed = vectorLen(%vehicle.getVelocity());
	if(%speed > %vehicle.getDataBlock().maxDismountSpeed)
		return;

	// Find the position above dismount point.
	%pos	 = getWords(%this.getTransform(), 0, 2);
	%oldPos = %pos;
	%vec[0] = " 2  0  0";
	%vec[1] = " 0  -2  0";
	%vec[2] = " 0  2  0";
	%vec[3] = "-2  0  0";
	%vec[4] = " 4  0  0";
	%impulseVec  = "0 0 0";
	%vec[0] = MatrixMulVector( %this.getTransform(), %vec[0]);

	// Make sure the point is valid
	%pos = "0 0 0";
	%numAttempts = 5;
	%success	  = -1;
	for (%i = 0; %i < %numAttempts; %i++) {
		%pos = VectorAdd(%oldPos, VectorScale(%vec[%i], 3));
		if(%this.checkDismountPoint(%oldPos, %pos)) {
			%success = %i;
			%impulseVec = %vec[%i];
			break;
		}
	}

	if(%forced && %success == -1)
		%pos = %oldPos;

	%this.unmount();
	%this.mountVehicle = false;

	// Schedule the function to set the mount flag, so that the player
	// can mount another vehicle in the future.
	%this.schedule(4000, "MountVehicles", true);

	// Position above dismount point
	%this.setTransform(%pos);
	%this.applyImpulse(%pos, VectorScale(%impulseVec, %this.getDataBlock().mass));

	%this.setActionThread("run",true,true);
	%this.setArmThread("look");

	%this.client.setControlObject(%this);

	// Command the client to display the correct movement map and
	// activate the command menu.
//	CommandToClient(%obj.client, 'PopActionMap', $Vehicle::moveMaps[%obj.mSeat]);
//	CommandToClient(%obj.client, 'PushActionMap', moveMap);
//	CommandToClient(%obj.client,'activateCommandMenu');
}

function Player::kill(%this, %damageType)
{
	%this.damage(0, %this.getPosition(), 10000, %damageType);
}

//----------------------------------------------------------------------------

function Player::isPilot(%this)
{
	%vehicle = %this.getObjectMount();
	// There are two "if" statements to avoid a script warning.
	if (%vehicle)
		if (%vehicle.getMountNodeObject(0) == %this)
			return true;
	return false;
}

//----------------------------------------------------------------------------

function Player::playDeathCry( %this )
{
	%client = %this.client;
	//playTargetAudio( %client.target, "replaceme", AudioClosest3d, false );
}

function Player::playPain( %this )
{
	%client = %this.client;
	//playTargetAudio( %client.target, "replaceme", AudioClosest3d, false);
}




//----------------------------------------------------------------------------

function Player::kill(%this, %damageType)
{
	%this.damage(0, %this.getPosition(), 10000, %damageType);
}


//----------------------------------------------------------------------------

function Player::mountVehicles(%this,%bool)
{
	// If set to false, this variable disables vehicle mounting.
	%this.mountVehicle = %bool;
}

function Player::isPilot(%this)
{
	%vehicle = %this.getObjectMount();
	// There are two "if" statements to avoid a script warning.
	if (%vehicle)
		if (%vehicle.getMountNodeObject(0) == %this)
			return true;
	return false;
}


//----------------------------------------------------------------------------

function Player::playDeathAnimation(%this, %damageLocation, %damageType)
{
	%vertPos =  getWord(%damageLocation, 0);
	%quadrant = getWord(%damageLocation, 1);

	//echo("damagetype: " @ %damageType);
	//echo("location: " @ %damageLocation);
	//echo("vert Pos: " @ %vertPos);
	//echo("quad: " @ %quadrant);

	if( %damageType == $DamageType::Splash )
	{
		if(%quadrant $= "front_left" || %quadrant $= "front_right")
			%curDie = $PlayerDeathAnim::ExplosionBlowBack;
		else
			%curDie = $PlayerDeathAnim::TorsoBackFallForward;
	}
	else if(%vertPos $= "head")
	{
		if(%quadrant $= "front_left" ||  %quadrant $= "front_right" )
			%curDie = $PlayerDeathAnim::HeadFrontDirect;
		else
			%curDie = $PlayerDeathAnim::HeadBackFallForward;
	}
	else if(%vertPos $= "torso")
	{
		if(%quadrant $= "front_left" )
			%curDie = $PlayerDeathAnim::TorsoLeftSpinDeath;
		else if(%quadrant $= "front_right")
			%curDie = $PlayerDeathAnim::TorsoRightSpinDeath;
		else if(%quadrant $= "back_left" )
			%curDie = $PlayerDeathAnim::TorsoBackFallForward;
		else if(%quadrant $= "back_right")
			%curDie = $PlayerDeathAnim::TorsoBackFallForward;
	}
	else if (%vertPos $= "legs")
	{
		if(%quadrant $= "front_left" ||  %quadrant $= "back_left")
			%curDie = $PlayerDeathAnim::LegsLeftGimp;
		if(%quadrant $= "front_right" || %quadrant $= "back_right")
			%curDie = $PlayerDeathAnim::LegsRightGimp;
	}

	if(%curDie $= "" || %curDie < 1 || %curDie > 11)
		%curDie = 1;

	%this.setArmThread("look");
	%this.setActionThread("Death" @ %curDie);
}

function Player::playPartyAnimation(%this,%key)
{
	if (%this.getState() !$= "Dead")
	{
		%idx = $PlayerPartyAnims.getIndexFromKey(%key);
		%thread = $PlayerPartyAnims.getValue(%idx);
		%idx = $PlayerPartySounds.getIndexFromKey(%key);
		%sound = $PlayerPartySounds.getValue(%idx);
		%this.setActionThread(%thread);
		%this.playAudio(1,%sound);
	}
}

//----------------------------------------------------------------------------

function Player::playDeathCry( %this )
{
	%client = %this.client;
	//playTargetAudio( %client.target, "replaceme", AudioClosest3d, false );
}

function Player::playPain( %this )
{
	%client = %this.client;
	//playTargetAudio( %client.target, "replaceme", AudioClosest3d, false);
}
