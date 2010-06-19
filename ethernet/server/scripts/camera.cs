//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----
// camera.cs
// every client has a camera associated with it (NOT the camera of the client's
// player-shape)
//-----

// Global movement speed that affects all cameras.  This should be moved
// into the camera datablock.
$Camera::movementSpeed = 40;

datablock CameraData(ObserverCamera)
{
	mode = "Observer";
};

//-----------------------------------------------------------------------------

function ObserverCamera::onTrigger(%this,%obj,%trigger,%state)
{
	// state = 0 means that a trigger key was released
	if (%state == 0)
		return;

	%client = %obj.getControllingClient();
	switch$( %obj.mode )
	{
		case "Observer":
			// Do something interesting.

		case "Corpse":
			if( %client.lastDeathTime > getSimTime() - 10*1000 ) return;

			// for round-based modes, we must place the player in an observer-like \
			// mode, without actualy setting his team to observers($Team0)...
			if( $Server::MissionType $= "confrontation" )
			{
				%client.setControlObject(%client.camera);
				%obj.setMode("Observer");
			}
			else
			{
				%client.player = 0;
				%client.spawnPlayer();
			}
	}
}

function ObserverCamera::setMode(%this,%obj,%mode,%arg1,%arg2,%arg3)
{
	switch$(%mode)
	{
		case "Observer":
			// Let the player fly around
			%obj.setFlyMode();
			break;

		case "Corpse":
			// Lock the camera in orbit around the object, which should be arg1
			if(isObject(%arg1))
			{
				%player = %arg1;
				%transform = %player.getTransform();
				if(%player.getDamageState() $= "Destroyed")
					%obj.setOrbitMode(0, %transform, 0.5, 4.5, 4.5);
				else
					%obj.setOrbitMode(%player, %transform, 0.5, 4.5, 4.5);
			}
			break;
			
		case "OrbitPoint":
			// Lock the camera in orbit around the point, which should be arg1
			%obj.setOrbitMode(0, %arg1, 0.5, 4.5, 4.5);
			break;
	}
	
	%obj.mode = %mode;
}


//-----------------------------------------------------------------------------
// Camera methods
//-----------------------------------------------------------------------------

function Camera::setMode(%this,%mode,%arg1,%arg2,%arg3)
{
	// Punt this one over to our datablock
	%this.getDatablock().setMode(%this,%mode,%arg1,%arg2,%arg3);
}

//-----------------------------------------------------------------------------
// Path Camera
//-----------------------------------------------------------------------------

datablock PathCameraData(LoopingCam)
{
	mode = "";
};

function LoopingCam::onNode(%this,%camera,%node)
{
	if (%node == %camera.loopNode) {
		%camera.pushPath(%camera.path);
		%camera.loopNode += %camera.path.getCount();
	}
}

//-----------------------------------------------------------------------------

function PathCamera::followPath(%this,%path)
{
	%this.path = %path;
	if (!(%this.speed = %path.speed))
		%this.speed = 100;
	if (%path.isLooping)
		%this.loopNode = %path.getCount() - 2;
	else
		%this.loopNode = -1;

	%this.pushPath(%path);
	%this.popFront();
}

function PathCamera::pushPath(%this,%path)
{
	for (%i = 0; %i < %path.getCount(); %i++)
		%this.pushNode(%path.getObject(%i));
}

function PathCamera::pushNode(%this,%node)
{
	if (!(%speed = %node.speed))
		%speed = %this.speed;
	if ((%type = %node.type) $= "")
		%type = "Normal";
	if ((%smoothing = %node.smoothing) $= "")
		%smoothing = "Linear";
	%this.pushBack(%node.getTransform(),%speed,%type,%smoothing);
}
