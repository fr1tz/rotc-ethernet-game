//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

$IRC::MsgType::Status = 0;
$IRC::MsgType::Talk   = 1;
$IRC::MsgType::Topic  = 2;
$IRC::MsgType::Users  = 3;

$IRC::Colors = "" @
	"BB0000" SPC "FF0000" SPC "00BB00" SPC "00FF00" SPC
	"0000BB" SPC "0000FF" SPC "BB4400" SPC "FFBB00" SPC
	"44BB00" SPC "BBFF00" SPC "00BB44" SPC "00FFBB" SPC
	"BB00BB" SPC "FF00FF" SPC "0044BB" SPC "00BBFF";
$IRC::NumColors = getWordCount($IRC::Colors);

if(!isObject(IRCconn))
{
	new TCPObject(IRCconn);
	$IRC::Offline = true;
}

if(!isObject($IRC::Names))
	$IRC::Names = new Array();

function irc_extract_user(%prefix)
{
	%idx = strpos(%prefix, "!", 0);
	if(%idx == -1) return %prefix;
	return getSubStr(%prefix, 0, %idx);
}

function irc_shorten_name(%name, %nocolor)
{
	%chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVXYZ-_";

	%c = 0;
	%n = strlen(%name);
	for(%i = 0; %i < %n; %i++)
	{
		%value = strpos(%chars, getSubStr(%name, %i, 1));
		if(%value >= 0)
			%c += %i * %value;
	}

	%name = strreplace(%name, "_rotc_", " [rotc]");

	if(%nocolor)
		return %name;

	%color = getWord($IRC::Colors, %c % $IRC::NumColors);
	return "<spush><color:" @ %color @ ">" @ %name @ "<spop>";
}

function irc_after_colon(%string)
{
	%idx = strpos(%string, ":");
	if(%idx == -1)
		return %string;
	return getSubStr(%string, %idx+1, 512);
}

function irc_connect(%this)
{
	$IRC::Offline = false;
	$IRC::Nick = $Pref::IRC::Name @ "_rotc_";
	$IRC::Server = $Pref::IRC::Server;
	$IRC::Channel = $Pref::IRC::Channel;

	IrcNickname.setActive(false);
	IrcConnectButton.setActive(false);
	irc_set_status("Connecting to" SPC $IRC::Server @ "...");

	IRCconn.connect($IRC::Server @ ":6667");
}

function irc_disconnect(%error)
{
	if($IRC::Offline)
		return;

	IRCconn.disconnect();

	irc_set_status(%error);

	IrcNickname.setActive(true);
	IrcConnectButton.setActive(true);
	IrcConnect.setVisible(true);

	$IRC::Offline = true;
}

function irc_set_status(%string)
{
	%text = "[" @ %string @ "]";
	IrcText.addLine(%text, %text, $IRC::MsgType::Status);
	IrcStatus.setText("<just:center>" @ %string);
}

function irc_process_line(%line)
{
	//error("IRC msg:" SPC %line);
	
	if(getSubStr(%line, 0, 1) $= ":")
	{
		%prefix = getSubStr(%line, 1, strlen(getWord(%line,0)) - 1);
		%command = getWord(%line, 1);
		%params = getWords(%line, 2);
	}
	else
	{
		%prefix = "";
		%command = getWord(%line, 0);
		%params = getWords(%line, 1);
	}

	if(%command $= "PRIVMSG")
	{
		%usr = irc_extract_user(%prefix);
		if(getWord(%params, 0) !$= $IRC::Channel)
		{
			irc_send("PRIVMSG" SPC %usr SPC ":(PRIVATE MESSAGE IGNORED)");
			return;
		}

		%msg = getWords(%params, 1);
		%msg = irc_after_colon(%msg);

		irc_talk(%usr, %msg);
	}
	else if(%command $= "PING")
	{
		irc_send("PONG" SPC %params);
	}
	else if(%command $= "NICK")
	{
		%usr = irc_extract_user(%prefix);
		%nick = irc_after_colon(%params);

		%mltext = "*" SPC irc_shorten_name(%usr) SPC 
			"is now known as" SPC irc_shorten_name(%nick);
		%rawtext = "*" SPC irc_shorten_name(%usr, true) SPC 
			"is now known as" SPC irc_shorten_name(%nick, true);
		IrcText.addLine(%mltext, %rawtext, $IRC::MsgType::Users);

		$IRC::Names.moveFirst();
		%idx = $IRC::Names.getIndexFromValue(%usr);
		if(%idx != -1)
			$IRC::Names.erase(%idx);
		$IRC::Names.push_back("", %nick);
		IrcNames.update();
	}
	else if(%command $= "JOIN")
	{
		%usr = irc_extract_user(%prefix);
		if(%usr $= $IRC::Nick)
		{
			irc_set_status("Now talking in" SPC $IRC::Channel);
			IrcConnect.setVisible(false);		
		}
		else
		{
			%mltext = "*" SPC irc_shorten_name(%usr) SPC 
				"has joined global player chat";
			%rawtext = "*" SPC irc_shorten_name(%usr, true) SPC 
				"has joined global player chat";			
			IrcText.addLine(%mltext, %rawtext, $IRC::MsgType::Users);

			$IRC::Names.push_back("", %usr);
			IrcNames.update();
		}
	}
	else if(%command $= "PART" || %command $= "QUIT")
	{
		%usr = irc_extract_user(%prefix);

		%mltext = "*" SPC irc_shorten_name(%usr) SPC 
			"has left global player chat";
		%rawtext = "*" SPC irc_shorten_name(%usr, true) SPC 
			"has left global player chat";			
		IrcText.addLine(%mltext, %rawtext, $IRC::MsgType::Users);

		$IRC::Names.moveFirst();
		%idx = $IRC::Names.getIndexFromValue(%usr);
		if(%idx != -1)
		{
			$IRC::Names.erase(%idx);
			IrcNames.update();
		}
	}
	else if(%command $= "353") // RPL_NAMREPLY
	{
		$IRC::Names.delete();
		$IRC::Names = new Array();
		%idx = strpos(%params, ":", 0);
		if(%idx == -1) return;
		%names = getSubStr(%params, %idx + 1, 512);
		%n = getWordCount(%names);
		for(%i = 0; %i < %n; %i++)
		{
			%name = getWord(%names, %i);
			$IRC::Names.push_back("", %name);
		}
		IrcNames.update();
	}
	else if(%command $= "332") // RPL_TOPIC
	{
		%topic = irc_after_colon(%params);
		%text = "* The topic is:" SPC %topic;
		IrcText.addLine(%text, %text, $IRC::MsgType::Topic);
	}
	else if(%command >= 431 && %command <= 436)
	{
		irc_disconnect("Pick another nickname (Error" SPC 
			%command SPC irc_after_colon(%params) @ ")");
	}
	else if(%command >= 400 && %command <= 499)
	{
		irc_disconnect("Error! (Error " SPC 
			%command SPC irc_after_colon(%params) @ ")");
	}
	else
	{
		echo("Ignored IRC msg:" SPC %line);
	}
}

function irc_send(%line)
{
	// error("IRC send:" SPC %line);
	IRCconn.send(%line @ "\n");
}

function irc_talk(%usr, %msg)
{
	%msg = strreplace(%msg, "_rotc_", " [rotc]");

	%mltext = "<" @ irc_shorten_name(%usr) @ ">" SPC %msg;
	if(strpos(%msg, $Pref::Irc::Name, 0) != -1)
		%mltext = "<spush><shadowcolor:00ff00><shadow:1:1>" @ %mltext @ "<spop>";

	%rawtext = "<" @ irc_shorten_name(%usr, true) @ ">" SPC %msg;

	IrcText.addLine(%mltext, %rawtext, $IRC::MsgType::Talk);
}

function irc_grab_attention()
{
	%txt = "[" SPC
	$IRC::Names.sorta();
	%idx = $IRC::Names.moveFirst();
	while(%idx != -1)
	{
		%k = $IRC::Names.getKey(%idx);
		%v = $IRC::Names.getValue(%idx);
		
		%txt = %txt SPC %v;

		%idx = $IRC::Names.moveNext();
	}
	%txt = %txt SPC "]";
	IrcSend.setText(IrcSend.getText() @ %txt);
}

function irc_ask_for_game()
{
	%txt = "[" SPC
	$IRC::Names.sorta();
	%idx = $IRC::Names.moveFirst();
	while(%idx != -1)
	{
		%k = $IRC::Names.getKey(%idx);
		%v = $IRC::Names.getValue(%idx);

		%txt = %txt SPC %v;

		%idx = $IRC::Names.moveNext();
	}
	%txt = %txt SPC "]";
	IrcSend.setText("Anyone up for a game?" SPC %txt);
	IrcSend.onReturn();

	%text = IrcAskForGameButton.getText();
	IrcAskForGameButton.setActive(false);
	IrcAskForGameButton.setText("Please wait...");
	IrcAskForGameButton.schedule(60000, "setActive", true);
	IrcAskForGameButton.schedule(60000, "setText", %text);
}

//------------------------------------------------------------------------------

function IRCconn::onLine(%this, %line)
{
	irc_process_line(%line);
}

function IRCconn::onConnected(%this)
{
	irc_set_status("Connected to" SPC $IRC::Server);
	irc_set_status("Joining channel" SPC $IRC::Channel @ "...");

	irc_send("USER" SPC $IRC::Nick SPC "x" SPC $IRC::Server SPC $IRC::Nick);
	irc_send("NICK" SPC $IRC::Nick);
	irc_send("JOIN" SPC $IRC::Channel);
}

function IRCconn::onConnectFailed(%this)
{
	irc_disconnect("Error: Connect failed");
}

function IRCconn::onDisconnect(%this)
{
	irc_disconnect("Error: Disconnected");
}

//----------------------------------------------------------------------------

function IrcSend::onReturn(%this)
{
	%text = strreplace(IrcSend.getText(), " [rotc]", "_rotc_");
	irc_send("PRIVMSG" SPC $IRC::Channel SPC ":" @ %text);
	irc_talk($IRC::Nick, %text);
	IrcSend.setText("");
}

function IrcText::addLine(%this, %mltext, %rawtext, %type)
{
	%storX = IrcTextScroll.getScrollPositionX();
	%storY = IrcTextScroll.getScrollPositionY();
	IrcTextScroll.scrollToBottom();
	%atBottom = IrcTextScroll.getScrollPositionY() == %storY;

	IrcText.addText(%mltext @ "\n", false);
	IrcText.forceReflow();

	if(%atBottom)
		IrcTextScroll.scrollToBottom();
	else
		IrcTextScroll.setScrollPosition(%storX, %storY);

	if((%type == $IRC::MsgType::Status && $Pref::Irc::ToChat::Status) ||
	   (%type == $IRC::MsgType::Talk && $Pref::Irc::ToChat::Talk) ||
	   (%type == $IRC::MsgType::Topic && $Pref::Irc::ToChat::Topic) ||
	   (%type == $IRC::MsgType::Users && $Pref::Irc::ToChat::Users ))
	{
		ChatHud.addLine("\c5" SPC %rawtext);
	}
}

function IrcNames::addLine(%this, %text)
{
	%storX = IrcNamesScroll.getScrollPositionX();
	%storY = IrcNamesScroll.getScrollPositionY();
	IrcNamesScroll.scrollToBottom();
	%atBottom = IrcNamesScroll.getScrollPositionY() == %storY;

	IrcNames.addText(%text @ "\n", false);
	IrcNames.forceReflow();

	if(%atBottom)
		IrcNamesScroll.scrollToBottom();
	else
		IrcNamesScroll.setScrollPosition(%storX, %storY);

}

function IrcNames::update(%this)
{
	IrcNames.setText("");
	$IRC::Names.sorta();
	%idx = $IRC::Names.moveFirst();
	while(%idx != -1)
	{
		%k = $IRC::Names.getKey(%idx);
		%v = $IRC::Names.getValue(%idx);
		
		IrcNames.addLine(irc_shorten_name(%v));

		%idx = $IRC::Names.moveNext();
	}
}

function IrcTextScroll::onWake(%this)
{
	%this.schedule(0, "setScrollPosition", %this.zPrevPosX, %this.zPrevPosY);	
}

function IrcTextScroll::onSleep(%this)
{
	%this.zPrevPosX = %this.getScrollPositionX();
	%this.zPrevPosY = %this.getScrollPositionY();  
}
