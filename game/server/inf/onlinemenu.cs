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
	return "<font:NovaSquare:16><linkcolor:FF00FF>";
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
			"<spop>\n";

        %r = %r @ "<just:center><spush><color:FFFF00>";
		%r = %r @ "Note: This is not ROTC: Ethernet!\n";
		%r = %r @ "You are playing a very early and incomplete version of Infantry (Ethernet)!\n";
		%r = %r @ "<bitmap:share/ui/rotc/construction><sbreak>";
        %r = %r @ "<spop><just:left>";
	}

	return %r;
}