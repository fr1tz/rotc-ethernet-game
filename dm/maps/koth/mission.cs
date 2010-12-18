// This script is executed on the server

$MAP_ROOT = "dm/maps/koth/";

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

%mapping = createMaterialMapping("dark_blue_grid");
%mapping.sound = $MaterialMapping::Sound::Metal;
%mapping.color = "0.3 0.3 0.3 0.4 0.0";
//%mapping.envmap = "share/textures/malloc/dark_blue_grid 0.5";


package KOtH {
    // We don't allow spawning in non-red zones
    function GameConnection::togglePlayerForm(%this) {
       	if(!isObject(%this.player))
	    	return;

        if (%this.player.getClassName() !$= "Player") { // etherform -> CAT
            %pos = %this.player.getWorldBoxCenter();

            InitContainerRadiusSearch(%pos, 0.0001, $TypeMasks::TacticalZoneObjectType);

            while((%srchObj = containerSearchNext()) != 0) {
    			%zoneTeamId = %srchObj.getTeamId();

	    		if (%zoneTeamId != 1) {
	    		    bottomPrint(%this, "Can only manifest inside the red zone!", 3, 1 );
        			return;
                }
            }
		}


        Parent::togglePlayerForm(%this);
    }

    // We'll just leave zones as they are.
    function TerritoryZone::updateOwner(%this, %zone) {
    }
};
activatePackage(KOtH);

datablock TriggerData(MissionWinTrigger) {
    tickPeriodMS = 200;
};

function MissionWinTrigger::onEnterTrigger(%this, %trigger, %obj) {
    Parent::onEnterTrigger(%this, %trigger, %obj);

    if (%obj.getClassName() !$= "Player")
        return;

    centerPrintAll(%obj.getShapeName() SPC "has climbed the hill!", 3);

	serverPlay2D(RedVictorySound);
	schedule(5000,0,"startNewRound");
	$Game::RoundRestarting = true;

    
    for (%i = 0; %i < ClientGroup.GetCount(); %i++) {
        %c = ClientGroup.getClient(%i);

        if (%c != %obj.client)
            %c.kill();
    }

}

function MissionWinTrigger::onLeaveTrigger(%this, %trigger, %obj) {
    Parent::onLeaveTrigger(%this, %trigger, %obj);

}

function MissionWinTrigger::onTickTrigger(%this, %trigger) {
    Parent::onTickTrigger(%this, %trigger);
}



