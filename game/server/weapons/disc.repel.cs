//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - disc.repel.cs
// Target-seeking disc projectile
//------------------------------------------------------------------------------

exec("./disc.repel.gfx.red.cs");
exec("./disc.repel.gfx.blue.cs");

//------------------------------------------------------------------------------

function launchRepelDisc(%obj)
{
	%player = %obj;
	%slot = 1;

	%projectile = %player.getMountedImage(%slot).repelDisc;

	%target = %player.getCurrTarget();

	if(%obj.hasDisc() && %target != 0
//	&& %target.numAttackingDiscs() == 0
//	&& !%target.hasBarrier()
	)
	{
		// determine muzzle-point...
		%muzzlePoint = %player.getMuzzlePoint(%slot);

		// determine initial projectile velocity...
		%muzzleSpeed = %projectile.muzzleVelocity;

		%muzzleVector = %player.getMuzzleVector(%slot);
		%muzzleVelocity = VectorAdd(
			VectorScale(%muzzleVector,  %muzzleSpeed),
			VectorScale(%player.getVelocity(), %projectile.velInheritFactor));

		// create the disc...
		%disc = new (NortDisc)() {
			dataBlock       = %projectile;
			teamId          = %player.teamId;
			initialVelocity = %muzzleVelocity;
			initialPosition = %muzzlePoint;
			sourceObject    = %player;
			sourceSlot      = %slot;
			client          = %player.client;
		};
		MissionCleanup.add(%disc);

		%disc.setTarget(%target);

		%player.clearDiscTarget();
		%player.decDiscs();
		%target.addAttackingDisc(%player);
		%player.playAudio(0, DiscThrowSound);
		if(%target.client)
			%target.client.play2D(DiscIncomingSound);
	}
	else
	{
		if(%player.client)
			%player.client.play2D(DiscSeekerDeniedSound);
	}
}

//-----------------------------------------------------------------------------
// projectile datablock...

datablock NortDiscData(RedRepelDisc)
{
	stat = "repel";

	// script damage properties...
	impactDamage       = 0;
	impactImpulse      = 0;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	
	energyDrain = 0; // how much energy does firing this projectile drain?

	sound = DiscProjectileSound;
	
	targetLockTimeMS = 0;

	maxTrackingAbility = 100;
	trackingAgility = 2;

	explodesNearEnemies = false;
	explodesNearEnemiesRadius = 5;

	muzzleVelocity		= 30 * $Server::Game.slowpokemod;
    maxVelocity         = 75 * $Server::Game.slowpokemod;
    acceleration        = 4 * $Server::Game.slowpokemod;
	velInheritFactor	= 0.5 * $Server::Game.slowpokemod;

	armingDelay			= 0*1000;
	lifetime				= 10*1000;
	fadeDelay			  = 3*1000;

	isBallistic = true;
	gravityMod			 = 1.0 * $Server::Game.slowpokemod;
	bounceElasticity	 = 0.99;
	bounceFriction		 = 0.0;
    numBounces           = 3;

	projectileShapeName = "share/shapes/rotc/weapons/disc/projectile_red.dts";

	explosion             = RedRepelDiscExplosion;
	hitEnemyExplosion     = RedRepelDiscHit;
// nearEnemyExplosion	 = ThisDoesNotExist;
	hitTeammateExplosion  = RedRepelDiscHit;
	hitDeflectorExplosion = RedRepelDiscDeflectedEffect;
	bounceExplosion       = RedRepelDiscHit;

	laserTrail[0]		= RedRepelDiscInnerLaserTrail;
	laserTrail[1]		= RedRepelDiscOuterLaserTrail;
	
	decals[0] = SlashDecalOne;

	hasLight	 = true;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
 
    startVertical = true;
};

function RedRepelDisc::onAdd(%this,%obj)
{
	Parent::onAdd(%this,%obj);
    %obj.setTargetingMask($TargetingMask::Disc);
}

function RedRepelDisc::onRemove(%this,%obj)
{
	Parent::onRemove(%this,%obj);
    if(%obj.state() == $NortDisc::Attacking)
        %obj.getTarget().removeAttackingDisc(%obj);

	%source = %obj.sourceObject;
	%source.incDiscs();
}

function RedRepelDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

	if(%col.getType() & $TypeMasks::PlayerObjectType)
	{
 		// collision with player:
		// give 20 points of energy to the attacker...
		%source = %obj.sourceObject;
		if(%source.getDamageState() $= "Enabled")
			%source.setEnergyLevel(%source.getEnergyLevel() + 20);

		// remove possible barrier from enemy...
		%col.deactivateBarrier();

		// don't remove anchoring for now...
		//%col.setGridConnection(%col.gridConnection*0.50);

		// push enemy away from player...
		%vec = VectorSub(%col.getPosition(), %source.getPosition());
		%vec = VectorNormalize(%vec);
		%vec = getWord(%vec,0) SPC getWord(%vec,1) SPC "0.5";
		//%vec = "0 0 1";
		%vec = VectorScale(%vec, 3500);
		// ignore anchoring for impulse...
		//%col.impulse(%col.getPosition(), %vec);
		%col.applyImpulse(%col.getPosition(), %vec);

		createExplosion(%this.explosion, %pos, %normal);
   
        // give another disc-lock to the attacker
//        %src =  %obj.getSourceObject();
//        if(%src)
//            %src.setDiscTarget(%col);
	}
}

function RedRepelDisc::onDeflected(%this, %obj)
{
    if(%obj.state() == $NortDisc::Attacking)
        %obj.getTarget().removeAttackingDisc(%obj);
}

function RedRepelDisc::onHitTarget(%this, %obj)
{
    %obj.getTarget().removeAttackingDisc(%obj);
    %obj.stopAttacking();
}

function RedRepelDisc::onMissedTarget(%this, %obj)
{
    if(%obj.state() == $NortDisc::Attacking)
        %obj.getTarget().removeAttackingDisc(%obj);

    %obj.stopAttacking();
}

function RedRepelDisc::onLostTarget(%this, %obj)
{
    %obj.stopAttacking();
}

//-----------------------------------------------------------------------------

datablock NortDiscData(BlueRepelDisc : RedRepelDisc)
{
	projectileShapeName = "share/shapes/rotc/weapons/disc/projectile_blue.dts";

	explosion             = BlueRepelDiscExplosion;
	hitEnemyExplosion     = BlueRepelDiscHit;
	hitTeammateExplosion  = BlueRepelDiscHit;
	hitDeflectorExplosion = BlueRepelDiscDeflectedEffect;
	bounceExplosion       = BlueRepelDiscHit;

	laserTrail[0]		= BlueRepelDiscInnerLaserTrail;
	laserTrail[1]		= BlueRepelDiscOuterLaserTrail;

	fxLight				 = BlueRepelDiscFxLight;

	hasLight	 = true;
	lightRadius = 10.0;
	lightColor  = "0.0 0.0 1.0";
};

function BlueRepelDisc::onAdd(%this,%obj)
{
	RedRepelDisc::onAdd(%this,%obj);
}

function BlueRepelDisc::onRemove(%this,%obj)
{
	RedRepelDisc::onRemove(%this,%obj);
}

function BlueRepelDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	RedRepelDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function BlueRepelDisc::onDeflected(%this, %obj)
{
    RedRepelDisc::onDeflected(%this, %obj);
}

function BlueRepelDisc::onHitTarget(%this, %obj)
{
	RedRepelDisc::onHitTarget(%this, %obj);
}

function BlueRepelDisc::onMissedTarget(%this, %obj)
{
	RedRepelDisc::onMissedTarget(%this, %obj);
}

function BlueRepelDisc::onLostTarget(%this, %obj)
{
	RedRepelDisc::onLostTarget(%this, %obj);
}


