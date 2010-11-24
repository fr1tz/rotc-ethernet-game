//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function OptAudioWindow::onWake(%this)
{
	OptAudioUpdate();
	OptAudioVolumeMaster.setValue( $pref::Audio::masterVolume);
	OptAudioVolumeShell.setValue(  $pref::Audio::channelVolume[$GuiAudioType]);
	OptAudioVolumeSim.setValue(	 $pref::Audio::channelVolume[$SimAudioType]);

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
	if (%volume == $pref::Audio::masterVolume)
		return;
	alxListenerf(AL_GAIN_LINEAR, %volume);
	$pref::Audio::masterVolume = %volume;
	if (!alxIsPlaying($AudioTestHandle))
	{
		$AudioTestHandle = alxCreateSource("AudioChannel0", expandFilename("share/sounds/rotc/testing.wav"));
		alxPlay($AudioTestHandle);
	}
}

function OptAudioUpdateChannelVolume(%channel, %volume)
{
	if (%channel < 1 || %channel > 8)
		return;

	if (%volume == $pref::Audio::channelVolume[%channel])
		return;

	alxSetChannelVolume(%channel, %volume);
	$pref::Audio::channelVolume[%channel] = %volume;
	if (!alxIsPlaying($AudioTestHandle))
	{
		$AudioTestHandle = alxCreateSource("AudioChannel"@%channel, expandFilename("share/sounds/rotc/testing.wav"));
		alxPlay($AudioTestHandle);
	}
}
