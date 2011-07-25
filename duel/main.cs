//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Package overrides to initialize the mod.

package Duel {

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

	echo("\n--------- Initializing MOD: Duel ---------");
 	exec("./server.cs");
}

function onExit()
{
	Parent::onExit();
}

}; // package Duel

activatePackage(Duel);
