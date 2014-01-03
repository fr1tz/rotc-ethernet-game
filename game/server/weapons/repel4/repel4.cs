//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./repel4.sfx.cs");
exec("./repel4.gfx.cs");

function deployRepel4(%obj)
{
	if(!%obj.isCAT)
		return;

	if(%obj.getEnergyLevel() < 50)
		return;

//	if($Sim::Time < %obj.lastRepelTime + 1)
//		return;

   %time = getSimTime();
	%targets = new SimSet();

	%pos = %obj.getWorldBoxCenter();
	%radius = 10;
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
   
      if(mAbs(%targetObject.lastRepelTime-%time) < 500)
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
  
   	%targetObject.lastRepelTime = %time;

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
            %targetObject.damage(%obj, %dmgpos, %damage, $DamageType::BOUNCE);
         }
      }

      %teamId = %obj.getTeamId();
      if(%teamId == 1)
         %exp = RedRepel4Explosion5;
      else
         %exp = BlueRepel4Explosion5;
		if(%speed == -1)
      {
         if(%teamId == 1)
            %exp = RedRepel4ProjectileExplosion;
         else
            %exp = BlueRepel4ProjectileExplosion;
      }
		else if(%speed < 10)
      {
         if(%teamId == 1)
            %exp = RedRepel4Explosion1;
         else
            %exp = BlueRepel4Explosion1;
      }
		else if(%speed < 25)
      {
         if(%teamId == 1)
            %exp = RedRepel4Explosion2;
         else
            %exp = BlueRepel4Explosion2;
      }
		else if(%speed < 50)
      {
         if(%teamId == 1)
            %exp = RedRepel4Explosion3;
         else
            %exp = BlueRepel4Explosion3;
      }
		else if(%speed < 70)
      {
         if(%teamId == 1)
            %exp = RedRepel4Explosion4;
         else
            %exp = BlueRepel4Explosion4;
      }
		createExplosion(%exp, %targetObject.getWorldBoxCenter(), "0 0 1");
	}

	%targets.delete();

	// source explosion effects...
	//createExplosion(RepelSourceExplosion, %pos, "0 0 1");
   %obj.stopAudio(0);
   %obj.playAudio(0, Repel4ExplosionSound);
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
}

