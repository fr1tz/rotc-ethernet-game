//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

function om_init()
{
	return "<font:Arial:16>";
}

function om_head(%title, %prev, %refresh)
{
	%r = "";
	if(%prev !$= "")
		%r = %r @ "\<\< <a:cmd" SPC %prev @ ">Back</a>\n\n";

	%r = %r @
		"<spush><font:Arial:24>" @ %title;

	if(%refresh !$= "")
	{
		%r = %r SPC
		"[ <a:cmd" SPC %refresh @ ">Refresh</a> ]" @
		"";
	}

	%r = %r @
		"<spop>\n\n";

	return %r;
}