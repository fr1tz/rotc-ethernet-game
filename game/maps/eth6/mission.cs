// This script is executed on the server

$MAP_ROOT = "ethernet/maps/wlan0/";

$sgLightEditor::lightDBPath = $MAP_ROOT @ "lights/";
$sgLightEditor::filterDBPath = $MAP_ROOT @ "filters/";
sgLoadDataBlocks($sgLightEditor::lightDBPath);
sgLoadDataBlocks($sgLightEditor::filterDBPath);

//------------------------------------------------------------------------------
// Material mappings
//------------------------------------------------------------------------------

%mapping = createMaterialMapping("gray3");
%mapping.sound = $MaterialMapping::Sound::Hard;
%mapping.color = "0.3 0.3 0.3 0.4 0.0";

%mapping = createMaterialMapping("grass2");
%mapping.sound = $MaterialMapping::Sound::Soft;
%mapping.color = "0.3 0.3 0.3 0.4 0.0";

%mapping = createMaterialMapping("rock");
%mapping.sound = $MaterialMapping::Sound::Hard;
%mapping.color = "0.3 0.3 0.3 0.4 0.0";

%mapping = createMaterialMapping("stone");
%mapping.sound = $MaterialMapping::Sound::Hard;
%mapping.color = "0.3 0.3 0.3 0.4 0.0";



/* We do not allow capturing of zones by jumping through neutral ground */
package Wlan0 {
    function TerritoryZone::setZoneOwner(%this, %zone, %teamId) {
        %i = %zone.zoneIndex;

        if (%teamId != 0) {
            if (%teamId == 1)
                %prezone = NameToID("zone" @ (%i-1));

            if (%teamId == 2)
                %prezone = NameToID("zone" @ (%i+1));

            if (%prezone != -1)
                if (%prezone.getTeamId() != %teamId) // the old zone isn't owned by the team
                    return;
            
        }

        Parent::setZoneOwner(%this, %zone, %teamId);
            
            
    }
};
activatePackage(Wlan0);



