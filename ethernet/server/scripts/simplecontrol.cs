//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - simplecontrol.cs
// Ccode for "simple control (tm)" remote control
//------------------------------------------------------------------------------

// mag: simple control is still buggy and currently not used

exec("./simplecontrol.gfx.cs");

//------------------------------------------------------------------------------

function serverCmdToggleSimpleControl(%client, %slot)
{
	%oldControl = 0;
	%newControl = 0;

	// if slot already controls an object, remove it from simple control...
	%client.simpleControl.moveFirst();
	%idx = %client.simpleControl.getIndexFromKey(%slot);
	if(%idx >= 0)
	{
		%oldControl = %client.simpleControl.getValue(%idx);
		%oldControl.removeFromSimpleControl();
	}
	
	%newControl = %client.getControlObject().getCurrTagged();
	if(!%newControl)
		return;
		
	%name = %newControl.getShapeName();
		
	if(%newControl == %oldControl)
		return;
		
	if(%newControl.teamId != %client.team.teamId)
	{
		bottomPrint(%client, %name SPC "is not on your team!", 3, 1 );
		return;
	}

	if(%newControl.client)
	{
		bottomPrint(%client, %name SPC "can not be put under simple control!", 3, 1 );
		return;
	}

	if(%newControl.getControllingClient() || %newControl.isUnderSimpleControl())
	{
		if(%newControl.isUnderSimpleControlBy(%client))
		{
			%newControl.removeFromSimpleControl();
		}
		else
		{
			bottomPrint(%client, %name SPC "is already under someone else's control!", 3, 1 );
			return;
		}
	}

	%newControl.addToSimpleControl(%client, %slot);
}

function serverCmdDoSimpleControlAction(%client, %slot)
{
	%idx = %client.simpleControl.getIndexFromKey(%slot);
	if(%idx >= 0)
	{
		%control = %client.simpleControl.getValue(%idx);
		%control.getDataBlock().doSimpleControlAction(%control, %client);
	}
}

//------------------------------------------------------------------------------

function GameConnection::clearSimpleControl(%this)
{
	if(!isObject(%this.simpleControl))
		return;

	%idx = %this.simpleControl.moveLast();
	while(%idx != -1)
	{
		%simpleControl = %this.simpleControl.getValue(%idx);
		if( isObject(%simpleControl) )
			%simpleControl.removeFromSimpleControl();
	
		%idx = %this.simpleControl.moveLast();
	}
	
	%this.simpleControl.moveFirst();
}
	
//------------------------------------------------------------------------------

function ShapeBase::isUnderSimpleControl(%this)
{
	return %this.simpleControlClient != 0;
}

function ShapeBase::isUnderSimpleControlBy(%this, %client)
{
	return %this.simpleControlClient == %client;
}

function ShapeBase::addToSimpleControl(%this, %client, %slot)
{
	if(%this.isUnderSimpleControl())
		return;
		
	%this.getHudInfo().markAsControlled(%client, %slot);

	%this.simpleControlClient = %client;
	%this.simpleControlSlot = %slot;

	%client.simpleControl.push_back(%slot, %this);
	
	%this.getDataBlock().onAddedToSimpleControl(%this, %client);
	
//	MessageClient(%client, 'MsgSimpleControlUpdate', "",
//		%slot, %client.getGhostID(%this));
}

function ShapeBase::removeFromSimpleControl(%this)
{
	if(!%this.isUnderSimpleControl())
		return;
		
	%this.getHudInfo().markAsControlled(0, 0);

	%client = %this.simpleControlClient;
	%slot	= %this.simpleControlSlot;

	%client.simpleControl.moveFirst();
	%idx = %client.simpleControl.getIndexFromKey(%slot);
	%client.simpleControl.erase(%idx);

	%this.simpleControlClient = 0;
	%this.simpleControlSlot	= 0;
	
	%this.getDataBlock().onRemovedFromSimpleControl(%this, %client);
	
//	MessageClient(%client, 'MsgSimpleControlUpdate', "",
//		%slot, 0);
}

//------------------------------------------------------------------------------

// script function used to to implement "simple control (tm)"
function ShapeBaseData::onAddedToSimpleControl(%this, %obj, %client)
{
	if(!%obj.getControllingClient())
	{
		%newName = getTaggedString(%client.name) @ "'s " @ %this.name;
		%obj.setShapeName(%newName);
	}
}

// script function used to to implement "simple control (tm)"
function ShapeBaseData::onRemovedFromSimpleControl(%this, %obj, %client)
{
	if(!%obj.getControllingClient())
	{
		%obj.setShapeName(%this.name);
	}
}

// script function used to to implement "simple control (tm)"
function ShapeBaseData::doSimpleControlAction(%this, %obj, %client)
{

}

