//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function showPlayerList(%client, %arg)
{
	%newtxt = om_init();
	%client.beginMenuText(%client.menu $= "playerlist");

	%newtxt = %newtxt @ 
		om_head(%client, "Player List");

	if(%arg $= "nogroup")
	{
		%client.playerListNoGroup = true;
		%show = %client.playerListShow;
	}
	else if(%arg $= "group")
	{
		%client.playerListNoGroup = false;	
		%show = %client.playerListShow;
	}
	else if(%arg !$= "")
	{
		%show = %arg;
	}
	else
	{
		%show = %client.playerListShow;
	}

	if(%show $= "")
		%show = "dmgrating";

	%client.playerListShow = %show;

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

	if(%client.playerListNoGroup == true)
		%groupByTeam = false;
	else
		%groupByTeam = true;

	%groupingLinks = "";
	if(!%groupByTeam)
		%groupingLinks = %groupingLinks @ "<a:cmd ShowPlayerList group>";
	%groupingLinks = %groupingLinks @ "Group by team";
	if(!%groupByTeam)
		%groupingLinks = %groupingLinks @ "</a>";
	%groupingLinks = %groupingLinks @ " | ";
	if(%groupByTeam)
		%groupingLinks = %groupingLinks @ "<a:cmd ShowPlayerList nogroup>";
	%groupingLinks = %groupingLinks @ "Don't group players";
	if(%groupByTeam)
		%groupingLinks = %groupingLinks @ "</a>";

	%newtxt = %newtxt @
		"Show:\n" @
		"<lmargin:10>" @
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
		"<a:cmd ShowPlayerList time>Time played</a>\n" @
		"\n" @ %groupingLinks @
		"<lmargin:0>\n\n" @
		"<tab:10, 150, 200, 250, 350>" @
		"\tName/Handicap\tTeam\tPing" TAB %showtext[0] @ "\n\t\t\t\t" @ %showtext[1] @ "\n\n" @
		"";

	%players = "";
	%reds = "";
	%blues = "";
	%others = "";

	%idx = %array.moveFirst();
	while(%idx != -1)
	{
		%k = %array.getKey(%idx);
		%v = %array.getValue(%idx);

		%line = "<spush>";
		
		%name = %k.nameBase;
		if(%k.team == $Team0)
			%team = "Obs.";
		else if(%k.team == $Team1)
		{
			%team = "Red";
			%line = %line @ "<color:AA0000>";
		}
		else if(%k.team == $Team2)
		{
			%team = "Blue";
			%line = %line @ "<color:0000AA>";
		}
		else
			%team = "-";
			
		%handicap = %k.handicap;
		if(%handicap == 1)
			%handicap = "1.0";			

		if(%k == %client)
			%line = %line @ "<spush><shadowcolor:00FF00><shadow:1:1>";

		%line = %line @ 
			"\>\t<a:cmd ShowPlayerInfo" SPC %k @ ">" @ %name @ "</a>" @ "-" @ %handicap TAB %team TAB %k.getPing() TAB %v @ "\n";

		if(%k == %client)
			%line = %line @ "<spop>";

		%line = %line @ "<spop>";


		if(%groupByTeam)
		{
			if(%k.team == $Team1)
				%reds = %reds @ %line;
			else if(%k.team == $Team2)
				%blues = %blues @ %line;
			else
				%others = %others @ %line;
		}
		else
		{
			%players = %players @ %line;			
		}

		%idx = %array.moveNext();
	}
	
	%array.delete();

	if(%groupByTeam)
	{
		if(%reds $= "") %reds = "<spush><color:AA0000>\t(No red players)<spop>\n";
		if(%blues $= "") %blues = "<spush><color:0000AA>\t(No blue players)<spop>\n";
		if(%others $= "") %others = "\t(No unassigned players)";
		%players = %reds @ "\n" @ %blues @ "\n" @ %others;
	}

	%newtxt = %newtxt @ %players;	

	%client.addMenuText(%newtxt);
	%client.endMenuText();

	%client.menu = "playerlist";
	%client.menuArgs = %show;
}

function showPlayerInfo(%client, %player)
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