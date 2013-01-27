//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(FF3ProjectileParticleEmitter_Particles)
{
	dragCoeffiecient	  = 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;

	lifetimeMS			  = 200;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/smoke_particle.png";

	colors[0]	  = "0.9 0.9 0.9 0.6";
	colors[1]	  = "0.9 0.9 0.9 0.3";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 0.8;
	sizes[1]		= 0.4;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(FF3ProjectileParticleEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 0.0;
	velocityVariance = 0.0;
	ejectionOffset	 = 0.0;
	thetaMin		 = 0;
	thetaMax		 = 0;
	phiReferenceVel  = 0;
	phiVariance		 = 0;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "FF3ProjectileParticleEmitter_Particles";
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(FF3ProjectileLaserTrail)
{
	hasLine = true;
	lineColor	= "1.00 1.00 1.00 0.03";

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = true;
	outerColor = "1.00 1.00 1.00 0.03";
	outerWidth = "0.1";

//	bitmap = "share/shapes/rotc/weapons/assaultrifle/lasertrail";
//	bitmapWidth = 0.25;

	blendMode = 1;
 
	windCoefficient = 0.0;

	// node x movement...
	nodeMoveMode[0]     = $NodeMoveMode::None;
	nodeMoveSpeed[0]    = -0.002;
	nodeMoveSpeedAdd[0] =  0.004;
	// node y movement...
	nodeMoveMode[1]     = $NodeMoveMode::None;
	nodeMoveSpeed[1]    = -0.002;
	nodeMoveSpeedAdd[1] =  0.004;
	// node z movement...
	nodeMoveMode[2]     = $NodeMoveMode::ConstantSpeed;
	nodeMoveSpeed[2]    = 0.5;
	nodeMoveSpeedAdd[2] = 0.5;
    
	nodeDistance = 2;
 
	fadeTime = 1000;
};

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(FF3ProjectileLaserTail)
{
	hasLine = true;
	lineStartColor	= "1.00 1.00 1.00 0.0";
	lineBetweenColor = "1.00 1.00 1.00 0.5";
	lineEndColor	  = "1.00 1.00 1.00 1.0";
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

	bitmap = "share/shapes/rotc/weapons/assaultrifle/lasertail";
	bitmapWidth = 0.1;
//	crossBitmap = "share/shapes/rotc/weapons/assaultrifle/lasertail.cross";
//	crossBitmapWidth = 0.25;

	betweenFactor = 0.5;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// bounce

datablock ExplosionData(FF3ProjectileBounceExplosion)
{
	soundProfile	= FF3ProjectileBounceSound;
	
	lifetimeMS = 200;
	
	// Dynamic light
	lightStartRadius = 6;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.8 0.2 1.0";
	lightEndColor = "1.0 0.8 0.2 0.3";
};


//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(FF3ProjectileExplosion_Cloud)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 600;
	lifetimeVarianceMS	= 0;

	useInvAlpha = false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/corona.png";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.0";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.5;
	times[0]		= 0.0;
	times[1]		= 0.2;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(FF3ProjectileExplosion_CloudEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;

	ejectionVelocity = 0.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "FF3ProjectileExplosion_Cloud";
};

datablock ParticleData(FF3ProjectileExplosion_Dust)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= -0.01;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = true;
	spinRandomMin		  = -90.0;
	spinRandomMax		  = 500.0;
	textureName			 = "share/textures/rotc/smoke_particle.png";
	colors[0]	  = "0.9 0.9 0.9 0.5";
	colors[1]	  = "0.9 0.9 0.9 0.5";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 0.9;
	sizes[1]		= 1.5;
	sizes[2]		= 1.6;
	times[0]		= 0.0;
	times[1]		= 0.7;
	times[2]		= 1.0;
	allowLighting = true;
};

datablock ParticleEmitterData(FF3ProjectileExplosion_DustEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 2.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	lifetimeMS		 = 50;
	particles = "FF3ProjectileExplosion_Dust";
};


datablock ParticleData(FF3ProjectileExplosion_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.5;	// rises slowly
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 1250;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  true;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/rotc/smoke_particle.png";

	colors[0]	  = "0.9 0.9 0.9 0.4";
	colors[1]	  = "0.9 0.9 0.9 0.2";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 0.6;
	sizes[1]		= 2.0;
	sizes[2]		= 0.6;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(FF3ProjectileExplosion_SmokeEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

	lifetimeMS		 = 250;

	particles = "FF3ProjectileExplosion_Smoke";
};

datablock ParticleData(FF3ProjectileExplosion_Sparks)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 350;
	textureName			 = "share/textures/rotc/particle1.png";
	colors[0]	  = "0.56 0.36 0.26 1.0";
	colors[1]	  = "0.56 0.36 0.26 1.0";
	colors[2]	  = "1.0 0.36 0.26 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.75;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	allowLighting = false;
};

datablock ParticleEmitterData(FF3ProjectileExplosion_SparksEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 4;
	velocityVariance = 1;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 60;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 100;
	particles = "FF3ProjectileExplosion_Sparks";
};

datablock MultiNodeLaserBeamData(FF3ProjectileExplosion_Debris_LaserTrail)
{
	hasLine = true;
	lineColor	= "1.00 1.00 0.00 0.5";

	hasInner = false;
	innerColor = "1.00 1.00 0.00 0.3";
	innerWidth = "0.20";

	hasOuter = false;
	outerColor = "1.00 1.00 0.00 0.3";
	outerWidth = "0.40";

//	bitmap = "share/shapes/rotc/weapons/hegrenade/lasertrail";
//	bitmapWidth = 0.1;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 500;
};

datablock DebrisData(FF3ProjectileExplosion_Debris)
{
//	shapeFile = "share/shapes/rotc/weapons/hegrenade/grenade.dts";
//	emitters[0] = GrenadeLauncherParticleEmitter;

	laserTrail = FF3ProjectileExplosion_Debris_LaserTrail;

	// bounce...
	numBounces = 3;
	explodeOnMaxBounce = true;

	// physics...
	gravModifier = 5.0;
	elasticity = 0.6;
	friction = 0.1;

	lifetime = 5.0;
	lifetimeVariance = 0.02;
};

datablock ExplosionData(FF3ProjectileExplosion)
{
	soundProfile = FF3ProjectileExplosionSound;

	lifetimeMS = 200;
 
 	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion2_white.dts";
	faceViewer	  = false;
	playSpeed = 8.0;
	sizes[0] = "0.2 0.2 0.2";
	sizes[1] = "0.2 0.2 0.2";
	times[0] = 0.0;
	times[1] = 1.0;

	//debris = 0; //FF3ProjectileExplosion_Debris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 180;
	//debrisNum = 3;
	//debrisVelocity = 50.0;
	//debrisVelocityVariance = 10.0;
	
	particleEmitter = FF3ProjectileExplosion_CloudEmitter;
	particleDensity = 25;
	particleRadius = 0.5;

	emitter[0] = FF3ProjectileExplosion_DustEmitter;
	emitter[1] = 0; // FF3ProjectileExplosion_SmokeEmitter;
	emitter[2] = 0; // FF3ProjectileExplosion_SparksEmitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 15;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.8 0.2";
	lightEndColor = "0.0 0.0 0.0";
};

//-----------------------------------------------------------------------------
// impact...

datablock ParticleData(FF3ProjectileImpact_Smoke)
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

datablock ParticleEmitterData(FF3ProjectileImpact_SmokeEmitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.1;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "FF3ProjectileImpact_Smoke";
};

datablock DebrisData(FF3ProjectileImpact_Debris)
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

datablock ExplosionData(FF3ProjectileImpact)
{
	soundProfile	= FF3ProjectileImpactSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 250;

	emitter[0] = DefaultMediumWhiteDebrisEmitter;
	emitter[1] = FF3ProjectileImpact_SmokeEmitter;

	//debris = FF3ProjectileImpact_Debris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 60;
	//debrisNum = 2;
	//debrisNumVariance = 1;
	//debrisVelocity = 10.0;
	//debrisVelocityVariance = 5.0;

	// Dynamic light
	lightStartRadius = 0.25;
	lightEndRadius = 4;
	lightStartColor = "1.0 0.8 0.2";
	lightEndColor = "0.0 0.0 0.0";

	shakeCamera = false;
};

//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(FF3ProjectileMissedEnemyEffect)
{
	soundProfile = FF3ProjectileMissedEnemySound;

	// shape...
	//explosionShape = "share/shapes/rotc/effects/explosion5.green.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.01 0.01 0.01";
	sizes[1] = "0.07 0.07 0.07";
	times[0] = 0.0;
	times[1] = 1.0;
	
	// dynamic light...
	lightStartRadius = 0;
	lightEndRadius = 0;
	lightStartColor = "0.0 1.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
};



