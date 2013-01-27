//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function GameConnection::playMusic(%this, %profile, %immediately)
{
	if(%this.musicProfile $= %profile)
		return;

	messageClient(%this, 'MsgMusic', "", %profile.getId(), %immediately);
	%this.musicProfile = %profile;
}

function playMusicAll(%profile, %immediately)
{
	%count = ClientGroup.getCount();
	for(%i = 0; %i < %count; %i++)
	{
		%obj = ClientGroup.getObject(%i);
		%obj.playMusic(%profile, %immediately);
	}
}

function playMusic(%teamId, %profile, %immediately)
{
	%count = ClientGroup.getCount();
	for(%i = 0; %i < %count; %i++)
	{
		%obj = ClientGroup.getObject(%i);
		if(%obj.team.teamId == %teamId)
			%obj.playMusic(%profile, %immediately);
	}
}

datablock AudioProfile(MusicStartup)
{
	filename = "share/music/eth1/work1_ethernet_startup.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicIdle)
{
	filename = "share/music/eth1/work1_ethernet_idle.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicVictory1)
{
	filename = "share/music/eth1/work1_ethernet_victory1.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicVictory2)
{
	filename = "share/music/eth1/work1_ethernet_victory2.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicVictory3)
{
	filename = "share/music/eth1/work1_ethernet_victory3.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicDefeat1)
{
	filename = "share/music/eth1/work1_ethernet_defeat1.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicDefeat2)
{
	filename = "share/music/eth1/work1_ethernet_defeat2.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicDefeat3)
{
	filename = "share/music/eth1/work1_ethernet_defeat3.wav";
	description = AudioMusic;
	preload = true;
};

function serverUpdateMusic()
{
	cancel($Music::UpdateThread);

	%teamHealth[1] = 0;
	%teamHealth[2] = 0;
	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
	{
		%client = ClientGroup.getObject(%clientIndex);
		if(isObject(%client.player))
		{
			%teamHealth[%client.team.teamId] += %client.player.getDataBlock().maxDamage
				- %client.player.getDamageLevel() + %client.player.getDamageBufferLevel();
		}
	}
	%teamHealth[1] = $Team1.numPlayers == 0 ? 1.0 : %teamHealth[1] / $Team1.numPlayers / 100;
	%teamHealth[2] = $Team2.numPlayers == 0 ? 1.0 : %teamHealth[2] / $Team2.numPlayers / 100;

	%gameBalance = %teamHealth[1] - %teamHealth[2];

	%t = getSimTime();
	if(%t - $Music::LastHitTime > 30000)
	{
		playMusicAll(MusicStartup);
		$Music::Action = false;
	}
	else
	{
		%immediately = false;
		if(!$Music::Action)
			%immediately = true;

		if(%gameBalance >= 0.8)
		{
			playMusic(0, MusicVictory3, %immediately);
			playMusic(1, MusicVictory3, %immediately);
			playMusic(2, MusicDefeat3, %immediately);
		}
		else if(%gameBalance >= 0.4)
		{
			playMusic(0, MusicVictory2, %immediately);
			playMusic(1, MusicVictory2, %immediately);
			playMusic(2, MusicDefeat2, %immediately);
		}
		else if(%gameBalance <= -0.8)
		{
			playMusic(0, MusicVictory3, %immediately);
			playMusic(1, MusicDefeat3, %immediately);
			playMusic(2, MusicVictory3, %immediately);
		}
		else if(%gameBalance <= -0.4)
		{
			playMusic(0, MusicVictory2, %immediately);
			playMusic(1, MusicDefeat2, %immediately);
			playMusic(2, MusicVictory2, %immediately);
		}
		else
		{
			playMusicAll(MusicVictory1, %immediately);
		}

		$Music::Action = true;
	}

	$Music::UpdateThread = schedule(3000, 0, "serverUpdateMusic");
}
