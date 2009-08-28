//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

//----------------------------------------

function StartServerWindow::onAddedAsWindow()
{
	SS_ServerInfo.setText($Pref::Server::Info);

	SS_missionList.clear();
	
	%i = 0;
	for(%file = findFirstFile($Server::MapInfoFileSpec); %file !$= ""; %file = findNextFile($Server::MapInfoFileSpec))
	{
		if(strStr(%file, "CVS/") == 0 || strStr(%file, "common/") == 0)
			continue;
			
		MapInfo::load(%file);
			
		SS_missionList.addRow(%i++, $MapInfo::Name @ "\t" @ $MapInfo::File );
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
    SS_UpdateMapInfo();
}

//----------------------------------------

function SS_StartMission()
{
	%id = SS_missionList.getSelectedId();
	
	%mapInfoFile = getField(SS_missionList.getRowTextById(%id), 1);

	//$Server::MissionType = getWord(getField(SS_missionList.getRowTextById(%id), 0),0);

	if ($pref::HostMultiPlayer)
		%serverType = "MultiPlayer";
	else
		%serverType = "SinglePlayer";
		
	$Pref::Server::Info = SS_ServerInfo.getText();

	createServer(%serverType, %mapInfoFile);
	%conn = new GameConnection(ServerConnection);
	RootGroup.add(ServerConnection);
	%conn.setConnectArgs($pref::Player::Name);
	%conn.setJoinPassword($Client::Password);
	%conn.connectLocal();
	onConnectionInitiated();
}


//----------------------------------------

function SS_UpdateMapInfo()
{
	// this is kinda hackish...

	%id = SS_missionList.getSelectedId();

	%mapInfoFile = getField(SS_missionList.getRowTextById(%id), 1);
	MapInfo::load(%mapInfoFile);

	handleMapInfoBasicsMessage('MsgMapInfoBasics', "",
		$MapInfo::Name, $MapInfo::Homepage);

	for( %i = 0; %i < $MapInfo::Desc.count(); %i++ )
		handleMapDescMessage('MsgMapDesc', "", $MapInfo::Desc.getValue(%i));

	for( %i = 0; %i < $MapInfo::Copyright.count(); %i++ )
		handleMapCopyrightMessage('MsgMapCopyright', "", $MapInfo::Copyright.getValue(%i));

	for( %i = 0; %i < $MapInfo::License.count(); %i++ )
		handleMapLicenseMessage('MsgMapLicense', "", $MapInfo::License.getValue(%i));

	for( %i = 0; %i < $MapInfo::Credits.count(); %i++ )
		handleMapCreditsMessage('MsgMapCredits', "", $MapInfo::Credits.getValue(%i));

	handleMapInfoDoneMessage('MsgMapInfoDone');
}

function SS_ShowMapInfo()
{
    SS_UpdateMapInfo();
	addWindow(MapInfoWindow);
}
