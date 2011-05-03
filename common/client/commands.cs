//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Cookies

function clientCmdCookie(%name, %value)
{
	echo("Got cookie!");
	echo("Name:" SPC %name);
	echo("Value:" SPC %value);

	$Pref::Cookie_[%name] = %value;
}

function clientCmdCookieRequest(%name)
{
	%value = $Pref::Cookie_[%name];
	commandToServer('Cookie', %name, %value);
}
