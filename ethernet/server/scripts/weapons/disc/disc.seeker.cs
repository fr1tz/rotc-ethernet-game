//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - disc.seeker.cs
// Target-seeking disc projectile
//------------------------------------------------------------------------------

exec("./disc.seeker.gfx.red.cs");
exec("./disc.seeker.gfx.blue.cs");

//-----------------------------------------------------------------------------
// projectile datablock...

datablock NortDiscData(RedSeekerDisc)
{
	// script damage properties...
	impactDamage		 = 60;
	impactImpulse		= 1000;
	splashDamage		 = 0;
	splashDamageRadius = 0;
	splashImpulse		= 0;
	
	energyDrain = 0; // how much energy does firing this projectile drain?

	sound = DiscProjectileSound;
	
	targetLockTimeMS = 0;

	maxTrackingAbility = 40;
	trackingAgility = 2;

	explodesNearEnemies = false;
	explodesNearEnemiesRadius = 3;

	muzzleVelocity		= 30;
    maxVelocity         = 75;
    acceleration        = 4;
	velInheritFactor	 = 0.0;

	armingDelay			= 0*1000;
	lifetime				= 10*1000;
	fadeDelay			  = 3*1000;

	isBallistic = true;
	gravityMod			 = 1.0;
	bounceElasticity	 = 0.99;
	bounceFriction		 = 0.0;
    numBounces           = 3;

	projectileShapeName = "~/data/weapons/disc/projectile_red.dts";

	explosion             = RedSeekerDiscExplosion;
	hitEnemyExplosion     = RedSeekerDiscHitEnemy;
// nearEnemyExplosion	 = ThisDoesNotExist;
	hitTeammateExplosion  = RedSeekerDiscHitEnemy;
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
    %obj.setTargetingMask($TargetingMask::Disc);
}

function RedSeekerDisc::onRemove(%this,%obj)
{
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
        %src =  %obj.getSourceObject();
        if(%src)
            %src.setDiscTarget(%col);
	}
}

function RedSeekerDisc::onDeflected(%this, %obj)
{
    if(%obj.state() == $NortDisc::Attacking)
    {
        %obj.getTarget().removeAttackingDisc(%obj);
        %obj.getTarget().startNoDiscGracePeriod();
    }
}

function RedSeekerDisc::onHitTarget(%this,%obj)
{
    %obj.getTarget().removeAttackingDisc(%obj);
}

//-----------------------------------------------------------------------------

datablock NortDiscData(BlueSeekerDisc : RedSeekerDisc)
{
	projectileShapeName = "~/data/weapons/disc/projectile_blue.dts";

	explosion             = BlueSeekerDiscExplosion;
	hitEnemyExplosion     = BlueSeekerDiscHitEnemy;
	hitTeammateExplosion  = BlueSeekerDiscHitEnemy;
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

function BlueSeekerDisc::onHitTarget(%this,%obj)
{
	RedSeekerDisc::onHitTarget(%this,%obj);
}


