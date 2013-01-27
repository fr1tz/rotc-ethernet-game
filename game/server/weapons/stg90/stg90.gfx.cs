//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(STG90ProjectileMissedEnemyEffect)
{
	soundProfile = STG90ProjectileMissedEnemySound;

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



