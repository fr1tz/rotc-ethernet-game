//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

addMessageCallback('MsgMenuText', handleMsgMenuText);

//-----------------------------------------------------------------------------

function handleMsgMenuText(%msgType, %msgString, %text)
{
	if(%text $= "")
		IngameMenuText.setText("");
	else
		IngameMenuText.setText(IngameMenuText.getText() @ %text);
}

function IngameMenuWindow::onWake(%this)
{
	if($Server::ServerType $= "SinglePlayer")
		IngameMenuDisconnect.text = "Quit Mission";
	else
		IngameMenuDisconnect.text = "Disconnect";
}

function IngameMenuText::onURL(%this, %url)
{
	if(getWord(%url, 0) $= "cmd")
	{
		%n = getWordCount(%url);
		if(%n < 2)
			return;

		%args = "";
		for(%i = 2; %i < %n; %i++)
			%args = %args SPC getField(%url, %i);
		
		commandToServer('SimpleCommand', getWord(%url, 1), getWord(%url, 2));
	}
	else 
	{
		gotoWebPage(%url);
	}
}