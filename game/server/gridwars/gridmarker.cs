//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

datablock StaticShapeData(GridMarker)
{
	shadowEnable = true;
	shapeFile = "share/shapes/rotc/green.dts";
	emap = true;	
	shapeFxTexture[0] = "share/textures/rotc/connection2.png";
	shapeFxTexture[1] = "share/textures/rotc/heating.png";
	shapeFxColor[0] = "1.0 1.0 1.0 1.0";  
 	shapeFxColor[1] = "1.0 0.0 0.0 1.0"; 
 	shapeFxColor[2] = "1.0 1.0 1.0 1.0";  
};

function GridMarker::onAdd(%this, %obj)
{
	//error("GridMarker::onAdd()");
	Parent::onAdd(%this, %obj);

	%obj.startFade(0, 0, true);

	%obj.shapeFxSetTexture(0, 1);
	%obj.shapeFxSetColor(0, 1);
	%obj.shapeFxSetBalloon(1, 1.2, 0);
	%obj.shapeFxSetActive(0, true, false);

}

