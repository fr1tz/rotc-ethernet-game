//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

function om_init()
{
	return "<font:NovaSquare:16>";
}

function om_head(%client, %title, %prev, %refresh)
{
	%r = "";

	if(%prev !$= "")
	{
		%r = %r @ "\<\< <a:cmd" SPC %prev @ ">Back</a>\n\n";
	}

	if(%title !$= "")
	{
		%r = %r @
			"<spush><font:NovaSquare:24>" @ %title;

		if(%refresh !$= "")
		{
			%r = %r SPC
				"[ <a:cmd" SPC %refresh @ ">Refresh</a> ]" @
			"";
		}

		%r = %r @
			"<spop>\n\n";
	}

	return %r;
}