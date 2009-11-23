//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Package overrides to initialize the mod.

package Railgun {

function displayHelp()
{
	Parent::displayHelp();
}

function parseArgs()
{
	Parent::parseArgs();
}

function onStart()
{
	Parent::onStart();
 
	echo("\n--------- Initializing MOD: Railgun ---------");
 	exec("./server.cs");
}

function onExit()
{
	Parent::onExit();
}

}; // package Railgun

activatePackage(Railgun);
