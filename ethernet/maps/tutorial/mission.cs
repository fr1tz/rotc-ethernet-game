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

        showTextDisplayDecals();
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

function createTargetBotAt(%spawnSphere) {
    if( !isObject($tutBots) ) {
		$aiPlayers = new Array();
		MissionCleanup.add($aiPlayers);
    }

    %player = new AiPlayer() {
		dataBlock = BlueStandardCat;
		path = "";
		teamId = 2;
	};
	MissionCleanup.add(%player);

	%pos = getRandomHorizontalPos(%spawnSphere.position,%spawnSphere.radius);
	%player.setShapeName("tutbot_" @ %spawnSphere.name);
	%player.name = "tutbot_" @ %spawnSphere.name;
	%player.setTransform(%pos);

	%player.weapon = 1;
	%player.charge = 100;

	$tutBots.push_back("",%player);

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

            schedule(400, showTextDisplayDecals, 1);
        } else 
            error("showTextDisplayDecals(): could not find any Triggers in the TutorialTextTrigger group.");
    } else
        error("showTextDisplayDecals(): could not find the TutorialTextTrigger group.");
}

//exec($MAP_ROOT @ "tutorial_texts.cs");
exec("./tutorial_texts.cs");

function displayText(%cl, %message) {
    %time = 0;
    %lines = 3;
    echo("displayText: " @ %message);
    commandToClient( %cl, 'CenterPrint', %message, %time, %lines );
}

function undisplayText(%cl) {
    commandToClient( %cl, 'ClearCenterPrint' );
}

datablock TriggerData(TextDisplay) {
    tickPeriodMS = 200;
};

function TextDisplay::onEnterTrigger(%this, %trigger, %obj) {
    %text = %trigger.text;
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




