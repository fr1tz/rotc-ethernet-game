//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------

// Variables used by client scripts & code.  The ones marked with (c)
// are accessed from code.  Variables preceeded by Pref:: are client
// preferences and stored automatically in the ~/client/prefs.cs file
// in between sessions.
//
//	 (c) Client::MissionFile				 Mission file name
//	 ( ) Client::Password					 Password for server join

//	 (?) Pref::Player::CurrentFOV
//	 (?) Pref::Player::DefaultFov
//	 ( ) Pref::Input::KeyboardTurnSpeed

//	 (c) pref::Master[n]					  List of master servers
//	 (c) pref::Net::RegionMask	  
//	 (c) pref::Client::ServerFavoriteCount
//	 (c) pref::Client::ServerFavorite[FavoriteCount]
//	 .. Many more prefs... need to finish this off

// Moves, not finished with this either...
//	 (c) firstPerson
//	 $mv*Action...

//-----------------------------------------------------------------------------
// These are variables used to control the shell scripts and
// can be overriden by mods:

//-----------------------------------------------------------------------------

function initClient()
{
	echo("\n--------- Initializing Ethernet: Client ---------");
	
	// Make sure this variable reflects the correct state.
	$Server::Dedicated = false;

	// Game information used to query the master server
	$Client::GameTypeQuery = $GameNameString;
	$Client::MissionTypeQuery = "Any";

	// The common module provides basic client functionality
	initBaseClient();
 
    // Our GUI profiles need to be created before initCanvas is called
    // and creates default profiles for essential ones that don't exist.
    exec("./ui/shell/profiles.cs");
    exec("./ui/hud/profiles.cs");

	// InitCanvas starts up the graphics system.
	// The canvas needs to be constructed before the gui scripts are
	// run because many of the controls assume the canvas exists at
	// load time.
	initCanvas("Revenge Of The Cats: Ethernet (" @ $GameVersionString @ ")");

	/// load client-side Audio Descriptions
	exec("./scripts/audioDescriptions.cs");

	// execute the UI scripts
	exec("./ui/init.cs");

	// execute the game client scripts
	exec("./scripts/client.cs");
	exec("./scripts/missionDownload.cs");
	exec("./scripts/serverConnection.cs");
	exec("./scripts/game.cs");
	exec("./scripts/misc.cs");
 	exec("./scripts/mumble.cs");

	// default key bindings
	exec("./scripts/actionmap.global.cs");
	exec("./scripts/actionmap.standard.cs");
	exec("./scripts/actionmap.aircraft.cs");
	exec("./scripts/actionmap.cmdrscreen.cs");

	// user-defined key bindings...
	exec("./config.cs");

	// Really shouldn't be starting the networking unless we are
	// going to connect to a remote server, or host a multi-player
	// game.
	setNetPort(0);

	// Copy saved script prefs into C++ code.
	setShadowDetailLevel( $Pref::shadows );
	setDefaultFov( $Pref::Player::DefaultFov );
	setZoomSpeed( $Pref::Player::ZoomSpeed );

    // Set default cursor.
    Canvas.setCursor(DefaultCursor);
 
    if($JoinGameAddress !$= "")
    {
        // If we are instantly connecting to an address, go directly
        // to the shell and attempt the connect.
        Canvas.setContent(Shell);
        connect($JoinGameAddress, "", $Pref::Player::Name);
    }
    else
    {
        // Otherwise go to the splash screen.
        showTorqueSplashScreen(Shell);
    }
}

//-----------------------------------------------------------------------------


