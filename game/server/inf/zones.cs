//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock TacticalZoneData(TerritoryZone)
{
	category = "Tactical Zones"; // for the mission editor

	// The period is value is used to control how often the console
	// onTick callback is called while there are any objects
	// in the zone.  The default value is 100 MS.
	tickPeriodMS = 200;

	colorChangeTimeMS = 200;

	colors[1]  = "1 1 1 0.1";  // neutral
	colors[2]  = "1 0 0 0.2";   // red
	colors[3]  = "0 0 1 0.2";   // blue
	colors[4]  = "1 0.5 0 0.2";   // red zBlocked
	colors[5]  = "0 0.5 1 0.2";   // blue zBlocked

	colors[6]  = "1 1 1 0.75";  // white flash
	colors[7]  = "1 0 0 0.75";   // red flash
	colors[8]  = "0 0 1 0.75";   // blue flash
	colors[9]  = "1 0.5 0 0.75";   // red zBlocked flash
	colors[10] = "0 0.5 1 0.75";   // blue zBlocked flash

	colors[11] = "0 1 0 0.75";   // bright green
	colors[12] = "1 1 0 0.75";   // bright yellow
	colors[13] = "0 1 0 0.1";   // constantly neutral
	colors[14] = "0 1 0 0.4";   // protected
	colors[15] = "1 1 1 1"; 

    texture = "share/textures/rotc/zone.grid";
};


