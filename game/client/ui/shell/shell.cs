//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - shell.cs
// Code for Revenge Of The Cats' Shell
//------------------------------------------------------------------------------

if(isObject(DefaultCursor))
{
    DefaultCursor.delete();

    new GuiCursor(DefaultCursor)
    {
	   hotSpot = "6 6";
	   bitmapName = "./pixmaps/mg_arrow6";
    };
}

function addWindow(%control, %inactive)
{
	%oldparent = %control.getParent();
	%parent = Shell;
	if(Canvas.getContent() != Shell.getId())
		%parent = ShellDlg;
	if(%control.getParent().getId() != %parent.getId())
	{
		%parent.add(%control);
		%parent.pushToBack(%control);
		if(%control.getParent() != %oldParent)
			%control.onAddedAsWindow();
	}
	if(!%inactive)
		windowSelected(%control);
	Canvas.repaint();
}

function removeWindow(%control)
{
	%control.getParent().remove(%control);
	%control.onRemovedAsWindow();
	Canvas.repaint();
}

function Shell::onWake(%this)
{
	// Make sure we're displaying the toolbox
	addWindow(RootMenuWindow);

	// Make sure we're displaying the IRC window if we're not offline...
	if(!$IRC::Offline)
		addWindow(IrcWindow, true);
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


function windowChangeProfile(%ctrl, %profile)
{
	%ctrl.profile = %profile;
	%parent = %ctrl.getParent();
	%parent.remove(%ctrl);
	%parent.add(%ctrl);
}

function windowSelected(%ctrl)
{
	if(%ctrl.getId() == $SelectedWindow.getId())
		return;

	if($SelectedWindow !$= "")
		windowChangeProfile($SelectedWindow, GuiInactiveWindowProfile);
		
	windowChangeProfile(%ctrl, GuiWindowProfile);
	%ctrl.makeFirstResponder(true);

	$SelectedWindow = %ctrl;
}

function GuiCanvas::onCanvasMouseDown(%this, %ctrl)
{
	%win = "";
	while(isObject(%ctrl))
	{
		if(%ctrl.getClassName() $= "GuiWindowCtrl")
		{
			%win = %ctrl;
			break;
		}
		%ctrl = %ctrl.getParent();
	}

	if(isObject(%win))
		windowSelected(%win);
}

