//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Server side of the scripted map info system.
//------------------------------------------------------------------------------

function MapInfo::readLinesIntoArray(%fileName, %array)
{
	%file = new FileObject();
	%file.openForRead(filePath($MapInfo::File) @ "/" @ %fileName);
	%line = 0;
	while(!%file.isEOF())
	{
		%line++;
		%array.push_back(%line, strreplace(%file.readLine(), "<br>", "\n"));
	}
	%file.delete();
}

function MapInfo::load(%mapInfoFile)
{
	$MapInfo::File = %mapInfoFile;

	$MapInfo::Name = "unnamed";
	$MapInfo::Homepage = "no homepage";
	
	$MapInfo::MissionFile = "";
	$MapInfo::ScriptFile = "";
	$MapInfo::DescFile = "";
	$MapInfo::CopyrightFile = "";
	$MapInfo::LicenseFile = "";
	$MapInfo::CreditsFile = "";
	
	exec(%mapInfoFile);
	
	if(isObject($MapInfo::Desc)) $MapInfo::Desc.delete();
	if(isObject($MapInfo::Copyright)) $MapInfo::Copyright.delete();
	if(isObject($MapInfo::License)) $MapInfo::License.delete();
	if(isObject($MapInfo::Credits)) $MapInfo::Credits.delete();

	$MapInfo::Desc = new Array();
	$MapInfo::Copyright = new Array();
	$MapInfo::License = new Array();
	$MapInfo::Credits = new Array();
	
	if($MapInfo::DescFile !$= "")
		MapInfo::readLinesIntoArray($MapInfo::DescFile, $MapInfo::Desc);
		
	if($MapInfo::CopyrightFile !$= "")
		MapInfo::readLinesIntoArray($MapInfo::CopyrightFile, $MapInfo::Copyright);
		
	if($MapInfo::LicenseFile !$= "")
		MapInfo::readLinesIntoArray($MapInfo::LicenseFile, $MapInfo::License);

	if($MapInfo::CreditsFile !$= "")
		MapInfo::readLinesIntoArray($MapInfo::CreditsFile, $MapInfo::Credits);
}

function MapInfo::sendToClient(%client)
{
	messageClient( %client, 'MsgMapInfoBasics', "",
		$MapInfo::Name, $MapInfo::Homepage);
		
	for( %i = 0; %i < $MapInfo::Desc.count(); %i++ )
		messageClient( %client, 'MsgMapDesc', "", $MapInfo::Desc.getValue(%i));

	for( %i = 0; %i < $MapInfo::Copyright.count(); %i++ )
		messageClient( %client, 'MsgMapCopyright', "", $MapInfo::Copyright.getValue(%i));

	for( %i = 0; %i < $MapInfo::License.count(); %i++ )
		messageClient( %client, 'MsgMapLicense', "", $MapInfo::License.getValue(%i));

	for( %i = 0; %i < $MapInfo::Credits.count(); %i++ )
		messageClient( %client, 'MsgMapCredits', "", $MapInfo::Credits.getValue(%i));

	messageClient( %client, 'MsgMapInfoDone' );
}
