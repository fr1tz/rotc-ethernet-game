//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----
// audioDescriptions.cs
// audio descriptions used by ROTC
//-----

//-----------------------------------------------------------------------------
// Channel assignments
// (Keep these in sync with ethernet/client/scripts/audioDescriptions.cs!)
$GuiAudioType	  = 1;
$SimAudioType	  = 2;
$MessageAudioType = 3;
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// 3D Sounds
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Single shot sounds

datablock AudioDescription(AudioParty)
{
	volume	= 1.0;
	isLooping= false;

	is3D	  = true;
	ReferenceDistance= 60.0;
	MaxDistance= 100.0;
	type	  = $SimAudioType;
};

datablock AudioDescription(AudioDefault3D)
{
	volume	= 1.0;
	isLooping= false;

	is3D	  = true;
	ReferenceDistance= 20.0;
	MaxDistance= 100.0;
	type	  = $SimAudioType;
};

datablock AudioDescription(AudioClose3D)
{
	volume	= 1.0;
	isLooping= false;

	is3D	  = true;
	ReferenceDistance= 10.0;
	MaxDistance= 60.0;
	type	  = $SimAudioType;
};

datablock AudioDescription(AudioClosest3D)
{
	volume	= 1.0;
	isLooping= false;

	is3D	  = true;
	ReferenceDistance= 5.0;
	MaxDistance= 30.0;
	type	  = $SimAudioType;
};

datablock AudioDescription(AudioFar3D)
{
	volume				= 1.0;
	isLooping			= false;
	is3D				  = true;
	ReferenceDistance = 100.0;
	MaxDistance		 = 200.0;
	type				  = $SimAudioType;
};

datablock AudioDescription(AudioVeryFar3D)
{
	volume				= 1.0;
	isLooping			= false;
	is3D				  = true;
	ReferenceDistance = 200.0;
	MaxDistance		 = 400.0;
	type				  = $SimAudioType;
};


//-----------------------------------------------------------------------------
// Looping sounds

datablock AudioDescription(AudioDefaultLooping3D)
{
	volume	= 1.0;
	isLooping= true;

	is3D	  = true;
	ReferenceDistance= 20.0;
	MaxDistance= 100.0;
	type	  = $SimAudioType;
};

datablock AudioDescription(AudioCloseLooping3D)
{
	volume	= 1.0;
	isLooping= true;

	is3D	  = true;
	ReferenceDistance= 10.0;
	MaxDistance= 50.0;
	type	  = $SimAudioType;
};

datablock AudioDescription(AudioClosestLooping3D)
{
	volume	= 1.0;
	isLooping= true;

	is3D	  = true;
	ReferenceDistance= 5.0;
	MaxDistance= 30.0;
	type	  = $SimAudioType;
};

//-----------------------------------------------------------------------------
// 2D sounds
//-----------------------------------------------------------------------------

// Used for non-looping environmental sounds (like power on, power off)
datablock AudioDescription(Audio2D)
{
	volume = 1.0;
	isLooping = false;
	is3D = false;
	type = $SimAudioType;
};

// Used for Looping Environmental Sounds
datablock AudioDescription(AudioLooping2D)
{
	volume = 1.0;
	isLooping = true;
	is3D = false;
	type = $SimAudioType;
};

// Critical game events use a the GUI audio channel
datablock AudioDescription(AudioCritical2D)
{
	volume = 1.0;
	isLooping = false;
	is3D = false;
	type = $GuiAudioType;
};

//-----------------------------------------------------------------------------

