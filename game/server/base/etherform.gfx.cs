//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// light images...

datablock ShapeBaseImageData(RedEtherformLightImage)
{
	// basic item properties
	shapeFile = "~/data/misc/nothing.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 4;
	offset = "0 0 0";
	
	// light properties...
	lightType = "ConstantLight";
	lightColor = "0.8 0.0 0.0";
	lightTime = 1000;
	lightRadius = 8;
	lightCastsShadows = true;
	lightAffectsShapes = true;

	stateName[0] = "DoNothing";
};

datablock ShapeBaseImageData(BlueEtherformLightImage : RedEtherformLightImage)
{
	lightColor = "0.0 0.0 0.8";
};

//------------------------------------------------------------------------------
// damage buffer particle emitter...

datablock ParticleData(RedEtherformDamageBufferEmitter_Particle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 1.0 0.0 1.0";
	colors[1]	  = "1.0 0.0 0.0 0.2";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "~/data/particles/corona";
	allowLighting = false;
};

datablock ParticleEmitterData(RedEtherformDamageBufferEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 5;
	velocityVariance = 0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = RedEtherformDamageBufferEmitter_Particle;
};

//-------------------------------

datablock ParticleData(BlueEtherformDamageBufferEmitter_Particle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "0.0 1.0 1.0 1.0";
	colors[1]	  = "0.0 0.0 1.0 0.2";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "~/data/particles/corona";
	allowLighting = false;
};

datablock ParticleEmitterData(BlueEtherformDamageBufferEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 5;
	velocityVariance = 0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = BlueEtherformDamageBufferEmitter_Particle;
};

//------------------------------------------------------------------------------
// damage repair particle emitter...

datablock ParticleData(RedEtherformRepairEmitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 220;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 0.5 0.0 0.0";
	colors[1]	  = "1.0 0.5 0.0 1.0";
	colors[2]	  = "1.0 0.5 0.0 1.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "~/data/particles/cross1";
	allowLighting = false;
};

datablock ParticleEmitterData(RedEtherformRepairEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = -20.0;
	velocityVariance = 0.0;
	ejectionOffset	= 4.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = RedEtherformRepairEmitter_Particle;
};

//-------------------------------

datablock ParticleData(BlueEtherformRepairEmitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 220;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "0.0 1.0 1.0 0.0";
	colors[1]	  = "0.0 1.0 1.0 1.0";
	colors[2]	  = "0.0 1.0 1.0 1.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "~/data/particles/cross1";
	allowLighting = false;
};

datablock ParticleEmitterData(BlueEtherformRepairEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = -20.0;
	velocityVariance = 0.0;
	ejectionOffset	= 4.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = BlueEtherformRepairEmitter_Particle;
};

//------------------------------------------------------------------------------
// damage buffer repair particle emitter...

datablock ParticleData(RedEtherformBufferRepairEmitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 220;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 1.0 1.0 0.0";
	colors[1]	  = "1.0 1.0 1.0 1.0";
	colors[2]	  = "1.0 1.0 1.0 1.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "~/data/particles/cross1";
	allowLighting = false;
};

datablock ParticleEmitterData(RedEtherformBufferRepairEmitter)
{
	ejectionPeriodMS = 500;
	periodVarianceMS = 0;
	ejectionVelocity = -20.0;
	velocityVariance = 0;
	ejectionOffset	= 4.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = RedEtherformBufferRepairEmitter_Particle;
};

//-------------------------------

datablock ParticleData(BlueEtherformBufferRepairEmitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 220;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 1.0 1.0 0.0";
	colors[1]	  = "1.0 1.0 1.0 1.0";
	colors[2]	  = "1.0 1.0 1.0 1.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "~/data/particles/cross1";
	allowLighting = false;
};

datablock ParticleEmitterData(BlueEtherformBufferRepairEmitter)
{
	ejectionPeriodMS = 500;
	periodVarianceMS = 0;
	ejectionVelocity = -20.0;
	velocityVariance = 0;
	ejectionOffset	= 4.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = BlueEtherformBufferRepairEmitter_Particle;
};

//------------------------------------------------------------------------------
// laserTrail...

datablock MultiNodeLaserBeamData(RedEtherform_LaserTrailOne)
{
	hasLine = false;
	lineColor	= "1.00 1.00 0.00 0.7";

	hasInner = true;
	innerColor = "1.0 0.0 0.0 0.5";
	innerWidth = "0.75";

	hasOuter = false;
	outerColor = "1.00 0.00 1.00 0.1";
	outerWidth = "0.10";

	//bitmap = "~/data/vehicles/team1scoutflyer/lasertrail";
	//bitmapWidth = 1;

	blendMode = 1;
	fadeTime = 1000;
};

datablock MultiNodeLaserBeamData(RedEtherform_LaserTrailTwo)
{
	hasLine = true;
	lineColor	= "1.00 0.50 0.00 0.5";

	hasInner = false;
	innerColor = "1.0 0.0 0.0 0.5";
	innerWidth = "0.75";

	hasOuter = false;
	outerColor = "1.00 0.00 1.00 0.1";
	outerWidth = "0.10";

	//bitmap = "~/data/vehicles/team1scoutflyer/lasertrail";
	//bitmapWidth = 1;

	blendMode = 1;
	fadeTime = 4000;
};

datablock MultiNodeLaserBeamData(BlueEtherform_LaserTrailOne)
{
	hasLine = false;
	lineColor	= "1.00 1.00 0.00 0.7";

	hasInner = true;
	innerColor = "0.0 0.0 1.0 0.5";
	innerWidth = "0.75";

	hasOuter = false;
	outerColor = "1.00 0.00 1.00 0.1";
	outerWidth = "0.10";

	//bitmap = "~/data/vehicles/team1scoutflyer/lasertrail";
	//bitmapWidth = 1;

	blendMode = 1;
	fadeTime = 1000;
};

datablock MultiNodeLaserBeamData(BlueEtherform_LaserTrailTwo)
{
	hasLine = true;
	lineColor	= "0.00 0.50 1.00 0.5";

	hasInner = false;
	innerColor = "0.0 0.0 1.0 0.5";
	innerWidth = "0.75";

	hasOuter = false;
	outerColor = "1.00 0.00 1.00 0.1";
	outerWidth = "0.10";

	//bitmap = "~/data/vehicles/team1scoutflyer/lasertrail";
	//bitmapWidth = 1;

	blendMode = 1;
	fadeTime = 4000;
};

//-----------------------------------------------------------------------------
// contrail...

datablock ParticleData(RedEtherform_ContrailParticle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= -0.2;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 0;
	textureName			 = "~/data/particles/small_particle4";
	colors[0]	  = "1.0 0.0 0.0 1.0";
	colors[1]	  = "1.0 0.0 1.0 0.66";
	colors[2]	  = "1.0 1.0 0.0 0.33";
	colors[3]	  = "0.0 5.0 0.0 0.0";
	sizes[0]		= 0.25;
	sizes[1]		= 0.25;
	sizes[2]		= 0.25;
	sizes[3]		= 0.25;
	times[0]		= 0.0;
	times[1]		= 0.333;
	times[2]		= 0.666;
	times[3]		= 1.0;
	renderDot = true;
};

datablock ParticleEmitterData(RedEtherform_ContrailEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 2;
	velocityVariance = 0;
	ejectionOffset	= 0.0;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 3000;
	phiVariance		= 0;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "RedEtherform_ContrailParticle";
};

datablock ParticleData(BlueEtherform_ContrailParticle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= -0.2;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 0;
	textureName			 = "~/data/particles/small_particle4";
	colors[0]	  = "0.0 0.0 1.0 1.0";
	colors[1]	  = "1.0 0.0 1.0 0.66";
	colors[2]	  = "0.0 1.0 1.0 0.33";
	colors[3]	  = "0.0 5.0 0.0 0.0";
	sizes[0]		= 0.25;
	sizes[1]		= 0.25;
	sizes[2]		= 0.25;
	sizes[3]		= 0.25;
	times[0]		= 0.0;
	times[1]		= 0.333;
	times[2]		= 0.666;
	times[3]		= 1.0;
	renderDot = true;
};

datablock ParticleEmitterData(BlueEtherform_ContrailEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 2;
	velocityVariance = 0;
	ejectionOffset	= 0.0;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 3000;
	phiVariance		= 0;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "BlueEtherform_ContrailParticle";
};
