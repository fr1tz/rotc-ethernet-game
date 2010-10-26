// This script is executed on the server

$MAP_ROOT = "ethernet/maps/eth1/";

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




