//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function reshapeWindow()
{
	ReshapeDlg.op = "reshape";
	ReshapeDlg.win = 0;
	ReshapeDlg.mouseDown = false;
	ReshapeDlgWindow.setVisible(false);
	Canvas.pushDialog(ReshapeDlg);
	Canvas.setCursor("PickCursor");
}

function lowerWindow()
{
	ReshapeDlg.op = "lower";
	ReshapeDlg.win = 0;
	ReshapeDlg.mouseDown = false;
	ReshapeDlgWindow.setVisible(false);
	Canvas.pushDialog(ReshapeDlg);
	Canvas.setCursor("PickCursor");
}

function ReshapeDlgMouseDetector::onMouseDown(%this,%modifier,%coord,%clickCount)
{
	if(!ReshapeDlg.mouseDown)
	{
		ReshapeDlg.mouseDown = true;
		return;
	}

	if(ReshapeDlg.op $= "reshape")
	{
		%this.startx = getWord(%coord, 0);
		%this.starty = getWord(%coord, 1);
	}
}

function ReshapeDlgMouseDetector::onMouseUp(%this,%modifier,%coord,%clickCount)
{
	if(!ReshapeDlg.mouseDown)
		return;

	if(ReshapeDlg.win == 0)
	{
		%x = getWord(%coord, 0);
		%y = getWord(%coord, 1);

		for(%i = Windows.getCount() - 1; %i >= 0 ; %i--)
		{
			%obj = Windows.getObject(%i);
			if(%obj.pointInControl(%x, %y))
			{
				ReshapeDlg.win = %obj;
				if(ReshapeDlg.op $= "reshape")
				{
					Canvas.setCursor("ReshapeCursor");
					return;
				}
				else if(ReshapeDlg.op $= "lower")
				{
					break;
				}
			}
		}
	}

	if(ReshapeDlg.win == 0)
	{

	}	
	else if(ReshapeDlg.op $= "reshape")
	{
		%x = getWord(ReshapeDlgWindow.getPosition(), 0);
		%y = getWord(ReshapeDlgWindow.getPosition(), 1);
		%w = getWord(ReshapeDlgWindow.getExtent(), 0);
		%h = getWord(ReshapeDlgWindow.getExtent(), 1);
		ReshapeDlg.win.resize(%x, %y, %w, %h);
		Windows.pushToBack(ReshapeDlg.win);
	}
	else if(ReshapeDlg.op $= "lower")
	{
		Windows.bringToFront(ReshapeDlg.win);
	}

	Canvas.popDialog(ReshapeDlg);
	Canvas.setCursor("DefaultCursor");
}

function ReshapeDlgMouseDetector::onMouseDragged(%this,%modifier,%coord,%clickCount)
{
	if(ReshapeDlg.op $= "pick")
	{


	}
	else if(ReshapeDlg.op $= "reshape")
	{
		ReshapeDlgWindow.setVisible(true);
	
		%x = getWord(%coord, 0);
		%y = getWord(%coord, 1);
	
		if(%this.startx < %x)
		{
			%w = %x - %this.startx;
			%x = %this.startx;
		}
		else
			%w = %this.startx - %x;
	
		if(%this.starty < %y)
		{
			%h = %y - %this.starty;
			%y = %this.starty;
		}
		else
			%h = %this.starty - %y;
	
		ReshapeDlgWindow.resize(%x, %y, %w, %h);
	}
}

function ReshapeDlgMouseDetector::onRightMouseDown(%this,%modifier,%coord,%clickCount)
{
	Canvas.popDialog(ReshapeDlg);
	Canvas.setCursor("DefaultCursor");
}

