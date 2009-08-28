//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

function AboutWindow::onWake(%this)
{
	%text="<just:center><font:Arial:24>Revenge Of The Cats: Ethernet\n"@
			"<font:Arial:22>"@ $ETHERNET_VERSION @"\n"@
			"<font:Arial:12>\nEngine version: "@ getVersionString() @", "@ getBuildString() @"Build\n\n"@
			"<font:Arial:16>See the <a:manual.html>manual</a> for complete credits.";
	aboutText.setText(%text);	
}	

