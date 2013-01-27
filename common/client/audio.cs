//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Engine
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

function OpenALInit()
{
	OpenALShutdownDriver();

	echo("");
	echo("OpenAL Driver Init:");

	echo ($pref::Audio::driver);

	if($pref::Audio::driver $= "OpenAL")
	{
		if(!OpenALInitDriver())
		{
			error("	Failed to initialize driver.");
			$Audio::initFailed = true;
		} else {
			// this should go here
			echo("	Vendor: " @ alGetString("AL_VENDOR"));
			echo("	Version: " @ alGetString("AL_VERSION"));  
			echo("	Renderer: " @ alGetString("AL_RENDERER"));
			echo("	Extensions: " @ alGetString("AL_EXTENSIONS"));

         if($Pref::Audio::masterMuted)
			   alxListenerf( AL_GAIN_LINEAR, 0 );
         else
			   alxListenerf( AL_GAIN_LINEAR, $pref::Audio::masterVolume );
	
			for (%channel=1; %channel <= 8; %channel++)
         {
            if($Pref::Audio::channelMuted[%channel])
				  alxSetChannelVolume(%channel, 0);
            else
				  alxSetChannelVolume(%channel, $pref::Audio::channelVolume[%channel]);
         }

			echo("");
		}
	}

}


//--------------------------------------------------------------------------

function OpenALShutdown()
{
	OpenALShutdownDriver();
	//alxStopAll();
	//AudioGui.delete();
	//sButtonDown.delete();
	//sButtonOver.delete();
}
