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
	{
		%newtext = IngameMenuText.getText() @ %text;

		while(true)
		{
			%p = strstr(%newtext, "@bind");
			if(%p == -1)
				break;

			%s = getSubStr(%newtext, %p, 7);
			%n = getSubStr(%s, 5, 2);

			%cmd = $RemapCmd[%n];
			%temp = moveMap.getBinding( %cmd );
			%device = getField( %temp, 0 );
			%object = getField( %temp, 1 );
			%mapString = getMapDisplayName( %device, %object );

			%newtext = strreplace(%newtext, %s, %mapString);
		} 

		IngameMenuText.setText(%newtext);
	}
}

function MissionWindow::resizeIdeal(%this)
{
	%s  = %this.getParent().getExtent();
	%sw = getWord(%s, 0);
	%sh = getWord(%s, 1);

	%w = getWord(%this.MinExtent, 0);
	%h = %sh - 20;

	%this.setPosition(%sw - %w - 10, 10);
	%this.setExtent(%w, %h);
}


function MissionWindow::onWake(%this)
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
