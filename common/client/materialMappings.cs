//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Client side of the scripted material mapping system.
//------------------------------------------------------------------------------

function clientCmdMaterialMapping(%material, %sound, %color, %detail, %envmap)
{
	addMaterialMapping(
		%material,
		%sound  $= "" ? "" : "sound:" SPC %sound,
		%color  $= "" ? "" : "color:" SPC %color,
		%detail $= "" ? "" : "detail:" SPC %detail,
		%envmap $= "" ? "" : "environment:" SPC %envmap
	);
}
