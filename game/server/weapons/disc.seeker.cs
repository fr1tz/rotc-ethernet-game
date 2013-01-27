//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - disc.seeker.cs
// Target-seeking disc projectile
//------------------------------------------------------------------------------

exec("./disc.seeker.gfx.red.cs");
exec("./disc.seeker.gfx.blue.cs");

//------------------------------------------------------------------------------

function launchSeekerDisc(%obj)
{
	%player = %obj;
	%slot = 1;

	%projectile = %player.getMountedImage(%slot).seekerDisc;

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

datablock NortDiscData(RedSeekerDisc)
{
	stat = "seeker";

	// script damage properties...
	impactDamage       = 60;
	impactImpulse      = 1000;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	
	energyDrain = 0; // how much energy does firing this projectile drain?

	sound = DiscProjectileSound;
	
	targetLockTimeMS = 0;

	maxTrackingAbility = 80;
	trackingAgility = 2;

	explodesNearEnemies = false;
	explodesNearEnemiesRadius = 5;

	muzzleVelocity		= 30;
    maxVelocity         = 75;
    acceleration        = 4;
	velInheritFactor	= 1.0;

	armingDelay			= 0*1000;
	lifetime				= 10*1000;
	fadeDelay			  = 3*1000;

	isBallistic = true;
	gravityMod			 = 1.0;
	bounceElasticity	 = 0.99;
	bounceFriction		 = 0.0;
    numBounces           = 3;

	projectileShapeName = "share/shapes/rotc/weapons/disc/projectile_red.dts";

	explosion             = RedSeekerDiscExplosion;
	hitEnemyExplosion     = RedSeekerDiscHit;
// nearEnemyExplosion	 = ThisDoesNotExist;
	hitTeammateExplosion  = RedSeekerDiscHit;
	hitDeflectorExplosion = RedSeekerDiscDeflectedEffect;
	bounceExplosion       = RedSeekerDiscHit;

	laserTrail[0]		= RedSeekerDiscInnerLaserTrail;
	laserTrail[1]		= RedSeekerDiscOuterLaserTrail;
	
	decals[0] = SlashDecalOne;

	hasLight	 = true;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
 
    startVertical = true;
};

function RedSeekerDisc::onAdd(%this,%obj)
{
	Parent::onAdd(%this,%obj);
    %obj.setTargetingMask($TargetingMask::Disc);
}

function RedSeekerDisc::onRemove(%this,%obj)
{
	Parent::onRemove(%this,%obj);
    if(%obj.state() == $NortDisc::Attacking)
        %obj.getTarget().removeAttackingDisc(%obj);

	%source = %obj.sourceObject;
	%source.incDiscs();
}

function RedSeekerDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
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
   
        // give another disc-lock to the attacker
        //%src =  %obj.getSourceObject();
        //if(%src)
        //   %src.setDiscTarget(%col);
	}
}

function RedSeekerDisc::onDeflected(%this, %obj)
{
    if(%obj.state() == $NortDisc::Attacking)
        %obj.getTarget().removeAttackingDisc(%obj);
}

function RedSeekerDisc::onHitTarget(%this, %obj)
{
    %obj.getTarget().removeAttackingDisc(%obj);
    %obj.stopAttacking();
}

function RedSeekerDisc::onMissedTarget(%this, %obj)
{
    if(%obj.state() == $NortDisc::Attacking)
        %obj.getTarget().removeAttackingDisc(%obj);

    %obj.stopAttacking();
}

function RedSeekerDisc::onLostTarget(%this, %obj)
{
    %obj.stopAttacking();
}

//-----------------------------------------------------------------------------

datablock NortDiscData(BlueSeekerDisc : RedSeekerDisc)
{
	projectileShapeName = "share/shapes/rotc/weapons/disc/projectile_blue.dts";

	explosion             = BlueSeekerDiscExplosion;
	hitEnemyExplosion     = BlueSeekerDiscHit;
	hitTeammateExplosion  = BlueSeekerDiscHit;
	hitDeflectorExplosion = BlueSeekerDiscDeflectedEffect;
	bounceExplosion       = BlueSeekerDiscHit;

	laserTrail[0]		= BlueSeekerDiscInnerLaserTrail;
	laserTrail[1]		= BlueSeekerDiscOuterLaserTrail;

	fxLight				 = BlueSeekerDiscFxLight;

	hasLight	 = true;
	lightRadius = 10.0;
	lightColor  = "0.0 0.0 1.0";
};

function BlueSeekerDisc::onAdd(%this,%obj)
{
	RedSeekerDisc::onAdd(%this,%obj);
}

function BlueSeekerDisc::onRemove(%this,%obj)
{
	RedSeekerDisc::onRemove(%this,%obj);
}

function BlueSeekerDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	RedSeekerDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function BlueSeekerDisc::onDeflected(%this, %obj)
{
    RedSeekerDisc::onDeflected(%this, %obj);
}

function BlueSeekerDisc::onHitTarget(%this, %obj)
{
	RedSeekerDisc::onHitTarget(%this, %obj);
}

function BlueSeekerDisc::onMissedTarget(%this, %obj)
{
	RedSeekerDisc::onMissedTarget(%this, %obj);
}

function BlueSeekerDisc::onLostTarget(%this, %obj)
{
	RedSeekerDisc::onLostTarget(%this, %obj);
}


