//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function constructManual(%indexFile)
{
   if(isObject($Manual))
   {
      while($Manual.count() > 0)
      {
         %obj = $Manual.getValue($Manual.count() - 1);
         %obj.delete();
         $Manual.pop_back();
      }
      $Manual.delete();
   }

   $Manual = new Array();
   MissionCleanup.add($Manual);

	%file = new FileObject();
	%file.openForRead(%indexFile);
	while(!%file.isEOF())
   {
      %line = %file.readLine();

      %obj = new ScriptObject();
      MissionCleanup.add(%obj);

      %obj.size = 40;
      %obj.page = getField(%line, 0);
      if(getSubStr(getField(%line,1),0,1) $= "/")
         %obj.file = getSubStr(getField(%line,1), 1, 255) @ ".rml";
      else
         %obj.file = "game/server/eth/help/" @ getField(%line,1) @ ".rml";
      %obj.name = getFields(%line, 2, getFieldCount(%line)-1);

      %obj.text = "";
      %textfile = new FileObject();
      %textfile.openForRead(%obj.file);
      %meta = true;
      while(!%textfile.isEOF())
      {
         %line = %textfile.readLine();
         if(%meta && getSubStr(%line,0,1) $= "#")
         {
            if(getWord(%line, 0) $= "#icon")
               %obj.icon = getWord(%line, 1);
            else if(getWord(%line, 0) $= "#size")
               %obj.size = getWord(%line, 1);
         }
         else
         {
            %meta = false;
            %obj.text =%obj.text @ strreplace(%line, "<br>", "\n") @ "\n";
         }
      }
      %textfile.delete();

      $Manual.push_back(%obj.page, %obj);
   }
	%file.delete();
}

function getManualPage(%page)
{
   %idx = $Manual.getIndexFromKey(%page);
   return $Manual.getValue(%idx);
}

function getManualPageByIndex(%idx)
{
   if(%idx >= $Manual.count())
      return "";
   if(%idx < 0)
      return "";
   return $Manual.getValue(%idx);
}

function getManualPageIndex(%page)
{
   %idx = $Manual.getIndexFromKey(%page);
   return %idx;
}

function showManualPage(%client, %page)
{
	%newtxt = om_init();
	%client.beginMenuText();

	if(%page $= "")
		%page = 0;

	%newtxt = %newtxt @
		om_head(%client, $Game::GameTypeString SPC "Manual") @
		"<just:center>";

   %idx = getManualPageIndex(%page);
   if(%idx < 0)
      %idx = 0;

   %previdx = %idx - 1;
   %nextidx = %idx + 1;

   %p = getManualPageByIndex(%previdx);
	if(%p !$= "")
	{
		%prev = "<a:cmd Manual" SPC %p.page @ ">" @
			%p.page SPC %p.name @ "</a>";
	}
	else
		%prev = "";

   %p = getManualPageByIndex(%nextidx);
	if(%p !$= "")
	{
      %p = getManualPageByIndex(%nextidx);
		%next = "<a:cmd Manual" SPC %p.page @ ">" @
			%p.page SPC %p.name @ "</a>";
	}
	else
		%next = "";

	%nav = "<spush><font:NovaSquare:13>";
	if(%prev !$= "")
		%nav = %nav @ "Prev:" SPC %prev SPC "|";
	%nav = %nav SPC "<a:cmd Manual 0>Index</a>";
	if(%next !$= "")
		%nav = %nav SPC "| Next:" SPC %next;
	%nav = %nav @ "<spop>";

   %p = getManualPageByIndex(%idx);

	%newtxt = %newtxt @ %nav @ "\n\n" @
		"<spush><font:NovaSquare:24>" @ %page @ "." SPC %p.name @
      "<spop><just:left><lmargin:5><rmargin:480>\n\n";

   if(%idx == 0)
   {
      for(%i = 1; %i < $Manual.count(); %i++)
      {
         %p = getManualPageByIndex(%i);
         %spc = "";
         if(strlen(%p.page) > 1)
            %spc = "   ";
         %icon = "";
         if(%p.icon !$= "")
            %icon = "<bitmap:" @ %p.icon @ ">";
         %link = "<a:cmd Manual" SPC %p.page @ ">" @
			   %p.page SPC %p.name @ "</a>";
         %newtxt = %newtxt @ %spc @ %link SPC %icon @ "\n\n";
      }
   }
   else
   {
      %newtxt = %newtxt @ %p.text;
   }

	%newtxt = %newtxt @ "\n<just:center>" @ %nav @ "\n";

	%client.addMenuText(%newtxt);
	%client.endMenuText(%newtxt);

	%client.menu = "manual" @ %p.name;
}
