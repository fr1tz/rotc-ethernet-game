//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Hook into the mission editor.

function StaticShapeData::create(%data)
{
	//error("StaticShapeData::create()");
	return %data.create();	
}

//------------------------------------------------------------------------------

function StaticShapeData::onAdd(%this,%obj)
{
	//error("StaticShapeData::onAdd()");
	%obj.playThread(0,"ambient");
}
