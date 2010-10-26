
datablock AudioDescription(MAP_AudioThunder3d)
{
	volume	= 1.0;
	isLooping= false;

	is3D	  = true;
//	minDistance= 10.0;
	referenceDistance = 10.0;
	MaxDistance= 250.0;
	type	  = $EffectAudioType;
};

datablock AudioDescription(MAP_AudioExplosion3d)
{
	volume	= 1.0;
	isLooping= false;

	is3D	  = true;
//	minDistance= 10.0;
	referenceDistance = 10.0;
	MaxDistance= 250.0;
	type	  = $EffectAudioType;
};

datablock AudioProfile(MAP_thunderCrash1)
{
	filename  = "~/data/sound/fx/environment/thunder1.wav";
	description = AudioThunder3d;
};

datablock AudioProfile(MAP_thunderCrash2)
{
	filename  = "~/data/sound/fx/environment/thunder2.wav";
	description = AudioThunder3d;
};

datablock AudioProfile(MAP_thunderCrash3)
{
	filename  = "~/data/sound/fx/environment/thunder3.wav";
	description = AudioThunder3d;
};

datablock AudioProfile(MAP_thunderCrash4)
{
	filename  = "~/data/sound/fx/environment/thunder4.wav";
	description = AudioThunder3d;
};

datablock AudioProfile(MAP_LightningHitSound)
{
	filename = $MAP_ROOT @ "sound/lightning_impact.wav";
	description = AudioExplosion3d;
};

datablock LightningData(MAP_Lightning)
{
	//strikeTextures[0]  = $MAP_PREFIX @ "textures/lightning.dml";
	//strikeTextures[1]  = $MAP_PREFIX @ "textures/lightning.dml";
	//strikeTextures[2]  = $MAP_PREFIX @ "textures/lightning.dml";
	//strikeTextures[3]  = $MAP_PREFIX @ "textures/lightning.dml";
	//strikeTextures[4]  = $MAP_PREFIX @ "textures/lightning.dml";
	//strikeTextures[5]  = $MAP_PREFIX @ "textures/lightning.dml";
	//strikeTextures[6]  = $MAP_PREFIX @ "textures/lightning.dml";
	//strikeTextures[7]  = $MAP_PREFIX @ "textures/lightning.dml";

	strikeSound = MAP_LightningHitSound;

	// There are 8 slots for thunder sounds. Make sure all 8 slots are filled.
	// If necessary, duplicate the sounds/textures into the extra slots.
	thunderSounds[0] = MAP_thunderCrash1;
	thunderSounds[1] = MAP_thunderCrash2;
	thunderSounds[2] = MAP_thunderCrash3;
	thunderSounds[3] = MAP_thunderCrash4;
	thunderSounds[4] = MAP_thunderCrash1;
	thunderSounds[5] = MAP_thunderCrash2;
	thunderSounds[6] = MAP_thunderCrash3;
	thunderSounds[7] = MAP_thunderCrash4;
};

