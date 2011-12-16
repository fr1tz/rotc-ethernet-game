//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function GameConnection::showLoadout(%this, %no, %expandslot, %showInfo, %infoPos)
{
	%L3 = om_init();
	%L3 = %L3 @ om_head(%this, "Edit Loadouts");
	%L3 = %L3 @ "<font:NoveSquare:24>Nothing here yet";

	%this.beginMenuText(false);
	if(%L1 !$= "") %this.addMenuText(%L1, 1);
	if(%L2 !$= "") %this.addMenuText(%L2, 2);
	if(%L3 !$= "") %this.addMenuText(%L3, 4);
	if(%L4 !$= "") %this.addMenuText(%L4, 8);
	%this.endMenuText();
}



