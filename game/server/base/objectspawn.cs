//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - objectspawn.cs
// code to spawn and respawn stuff
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------

function ObjectSpawn::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type. For the mission editor
	%obj = new SpawnSphere() {
		dataBlock = %data;
		radius = 1;
	};

	return %obj;
}

function ObjectSpawn::onAdd(%this, %obj)
{
	%obj.getDataBlock().schedule(1000,"spawn",%obj);
}

function ObjectSpawn::onRemove(%this, %obj)
{
}

function ObjectSpawn::spawn(%this, %obj)
{
	%newObject = %this.data.create(%this.data);
	%newObject.setTransform(%obj.getTransform());
	%newObject.spawn = %obj;
	%newObject.setTeam(%obj.teamId);
	MissionCleanup.add(%newObject);
}

function ObjectSpawn::respawn(%this, %obj)
{
	%obj.getDataBlock().schedule(%this.respawnTime,"spawn",%obj);
}

//-----------------------------------------------------------------------------

datablock MissionMarkerData(BomberSpawn)
{
	category = "Misc"; // for the mission editor
	className = ObjectSpawn;
	data = Bomber;
	respawnTime = 30*1000;
};

datablock MissionMarkerData(TankSpawn)
{
	category = "Misc"; // for the mission editor
	className = ObjectSpawn;
	data = Tank;
	respawnTime = 60*1000;
};

datablock MissionMarkerData(FlakBuggySpawn)
{
	category = "Misc"; // for the mission editor
	className = ObjectSpawn;
	data = FlakBuggy;
	respawnTime = 30*1000;
};

datablock MissionMarkerData(HowitzerSpawn)
{
	category = "Misc"; // for the mission editor
	className = ObjectSpawn;
	data = Howitzer;
	respawnTime = 180*1000;
};

datablock MissionMarkerData(ScoutBotSpawn)
{
	category = "Misc"; // for the mission editor
	className = ObjectSpawn;
	data = ScoutBot;
	respawnTime = 10*1000;
};


