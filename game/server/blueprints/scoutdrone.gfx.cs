//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// debris...

datablock DebrisData(ScoutDroneDebris)
{
   //explosion = Team1HeGrenadeExplosion;
	//explodeOnMaxBounce = true;

	elasticity = 0.15;
	friction = 0.5;

	lifetime = 60.0;
	lifetimeVariance = 0.0;

	minSpinSpeed = 40;
	maxSpinSpeed = 200;

	numBounces = 5;
	bounceVariance = 0;

	staticOnMaxBounce = true;
	gravModifier = 1.0;

	useRadiusMass = true;
	baseRadius = 1;

	velocity = 30.0;
	velocityVariance = 12.0;

   //laserTrail = Team1MediumTurretDebris_LaserTrail;
};

//-----------------------------------------------------------------------------
// explosion...

datablock ExplosionData(ScoutDroneExplosion)
{
   soundProfile = ScoutDroneExplosionSound;
   
   explosionShape = "share/shapes/rotc/effects/explosion_white.dts";
   faceViewer     = true;
   explosionScale = "1.0 1.0 1.0";
};

//-----------------------------------------------------------------------------
// defunct effect...

datablock ParticleData(ScoutDroneDefunctEffect_Dust)
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
   textureName          = "share/textures/rotc/smoke_particle.png";
   colors[0]     = "0.3 0.3 0.3 0.5";
   colors[1]     = "0.3 0.3 0.3 0.5";
   colors[2]     = "0.3 0.3 0.3 0.0";
   sizes[0]      = 6.2;
   sizes[1]      = 7.6;
   sizes[2]      = 8.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ScoutDroneDefunctEffect_DustEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 15.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 0;
   particles = "ScoutDroneDefunctEffect_Dust";
};


datablock ParticleData(ScoutDroneDefunctEffect_ExplosionSmoke)
{
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.5;   // rises slowly
   inheritedVelFactor   = 0.025;

   lifetimeMS           = 1250;
   lifetimeVarianceMS   = 0;

   useInvAlpha =  true;
   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   textureName = "share/textures/rotc/smoke_particle.png";

   colors[0]     = "0.7 0.7 0.7 1.0";
   colors[1]     = "0.2 0.2 0.2 1.0";
   colors[2]     = "0.1 0.1 0.1 0.0";
   sizes[0]      = 2.0;
   sizes[1]      = 6.0;
   sizes[2]      = 2.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ScoutDroneDefunctEffect_ExplosionSmokeEmitter)
{
   ejectionOffset   = 0;

   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 6.25;
   velocityVariance = 0.25;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   lifetimeMS       = 0;

   particles = "ScoutDroneDefunctEffect_ExplosionSmoke";
};



datablock ParticleData(ScoutDroneDefunctEffect_Sparks)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 350;
   textureName          = "share/textures/rotc/particle1.png";
   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.56 0.36 0.26 1.0";
   colors[2]     = "1.0 0.36 0.26 0.0";
   sizes[0]      = 8.5;
   sizes[1]      = 8.5;
   sizes[2]      = 10.75;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ScoutDroneDefunctEffect_SparksEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 12;
   velocityVariance = 6.75;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 0;
   particles = "ScoutDroneDefunctEffect_Sparks";
};

datablock ParticleData(TScoutDroneDefunctEffect_MemoryLeak0Particles)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 350;
   textureName          = "share/textures/rotc/bin0";
   colors[0]     = "1.0 0.9 0.0 0.5";
   colors[1]     = "1.0 0.0 0.0 0.5";
   colors[2]     = "1.0 0.0 0.0 0.0";
   sizes[0]      = 1.0;
   sizes[1]      = 1.0;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ScoutDroneDefunctEffect_MemoryLeak0Emitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 5;
   ejectionVelocity = 6;
   velocityVariance = 2;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 10;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = false;
   lifetimeMS       = 0; // forever
   particles = ScoutDroneDefunctEffect_MemoryLeak0Particles;
};

//------------------------------------------------------------------------------

datablock ParticleData(ScoutDroneDefunctEffect_MemoryLeak1Particles)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 350;
   textureName          = "share/textures/rotc/bin1";
   colors[0]     = "1.0 0.9 0.0 0.5";
   colors[1]     = "1.0 0.0 0.0 0.5";
   colors[2]     = "1.0 0.0 0.0 0.0";
   sizes[0]      = 1.0;
   sizes[1]      = 1.0;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ScoutDroneDefunctEffect_MemoryLeak1Emitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 5;
   ejectionVelocity = 6;
   velocityVariance = 2;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 10;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = false;
   lifetimeMS       = 0; // forever
   particles = ScoutDroneDefunctEffect_MemoryLeak1Particles;
};

datablock FXShapeData(ScoutDroneDefunctEffect)
{
// soundProfile   = Team1ProjectileRelaySound;

// shape = "~/data/explosions/team1hegrenade_exp/explosion.dts";
   faceViewer     = false;
   scale = "1.0 1.0 1.0";

//   debris = GrenadeDebris;
//   debrisThetaMin = 10;
//   debrisThetaMax = 50;
//   debrisNum = 8;
//   debrisVelocity = 26.0;
//   debrisVelocityVariance = 7.0;

   emitter[0] = ScoutDroneDefunctEffect_MemoryLeak0Emitter;
   emitter[1] = ScoutDroneDefunctEffect_MemoryLeak1Emitter;
   emitter[2] = ScoutDroneDefunctEffect_SparksEmitter;
//   emitter[1] = ScoutDroneDefunctEffect_DustEmitter;
//   emitter[2] = ScoutDroneDefunctEffect_ExplosionSmokeEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 6.0 9.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 20.0;
};

//-----------------------------------------------------------------------------
// laserTrail...

datablock MultiNodeLaserBeamData(RedScoutDrone_LaserTrail)
{
   hasLine = false;
   lineColor   = "1.00 1.00 0.00 0.7";

   hasInner = true;
   innerColor = "1.0 0.0 0.0 0.5";
   innerWidth = "0.75";

   hasOuter = false;
   outerColor = "1.00 0.00 1.00 0.1";
   outerWidth = "0.10";

   //bitmap = "~/data/vehicles/team1scoutScoutDrone/lasertrail";
   //bitmapWidth = 1;

   blendMode = 1;
   fadeTime = 1000;
};

datablock MultiNodeLaserBeamData(BlueScoutDrone_LaserTrail)
{
   hasLine = false;
   lineColor   = "1.00 1.00 0.00 0.7";

   hasInner = true;
   innerColor = "0.0 0.0 1.0 0.5";
   innerWidth = "0.75";

   hasOuter = false;
   outerColor = "1.00 0.00 1.00 0.1";
   outerWidth = "0.10";

   //bitmap = "~/data/vehicles/team1scoutScoutDrone/lasertrail";
   //bitmapWidth = 1;

   blendMode = 1;
   fadeTime = 1000;
};

//-----------------------------------------------------------------------------
// contrail...

datablock ParticleData(RedScoutDrone_ContrailParticle)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 200;
   lifetimeVarianceMS   = 50;
   textureName          = "share/textures/rotc/particle1.png";
   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.56 0.36 0.26 1.0";
   colors[2]     = "1.0 0.36 0.26 0.0";
   sizes[0]      = 2.5;
   sizes[1]      = 2.5;
   sizes[2]      = 3.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(RedScoutDrone_ContrailEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 12;
   velocityVariance = 6.75;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 40;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 0;
   particles = "RedScoutDrone_ContrailParticle";
};

//-----------------------------------------------------------------------------
// tank wreck fx...

datablock ParticleData(ScoutDroneWreckFX_Sparks)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 350;
   textureName          = "share/textures/rotc/particle1.png";
   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.56 0.36 0.26 1.0";
   colors[2]     = "1.0 0.36 0.26 0.0";
   sizes[0]      = 8.5;
   sizes[1]      = 8.5;
   sizes[2]      = 10.75;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ScoutDroneWreckFX_SparksEmitter)
{
   ejectionPeriodMS = 20;
   periodVarianceMS = 0;
   ejectionVelocity = 12;
   velocityVariance = 6.75;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 0;
   particles = "ScoutDroneWreckFX_Sparks";
};

datablock ParticleData(ScoutDroneWreckFX_MemoryLeak0Particles)
{
   dragCoefficient      = 1;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 5000;
   lifetimeVarianceMS   = 350;
   textureName          = "share/textures/rotc/bin0";
   colors[0]     = "1.0 0.9 0.0 0.5";
   colors[1]     = "1.0 0.0 0.0 0.5";
   colors[2]     = "1.0 0.0 0.0 0.0";
   sizes[0]      = 1.0;
   sizes[1]      = 1.0;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ScoutDroneWreckFX_MemoryLeak0Emitter)
{
   ejectionPeriodMS = 50;
   periodVarianceMS = 5;
   ejectionVelocity = 15;
   velocityVariance = 2;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 10;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = false;
   lifetimeMS       = 0; // forever
   particles = ScoutDroneWreckFX_MemoryLeak0Particles;
};

datablock ParticleData(ScoutDroneWreckFX_MemoryLeak1Particles)
{
   dragCoefficient      = 1;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 5000;
   lifetimeVarianceMS   = 350;
   textureName          = "share/textures/rotc/bin1";
   colors[0]     = "1.0 0.9 0.0 0.5";
   colors[1]     = "1.0 0.0 0.0 0.5";
   colors[2]     = "1.0 0.0 0.0 0.0";
   sizes[0]      = 1.0;
   sizes[1]      = 1.0;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ScoutDroneWreckFX_MemoryLeak1Emitter)
{
   ejectionPeriodMS = 50;
   periodVarianceMS = 5;
   ejectionVelocity = 15;
   velocityVariance = 2;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 10;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = false;
   lifetimeMS       = 0; // forever
   particles = ScoutDroneWreckFX_MemoryLeak1Particles;
};

datablock FXShapeData(ScoutDroneWreckFX)
{
   emitter[0] = ScoutDroneWreckFX_MemoryLeak0Emitter;
   emitter[1] = ScoutDroneWreckFX_MemoryLeak1Emitter;
   emitter[2] = ScoutDroneWreckFX_SparksEmitter;
};
