//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// fire explosion

datablock ExplosionData(BlueSniperRifle3ProjectileFireExplosion)
{
	soundProfile = SniperRifle3FireSound;

//	particleEmitter = BlueSniperRifle3ProjectileFireExplosion_CloudEmitter;
//	particleDensity = 5;
//	particleRadius = 0.1;
};

//-----------------------------------------------------------------------------
// projectile particle emitter

//-----------------------------------------------------------------------------
// laser tail...

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(BlueSniperRifle3ProjectileLaserTrailOne)
{
	hasLine   = false;
	lineColor = "1.00 1.00 1.00 1.0";
   lineWidth = 2.0;

	hasInner = true;
	innerColor = "0.00 0.00 1.00 1.0";
	innerWidth = "0.6";

	hasOuter = true;
	outerColor = "0.00 0.00 1.00 1.0";
	outerWidth = "0.6";

	bitmap = "share/textures/rotc/laser1.blue";
	bitmapWidth = 2.4;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 200;

    windCoefficient = 0.0;

	// node x movement...
	nodeMoveMode[0]     = $NodeMoveMode::None;
	nodeMoveSpeed[0]    = 16.0;
	nodeMoveSpeedAdd[0] = -32.0;
	// node y movement...
	nodeMoveMode[1]     = $NodeMoveMode::None;
	nodeMoveSpeed[1]    = 16.0;
	nodeMoveSpeedAdd[1] = -32.0;
	// node z movement...
	nodeMoveMode[2]     = $NodeMoveMode::None;
	nodeMoveSpeed[2]    = 16.0;
	nodeMoveSpeedAdd[2] = -32.0;

	nodeDistance = 4;
};

datablock MultiNodeLaserBeamData(BlueSniperRifle3ProjectileLaserTrailThree)
{
	hasLine   = false;
	lineColor = "1.00 0.50 0.50 0.5";
	lineWidth = 2.0;

	hasInner = false;
	innerColor = "1.00 0.50 0.50 0.5";
	innerWidth = "0.08";

	hasOuter = false;
	outerColor = "1.00 0.00 0.00 0.75";
	outerWidth = "0.20";

	bitmap = "share/textures/rotc/smoke4.blue";
	bitmapWidth = 1.5;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 3000;

    windCoefficient = 0.0;

    // node x movement...
    nodeMoveMode[0]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[0]    = -2.0;
    nodeMoveSpeedAdd[0] =  4.0;
    // node y movement...
    nodeMoveMode[1]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[1]    = -2.0;
    nodeMoveSpeedAdd[1] =  4.0;
    // node z movement...
    nodeMoveMode[2]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[2]    = -2.0;
    nodeMoveSpeedAdd[2] =  4.0;

    nodeDistance = 4;
};

//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(BlueSniperRifle3ProjectileMissedEnemyEffect)
{
	soundProfile = SniperRifle3ProjectileMissedEnemySound;

	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion2_white.dts";
	faceViewer	  = true;
	playSpeed = 8.0;
	sizes[0] = "0.07 0.07 0.07";
	sizes[1] = "0.01 0.01 0.01";
	times[0] = 0.0;
	times[1] = 1.0;

	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 2;
	lightStartColor = "0.5 0.5 0.5";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
};

//-----------------------------------------------------------------------------
// explosion...

datablock ParticleData(BlueSniperRifle3ProjectileExplosion_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.5;	// rises slowly
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 3000;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/smoke_particle.png";

	colors[0]	  = "0.0 0.0 0.9 1.0";
	colors[1]	  = "0.9 0.9 0.9 0.5";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 2.0;
	sizes[2]		= 8.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(BlueSniperRifle3ProjectileExplosion_SmokeEmitter)
{
	ejectionPeriodMS = 200;
	periodVarianceMS = 0;

	ejectionVelocity = 0;
	velocityVariance = 0;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

	lifetimeMS		 = 8000;

	particles = "BlueSniperRifle3ProjectileExplosion_Smoke";
};

datablock ExplosionData(BlueSniperRifle3ProjectileHit)
{
	soundProfile = SniperRifle3ProjectileImpactSound;

	lifetimeMS = 300;

	//particleEmitter = BlueSniperRifle3ProjectileExplosion_CloudEmitter;
	//particleDensity = 15;
	//particleRadius = 1;

	//emitter[0] = BlueSniperRifle3ProjectileExplosion_SparksEmitter;
	//emitter[2] = BlueSniperRifle3ProjectileExplosion_DustEmitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 4;
	lightEndRadius = 0;
	lightStartColor = "0.0 0.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
};

datablock ExplosionData(BlueSniperRifle3ProjectileExplosion : BlueSniperRifle3ProjectileHit)
{
 	// shape...
	explosionShape = "share/shapes/rotc/weapons/blaster/projectile.impact.blue.dts";
	faceViewer = false;
	playSpeed = 0.1;
	sizes[0] = "1 1 1";
	sizes[1] = "1 1 1";
	times[0] = 0.0;
	times[1] = 1.0;

	lifetimeMS = 0;

	//debris = SniperRifle3ProjectileExplosion_LargeDebris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 80;
	//debrisNum = 6;
   //debrisNumVariance = 0;
	//debrisVelocity = 120.0;
	//debrisVelocityVariance = 10.0;

	//particleEmitter = BlueSniperRifle3ProjectileExplosion_DustEmitter;
	//particleDensity = 15;
	//particleRadius = 2;

	emitter[0] = SniperRifle3ProjectileExplosion_DebrisEmitter;
	emitter[1] = BlueSniperRifle3ProjectileExplosion_SmokeEmitter;
	//emitter[2] = BlueSniperRifle3ProjectileExplosion_SparksEmitter;

	// Dynamic light
	lightStartRadius = 0;
	lightEndRadius = 0;
	lightStartColor = "0.0 0.0 1.0 1.0";
	lightEndColor = "0.0 0.0 1.0 0.0";
};


