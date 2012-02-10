//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function showSettings(%client, %section)
{
	%newtxt = om_init();
	%client.beginMenuText();
	
	%newtxt = %newtxt @ om_head(%client, $Game::GameTypeString SPC "Settings");	

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
	%schemeDesc[%n] = "based_on_condition"; %n++;
	%schemeName[%n] = "team";
	%schemeDesc[%n] = "based_on_team"; %n++;
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
	
	%client.addMenuText(%newtxt);
	%client.endMenuText();

	%client.menu = "settings";
}

function setSetting(%client, %str)
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

