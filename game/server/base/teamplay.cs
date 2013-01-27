//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - teamplay.cs
// Stuff concerning team-play
//------------------------------------------------------------------------------

function serverCmdTeamplayCommand(%client, %action, %otherClient)
{
	if( !isObject(%client.player) ) return;
	
	%player = %client.player;

	// create waypoint?...
	if(%action $= "w")
	{
		if( isObject(%client.waypoint) )
			%client.waypoint.delete();
		
		%pos = %player.getCurrTaggedPos();
		%pos = getTerrainLevel(%pos);
		%x = getWord(%pos, 0);
		%y = getWord(%pos, 1);
		%z = getWord(%pos, 2) + 2.0;
		%pos = %x @ " " @ %y @ " " @ %z;

		%waypoint = new RotcWaypoint() {
			dataBlock = Team1Waypoint;
			name = %player.getShapeName() @ "'s waypoint";
		};
		%waypoint.setTransform(%pos SPC "0 0 0 0");
		%waypoint.updateClients();
		
		%client.waypoint = %waypoint;
	}
	
	// get curr tagged pos from other player?...
	if(%action $= "c")
	{
		if( isObject(%otherClient.player) )
			%player.setCurrTaggedPos(%otherClient.player.getCurrTaggedPos());
	}
}

function serverCmdCycleThroughTaggedEnemiesForward(%client)
{
	if( !isObject(%client.player) ) return;
	
	%player = %client.player;
	
	%candidateFound = false;
	%selectNextCandidate = false;
	%currTagged = %player.getCurrTagged();
	if( !isObject(%currTagged) )
		%selectNextCandidate = true;

	for(%i = 0; %i < MissionCleanup.getCount(); %i++)
	{
		%obj = MissionCleanup.getObject(%i);
		if( (%obj.getType() & $TypeMasks::ShapeBaseObjectType)
			 && %obj.teamId != %player.teamId
			 && %obj.isTagged() )
		{
			if( %obj != %currTagged )
				%candidateFound = true;
		
			if(%selectNextCandidate)
			{
				%player.setCurrTagged(%obj);
				return;
			}
			else
			{
				if( %obj == %currTagged )
					%selectNextCandidate = true;
			}
		}
	}

	if( %candidateFound )
		for(%i = 0; %i < MissionCleanup.getCount(); %i++)
		{
			%obj = MissionCleanup.getObject(%i);
			if( (%obj.getType() & $TypeMasks::ShapeBaseObjectType)
				 && %obj.teamId != %player.teamId
				 && %obj.isTagged() )
			{
				if( %obj != %currTagged )
				{
					%player.setCurrTagged(%obj);
					return;
				}
			}
		}
}

function serverCmdCycleThroughTaggedEnemiesBackward(%client)
{
	if( !isObject(%client.player) ) return;

	%player = %client.player;

	%candidateFound = false;
	%selectNextCandidate = false;
	%currTagged = %player.getCurrTagged();
	if( !isObject(%currTagged) )
		%selectNextCandidate = true;

	for(%i = MissionCleanup.getCount() - 1; %i >= 0; %i--)
	{
		%obj = MissionCleanup.getObject(%i);
		if( (%obj.getType() & $TypeMasks::ShapeBaseObjectType)
			 && %obj.teamId != %player.teamId
			 && %obj.isTagged() )
		{
			if( %obj != %currTagged )
				%candidateFound = true;

			if(%selectNextCandidate)
			{
				%player.setCurrTagged(%obj);
				return;
			}
			else
			{
				if( %obj == %currTagged )
					%selectNextCandidate = true;
			}
		}
	}

	if( %candidateFound )
		for(%i = MissionCleanup.getCount() - 1; %i >= 0; %i--)
		{
			%obj = MissionCleanup.getObject(%i);
			if( (%obj.getType() & $TypeMasks::ShapeBaseObjectType)
				 && %obj.teamId != %player.teamId
				 && %obj.isTagged() )
			{
				if( %obj != %currTagged )
				{
					%player.setCurrTagged(%obj);
					return;
				}
			}
		}
}
