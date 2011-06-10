// Mission script
// This script is executed from the mission init script in game/server/missions

// #############################################################################
// Scripting stuff now...
// #############################################################################

package Mesh {
    function startNewRound() {
        Parent::startNewRound();
        meshToggleProtected(1);
    }
};
activatePackage(Mesh);

function meshToggleProtected(%seq) {
    cancel($meshToggleProtectedMsgThread);
    cancel($meshToggleProtectedThread);

    %numZones = 8;
    %zonenames[0] = "corrtl";
    %zonenames[1] = "corrtr";
    %zonenames[2] = "corrbl";
    %zonenames[3] = "corrbr";

    %zonenames[4] = "corrl";
    %zonenames[5] = "corrr";
    %zonenames[6] = "corrt";
    %zonenames[7] = "corrb";

    if (%seq == 0) {
        %zoneprot[0] = false;
        %zoneprot[1] = true;
        %zoneprot[2] = true;
        %zoneprot[3] = false;

        %zoneprot[4] = false;
        %zoneprot[5] = false;
        %zoneprot[6] = true;
        %zoneprot[7] = true;
    } else if (%seq == 1) {
        %zoneprot[0] = true;
        %zoneprot[1] = false;
        %zoneprot[2] = false;
        %zoneprot[3] = true;

        %zoneprot[4] = true;
        %zoneprot[5] = true;
        %zoneprot[6] = false;
        %zoneprot[7] = false;
    }

    for (%i = 0; %i < %numZones; %i++) {
        %zone = NameToID(%zonenames[%i]);

		if (%zoneprot[%i] == true) {
			// break up all connections of the zone
			for (%j = 0; %j <= 1; %j++) {
				%zone.connection[%j] = "";
			}

			// set the "inital" states and execute a reset on the zone
			%zone.initiallyProtected = 1;
			%zone.initalOwner = 0;
			%zone.getDataBlock().reset(%zone);
		} else {
			// Restore the zones to their original (connected) state
			for (%j = 0; %j <= 1; %j ++) {
				%zone.connection[%j] = %zone.connection_wobbly[%j];
			}

			%zone.initiallyProtected = 0;
			%zone.initalOwner = 0;
			%zone.getDataBlock().reset(%zone);
		}

		//%zone.zProtected = %zoneprot[%i];
		//%zone.getDataBlock().setZoneOwner(%zone, 0);
    }

    $meshToggleProtectedMsgThread =
        schedule(28500, 0, "centerPrintAll", "Corridors are changing!", 1, 0);
    if (%seq == 0) 
        $meshToggleProtectedThread = schedule(30000, 0, "meshToggleProtected", 1);
    else
        $meshToggleProtectedThread = schedule(30000, 0, "meshToggleProtected", 0);
}

