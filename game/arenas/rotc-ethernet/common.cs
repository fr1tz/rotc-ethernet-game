$MissionInfo::Type = "\cp\c0ROTC:ETH\c6" SPC $GameVersionString @ "\co";
$MissionInfo::TypeDesc = "Capture all the opposing team's zones to win.";
$MissionInfo::InitScript = "game/server/missions/rotc-eth.cs";
$MissionInfo::MutatorDesc = ""
	@ "supersniper-arena\tCATs carry hyper-powered sniper rifles\n"
	@ "temptag\tPlayers are tagged from when they take damage until they're out of enemy LOS\n"
	@ "nevertag\tPlayers are never tagged\n"
	@ "halfhealth\tCATs have 75 instead of 150 health\n"
	@ "lowhealth\tCATs have 50 instead of 150 health\n"
	@ "shields\tCATs have a small regenerating shield\n"
	@ "damper\tCATs take less damage the more energy they have\n"
	@ "novamp\tDeactivate CAT 'V-AMP' module\n"
	@ "crater\tTriples falling damage\n"
	@ "slowpoke\tMakes the game slow\n"
	@ "oobdeath\tMakes CATs lose health when not inside a zone\n"
	@ "PRO\tCombo: damper/shields/halfhealth/crater\n"
	@ "INSTAGIB\tCombo: supersniper-arena/novamp/nevertag/oobdeath\n"
	@ "";

