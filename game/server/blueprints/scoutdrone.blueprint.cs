//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock StaticShapeData(ScoutDroneBlueprint)
{
	category = "Blueprints"; // for the mission editor
	className = Blueprint;
	
	shadowEnable = false;
	
	shapeFile = "share/shapes/rotc/vehicles/scoutdrone/blueprint.dts";
   emap = false;	
   
	shapeFxTexture[0] = "share/textures/rotc/connection2.png";
	shapeFxTexture[1] = "share/textures/rotc/connection.png";
	shapeFxTexture[2] = "share/textures/rotc/locked.png";
	shapeFxTexture[3] = "share/textures/rotc/armor.white.png";
	shapeFxTexture[4] = "share/textures/rotc/armor.orange.png";

	shapeFxColor[0] = "0.5 0.5 0.5 1.0";  
	shapeFxColor[1] = "1.0 0.0 0.0 1.0";  
	shapeFxColor[2] = "1.0 0.5 0.0 1.0";  
	shapeFxColor[3] = "0.0 0.5 1.0 1.0";  
	shapeFxColor[4] = "1.0 0.0 0.0 1.0"; // repel hit
	shapeFxColor[5] = "1.0 0.5 0.5 1.0"; // repel missed
	shapeFxColor[6] = "0.0 0.5 0.0 1.0"; // permanently neutral zone   
};

function ScoutDroneBlueprint::onAdd(%this, %obj)
{
	//error("ScoutDroneBlueprint::onAdd()");

	Parent::onAdd(%this, %obj);
	
	%obj.startFade(0, 0, true);
	%obj.shapeFxSetActive(0, true, true);
	%obj.shapeFxSetActive(1, true, true);	
}

function ScoutDroneBlueprint::build(%this, %blueprint, %client)
{
	//error("ScoutDroneBlueprint::build()");
	
	%teamId = %client.team.teamId;
	
	%data = RedScoutDrone;
	
	%obj = new FlyingVehicle() {
		dataBlock = %data;
		client = %client;
		teamId = %teamId;
	};			
	
	return %obj;
}