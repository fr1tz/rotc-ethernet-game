//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(BlueSTG90ProjectileLaserTail)
{
	hasLine = true;
	lineStartColor	= "0.00 0.00 1.00 0.0";
	lineBetweenColor = "0.00 0.00 1.00 1.0";
	lineEndColor	  = "0.00 0.00 1.00 0.0";
 	lineWidth		  = 2.0;

	hasInner = false;
	innerStartColor	= "1.00 1.00 0.00 0.5";
	innerBetweenColor = "1.00 1.00 0.00 0.5";
	innerEndColor	  = "1.00 1.00 0.00 0.5";
	innerStartWidth	= "0.0";
	innerBetweenWidth = "0.05";
	innerEndWidth	  = "0.1";

	hasOuter = false;
	outerStartColor	= "1.00 1.00 0.00 0.0";
	outerBetweenColor = "1.00 1.00 0.00 0.2";
	outerEndColor	  = "1.00 1.00 0.00 0.2";
	outerStartWidth	= "0.0";
	outerBetweenWidth = "0.3";
	outerEndWidth	  = "0.0";

	bitmap = "share/textures/inf/lasertail1.blue";
	bitmapWidth = 0.5;
//	crossBitmap = "share/shapes/rotc/weapons/BlueSTG90/lasertail.cross";
//	crossBitmapWidth = 0.25;

	betweenFactor = 0.7;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// impact...

datablock ParticleData(BlueSTG90ProjectileImpact_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.4;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 200;

	useInvAlpha =  true;

	textureName = "share/textures/rotc/smoke_particle";

	colors[0]	  = "1.0 1.0 1.0 0.5";
	colors[1]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(BlueSTG90ProjectileImpact_SmokeEmitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.1;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "BlueSTG90ProjectileImpact_Smoke";
};

datablock DebrisData(BlueSTG90ProjectileImpact_Debris)
{
	// shape...
	shapeFile = "share/shapes/rotc/misc/debris1.white.dts";

	// bounce...
	staticOnMaxBounce = true;
	numBounces = 5;

	// physics...
	gravModifier = 2.0;
	elasticity = 0.6;
	friction = 0.1;

	// spin...
	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	// lifetime...
	lifetime = 4.0;
	lifetimeVariance = 1.0;
};

datablock ExplosionData(BlueSTG90ProjectileImpact)
{
	soundProfile = STG90ProjectileImpactSound;

	lifetimeMS = 250;

	emitter[0] = DefaultSmallWhiteDebrisEmitter;
	emitter[1] = BlueSTG90ProjectileImpact_SmokeEmitter;

	//debris = BlueSTG90ProjectileImpact_Debris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 60;
	//debrisNum = 1;
	//debrisNumVariance = 1;
	//debrisVelocity = 10.0;
	//debrisVelocityVariance = 5.0;

	// Dynamic light
	lightStartRadius = 3;
	lightEndRadius = 0;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
   lightCastShadows = false;

	shakeCamera = false;
};

//-----------------------------------------------------------------------------
// hit enemy...

datablock ExplosionData(BlueSTG90ProjectileHit)
{
	soundProfile = STG90ProjectileHitSound;
};


