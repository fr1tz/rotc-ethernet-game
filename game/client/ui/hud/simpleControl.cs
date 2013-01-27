//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - simpleControl.cs
// code to maintain the "simple control" HUD
//------------------------------------------------------------------------------

addMessageCallback('MsgSimpleControlUpdate', handleSimpleControlUpdate);

//-----------------------------------------------------------------------------

function handleSimpleControlUpdate(%msgType, %msgString, %slot, %ghostId )
{
	if(%ghostId == 0)
		%obj = 0;
	else
		%obj = GameConnection::getServerConnection().resolveGhostID(%ghostId);

	%text = " " TAB " " TAB " " TAB %obj;
	SimpleControlHudList.setRowById(%slot, %text);
	SimpleControlHudList.sortNumerical(0, true);


	//cancel($SimpleControlHudUpdateThread);
	//$SimpleControlHudUpdateThread = schedule(100, 0, "updateSimpleControlHud");
}

//-----------------------------------------------------------------------------

function updateSimpleControlHud()
{
	%slot = SimpleControlHudList.rowCount();
	while(%slot)
	{
		%text = SimpleControlHudList.getRowTextById(%slot);
		%obj = getField(%text,3);
		
		if(%obj != 0)
		{
			%control = GameConnection::getServerConnection().getControlObject();
			%vec = VectorSub(%obj.getPosition(), %control.getPosition());
			%dist = VectorLen(%vec);

			%text = setField(%text, 0, %slot);
			%text = setField(%text, 1, StripMLControlChars(%obj.getShapeName()));
			%text = setField(%text, 2, %dist);

			SimpleControlHudList.setRowById(%slot, %text);
		}
		
		%slot -= 1;
	}
	
	$SimpleControlHudUpdateThread = schedule(100, 0, "updateSimpleControlHud");
}
