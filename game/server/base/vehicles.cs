//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - vehicles.cs
// Code for all vehicles
//------------------------------------------------------------------------------

function executeVehicleScripts()
{
	 echo(" ----- executing vehicle scripts ----- ");
	 
	 exec("./vehicles.gfx.cs");
}

executeVehicleScripts();

//-----------------------------------------------------------------------------

function HoverVehicleData::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type.  For the mission editor
	%obj = new HoverVehicle() {
		dataBlock = %data;
	};
	
	return %obj;
}

function WheeledVehicleData::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type.  For the mission editor
	%obj = new AIWheeledVehicle() {
		dataBlock = %data;
	};

	return %obj;
}

function TankVehicleData::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type.  For the mission editor
	%obj = new  TankVehicle() {
		dataBlock = %data;
	};

	return %obj;
}

function FlyingVehicleData::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type.  For the mission editor
	%obj = new AIFlyingVehicle() {
		dataBlock = %data;
	};

	return %obj;
}

//-----------------------------------------------------------------------------

function VehicleData::onAdd(%this,%obj)
{
	Parent::onAdd(%this,%obj);
}

function VehicleData::onRemove(%this,%obj)
{
	Parent::onRemove(%this,%obj);
}

// *** Callback function: called by engine
function VehicleData::onTrigger(%this, %obj, %triggerNum, %val)
{

}

//----------------------------------------------------------------------------

// *** Callback function: called by engine
function VehicleData::onCollision(%this,%obj,%col,%vec,%vecLen)
{
	Parent::onCollision(%this,%obj,%col,%vec,%vecLen);

	if (%obj.getDamageState() $= "Destroyed")
		return;

	// push & damage players that are in our way...
	if(%col.getType() & $TypeMasks::PlayerObjectType)
	{
		%vel = VectorLen(%obj.getVelocity());
		%pos = %col.getWorldBoxCenter();
		
		if(%vel > 1)
		{
			%impulseVec = VectorNormalize(%vec);
			%impulseVec = VectorScale(%impulseVec,5*%col.getDataBlock().mass);
			%col.applyImpulse(%pos, %impulseVec);
		}

		// kill player when not moving slowly
		if(%vel > 10)
			%col.damage(%obj,%pos,%vel,"VehicleCollision");
	}
}

// *** Callback function: called by engine
function VehicleData::onImpact(%this, %obj, %col, %vec, %vecLen)
{
	%obj.damage(0, VectorAdd(%obj.getPosition(),%vec),
		%vecLen * %this.speedDamageScale, $DamageType::Force);
}

function VehicleData::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
{
	Parent::damage(%this, %obj, %sourceObject, %position, %damage, %damageType);

//	%location = "Body";
//
//	%client = %obj.client;
//	%sourceClient = %sourceObject ? %sourceObject.client : 0;

//	if( %obj.getDamageLevel() >= %this.maxDamage )
//		%client.onDeath(%sourceObject, %sourceClient, %damageType, %location);
}

// *** Callback function:
// Invoked by ShapeBase code whenever the object's damage level changes
function VehicleData::onDamage(%this, %obj, %delta)
{
	%flash = %obj.getWhiteOut() + ((%delta / %this.maxDamage));
	if (%flash > 0.75)
		%flash = 0.75;
	%obj.setWhiteOut(%flash);

	%damage = %obj.getDamageLevel();
	if(%damage >= %this.maxDamage)
	{
		if(%obj.getDamageState() !$= "Destroyed")
			%obj.setDamageState("Destroyed");
	}
}

// *** Callback function:
// Invoked by ShapeBase code when object's damageState was set to 'Destroyed'
function VehicleData::onDestroyed(%this, %obj, %prevState)
{
	// unmount all mounted objects...
	for(%i = 0; %i < 8; %i++)
		if( isObject(%obj.getMountedObject(%i)) )
		 %obj.unmountObject(%obj.getMountedObject(%i));

	// create wreck?...
	if(isObject(%this.wreck))
	{
		%wreck = new RigidShape() {
			dataBlock = %this.wreck;
		};
		MissionCleanup.add(%wreck);
		
		%wreck.setTransform(%obj.getTransform());
		%wreck.applyImpulse(%obj.getPosition(), %obj.getVelocity());
		%wreck.schedule(60*1000, "startFade", 3*1000, 0, true);
		%wreck.schedule(60*1000+3*1000, "delete");
	}
	
	// put vehicle away and schedule its removal...
	%obj.setTransform("0 0 -10000");
	%obj.schedule(2000,"delete");

	if(%obj.getControllingClient())
		%obj.getControllingClient().toggleFullControl(false);
}

// *** callback function: called by player script code
function VehicleData::onPlayerMounted(%this,%obj,%player,%node)
{
	// only here to avoid console spam
}

//----------------------------------------------------------------------------

function serverCmdMountVehicle(%client)
{
	%player = %client.getControlObject();
	if(!isObject(%player))
		return;
		
	// if the player is already mounted, re-mount him in the next free seat...
	if(%player.isMounted())
	{
		%vehicle = %player.getObjectMount();
		%player.mountVehicle(%vehicle);
		return;
	}

	%selectRange = 6;
	%searchMasks = $TypeMasks::VehicleObjectType;
	%pos = %player.getEyePoint();

	%eye = %player.getEyeVector();
	%eye = vectorNormalize(%eye);
	%vec = vectorScale(%eye, %selectRange);

	%end = vectorAdd(%vec, %pos);

	%scanTarg = ContainerRayCast (%pos, %end, %searchMasks);

	if(%scanTarg)
	{
		%targetObject = firstWord(%scanTarg);
		echo("Found a vehicle: " @ %targetObject);
		%player.mountVehicle(%targetObject);
	}
	else
	{
		echo("No object found");
	}
}

function serverCmdDismountVehicle(%client)
{
	%client.player.dismountVehicle(%client.player);
}

function Vehicle::findEmptySeat(%this)
{
	%data = %this.getDataBlock();
	echo("This vehicle has " @ %data.numMountPoints @ " mount points.");
	for (%i = 0; %i <  %data.numMountPoints; %i++)
	{
		%obj = %this.getMountNodeObject(%i);
		if(%obj == 0)
		{
			return %i;
		}
	}
	return -1;
}

function Vehicle::isMoving(%this)
{
	%vel = %obj.getVelocity();
	%speed = vectorLen(%vel);

	if (%speed > %this.getDataBlock().stationaryThreshold)
		return true;
	else
		return false;
}

// utility function
function dumpMounts(%vehicle)
{
	echo("**************");
	echo("Dumping mounts");
	echo("--------------");
	%vehicleData = %vehicle.getDataBlock();
	for (%ii=0; %ii<%vehicleData.numMountPoints;%ii++)
	{
	  echo(%ii @ ": " @ %vehicle.getMountNodeObject(%ii));
	}
	echo("**************");
}

//----------------------------------------------------------------------------

