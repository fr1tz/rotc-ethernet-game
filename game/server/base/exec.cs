//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

// to get the Lighting Editor to work, common/server/lightingSystem.cs
// must be loaded. no idea why common doesn't load it since both
// $sgLightEditor::lightDBPath and $sgLightEditor::filterDBPath are set
// within it. -mag
exec("common/server/lightingsystem.cs");

exec("./compat.cs");

exec("./commands.cs");
exec("./centerPrint.cs");
exec("./game.cs");

exec("./audiodescriptions.cs");
exec("./etherform.cs");
exec("./gameconnection.cs");
exec("./globals.cs");
exec("./party.cs");
exec("./booze.cs");
exec("./cats.cs");
exec("./fullcontrol.cs");
exec("./waypointdata.cs");
exec("./misc.cs");
exec("./camera.cs");
exec("./markers.cs");
exec("./triggers.cs");
exec("./tacticalzones.cs");
exec("./shapebase.cs");
exec("./simplecontrol.cs");
exec("./staticshape.cs");
exec("./radiusdamage.cs");
exec("./rigidShape.cs");
exec("./aiplayer.cs");
exec("./teams.cs");
exec("./loadout.cs");
exec("./spawn_and_death.cs");
exec("./teamplay.cs");
exec("./weapons.cs");
exec("./players.cs");
exec("./vehicles.cs");
exec("./turrets.cs");
exec("./objectspawn.cs");
exec("./stats.cs");
exec("./music.cs");
exec("./onlinemenu.cs");
exec("./gamestatus.cs");
