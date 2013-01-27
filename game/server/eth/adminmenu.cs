//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function AdminMenu_Link(%text, %client, %arg1, %arg2, %arg3, %arg4)
{
   if(!%client.isAdmin)
      return "[" @ %text @ "]";

   if(%arg1 $= "") %arg1 = 0;
   if(%arg2 $= "") %arg2 = 0;
   if(%arg3 $= "") %arg3 = 0;
   if(%arg4 $= "") %arg4 = 0;
   %arg = %arg1 @"/"@ %arg2 @"/"@ %arg3 @"/"@ %arg4;
   return "[<a:cmd Admin " @ %arg @ ">" @ %text @ "</a>]";
}

function GameConnection::showAdminMenu(%this)
{
	%L3 = om_init();
	%L3 = %L3 @ om_head(%this, "Arena Admin");

   if(!%this.isAdmin)
   {
   	%L3 = %L3 @ "<just:center><spush><font:NovaSquare:24><color:AA0000>";
   	%L3 = %L3 @ "You are not an admin!";
   	%L3 = %L3 @ "<spop><just:left>\n\n";
   }

	%L3 = %L3 @ "Target Practice Bots:\n\n<lmargin:20>";
	%L3 = %L3 @ "Add Red Bot:\n<lmargin:40>";
   for(%i = 1; %i <= 5; %i++)
   	%L3 = %L3 @ AdminMenu_Link(LoadoutMenu_getWeaponName(%i), %this, 1, %i);
	%L3 = %L3 @ "\n<lmargin:20>Add Blue Bot:\n<lmargin:40>";
   for(%i = 1; %i <= 5; %i++)
   	%L3 = %L3 @ AdminMenu_Link(LoadoutMenu_getWeaponName(%i), %this, 2, %i);
	%L3 = %L3 @ "\n<lmargin:20>Bots:\n<lmargin:40>";
   %L3 = %L3 @ AdminMenu_Link("Start Moving", %this, 3, 1);
   %L3 = %L3 @ AdminMenu_Link("Start Firing", %this, 3, 2);
   %L3 = %L3 @ AdminMenu_Link("Start Fighting", %this, 3, 3);
   %L3 = %L3 @ AdminMenu_Link("Remove", %this, 3, 4);

	%this.beginMenuText(%this.menu $= "admin");
   if(%L1 !$= "") %this.addMenuText(%L1, 1);
	if(%L2 !$= "") %this.addMenuText(%L2, 2);
	if(%L3 !$= "") %this.addMenuText(%L3, 4);
	if(%L4 !$= "") %this.addMenuText(%L4, 8);
	%this.endMenuText();
}



