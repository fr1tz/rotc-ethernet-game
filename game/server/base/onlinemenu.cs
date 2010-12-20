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

function om_head(%client, %title, %prev, %refresh)
{
	%r = "<just:center><spush><shadowcolor:888888><linkcolor:000000><shadow:1:1><font:Arial:16>";

	if(%client.loadingMission)
	{
		%r = %r @	
			"No actions available while arena is loading" @
			"";
	}
	else
	{
		%r = %r @	
			"<a:cmd JoinTeam 0>Join observers</a>    " @
			"<a:cmd JoinTeam 1>Join Reds</a>     " @
			"<a:cmd JoinTeam 2>Join Blues</a>" @
			"";
	}

	%r = %r @ " <spop>\n<bitmap:share/misc/ui/sep.png>\n\n<just:left>";

	if(%prev !$= "")
	{
		%r = %r @ "\<\< <a:cmd" SPC %prev @ ">Back</a>\n\n";
	}

	if(%title !$= "")
	{
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
	}

	return %r;
}