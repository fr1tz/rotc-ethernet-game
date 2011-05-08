//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------


//------------------------------------------------------------------------------
// tank projectile particles

datablock ParticleData(ExampleTurretProjectileParticleEmitter_Particle)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 350;
   colors[0]     = "1.0 0.6 0.5 1.0";
   colors[1]     = "1.0 0.6 0.5 0.0";
   sizes[0]      = 0.8;
   sizes[1]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 1.0;
   textureName   = "share/textures/rotc/smoke_particle";
};

datablock ParticleEmitterData(ExampleTurretProjectileParticleEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 0;
   velocityVariance = 0;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 10;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = false;
   lifetimeMS       = 0; // forever
   particles = ExampleTurretProjectileParticleEmitter_Particle;
};

//------------------------------------------------------------------------------
// tank fire explosion

datablock ParticleData(TankFireExplosionParticle)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.2;
   windCoefficient      = 1.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 900;
   lifetimeVarianceMS   = 225;
   textureName          = "share/textures/rotc/smoke_particle.png";
   useInvAlpha          = true;
   colors[0]            = "0.50 0.50 0.50 0.5";
   colors[1]            = "0.50 0.50 0.50 0.0";
   sizes[0]             = 3;
   sizes[1]             = 5;
};

datablock ParticleEmitterData(TankFireExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 10;
   velocityVariance = 10;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "TankFireExplosionParticle";
};

datablock ExplosionData(TankFireExplosion)
{
   soundProfile    = ExampleTurretFireSound;
   
   particleEmitter = TankFireExplosionEmitter;
   particleDensity = 100;
   particleRadius  = 0.50;
   faceViewer      = true;

   shakeCamera = true;
   camShakeFreq = "5.0 7.0 5.0";
   camShakeAmp = "80.0 80.0 80.0";
   camShakeDuration = 1.0;
   camShakeRadius = 30.0;
};

//------------------------------------------------------------------------------
// tank projectile explosion

datablock ParticleData(ExampleTurretProjectileExplosion_Cloud)
{
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.025;

   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;

   useInvAlpha =  false;
   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   textureName = "share/textures/rotc/corona.png";

   colors[0]     = "1.0 1.0 1.0 1.0";
   colors[1]     = "1.0 1.0 1.0 0.0";
   colors[2]     = "1.0 1.0 1.0 0.0";
   sizes[0]      = 6.0;
   sizes[1]      = 6.0;
   sizes[2]      = 2.0;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ExampleTurretProjectileExplosion_CloudEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 6.25;
   velocityVariance = 0.25;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   lifetimeMS       = 100;

   particles = "ExampleTurretProjectileExplosion_Cloud";
};

datablock ParticleData(ExampleTurretProjectileExplosion_Dust)
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
   sizes[0]      = 3.2;
   sizes[1]      = 4.6;
   sizes[2]      = 5.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ExampleTurretProjectileExplosion_DustEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 15.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 250;
   particles = "ExampleTurretProjectileExplosion_Dust";
};


datablock ParticleData(ExampleTurretProjectileExplosion_Smoke)
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

datablock ParticleEmitterData(ExampleTurretProjectileExplosion_SmokeEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 6.25;
   velocityVariance = 0.25;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   lifetimeMS       = 250;

   particles = "ExampleTurretProjectileExplosion_Smoke";
};



datablock ParticleData(ExampleTurretProjectileExplosion_Sparks)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 350;
   textureName          = "share/textures/rotc/particle1.png";
   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.56 0.36 0.26 1.0";
   colors[2]     = "1.0 0.36 0.26 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.75;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ExampleTurretProjectileExplosion_SparksEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;
   ejectionVelocity = 12;
   velocityVariance = 6.75;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "ExampleTurretProjectileExplosion_Sparks";
};

datablock ExplosionData(ExampleTurretProjectileExplosion)
{
   soundProfile   = ExampleTurretProjectileExplosionSound;

   faceViewer     = true;
   explosionScale = "0.8 0.8 0.8";

   //debris = GrenadeDebris;
   //debrisThetaMin = 10;
   //debrisThetaMax = 50;
   //debrisNum = 8;
   //debrisVelocity = 26.0;
   //debrisVelocityVariance = 7.0;

   particleEmitter = ExampleTurretProjectileExplosion_CloudEmitter;
   particleDensity = 100;
   particleRadius = 1.25;

   emitter[0] = ExampleTurretProjectileExplosion_DustEmitter;
   emitter[1] = ExampleTurretProjectileExplosion_SmokeEmitter;
   emitter[2] = ExampleTurretProjectileExplosion_SparksEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 6.0 9.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 20.0;
};
