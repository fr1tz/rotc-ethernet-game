// This script is executed on the server

$MAP_ROOT = "ethernet/maps/eth2/";

$sgLightEditor::lightDBPath = $MAP_ROOT @ "lights/";
$sgLightEditor::filterDBPath = $MAP_ROOT @ "filters/";
sgLoadDataBlocks($sgLightEditor::lightDBPath);
sgLoadDataBlocks($sgLightEditor::filterDBPath);

// sound mapping:
// 0 = soft
// 1 = hard
// 2 = metal
// 3 = snow

addMaterialMapping(
	 "dark_grey_blue_grid",
	 "sound: 2",
	 "color: 0.3 0.3 0.3 0.4 0.0"
);

