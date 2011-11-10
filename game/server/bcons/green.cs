//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function dropGreen(%obj)
{
	%throwForce = 200;

	%vec = %obj.getEyeVector();
	%vec = VectorAdd(%vec, %obj.getVelocity());
	%vec = VectorNormalize(%vec);

	%vec = VectorScale(%vec, %throwForce);

	// get initial position...
	%pos = %obj.getWorldBoxCenter();
	%pos = VectorAdd(%pos, "0 0 0.5");

	// create the green...
	%green = new RigidShape() {
		dataBlock = Green;
	};
	MissionCleanup.add(%green);

	%green.setTransform(%pos);
	%green.applyImpulse(%pos, %vec);
}

datablock AudioProfile(GreenImpactSound)
{
	filename = "share/shapes/rotc/vehicles/etherform/sound.impact.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock RigidShapeData(Green)
{
	isInvincible = true;
	
	shapeFile = "share/shapes/rotc/green.dts";
	emap = false;

	// Rigid body
	mass = 10;
	massCenter = "0 0 0";   // Center of mass for rigid body
	massBox = "0 0 0";      // Size of box used for moment of inertia, \
	                      // if zero it defaults to object bounding box
	bodyFriction = 0.9;     // Don't mess with this.
	bodyRestitution = 0.5;  // When you hit the ground, how much you rebound. (between 0 and 1)
	
	softImpactSpeed = 0.5;    // Sound hooks. This is the soft hit.
	hardImpactSpeed = 1;    // Sound hooks. This is the hard hit.
	
	integration = 4;        // Physics integration: TickSec/Rate
	collisionTol = 0.2;     // Collision distance tolerance
	contactTol = 0.1;    // Contact velocity tolerance
	
	softImpactSound  = GreenImpactSound;
	hardImpactSound  = GreenImpactSound;

	shapeFxTexture[0] = "share/shapes/rotc/misc/debris1.white.png";
	shapeFxTexture[1] = "share/textures/rotc/heating.png";
	shapeFxColor[0] = "0.0 1.0 0.0 1.0"; 
	
	// Script fields...
	category = "Misc"; // for the mission editor
};

function Green::onAdd(%this, %obj)
{
	//error("Green::onAdd()");
	Parent::onAdd(%this, %obj);
	
	//%obj.startFade(0, 0, true);

	//%obj.shapeFxSetTexture(0, 0);
	//%obj.shapeFxSetActive(0, true, false);

	%obj.shapeFxSetTexture(1, 1);
	%obj.shapeFxSetBalloon(1, 1.2, 0);
	%obj.shapeFxSetActive(1, true, false);

	%obj.freezeThread = schedule(250, %obj, "checkFreeze", %obj);
}

function Green::onCollision(%this, %obj, %col, %vec, %speed)
{
	if(%col.isMethod("getDataBlock")
	&& %col.getDataBlock().getName() $= "GridBeacon")
	{
		%col.gridRadius += 0.1;
		%obj.schedule(0, "delete");
		return;
	}


	return;

   if(%obj == %col || %col.getType() & $TypeMasks::TerrainObjectType)
      return;

   error("Green::onCollision %col:" SPC %col.getDataBlock().getName());
   error("Green::onCollision %obj:" SPC %obj.getDataBlock().getName());

   %normal = vectorDot(%speed, vectorNormalize(%speed));
   if(%normal > 58)
      %scale = %normal / 2;

   %objMass = %obj.getDataBlock().mass;
   %colVel = vectorLen(%col.getVelocity());

   //add some random boost to the Z
   %objVec = getWord(%vec, 0) SPC getWord(%vec, 1) SPC mFloor(getRandom(2, 5));

   %objImpulse = %objMass * ((%speed / 100) + (%colVel / 15));

   //the case is, we don't want to apply an impulse greater then X times the objects mass, so
   //clamp it down, otherwise it might visit the moon
   %objImpulse = %objImpulse / 8 > %objMass ? %objMass * 8 : %objImpulse;

   error("objImpulse:" SPC %objImpulse);

   %obj.applyImpulse(%obj.getWorldBoxCenter(), VectorScale(%objVec, %objImpulse));
}

function freeze(%obj)
{
	error("freeze!");

	if(false)
	{
		%obj.reset();
		%obj.freezeSim(true);
	}
	else
	{
		%new = new StaticShape() {
			dataBlock = FrozenGreen;
		};
		MissionCleanup.add(%new);
		%new.setTransform(%obj.getTransform());
		%obj.delete();
	}
}

function checkFreeze(%obj)
{	
	if(!isObject(%obj))
		return;

	%speed = VectorLen(%obj.getVelocity());
	if(%speed < 0.75)
	{
		freeze(%obj);
	}
	else
	{
		// Check again later.
		%obj.freezeThread = schedule(250, %obj, "checkFreeze", %obj);
	}
}

datablock StaticShapeData(FrozenGreen)
{
	shadowEnable = false;
	shapeFile = "share/shapes/rotc/green.dts";
	emap = false;	
	shapeFxTexture[0] = "share/textures/rotc/heating.png";
	shapeFxColor[0] = "0.0 1.0 0.0 1.0";   
};

function FrozenGreen::onAdd(%this, %obj)
{
	//error("StaticGreen::onAdd()");

	%obj.shapeFxSetActive(0, false, false);

	%obj.shapeFxSetTexture(1, 0);
	%obj.shapeFxSetBalloon(1, 1.2, 0);
	%obj.shapeFxSetActive(1, true, false);
}

function FrozenGreen::use(%this, %obj, %user)
{
	//error("Green::use()");
	%obj.delete();
}

