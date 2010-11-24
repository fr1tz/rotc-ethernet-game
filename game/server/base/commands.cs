//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----
// commands.cs
// Misc. server commands avialable to clients
//-----

//-----------------------------------------------------------------------------
// Inventory server commands
//-----------------------------------------------------------------------------

function serverCmdUseInv(%client, %data)
{
	if( %client.player == 0 ) return false;
	%client.getControlObject().use(%data);
}

function serverCmdUseWeapon(%client, %nr)
{
	if( %client.player == 0 ) return false;
	%client.getControlObject().useWeapon(%nr);
}

function serverCmdUseNextWeapon(%client)
{
	if( %client.player == 0 ) return false;
	%image = %client.player.getDataBlock().weapon[%client.currWeapon+1];
	if( isObject(%image) )
	{
		%client.player.mountWeaponImage(%image);
		%client.currWeapon++;
	}
}

function serverCmdUsePrevWeapon(%client)
{
	if( %client.player == 0 ) return false;
	%image = %client.player.getDataBlock().weapon[%client.currWeapon-1];
	if( isObject(%image) && %client.currWeapon-1 != 0 )
	{
		%client.player.mountWeaponImage(%image);
		%client.currWeapon--;
	}
}

//-----------------------------------------------------------------------------

function serverCmdToggleCamera(%client)
{
	if ($Server::TestCheats || $Server::ServerType $= "SinglePlayer")
	{
		%control = %client.getControlObject();
		if (%control == %client.player)
		{
			%control = %client.camera;
			%control.mode = toggleCameraFly;
		}
		else
		{
			%control = %client.player;
			%control.mode = observerFly;
		}
		%client.setControlObject(%control);
	}
}

function serverCmdDropPlayerAtCamera(%client)
{
	if ($Server::TestCheats)
	{
		if (!%client.player.isMounted())
			%client.player.setTransform(%client.camera.getTransform());
		%client.player.setVelocity("0 0 0");
		%client.setControlObject(%client.player);
	}
}

function serverCmdDropCameraAtPlayer(%client)
{
	if ($Server::TestCheats)
	{
		%client.camera.setTransform(%client.player.getEyeTransform());
		%client.camera.setVelocity("0 0 0");
		%client.setControlObject(%client.camera);
	}
}


//-----------------------------------------------------------------------------

function serverCmdJoinTeam(%client, %teamId)
{
	%client.joinTeam(%teamId);
}

function serverCmdTogglePlayerForm(%client)
{
	%client.togglePlayerForm();
}


function serverCmdSuicide(%client)
{
	if (isObject(%client.player))
		%client.player.kill("Suicide");
}	

function serverCmdPlayParty(%client,%anim)
{
	if (isObject(%client.player))
		%client.player.playPartyAnimation(%anim);
}

function serverCmdPlayDeath(%client)
{
	if (isObject(%client.player))
		%client.player.playDeathAnimation();
}

function serverCmdSelectObject(%client, %mouseVec, %cameraPoint)
{
	//Determine how far should the picking ray extend into the world?
	%selectRange = 200;
	// scale mouseVec to the range the player is able to select with mouse
	%mouseScaled = VectorScale(%mouseVec, %selectRange);
	// cameraPoint = the world position of the camera
	// rangeEnd = camera point + length of selectable range
	%rangeEnd = VectorAdd(%cameraPoint, %mouseScaled);

	// Search for anything that is selectable � below are some examples
	%searchMasks = $TypeMasks::PlayerObjectType | $TypeMasks::CorpseObjectType |
						$TypeMasks::ItemObjectType | $TypeMasks::TriggerObjectType;

	// Search for objects within the range that fit the masks above
	// If we are in first person mode, we make sure player is not selectable by setting fourth parameter (exempt
	// from collisions) when calling ContainerRayCast
	%player = %client.player;
	if ($firstPerson)
	{
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks, %player);
	}
	else //3rd person - player is selectable in this case
	{
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks);
	}

	// a target in range was found so select it
	if (%scanTarg)
	{
		%targetObject = firstWord(%scanTarg);

		%client.setSelectedObj(%targetObject);
	}
}

function serverCmdInstantGrenadeThrow(%client)
{
    %player = %client.player;
	if(!isObject(%player))
        return;

    %player.doInstantGrenadeThrow = true;
    %player.setImageTrigger(2, true);
    %player.setImageTrigger(2, false);
}

//-----------------------------------------------------------------------------

function serverCmdMainMenu(%client)
{
	%newtxt = "";
	%client.clearMenuText();

	%newtxt = %newtxt @
		"<spush><font:Arial:24>Main Menu<spop>\n\n" @	
		"<spush><font:Arial:18>" @
		"Welcome to" SPC $Server::MissionType SPC 
		$Server::MissionName @ 
		"<spop>\n\n" @
		"Hosted by" SPC $Pref::Server::Name @ "\n\n" @
		"";

	if(%client.loadingMission || %client.menu $= "mainmenu")
	{
		%newtxt = %newtxt @
			"If you're playing this arena for the first time, loading" SPC
			"might take\nsome time while the game downloads needed" SPC
			"art from the server.\nConsider using the time to read up on" SPC
			"<a:cmd HowToPlay>how to play in this arena</a>.\n" @
			"This main menu can be used to join the game once loading" SPC
			"has finished.\n\n" @
			"";
	}

	%newtxt = %newtxt @	
		"<spush><font:Arial:16>Actions<spop>\n" @
		"";

	if(%client.loadingMission)
	{
		%newtxt = %newtxt @	
			"   No actions available while arena is loading.\n" @
			"";
	}
	else
	{
		%newtxt = %newtxt @	
			"   [<a:cmd JoinTeam 0>Become observer</a>]\n" @
			"   [<a:cmd JoinTeam 1>Become red</a>]\n" @
			"   [<a:cmd JoinTeam 2>Become blue</a>]\n" @
			"";
	}

	%newtxt = %newtxt @	
		"\n<spush><font:Arial:16>Information<spop>\n" @
		"   \>\> <a:cmd ShowPlayerList>Player statistics</a>\n" @
		"   \>\> <a:cmd HowToPlay>How to play in this arena?</a>\n" @
		"";

	%client.addMenuText(%newtxt);

	%client.menu = "mainmenu";
}

function serverCmdShowPlayerList(%client, %show)
{
	%newtxt = "";
	%client.clearMenuText();

	%newtxt = %newtxt @
		"\<\< <a:cmd MainMenu>Back</a>\n\n" @
		"<spush><font:Arial:24>Player statistics" SPC
		"[ <a:cmd ShowPlayerList" SPC %show @ ">Refresh</a> ]<spop>\n\n" @
		"";

	if(%show $= "")
		%show = "latency";

	%array = new Array();

	%count = ClientGroup.getCount();
	for(%cl= 0; %cl < %count; %cl++)
	{
		%k = ClientGroup.getObject(%cl);
		%p = %k.pstats;

		if(%show $= "latency")
			%v = %k.getPing();
		else if(%show $= "dmgratio")
			%v = trimStat(%p.totalDmgCaused / %p.totalDmgTaken);
		else if(%show $= "healthlost")
			%v = trimStat((%p.totalHealthLost-%p.totalHealthRegained)/%p.timePlayed);
		else if(%show $= "totalF")
			%v = trimStat(%p.totalEffectiveness);
		else if(%show $= "discF")
			%v = trimStat(%p.discEffectiveness);
		else if(%show $= "grenadeF")
			%v = trimStat(%p.grenadeEffectiveness);
		else if(%show $= "tridentF")
			%v = trimStat(%p.tridentEffectiveness);
		else if(%show $= "mgF")
			%v = trimStat(%p.mgEffectiveness);
		else if(%show $= "sniperF")
			%v = trimStat(%p.sniperEffectiveness);
		else if(%show $= "glF")
			%v = trimStat(%p.glEffectiveness);
		else if(%show $= "PvE")
			%v = trimStat(%p.forceDmgTaken / %p.timePlayed);
		else if(%show $= "chuck")
			%v = trimStat(%p.totalDmgTaken / %p.totalHealthLost);
		else if(%show $= "time")
			%v = trimStat(%p.timePlayed);
			
		%array.push_back(%k, %v);
	}

	%array.sortnd();

	if(%show $= "latency")
		%showtext = "Latency (MS)";
	else if(%show $= "dmgratio")
		%showtext = "Damage ratio (total dmg caused / taken)";
	else if(%show $= "healthlost")
		%showtext = "Effective health loss ( (lost - regained) / time played )";
	else if(%show $= "totalF")
		%showtext = "Weapon effectiveness total (%)";
	else if(%show $= "discF")
		%showtext = "Disc effectiveness (%)";
	else if(%show $= "grenadeF")
		%showtext = "Grenade effectiveness (%)";
	else if(%show $= "tridentF")
		%showtext = "Trident effectiveness (%)";
	else if(%show $= "mgF")
		%showtext = "MG effectiveness (%)";
	else if(%show $= "sniperF")
		%showtext = "Sniper effectiveness (%)";
	else if(%show $= "glF")
		%showtext = "GL effectivenes (%)";
	else if(%show $= "PvE")
		%showtext = "PvE (Damage taken from environment / time played)";
	else if(%show $= "chuck")
		%showtext = "Invincibility (total damage taken / total health lost)";
	else if(%show $= "time")
		%showtext = "Time played (mins)";

	%newtxt = %newtxt @
		"Show:\n" @
		"<lmargin:25>" @
		"<a:cmd ShowPlayerList latency>Latency</a> |" SPC
		"<a:cmd ShowPlayerList dmgratio>Damage ratio</a> |" SPC
		"<a:cmd ShowPlayerList healthlost>Effective health loss</a> |" SPC
		"\nWeapon effectiveness:" SPC
		"<a:cmd ShowPlayerList totalF>Total</a>," SPC
		"<a:cmd ShowPlayerList discF>Disc</a>," SPC
		"<a:cmd ShowPlayerList grenadeF>Grenade</a>," SPC
		"<a:cmd ShowPlayerList tridentF>Trident</a>," SPC
		"<a:cmd ShowPlayerList mgF>MG</a>," SPC
		"<a:cmd ShowPlayerList sniperF>Sniper</a>," SPC
		"<a:cmd ShowPlayerList glF>GL</a> |\n" SPC
		"<a:cmd ShowPlayerList PvE>PvE</a> |" SPC
		"<a:cmd ShowPlayerList chuck>Invincibility</a> |" SPC
		"<a:cmd ShowPlayerList time>Time played</a>" @
		"<lmargin:0>\n\n" @
		"<tab:25, 125, 200, 300, 400>" @
		"\tName\tTeam" TAB %showtext @ "\n\n" @
		"";

	%idx = %array.moveFirst();
	while(%idx != -1)
	{
		%k = %array.getKey(%idx);
		%v = %array.getValue(%idx);
		
		%name = %k.nameBase;
		if(%k.team == $Team0)
			%team = "Observers";
		else if(%k.team == $Team1)
			%team = "Reds";
		else
			%team = "Blues";

		if(%k == %client)
			%newtxt = %newtxt @ "<spush><color:FF00FF>";

		%newtxt = %newtxt @ 
			"\>\>\t<a:cmd ShowPlayerInfo" SPC %k @ ">" @ %name @ "</a>" TAB %team TAB %v @ "\n";

		if(%k == %client)
			%newtxt = %newtxt @ "<spop>";

		%idx = %array.moveNext();
	}

	%client.addMenuText(%newtxt);

	%array.delete();

	%client.menu = "playerlist";
}

function serverCmdShowPlayerInfo(%client, %player)
{
	%p = %player.pstats;
	if(!isObject(%p))
		return;

	%client.clearMenuText();
	%client.addMenuText(
		"\<\< <a:cmd ShowPlayerList>Back</a>\n\n" @
		"<spush><font:Arial:24>Info on" SPC %player.nameBase @ 
		" [ <a:cmd ShowPlayerInfo" SPC %player @ ">Refresh</a> ]<spop>\n\n" @
		"(last updated @" SPC removeDecimals(%p.lastUpdate) SPC "secs)\n\n" @
		"<tab:200>" @
		"Time played:" TAB trimStat(%p.timePlayed) SPC "mins" @ "\n" @
		"Total damage caused:" TAB trimStat(%p.totalDmgCaused)  @ "\n" @
		"Total damage taken:" TAB trimStat(%p.totalDmgTaken) @ "\n" @
		"Total health lost:" TAB trimStat(%p.totalHealthLost) @ "\n" @
		"Total health regained:" TAB trimStat(%p.totalHealthRegained) @ "\n" @
		"\n<tab:150,200,250,300,350,400,450>" @
		"\tDisc\tGrenade\tTrident\tMG\tSniper\tGL\tEnvironment\n\n" @
		"Damage caused:" TAB 
		trimStat(%p.discDmgCaused) TAB
		trimStat(%p.grenadeDmgCaused) TAB
		trimStat(%p.tridentDmgCaused) TAB
		trimStat(%p.mgDmgCaused) TAB
		trimStat(%p.sniperDmgCaused) TAB
		trimStat(%p.glDmgCaused) TAB
		"\n\n" @
		"Damage taken:" TAB 
		trimStat(%p.discDmgTaken) TAB
		trimStat(%p.grenadeDmgTaken) TAB
		trimStat(%p.tridentDmgTaken) TAB
		trimStat(%p.mgDmgTaken) TAB
		trimStat(%p.sniperDmgTaken) TAB
		trimStat(%p.glDmgTaken) TAB
		trimStat(%p.forceDmgTaken) TAB
		"\n\n" @
		"Health lost:" TAB 
		trimStat(%p.discHealthLost) TAB
		trimStat(%p.grenadeHealthLost) TAB
		trimStat(%p.tridentHealthLost) TAB
		trimStat(%p.mgHealthLost) TAB
		trimStat(%p.sniperHealthLost) TAB
		trimStat(%p.glHealthLost) TAB
		trimStat(%p.forceHealthLost) TAB
		"\n\n" @
		"\nWeapon effectiveness:\n\n" @
		"<tab:75,150>" @
		"\tFired\tEffectiveness\n\n" @
		"Disc" TAB %p.discFired TAB trimStat(%p.discEffectiveness) @ "%" @ "\n\n" @
		"Grenade" TAB %p.grenadeFired TAB trimStat(%p.grenadeEffectiveness) @ "%" @ "\n\n" @
		"Trident" TAB %p.tridentFired TAB trimStat(%p.tridentEffectiveness) @ "%" @ "\n\n" @
		"MG" TAB %p.mgFired TAB trimStat(%p.mgEffectiveness) @ "%" @ "\n\n" @
		"Sniper" TAB %p.sniperFired TAB trimStat(%p.sniperEffectiveness) @ "%" @ "\n\n" @
		"GL" TAB %p.glFired TAB trimStat(%p.glEffectiveness) @ "%" @ "\n\n" @
		"Total" TAB %p.totalFired TAB trimStat(%p.totalEffectiveness) @ "%" @ "\n\n" @
		""
	);

	%client.menu = "playerinfo";
}
