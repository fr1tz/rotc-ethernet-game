//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function OptAudioWindow::onWake(%this)
{
	OptAudioVolumeMaster.setValue( $pref::Audio::masterVolume);
	OptAudioVolumeShell.setValue(  $pref::Audio::channelVolume[$GuiAudioType]);
	OptAudioVolumeSim.setValue(	 $pref::Audio::channelVolume[$SimAudioType]);
	OptAudioVolumeMusic.setValue($pref::Audio::channelVolume[$MusicAudioType]);
}

function OptAudioWindow::onAddedAsWindow(%this)
{
	OptAudioUpdate();
}

// Audio
function OptAudioUpdate()
{
	// set the driver text
	%text =	"Vendor: " @ alGetString("AL_VENDOR") @
			  "\nVersion: " @ alGetString("AL_VERSION") @
			  "\nRenderer: " @ alGetString("AL_RENDERER") @
			  "\nExtensions: " @ alGetString("AL_EXTENSIONS");
	OptAudioInfo.setText(%text);
}


// Channel 0 is unused in-game, but is used here to test master volume.

new AudioDescription(AudioChannel0)
{
	volume	= 1.0;
	isLooping= false;
	is3D	  = false;
	type	  = 0;
};

new AudioDescription(AudioChannel1)
{
	volume	= 1.0;
	isLooping= false;
	is3D	  = false;
	type	  = 1;
};

new AudioDescription(AudioChannel2)
{
	volume	= 1.0;
	isLooping= false;
	is3D	  = false;
	type	  = 2;
};

new AudioDescription(AudioChannel3)
{
	volume	= 1.0;
	isLooping= false;
	is3D	  = false;
	type	  = 3;
};

new AudioDescription(AudioChannel4)
{
	volume	= 1.0;
	isLooping= false;
	is3D	  = false;
	type	  = 4;
};

new AudioDescription(AudioChannel5)
{
	volume	= 1.0;
	isLooping= false;
	is3D	  = false;
	type	  = 5;
};

new AudioDescription(AudioChannel6)
{
	volume	= 1.0;
	isLooping= false;
	is3D	  = false;
	type	  = 6;
};

new AudioDescription(AudioChannel7)
{
	volume	= 1.0;
	isLooping= false;
	is3D	  = false;
	type	  = 7;
};

new AudioDescription(AudioChannel8)
{
	volume	= 1.0;
	isLooping= false;
	is3D	  = false;
	type	  = 8;
};

$AudioTestHandle = 0;

function OptAudioUpdateMasterVolume(%volume)
{
   if($Pref::Audio::masterMuted)
   {
      alxListenerf(AL_GAIN_LINEAR, 0);
      return;
   }

	if(%volume == $pref::Audio::masterVolume)
		return;

   %playTest = true;

   if(%volume == -1)
   {
      %volume = $pref::Audio::masterVolume;
      %playTest = false;
   }

	alxListenerf(AL_GAIN_LINEAR, %volume);
	$pref::Audio::masterVolume = %volume;
	if(%playTest && !alxIsPlaying($AudioTestHandle))
	{
		$AudioTestHandle = alxCreateSource("AudioChannel0", expandFilename("share/sounds/rotc/spin2.wav"));
		alxPlay($AudioTestHandle);
	}
}

function OptAudioUpdateChannelVolume(%channel, %volume)
{
	if (%channel < 1 || %channel > 8)
		return;

   if($Pref::Audio::channelMuted[%channel])
   {
      alxSetChannelVolume(%channel, 0);
      return;
   }

	if (%volume == $Pref::Audio::channelVolume[%channel])
		return;

   %playTest = true;

   if(%volume == -1)
   {
      %volume = $Pref::Audio::channelVolume[%channel];
      %playTest = false;
   }

	alxSetChannelVolume(%channel, %volume);
	$pref::Audio::channelVolume[%channel] = %volume;
	if(%playTest && !alxIsPlaying($AudioTestHandle))
	{
		$AudioTestHandle = alxCreateSource("AudioChannel"@%channel, expandFilename("share/sounds/rotc/spin2.wav"));
		alxPlay($AudioTestHandle);
	}
}
