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

package Deathmatch {

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
	echo("\n--------- Initializing MOD: Deathmatch ---------");
 	exec("./server.cs");

	Parent::onStart();
}

function onExit()
{
	Parent::onExit();
}

}; // package Deathmatch

activatePackage(Deathmatch);
