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
%mapping.color = "0 1 0 1.0 0.0";

%mapping = createMaterialMapping("gray3");
%mapping.sound = $MaterialMapping::Sound::Hard;
%mapping.color = "0.3 0.3 0.3 0.4 0.0";

//------------------------------------------------------------------------------
// Precipitation
//------------------------------------------------------------------------------

datablock PrecipitationData(MissionSnow)
{
   dropTexture = "share/textures/eth/precipitation1";
   //splashTexture = "~/data/precipitation/water_splash";
   dropSize = 0.40;
   //splashSize = 0.2;
   useTrueBillboards = true;
   splashMS = 200;
};

