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

function addWindow(%control)
{
	%oldparent = %control.getParent();
	%parent = Shell;
	if(Canvas.getContent() != Shell.getId())
		%parent = ShellDlg;
	%parent.add(%control);
	%parent.pushToBack(%control);
	if(%control.getParent() != %oldParent)
		%control.onAddedAsWindow();
	windowSelected(%control);
	Canvas.repaint();
}

function removeWindow(%control)
{
	%control.getParent().remove(%control);
	%control.onRemovedAsWindow();
	Canvas.repaint();
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
	if(%ctrl == $SelectedWindow)
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

