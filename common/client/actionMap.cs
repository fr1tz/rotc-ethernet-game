//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//------------------------------------------------------------------------------
// All Pushing and popping of action maps should go through these functions:
//------------------------------------------------------------------------------

function pushActionMap(%actionMap)
{
	echo("Pushing action map:" SPC %actionMap);
	%actionMap.push();
	if(%actionMap.pushCallback !$= "")
		call(%actionMap.pushCallback);
}

function popActionMap(%actionMap)
{
	echo("Popping action map:" SPC %actionMap);
	%actionMap.pop();
	if(%actionMap.popCallback !$= "")
		call(%actionMap.popCallback);
}

//------------------------------------------------------------------------------
// Utility remap functions:
//------------------------------------------------------------------------------

function ActionMap::copyBind( %this, %otherMap, %command )
{
	if ( !isObject( %otherMap ) )
	{
		error( "ActionMap::copyBind - \"" @ %otherMap @ "\" is not an object!" );
		return;
	}

	%bind = %otherMap.getBinding( %command );
	if ( %bind !$= "" )
	{
		%device = getField( %bind, 0 );
		%action = getField( %bind, 1 );
		%flags = %otherMap.isInverted( %device, %action ) ? "SDI" : "SD";
		%deadZone = %otherMap.getDeadZone( %device, %action );
		%scale = %otherMap.getScale( %device, %action );
		%this.bind( %device, %action, %flags, %deadZone, %scale, %command );
	}
}

//------------------------------------------------------------------------------
function ActionMap::blockBind( %this, %otherMap, %command )
{
	if ( !isObject( %otherMap ) )
	{
		error( "ActionMap::blockBind - \"" @ %otherMap @ "\" is not an object!" );
		return;
	}

	%bind = %otherMap.getBinding( %command );
	if ( %bind !$= "" )
		%this.bind( getField( %bind, 0 ), getField( %bind, 1 ), "" );
}

