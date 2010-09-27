//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function AvatarWindow::grabInput()
{
	activateWindowInputGrab(AvatarWindow);
	pushActionMap(MoveMap);
}


function AvatarWindow::onWake(%this)
{
	AvatarWindowPopupMenu.clear();
	AvatarWindowPopupMenu.add("Lower", 1);
	AvatarWindowPopupMenu.add("Reshape", 2);
	AvatarWindowPopupMenu.add("Maximize", 3);
	AvatarWindowPopupMenu.add("Use left half of screen", 4);
	AvatarWindowPopupMenu.add("Use right half of screen", 5);
	//AvatarWindowPopupMenu.add("Use top-left quarter of screen", 6);
	//AvatarWindowPopupMenu.add("Use bottom-left quarter of screen", 7);
	//AvatarWindowPopupMenu.add("Use top-right quarter of screen", 8);
	//AvatarWindowPopupMenu.add("Use bottom-right quarter of screen", 9);
}

function AvatarWindowMouseDetector::onMouseDown(%this,%modifier,%coord,%clickCount)
{
	AvatarWindow.grabInput();
}

function AvatarWindowPopupMenu::onSelect( %this, %id, %text )
{
	AvatarWindowPopupMenu.setSelected(-1);
	
	if(%id == 1)
		Shell.bringToFront(AvatarWindow);
	else if(%id == 2)
	{
		reshapeWindow(AvatarWindow);
	}
	else 
	{
		%r = Shell.getExtent();
		%w = getWord(%r, 0);
		%h = getWord(%r, 1);
		if(%id == 3)
			AvatarWindow.resize(0, 0, %w, %h);
		else if(%id == 4)
			AvatarWindow.resize(0, 0, %w/2, %h);
		else if(%id == 5)
			AvatarWindow.resize(%w/2, 0, %w/2, %h);
		else if(%id == 6)
			AvatarWindow.resize(0, 0, %w/2, %h/2);
		else if(%id == 7)
			AvatarWindow.resize(0, %h/2, %w/2, %h/2);
		else if(%id == 8)
			AvatarWindow.resize(%w/2, 0, %w/2, %h/2);
		else if(%id == 9)
			AvatarWindow.resize(%w/2, %h/2, %w/2, %h/2);
	}
}
