//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Client side of the scripted material mapping system.
//------------------------------------------------------------------------------

addMessageCallback( 'MsgMaterialMapping', handleMaterialMappingMessage );

function handleMaterialMappingMessage(%msgType, %msgString,
	%material, %sound, %color, %detail, %envmap)
{
	addMaterialMapping(
		%material,
		%sound  $= "" ? "" : "sound:" SPC %sound,
		%color  $= "" ? "" : "color:" SPC %color,
		%detail $= "" ? "" : "detail:" SPC %detail,
		%envmap $= "" ? "" : "environment:" SPC %envmap
	);
}
