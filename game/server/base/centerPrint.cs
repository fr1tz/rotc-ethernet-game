//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

function centerPrintAll( %message, %time, %append )
{
	%count = ClientGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if( !%cl.isAIControlled() )
			centerPrint(%cl, %message, %time, %append);
	}
}

function bottomPrintAll( %message, %time )
{
	%count = ClientGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if( !%cl.isAIControlled() )
			bottomPrint(%cl, %message, %time);
	}
}

//-------------------------------------------------------------------------------------------------------

function centerPrint( %client, %message, %time, %append )
{
	commandToClient( %client, 'CenterPrint', %message, %time, %append );
}

function bottomPrint( %client, %message, %time )
{
	commandToClient( %client, 'BottomPrint', %message, %time );
}

//-------------------------------------------------------------------------------------------------------

function clearCenterPrint( %client )
{
	commandToClient( %client, 'ClearCenterPrint');
}

function clearBottomPrint( %client )
{
	commandToClient( %client, 'ClearBottomPrint');
}

//-------------------------------------------------------------------------------------------------------

function clearCenterPrintAll()
{
	%count = ClientGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if( !%cl.isAIControlled() )
			commandToClient( %cl, 'ClearCenterPrint');
	}
}

function clearBottomPrintAll()
{
	%count = ClientGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if( !%cl.isAIControlled() )
			commandToClient( %cl, 'ClearBottomPrint');
	}
}
