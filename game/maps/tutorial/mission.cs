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
        */

        Parent::startNewRound();

        // show the info box to all players not in the read team
        for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
            %client = ClientGroup.getObject( %clientIndex );

            if( %client.team == $Team0 ) 
                displayText(%client, $tutorialText[70]);
            else if( %client.team == $Team2 ) 
                displayText(%client, $tutorialText[90]);
        }

        // create target bots
        createTargetBotAt(NameToID("genericTargetS"), 0);
        createTargetBotAt(NameToID("genericTargetM"), 0);
        createTargetBotAt(NameToID("genericTargetL"), 0);
        createTargetBotAt(NameToID("sniperTarget"), 0);

        createTargetBotAt(NameToID("genericTargetZ1"), 0);
        createTargetBotAt(NameToID("genericTargetZ2"), 0);

        showTextDisplayDecals();
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
        if (%text == 71) { // tactical room
            %numZones = 13;
            // check if someone is in the tactical room

            %redFound = 0;
            for (%i = 1; %i <= %numZones; %i++) {
                %zone = NameToID("bottomright" @ %i);
                if (%zone.numReds > 0)
                    %redFound = 1;
            }

            if (%redFound == 0) { // reset tactical room
                for (%i = 1; %i <= %numZones; %i++) {
                    %zone = NameToID("bottomright" @ %i);
                    // reset owner
                    %zone.getDataBlock().setZoneOwner(%zone, %zone.initialOwner);

                    // create bots if they're dead
                    if (%i == 2 && %zone.numBlues == 0)
                        createTargetBotAt(NameToID("genericTargetZ1"), 0);
                    if (%i == 5 && %zone.numBlues == 0)
                        createTargetBotAt(NameToID("genericTargetZ2"), 0);

                }
            }
        }
        else if (%text == 75)
            createTargetBotAt(NameToID("genericTargetS"), 0);
        else if (%text == 76)
            createTargetBotAt(NameToID("genericTargetM"), 0);
        else if (%text == 77)
            createTargetBotAt(NameToID("genericTargetL"), 0);
        else if (%text == 78)
            createTargetBotAt(NameToID("sniperTarget"), 0);
        else if (%text == 79) {
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




