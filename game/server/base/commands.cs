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
	else if(%nr == 13 && %val)
	{
		%client.getControlObject().useWeapon(6);
	}
	else if(%nr == 14 && %val)
	{
		%client.getControlObject().useWeapon(7);
	}
	else if(%nr == 17 && %val)
	{		
		%client.getControlObject().useWeapon(-17);
	}
	else if(%nr >= 21 && %nr <= 30 && %val)
	{
		%client.getControlObject().useWeapon(%nr-20);
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
		%newtxt = %newtxt SPC %s SPC "[<a:cmd HowToPlay H>what's this?</a>]\n";
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


