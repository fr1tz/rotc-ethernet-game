//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - misc.cs
// various code snippets which are too small to jusitfy an own file for them
//------------------------------------------------------------------------------

datablock ParticleEmitterNodeData(DefaultEmitterNode)
{
	timeMultiple = 1;
};

datablock ParticleEmitterNodeData(DoubleTimeEmitterNode)
{
	timeMultiple = 2;
};

datablock ParticleEmitterNodeData(HalfTimeEmitterNode)
{
	timeMultiple = 0.5;
};

// hook into the mission editor...
function fxLightData::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type.
	%obj = new fxLight() {
		dataBlock = %data;
	};
	return %obj;
}

datablock AudioProfile(ClownSound)
{
	filename = "~/data/sound/events/clown.wav";
	description = Audio2D;
	preload = true;
};

datablock AudioProfile(RedVictorySound)
{
	filename = "~/data/sound/events/victory.wav";
	description = Audio2D;
	preload = true;
};

datablock AudioProfile(BlueVictorySound)
{
	filename = "~/data/sound/events/victory.wav";
	description = Audio2D;
	preload = true;
};

datablock AudioProfile(DamageSoundOne)
{
	filename = "~/data/sound/damage1.wav";
	description = Audio2D;
	preload = true;
};

datablock AudioProfile(DamageSoundTwo)
{
	filename = "~/data/sound/damage2.wav";
	description = Audio2D;
	preload = true;
};

function serverCmdTaggedToTarget(%client)
{
	if( isObject(%client.player) )
	{
		%player = %client.player;
		
		// check if player's able to select target...
		if( getSimTime() < %player.targetSelectTime + 2000 )
			return;

		// want to target current target = clear current target...
		%oldTarget = %player.getCurrTarget();
		%newTarget = %player.getCurrTagged();
		if( %newTarget == %oldTarget )
			%newTarget = 0;

		// notify old target that he's not targeted anymore...
//		if( isObject(%oldTarget) )
//		{
//			commandToClient(%oldTarget.client,'DecreaseLockCount');
//			%oldTarget.client.play2D(TargetLockDecreaseSound);
//		}

		// notify new target that he's now targeted
		if( isObject(%newTarget) )
		{
			%newTarget.client.play2D(TargetLockAquiredSound);
		}

		%player.setCurrTarget(%newTarget);
		%player.client.play2D(TargetLockAquiredSound);
		%player.targetSelectTime = getSimTime();
	}
}

function createExplosion(%data,%pos)
{
	%count = ClientGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if( !%cl.isAIControlled() )
			commandToClient(%cl, 'CreateExplosion', %data, %pos);
	}
}

function setTimeScale(%x)
{
	$timeScale = %x;
	%count = ClientGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if( !%cl.isAIControlled() )
			commandToClient(%cl, 'SetTimeScale', %x);
	}
}

function getVisibleDistance()
{
	%sky = nameToID("Sky");
	if(%sky != -1)
	{
		return %sky.visibleDistance;
	}
	else
	{
		error("getVisibleDistance(): 'Sky' not found to get visible distance from");
		return 0;
	}
}

