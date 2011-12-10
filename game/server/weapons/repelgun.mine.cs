//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// light images...

datablock ShapeBaseImageData(RedRepelGunMineLightImage)
{
	// basic item properties
	shapeFile = "share/shapes/rotc/misc/nothing.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset = "0 0 0";
	
	// light properties...
	lightType = "PulsingLight";
	lightColor = "1 0 0";
	lightTime = 200;
	lightRadius = 5;
	lightCastsShadows = false;
	lightAffectsShapes = false;

	stateName[0] = "DoNothing";
};

datablock ShapeBaseImageData(BlueRepelGunMineLightImage : RedRepelGunMineLightImage)
{
	lightColor = "0 0.0 1";
};

//-----------------------------------------------------------------------------
// shape...

datablock StaticShapeData(RedRepelGunMine)
{
   shapeFile = "share/shapes/rotc/misc/mine.dts";
   
	shapeFxTexture[0] = "share/textures/rotc/heating.png";

	shapeFxColor[0] = "1.0 0.5 0.0 1.0"; // repel hit
	shapeFxColor[1] = "1.0 0.5 0.5 1.0"; // repel missed
	
	// script fields...
	light = RedRepelGunMineLightImage;
};

function RedRepelGunMine::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);
	
	%obj.shapeFxSetTexture(1, 0);
	%obj.shapeFxSetColor(1, 0);
	%obj.shapeFxSetBalloon(1, 1.1, 0);
	%obj.shapeFxSetActive(1, true, true);
	
	%obj.mountImage(%this.light, 0);	
	
	%this.checkDetonate(%obj);
	
	%this.schedule(4000, "detonate", %obj, false);
}

function RedRepelGunMine::detonate(%this, %obj, %hit)
{
	if(!isObject(%obj))
		return;
		
	if(%obj.detonated)
		return;
	
	%obj.unmountImage(0);
		
	%obj.playAudio(0, RepelGunMineExplosionSound);
		
	%obj.startFade(0, 0, true);
	%obj.shapeFxSetColor(1, %hit ? 0 : 1);
	%obj.shapeFxSetBalloon(1, 1.1, 200);
	%obj.shapeFxSetFade(1, 1, -1/0.15);
	
	%obj.schedule(1000, "delete");
	
	%obj.detonated = true;
}

function RedRepelGunMine::checkDetonate(%this, %obj)
{
	if(!isObject(%obj))
		return;
		
	if(%obj.detonated)
		return;

	%pos = %obj.getWorldBoxCenter();
	%radius = 5;

	%hitEnemy = false;

	InitContainerRadiusSearch(%pos, %radius, $TypeMasks::PlayerObjectType);
	%halfRadius = %radius / 2;
	while ((%targetObject = containerSearchNext()) != 0)
	{
		if(%targetObject.getTeamId() == %obj.zTeamId)
			continue;

      if(%targetObject.hasBarrier())
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
		
		%vel = %targetObject.getVelocity();

		// bouncy bounce...
		%vec = VectorNormalize(%vel);
		%vec = VectorScale(%vec, -50);
		%targetObject.setVelocity(%vec);
		%targetObject.activateBarrier(1);

		// damage based on speed...
		%speed = VectorLen(%vel);
		%damage = %speed * 2;
		%dmgpos = %targetObject.getWorldBoxCenter();
		%targetObject.damage(0, %dmgpos, %damage, $DamageType::Force);		
		
		if(VectorLen(%vel) != 0)
		{
			%exp = RepelExplosion5;
			createExplosion(%exp, %targetObject.getWorldBoxCenter(), "0 0 1");
		}
	}	
	
	if(%hitEnemy)
	{
		%this.detonate(%obj, true);
		return;
	}
	
	%this.schedule(50, "checkDetonate", %obj);	
}

//--------------------------------------------------------------------------

datablock StaticShapeData(BlueRepelGunMine : RedRepelGunMine)
{
	shapeFxColor[0] = "0.0 1.0 0.0 1.0"; // repel hit
	shapeFxColor[1] = "0.5 1.0 0.5 1.0"; // repel missed
	light = BlueRepelGunMineLightImage;
};

function BlueRepelGunMine::onAdd(%this, %obj)
{
    RedRepelGunMine::onAdd(%this, %obj);
}


function BlueRepelGunMine::detonate(%this, %obj, %hit)
{
	RedRepelGunMine::detonate(%this, %obj, %hit);
}

function BlueRepelGunMine::checkDetonate(%this, %obj)
{
	RedRepelGunMine::checkDetonate(%this, %obj);
}


