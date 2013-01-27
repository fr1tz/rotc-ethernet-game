//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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
	%balance = 1 - ((%balance+1)/2);

	%spacers = 280 * %balance - 8;
	if(%spacers < 0) %spacers = 0;
	if(%spacers > 264) %spacers = 264;
	
	$Server::GameStatus::HealthBalance::Spacers = %spacers;
	
	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
	{
		%client = ClientGroup.getObject(%clientIndex);
		if(%client.topHudMenu $= "healthbalance")
			%client.updateTopHudMenuThread();
	}
}
