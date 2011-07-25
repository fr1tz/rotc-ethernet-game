//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// *** radius damage that simulates the effects of a shock wave
function shockWaveRadiusDamage(%sourceObject, %position, %radius,
	%damage, %damageType, %impulse)
{
	// Use the container system to iterate through all the objects
	// within our explosion radius.  We'll apply damage to all ShapeBase
	// objects.

	%targets = new SimSet();

	InitContainerRadiusSearch(%pos, %radius, $TypeMasks::ShapeBaseObjectType);
	while( (%targetObject = containerSearchNext()) != 0 )
		%targets.add(%targetObject);

	%halfRadius = %radius / 2;
	for(%idx = %targets.getCount()-1; %idx >= 0; %idx-- )
	{
		%targetObject = %targets.getObject(%idx);

		// Calculate how much exposure the current object has to
		// the explosive force.  The object types listed are objects
		// that will block an explosion.  If the object is totally blocked,
		// then no damage is applied.
		%coverage = calcExplosionCoverage(%position, %targetObject,
			$TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
			$TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType |
			$TypeMasks::TurretObjectType);

		if (%coverage == 0)
			continue;

		// Radius distance subtracts out the length of smallest bounding
		// box axis to return an appriximate distance to the edge of the
		// object's bounds, as opposed to the distance to it's center.
		%dist = containerSearchCurrRadiusDist();

		// Calculate a distance scale for the damage and the impulse.
		// Full damage is applied to anything less than half the radius away,
		// linear scale from there.
		%distScale = (%dist < %halfRadius)? 1.0:
			1.0 - ((%dist - %halfRadius) / %halfRadius);
			
		// Calculate shock wave time
		%delay = %radius*5; // 100 ticks dist = 0.5 sec delay

		// Schedule the damage
		%targetObject.schedule(%delay, "damage", %sourceObject, %position,
			%damage * %coverage * %distScale, %damageType);

		// Schedule the impulse
		if (%impulse) {
			%impulseVec = VectorSub(%targetObject.getWorldBoxCenter(), %position);
			%impulseVec = VectorNormalize(%impulseVec);
			%impulseVec = VectorScale(%impulseVec, %impulse * %distScale);
			%targetObject.schedule(%delay, "impulse", %position, %impulseVec);
		}
	}

	%targets.delete();
}

function radiusDamage(%sourceObject, %position, %radius, %damage, %damageType, %impulse)
{
	// Use the container system to iterate through all the objects
	// within our explosion radius.  We'll apply damage to all ShapeBase
	// objects.

	%targets = new SimSet();

	InitContainerRadiusSearch(%pos, %radius, $TypeMasks::ShapeBaseObjectType);
	while( (%targetObject = containerSearchNext()) != 0 )
		%targets.add(%targetObject);

	%halfRadius = %radius / 2;
	for(%idx = %targets.getCount()-1; %idx >= 0; %idx-- )
	{
		%targetObject = %targets.getObject(%idx);

		// Calculate how much exposure the current object has to
		// the explosive force.  The object types listed are objects
		// that will block an explosion.  If the object is totally blocked,
		// then no damage is applied.
		%coverage = calcExplosionCoverage(%position, %targetObject,
			$TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
			$TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType |
			$TypeMasks::TurretObjectType);
			
		if (%coverage == 0)
			continue;

		// Radius distance subtracts out the length of smallest bounding
		// box axis to return an appriximate distance to the edge of the
		// object's bounds, as opposed to the distance to it's center.
		%dist = containerSearchCurrRadiusDist();

		// Calculate a distance scale for the damage and the impulse.
		// Full damage is applied to anything less than half the radius away,
		// linear scale from there.
		%distScale = (%dist < %halfRadius)? 1.0:
			1.0 - ((%dist - %halfRadius) / %halfRadius);

		// Apply the damage
		%targetObject.damage(%sourceObject, %position,
			%damage * %coverage * %distScale, %damageType);

		// Apply the impulse
		if (%impulse) {
			%impulseVec = VectorSub(%targetObject.getWorldBoxCenter(), %position);
			%impulseVec = VectorNormalize(%impulseVec);
			%impulseVec = VectorScale(%impulseVec, %impulse * %distScale);
			%targetObject.impulse(%position, %impulseVec);
		}
	}

	%targets.delete();
}
