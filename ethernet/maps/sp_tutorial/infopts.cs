function map_checkInfoPointProximity()
{
	%pos1 = $Map::CurrentInfoBot.getPosition();
	%pos2 = $aiTarget.getPosition();

	%dist = VectorLen(VectorSub(%pos1,%pos2));
	if(%dist > 3)
		clearCenterPrintAll();
	else
		schedule(500, 0, map_checkInfoPointProximity);
}

function map_getInfoPoint(%num)
{
	%group = nameToID("TutorialInfoPoints");

	if (%group != -1)
	{
		%count = %group.getCount();
		for (%i = 0; %i < %count; %i++)
		{
			%point = %group.getObject(%i);
			if(%point.getName() $= %num)
				return %point;
		}
		error("map_getInfoPoint():" SPC
			%num SPC "not found in TutorialInfoPoints group!");
	}
	else
		error("map_getInfoPoint(): missing TutorialInfoPoints group!");

	return -1;
}

function map_useInfoPoint(%num)
{
	%text = getVariable("Map::Text::Info" @ %num);

	map_displayCenter(%text);

	schedule(500, 0, map_checkInfoPointProximity);

	if(%num == 1)
	{
		map_displayObjective($Map::Text::Continue);
	}
	else if(%num == 2)
	{
		map_displayObjective($Map::Text::Continue);
	}
	else if(%num == 3)
	{
		map_displayObjective("Use your etherform to reach the other end of" SPC
			"this zone as quickly as possible and change back to CAT form.");
	}
	else if(%num == 4)
	{
		map_displayObjective("Jump the beams.");
	}
	else if(%num == 5)
	{
		map_displayObjective("Use your jump boosters to jump over the beams.");
	}
	else if(%num == 6)
	{
		map_displayObjective("Use your etherboard to jump the deadly pit.");
	}
	else if(%num == 7)
	{
		map_displayObjective($Map::Text::Continue);
	}
}

function map_activateInfoPoint(%num)
{
	$Map::CurrentInfoBot.delete();

	%spawnSphere = map_getInfoPoint(%num);
	%transform = %spawnSphere.getTransform();

	createExplosionOnClients(CatJumpExplosion, %transform, "0 0 1");

    %player = new AiPlayer() {
		dataBlock = RedStandardCat;
		teamId = 1;
		infoPoint = %num;
	};
	MissionCleanup.add(%player);

	%player.setShapeName("cat /info/" @ %num);
	%player.setTransform(%transform);

	$Map::CurrentInfoPoint = %num;
	$Map::CurrentInfoBot = %player;
}





