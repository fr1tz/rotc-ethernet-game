//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//----------------------------------------

function StartServerWindow::onAddedAsWindow()
{
	SS_ServerInfo.setText($Pref::Server::Info);

	SS_missionList.clear();
	
	%i = 0;
	for(%file = findFirstFile($Server::MissionFileSpec); %file !$= ""; %file = findNextFile($Server::MissionFileSpec))
	{
		if(strStr(%file, "CVS/") == 0 || strStr(%file, "common/") == 0)
			continue;
			
		MissionInfo::load(%file);
			
		SS_missionList.addRow(%i++, 
			$MissionInfo::Type SPC
			$MissionInfo::Name @
			"\t" @ $MissionInfo::File );
	}
	
	SS_missionList.sort(0);
	SS_missionList.setSelectedRow(0);
	SS_missionList.scrollVisible(0);

	SS_NoMaps_Text.setText("<just:center>No maps available.");
	SS_NoMaps_Text.setVisible(%i == 0);
	SS_StartMissionButton.setActive(%i != 0);
	SS_ShowMapInfoButton.setActive(%i != 0);
}

function StartMissionWindow::onRemovedAsWindow()
{
	//
}

//----------------------------------------

function SS_missionList::onSelect(%this, %id, %text)
{
	%id = SS_missionList.getSelectedId();
	%missionFile = getField(SS_missionList.getRowTextById(%id), 1);
	MissionInfo::load(%missionFile);
	SS_EnvironmentInfo.setText($MissionInfo::Desc);
}

//----------------------------------------

function SS_StartMission()
{
	%id = SS_missionList.getSelectedId();
	
	%mapInfoFile = getField(SS_missionList.getRowTextById(%id), 1);

	if ($pref::HostMultiPlayer)
		%serverType = "MultiPlayer";
	else
		%serverType = "SinglePlayer";
		
	$Pref::Server::Info = SS_ServerInfo.getText();

	disconnect();
	createServer(%serverType, %mapInfoFile);
	%conn = new GameConnection(ServerConnection);
	RootGroup.add(ServerConnection);
	%conn.setConnectArgs($GameNameString, $GameVersionString, $Pref::Player::Name);
	%conn.setJoinPassword($Client::Password);
	%conn.connectLocal();
	onConnectionInitiated();
}


