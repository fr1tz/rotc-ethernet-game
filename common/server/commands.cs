//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Misc. server commands avialable to clients
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

function serverCmdSAD( %client, %password )
{
	if( %password !$= "" && %password $= $Pref::Server::AdminPassword)
	{
		%client.isAdmin = true;
		%client.isSuperAdmin = true;
		%name = getTaggedString( %client.name );
		MessageAll( 'MsgAdminForce', "\c2" @ %name @ " has become Admin by force.", %client );	
	}
}

function serverCmdSADSetPassword(%client, %password)
{
	if(%client.isSuperAdmin)
		$Pref::Server::AdminPassword = %password;
}

//----------------------------------------------------------------------------
// Server chat message handlers

function serverCmdTeamMessageSent(%client, %text)
{
	if(strlen(%text) >= $Pref::Server::MaxChatLen)
		%text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
	chatMessageTeam(%client, %client.team, '\c4[team] \c3%1: %2', %client.name, %text);
}

function serverCmdMessageSent(%client, %text)
{
	if(strlen(%text) >= $Pref::Server::MaxChatLen)
		%text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
	chatMessageAll(%client, '\c4%1: %2', %client.name, %text);
}

//----------------------------------------------------------------------------
// Server commands wrapper

function serverCmdSimpleCommand(%client, %cmd, %arg)
{
	call("serverCmd" @ %cmd, %client, %arg);
}

//-----------------------------------------------------------------------------
// Cookies

function serverCmdCookie(%client, %name, %value)
{
   //error("serverCmdCookie:" SPC %name SPC %value);
   if(!isObject(%client.cookies)) return;
   arrayChangeElement(%client.cookies, %name, %value);
}

//-----------------------------------------------------------------------------
// Recordings

function serverCmdRecordingDemo(%client, %isRecording)
{
   %client.onRecordingDemo(%isRecording);
}

