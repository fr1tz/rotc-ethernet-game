//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - fullcontrol.cs
// Code for "full control" remote control
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------

function serverCmdToggleFullControl(%client, %take)
{
	%client.toggleFullControl(%take);
}

//------------------------------------------------------------------------------

function GameConnection::toggleFullControl(%this, %take)
{
	if(%take)
	{
		%control = %this.getControlObject();
		%newControl = %control.getCurrTagged();
		if(!%newControl)
			return;

		%name = %newControl.getShapeName();

		if(%newControl == %control)
			return;

		if(%newControl.teamId != %this.team.teamId)
		{
			bottomPrint(%this, %name SPC "is not on your team!", 3, 1 );
			return;
		}
		
		if(%newControl.client)
		{
			bottomPrint(%client, %name SPC "can not be put under full control!", 3, 1 );
			return;
		}

		if( %newControl.getControllingClient()
		|| (%newControl.isUnderSimpleControl() && !%newControl.isUnderSimpleControlBy(%this)))
		{
			bottomPrint(%this, %name SPC "is already under someone else's control!", 3, 1 );
			return;
		}

		%this.takeFullControl(%newControl);
	}
	else
	{
		%this.clearFullControl();

		if(%this.player)
			%this.control(%this.player);
		else
			%this.control(%this.camera);
	}
}

//------------------------------------------------------------------------------

function GameConnection::takeFullControl(%this, %obj)
{
	%this.clearFullControl();
	%this.control(%obj);
	%this.rcobj = %obj;
	%this.rcobj.getDataBlock().onFullControlTaken(%this.rcobj, %this);
}

function GameConnection::clearFullControl(%this)
{
	if(%this.rcobj)
		%this.rcobj.getDataBlock().onFullControlRelinquished(%this.rcobj, %this);

	%this.rcobj = 0;
}

//------------------------------------------------------------------------------

// script function used to to implement "full control"
function ShapeBaseData::onFullControlTaken(%this, %obj, %client)
{
	if(!%obj.isUnderSimpleControl())
	{
		%obj.getHudInfo().markAsControlled(%client, 0);
		%newName = getTaggedString(%client.name) @ "'s " @ %this.name;
		%obj.setShapeName(%newName);
	}
}

// script function used to to implement "full control"
function ShapeBaseData::onFullControlRelinquished(%this, %obj, %client)
{
	if(!%obj.isUnderSimpleControl())
	{
		%obj.getHudInfo().markAsControlled(0, 0);
		%obj.setShapeName(%this.name);
	}
}

