//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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

	// Update environment description
	SS_EnvironmentInfo.setText($MissionInfo::Desc);

	// Update available mutators list
	SS_AvailableMutatorsList.clear();
	for(%i = 0; %i < getRecordCount($MissionInfo::MutatorDesc); %i++)
	{
		%line = getRecord($MissionInfo::MutatorDesc, %i);
		SS_AvailableMutatorsList.addRow(%i, %line);
	}

	// Update active mutators list
	for(%i = SS_ActiveMutatorsList.rowCount()-1; %i >= 0; %i--)
	{
		%line1 = SS_ActiveMutatorsList.getRowText(%i);
		%str1 = getField(%line1, 0);
		%keep = false;
		for(%j = 0; %j < SS_AvailableMutatorsList.rowCount(); %j++)
		{
			%line2 = SS_AvailableMutatorsList.getRowText(%j);
			%str2 = getField(%line2, 0);
			if(%str2 $= %str1)
			{
				%keep = true;
				break;
			}
		}
		if(!%keep)
			SS_ActiveMutatorsList.removeRow(%i);
	}
}

//----------------------------------------

function SS_AddMutator()
{
	%r = SS_AvailableMutatorsList.getSelectedRow();
	if(%r == -1)
		return;
	%line = SS_AvailableMutatorsList.getRowText(%r);
	%str = getField(%line, 0);

	%rc = SS_ActiveMutatorsList.rowCount();
	for(%i = 0; %i < SS_ActiveMutatorsList.rowCount(); %i++)
	{
		%line2 = SS_ActiveMutatorsList.getRowText(%i);
		%str2 = getField(%line2, 0);
		if(%str $= %str2)
			return;
	}

	SS_ActiveMutatorsList.addRow(%rc, %line);
}

function SS_RemoveMutator()
{
	%r = SS_ActiveMutatorsList.getSelectedRow();
	if(%r == -1)
		return;
	SS_ActiveMutatorsList.removeRow(%r);
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

	$Pref::Server::Mutators = "";
	for(%i = 0; %i < SS_ActiveMutatorsList.rowCount(); %i++)
	{
		%str = getField(SS_ActiveMutatorsList.getRowText(%i), 0);
		$Pref::Server::Mutators = $Pref::Server::Mutators SPC %str;
	}

	disconnect();
	createServer(%serverType, %mapInfoFile);
	%conn = new GameConnection(ServerConnection);
	RootGroup.add(ServerConnection);
	%conn.setConnectArgs($GameNameString, $GameVersionString, $Pref::Player::Name);
	%conn.setJoinPassword($Client::Password);
	%conn.connectLocal();
	onConnectionInitiated();
}


