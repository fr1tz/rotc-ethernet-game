//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - shell.cs
// Code for Revenge Of The Cats' Shell
//------------------------------------------------------------------------------

if(isObject(PickCursor)) PickCursor.delete();
new GuiCursor(PickCursor)
{
	hotSpot = "0 0";
	bitmapName = "./pixmaps/mg_arrow2";
};


if(isObject(ReshapeCursor)) ReshapeCursor.delete();
new GuiCursor(ReshapeCursor)
{
	hotSpot = "7 7";
	bitmapName = "./pixmaps/mg_crosshair";
};

function addWindow(%control)
{
	if(Canvas.getContent() == Shell.getId())
		Windows.add(%control);
	else
		ShellDlg.add(%control);

	%control.onAddedAsWindow();
	Canvas.repaint();
}

function removeWindow(%control)
{
	%control.getParent().remove(%control);
	%control.onRemovedAsWindow();
	Canvas.repaint();
}

function bootSeq()
{
	cursorOff();

	ShellFadein.setVisible(true);
	ShellFadein.done = false;
	ShellFadein.schedule(2000, "setVisible", false);

	if(isObject(DefaultCursor)) DefaultCursor.delete();
	new GuiCursor(DefaultCursor)
	{
		hotSpot = "0 0";
		bitmapName = "./pixmaps/mg_arrow";
	};

	Windowops.setVisible(false);
	TerminalMenuWindow.setVisible(false);
	SystemMenuWindow.setVisible(false);
	ConnectionWindow.setVisible(false);

	ConnectionWindowText.HorizSizing = "center";
	ConnectionWindowText.VertSizing = "center";
	ConnectionWindowText.setText("");
	Shell.add(ConnectionWindowText);

	%t  =   0; ConnectionWindowText.schedule(%t, "append", "TERMINIT\n");
	%t +=   0; ConnectionWindowText.schedule(%t, "append", "Checking memory");
	for(%i = 0; %i < 16; %i++)
	{
		%t += 32; ConnectionWindowText.schedule(%t, "append", "..");
	}
	%t +=  50; ConnectionWindowText.schedule(%t, "append", " OK.\n");
	%t +=  100; ConnectionWindowText.schedule(%t, "append", "\nConnected to:\n\n");
	%t +=  100; ConnectionWindowText.schedule(%t, "append", "     P-OS Timesharing System\n");
	%t +=  100; ConnectionWindowText.schedule(%t, "append", "     Copyright (C) 1980 BünzliTech\n");
	%t +=  100; ConnectionWindowText.schedule(%t, "append", "\n");
	%t +=  100; ConnectionWindowText.schedule(%t, "append", "Connection speed: unknown\n");
	%t +=  100; ConnectionWindowText.schedule(%t, "append", "Downloading user interface");
	for(%i = 0; %i < 6; %i++)
	{
		%t += 50; ConnectionWindowText.schedule(%t, "append", ".");
	}
	%t +=    0; Canvas.schedule(%t, "cursorOn");
	for(%i = 0; %i < 16; %i++)
	{
		%t += 50; ConnectionWindowText.schedule(%t, "append", ".");
	}
	%t +=   0; Windowops.schedule(%t, "setVisible", true);
	for(%i = 0; %i < 8; %i++)
	{
		%t += 50; ConnectionWindowText.schedule(%t, "append", ".");
	}
	%t +=   0; TerminalMenuWindow.schedule(%t, "setVisible", true);
	for(%i = 0; %i < 4; %i++)
	{
		%t += 50; ConnectionWindowText.schedule(%t, "append", ".");
	}
	%t +=  100; SystemMenuWindow.schedule(%t, "setVisible", true);

	%t +=  100; ConnectionWindowText.schedule(%t, "append", " done.\n\n");
	%t +=  100; ConnectionWindowText.schedule(%t, "append", "     === SYSTEM READY ===\n");
}

function Shell::onWake(%this)
{	
	if(%this.bootSeqDone)
		return;

	bootSeq();

	%this.bootSeqDone = true;
}

function ConnectionWindowText::append(%this, %text)
{
	%this.addText(%text, true);
	if(ConnectionWindow.isVisible())
	{
		ConnectionWindowText.setPosition(0,0);
		ConnectionWindowScroll.scrollToBottom();
	}	
}

function ShellRoot::onMouseDown(%this,%modifier,%coord,%clickCount)
{
	//
	// display the root menu...
	//

	if( Shell.isMember(RootMenu) )
		removeWindow(RootMenu);
	else
	{
		RootMenu.position = %coord;
		//addWindow(RootMenu);
		//Canvas.repaint();
	}
}

function ShellRoot::onMouseEnter(%this,%modifier,%coord,%clickCount)
{
 //
}

function WindowsMenu::onWake(%this)
{
	%this.update();
}

function WindowsMenu::update(%this)
{
	%this.clear();
	%this.add("Sink (alt-s)", 1);
	%this.add("Reshape (alt-r)", 2);
	%this.add("Use entire screen (alt-f)", 3);
	%this.add("Use left half of screen", 4);
	%this.add("Use right half of screen", 5);
}

function WindowsMenu::onSelect( %this, %id, %text )
{
	WindowsMenu.setSelected(-1);
	
	if(%id == 1)
		lowerWindow();
	else if(%id == 2)
		reshapeWindow();
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
