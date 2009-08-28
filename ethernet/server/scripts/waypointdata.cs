//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - wapointdata.cs
// DataBlocks for commonly used waypoints
//------------------------------------------------------------------------------

//
// waypoints for checkpoints
//

datablock RotcWaypointData(Team1Checkpoint)
{
	name = ""; // get's overwritten by checkpoint code
	desc = "";
	icon = "~/data/textures/checkpoint_t1.png";
	groundShape = "~/data/waypoints/shape.dts";
};

datablock RotcWaypointData(Team2Checkpoint)
{
	name = ""; // get's overwritten by checkpoint code
	desc = "";
	icon = "~/data/textures/checkpoint_t2.png";
	groundShape = "~/data/waypoints/shape.dts";
};

datablock RotcWaypointData(NeutralCheckpoint)
{
	name = ""; // get's overwritten by checkpoint code
	desc = "";
	icon = "~/data/textures/checkpoint_neutral.png";
	groundShape = "~/data/waypoints/shape.dts";
};

//
// other waypoints...
//

datablock RotcWaypointData(Team1Waypoint)
{
	name = ""; // will probably be overridden...
	desc = "";
	icon = "~/data/textures/checkpoint_neutral.png";
	groundShape = "~/data/waypoints/shape.dts";
};
