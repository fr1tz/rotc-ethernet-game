//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function Blueprint::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type. For the mission editor
	%obj = new StaticShape() {
		dataBlock = %data;
		isBlueprint = true;
	};
	
	return %obj;
}
