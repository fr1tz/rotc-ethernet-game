// This script is executed on the server

$MAP_ROOT = "ethernet/maps/sp_tutorial/";

$sgLightEditor::lightDBPath = $MAP_ROOT @ "lights/";
$sgLightEditor::filterDBPath = $MAP_ROOT @ "filters/";
sgLoadDataBlocks($sgLightEditor::lightDBPath);
sgLoadDataBlocks($sgLightEditor::filterDBPath);

exec("./bluebots.cs");
exec("./disc.cs");
exec("./display.cs");
exec("./infopts.cs");
exec("./materials.cs");
exec("./stages.cs");
exec("./text.cs");

package Map {
    function startNewRound() 
	{

		if($Map::Stage == 19)
		{
			disconnect();
			return;
		}

        Parent::startNewRound();

		BlueBlasterProjectile.impactDamage = 0;

		if( !isObject($aiPlayers) )
		{
			$aiPlayers = new Array();
			MissionCleanup.add($aiPlayers);
		}

        // show the info box to all players not in the read team
        for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
            %client = ClientGroup.getObject( %clientIndex );

            if( %client.team == $Team0 ) 
                map_displayCenter($tutorialText[70]);
            else if( %client.team == $Team2 ) 
                map_displayCenter($tutorialText[90]);
        }

		map_spawnBlueBots();

		$Map::Stage = 0;
		map_activateInfoPoint(1);
    }

    function serverCmdJoinTeam(%client, %team) {
        
		if(Parent::serverCmdJoinTeam(%client, %team))
		{
			if (%team == 0) {
				$Map::Stage = 1;
				map_displayCenter($tutorialText[70]);
			} else if (%team == 1) {
			} else if (%team == 2) {
				$Map::Stage = 2;
				map_displayCenter($tutorialText[90]);
			}

			map_clearObjective();
		}
    }

	function EtherformData::onAdd(%this, %obj)
	{
		Parent::onAdd(%this, %obj);
		if($Map::Stage < 3)
		{
			$Map::Stage = 3;
			map_displayCenter($Map::Text::Info0);
		}

		map_displayObjective();	
	}

	function RedStandardCat::onAdd(%this, %obj)
	{
		Parent::onAdd(%this, %obj);

		if(%obj.infoPoint)
			return;

		clearCenterPrintAll();
		
		if($Map::Stage < 4)
		{
			$Map::Stage = 4;
			map_displayObjective("Touch the InfoBot");	
		}
		else
			map_displayObjective();	
	}

	function RedStandardCat::damage(%this, %obj, %sourceObject, %pos, %damage, %damageType)
	{
		if(%obj.infoPoint)
		{

		}
		else
			Parent::damage(%this, %obj, %sourceObject, %pos, %damage, %damageType);
	}

	function RedStandardCat::onCollision(%this,%obj,%col,%vec,%vecLen)
	{
		Parent::onCollision(%this,%obj,%col,%vec,%vecLen);

		if(%obj.infoPoint)
			map_useInfoPoint(%obj.infoPoint);
	}


};
activatePackage(Map);




