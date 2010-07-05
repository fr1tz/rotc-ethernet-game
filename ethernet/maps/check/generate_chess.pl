#!/usr/bin/perl
use strict;
use warnings;

my $top = q~
//--- OBJECT WRITE BEGIN ---
new SimGroup(MissionGroup) {
   canSaveDynamicFields = "1";

   new SimGroup(essentials) {
      canSaveDynamicFields = "1";

      new MissionArea(MissionArea) {
         canSaveDynamicFields = "1";
         Area = "-6016 -6016 12032 12032";
         flightCeiling = "1000";
         flightCeilingRange = "20";
            Locked = "true";
      };
      new Sky(Sky) {
         canSaveDynamicFields = "1";
         position = "744 -432 0";
         rotation = "1 0 0 0";
         scale = "1 1 1";
         materialList = "share/skies/eth4/sky.dml";
         cloudHeightPer[0] = "0.2";
         cloudHeightPer[1] = "0.2";
         cloudHeightPer[2] = "0.1";
         cloudSpeed1 = "0.001";
         cloudSpeed2 = "0.09";
         cloudSpeed3 = "0.09";
         visibleDistance = "300";
         fogDistance = "250";
         fogColor = "0.015686 0 0.192157 1";
         fogStorm1 = "0";
         fogStorm2 = "0";
         fogStorm3 = "0";
         fogVolume1 = "300 0 190";
         fogVolume2 = "0 0 0";
         fogVolume3 = "0 0 0";
         fogVolumeColor1 = "1 1 1 1";
         fogVolumeColor2 = "1 1 1 1";
         fogVolumeColor3 = "0 0 0 1";
         windVelocity = "0 1 0";
         windEffectPrecipitation = "1";
         SkySolidColor = "0 0 0 1";
         useSkyTextures = "1";
         renderBottomTexture = "0";
         noRenderBans = "0";
            Locked = "true";
      };
      new Sun(Sun) {
         canSaveDynamicFields = "1";
         azimuth = "0";
         elevation = "30";
         color = "0.4 0.4 0.4 1";
         ambient = "0.2 0.2 0.2 1";
         CastsShadows = "1";
            Locked = "1";
      };
      new TerrainBlock(Terrain) {
         canSaveDynamicFields = "1";
         rotation = "1 0 0 0";
         scale = "1 1 1";
         detailTexture = "share/textures/malloc/detail.grid";
         terrainFile = "./mission.ter";
         squareSize = "8";
         bumpScale = "1";
         bumpOffset = "0.01";
         zeroBumpScale = "8";
         tile = "1";
            Locked = "true";
            position = "-1024 -1024 0";
      };
   };
   new SimGroup(ObserverPoints) {
      canSaveDynamicFields = "1";

      new SpawnSphere(OBSsphere) {
         canSaveDynamicFields = "1";
         position = "0 0 250";
         rotation = "1 0 0 0";
         scale = "1 1 1";
         dataBlock = "SpawnSphereMarker";
         teamId = "0";
         isTagging = "0";
         Radius = "20";
         sphereWeight = "1";
         indoorWeight = "1";
         outdoorWeight = "1";
            Locked = "False";
            lockCount = "0";
            checkTaggedThread = "3172";
            homingCount = "0";
      };
   };
   new SimGroup(RedSpawnPoints) {
      canSaveDynamicFields = "1";

      new SpawnSphere(T1drop) {
         canSaveDynamicFields = "1";
         position = "-228 172 160";
         rotation = "1 0 0 0";
         scale = "1 1 1";
         nameTag = "T1drop";
         dataBlock = "SpawnSphereMarker";
         teamId = "0";
         isTagging = "0";
         Radius = "10";
         sphereWeight = "1";
         indoorWeight = "1";
         outdoorWeight = "1";
            Locked = "False";
            lockCount = "0";
            checkTaggedThread = "3173";
            homingCount = "0";
      };
   };
   new SimGroup(BlueSpawnPoints) {
      canSaveDynamicFields = "1";

      new SpawnSphere(T2drop) {
         canSaveDynamicFields = "1";
         position = "228 -172 160";
         rotation = "1 0 0 0";
         scale = "1 1 1";
         nameTag = "T2drop";
         dataBlock = "SpawnSphereMarker";
         teamId = "0";
         isTagging = "0";
         Radius = "10";
         sphereWeight = "1";
         indoorWeight = "1";
         outdoorWeight = "1";
            Locked = "False";
            lockCount = "0";
            checkTaggedThread = "3174";
            homingCount = "0";
      };
   };
   new SimGroup(TerritoryZones) {
      canSaveDynamicFields = "1";

      new TacticalZone(topleft) {
         canSaveDynamicFields = "1";
         position = "6 6 120";
         rotation = "1 0 0 0";
         scale = "12 12 50";
         dataBlock = "TerritoryZone";
         teamId = "0";
         borderBottom = "0";
         borderLeft = "1";
         borderBack = "1";
         borderFront = "1";
         borderRight = "1";
         borderTop = "0";
            numBlues = "0";
            initialOwner = "1";
            numReds = "0";
      };
~;

my $bottom = q~
   };
};
//--- OBJECT WRITE END ---
~;

my $zone = q~
      new TacticalZone(zone%i%j) {
         canSaveDynamicFields = "1";
         position = "%x %y 120";
         rotation = "1 0 0 0";
         scale = "12 12 50";
         dataBlock = "TerritoryZone";
         teamId = "0";
         borderBottom = "0";
         borderLeft = "1";
         borderBack = "1";
         borderFront = "1";
         borderRight = "1";
         borderTop = "0";
            numBlues = "0";
            initialOwner = "%o";
            numReds = "0";
      };~;

 
print $top;
for (my $x = -4; $x < 4; ++$x) {
    for (my $y = -4; $y < 4; ++$y) {
        my $o = (($x + $y)%2==0) ? 1 : 2;

        my $z = $zone;
        $z =~ s~%i~$x~g;
        $z =~ s~%j~$y~g;
        $z =~ s~%x~$x*24+12~e;
        $z =~ s~%y~$y*24+12~e;
        $z =~ s~%o~$o~e;

        print $z;
    }
}
print $bottom;

