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




