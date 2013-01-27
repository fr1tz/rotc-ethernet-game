//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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
	icon = "share/textures/rotc/checkpoint_t1.png";
	groundShape = "share/shapes/rotc/waypoints/shape.dts";
};

datablock RotcWaypointData(Team2Checkpoint)
{
	name = ""; // get's overwritten by checkpoint code
	desc = "";
	icon = "share/textures/rotc/checkpoint_t2.png";
	groundShape = "share/shapes/rotc/waypoints/shape.dts";
};

datablock RotcWaypointData(NeutralCheckpoint)
{
	name = ""; // get's overwritten by checkpoint code
	desc = "";
	icon = "share/textures/rotc/checkpoint_neutral.png";
	groundShape = "share/shapes/rotc/waypoints/shape.dts";
};

//
// other waypoints...
//

datablock RotcWaypointData(Team1Waypoint)
{
	name = ""; // will probably be overridden...
	desc = "";
	icon = "share/textures/rotc/checkpoint_neutral.png";
	groundShape = "share/shapes/rotc/waypoints/shape.dts";
};
