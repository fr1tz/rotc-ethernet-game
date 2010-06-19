// This script is executed on the server

$MAP_ROOT = "ethernet/maps/tutorial/";

$sgLightEditor::lightDBPath = $MAP_ROOT @ "lights/";
$sgLightEditor::filterDBPath = $MAP_ROOT @ "filters/";
sgLoadDataBlocks($sgLightEditor::lightDBPath);
sgLoadDataBlocks($sgLightEditor::filterDBPath);

//------------------------------------------------------------------------------
// Material mappings
//------------------------------------------------------------------------------

%mapping = createMaterialMapping("dark_grey_blue_grid");
%mapping.sound = $MaterialMapping::Sound::Metal;
%mapping.color = "0.3 0.3 0.3 0.4 0.0";

%mapping = createMaterialMapping("tutorial/dark_blue_grid");
%mapping.sound = $MaterialMapping::Sound::Metal;
%mapping.color = "0.3 0.3 0.3 0.4 0.0";
//%mapping.envmap = "share/textures/tutorial/dark_blue_grid 0.5";

// #############################################################################
// Scripting stuff now...
// #############################################################################

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


        for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
            %client = ClientGroup.getObject( %clientIndex );

            if( %client.team == $Team1 ) {
                %client.player.setEnergyLevel(75);
                %client.togglePlayerForm();
            }
        }
        */

        Parent::startNewRound();

        // create target bots
        createTargetBotAt(NameToID("genericTargetS"), 0);
        createTargetBotAt(NameToID("genericTargetM"), 0);
        createTargetBotAt(NameToID("genericTargetL"), 0);
        createTargetBotAt(NameToID("sniperTarget"), 0);

    }

    function serverCmdJoinTeam(%client, %team) {
        Parent::serverCmdJoinTeam(%client, %team);

        if (%team == 0) {
            displayText(%client, $tutorialText[70]);
        } else if (%team == 1) {
        } else if (%team == 2) {
            displayText(%client, $tutorialText[90]);
        }
    }
};
activatePackage(Tutorial);


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
        initialVelocity  = "0 0 100";
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

$tutorialTextDecalsCount = 0;

schedule(5000, 0, "showTextDisplayDecals");
function showTextDisplayDecals() {
    echo("Redraw Decals");
    %group = nameToID("TutorialTextTriggers");

    // Delete decals
    for (%i = 0; %i < $tutorialTextDecalsCount; %i++) {
        %trig = $tutorialTextDecals[%i];
        //MissionCleanup.removeObject(%trig); // Is this even necessary?
        %trig.delete();
    }


    if (%group != -1) {
        %count = %group.getCount();
        $tutorialTextDecalsCount = %count;

        if (%count != 0) {
            for (%i = 0; %i < %count; %i++) {
                %trig = %group.getObject(%i);

                // Center the decal - this is a dirty hack
                %x = getWord(%trig.position, 0) + 2.2;
                %y = getWord(%trig.position, 1) - 2.2;
                %z = getWord(%trig.position, 2);

                %decal = new sgDecalProjector(%decalPrototype) {
                    position = %x SPC %y SPC %z;
                    dataBlock = "TextDisplayDecal";
                    rotation = "1 0 0 0";
                    scale = "1 1 1";
                    canSaveDynamicFields = "1";
                };
                $tutorialTextDecals[%i] = %decal;
                MissionCleanup.add(%decal);
            }

            schedule(TextDisplayDecal.lifeSpan/3*2, 0, "showTextDisplayDecals");
        } else 
            error("showTextDisplayDecals(): could not find any Triggers in the TutorialTextTrigger group.");
    } else
        error("showTextDisplayDecals(): could not find the TutorialTextTrigger group.");
}

//exec($MAP_ROOT @ "tutorial_texts.cs");
exec("./tutorial_texts.cs");

function displayText(%cl, %message) {
	commandToClient( %cl, 'CenterPrint', "", 0, 0 );
	%l = strlen(%message); %n = 0;
	while(%n < %l)
	{
		%chunk = getSubStr(%message, %n, 255);
		commandToClient( %cl, 'CenterPrint', %chunk, 0, 1 );
		%n += 255;
	}
}

function undisplayText(%cl) {
    commandToClient( %cl, 'ClearCenterPrint' );
}

datablock TriggerData(TextDisplay) {
    tickPeriodMS = 200;
};

function TextDisplay::onEnterTrigger(%this, %trigger, %obj) {
    %text = %trigger.text;

    if (%text >= 70) { // these texts will invoke additional functions
        if (%text == 75)
            createTargetBotAt(NameToID("genericTargetS"), 0);
        if (%text == 76)
            createTargetBotAt(NameToID("genericTargetM"), 0);
        if (%text == 77)
            createTargetBotAt(NameToID("genericTargetL"), 0);
        if (%text == 78)
            createTargetBotAt(NameToID("sniperTarget"), 0);
        if (%text == 79) {
            createTargetBotAt(NameToID("aiSpawn1"), 1);
            createTargetBotAt(NameToID("aiSpawn2"), 1);
            createTargetBotAt(NameToID("aiSpawn3"), 1);
        }

    }

    displayText(%obj.client, $tutorialText[%text]);

    Parent::onEnterTrigger(%this, %trigger, %obj);
}

function TextDisplay::onLeaveTrigger(%this, %trigger, %obj) {
    undisplayText(%obj.client);

    Parent::onLeaveTrigger(%this, %trigger, %obj);

}

function TextDisplay::onTickTrigger(%this, %trigger) {
    Parent::onTickTrigger(%this, %trigger);
}




