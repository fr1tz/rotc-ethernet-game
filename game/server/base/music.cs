//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
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

datablock AudioProfile(MusicStartup)
{
	filename = "share/music/work1_ethernet_startup.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicIdle)
{
	filename = "share/music/work1_ethernet_idle.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicRedVictory1)
{
	filename = "share/music/work1_ethernet_victory1.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicRedVictory2)
{
	filename = "share/music/work1_ethernet_victory2.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicRedVictory3)
{
	filename = "share/music/work1_ethernet_victory3.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicBlueVictory1)
{
	filename = "share/music/work1_ethernet_defeat1.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicBlueVictory2)
{
	filename = "share/music/work1_ethernet_defeat2.wav";
	description = AudioMusic;
	preload = true;
};

datablock AudioProfile(MusicBlueVictory3)
{
	filename = "share/music/work1_ethernet_defeat3.wav";
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

	%f = mFloatLength(%gameBalance,1);
	messageTeam($Team0, 'MsgGameBalance', "", "=" @ %f);

	if(%f > 0)
	{
		messageTeam($Team1, 'MsgGameBalance', "", "+" @ %f);
		messageTeam($Team2, 'MsgGameBalance', "", -%f);
	}
	else if(%f < 0)
	{
		messageTeam($Team1, 'MsgGameBalance', "", %f);
		messageTeam($Team2, 'MsgGameBalance', "", "+" @ -%f);
	}
	else
	{
		messageTeam($Team1, 'MsgGameBalance', "", "---");
		messageTeam($Team2, 'MsgGameBalance', "", "---");
	}
	

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
			playMusicAll(MusicRedVictory3, %immediately);
		else if(%gameBalance >= 0.4)
			playMusicAll(MusicRedVictory2, %immediately);
		else if(%gameBalance <= -0.8)
			playMusicAll(MusicBlueVictory3, %immediately);
		else if(%gameBalance <= -0.4)
			playMusicAll(MusicBlueVictory2, %immediately);
		else
			playMusicAll(MusicRedVictory1, %immediately);

		$Music::Action = true;
	}

	$Music::UpdateThread = schedule(3000, 0, "serverUpdateMusic");
}
