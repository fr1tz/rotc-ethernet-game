//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Server side of the scripted material mapping system.
//------------------------------------------------------------------------------

$MaterialMapping::Sound::Soft  = 0;
$MaterialMapping::Sound::Hard  = 1;
$MaterialMapping::Sound::Metal = 2;
$MaterialMapping::Sound::Snow  = 3;

// This function creates material mapping information on the server.
function createMaterialMapping(%material)
{
	if(!isObject($MaterialMappings))
	{
		error("createMaterialMapping(): MaterialMappings object does not exist!");
		return;
	}

	%obj = new ScriptObject()
	{
		material = %material;
		sound = "";
		color = "";
		detail = "";
		envmap = "";
	};
	
	$MaterialMappings.push_back(%material, %obj);
	
	return %obj;
}

// This functions sends the material mapping information to a client.
// It is then up to the client to actually load the mapping into the engine.
function sendMaterialMappingsToClient(%client)
{
	for( %i = 0; %i < $MaterialMappings.count(); %i++ )
	{
		%obj = $MaterialMappings.getValue(%i);
		messageClient( %client, 'MsgMaterialMapping', "",
			%obj.material, %obj.sound, %obj.color, %obj.detail, %obj.envmap);
	}
}
