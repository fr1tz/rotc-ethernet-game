//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function clientCmdBeginMenuTxt(%update)
{
	IngameMenuText.zText = "";
	if(%update)
	{
		IngameMenuScroll.zPrevPosX = IngameMenuScroll.getScrollPositionX();
		IngameMenuScroll.zPrevPosY = IngameMenuScroll.getScrollPositionY();
	}
	else
	{
		IngameMenuScroll.zPrevPosX = 0;
		IngameMenuScroll.zPrevPosY = 0;
	}
}

function clientCmdAddMenuTxt(%text)
{
	IngameMenuText.zText = IngameMenuText.zText @ %text;
}

function clientCmdEndMenuTxt()
{
	IngameMenuText.setText(replaceBindVars(IngameMenuText.zText));
	IngameMenuText.forceReflow();
	IngameMenuScroll.setScrollPosition(IngameMenuScroll.zPrevPosX,
		IngameMenuScroll.zPrevPosY);
}

//-----------------------------------------------------------------------------

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
		
  commandToServer('MenuVisible', true); 
}

function MissionWindow::onSleep(%this)
{
  commandToServer('MenuVisible', false); 
}

function MissionWindow::showTextInputBox(%this, %label, %text)
{
	MissionServerInputLabel.setText(%label);
	MissionServerInputText.setText(%text);
	MissionServerInput.setVisible(true);
}

function MissionWindow::sendInput(%this)
{
	commandToServer('TextInput', MissionWindowServerInputText.getText()); 
	MissionServerInput.setVisible(false);
}

function MissionWindow::cancelInput(%this)
{
	MissionServerInput.setVisible(false);
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
