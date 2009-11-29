// This script is executed on the server

$MAP_ROOT = "dm/maps/dm2/";

$sgLightEditor::lightDBPath = $MAP_ROOT @ "lights/";
$sgLightEditor::filterDBPath = $MAP_ROOT @ "filters/";
sgLoadDataBlocks($sgLightEditor::lightDBPath);
sgLoadDataBlocks($sgLightEditor::filterDBPath);

//exec("./difs/propertymap.cs");
//exec("./scripts/env.cs");



