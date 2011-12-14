//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./repel.sfx.cs");
exec("./repel.gfx.cs");

function deployRepel3(%obj)
{
	if(!%obj.isCAT)
		return;

	if(%obj.getEnergyLevel() < 50)
		return;

//	if($Sim::Time < %obj.lastRepelTime + 1)
//		return;

	%targets = new SimSet();

	%pos = %obj.getWorldBoxCenter();
	%radius = 7;
   %mask = $TypeMasks::PlayerObjectType | $TypeMasks::ProjectileObjectType;

	InitContainerRadiusSearch(%pos, %radius, %mask);
	while( (%targetObject = containerSearchNext()) != 0 )
		%targets.add(%targetObject);

   %hitEnemy = false;

	%halfRadius = %radius / 2;
	for(%idx = %targets.getCount()-1; %idx >= 0; %idx-- )
	{
		%targetObject = %targets.getObject(%idx);

      if(%targetObject.getType() & $TypeMasks::ProjectileObjectType)
         if(!%targetObject.isAlive())
            continue;

		if(%targetObject.getTeamId() == %obj.getTeamId())
			continue;

      if(%targetObject.isMethod("hasBarrier") && %targetObject.hasBarrier())
         continue;

		// Calculate how much exposure the current object has to
		// the effect.  The object types listed are objects
		// that will block an explosion.
		%coverage = calcExplosionCoverage(%pos, %targetObject,
			$TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
			$TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType |
			$TypeMasks::TurretObjectType);

		if (%coverage == 0)
			continue;

		%hitEnemy = true;

      if(%targetObject.getClassName() $= "Projectile")
      {
   		%speed = -1;
         %targetObject.explode();
      }
      else
      {
   		// bouncy bounce...
		   %vec = %targetObject.getVelocity();
   		%vec = VectorScale(%vec, -1);
         %targetObject.setVelocity(%vec);

         if(%targetObject.getClassName() $= "NortDisc")
         {
            %speed = -1;
            %targetObject.setTrackingAbility(0);
         }
         else
         {
            // damage based on speed...
            %speed = VectorLen(%vec);
            %damage = %speed;
            %dmgpos = %targetObject.getWorldBoxCenter();
            %targetObject.damage(0, %dmgpos, %damage, $DamageType::BOUNCE);
         }
      }

      %teamId = %obj.getTeamId();
      if(%teamId == 1)
         %exp = OrangeRepelExplosion5;
      else
         %exp = GreenRepelExplosion5;
		if(%speed == -1)
      {
         if(%teamId == 1)
            %exp = OrangeRepelProjectileExplosion;
         else
            %exp = GreenRepelProjectileExplosion;
      }
		else if(%speed < 10)
      {
         if(%teamId == 1)
            %exp = OrangeRepelExplosion1;
         else
            %exp = GreenRepelExplosion1;
      }
		else if(%speed < 25)
      {
         if(%teamId == 1)
            %exp = OrangeRepelExplosion2;
         else
            %exp = GreenRepelExplosion2;
      }
		else if(%speed < 50)
      {
         if(%teamId == 1)
            %exp = OrangeRepelExplosion3;
         else
            %exp = GreenRepelExplosion3;
      }
		else if(%speed < 70)
      {
         if(%teamId == 1)
            %exp = OrangeRepelExplosion4;
         else
            %exp = GreenRepelExplosion4;
      }
		createExplosion(%exp, %targetObject.getWorldBoxCenter(), "0 0 1");
	}

	%targets.delete();

	// source explosion effects...
	//createExplosion(RepelSourceExplosion, %pos, "0 0 1");
   %obj.stopAudio(0);
   %obj.playAudio(0, RepelExplosionSound);
   %obj.shapeFxSetColor($PlayerShapeFxSlot::Misc, 0);
	%obj.shapeFxSetBalloon($PlayerShapeFxSlot::Misc, 1.025, 150);
	%obj.shapeFxSetFade($PlayerShapeFxSlot::Misc, 1, -1/0.25);
	if(%hitEnemy)
	{
   	%obj.shapeFxSetTexture($PlayerShapeFxSlot::Misc, 5);
	}
	else
	{
		%obj.shapeFxSetTexture($PlayerShapeFxSlot::Misc, 6);
		%obj.setEnergyLevel(%obj.getEnergyLevel() - 50);
	}
	%obj.shapeFxSetActive($PlayerShapeFxSlot::Misc, true, false, true);

	%obj.lastRepelTime = $Sim::Time;
}

function deployRepel2(%obj)
{
	if(!%obj.isCAT)
		return;
		
	if(%obj.gridConnection == 0)
		return;		

	if($Sim::Time < %obj.lastRepelTime + 1)
		return;
		
	%pos = %obj.getWorldBoxCenter();
	%radius = 10;
	%impulse = 10000 * %obj.gridConnection;

	%hitEnemy = false;
	
	InitContainerRadiusSearch(%pos, %radius, $TypeMasks::PlayerObjectType);
	%halfRadius = %radius / 2;
	while ((%targetObject = containerSearchNext()) != 0)
	{
		if(%targetObject.getTeamId() == %obj.getTeamId())
			continue;

		// Calculate how much exposure the current object has to
		// the effect.  The object types listed are objects
		// that will block an explosion. 
		%coverage = calcExplosionCoverage(%pos, %targetObject,
			$TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
			$TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType |
			$TypeMasks::TurretObjectType);
			
		if (%coverage == 0)
			continue;

		%hitEnemy = true;

		%dist = containerSearchCurrRadiusDist();

		%distScale = (%dist < %halfRadius)? 1.0:
			1.0 - ((%dist - %halfRadius) / %halfRadius);


		%impulseVec = VectorSub(%targetObject.getWorldBoxCenter(), %pos);
		%impulseVec = VectorNormalize(%impulseVec);
		%impulseVec = VectorScale(%impulseVec, %impulse * %distScale);
		%targetObject.impulse(%position, %impulseVec);
	}

	// source explosion effects...

	%obj.shapeFxSetBalloon($PlayerShapeFxSlot::GridConnection, 1.025, 100);
	%obj.shapeFxSetFade($PlayerShapeFxSlot::GridConnection, %obj.gridConnection, -1/0.15);
	%obj.shapeFxSetActive($PlayerShapeFxSlot::GridConnection, true, true);
	createExplosion(RepelSourceExplosion, %pos, "0 0 1");
	
	if(%hitEnemy)
	{
		//%obj.shapeFxSetTexture($PlayerShapeFxSlot::Repel, 1);
	}
	else
	{
		//%obj.shapeFxSetTexture($PlayerShapeFxSlot::Repel, 0);
		//%obj.setEnergyLevel(%obj.getEnergyLevel() - 50);	
	}
	
	// hack hack hack
	%obj.gridConnection = 0;
	cancel(%obj.updateGridConnectionThread);
	%obj.updateGridConnectionThread = -1;
	%obj.schedule(150, "updateGridConnection");	
	%obj.forceGridConnectionShapeFxUpdate = true;

	%obj.lastRepelTime = $Sim::Time;
}

function deployRepel1(%obj)
{
	if(!%obj.isCAT)
		return;

	if(%obj.getEnergyLevel() < 50)
		return;

	if($Sim::Time < %obj.lastRepelTime + 1)
		return;
		
	%pos = %obj.getWorldBoxCenter();
	%radius = 10;

	%hitEnemy = false;

	InitContainerRadiusSearch(%pos, %radius, $TypeMasks::PlayerObjectType);
	%halfRadius = %radius / 2;
	while ((%targetObject = containerSearchNext()) != 0)
	{
		if(%targetObject.getTeamId() == %obj.getTeamId())
			continue;

		// Calculate how much exposure the current object has to
		// the effect.  The object types listed are objects
		// that will block an explosion. 
		%coverage = calcExplosionCoverage(%pos, %targetObject,
			$TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
			$TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType |
			$TypeMasks::TurretObjectType);
			
		if (%coverage == 0)
			continue;

		%hitEnemy = true;

		%vec = %targetObject.getVelocity();
		%vec = VectorScale(%vec, -1);
		%targetObject.setVelocity(%vec);

		%speed = VectorLen(%vec);
		//error("speed:" SPC %speed);

		%exp = RepelExplosion5;
		if(%speed < 10)
			%exp = RepelExplosion1;	
		else if(%speed < 25)
			%exp = RepelExplosion2;			
		else if(%speed < 50)
			%exp = RepelExplosion3;			
		else if(%speed < 70)
			%exp = RepelExplosion4;		
		createExplosion(%exp, %targetObject.getWorldBoxCenter(), "0 0 1");
	}

	// source explosion effects...

	%obj.shapeFxSetBalloon($PlayerShapeFxSlot::Repel, 1.025, 100);
	%obj.shapeFxSetFade($PlayerShapeFxSlot::Repel, 1, -1/0.15);
	%obj.shapeFxSetActive($PlayerShapeFxSlot::Repel, true, true);
	createExplosion(RepelSourceExplosion, %pos, "0 0 1");

	if(%hitEnemy)
	{
		%obj.shapeFxSetTexture($PlayerShapeFxSlot::Repel, 1);
	}
	else
	{
		%obj.shapeFxSetTexture($PlayerShapeFxSlot::Repel, 0);
		%obj.setEnergyLevel(%obj.getEnergyLevel() - 50);	
	}

	%obj.lastRepelTime = $Sim::Time;
}
