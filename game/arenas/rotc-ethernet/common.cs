$MissionInfo::Type = "\cp\c6^_^ \c0ROTC:ETH\c2" SPC $GameVersionString @ "\co";
$MissionInfo::TypeDesc = "Capture all the opposing team's zones to win.";
$MissionInfo::InitScript = "game/server/missions/rotc-eth.cs";
$MissionInfo::MutatorDesc = ""
	@ "temptag\tPlayers are tagged from when they take damage until they're out of enemy LOS\n"
	@ "nevertag\tPlayers are never tagged\n"
	@ "noshield\tDeactivate CAT shields\n"
	@ "lowhealth\tCATs have 50 instead of 75 health\n"
	@ "slowpoke\tMakes the game slow\n"
	@ "superblaster\tReplaces normal blaster with high-powered hitscan version\n"
	@ "";

