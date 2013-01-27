//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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
	echo("\n--------- Initializing MOD: Railgun ---------");
 	exec("./server.cs");

	Parent::onStart();
}

function onExit()
{
	Parent::onExit();
}

}; // package Railgun

activatePackage(Railgun);
