//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

if(!isObject(XSLconn)) // EXtended Server List
{
	new TCPObject(XSLConn);
}

//----------------------------------------

function JoinServerWindow::query(%this)
{
	hilightControl(JS_queryMaster, false);

	JS_RefreshServer.setActive(false);
	JS_JoinServer.setActive(false);

	queryMasterServer(
		0,			 // Query flags
		$Client::GameTypeQuery,		 // gameTypes
		$Client::MissionTypeQuery,	 // missionType
		0,			 // minPlayers
		100,		  // maxPlayers
		0,			 // maxBots
		2,			 // regionMask
		0,			 // maxPing
		100,		  // minCPU
		0			  // filterFlags
	);
}

//----------------------------------------

function JoinServerWindow::queryLan(%this)
{
	hilightControl(JS_queryMaster, false);

	JS_RefreshServer.setActive(false);
	JS_JoinServer.setActive(false);

	queryLANServers(
		28000,		// lanPort for local queries
		0,			 // Query flags
		$Client::GameTypeQuery,		 // gameTypes
		$Client::MissionTypeQuery,	 // missionType
		0,			 // minPlayers
		100,		  // maxPlayers
		0,			 // maxBots
		2,			 // regionMask
		0,			 // maxPing
		100,		  // minCPU
		0			  // filterFlags
	);
}

//----------------------------------------

function onServerQueryStatus(%status, %msg, %value)
{
	echo("ServerQuery: " SPC %status SPC %msg SPC %value);
	// Update query status
	// States: start, update, ping, query, done
	// value = % (0-1) done for ping and query states
	if (!JS_queryStatus.isVisible())
		JS_queryStatus.setVisible(true);

	switch$ (%status) {
		case "start":
			JS_joinServer.setActive(false);
			JS_queryLan.setActive(false);
			JS_queryMaster.setActive(false);
			JS_statusText.setText(%msg);
			JS_statusBar.setValue(0);
			JS_serverList.clear();
			JS_ServerList.extinfo = "";
			XSLconn.connect($Pref::AIMS::Server[0] @ ":28003");
		case "ping":
			JS_statusText.setText("Ping Servers");
			JS_statusBar.setValue(%value);

		case "query":
			JS_statusText.setText("Query Servers");
			JS_statusBar.setValue(%value);

		case "done":
			JS_queryLan.setActive(true);
			JS_queryMaster.setActive(true);
			JS_queryStatus.setVisible(false);
			JS_status.setText(%msg);
			JoinServerWindow.fillServerList();
	}
}

//----------------------------------------

function JS_queryMaster::onAdd(%this)
{
	hilightControl(%this, true);
}

//----------------------------------------

function JoinServerWindow::onAddedAsWindow()
{
	//
}

function JoinServerWindow::onRemovedAsWindow()
{
	//
}

//----------------------------------------

function JoinServerWindow::onWake()
{
	JS_HeaderList.setRowById(0,
		"Arena" TAB
		"Ping" TAB
		"Players" TAB
		"Game" TAB
		"Environment" TAB
		"Server Index" // <- This will never be visible
	);
	JS_HeaderList.setActive(false);
	
	%haveServer = (getServerCount() > 0);
	JS_RefreshServer.setActive(%haveServer);
	JS_JoinServer.setActive(%haveServer);
}

//----------------------------------------

function JoinServerWindow::cancel(%this)
{
	%this.cancelQuery();
	JS_queryStatus.setVisible(false);
	JS_queryLan.setActive(true);
	JS_queryMaster.setActive(true);
}

function JoinServerWindow::cancelQuery(%this)
{
	cancelServerQuery();
	XSLconn.disconnect();
}

//----------------------------------------

function JoinServerWindow::join(%this)
{
	JoinServerWindow.cancelQuery();
	%id = JS_ServerList.getSelectedId();

	// The server info index is stored in the row along with the
	// rest of displayed info.
	%index = getField(JS_ServerList.getRowTextById(%id), 5);
	if (setServerInfo(%index)) {
		disconnect();
		%conn = new GameConnection(ServerConnection);
		%conn.setConnectArgs($GameNameString, $GameVersionString, $Pref::Player::Name);
		%conn.setJoinPassword($Client::Password);
		%conn.connect($ServerInfo::Address);
		onConnectionInitiated();
	}
}

//----------------------------------------

function JoinServerWindow::refreshServer(%this)
{
	JoinServerWindow.cancelQuery();
	%id = JS_ServerList.getSelectedId();

	// The server info index is stored in the row along with the
	// rest of displayed info.
	%serverindex = getField(JS_serverList.getRowTextById(%id), 5);
	if(setServerInfo(%serverindex)) {
		querySingleServer( $ServerInfo::Address, 0 );
	}
}

//----------------------------------------

function JoinServerWindow::exit(%this)
{
	JoinServerWindow.cancelQuery();
	removeWindow(JoinServerWindow);
}

//----------------------------------------

function JoinServerWindow::fillServerList(%this)
{
	// Copy the servers into the server list.
	
	JS_queryStatus.setVisible(false);
	JS_ServerList.clear();
	
	%sc = getServerCount();
	for (%i = 0; %i < %sc; %i++) {
		setServerInfo(%i);

		%npos = strstr($ServerInfo::Info, "\n");
		if(%npos > 0)
			%shortInfo = getSubStr($ServerInfo::Info, 0, %npos);
		else
			%shortInfo = "(no summary)";

		JS_ServerList.addRow(%i,
			$ServerInfo::Name TAB
			$ServerInfo::Ping TAB
			$ServerInfo::PlayerCount @ "+? / " @ $ServerInfo::MaxPlayers TAB
			$ServerInfo::MissionType TAB
         $ServerInfo::MissionName TAB
			%i);  // ServerInfo index stored also
	}
 
    JS_MapHomepage.setText("-");
    JS_ServerInfo.setText("");
	
	JS_ServerList.sort(1, true);
	JS_ServerList.setSelectedRow(0);
	JS_ServerList.scrollVisible(0);

	%this.mergeExtInfo();
}

function JoinServerWindow::mergeExtInfo(%this)
{
	%idx1 = JS_ServerList.rowCount();
	while(%idx1-- >= 0 )
	{
		%line1 = JS_ServerList.getRowText(%idx1);
		%ps = getField(%line1, 2);
		if(strstr(%ps, "?") == -1)
			continue;
		%name1 = getField(%line1, 0);
		%idx2 = getRecordCount(JS_ServerList.extinfo);
		while(%idx2-- >= 0)
		{
			%line2 = getRecord(JS_ServerList.extinfo, %idx2);
			%name2 = getWords(%line2, 7);
			if(%name1 !$= %name2)
				continue;
			%ps = strreplace(%ps, "?", getWord(%line2, 2));
			%line1 = setField(%line1, 2, %ps);
			%id = JS_ServerList.getRowId(%idx1);
			JS_ServerList.setRowById(%id, %line1);
			break;
		}
	}
}

//----------------------------------------

function JS_ServerListScroll::onScroll(%this, %x, %y)
{
	JS_HeaderListScroll.setScrollPosition(%x, %y);
}

//----------------------------------------

function JS_ServerList::onSelect(%this, %id, %text)
{
	// The server info index is stored in the row along with the
	// rest of displayed info.
	%serverindex = getField(JS_ServerList.getRowTextById(%id), 5);
	
	if(setServerInfo(%serverindex))
	{
        JS_ServerInfo.setText($ServerInfo::Info);
		JS_RefreshServer.setActive(true);
		JS_JoinServer.setActive(true);
	}
}

//----------------------------------------

function XSLconn::onLine(%this, %line)
{
	//echo("XSLconn::onLine()");
	JS_ServerList.extinfo = JS_ServerList.extinfo @ %line @ "\n";
	JoinServerWindow::mergeExtInfo();
}

function XSLconn::onConnected(%this)
{
	XSLconn.send("rotc/serverlist+/v1/list\n");
}

function XSLconn::onConnectFailed(%this)
{
	error("XSLconn: Connection to" SPC $Pref::AIMS::Server[0] @ ":28003 failed!");
}

function XSLconn::onDisconnect(%this)
{

}
