// Stages:
// 1 = Observe
// 2 = Wrong team
// 3 = Initial etherform
// 4 = Initial CAT
// 5 = Captured first zone
// 6 = Captured long trench zone
// 7 = jump beams
// 8 = jumped beams
// 9 = jumped pit


datablock TriggerData(Map_StageTrigger) {
    tickPeriodMS = 200;
};

function Map_StageTrigger::onEnterTrigger(%this, %trigger, %obj) {
	if(%trigger.getName() $= "StageTrigger2" && $Map::Stage == 4)
	{
		$Map::Stage = 5;
		map_displayCenter($Map::Text::3);
	}
	else if(%trigger.getName() $= "StageTrigger3" && $Map::Stage == 5)
	{
		$Map::Stage = 6;
		map_displayCenter($Map::Text::4);
	}
	else if(%trigger.getName() $= "StageTrigger4" && $Map::Stage == 6)
	{
		$Map::Stage = 7;
		map_displayCenter($Map::Text::5);
	}
	else if(%trigger.getName() $= "StageTrigger5" && $Map::Stage == 7)
	{
		$Map::Stage = 8;
		map_displayCenter($Map::Text::6);
	}
	else if(%trigger.getName() $= "StageTrigger6" && $Map::Stage == 8)
	{
		map_displayCenter($Map::Text::7);
	}
	else if(%trigger.getName() $= "StageTrigger7" && $Map::Stage == 8)
	{
		$Map::Stage = 9;
		map_displayCenter($Map::Text::8);
	}
    Parent::onEnterTrigger(%this, %trigger, %obj);
}

function Map_StageTrigger::onLeaveTrigger(%this, %trigger, %obj) {
	if(%trigger.getName() $= "StageTrigger3" && $Map::Stage == 6)
	{
		map_clearCenter();
	}
	else if(%trigger.getName() $= "StageTrigger6" && $Map::Stage == 8)
	{
		map_clearCenter();
	}

    Parent::onLeaveTrigger(%this, %trigger, %obj);
}

function Map_StageTrigger::onTickTrigger(%this, %trigger) {
    Parent::onTickTrigger(%this, %trigger);
}
