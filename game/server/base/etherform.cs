//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./etherform.sfx.cs");
exec("./etherform.gfx.cs");

//-----------------------------------------------------------------------------

function EtherformData::useWeapon(%this, %obj, %nr)
{
	%client = %obj.client;

	if(%obj.inventoryMode $= "show")
	{
		if(%nr < 1 || %nr > 2)
			return;

		%obj.inventoryMode = "select";
		%obj.inventoryMode[1] = %nr;
		%this.displayInventory(%obj);
	}
	else if(%obj.inventoryMode $= "select")
	{
		if(%nr < 1 || %nr > 5)
			return;

		%client.weapon[%obj.inventoryMode[1]] = %nr;

		%obj.inventoryMode = "show";
		%this.displayInventory(%obj);
	}
}

function EtherformData::damage(%this, %obj, %sourceObject, %pos, %damage, %damageType)
{
	// ignore damage
}

function EtherformData::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);
	
	// start singing...
	%obj.playAudio(1, EtherformSingSound);

	// Clear HUD menus...
	if(isObject(%obj.client))
	{
		%obj.client.setHudMenuL("*", " ", 1, 0);
		%obj.client.setHudMenuR("*", " ", 1, 0);
	}
	
	// Make sure grenade ammo bar is not visible...
	messageClient(%obj.client, 'MsgGrenadeAmmo', "", 1);

	// lights...
	if(%obj.getTeamId() == 1)
		%obj.mountImage(RedEtherformLightImage, 3);
	else
		%obj.mountImage(BlueEtherformLightImage, 3);

	%obj.inventoryMode = "show";
	%this.displayInventory(%obj);
}

function EtherformData::onDamage(%this, %obj, %delta)
{
	// don't do anything
}

//-----------------------------------------------------------------------------

function EtherformData::displayInventory(%this, %obj)
{
	%client = %obj.client;
	if(!isObject(%client))
		return;

	%weapons[1] = "blaster";
	%weapons[2] = "rifle";
	%weapons[3] = "sniper";
	%weapons[4] = "minigun";
	%weapons[5] = "grenadelauncher";

	if(%obj.inventoryMode $= "show")
	{
		for(%i = 1; %i <= 2; %i++)
			%icon[%i] = %weapons[%client.weapon[%i]];
	
		%client.setHudMenuL("*", " ", 1, 0);
		%client.setHudMenuL(1, "Weapon #1:\n", 1, 1);
		%client.setHudMenuL(2, "<bitmap:share/hud/rotc/icon." @ %icon[1] @ ">", 1, 1);
		%client.setHudMenuL(3, "<sbreak>(Press @bind35 to change)", 1, 1);
		
		%client.setHudMenuL(4, "\n\n\n\n\n\n\n\nWeapon #2:\n", 1, 1);
		%client.setHudMenuL(5, "<bitmap:share/hud/rotc/icon." @ %icon[2] @ ">", 1, 1);
		%client.setHudMenuL(6, "<sbreak>(Press @bind36 to change)", 1, 1);
	}
	else if(%obj.inventoryMode $= "select")
	{
		%client.setHudMenuL("*", " ", 1, 0);
		%client.setHudMenuL(0, "Select weapon #" @ %obj.inventoryMode[1] @ ":\n\n", 1, 1);
		for(%i = 1; %i <= 5; %i++)
			%client.setHudMenuL(%i, %i @ "<bitmap:share/hud/rotc/icon." @ %weapons[%i] @ "><sbreak>\n", 1, 1);		
	}
}

//-----------------------------------------------------------------------------

datablock EtherformData(RedEtherform)
{
	hudImageNameFriendly = "~/client/ui/hud/pixmaps/teammate.etherform.png";
	hudImageNameEnemy = "~/client/ui/hud/pixmaps/enemy.etherform.png";
	
	thirdPersonOnly = true;

    //category = "Vehicles"; don't appear in mission editor
	shapeFile = "share/shapes/rotc/vehicles/etherform/vehicle.red.dts";
	emap = true;
 
	cameraDefaultFov = 110.0;
	cameraMinFov     = 110.0;
	cameraMaxFov     = 130.0;
	cameraMinDist    = 2;
	cameraMaxDist    = 3;
	
	//renderWhenDestroyed = false;
	//explosion = FlyerExplosion;
	//defunctEffect = FlyerDefunctEffect;
	//debris = BomberDebris;
	//debrisShapeName = "share/shapes/rotc/vehicles/bomber/vehicle.dts";

	mass = 90;
	drag = 0.99;
	density = 10;

	maxDamage = 75;
	damageBuffer = 25;
	maxEnergy = 100;

	damageBufferRechargeRate = 0.15;
	damageBufferDischargeRate = 0.05;
	energyRechargeRate = 0.4;
 
    // collision box...
    boundingBox = "1.0 1.0 1.0";
 
    // etherform movement...
    accelerationForce = 100;

	// impact damage...
	minImpactSpeed = 1;		// If hit ground at speed above this then it's an impact. Meters/second
	speedDamageScale = 0.0;	// Dynamic field: impact damage multiplier

	// damage info eyecandy...
	damageBufferParticleEmitter = RedEtherformDamageBufferEmitter;
	repairParticleEmitter = RedEtherformRepairEmitter;
	bufferRepairParticleEmitter = RedEtherformBufferRepairEmitter;

	// laser trail...
	laserTrail[0] = RedEtherform_LaserTrailOne;
	laserTrail[1] = RedEtherform_LaserTrailTwo;

	// contrail...
	minTrailSpeed = 1;
	//particleTrail = RedEtherform_ContrailEmitter;
	
	// various emitters...
	//forwardJetEmitter = FlyerJetEmitter;
	//downJetEmitter = FlyerJetEmitter;

	//
//	jetSound = Team1ScoutFlyerThrustSound;
//	engineSound = EtherformSound;
	softImpactSound = EtherformImpactSound;
	hardImpactSound = EtherformImpactSound;
	//wheelImpactSound = WheelImpactSound;


};

datablock EtherformData(BlueEtherform : RedEtherform)
{
	shapeFile = "share/shapes/rotc/vehicles/etherform/vehicle.blue.dts";
	damageBufferParticleEmitter = BlueEtherformDamageBufferEmitter;
	repairParticleEmitter = BlueEtherformRepairEmitter;
	bufferRepairParticleEmitter = BlueEtherformBufferRepairEmitter;
	laserTrail[0] = BlueEtherform_LaserTrailOne;
	laserTrail[1] = BlueEtherform_LaserTrailTwo;
	//particleTrail = BlueEtherform_ContrailEmitter;
};


