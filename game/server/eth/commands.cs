//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function serverCmdTextInput(%client, %text)
{
   if(%client.textInputTargetName $= "LoadoutName")
   {
      %loadout = %client.textInputTargetArg[0];
      %client.loadoutName[%loadout] = %text;
      %client.sendCookie("ETH_LNAME" @ %loadout, %text);
      %client.displayInventory();
      serverCmdLoadout(%client, %loadout);
   }
   %client.textInputTargetName = "";
}

function serverCmdLoadout(%client, %str)
{
	%str = strreplace(%str, "/", " ");
	%arg1 = getWord(%str, 0);
	%arg2 = getWord(%str, 1);
	%arg3 = getWord(%str, 2);
	%arg4 = getWord(%str, 3);

   %loadout = %arg1;
   if(%loadout $= "")
   {
      %loadout = 1;
      %client.loadoutMenuExpandedSlot = 0;
   }
   else if(!(%loadout > 0 && %loadout <= 10))
      return;

   %showInfo = 0;
   %infoPos = 0;

   if(%arg2 $= "n")
   {
      %client.textInputTargetName = "LoadoutName";
      %client.textInputTargetArg[0] = %loadout;
      commandToClient(%client, 'RequestTextInput', "New name", %client.loadoutName[%loadout]);
   }
   else if(%arg2 $= "e")
   {
      %slot = %arg3;
      if(%slot >= 1 && %slot <= 3)
         %client.loadoutMenuExpandedSlot = %slot;
      else
         %client.loadoutMenuExpandedSlot = 0;
   }
   else if(%arg2 $= "s")
   {
      %slot = %arg3;
      %item = %arg4;
      if(%slot >= 1 && %slot <= 3)
      {
         if(%item >= 1 && %item <= 7)
         {
            %newCode = "";
            for(%i = 1; %i <= 3; %i++)
            {
               if(%i == %slot)
                  %c = %item;
               else
                  %c = getSubStr(%client.loadoutCode[%loadout], %i-1, 1);
               %newCode = %newCode @ %c;
            }
            %client.loadoutCode[%loadout] = %newCode;
            %client.sendCookie("ETH_LCODE" @ %loadout, %newCode);
            %client.loadoutMenuExpandedSlot = 0;
         }
      }
   }
   else if(%arg2 $= "i")
   {
      %showInfo = %arg3;
      %infoPos = %arg4;
   }

   %client.showLoadout(%loadout, %client.loadoutMenuExpandedSlot,
      %showInfo, %infoPos);

	%client.menu = "loadout";
}

function serverCmdTeams(%client)
{
   %client.showTeamsMenu();
	%client.menu = "teams";
}

function serverCmdAdmin(%client, %str)
{
	%str = strreplace(%str, "/", " ");
	%arg1 = getWord(%str, 0);
	%arg2 = getWord(%str, 1);
	%arg3 = getWord(%str, 2);
	%arg4 = getWord(%str, 3);

   if(%arg1 == 1)
   {
      aiAddRed(%arg2);
   }
   else if(%arg1 == 2)
   {
      aiAddBlue(%arg2);
   }
   else if(%arg1 == 3)
   {
      if(%arg2 == 1)
         aiStartMove();
      else if(%arg2 == 2)
         aiStartFire();
      else if(%arg2 == 3)
         aiStartFight();
      else if(%arg2 == 4)
         aiKill();
   }
   else
   {
      %client.showAdminMenu();
      %client.menu = "admin";
   }
}

function serverCmdHowToPlay(%client, %page)
{
   showManualPage(%client, %page);
}

function serverCmdManual(%client, %page)
{
   showManualPage(%client, %page);
}

