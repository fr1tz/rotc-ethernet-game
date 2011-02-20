//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function serverUpdateGameStatus()
{
	cancel($Server::UpdateGameStatusThread);

	serverUpdateHealthBalanceStatus();

	$Server::UpdateGameStatusThread = schedule(1000, 0, "serverUpdateGameStatus");
}

function setAllHudMenuT(%slot, %text, %repetitions, %visible)
{
	%count = ClientGroup.getCount();
	for(%cl= 0; %cl < %count; %cl++)
	{
		%recipient = ClientGroup.getObject(%cl);
		%recipient.setHudMenuT(%slot, %text, %repetitions, %visible);
	}
}

function serverUpdateHealthBalanceStatus()
{
	%teamHealth[1] = 0;
	%teamHealth[2] = 0;
	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
	{
		%client = ClientGroup.getObject(%clientIndex);
		if(isObject(%client.player))
		{
			%teamHealth[%client.team.teamId] += %client.player.getDataBlock().maxDamage
				- %client.player.getDamageLevel() + %client.player.getDamageBufferLevel();
		}
	}
	%teamHealth[1] = $Team1.numPlayers == 0 ? 1.0 : %teamHealth[1] / $Team1.numPlayers / 100;
	%teamHealth[2] = $Team2.numPlayers == 0 ? 1.0 : %teamHealth[2] / $Team2.numPlayers / 100;

	%balance = %teamHealth[1] - %teamHealth[2];

	setAllHudMenuT(0, 
		"\n<just:center><color:888888>Health balance:\n<just:left>" @
		"<bitmap:share/hud/rotc/spec><sbreak>",
		1, 1);

	%balance = 1 - ((%balance+1)/2);

	%spacers = 280 * %balance - 8;
	if(%spacers < 0) %spacers = 0;
	if(%spacers > 264) %spacers = 264;
	setAllHudMenuT(1, "<bitmap:share/hud/rotc/spacer.1x14>", %spacers, 1);

	setAllHudMenuT(2, "<bitmap:share/hud/rotc/marker.up>", 1, 1);
}
