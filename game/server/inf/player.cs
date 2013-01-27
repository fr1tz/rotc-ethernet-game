//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function Player::changePose(%this, %pose)
{
   if(%this.zPose == %pose)
      return;

   %client = %this.client;

   if(%pose == 0)
   {
      if( %client.team == $Team1 )
         %data = RedInfantryCat;
      else
         %data = BlueInfantryCat;
      %this.setDataBlock(%data);
      %this.setBodyPose($PlayerBodyPose::Standard);
      %this.setEnergyRechargeRate(%this.getDataBlock().energyRechargeRate);
   }
   else if(%pose == 1)
   {
      if( %client.team == $Team1 )
         %data = RedInfantryCatSliding;
      else
         %data = BlueInfantryCatSliding;
      %this.setDataBlock(%data);
      %this.setBodyPose($PlayerBodyPose::Standard);
      %this.setEnergyRechargeRate(-0.5);
   }
   else if(%pose == 2)
   {
      if( %client.team == $Team1 )
         %data = RedInfantryCatCrouched;
      else
         %data = BlueInfantryCatCrouched;
      %this.setDataBlock(%data);
      %this.setBodyPose($PlayerBodyPose::Crouched);
      %this.setEnergyRechargeRate(%this.getDataBlock().energyRechargeRate);
   }
   else if(%pose == 3)
   {
      if( %client.team == $Team1 )
         %data = RedInfantryCatOrb;
      else
         %data = BlueInfantryCatOrb;
      %this.setDataBlock(%data);
      %this.setBodyPose($PlayerBodyPose::Sliding);
      %this.setEnergyRechargeRate(-0.5);
   	%this.unmountImage(0);
      commandToClient(%client, 'Crosshair', 0);
   }

   if(%this.zPose == 3)
   {
      %this.useWeapon(%this.currWeapon);
   }

   %this.zPose = %pose;
}

function Player::updatePose(%this)
{
	%dtTime = 32;

	if(%this.updatePoseThread !$= "")
		cancel(%this.updatePoseThread);

	%this.updatePoseThread = %this.schedule(%dtTime, "updatePose");

   if(%this.wantsToCrouch)
   {
      if(%this.wantsToBoost)
      {
         %orb = false;
         if(%this.getBodyPose() == $PlayerBodyPose::Crouched
         && %this.getEnergyLevel() > 10)
            %orb = true;
         else if(%this.zPose == 3 && %this.getEnergyLevel() > 0)
            %orb = true;
         if(%orb)
         {
            %this.changePose(3);
            return;
         }
      }

      if(%this.hasRunSurface())
      {
         %this.changePose(2);
         return;
      }
   }

   if(%this.wantsToBoost)
   {
      %minEnergy = (%this.zPose == 1 ? 0 : 10);
      if(%this.getEnergyLevel() > %minEnergy)
      {
         %this.changePose(1);
         return;
      }
   }

   %this.changePose(0);
}

function PlayerData::onTrigger(%this, %obj, %triggerNum, %val)
{
	// This method is invoked when the player receives a trigger
	// move event.  The player automatically triggers slot 0 and
	// slot 1 off of triggers # 0 & 1.  Trigger # 2 is also used
	// as the jump key.

	if(!%obj.client)
		return;

	//--------------------------------------------------------------------------
	// Primary fire
	//--------------------------------------------------------------------------
	if( %triggerNum == 0 )
	{

	}

	//--------------------------------------------------------------------------
	// Booster
	//--------------------------------------------------------------------------
  	if(%triggerNum == 1)
   {
      %obj.wantsToBoost = %val;
      %obj.updatePose();
      return;
   }

   //--------------------------------------------------------------------------
	// Jump
	//--------------------------------------------------------------------------
	if( %triggerNum == 2 && %obj.isCAT )
	{
      if(%val)
      {
         %obj.schedule(0, "checkReJump");
      }
      %obj.updatePose();
      return;
	}

	//--------------------------------------------------------------------------
	// Grenade
	//--------------------------------------------------------------------------
	if( %triggerNum == 3 )
	{
		%obj.setImageTrigger(2, %val);
      return;
	}

	//--------------------------------------------------------------------------
	// Crouching
	//--------------------------------------------------------------------------
  	if(%triggerNum == 5)
   {
      %obj.wantsToCrouch = %val;
      %obj.updatePose();
      return;
   }
}

