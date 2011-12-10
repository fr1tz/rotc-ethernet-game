// Mission environment script
// This script is executed from the mission init script in game/server/missions

//------------------------------------------------------------------------------
// Lights
//------------------------------------------------------------------------------

$sgLightEditor::lightDBPath = $Server::MissionDirectory @ "lights/";
$sgLightEditor::filterDBPath = $Server::MissionDirectory @ "filters/";
sgLoadDataBlocks($sgLightEditor::lightDBPath);
sgLoadDataBlocks($sgLightEditor::filterDBPath);

//------------------------------------------------------------------------------
// Material mappings
//------------------------------------------------------------------------------

%mapping = createMaterialMapping("dark_grey_blue_grid");
%mapping.sound = $MaterialMapping::Sound::Metal;
%mapping.color = "0.3 0.3 0.3 0.4 0.0";

%mapping = createMaterialMapping("malloc/dark_blue_grid");
%mapping.sound = $MaterialMapping::Sound::Metal;
%mapping.color = "0.3 0.3 0.3 0.4 0.0";
//%mapping.envmap = "share/textures/malloc/dark_blue_grid 0.5";
