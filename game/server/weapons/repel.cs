//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./repel.sfx.cs");
exec("./repel.gfx.cs");

function deployRepel(%obj)
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
		createExplosionOnClients(%exp, %targetObject.getWorldBoxCenter(), "0 0 1");
	}

	// source explosion effects...

	%obj.shapeFxSetBalloon($PlayerShapeFxSlot::Repel, 1.025, 100);
	%obj.shapeFxSetFade($PlayerShapeFxSlot::Repel, 1, -1/0.15);
	%obj.shapeFxSetActive($PlayerShapeFxSlot::Repel, true, true);
	createExplosionOnClients(RepelSourceExplosion, %pos, "0 0 1");

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