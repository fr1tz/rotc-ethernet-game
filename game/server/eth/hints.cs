//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function constructHints(%hintsFile)
{
   if(isObject($Hints))
   {
      while($Hints.count() > 0)
      {
         %obj = $Hints.getValue($Hints.count() - 1);
         %obj.delete();
         $Hints.pop_back();
      }
      $Hints.delete();
   }

   $Hints = new Array();
   MissionCleanup.add($Hints);

   %hint = "";
	%file = new FileObject();
	%file.openForRead(%hintsFile);
	while(!%file.isEOF())
   {
      %line = %file.readLine();
      %hint = %hint @ %line @ " ";

      if(%line $= "" || %file.isEOF())
      {
         if(getSubStr(%hint,0,1) !$= " ")
         {
            //error("Found hint:" SPC %hint);
            %obj = new ScriptObject();
            MissionCleanup.add(%obj);
            %obj.text = %hint;
            $Hints.push_back("", %obj);
            %hint = "";
         }
      }
   }
	%file.delete();
}

function getRandomHint()
{
   if($Hints.count() == 0)
      return;
   %rand = getRandom($Hints.count()-1);
   %obj = $Hints.getValue(%rand);
   return %obj.text;
}

