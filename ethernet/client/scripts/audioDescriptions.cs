//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine
// Copyright (c) 2001 GarageGames.Com
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Channel assignments
// (Keep these in sync with ethernet/server/scripts/audioDescriptions.cs!)
$GuiAudioType	  = 1;
$SimAudioType	  = 2;
$MessageAudioType = 3;
//-----------------------------------------------------------------------------

new AudioDescription(AudioGui)
{
	volume	= 1.0;
	isLooping= false;
	is3D	  = false;
	type	  = $GuiAudioType;
};

new AudioDescription(AudioMessage)
{
	volume	= 1.0;
	isLooping= false;
	is3D	  = false;
	type	  = $MessageAudioType;
};
