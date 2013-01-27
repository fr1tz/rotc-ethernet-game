//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - disc.explosive.cs
// Target-seeking disc projectile
//------------------------------------------------------------------------------

exec("./disc.explosive.gfx.red.cs");
exec("./disc.explosive.gfx.blue.cs");

//------------------------------------------------------------------------------

function launchExplosiveDisc(%obj)
{
	%player = %obj;
	%slot = 1;

	%projectile = %player.getMountedImage(%slot).explosiveDisc;

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
		%objectVelocity = %player.getVelocity();
		%muzzleVelocity = VectorAdd(
			VectorScale(%muzzleVector,  %muzzleSpeed),
			VectorScale(%objectVelocity, %projectile.velInheritFactor));

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

datablock NortDiscData(RedExplosiveDisc)
{
	stat = "explosivedisc";

	// script damage properties...
	impactDamage       = 0;
	impactImpulse      = 0;
	splashDamage       = 250;
	splashDamageRadius = 10;
	splashImpulse      = 5000;
	
	energyDrain = 0; // how much energy does firing this projectile drain?

	sound = ExplosiveDiscProjectileSound;
	
	targetLockTimeMS = 0;

	maxTrackingAbility = 20;
	trackingAgility = 1;

	explodesNearEnemies = false;
	explodesNearEnemiesRadius = 5;

	muzzleVelocity		= 2;
    maxVelocity         = 25;
    acceleration        = 1;
	velInheritFactor	= 1.0;

	armingDelay  = 0*1000;
	lifetime     = 10*1000;
	fadeDelay    = 0*1000;

	isBallistic = false;
	gravityMod			 = 1.0;
	bounceElasticity	 = 0.99;
	bounceFriction		 = 0.0;
    numBounces           = 0;

	projectileShapeName = "share/shapes/rotc/weapons/disc/projectile_red.dts";

	explosion             = RedExplosiveDiscExplosion;
	hitEnemyExplosion     = RedExplosiveDiscHit;
// nearEnemyExplosion	 = ThisDoesNotExist;
	hitTeammateExplosion  = RedExplosiveDiscHit;
	hitDeflectorExplosion = RedExplosiveDiscDeflectedEffect;
	bounceExplosion       = RedExplosiveDiscHit;

	particleEmitter = RedExplosiveDisc_ParticleEmitter;

	laserTrail[0] = RedExplosiveDisc_LaserTrail;
	
	//decals[0] = SlashDecalOne;

	hasLight	 = true;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
 
    startVertical = true;
};

function RedExplosiveDisc::onAdd(%this,%obj)
{
	Parent::onAdd(%this,%obj);
    %obj.setTargetingMask($TargetingMask::Disc);
}

function RedExplosiveDisc::onRemove(%this,%obj)
{
	Parent::onRemove(%this,%obj);
    if(%obj.state() == $NortDisc::Attacking)
        %obj.getTarget().removeAttackingDisc(%obj);

	%source = %obj.sourceObject;
	%source.incDiscs();
}

function RedExplosiveDisc::onDeflected(%this, %obj)
{
    if(%obj.state() == $NortDisc::Attacking)
        %obj.getTarget().removeAttackingDisc(%obj);
}

function RedExplosiveDisc::onHitTarget(%this, %obj)
{
    %obj.getTarget().removeAttackingDisc(%obj);
	%obj.schedule(0, "explode");
}

function RedExplosiveDisc::onMissedTarget(%this, %obj)
{
    // Don't stop attacking!
    //if(%obj.state() == $NortDisc::Attacking)
    //    %obj.getTarget().removeAttackingDisc(%obj);
    //
    //%obj.stopAttacking();
}

function RedExplosiveDisc::onLostTarget(%this, %obj)
{
    %obj.stopAttacking();
}

//-----------------------------------------------------------------------------

datablock NortDiscData(BlueExplosiveDisc : RedExplosiveDisc)
{
	projectileShapeName = "share/shapes/rotc/weapons/disc/projectile_blue.dts";

	explosion             = BlueExplosiveDiscExplosion;
	hitEnemyExplosion     = BlueExplosiveDiscHit;
	hitTeammateExplosion  = BlueExplosiveDiscHit;
	hitDeflectorExplosion = BlueExplosiveDiscDeflectedEffect;
	bounceExplosion       = BlueExplosiveDiscHit;

	particleEmitter = BlueExplosiveDisc_ParticleEmitter;
	laserTrail[0] = BlueExplosiveDisc_LaserTrail;

	hasLight	 = true;
	lightRadius = 10.0;
	lightColor  = "0.0 0.0 1.0";
};

function BlueExplosiveDisc::onAdd(%this,%obj)
{
	RedExplosiveDisc::onAdd(%this,%obj);
}

function BlueExplosiveDisc::onRemove(%this,%obj)
{
	RedExplosiveDisc::onRemove(%this,%obj);
}

function BlueExplosiveDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	RedExplosiveDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function BlueExplosiveDisc::onDeflected(%this, %obj)
{
    RedExplosiveDisc::onDeflected(%this, %obj);
}

function BlueExplosiveDisc::onHitTarget(%this, %obj)
{
	RedExplosiveDisc::onHitTarget(%this, %obj);
}

function BlueExplosiveDisc::onMissedTarget(%this, %obj)
{
	RedExplosiveDisc::onMissedTarget(%this, %obj);
}

function BlueExplosiveDisc::onLostTarget(%this, %obj)
{
	RedExplosiveDisc::onLostTarget(%this, %obj);
}


