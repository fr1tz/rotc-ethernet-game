
function map_getBlueBotSpawn(%botname)
{
	%group = nameToID("BlueBotSpawnPoints");

	if (%group != -1)
	{
		%count = %group.getCount();
		for (%i = 0; %i < %count; %i++)
		{
			%point = %group.getObject(%i);
			if(%point.botname $= %botname)
				return %point;
		}
		error("map_getBlueBotSpawn():" SPC
			%num SPC "not found in BlueBotSpawnPoints group!");
	}
	else
		error("map_getBlueBotSpawn(): missing BlueBotSpawnPoints group!");

	return -1;
}


function map_spawnBlueBots()
{
	%group = nameToID("BlueBotSpawnPoints");

	if (%group != -1)
	{
		%count = %group.getCount();
		for (%i = 0; %i < %count; %i++)
		{
			%spawnSphere = %group.getObject(%i);
			%transform = %spawnSphere.getTransform();

   			 %player = new AiPlayer() {
				dataBlock = BlueStandardCat;
				teamId = 2;
			};
			MissionCleanup.add(%player);

			%player.setShapeName(%spawnSphere.botname);
			%player.setTransform(%transform);

			%spawnSphere.bot = %player;
		}
	}
	else
		error("map_spawnBlueBots(): missing BlueBotSpawnPoints group!");
}



