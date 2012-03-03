//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./repelgun.mine.gfx.cs");

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
	lightRadius = 6;
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
	// script damage properties...
	impactDamage        = 10; // only used to determine time for barrier
	impactImpulse       = 0;
	splashDamage        = 40;
	splashDamageRadius  = 6;
	splashImpulse       = 6000;
   splashDamageFalloff = $SplashDamageFalloff::None;
	bypassDamageBuffer  = true;

   shapeFile = "share/shapes/rotc/misc/mine.dts";

	shapeFxTexture[0] = "share/textures/rotc/white.png";
	shapeFxTexture[1] = "share/textures/rotc/zone.grid.png";
	shapeFxTexture[5] = "share/textures/rotc/bounce.orange.hit.png";
	shapeFxTexture[6] = "share/textures/rotc/bounce.orange.miss.png";

	shapeFxColor[0] = "1.00 1.00 1.00 1.00";
	shapeFxColor[1] = "1.00 0.50 0.00 1.00";
	
	// script fields...
	light = RedRepelGunMineLightImage;
};

function RedRepelGunMine::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);

   %obj.getHudInfo().setActive(false);

	%obj.shapeFxSetTexture(1, 0);
	%obj.shapeFxSetColor(1, 1);
	%obj.shapeFxSetBalloon(1, 1.1, 0);
	%obj.shapeFxSetFade(1, 0, 1/4.0);
	%obj.shapeFxSetActive(1, true, false);
	
	%obj.shapeFxSetTexture(2, 1);
	%obj.shapeFxSetColor(2, 1);
	%obj.shapeFxSetBalloon(2, 1.1, 0);
	%obj.shapeFxSetActive(2, true, false);
	
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

	%pos = %obj.getPosition();
	%normal = %obj.zNormal;
	%fade = 1;
	%dist = 0;
	%expType = 0;

	// Disc lock...
	if(isObject(%obj.client.player) && %obj.client.player.isCAT)
	{
		%radius = %this.splashDamageRadius;
		InitContainerRadiusSearch(%pos, %radius, $TypeMasks::ShapeBaseObjectType);
		%halfRadius = %radius / 2;
		while( (%targetObject = containerSearchNext()) != 0 )
		{
			// the observer cameras are ShapeBases; ignore them...
			if(%targetObject.getType() & $TypeMasks::CameraObjectType)
				continue;

			// ignore shapes with a barrier...
			if(%targetObject.hasBarrier())
				continue;

			%coverage = calcExplosionCoverage(%pos, %targetObject,
				$TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
				$TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType |
				$TypeMasks::TurretObjectType);

			if (%coverage == 0)
				continue;

			%obj.client.player.setDiscTarget(%targetObject);
		}
	}

	ProjectileData::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);
	
	%obj.unmountImage(0);

	createExplosion(RepelGunMineExplosionExplosion, %pos, %normal);

	%obj.startFade(0, 0, true);

	%obj.shapeFxSetTexture(1, 0);
	%obj.shapeFxSetBalloon(1, 1.1, 200);
	%obj.shapeFxSetFade(1, 0.5, -1/0.15);
	%obj.shapeFxSetActive(1, true, false);

	%obj.shapeFxSetTexture(2, 6);
	%obj.shapeFxSetColor(2, 0);
	%obj.shapeFxSetBalloon(2, 1.1, 200);
	%obj.shapeFxSetFade(2, 1, -1/0.15);

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
	%radius = 6;

	%hitEnemy = false;

	InitContainerRadiusSearch(%pos, %radius, $TypeMasks::PlayerObjectType);
	%halfRadius = %radius / 2;
	while ((%targetObject = containerSearchNext()) != 0)
	{
		if(%targetObject.teamId == 0 || %targetObject.teamId == %obj.teamId)
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

		%this.detonate(%obj, true);
		return;
	}	
	
	%this.schedule(50, "checkDetonate", %obj);	
}

//--------------------------------------------------------------------------

datablock StaticShapeData(BlueRepelGunMine : RedRepelGunMine)
{
	shapeFxTexture[5] = "share/textures/rotc/bounce.green.hit.png";
	shapeFxTexture[6] = "share/textures/rotc/bounce.green.miss.png";
	shapeFxColor[1] = "0.00 1.00 0.00 1.00";
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


