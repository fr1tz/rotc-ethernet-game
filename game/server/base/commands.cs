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
// Server commands available to clients
//-----

function serverCmdPlayerAction(%client, %nr, %val)
{	
	if(%nr == 31 && %val)
	{	
		%client.topHudMenu = "newbiehelp";
		%client.setNewbieHelp("random", true);
		return;	
	}
	else if(%nr == 32 && %val)
	{		
		%client.switchTopHudMenuMode();
		return;
	}	

	%player = %client.player;
	if(%player == 0)
		return false;

	if(%nr == 0 && %val && %client.menuVisible == false)
	{
		%client.togglePlayerForm();
	}
	else if(%nr <= 10 && %val)
	{
		%client.getControlObject().useWeapon(%nr);
	}
	else if(%nr == 11 && %val)
	{
		%client.getControlObject().useWeapon(0);
	}
	else if(%nr == 12 && %val)
	{
		%client.player.fullForceGrenade = true;
		%client.player.setImageTrigger(2, true);
		%client.player.setImageTrigger(2, false);
	}
	else if(%nr == 13)
	{
		%client.getControlObject().useWeapon(6);
	}
	else if(%nr == 14)
	{
		%client.getControlObject().useWeapon(7);
	}
	else if(%nr == 17 && %val)
	{		
		deployRepel3(%player);
	}
	else if(%nr == 39 && %val)
	{		
		serverCmdToggleCamera(%client);
	}
}

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

function serverCmdMenuVisible(%client, %visible)
{
  %client.menuVisible = %visible;
}

//-----------------------------------------------------------------------------

function serverCmdMainMenu(%client)
{
	%newtxt = om_init();
	%client.beginMenuText(%client.menu $= "mainmenu");

	%newtxt = %newtxt @
		om_head(%client, "Arena Info") @
		"<spush><font:NovaSquare:20>" @
		"Welcome to" SPC $Server::MissionType SPC 
		$Server::MissionName @ 
		"<spop>\n\n<a:cmd News>What's new in" SPC $Server::MissionType @ "?</a>\n\n" @
		"Hosted by" SPC $Pref::Server::Name @ "\n\n" @
		"<spush>" @ $Pref::Server::Info @ "<spop>\n\n" @
		"";

	if(%client.loadingMission || %client.menu $= "mainmenu")
	{
		%newtxt = %newtxt @
			"If you're playing this arena for the first time, loading" SPC
			"might take\nsome time while the game downloads needed" SPC
			"art from the server.\nConsider using the time to read" SPC
			"<a:cmd HowToPlay 1>ROTC: Ethernet in a nutshell</a>.\n" @
			"";
	}

	%client.addMenuText(%newtxt);
	%client.endMenuText();

	%client.menu = "mainmenu";
}

function serverCmdNews(%client)
{
	%newtxt = om_init();
	%client.beginMenuText();

	if(%page $= "")
		%page = 1;

	%newtxt = %newtxt @ om_head(%client, "", "MainMenu");

	%filename = "NEWS";

	%file = new FileObject();
	%file.openForRead(%fileName);
	while(!%file.isEOF())
		%newtxt = %newtxt @ strreplace(%file.readLine(), "<br>", "\n") @ "\n";
	%file.delete();

	%client.addMenuText(%newtxt);
	%client.endMenuText();

	%client.menu = "news";
}


function serverCmdShowPlayerList(%client, %show)
{
	%newtxt = om_init();
	%client.beginMenuText(%client.menu $= "playerlist");

	%newtxt = %newtxt @ 
		om_head(%client, "Player List");

	if(%show $= "")
		%show = "dmgrating";

	%array = new Array();

	%count = ClientGroup.getCount();
	for(%cl= 0; %cl < %count; %cl++)
	{
		%k = ClientGroup.getObject(%cl);
		%p = %k.pstats;

		if(%show $= "latency")
			%v = %k.getPing();
		else if(%show $= "handicap")
			%v = mfloor(%k.handicap*100) @ "%";
		else if(%show $= "dmgrating")
			%v = trimStat(%p.totalDmgApplied / %p.timePlayed);            
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
		else if(%show $= "blasterF")
			%v = trimStat(%p.blasterEffectiveness);
		else if(%show $= "brF")
			%v = trimStat(%p.brEffectiveness);
		else if(%show $= "minigunF")
			%v = trimStat(%p.minigunEffectiveness);			
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
		%showtext[0] = "Latency (MS)";
	else if(%show $= "handicap")
	{
		%showtext[0] = "Handicap";
	}
	else if(%show $= "dmgrating")
	{
		%showtext[0] = "Damage rating";
		%showtext[1] = "(total dmg applied / time played)";		
	}
	else if(%show $= "dmgratio")
	{
		%showtext[0] = "Damage ratio";
		%showtext[1] = "(total dmg caused / taken)";
	}
	else if(%show $= "healthlost")
	{
		%showtext[0] = "Effective health loss";
		%showtext[1] = "( (lost - regained) / time played )";
	}
	else if(%show $= "totalF")
		%showtext[0] = "Weapon effectiveness total (%)";
	else if(%show $= "discF")
		%showtext[0] = "Disc effectiveness (%)";
	else if(%show $= "grenadeF")
		%showtext[0] = "Grenade effectiveness (%)";
	else if(%show $= "blasterF")
		%showtext[0] = "Blaster effectiveness (%)";
	else if(%show $= "brF")
		%showtext[0] = "Battle Rifle effectiveness (%)";
	else if(%show $= "minigunF")
		%showtext[0] = "Minigun effectiveness (%)";
	else if(%show $= "sniperF")
		%showtext[0] = "Sniper effectiveness (%)";
	else if(%show $= "glF")
		%showtext[0] = "GL effectivenes (%)";
	else if(%show $= "PvE")
	{
		%showtext[0] = "PvE (Damage taken from";
		%showtext[1] = "environment / time played)";
	}
	else if(%show $= "chuck")
	{
		%showtext[0] = "Invincibility (total damage";
		%showtext[1] = "taken / total health lost)";
	}
	else if(%show $= "time")
		%showtext[0] = "Time played (mins)";

	%newtxt = %newtxt @
		"Show:\n" @
		"<lmargin:25>" @
		"<a:cmd ShowPlayerList dmgrating>Damage rating</a> |" SPC
		"<a:cmd ShowPlayerList dmgratio>Damage ratio</a> |" SPC
		"<a:cmd ShowPlayerList healthlost>Effective health loss</a> |" SPC
		"<a:cmd ShowPlayerList PvE>PvE</a> |" SPC
		"<a:cmd ShowPlayerList chuck>Invincibility</a> |" SPC
		"\nWeapon effectiveness:\n   " SPC
		"<a:cmd ShowPlayerList totalF>Total</a>," SPC
		"<a:cmd ShowPlayerList discF>Disc</a>," SPC
		"<a:cmd ShowPlayerList grenadeF>Grenade</a>," SPC
		"<a:cmd ShowPlayerList blasterF>Blaster</a>," SPC
		"<a:cmd ShowPlayerList brF>BR</a>," SPC
		"<a:cmd ShowPlayerList sniperF>Sniper</a>," SPC		
		"<a:cmd ShowPlayerList minigunF>Minigun</a>," SPC
		"<a:cmd ShowPlayerList glF>GL</a>\n" SPC
		"<a:cmd ShowPlayerList time>Time played</a>" @
		"<lmargin:0>\n\n" @
		"<tab:25, 150, 200, 250, 350>" @
		"\tName/Handicap\tTeam\tPing" TAB %showtext[0] @ "\n\t\t\t\t" @ %showtext[1] @ "\n\n" @
		"";

	%idx = %array.moveFirst();
	while(%idx != -1)
	{
		%k = %array.getKey(%idx);
		%v = %array.getValue(%idx);
		
		%name = %k.nameBase;
		if(%k.team == $Team0)
			%team = "Obs.";
		else if(%k.team == $Team1)
			%team = "Red";
		else if(%k.team == $Team2)
			%team = "Blue";
		else
			%team = "-";
			
		%handicap = %k.handicap;
		if(%handicap == 1)
			%handicap = "1.0";			

		if(%k == %client)
			%newtxt = %newtxt @ "<spush><shadowcolor:00FF00><shadow:1:1>";

		%newtxt = %newtxt @ 
			"\>\>\t<a:cmd ShowPlayerInfo" SPC %k @ ">" @ %name @ "</a>" @ "-" @ %handicap TAB %team TAB %k.getPing() TAB %v @ "\n";

		if(%k == %client)
			%newtxt = %newtxt @ "<spop>";

		%idx = %array.moveNext();
	}
	
	%array.delete();	

	%client.addMenuText(%newtxt);
	%client.endMenuText();

	%client.menu = "playerlist";
	%client.menuArgs = %show;
}

function serverCmdShowPlayerInfo(%client, %player)
{
	%p = %player.pstats;
	if(!isObject(%p))
		return;

	%client.beginMenuText(%client.menu $= "playerinfo");
	%client.addMenuText(
		om_init() @
		om_head(%client, "Info on" SPC %player.nameBase, 
			"ShowPlayerList") @
		"(last updated @" SPC removeDecimals(%p.lastUpdate) SPC "secs)\n\n" @
		"<tab:200>" @
        "Handicap:" TAB trimStat(%p.client.handicap) @ "\n" @
		"Time played:" TAB trimStat(%p.timePlayed) SPC "mins" @ "\n" @
		"Total damage caused:" TAB trimStat(%p.totalDmgCaused)  @ "\n" @
		"Total damage taken:" TAB trimStat(%p.totalDmgTaken) @ "\n" @
		"Total health lost:" TAB trimStat(%p.totalHealthLost) @ "\n" @
		"Total health regained:" TAB trimStat(%p.totalHealthRegained) @ "\n" @
		"\n<tab:100,200,300>" @
		"\tDMG Caused\tDMG Taken\tHealth Lost\n\n" @
		"Disc" TAB trimStat(%p.discDmgCaused) TAB trimStat(%p.discDmgTaken) TAB trimStat(%p.discHealthLost) @ "\n\n" @
		"Grenade" TAB trimStat(%p.grenadeDmgCaused) TAB trimStat(%p.grenadeDmgTaken) TAB trimStat(%p.grenadeHealthLost) @ "\n\n" @
		"Blaster" TAB trimStat(%p.blasterDmgCaused) TAB trimStat(%p.blasterDmgTaken) TAB trimStat(%p.blasterHealthLost) @ "\n\n" @
		"Battle Rifle" TAB trimStat(%p.brDmgCaused) TAB trimStat(%p.brDmgTaken) TAB trimStat(%p.brHealthLost) @ "\n\n" @		
		"Sniper" TAB trimStat(%p.sniperDmgCaused) TAB trimStat(%p.sniperDmgTaken) TAB trimStat(%p.sniperHealthLost) @ "\n\n" @
		"Minigun" TAB trimStat(%p.minigunDmgCaused) TAB trimStat(%p.minigunDmgTaken) TAB trimStat(%p.minigunHealthLost) @ "\n\n" @		
		"GL" TAB trimStat(%p.glDmgCaused) TAB trimStat(%p.glDmgTaken) TAB trimStat(%p.glHealthLost) @ "\n\n" @
		"Environment" TAB "" TAB trimStat(%p.forceDmgTaken) TAB trimStat(%p.forceHealthLost) @ "\n\n" @
		"\n\n" @
		"\nWeapon effectiveness:\n\n" @
		"<tab:75,150>" @
		"\tFired\tEffectiveness\n\n" @
		"Disc" TAB %p.discFired TAB trimStat(%p.discEffectiveness) @ "%" @ "\n\n" @
		"Grenade" TAB %p.grenadeFired TAB trimStat(%p.grenadeEffectiveness) @ "%" @ "\n\n" @
		"Blaster" TAB %p.blasterFired TAB trimStat(%p.blasterEffectiveness) @ "%" @ "\n\n" @
		"BR" TAB %p.brFired TAB trimStat(%p.brEffectiveness) @ "%" @ "\n\n" @
		"Sniper" TAB %p.sniperFired TAB trimStat(%p.sniperEffectiveness) @ "%" @ "\n\n" @
		"Minigun" TAB %p.minigunFired TAB trimStat(%p.minigunEffectiveness) @ "%" @ "\n\n" @		
		"GL" TAB %p.glFired TAB trimStat(%p.glEffectiveness) @ "%" @ "\n\n" @
		"Total" TAB %p.totalFired TAB trimStat(%p.totalEffectiveness) @ "%" @ "\n\n" @
		""
	);
	%client.endMenuText();

	%client.menu = "playerinfo";
	%client.menuArgs = %player;
}

function serverCmdShowSettings(%client, %section)
{
	%newtxt = om_init();
	%client.beginMenuText();
	
	%note = "";

	if(%section == 1)
	{
		%newtxt = %newtxt @ om_head(%client, "Settings", "ShowSettings");	
		
		%newtxt = %newtxt @ %note;
	}
	else
	{
		%newtxt = %newtxt @ om_head(%client, "Settings");
		
		%newtxt = %newtxt @ %note @ "\n\n";

		// Handicap
		%hand = (%client.handicap >= 0)?%client.handicap:1;
		%newtxt = %newtxt @ "<lmargin:0>Handicap:\n<lmargin:25>";
		%newtxt = %newtxt @ "<a:cmd SetSetting handicap/" @ ((%hand - 0.1 >= 0)?%hand - 0.1:0) @ ">\<</a>";
		for (%i = 0; %i <= 10; %i += 1) {
			if (mfloor(100*mabs(%i/10 - %hand)) == 0) {
				 %newtxt = %newtxt @ "<a:cmd SetSetting handicap/" @ %i/10 @ ">#</a>";
			} else {
				 %newtxt = %newtxt @ "<a:cmd SetSetting handicap/" @ %i/10 @ ">-</a>";
			}
		}
		%newtxt = %newtxt @ "<a:cmd SetSetting handicap/" @ ((%hand + 0.1 <= 1)?%hand + 0.1:1) @ ">\></a>";
		%s = %hand;
		if(%s == 0) %s = "0.0";
		else if(%s == 1) %s = "1.0";				
		%newtxt = %newtxt SPC %s SPC "[<a:cmd HowToPlay 7>what's this?</a>]\n";
		if (%hand == 0)
		{
			%newtxt = %newtxt @ "<spush><color:ff4444>Your handicap is 0.0, this means you'll do no damage to players with a handicap of 1.0.<spop>\n";
		}		
		%newtxt = %newtxt @ "\n";			
		
		//Initial display

		%newtxt = %newtxt @ "<lmargin:0>Initially display:\n<lmargin:25>";	
		if(%client.initialTopHudMenu !$= "newbiehelp")
			%newtxt = %newtxt @ "<a:cmd SetSetting initialTopHudMenu/0>";		
		%newtxt = %newtxt @ "Newbie helper";	
		if(%client.initialTopHudMenu !$= "newbiehelp")
			%newtxt = %newtxt @ "</a>";			
		%newtxt = %newtxt @ " | ";		
		if(%client.initialTopHudMenu !$= "healthbalance")
			%newtxt = %newtxt @ "<a:cmd SetSetting initialTopHudMenu/1>";		
		%newtxt = %newtxt @ "Health balance";	
		if(%client.initialTopHudMenu !$= "healthbalance")
			%newtxt = %newtxt @ "</a>";			
		%newtxt = %newtxt @ " | ";				
		if(%client.initialTopHudMenu !$= "nothing")
			%newtxt = %newtxt @ "<a:cmd SetSetting initialTopHudMenu/2>";		
		%newtxt = %newtxt @ "Nothing";	
		if(%client.initialTopHudMenu !$= "nothing")
			%newtxt = %newtxt @ "</a>";			
		%newtxt = %newtxt @ "\n\n";				
		
		%n = 0;
		%schemeName[%n] = "";
		%schemeDesc[%n] = "Based on team"; %n++;
		%schemeName[%n] = "fr1tz";
		%schemeDesc[%n] = "fr1tz"; %n++;		
		%schemeName[%n] = "kurrata";
		%schemeDesc[%n] = "kurrata"; %n++;		
		%schemeName[%n] = "c&c";
		%schemeDesc[%n] = "c&c"; %n++;			
		%schemeName[%n] = "cga1dark";
		%schemeDesc[%n] = "CGA #1 Dark"; %n++;		
		%schemeName[%n] = "cga1light";
		%schemeDesc[%n] = "CGA #1 Light"; %n++;	
		
		%newtxt = %newtxt @ "<lmargin:0>HUD Colors:\n<lmargin:25>";			
		for(%i = 0; %i < %n; %i++)
		{
			if(%client.hudColor !$= %schemeName[%i])
				%newtxt = %newtxt @ "<a:cmd SetSetting hudColor/" @ %schemeName[%i] @ ">";
			
			%newtxt = %newtxt @ %schemeDesc[%i];					
				
			if(%client.hudColor !$= %schemeName[%i])
				%newtxt = %newtxt @ "</a>";				

			%newtxt = %newtxt @ "   ";								
		}
		%newtxt = %newtxt @ "\n\n";					
	
	}

	%client.addMenuText(%newtxt);
	%client.endMenuText();

	%client.menu = "settings";
}

function serverCmdSetSetting(%client, %str)
{
	%str = strreplace(%str, "/", " ");
	%name = getWord(%str, 0);
	%value = getWord(%str, 1);

	if(%name $= "initialTopHudMenu")
	{
		%mode = "";
		if(%value == 0)
			%mode = "newbiehelp";
		else if(%value == 1)
			%mode = "healthbalance";
		else if(%value == 2)
			%mode = "nothing";			
			
		if(%mode !$= "")
		{
			%client.initialTopHudMenu = %mode;
			%client.sendCookie("ROTC_HudMenuTMode", %mode);
		
			if(%client.menu $= "settings")
				serverCmdShowSettings(%client);
		}
	}
	else if(%name $= "hudColor")
	{
		%hudColor = %value;
		%client.hudColor = %hudColor;
		%client.sendCookie("ROTC_HudColor", %hudColor);		
		%client.updateHudColors();
		
		if(%client.menu $= "settings")
			serverCmdShowSettings(%client);		
	}
	else if (%name $= "handicap")
	{
		%client.setHandicap(%value);
		%client.sendCookie("ROTC_Handicap", %value);

		if(%client.menu $= "settings")
			serverCmdShowSettings(%client);
	}
}


// ---------------------------------------------------------

