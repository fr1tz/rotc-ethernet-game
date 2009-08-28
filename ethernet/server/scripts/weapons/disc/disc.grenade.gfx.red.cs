//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(RedDiscGrenade_ParticleEmitter_Particles)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   windCoefficient      = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 0;
   textureName          = "~/data/particles/smoke_particle";
   colors[0]     = "1.0 1.0 0.0 1.0";
   colors[1]     = "1.0 0.0 0.0 1.0";
   colors[2]     = "1.0 0.0 0.0 0.0";
   sizes[0]      = 0.80;
   sizes[1]      = 0.80;
   sizes[2]      = 0.50;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(RedDiscGrenade_ParticleEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 0;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = false;
   lifetimeMS       = 0;
   particles = "RedDiscGrenade_ParticleEmitter_Particles";
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(RedDiscGrenade_Lasertrail)
{
	hasLine = false;
	lineColor	= "1.00 1.00 1.00 0.02";

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = true;
	outerColor = "1.00 0.00 0.00 1.00";
	outerWidth = "0.20";

//	bitmap = "~/data/weapons/assaultrifle/lasertrail";
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

	fadeTime = 1000;
};

//-----------------------------------------------------------------------------
// bounce

datablock ExplosionData(RedDiscGrenadeBounceEffect)
{
    soundProfile   = DiscGrenadeBounceSound;
};

//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(RedDiscGrenadeExplosion_Cloud)
{
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.025;

   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;

   useInvAlpha =  false;
   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   textureName = "~/data/particles/corona.png";

   colors[0]     = "1.0 0.2 0.0 1.0";
   colors[1]     = "1.0 0.2 0.0 0.0";
   colors[2]     = "1.0 0.2 0.0 0.0";
   sizes[0]      = 6.0;
   sizes[1]      = 6.0;
   sizes[2]      = 2.0;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;

   allowLighting = true;
};

datablock ParticleEmitterData(RedDiscGrenadeExplosion_CloudEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 6.25;
   velocityVariance = 0.25;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   lifetimeMS       = 100;

   particles = "RedDiscGrenadeExplosion_Cloud";
};

datablock ParticleData(RedDiscGrenadeExplosion_Dust)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "~/data/particles/smoke_particle.png";
   colors[0]     = "0.9 0.9 0.9 0.5";
   colors[1]     = "0.9 0.9 0.9 0.5";
   colors[2]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 3.2;
   sizes[1]      = 4.6;
   sizes[2]      = 5.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
   allowLighting = true;
};

datablock ParticleEmitterData(RedDiscGrenadeExplosion_DustEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 15.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 90;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 250;
   particles = "RedDiscGrenadeExplosion_Dust";
};


datablock ParticleData(RedDiscGrenadeExplosion_Smoke)
{
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.5;   // rises slowly
   inheritedVelFactor   = 0.025;

   lifetimeMS           = 1250;
   lifetimeVarianceMS   = 0;

   useInvAlpha =  true;
   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   textureName = "~/data/particles/smoke_particle.png";

   colors[0]     = "0.9 0.9 0.9 0.4";
   colors[1]     = "0.9 0.9 0.9 0.2";
   colors[2]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 2.0;
   sizes[1]      = 6.0;
   sizes[2]      = 2.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

   allowLighting = true;
};

datablock ParticleEmitterData(RedDiscGrenadeExplosion_SmokeEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 6.25;
   velocityVariance = 0.25;

   thetaMin         = 0.0;
   thetaMax         = 180.0;

   lifetimeMS       = 250;

   particles = "RedDiscGrenadeExplosion_Smoke";
};

datablock ParticleData(RedDiscGrenadeExplosion_Shards)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 250;
   lifetimeVarianceMS   = 50;
   textureName          = "~/data/weapons/disc/shard.red";
   colors[0]     = "1.0 1.0 1.0 1.0";
   colors[1]     = "1.0 1.0 1.0 1.0";
   colors[2]     = "1.0 1.0 1.0 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
   allowLighting = false;
};

datablock ParticleEmitterData(RedDiscGrenadeExplosion_ShardsEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;
   ejectionVelocity = 50;
   velocityVariance = 5;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 89;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "RedDiscGrenadeExplosion_Shards";
};

datablock MultiNodeLaserBeamData(RedDiscGrenadeExplosion_Debris_LaserTrail)
{
   hasLine = true;
   lineColor   = "1.00 1.00 0.00 0.5";

   hasInner = false;
   innerColor = "1.00 1.00 0.00 0.3";
   innerWidth = "0.20";

   hasOuter = false;
   outerColor = "1.00 1.00 0.00 0.3";
   outerWidth = "0.40";

   //bitmap = "~/data/weapons/hegrenade/lasertrail";
   //bitmapWidth = 0.25;

   blendMode = 1;
   renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
   fadeTime = 1000;
};

datablock DebrisData(RedDiscGrenadeExplosion_Debris)
{
//   shapeFile = "~/data/weapons/hegrenade/grenade.dts";
//   emitters[0] = GrenadeLauncherParticleEmitter;

   laserTrail = RedDiscGrenadeExplosion_Debris_LaserTrail;

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

datablock ExplosionData(RedDiscGrenadeExplosion)
{
    soundProfile   = DiscGrenadeExplodeSound;

    lifetimeMS = 250;
    
	// Shape
	explosionShape = "~/data/weapons/disc/explosion.red.dts";
	faceViewer     = false;
	playSpeed      = 4.0;
	sizes[0]       = "0.1 0.1 0.1";
	sizes[1]       = "0.1 0.1 0.1";
	times[0]       = 0.0;
	times[1]       = 1.0;

    //debris = RedDiscGrenadeExplosion_Debris;
    //debrisThetaMin = 0;
    //debrisThetaMax = 180;
    //debrisNum = 5;
    //debrisVelocity = 50.0;
    //debrisVelocityVariance = 10.0;
   
    particleEmitter = RedDiscGrenadeExplosion_CloudEmitter;
    particleDensity = 100;
    particleRadius = 2.5;

    //emitter[0] = RedDiscGrenadeExplosion_DustEmitter;
    //emitter[1] = RedDiscGrenadeExplosion_SmokeEmitter;
    emitter[2] = RedDiscGrenadeExplosion_ShardsEmitter;
   
	// Dynamic light
	lightStartRadius = 15;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.5 0.0";
	lightEndColor = "0.0 0.0 0.0";

    // Camera shake
    shakeCamera = true;
    camShakeFreq = "10.0 6.0 9.0";
    camShakeAmp = "20.0 20.0 20.0";
    camShakeDuration = 0.5;
    camShakeRadius = 20.0;
};
