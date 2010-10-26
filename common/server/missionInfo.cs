//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Server side of the scripted mission info system.
//------------------------------------------------------------------------------

function MissionInfo::readLinesIntoArray(%fileName, %array)
{
	%file = new FileObject();
	%file.openForRead(filePath($MissionInfo::File) @ "/" @ %fileName);
	%line = 0;
	while(!%file.isEOF())
	{
		%line++;
		%array.push_back(%line, strreplace(%file.readLine(), "<br>", "\n"));
	}
	%file.delete();
}

function MissionInfo::load(%missionFile)
{
	$MissionInfo::File = %missionFile;

	$MissionInfo::Name       = "";
	$MissionInfo::EnvFile    = "";
	$MissionInfo::ScriptFile = "";
	$MissionInfo::Desc       = "";

	exec(%missionFile);
	
	if(isObject($MissionInfo::Desc)) $MissionInfo::Desc.delete();
	if(isObject($MissionInfo::Copyright)) $MissionInfo::Copyright.delete();
	if(isObject($MissionInfo::License)) $MissionInfo::License.delete();
	if(isObject($MissionInfo::Credits)) $MissionInfo::Credits.delete();

	$MissionInfo::Desc = new Array();
	$MissionInfo::Copyright = new Array();
	$MissionInfo::License = new Array();
	$MissionInfo::Credits = new Array();
	
	if($MissionInfo::DescFile !$= "")
		MissionInfo::readLinesIntoArray($MissionInfo::DescFile, $MissionInfo::Desc);
		
	if($MissionInfo::CopyrightFile !$= "")
		MissionInfo::readLinesIntoArray($MissionInfo::CopyrightFile, $MissionInfo::Copyright);
		
	if($MissionInfo::LicenseFile !$= "")
		MissionInfo::readLinesIntoArray($MissionInfo::LicenseFile, $MissionInfo::License);

	if($MissionInfo::CreditsFile !$= "")
		MissionInfo::readLinesIntoArray($MissionInfo::CreditsFile, $MissionInfo::Credits);
}

function MissionInfo::sendToClient(%client)
{
	messageClient( %client, 'MsgMissionInfoBasics', "",
		$MissionInfo::Name, $MissionInfo::Homepage);
		
	for( %i = 0; %i < $MissionInfo::Desc.count(); %i++ )
		messageClient( %client, 'MsgMapDesc', "", $MissionInfo::Desc.getValue(%i));

	for( %i = 0; %i < $MissionInfo::Copyright.count(); %i++ )
		messageClient( %client, 'MsgMapCopyright', "", $MissionInfo::Copyright.getValue(%i));

	for( %i = 0; %i < $MissionInfo::License.count(); %i++ )
		messageClient( %client, 'MsgMapLicense', "", $MissionInfo::License.getValue(%i));

	for( %i = 0; %i < $MissionInfo::Credits.count(); %i++ )
		messageClient( %client, 'MsgMapCredits', "", $MissionInfo::Credits.getValue(%i));

	messageClient( %client, 'MsgMissionInfoDone' );
}
