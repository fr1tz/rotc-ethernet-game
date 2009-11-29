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
