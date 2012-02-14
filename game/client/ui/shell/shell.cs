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

function startUpdateHilightedGuiControlsThread()
{
	%set = HilightedGuiControlsSet;
	if(%set.updateThread $= "")
	{
		%set.updateThread = schedule(500, HilightedGuiControlsSet,
			"updateHilightedGuiControlsThread");
	}
}

function stopUpdateHilightedGuiControlsThread()
{
	%set = HilightedGuiControlsSet;
	if(%set.updateThread !$= "")
	{
		cancel(%set.updateThread);
		%set.updateThread = "";
	}
}

function updateHilightedGuiControlsThread()
{
	//echo("updateHilightedGuiControlsThread()");

	stopUpdateHilightedGuiControlsThread();

	%set = HilightedGuiControlsSet;
	if(%set.hilightState $= "")
		%set.hilightState = true;
	else
		%set.hilightState = "";

	for(%i = 0; %i < %set.getCount(); %i++)
	{
		%control = %set.getObject(%i);
		if(%set.hilightState == true)
			windowChangeProfile(%control, GuiHilightButtonProfile);
		else
			windowChangeProfile(%control, GuiButtonProfile);

	}

	startUpdateHilightedGuiControlsThread();
}

function hilightControl(%control, %hilight)
{
	%set = HilightedGuiControlsSet;
	if(%hilight)
	{
		%set.add(%control);
	}
	else
	{
		%set.remove(%control);
			windowChangeProfile(%control, GuiButtonProfile);

	}
}

function Shell::onAdd(%this)
{
	new SimSet(HilightedGuiControlsSet);
}

function Shell::onWake(%this)
{
	ShellVersionString.setText("client version:" SPC $GameVersionString);
	windowSelected(RootMenuWindow);
	startUpdateHilightedGuiControlsThread();
}

function Shell::onSleep(%this)
{
	stopUpdateHilightedGuiControlsThread();
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

