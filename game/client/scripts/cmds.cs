//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function clientCmdSkyColor(%color, %elementMask)
{
   if(!(%elementMask > 0))
      return;

	if($sky $= "")
		$sky = client_find_sky();

   %mask = 1;
   for(%i = 0; %i < 9; %i++)
   {
      if(%elementMask & %mask)
      	$sky.changeColor(%i, %color);
      %mask *= 2;
   }
}


function clientCmdCrosshair(%option, %arg1, %arg2, %arg3, %arg4, %arg5)
{
   Hud.setCrosshair(%option, %arg1, %arg2, %arg3, %arg4, %arg5);
}
