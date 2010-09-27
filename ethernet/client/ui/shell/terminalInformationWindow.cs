//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function TerminalInformationWindow::onWake(%this)
{
	%text= ""@
		"- 1024 KiloBytes of memory\n"@
		"- Bitmap display\n"@
		"- Digitizing mouse\n"@
		"- Integrated speaker & microphone";

	TerminalInfoWindowText.setText(%text);	
}	

