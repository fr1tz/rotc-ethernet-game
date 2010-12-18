//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Load up defaults console values.
// Currently there are no defaults pref values that need to be loaded at this
// point. Simply uncomment this line of code if you do need them in the future.
//exec("./defaults.cs");

//-----------------------------------------------------------------------------

function initCommon()
{
	// All mods need the random seed set
	setRandomSeed();

	// Very basic functions used by everyone
	exec("./client/canvas.cs");
	exec("./client/audio.cs");
	exec("./auxiliary/array.cs");
	exec("./auxiliary/missionInfo.cs");
}

function initBaseClient()
{
	// Base client functionality
	exec("./client/message.cs");
	exec("./client/mission.cs");
	exec("./client/missionDownload.cs");
	exec("./client/actionMap.cs");
	exec("./client/actionMap.recording.cs");
	exec("./client/materialMappings.cs");
    exec("./client/mumble.cs");
    exec("./client/tacticalZones.cs");

	// There are also a number of support scripts loaded by the canvas
	// when it's first initialized.  Check out client/canvas.cs
 
    // Init Mumble link subsystem.
    initMumbleLink();
}

function initBaseServer()
{
	// Base server functionality
	exec("./server/audio.cs");
	exec("./server/server.cs");
	exec("./server/message.cs");
	exec("./server/commands.cs");
	exec("./server/missionLoad.cs");
	exec("./server/missionDownload.cs");
	exec("./server/clientConnection.cs");
	exec("./server/kickban.cs");
	exec("./server/materialMappings.cs");
}	


//-----------------------------------------------------------------------------
package Common {

function displayHelp() {
	Parent::displayHelp();
	error(
		"Common Mod options:\n"@
		"  -fullscreen         Starts game in full screen mode\n"@
		"  -windowed           Starts game in windowed mode\n"@
		"  -autoVideo          Auto detect video, but prefers OpenGL\n"@
		"  -openGL             Force OpenGL acceleration\n"@
		"  -directX            Force DirectX acceleration\n"@
		"  -voodoo2            Force Voodoo2 acceleration\n"@
		"  -noSound            Starts game without sound\n"@
		"  -prefs <file>       Exec the config file\n"
	);
}

function parseArgs()
{
	Parent::parseArgs();

	// Arguments override defaults...
	for (%i = 1; %i < $Game::argc ; %i++)
	{
		%arg = $Game::argv[%i];
		%nextArg = $Game::argv[%i+1];
		%hasNextArg = $Game::argc - %i > 1;
	
		switch$ (%arg)
		{
			//--------------------
			case "-fullscreen":
				$pref::Video::fullScreen = 1;
				$argUsed[%i]++;

			//--------------------
			case "-windowed":
				$pref::Video::fullScreen = 0;
				$argUsed[%i]++;

			//--------------------
			case "-noSound":
				error("no support yet");
				$argUsed[%i]++;

			//--------------------
			case "-openGL":
				$pref::Video::displayDevice = "OpenGL";
				$argUsed[%i]++;

			//--------------------
			case "-directX":
				$pref::Video::displayDevice = "D3D";
				$argUsed[%i]++;

			//--------------------
			case "-voodoo2":
				$pref::Video::displayDevice = "Voodoo2";
				$argUsed[%i]++;

			//--------------------
			case "-autoVideo":
				$pref::Video::displayDevice = "";
				$argUsed[%i]++;

			//--------------------
			case "-prefs":
				$argUsed[%i]++;
				if (%hasNextArg) {
					exec(%nextArg, true, true);
					$argUsed[%i+1]++;
					%i++;
				}
				else
					error("Error: Missing Command Line argument. Usage: -prefs <path/script.cs>");
		}
	}
}

function onStart()
{
	Parent::onStart();
	echo("\n--------- Initializing MOD: Common ---------");
	initCommon();
}

function onExit()
{
	echo("Exporting client prefs");
	export("$pref::*", "./client/prefs.cs", False);

	echo("Exporting server prefs");
	export("$Pref::Server::*", "./server/prefs.cs", False);
	BanList::Export("./server/banlist.cs");

	OpenALShutdown();
	Parent::onExit();
}

}; // Common package
activatePackage(Common);

// This is here to prevent console spam
// EditorGui.cs will eventually override it
// with a more useful function call
function onNeedRelight()
{
}


function generateRandomChunkery(%parent)
{
	$chunkTestLevel--;
	%count = getRandom(100);
	
	for(%i=0; %i<%count; %i++)
	{
		$chunkCreateCount++;
		
			
		%obj = new TextChunk()
		{
			textData = "level= " @ $chunkTestLevel @ ", idx = " @ %i @ $buff;
		};
		
		if($chunkTestLevel > 0)
			generateRandomChunkery(%obj);

		%parent.add(%obj);
		
		// If we're hitting our limit, stop making stuff.
		if($chunkCreateCount > $chunkMaxCount)
			break;
	}
	
	$chunkTestLevel++;
}

function generateBigChunkTest(%buffSize, %chunkCount)
{
	$chunkStartTime = getRealTime();
	
	$chunkTestLevel = 10;
	$chunkCreateCount = 0;
	$chunkMaxCount = %chunkcount; // At most 16 megs of data.
	
	$a = getRealTime();
	$buff = "";
	while(strlen($buff) < %buffSize)
		$buff = $buff @ "klmadskldasjkadlkjsakjlsdjksldakjlkjdlaakjlsdkljaslkjslkjdaslkjd";
	$b = getRealTime();

	$root = new TextChunk() { textData = "ROOT"; };

	$c = getRealTime();
	generateRandomChunkery($root);
	$d = getRealTime();

	echo("Created " @ $chunkCreateCount @ " chunks in " @ $chunkTestLevel @ " levels.");
	
	%file = "common/bigchunktest.chunk";
	echo("Saving to '" @ %file @ "'...");
	$e = getRealTime();
	saveChunkFile($root, %file);
	$f = getRealTime();
	echo("Done.");
	
	echo("Deleting object hierarchy...");
	$root.delete();
	echo("Done.");
	
	echo("Loading object hierarchy from '"@%file@"'...");
	$g = getRealTime();
	$newRoot = loadChunkFile(%file);
	$h = getRealTime();
	echo("Done.");
	
	echo("chunkCount = " @ $chunkCreateCount @ ", chunkSize=" @ %buffSize);
	echo("Generated data = " @ ($chunkCreateCount * %buffSize));
	echo("Elapsed time = " @ ((getRealTime() - $chunkStartTime)/1000) @ " sec.");
	echo("Buffer init  = " @ (($b-$a)/1000) @ " sec.");
	echo("chunk gen	 = " @ (($d-$c)/1000) @ " sec.");
	echo("save time	 = " @ (($f-$e)/1000) @ " sec.");
	echo("load time	 = " @ (($h-$g)/1000) @ " sec.");
	
	return $newRoot;
}

function testchunk()
{
	%foo = 
	new TextChunk()
	{
		textData = "pony";
		
		new TextChunk()
		{
			textData = "child1";
			
		new TextChunk()
		{
			textData = "childQ";
		};
		};

		new TextChunk()
		{
			textData = "child2";
		};
		new TextChunk()
		{
			textData = "child3";

		new TextChunk()
		{
			textData = "childA";
		};

		new TextChunk()
		{
			textData = "childB";
		};

		new TextChunk()
		{
			textData = "childC";
		};

		new TextChunk()
		{
			textData = "childD";
		};

		new TextChunk()
		{
			textData = "childE";
		};
		};
	 };
	 
	 saveChunkFile(%foo, "starter.fps/test.chunk");
	 
	 %foo.delete();
	 
	 $foo = loadChunkFile("starter.fps/test.chunk");
		
		
}
