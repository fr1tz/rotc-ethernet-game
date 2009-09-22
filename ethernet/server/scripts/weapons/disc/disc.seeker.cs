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
	
	targetLockTimeMS = 50;

	maxTrackingAbility = 150;
	trackingAgility = 2;

	explodesNearEnemies = false;
	explodesNearEnemiesRadius = 3;

	muzzleVelocity		= 75;
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

	projectileShapeName = "~/data/weapons/disc/projectile.red.dts";

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
};

function RedSeekerDisc::onAdd(%this,%obj)
{

}

function RedSeekerDisc::onRemove(%this,%obj)
{
//	if(%obj.state() == $NortDisc::Attacking)
//		%obj.getTarget().attackedByDisc = false;

	%source = %obj.sourceObject;
	%source.incDiscs();
}

function RedSeekerDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
 
	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

    %src =  %obj.getSourceObject();
    if(%src)
        %src.setDiscTarget(%col);
	
	if(%col.getType() & $TypeMasks::PlayerObjectType)
	{
		// collision with player:
		// transfer max. 20 points of energy from the victim to the attacker...
		%source = %obj.sourceObject;
		%victimEnergy = %col.getEnergyLevel();
		%energySuck = %victimEnergy >= 20 ? 20 : %victimEnergy;
		%col.setEnergyLevel(%victimEnergy - %energySuck);
		if(%source.getDamageState() $= "Enabled")
			%source.setEnergyLevel(%source.getEnergyLevel() + %energySuck);
	}
}

function RedSeekerDisc::onHitTarget(%this,%obj)
{
	%obj.getTarget().attackedByDisc = false;
}

//-----------------------------------------------------------------------------

datablock NortDiscData(BlueSeekerDisc : RedSeekerDisc)
{
	projectileShapeName = "~/data/weapons/disc/projectile.blue.dts";

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

function BlueSeekerDisc::onHitTarget(%this,%obj)
{
	RedSeekerDisc::onHitTarget(%this,%obj);
}


