//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

function IngameMenuWindow::onWake(%this)
{
	if($Server::ServerType $= "SinglePlayer")
		IngameMenuDisconnect.text = "Quit Mission";
	else
		IngameMenuDisconnect.text = "Disconnect";
}
