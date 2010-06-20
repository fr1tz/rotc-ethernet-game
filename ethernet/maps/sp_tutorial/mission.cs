// This script is executed on the server

$MAP_ROOT = "ethernet/maps/sp_tutorial/";

$sgLightEditor::lightDBPath = $MAP_ROOT @ "lights/";
$sgLightEditor::filterDBPath = $MAP_ROOT @ "filters/";
sgLoadDataBlocks($sgLightEditor::lightDBPath);
sgLoadDataBlocks($sgLightEditor::filterDBPath);

exec("./materials.cs");
exec("./text.cs");
exec("./stages.cs");
exec("./infopts.cs");

package Tutorial {
    function startNewRound() {
        /*
        // Let all players in observer-teams join the red team
        for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
            %client = ClientGroup.getObject( %clientIndex );

            echo(%client);
            if( %client.team == $Team0 )
                %client.joinTeam(1);
        }
        */

        Parent::startNewRound();

        // show the info box to all players not in the read team
        for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
            %client = ClientGroup.getObject( %clientIndex );

            if( %client.team == $Team0 ) 
                map_displayCenter($tutorialText[70]);
            else if( %client.team == $Team2 ) 
                map_displayCenter($tutorialText[90]);
        }

        // create target bots
        createTargetBotAt(NameToID("genericTargetS"), 0);
        createTargetBotAt(NameToID("genericTargetM"), 0);
        createTargetBotAt(NameToID("genericTargetL"), 0);
        createTargetBotAt(NameToID("sniperTarget"), 0);

        createTargetBotAt(NameToID("genericTargetZ1"), 0);
        createTargetBotAt(NameToID("genericTargetZ2"), 0);

        showTextDisplayDecals();

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
activatePackage(Tutorial);

function map_displayCenter(%message)
{
	centerPrintAll("", 0, 0 );
	%l = strlen(%message); %n = 0;
	while(%n < %l)
	{
		%chunk = getSubStr(%message, %n, 255);
		centerPrintAll(%chunk, 0, 1);
		%n += 255;
	}
}

function map_displayObjective(%message)
{
	if(%message !$= "")
		$Map::Objective = %message;

	clearBottomPrintAll();
	%l = strlen($Map::Objective); %n = 0;
	while(%n < %l)
	{
		%chunk = getSubStr($Map::Objective, %n, 255);
		bottomPrintAll(%chunk);
		%n += 255;
	}
}

function map_clearObjective()
{
	$Map::Objective = "";
	clearBottomPrintAll();
}


// -----------------------------
// Trigger functions
// -----------------------------


datablock TriggerData(ThrowDiscAtMe) {
    tickPeriodMS = 200;
};

function ThrowDiscAtMe::onEnterTrigger(%this, %trigger, %obj) {
    if (%obj.isCAT) {
        %origin = NameToID(%trigger.originObjName);

        %obj.client.play2D(DiscIncomingSound);
        throwDiscAt(%origin, %obj);
    }

    Parent::onEnterTrigger(%this, %trigger, %obj);
}

datablock NortDiscData(PussyBlueSeekerDisc : BlueSeekerDisc) {
    impactDamage         = 20;
};

function throwDiscAt(%origin, %target) {
    %disc = new (NortDisc)() {
        dataBlock         = PussyBlueSeekerDisc;
        teamId            = 2;
        initialVelocity  = "0" SPC getRandom(-100,100) SPC getRandom(1,100);
        initialPosition  = %origin.position;
        sourceObject      = %origin;
        sourceSlot       = 0;
        client            = 0;
    };
    MissionCleanup.add(%disc);

    %disc.setTarget(%target);
    %disc.setTargetingMask($TargetingMask::Disc);

}


// -----------------------------
// Create bots
// -----------------------------

function createTargetBotAt(%spawnSphere, %fight) {
    if( !isObject($tutBots) ) {
		$tutBots = new Array();
		MissionCleanup.add($aiPlayers);
    }

    %player = new AiPlayer() {
		dataBlock = BlueStandardCat;
		path = "";
		teamId = 2;
	};
	MissionCleanup.add(%player);

	%pos = getRandomHorizontalPos(%spawnSphere.position,%spawnSphere.radius);
//	%player.setShapeName("");
	%player.name = "tutbot_" @ %spawnSphere.getName();
	%player.setTransform(%pos);

	%player.weapon = 1;
	%player.charge = 100;

	$tutBots.push_back("",%player);

    if (%fight == 1) {
		xxx_aiStartMove(%player);
		xxx_aiStartFire(%player);
		xxx_aiChooseWeapon(%player);

        schedule(20000, %player, "kill"); // after 20 seconds, a fighting bot dies
	}
}


// -----------------------------
// Text display
// -----------------------------
//
datablock DecalData(TextDisplayDecal)
{
    sizeX = "3.00";
    sizeY = "3.00";
    textureName = "share/textures/eth3/circuit_glow";
    SelfIlluminated = true;
    lifeSpan = 600000;
};

if (! $tutorialTextDecalsCount)
    $tutorialTextDecalsCount = 0;

function showTextDisplayDecals() {
    echo("Redraw Decals");
    cancel($tutorialTextDecalsThread);
    %group = nameToID("TutorialTextTriggers");

    // Delete decals
    for (%i = 0; %i < $tutorialTextDecalsCount; %i++) {
        %trig = $tutorialTextDecals[%i];
        %trig.delete();
    }
    $tutorialTextDecalsCount = 0;


    if (%group != -1) {
        %count = %group.getCount();

        if (%count != 0) {
            %j = 0;
            for (%i = 0; %i < %count; %i++) {
                %trig = %group.getObject(%i);

                if (%trig.noDecal == 1)
                    continue;

                %scalexy = getWord(%trig.scale,0) SPC (-1*getWord(%trig.scale,1)) SPC "0";
                %pos = VectorAdd(%trig.position, VectorScale(%scalexy, 0.5));
                // Center the decal - this is a dirty hack

                %decal = new sgDecalProjector(%decalPrototype) {
                    position = %pos;
                    dataBlock = "TextDisplayDecal";
                    rotation = "1 0 0 0";
                    scale = "1 1 1";
                    canSaveDynamicFields = "1";
                };
                $tutorialTextDecals[%j++] = %decal;
                MissionCleanup.add(%decal);
            }

            $tutorialTextDecalsCount = %j;

            $tutorialTextDecalsThread =
                schedule(TextDisplayDecal.lifeSpan/3*2, 0, "showTextDisplayDecals");
        } else {
            error("showTextDisplayDecals(): could not find any Triggers in the TutorialTextTrigger group.");
        }
    } else
        error("showTextDisplayDecals(): could not find the TutorialTextTrigger group.");
}


