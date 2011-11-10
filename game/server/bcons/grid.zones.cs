//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

datablock TacticalZoneData(GridZone)
{
	//category = "Tactical Zones"; // for the mission editor

	// The period is value is used to control how often the console
	// onTick callback is called while there are any objects
	// in the zone.  The default value is 100 MS.
	tickPeriodMS = 60000;

	colorChangeTimeMS = 100;

	colors[0]  = "1.0 1.0 1.0 0.1"; 
	colors[1]  = "1.0 0.0 0.0 0.4"; 
	colors[2]  = "0.0 0.0 1.0 0.4"; 
	colors[3]  = "1.0 0.0 1.0 0.2"; 
	colors[4]  = "0.0 1.0 0.0 0.2"; 

	colors[5]  = "1.0 0.0 0.0 1.0"; 
	colors[6]  = "0.0 0.0 1.0 1.0"; 
	colors[7]  = "0.0 1.0 0.0 1.0"; 
	colors[8]  = "1.0 0.0 1.0 1.0";

	colors[9]  = "1.0 0.0 0.0 1.0";
	colors[10] = "0.0 0.0 1.0 1.0";

	colors[15] = "1.0 1.0 1.0 1.0"; 

    texture = "share/textures/rotc/zone.grid";

	// Script fields...
	isTerritoryZone = true;
};

function GameGrid_deleteZones()
{
	for( %idx = MissionCleanup.getCount()-1; %idx >= 0; %idx-- )
	{
		%obj = MissionCleanup.getObject(%idx);
		if(%obj.getType() & $TypeMasks::TacticalZoneObjectType
		&& %obj.getDataBlock().getName() $= "GridZone")
			%obj.delete();
	}
}

function GameGrid_createCurrChunkZones()
{
	// Maximum width/height of zones measured in game grid squares...
	%zoneMaxSize = 1024 / $Round.grid.squareSize;

	if($Round.grid.zi $= "")
		$Round.grid.zi = $Round.grid.currChunkFirst;

	$Round.grid.zc = 0;
	$Round.grid.zx = 0; $Round.grid.zy = 0;
	$Round.grid.zState = 0; // 0 = find start   1 = go right    2 = go down 
	$Round.grid.ziterations = $Round.grid.numSquares * 4;

	while($Round.grid.ziterations >= 0)
	{
		$Round.grid.ziterations--;

		%x = $Round.grid.zi % $Round.grid.width;
		%y = ($Round.grid.zi-%x) / $Round.grid.width;

		//error($Round.grid.zState @ ":" @ %x SPC %y);

		if($Round.grid.zState == 0)
		{
			if($Round.grid.zi > $Round.grid.currChunkLast)
			{
				$Round.grid.zi = "";
				return true;
			}

			if(!($Round.grid.squareF[$Round.grid.zi] & $Round.grid.FDone))
			{
				%color = $Round.grid.squareF[$Round.grid.zi] & $Round.grid.ColorMask;
				$Round.grid.zf = $Round.grid.zi;
				$Round.grid.zl = $Round.grid.zi;
				$Round.grid.zc = %color;
				$Round.grid.zx = %x;
				$Round.grid.zy = %y;
				$Round.grid.zxmax = %x + %zoneMaxSize;
				if($Round.grid.zxmax > $Round.grid.currChunkRight)
					$Round.grid.zxmax = $Round.grid.currChunkRight;
				$Round.grid.zState = 1;
			}
			else
			{
				if(%x >= $Round.grid.currChunkRight)
				{
					%x = $Round.grid.currChunkLeft;
					%y = %y + 1;
					$Round.grid.zi = %y*$Round.grid.width + %x;
				}
				else
					$Round.grid.zi++;
			}
		}
		else if($Round.grid.zState == 1)
		{
			%color = $Round.grid.squareF[$Round.grid.zi] & $Round.grid.ColorMask;
			%samecolor = (%color == $Round.grid.zc);
			%done = ($Round.grid.squareF[$Round.grid.zi] & $Round.grid.FDone);

			if(%samecolor && !%done)
				$Round.grid.zl = $Round.grid.zi;

			if(%x == $Round.grid.zxmax || !%samecolor || %done)
			{
				if(%samecolor && !%done)
					$Round.grid.zxmax = %x;	
				else
					$Round.grid.zxmax = %x-1;

				//error("xmax:" SPC $Round.grid.zxmax);
				%x = $Round.grid.zx;
				%y = %y + 1;
				$Round.grid.zi = %y*$Round.grid.width + %x;
				$Round.grid.zState = 2;
			}
			else
			{
				$Round.grid.zi++;
			}
		}
		else if($Round.grid.zState == 2)
		{
			%color = $Round.grid.squareF[$Round.grid.zi] & $Round.grid.ColorMask;
			%samecolor = (%color == $Round.grid.zc);
			%done = ($Round.grid.squareF[$Round.grid.zi] & $Round.grid.FDone);

			if($Round.grid.zi > $Round.grid.currChunkLast 
			|| %y > $Round.grid.currChunkBottom
			|| !%samecolor || %done)
			{
				GameGrid_createZones_dropZone();	
			}
			else
			{
				$Round.grid.zState = 1;
			}
		}
		else if($Round.grid.zState == 3)
		{
			return false;
		}
	}

	return false;
}

function GameGrid_createZones_dropZone()
{
	%zc = $Round.grid.zc;

	%zx = $Round.grid.zx;
	%zy = $Round.grid.zy; 

	%endx = $Round.grid.zl % $Round.grid.width;
	%endy = $Round.grid.zl >= $Round.grid.width ? ($Round.grid.zl-%endx) / $Round.grid.width : 0;

	%zw = %endx - %zx + 1;
	%zh = %endy - %zy + 1;

//	echo("GameGrid_createZones_dropZone():" SPC
//		%zx SPC %zy SPC %zw @ "x" @ %zh @ " : " @ %zc);

	%bl = 0; %bt = 0; %br = 0; %bb = 0;

	for(%i = %zy; %i < %zy + %zh; %i++)
	{
		for(%j = %zx; %j < %zx + %zw; %j++)
		{
			%idx = %i*$Round.grid.width + %j;
			$Round.grid.squareF[%idx] = $Round.grid.squareF[%idx] | $Round.grid.FDone;
		}
	}

	%idx = $Round.grid.zf;
	%flags = $Round.grid.squareF[%idx];

	$Round.grid.zi = $Round.grid.zf;
	$Round.grid.zState = 3;

	if(!(%flags & ($Round.grid.Red | $Round.grid.Green | $Round.grid.Blue)))
		return;

	if((%flags & $Round.grid.Red) && (%flags & $Round.grid.Blue))
		return;

	%datablock = GridZone;

	%r = $Round.grid.squareRC[%idx];
	%b = $Round.grid.squareBC[%idx];

	if(false && $Round.grid.squareF[%idx] & $Round.grid.Edge)
	{
		if((%flags & $Round.grid.Red) && (%flags & $Round.grid.Blue))
		{
			%datablock = BrokenGridZone;
		}

		if($Round.grid.squareGC[%idx] != 0)
		{
			%c1 = 7; %c2 = 7; %ci = 1;
		}
		else if(%r > 0 && %b > 0)
		{
			%c1 = 9; %c2 = 8; 

			if(%r > %b)
				%ci = 1 - (%b/%r)/2;
			else
				%ci = 1 - (%r/%b)/2;
		}
		else if($Round.grid.squareRC[%idx] > 0)
		{
			%c1 = 1; %c2 = 5; %ci = $Round.grid.squareRC[%idx];
		}
		else if($Round.grid.squareBC[%idx] > 0)
		{
			%c1 = 1; %c2 = 6; %ci = $Round.grid.squareBC[%idx];
		}
		else
		{
			%c1 = 0; %c2 = 0; %ci = 1;
		}
	}
	else 
	{
		%flags = $Round.grid.squareF[%idx];
		if(%flags & $Round.grid.Green)
		{
			%c1 = 7; %c2 = 7; %ci = 1;
			%zone.setColor(4, 4, 1);
		}
		else if((%flags & $Round.grid.Red) && (%flags & $Round.grid.Blue))
		{
			return;
			%c1 = 0; %c2 = 0; %ci = 1;
		}
		else if(%flags & $Round.grid.Red)
		{
			%c1 = 1; %c2 = 1; %ci = 1;
		}
		else if(%flags & $Round.grid.Blue)
		{
			%c1 = 2; %c2 = 2; %ci = 1;
		}
		else
		{
			%c1 = 15; %c2 = 15; %ci = 1;
		}
	}

	%team = 0;
	if((%flags & $Round.grid.Red) && !(%flags & $Round.grid.Blue))
		%team = 1;
	else if((%flags & $Round.grid.Blue) && !(%flags & $Round.grid.Red))
		%team = 2;

	%w = %zw * $Round.grid.squareSize;
	%h = %zh * $Round.grid.squareSize;

	%x = (%zx - $Round.grid.width/2) * $Round.grid.squareSize + %w/2;
	%y = (%zy - $Round.grid.height/2) * $Round.grid.squareSize + %h/2;
	%y = -%y;
	%z = getTerrainHeight(%x SPC %y);

	%zone = new TacticalZone() {
		teamId = %team;
		//hidden = true;	
		locked = true;
		dataBlock = %datablock; 
		position = %x SPC %y SPC %z;
		scale = %w/2 SPC %h/2 SPC 100;
		rotation = "0 0 1 0";
		borderTop = 0;
		borderBottom = 0;
		borderLeft = 0;
		borderRight = 0;
		borderFront = 0;
		borderBack = 0;
		renderTerrain = 1;
	};
	MissionCleanup.add(%zone);

	$Round.chunkmodgrid.currZoneSet.add(%zone);

	%zone.setColor(%c1, %c2, %ci);
}



