//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Hook into the client update messages to maintain our player list
// and scoreboard.
//-----------------------------------------------------------------------------

addMessageCallback('MsgClientJoin', handleClientJoin);
addMessageCallback('MsgClientDrop', handleClientDrop);
addMessageCallback('MsgClientJoinTeam', handleClientJoinTeam);
addMessageCallback('MsgClientScoreChanged', handleClientScoreChanged);

//-----------------------------------------------------------------------------

function handleClientJoin(%msgType, %msgString, %clientName, %clientId, %guid, %score, %isAI, %isAdmin, %isSuperAdmin )
{
	PlayerListWindow.update(%clientId,detag(%clientName),"unassigned",-1,%isSuperAdmin,%isAdmin,%isAI,%score);
}

function handleClientJoinTeam(%msgType, %msgString, %clientName, %clientTeamName, %clientTeamId, %clientId, %guid, %score, %isAI, %isAdmin, %isSuperAdmin )
{
	PlayerListWindow.update(%clientId,detag(%clientName),%clientTeamName,%clientTeamId,%isSuperAdmin,%isAdmin,%isAI,%score);
}

function handleClientDrop(%msgType, %msgString, %clientName, %clientId)
{
	PlayerListWindow.remove(%clientId);
}

function handleClientScoreChanged(%msgType, %msgString, %score, %clientId)
{
	PlayerListWindow.updateScore(%clientId,%score);
}

// PlayerListWindowList field layout:
//	 0: player name + tag
//	 1: team name
//	 2: score
//	 3: team id

function PlayerListWindow::update(%this,%clientId,%name,%teamName,%teamId,%isSuperAdmin,%isAdmin,%isAI,%score)
{
	// Build the row to display.  The name can have ML control tags,
	// including color and font.  Since we're not using and
	// ML control here, we need to strip them off.
	%tag = %isSuperAdmin? "[Super]":
			 (%isAdmin? "[Admin]":
			 (%isAI? "[Bot]":
			 ""));

	%text = StripMLControlChars(%name) SPC %tag TAB StripMLControlChars(%teamName) TAB %score TAB %teamId;

	// update or add the player to the control
	if(PlayerListWindowList.getRowNumById(%clientId) == -1)
		PlayerListWindowList.addRow(%clientId, %text);
	else
		PlayerListWindowList.setRowById(%clientId, %text);

	// sort by team
	PlayerListWindowList.sortNumerical(3);
}

function PlayerListWindow::updateScore(%this,%clientId,%score)
{
	%text = PlayerListWindowList.getRowTextById(%clientId);
	%text = setField(%text,2,%score);
	PlayerListWindowList.setRowById(%clientId, %text);
	PlayerListWindowList.sortNumerical(3);
}

function PlayerListWindow::remove(%this,%clientId)
{
	PlayerListWindowList.removeRowById(%clientId);
}

function PlayerListWindow::toggle(%this)
{
	if( %this.isMaximized )
	{
		%this.resize(getWord(%this.oldPos,0),getWord(%this.oldPos,1),300,52);
		%this.isMaximized = false;

		// make sure selected row is visible...
		%id = PlayerListWindowList.getSelectedId();
		%ix = PlayerListWindowList.getRowNumById(%id);
		PlayerListWindowList.scrollVisible(%ix);
	}
	else
	{
		%this.oldPos = %this.position;
		%res = getResolution();
		%resWidth = getWord(%res,0);
		%resHeight = getWord(%res,1);
		%this.resize(%resWidth/2-250,%resHeight/2-200,501,401);
		%this.isMaximized = true;
	}
}

function PlayerListWindow::clear(%this)
{
	// Override to clear the list.
	PlayerListWindowList.clear();
}

function PlayerListWindow::selectNext(%this)
{
	%id = PlayerListWindowList.getSelectedId();
	%index = PlayerListWindowList.getRowNumById(%id);
	if( %index < 0 || %index == PlayerListWindowList.rowCount()-1 )
		PlayerListWindowList.setSelectedRow(0);
	else
		PlayerListWindowList.setSelectedRow(%index+1);
}

function PlayerListWindow::selectPrev(%this)
{
	%id = PlayerListWindowList.getSelectedId();
	%index = PlayerListWindowList.getRowNumById(%id);
	if( %index <= 0 )
		PlayerListWindowList.setSelectedRow(PlayerListWindowList.rowCount()-1);
	else
		PlayerListWindowList.setSelectedRow(%index-1);
}



