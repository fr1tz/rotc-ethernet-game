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
	return "<font:NovaSquare:16>";
}

function om_head(%client, %title, %prev, %refresh)
{
	%r = "<just:center><spush><font:NovaSquare:12>";

	if(%client.loadingMission)
	{
		%r = %r @	
			"Arena is loading" @
			"";
	}
	else
	{
		%r = %r @	
			"<a:cmd JoinTeam 0>Join Observers</a>    " @
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