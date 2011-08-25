//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

datablock StaticShapeData(GridBeacon)
{
	shadowEnable = true;
	shapeFile = "share/shapes/rotc/gridbeacon.dts";
	emap = true;	
	shapeFxTexture[0] = "share/textures/rotc/connection2.png";
	shapeFxTexture[1] = "share/textures/rotc/heating.png";
	shapeFxColor[0] = "1.0 1.0 1.0 1.0";  
 	shapeFxColor[1] = "1.0 0.0 0.0 1.0"; 
 	shapeFxColor[2] = "1.0 1.0 1.0 1.0";  

	// Script fields...
	category = "Grid Wars Structures"; // for the mission editor
};

function GridBeacon::onAdd(%this, %obj)
{
	//error("GridBeacon::onAdd()");
	Parent::onAdd(%this, %obj);

	$Round.beacons.add(%obj);

	//%obj.startFade(0, 0, true);

	%obj.shapeFxSetTexture(0, 0);
	%obj.shapeFxSetColor(0, %obj.getTeamId());
	%obj.shapeFxSetActive(0, true, false);

	%obj.shapeFxSetTexture(1, 1);
	%obj.shapeFxSetColor(1, %obj.getTeamId());
	%obj.shapeFxSetBalloon(1, 1, 0);
	%obj.shapeFxSetActive(1, true, false);

	//%obj.freezeThread = schedule(250, %obj, "checkFreeze", %obj);
}

