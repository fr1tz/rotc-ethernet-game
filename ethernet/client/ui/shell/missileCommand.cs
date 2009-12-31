//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function MissileCommand::incScore(%this,%value)
{
	%this.score += %value;
	MissileCommandScore.setText(MissileCommand.score);
}

function MissileCommand::onCityDestroyed(%this)
{
	%this.incScore(-100);
}

function MissileCommand::onAllCitiesDestroyed(%this)
{
	%this.incScore(-1000);
}

function MissileCommand::onMissileBaseDestroyed(%this)
{
	%this.incScore(-500);
}

function MissileCommand::onMissileLaunched(%this)
{
	%this.incScore(-1);
}

function MissileCommand::onEnemyMissileDestroyed(%this)
{
	%this.incScore(20);
}

//-----------------------------------------------

function MissileCommand::reset(%this)
{
	%this.score=0;
	MissileCommandScore.setText(0);
	
	if(%this.isPaused())
		%this.togglePause();
		
	MissileCommand.resetGame();
}

function MissileCommand::applyOptions(%this)
{
	%this.ownMissileFireInterval = MissileCommandOpt1.getValue();
	%this.enemyMissileFireInterval = MissileCommandOpt2.getValue();
	%this.ownMissileSpeed = MissileCommandOpt3.getValue();
	%this.enemyMissileSpeed = MissileCommandOpt4.getValue();
	%this.missileExplosionBlastRadius = MissileCommandOpt5.getValue();
	%this.missileExplosionBlastSpeed = MissileCommandOpt6.getValue();
	%this.missileBaseBarrelAdjustSpeed = MissileCommandOpt7.getValue();
}

function MissileCommand::loadOldschoolPreset(%this)
{
	%this.ownMissileFireInterval = "2000";
	%this.enemyMissileFireInterval = "1000";
	%this.ownMissileSpeed = "100";
	%this.enemyMissileSpeed = "30";
	%this.missileExplosionBlastRadius = "100";
	%this.missileExplosionBlastSpeed = "40";
	%this.missileBaseBarrelAdjustSpeed = "180";
}

function MissileCommand::loadArcadePreset(%this)
{
	%this.ownMissileFireInterval = "100";
	%this.enemyMissileFireInterval = "1000";
	%this.ownMissileSpeed = "500";
	%this.enemyMissileSpeed = "100";
	%this.missileExplosionBlastRadius = "30";
	%this.missileExplosionBlastSpeed = "100";
	%this.missileBaseBarrelAdjustSpeed = "180";
}

function MissileCommand::loadUltraHeavyPreset(%this)
{
	%this.ownMissileFireInterval = "10";
	%this.enemyMissileFireInterval = "100";
	%this.ownMissileSpeed = "1000";
	%this.enemyMissileSpeed = "250";
	%this.missileExplosionBlastRadius = "30";
	%this.missileExplosionBlastSpeed = "100";
	%this.missileBaseBarrelAdjustSpeed = "360";
}
