//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

// script function called by team drag race zone code
function ShapeBaseData::updateRacingLaneZone(%this, %obj, %enterZone, %leftZone)
{
	%this.updateZone(%obj, %enterZone, %leftZone);
	if(!%obj.zInOwnZone)
	{
		//echo(" not in own zone");
        %this.onLeaveMissionArea(%obj);  
   }
}
