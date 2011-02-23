// Stages:
// 1 = Observer
// 2 = Wrong team
// 3 = Initial etherform
// 4 = Initial CAT
// 5 = Captured first zone
// 6 = Captured long trench zone
// 7 = jump beams
// 8 = jumo wall of beams
// 9 = jump pit
// 10 = jumped pit
// 11 = destroy first bot
// 12 = after first bot
// 13 = snipe second bot
// 14 = destroy bot behind fence
// 15 = after bot behind fence
// 16 = disc bot #1
// 17 = disc bot #2
// 18 = disc bot #3
// 19 = final info bot

datablock TriggerData(Map_StageTrigger) {
    tickPeriodMS = 200;
};

function Map_StageTrigger::onEnterTrigger(%this, %trigger, %obj) {
    Parent::onEnterTrigger(%this, %trigger, %obj);

	if(%obj.infoPoint)
		return;

	if(!%obj.isCAT)
		return;

	if(%obj.getTeamId() != 1)
		return;

	if(%trigger.getName() $= "StageTrigger2" && $Map::Stage == 4)
	{
		$Map::Stage = 5;
		map_activateInfoPoint(2);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
	else if(%trigger.getName() $= "StageTrigger3" && $Map::Stage < 6)
	{
		$Map::Stage = 6;
		map_activateInfoPoint(3);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
	else if(%trigger.getName() $= "StageTrigger4" && $Map::Stage < 7)
	{
		$Map::Stage = 7;
		map_activateInfoPoint(4);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
	else if(%trigger.getName() $= "StageTrigger5" && $Map::Stage < 8)
	{
		$Map::Stage = 8;
		map_activateInfoPoint(5);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
	else if(%trigger.getName() $= "StageTrigger6" && $Map::Stage < 9)
	{
		$Map::Stage = 9;
		map_activateInfoPoint(6);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
	else if(%trigger.getName() $= "StageTrigger7" && $Map::Stage < 10)
	{
		$Map::Stage = 10;
		map_activateInfoPoint(7);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
	else if(%trigger.getName() $= "StageTrigger8" && $Map::Stage < 11)
	{
		$Map::Stage = 11;
		map_activateInfoPoint(8);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
	else if(%trigger.getName() $= "StageTrigger9" && $Map::Stage < 12)
	{
		$Map::Stage = 12;
		map_activateInfoPoint(9);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
	else if(%trigger.getName() $= "StageTrigger10" && $Map::Stage < 13)
	{
		$Map::Stage = 13;
		map_activateInfoPoint(10);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
	else if(%trigger.getName() $= "StageTrigger11" && $Map::Stage < 14)
	{
		$Map::Stage = 14;
		map_activateInfoPoint(11);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
	else if(%trigger.getName() $= "StageTrigger12" && $Map::Stage < 15)
	{
		$Map::Stage = 15;
		map_activateInfoPoint(12);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
	else if(%trigger.getName() $= "StageTrigger13" && $Map::Stage < 16)
	{
		$Map::Stage = 16;
		$aiPlayers.push_back("", map_getBlueBotSpawn("/bin/bash").bot);
		aiStartFire();
		map_displayObjective($Map::Text::DestroyBlueBot);
	}
	else if(%trigger.getName() $= "StageTrigger14" && $Map::Stage < 17)
	{
		$Map::Stage = 17;
		$aiPlayers.push_back("", map_getBlueBotSpawn("/usr/bin/tac").bot);
		aiStartFire();
		map_displayObjective($Map::Text::DestroyBlueBot);
	}
	else if(%trigger.getName() $= "StageTrigger15" && $Map::Stage < 18)
	{
		$Map::Stage = 18;
		$aiPlayers.push_back("", map_getBlueBotSpawn("/bin/csh").bot);
		aiStartFire();
		map_displayObjective($Map::Text::DestroyBlueBot);
	}
	else if(%trigger.getName() $= "StageTrigger16" && $Map::Stage < 19)
	{
		$Map::Stage = 19;
		map_activateInfoPoint(13);
		map_displayObjective($Map::Text::SeeInfoBot);
	}
}

function Map_StageTrigger::onLeaveTrigger(%this, %trigger, %obj) {
    Parent::onLeaveTrigger(%this, %trigger, %obj);
}

function Map_StageTrigger::onTickTrigger(%this, %trigger) {
    Parent::onTickTrigger(%this, %trigger);
}
