//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Hook into the mission editor.

function StaticShapeData::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type.
	//error("StaticShapeData::create()");
	if(false)
	{
		return %data.create();
	}
	else
	{
		%obj = new StaticShape() {
			dataBlock = %data;
		};
		return %obj;
	}	
}

//------------------------------------------------------------------------------

function StaticShapeData::onAdd(%this,%obj)
{
	//error("StaticShapeData::onAdd()");
	%obj.playThread(0,"ambient");
}
