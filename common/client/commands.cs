//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Cookies

function clientCmdCookie(%name, %value)
{
	echo("Got cookie!");
	echo("Name:" SPC %name);
	echo("Value:" SPC %value);

   // Make sure the server can't interject code into our prefs.cs file!!!
   %legal = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
   %len = strlen(%name);
   for(%i = 0; %i < %len; %i++)
   {
      %c = getSubStr(%name, %i, 1);

      if(strpos(%legal, %c) < 0)
      {
         error("Illegal cookie name! Contains" SPC %c);
         return;
      }
   }

	$Pref::Cookie_[%name] = %value;
}

function clientCmdCookieRequest(%name)
{
	%value = $Pref::Cookie_[%name];
   if(%value !$= "")
   	commandToServer('Cookie', %name, %value);
}
