//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function TopDownWindow::grabInput()
{
	activateWindowInputGrab(TopDownWindow);
    pushActionMap(CmdrScreenActionMap);
}

function TopDownWindow::onWake(%this)
{
	TopDownWindowPopupMenu.clear();
	TopDownWindowPopupMenu.add("Lower", 1);
	TopDownWindowPopupMenu.add("Reshape", 2);
	TopDownWindowPopupMenu.add("Maximize", 3);
	TopDownWindowPopupMenu.add("Use left half of screen", 4);
	TopDownWindowPopupMenu.add("Use right half of screen", 5);
	//TopDownWindowPopupMenu.add("Use top-left quarter of screen", 6);
	//TopDownWindowPopupMenu.add("Use bottom-left quarter of screen", 7);
	//TopDownWindowPopupMenu.add("Use top-right quarter of screen", 8);
	//TopDownWindowPopupMenu.add("Use bottom-right quarter of screen", 9);
}

function TopDownWindowMouseDetector::onMouseDown(%this,%modifier,%coord,%clickCount)
{
	TopDownWindow.grabInput();
}


function TopDownWindowPopupMenu::onSelect( %this, %id, %text )
{
	TopDownWindowPopupMenu.setSelected(-1);
	
	if(%id == 1)
		Shell.bringToFront(TopDownWindow);
	else if(%id == 2)
	{
		reshapeWindow(TopDownWindow);
	}
	else 
	{
		%r = Shell.getExtent();
		%w = getWord(%r, 0);
		%h = getWord(%r, 1);
		if(%id == 3)
			TopDownWindow.resize(0, 0, %w, %h);
		else if(%id == 4)
			TopDownWindow.resize(0, 0, %w/2, %h);
		else if(%id == 5)
			TopDownWindow.resize(%w/2, 0, %w/2, %h);
		else if(%id == 6)
			TopDownWindow.resize(0, 0, %w/2, %h/2);
		else if(%id == 7)
			TopDownWindow.resize(0, %h/2, %w/2, %h/2);
		else if(%id == 8)
			TopDownWindow.resize(%w/2, 0, %w/2, %h/2);
		else if(%id == 9)
			TopDownWindow.resize(%w/2, %h/2, %w/2, %h/2);
	}
}
