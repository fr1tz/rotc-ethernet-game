//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function GameGrid_createCurrChunkEdgeZones(%obj)
{
	%chunkidx = $Round.grid.currChunkIndex;

	%n = 64;

	%edgeSquares = %obj.zEdgeSquares[%chunkidx];
	while(%edgeSquares.numSquares > 0)
	{
		if(%n-- < 0) return false;

		%edgeidx = %edgeSquares.squareidx[%edgeSquares.numSquares-1];

		%x = %edgeidx % $Round.grid.width;
		%y = %edgeidx >= $Round.grid.width ? (%edgeidx-%x) / $Round.grid.width : 0;

		%nx = 0; %ny = 0;
		if(%obj.zGrid.squareF[%y*$Round.grid.width+(%x-1)] & 2
		&& %obj.zGrid.squareF[%y*$Round.grid.width+(%x+1)] & 1)
			%nx++;
		if(%obj.zGrid.squareF[(%y-1)*$Round.grid.width+%x] & 2
		&& %obj.zGrid.squareF[(%y+1)*$Round.grid.width+%x] & 1)
			%ny++;
		if(%obj.zGrid.squareF[%y*$Round.grid.width+(%x+1)] & 2
		&& %obj.zGrid.squareF[%y*$Round.grid.width+(%x-1)] & 1)
			%nx++;
		if(%obj.zGrid.squareF[(%y+1)*$Round.grid.width+%x] & 2
		&& %obj.zGrid.squareF[(%y-1)*$Round.grid.width+%x] & 1)
			%ny++;

		if(%nx == 1 && %ny == 1)	
			%obj.zGrid.squareF[%edgeidx] = %obj.zGrid.squareF[%edgeidx] & (~2);

		%edgeSquares.numSquares--;
	}

	// Maximum width/height of zones measured in game grid squares...
	%zoneMaxSize = 1024 / $Round.grid.squareSize;

	if($Round.grid.zi $= "")
		$Round.grid.zi = $Round.grid.currChunkFirst;

	$Round.grid.obj = %obj;
	$Round.grid.team = %obj.getTeamId();
	$Round.grid.value = 1-(mCeil(%obj.gridRadius)-%obj.gridRadius);

	$Round.grid.zc = 0;
	$Round.grid.zx = 0; $Round.grid.zy = 0;
	$Round.grid.zState = 0; // 0 = find start   1 = go right    2 = go down 
	$Round.grid.ziterations = $Round.grid.numSquares * 4;

	while($Round.grid.ziterations >= 0)
	{
		$Round.grid.ziterations--;

		%x = $Round.grid.zi % $Round.grid.width;
		%y = ($Round.grid.zi-%x) / $Round.grid.width;

		//error(%chunkidx @ ":" @ $Round.grid.zi @ " = " @ %obj.zGrid.squareF[$Round.grid.zi]);

		if($Round.grid.zState == 0)
		{
			if($Round.grid.zi > $Round.grid.currChunkLast)
			{
				$Round.grid.zi = "";
				return true;
			}

			if(%obj.zGrid.squareF[$Round.grid.zi] == (2|1))
			{
				%color = %obj.zGrid.squareF[$Round.grid.zi];
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
			%color = %obj.zGrid.squareF[$Round.grid.zi];
			%samecolor = (%color == $Round.grid.zc);

			if(%samecolor)
				$Round.grid.zl = $Round.grid.zi;

			if(%x == $Round.grid.zxmax || !%samecolor)
			{
				if(%samecolor)
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
			%color = %obj.zGrid.squareF[$Round.grid.zi];
			%samecolor = (%color == $Round.grid.zc);

			if($Round.grid.zi > $Round.grid.currChunkLast 
			|| %y > $Round.grid.currChunkBottom
			|| !%samecolor)
			{
				GameGrid_createZones_dropEdgeZone();	
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


function GameGrid_createZones_dropEdgeZone()
{
	%obj = $Round.grid.obj;

	%zc = $Round.grid.zc;

	%zx = $Round.grid.zx;
	%zy = $Round.grid.zy; 

	%endx = $Round.grid.zl % $Round.grid.width;
	%endy = $Round.grid.zl >= $Round.grid.width ? ($Round.grid.zl-%endx) / $Round.grid.width : 0;

	%zw = %endx - %zx + 1;
	%zh = %endy - %zy + 1;

//	echo("GameGrid_createZones_dropEdgeZone():" SPC
//		%zx SPC %zy SPC %zw @ "x" @ %zh @ " : " @ $Round.grid.team);

	%bl = 0; %bt = 0; %br = 0; %bb = 0;

	for(%i = %zy; %i < %zy + %zh; %i++)
	{
		for(%j = %zx; %j < %zx + %zw; %j++)
		{
			%idx = %i*$Round.grid.width + %j;
			if(%obj.zGrid.squareF[%i*$Round.grid.width+(%j-1)] == "")
				%bl = 1;
			if(%obj.zGrid.squareF[(%i-1)*$Round.grid.width+%j] == "")
				%bt = 1;
			if(%obj.zGrid.squareF[%i*$Round.grid.width+(%j+1)] == "")
				%br = 1;
			if(%obj.zGrid.squareF[(%i+1)*$Round.grid.width+%j] == "")
				%bb = 1;
			%obj.zGrid.squareF[%idx] = %obj.zGrid.squareF[%idx] | 4;
		}
	}

	%idx = $Round.grid.zf;
	%flags = $Round.grid.squareF[%idx];

	$Round.grid.zi = $Round.grid.zf;
	$Round.grid.zState = 3;

	if($Round.grid.team == 1)
	{
		%c1 = 1; %c2 = 5; %ci = $Round.grid.value;
	}
	else if($Round.grid.team == 2)
	{
		%c1 = 2; %c2 = 6; %ci = $Round.grid.value;
	}
	else
	{
		%c1 = 0; %c2 = 0; %ci = 1;
	}

	%w = %zw * $Round.grid.squareSize;
	%h = %zh * $Round.grid.squareSize;

	%x = (%zx - $Round.grid.width/2) * $Round.grid.squareSize + %w/2;
	%y = (%zy - $Round.grid.height/2) * $Round.grid.squareSize + %h/2;
	%y = -%y;
	%z = getTerrainHeight(%x SPC %y);

	%bw = 4;
	%bl *= %bw;
	%bt *= %bw;
	%br *= %bw;
	%bb *= %bw;

	%zone = new TacticalZone() {
		teamId = $Round.grid.team;
		//hidden = true;	
		locked = true;
		dataBlock = GridZone; 
		position = %x SPC %y SPC %z;
		scale = %w/2 SPC %h/2 SPC 100;
		rotation = "0 0 1 0";
		borderTop = 0;
		borderBottom = 0;
		borderLeft = %bl;
		borderRight = %br;
		borderFront = %bt;
		borderBack = %bb;
		renderTerrain = 0;
	};
	MissionCleanup.add(%zone);

	$Round.chunkmodgrid.currZoneSet.add(%zone);

	%zone.setColor(%c1, %c2, %ci);
}

