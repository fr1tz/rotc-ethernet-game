//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Functions to deactivate and reactivate all action maps
//------------------------------------------------------------------------------

function deactivateActionMaps()
{
	if(!isObject($ActionMapStash))
	{
		$ActionMapStash = new SimSet();
		while(ActiveActionMapSet.getCount() > 1) // don't remove GlobalActionMap
		{
			%actionMap = ActiveActionMapSet.getObject(1);
			//echo("Stashing action map" SPC %actionMap.getName());
			$ActionMapStash.add(%actionMap);
			ActiveActionMapSet.remove(%actionMap);
		}
	}
}

function reactivateActionMaps()
{
	if(isObject($ActionMapStash))
	{
		for(%idx = 0; %idx < $ActionMapStash.getCount(); %idx++ )
		{
			%actionMap = $ActionMapStash.getObject(%idx);
			//echo("Reactivating stashed action map" SPC %actionMap.getName());
			ActiveActionMapSet.add(%actionMap);
		}
		$ActionMapStash.delete();
		$ActionMapStash = "";
	}
}

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

