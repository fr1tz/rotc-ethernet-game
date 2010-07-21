
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
		}
	}
	else
		error("map_spawnBlueBots(): missing BlueBotSpawnPoints group!");
}



