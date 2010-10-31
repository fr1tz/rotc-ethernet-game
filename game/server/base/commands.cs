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
	%client.clearMenuText();
	%client.addMenuText(
		"Welcome to" SPC $Pref::Server::Name SPC ".\n\n" @
		$Pref::Server::Info @ "\n\n" @
		"<spush><font:Arial:24>Main Menu<spop>\n\n" @
		"   [<a:cmd JoinTeam 0>Become observer</a>]\n" @
		"   [<a:cmd JoinTeam 1>Become red</a>]\n" @
		"   [<a:cmd JoinTeam 2>Become blue</a>]\n\n" @
		"   \>\> <a:cmd ShowPlayerList>Detailed Player List</a>\n" @
		""
	);
}

function serverCmdShowPlayerList(%client, %show)
{
	%client.clearMenuText();
	%client.addMenuText(
		"\<\< <a:cmd MainMenu>Back</a>\n\n" @
		"<spush><font:Arial:24>Detailed Player List" SPC
		"[ <a:cmd ShowPlayerList" SPC %show @ ">Refresh</a> ]<spop>\n\n" @
		""
	);

	if(%show $= "")
		%show = "latency";

	%array = new Array();

	%count = ClientGroup.getCount();
	for(%cl= 0; %cl < %count; %cl++)
	{
		%k = ClientGroup.getObject(%cl);
		%s = %k.stats;

		if(%show $= "latency")
			%v = %k.getPing();
		else if(%show $= "dmgdealt")
			%v = %s.damageDealt;
		else if(%show $= "dmgtaken")
			%v = %s.healthLost;
		else if(%show $= "dmgratio")
			%v = trimStat(%s.damageDealt / %s.healthLost);
		else if(%show $= "time")
			%v = trimStat(($Sim::Time-%s.joinTime)/60);
			
		%array.push_back(%k, %v);
	}

	%array.sortnd();

	if(%show $= "latency")
		%showtext = "Latency (MS)";
	else if(%show $= "dmgdealt")
		%showtext = "Damage dealt";
	else if(%show $= "dmgtaken")
		%showtext = "Damage taken";
	else if(%show $= "dmgratio")
		%showtext = "Damage ratio";
	else if(%show $= "time")
		%showtext = "Time played (mins)";

	%client.addMenuText(
		"Show:\n" @
		"<lmargin:25>" @
		"<a:cmd ShowPlayerList latency>Latency</a> |" SPC
		"<a:cmd ShowPlayerList dmgdealt>Damage dealt</a> |" SPC
		"<a:cmd ShowPlayerList dmgtaken>Damage taken</a> |" SPC
		"<a:cmd ShowPlayerList dmgratio>Damage ratio</a> |" SPC
		"<a:cmd ShowPlayerList time>Time played</a>" @
		"<lmargin:0>\n\n" @
		"<tab:100, 175, 300, 400>" @
		"Name\tTeam" TAB %showtext @ "\n\n" @
		""
	);

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
			%client.addMenuText("<spush><color:FF00FF>");

		%client.addMenuText(%name TAB %team TAB %v TAB 
			"\>\> <a:cmd ShowPlayerInfo" SPC %k @ ">More</a>\n");

		if(%k == %client)
			%client.addMenuText("<spop>");

		%idx = %array.moveNext();
	}


	%array.delete();
}

function serverCmdShowPlayerInfo(%client, %player)
{
	if(!isObject(%player))
		return;

	%stats = %player.stats;

	%client.clearMenuText();
	%client.addMenuText(
		"\<\< <a:cmd ShowPlayerList>Back</a>\n\n" @
		"<spush><font:Arial:24>Info on" SPC %player.nameBase @ 
		" [ <a:cmd ShowPlayerInfo" SPC %player @ ">Refresh</a> ]<spop>\n\n" @
		"<tab:100, 200, 300, 400>" @
		"Time played:" TAB trimStat(($Sim::Time-%stats.joinTime)/60) SPC "mins" @ "\n" @
		"Damage dealt:" TAB trimStat(%stats.damageDealt) @ "\n" @
		"Damage taken:" TAB trimStat(%stats.healthLost) @ "\n" @
		"Damage ratio:" TAB trimStat(%stats.damageDealt / %stats.healthLost) @ "\n" @
		""
	);
}