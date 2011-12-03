//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function clientCmdLoadingBarTxt(%text)
{
	LoadingProgressTxt.setValue(replaceBindVars(%text));
}

//------------------------------------------------------------------------------

function clientCmdBeginQuickbarTxt(%update)
{
	IngameQuickbarText1.zText = "";
	IngameQuickbarText2.zText = "";
	IngameQuickbarText4.zText = "";
	IngameQuickbarText8.zText = "";
	if(%update)
	{
		IngameQuickbarScroll.zPrevPosX = IngameQuickbarScroll.getScrollPositionX();
		IngameQuickbarScroll.zPrevPosY = IngameQuickbarScroll.getScrollPositionY();
	}
	else
	{
		IngameQuickbarScroll.zPrevPosX = 0;
		IngameQuickbarScroll.zPrevPosY = 0;
	}
}

function clientCmdAddQuickbarTxt(%text, %layerMask)
{
   if(%layerMask $= "")
   {
   	IngameQuickbarText1.zText = IngameQuickbarText1.zText @ %text;
      return;
   }
   if(%layerMask & 1)
   	IngameQuickbarText1.zText = IngameQuickbarText1.zText @ %text;
   if(%layerMask & 2)
   	IngameQuickbarText2.zText = IngameQuickbarText2.zText @ %text;
   if(%layerMask & 4)
   	IngameQuickbarText4.zText = IngameQuickbarText4.zText @ %text;
   if(%layerMask & 8)
   	IngameQuickbarText4.zText = IngameQuickbarText8.zText @ %text;
}

function clientCmdEndQuickbarTxt()
{
	IngameQuickbarText1.setText(replaceBindVars(IngameQuickbarText1.zText));
	IngameQuickbarText2.setText(replaceBindVars(IngameQuickbarText2.zText));
	IngameQuickbarText4.setText(replaceBindVars(IngameQuickbarText4.zText));
	IngameQuickbarText8.setText(replaceBindVars(IngameQuickbarText8.zText));
	IngameQuickbarText1.forceReflow();
	IngameQuickbarText2.forceReflow();
	IngameQuickbarText4.forceReflow();
	IngameQuickbarText8.forceReflow();
	IngameQuickbarScroll.setScrollPosition(
		IngameQuickbarScroll.zPrevPosX,
		IngameQuickbarScroll.zPrevPosY
	);

   %quickBarHeight = getWord(IngameQuickbarText1.extent, 1);
   if(getWord(IngameQuickbarText2.extent, 1) > %quickBarHeight)
      %quickBarHeight = getWord(IngameQuickbarText2.extent, 1);
   if(getWord(IngameQuickbarText4.extent, 1) > %quickBarHeight)
      %quickBarHeight = getWord(IngameQuickbarText4.extent, 1);
   if(getWord(IngameQuickbarText8.extent, 1) > %quickBarHeight)
      %quickBarHeight = getWord(IngameQuickbarText8.extent, 1);

	// Minimize size of Quickbar and adjust size of the menu...
	%w = getWord(IngameQuickbarScroll.extent, 0);
	%h = %quickBarHeight + 4;
	%y = getWord(IngameQuickbarScroll.position, 1);
	IngameQuickbarScroll.setExtent(%w, %h);
	%stor = getWord(IngameMenuScroll.position, 1);
	IngameMenuScroll.setPosition(14, %y + %h - 2);
	%y = getWord(IngameMenuScroll.position, 1);
	%h = getWord(IngameMenuScroll.extent, 1);
	IngameMenuScroll.setExtent(%w, %h + (%stor - %y));
}

//------------------------------------------------------------------------------

function clientCmdBeginMenuTxt(%update)
{
	IngameMenuText1.zText = "";
	IngameMenuText2.zText = "";
	IngameMenuText4.zText = "";
	IngameMenuText8.zText = "";
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

function clientCmdAddMenuTxt(%text, %layerMask)
{
   if(%layerMask $= "")
   {
   	IngameMenuText1.zText = IngameMenuText1.zText @ %text;
      return;
   }
   if(%layerMask & 1)
   	IngameMenuText1.zText = IngameMenuText1.zText @ %text;
   if(%layerMask & 2)
   	IngameMenuText2.zText = IngameMenuText2.zText @ %text;
   if(%layerMask & 4)
   	IngameMenuText4.zText = IngameMenuText4.zText @ %text;
   if(%layerMask & 8)
   	IngameMenuText4.zText = IngameMenuText8.zText @ %text;
}

function clientCmdEndMenuTxt()
{
	IngameMenuText1.setText(replaceBindVars(IngameMenuText1.zText));
	IngameMenuText2.setText(replaceBindVars(IngameMenuText2.zText));
	IngameMenuText4.setText(replaceBindVars(IngameMenuText4.zText));
	IngameMenuText8.setText(replaceBindVars(IngameMenuText8.zText));
	IngameMenuText1.forceReflow();
	IngameMenuText2.forceReflow();
	IngameMenuText4.forceReflow();
	IngameMenuText8.forceReflow();
	IngameMenuScroll.setScrollPosition(
		IngameMenuScroll.zPrevPosX,
		IngameMenuScroll.zPrevPosY
	);
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

function IngameMenuScroll::onWake(%this)
{
	%this.schedule(0, "setScrollPosition", %this.zPrevPosX, %this.zPrevPosY);	
}

function IngameMenuScroll::onSleep(%this)
{
	%this.zPrevPosX = %this.getScrollPositionX();
	%this.zPrevPosY = %this.getScrollPositionY();  
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

function IngameQuickbarText1::onURL(%this, %url)
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

function IngameQuickbarText2::onURL(%this, %url)
{
	IngameQuickbarText1.onURL(%url);
}

function IngameQuickbarText4::onURL(%this, %url)
{
	IngameQuickbarText1.onURL(%url);
}

function IngameQuickbarText8::onURL(%this, %url)
{
	IngameQuickbarText1.onURL(%url);
}

function IngameMenuText1::onURL(%this, %url)
{
	IngameQuickbarText1.onURL(%url);
}

function IngameMenuText2::onURL(%this, %url)
{
	IngameQuickbarText1.onURL(%url);
}

function IngameMenuText4::onURL(%this, %url)
{
	IngameQuickbarText1.onURL(%url);
}

function IngameMenuText8::onURL(%this, %url)
{
	IngameQuickbarText1.onURL(%url);
}

