//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./grid.zones.cs");
exec("./grid.edgezones.cs");

function GameGrid_reset(%w, %h, %squareSize)
{
	%w = mFloatLength(%w, 0);
	%h = mFloatLength(%h, 0);

	if(isObject($Round.grid))
		$Round.grid.delete();
	$Round.grid = new ScriptObject();
	MissionCleanup.add($Round.grid);
	$Round.grid.squareSize = %squareSize;
	$Round.grid.numSquares = %w * %h;
	$Round.grid.width = %w;
	$Round.grid.height = %h;

	if(isObject($Round.tmpset))
		$Round.tmpset.delete();
	$Round.tmpset = new SimSet();
	MissionCleanup.add($Round.tmpset);

	// Flags for $Round.grid.squareF
	$Round.grid.Red    = 1;
	$Round.grid.Blue   = 2;
	$Round.grid.Green  = 4;
	$Round.grid.Edge   = 8;
	$Round.grid.FDone  = 32;  // used during GameGrid_createZones()

	$Round.grid.ColorMask = (
		$Round.grid.Red |
		$Round.grid.Blue |
		$Round.grid.Green
	);
}

function GameGrid_updateGridThread()
{
	//echo("GameGrid_updateGridThread()");

	if($Round.updateGridThread !$= "")
		cancel($Round.updateGridThread);

	if($Round.grid.state $= "")
	{
		$Round.tmpset.clear();
		for(%i = 0; %i < $Round.beacons.getCount(); %i++ )
		{	
			%obj = $Round.beacons.getObject(%i);
			GameGrid_clearBeaconGrid(%obj);
			$Round.tmpset.add(%obj);
		}

		if(isObject($Round.newgrid))
			$Round.newgrid.delete();
		$Round.newgrid = new ScriptObject();
		MissionCleanup.add($Round.newgrid);
		$Round.newgrid.currSquare = 0;

		if(isObject($Round.modgrid))
			$Round.modgrid.delete();
		$Round.modgrid = new ScriptObject();
		MissionCleanup.add($Round.modgrid);

		$Round.grid.state = 1;
	}

	if($Round.grid.state == 1)
	{
		if($Round.tmpset.getCount() > 0)
		{
			%obj = $Round.tmpset.getObject(0);
			%done = GameGrid_updateBeaconGrid(%obj);
			if(%done)
				$Round.tmpset.remove(%obj);
		}
		else
		{
			$Round.tmpset.clear();
			for(%i = 0; %i < $Round.beacons.getCount(); %i++ )
			{	
				%obj = $Round.beacons.getObject(%i);
				$Round.tmpset.add(%obj);
			}
			$Round.grid.state = 2;
		}
		$Round.updateGridThread = schedule(32, $Round, "GameGrid_updateGridThread");
	}
	else if($Round.grid.state == 2)
	{
		%done = GameGrid_mergeNewGrid();
		if(%done)
		{
			error("new:");
			GameGrid_print($Round.newgrid);
			error("mod:");
			GameGrid_print($Round.modgrid);
			error("grid:");
			GameGrid_print();
			$Round.grid.state = 4;
			GameGrid_deleteZones();
			$Round.grid.currChunkX = 0;
			$Round.grid.currChunkY = 0;
			$Round.grid.zf = 0;
			$Round.updateGridThread = schedule(4000, $Round, "GameGrid_updateGridThread");
		}
		else
			$Round.updateGridThread = schedule(32, $Round, "GameGrid_updateGridThread");
	}
	else if($Round.grid.state == 3)
	{
		if($Round.grid.currChunkX >= $Round.grid.width)
		{
			$Round.grid.currChunkY += 16;
			$Round.grid.currChunkX = 0;
		}
		%min_x = $Round.grid.currChunkX;
		%min_y = $Round.grid.currChunkY;
		%max_x = %min_x + 15;
		%max_y = %min_y + 15;
		GameGrid_updateGrid(%min_x, %min_y, %max_x, %max_y);
		$Round.grid.currChunkX += 16;
		if($Round.grid.currChunkLast >= $Round.grid.numSquares)
		{
			$Round.grid.currChunkX = 0;
			$Round.grid.currChunkY = 0;
			$Round.grid.state = "";
			$Round.grid.zf = 0;
			$Round.updateGridThread = schedule(0, $Round, "GameGrid_updateGridThread");
		}
		else
		{
			$Round.updateGridThread = schedule(16, $Round, "GameGrid_updateGridThread");
		}

	}
	else if($Round.grid.state == 4)
	{
		if($Round.grid.currChunkX >= $Round.grid.width)
		{
			$Round.grid.currChunkY += 8;
			$Round.grid.currChunkX = 0;
		}
		%min_x = $Round.grid.currChunkX;
		%min_y = $Round.grid.currChunkY;
		%max_x = %min_x + 7;
		%max_y = %min_y + 7;
		GameGrid_updateGridZones(%min_x, %min_y, %max_x, %max_y);
		if($Round.grid.currChunkLast >= $Round.grid.numSquares)
		{
			$Round.grid.state = 8;
			$Round.updateGridThread = schedule(4000, $Round, "GameGrid_updateGridThread");
		}
		else if($Round.grid.zi > $Round.grid.currChunkLast)
		{
			$Round.grid.currChunkX += 8;
			$Round.grid.zf = $Round.grid.currChunkY*$Round.grid.width + $Round.grid.currChunkX;
			$Round.updateGridThread = schedule(32, $Round, "GameGrid_updateGridThread");
		}
		else
		{
			$Round.updateGridThread = schedule(32, $Round, "GameGrid_updateGridThread");
		}
	}

	return;

//	for(%i = 0; %i < $Round.beacons.getCount(); %i++ )
//	{
//		%obj = $Round.beacons.getObject(%i);
//		GameGrid_createEdgeZones(%obj);
//	}

	for(%i = 0; %i < %obj.zGrid.numTmpSquares; %i++)
	{
		%edgeidx = %obj.zGrid.tmpSquare[%i];
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
		{
			$Round.grid.squareF[%edgeidx] = $Round.grid.squareF[%edgeidx] & (~$Round.grid.Edge);
		}
		else
		{
			%obj.zGrid.edgeSquare[%obj.zGrid.numEdgeSquares] = %edgeidx;
			%obj.zGrid.numEdgeSquares++;
		}
	}

}

function GameGrid_updateGrid(%min_x, %min_y, %max_x, %max_y)
{
//	error("GameGrid_updateGrid():" SPC %min_x SPC %min_y SPC %max_x SPC %max_y);

	$Round.grid.currChunkLeft = %min_x;
	$Round.grid.currChunkTop = %min_y;
	$Round.grid.currChunkRight = %max_x;
	$Round.grid.currChunkBottom = %max_y;
	$Round.grid.currChunkWidth = %max_x - %min_x + 1;
	$Round.grid.currChunkHeight = %max_y - %min_y + 1;
	$Round.grid.currChunkFirst = %min_y*$Round.grid.width + %min_x;
	$Round.grid.currChunkLast = %max_y*$Round.grid.width + %max_x;
	$Round.grid.currChunkNumSquares = $Round.grid.currChunkLast -
		$Round.grid.currChunkFirst + 1;

	for(%y = $Round.grid.currChunkTop; %y <= $Round.grid.currChunkBottom; %y++)
	{
		for(%x = $Round.grid.currChunkLeft; %x <= $Round.grid.currChunkRight; %x++)
		{	
			%idx = %y*$Round.grid.width + %x;
			$Round.grid.squareF[%idx]  = 0; // basic flags
			$Round.grid.squareC[%idx]  = 0; // foundation color
		}
	}

	for(%i = 0; %i < $Round.beacons.getCount(); %i++ )
	{
		%obj = $Round.beacons.getObject(%i);
		GameGrid_addBeacon(%obj);
	}
}

function GameGrid_updateGridZones(%min_x, %min_y, %max_x, %max_y)
{
	error("GameGrid_updateGridZones():" SPC %min_x SPC %min_y SPC %max_x SPC %max_y);

	$Round.grid.currChunkLeft = %min_x;
	$Round.grid.currChunkTop = %min_y;
	$Round.grid.currChunkRight = %max_x;
	$Round.grid.currChunkBottom = %max_y;
	$Round.grid.currChunkWidth = %max_x - %min_x + 1;
	$Round.grid.currChunkHeight = %max_y - %min_y + 1;
	$Round.grid.currChunkFirst = %min_y*$Round.grid.width + %min_x;
	$Round.grid.currChunkLast = %max_y*$Round.grid.width + %max_x;
	$Round.grid.currChunkNumSquares = $Round.grid.currChunkLast -
		$Round.grid.currChunkFirst + 1;

	GameGrid_createZones_foundation();
}


function GameGrid_print(%grid)
{
	if(%grid $= "")
		%grid = $Round.grid;

	for(%i = 0; %i < $Round.grid.height; %i++)
	{
		%row = "";
		for(%j = 0; %j < $Round.grid.width; %j++)
		{	
			%s = %grid.squareF[%i*$Round.grid.width + %j];
			switch(%s & 7)
			{
				case 0: %c = "N";
				case 1: %c = "R";
				case 2: %c = "B";
				case 3: %c = "V";
				case 4: %c = "G";
				case 5: %c = "O";
				case 6: %c = "C";
				case 7: %c = "W";
			}
			if(%s & %grid.grid.FDone)
				%c = strlwr(%c);
			%row = %row @ %c;
		}
		echo(%row);
	}
}

function GameGrid_printEdges()
{
	for(%i = 0; %i < $Round.grid.height; %i++)
	{
		%row = "";
		for(%j = 0; %j < $Round.grid.width; %j++)
		{	
			%flags = $Round.grid.squareF[%i*$Round.grid.width + %j];
			if(%flags & $Round.grid.Edge)
				%c = "O";
			else
				%c = "+";
			%row = %row @ %c;
		}
		echo(%row);
	}
}

function GameGrid_c2s(%nr)
{	
	%l = strlen(%nr);
	%n = 13 - %l;
	while(%n > 0)
	{
		%nr = " " @ %nr;
		%n--;
	}
	return %nr;
}

function GameGrid_clearBeaconGrid(%obj)
{
	if(isObject(%obj.zGrid))
		%obj.zGrid.delete();

	%obj.zGrid = new ScriptObject();
	MissionCleanup.add(%obj.zGrid);
	%obj.zGrid.complete = false;
	%obj.zGrid.currSquare = -1;
	%obj.zGrid.numUsedSquares = 0;
	%obj.zGrid.numEdgeSquares = 0;
	%obj.zGrid.numTmpSquares = 0;
}

function GameGrid_updateBeaconGrid(%obj)
{
	//echo("GameGrid_updateBeaconGrid():" SPC %obj.getId());

	%team = %obj.getTeamId();
	%radius = %obj.gridRadius;
	%ceil = mCeil(%radius);
	%rest = 1 - (%ceil-%radius);

	if(%obj.zGrid.currSquare == -1)
	{
		%pos = %obj.getPosition();
		%x = getWord(%pos, 0) / $Round.grid.squareSize;
		%x = mFloor(%x);
		%y = getWord(%pos, 1) / $Round.grid.squareSize;
		%y = -%y;
		%y = mFloor(%y);
		%x = mFloatLength(%x, 0) + $Round.grid.width/2;
		%y = mFloatLength(%y, 0) + $Round.grid.width/2;

		%c1 = %x SPC %y;
		%minx = mFloor(%x - %radius);
		if(%minx < 0) %minx = 0;
		%miny = mFloor(%y - %radius);
		if(%miny < 0) %miny = 0;
		%maxx = mCeil(%x + %radius);
		if(%maxx > $Round.grid.width-1) %maxx = $Round.grid.width-1;
		%maxy = mCeil(%y + %radius);
		if(%maxy > $Round.grid.height-1) %maxy = $Round.grid.height-1;

		%obj.zGrid.center = %c1;

		%obj.zGrid.minX = %minx;
		%obj.zGrid.minY = %miny;
		%obj.zGrid.maxX = %maxx;
		%obj.zGrid.maxY = %maxy;

		%obj.zGrid.firstSquare = %miny*$Round.grid.width + %minx;
		%obj.zGrid.lastSquare = %maxy*$Round.grid.width + %maxx;
		%obj.zGrid.currSquare = %obj.zGrid.firstSquare;
	}

	%n = 64;
	while(%obj.zGrid.currSquare <= %obj.zGrid.lastSquare)
	{	
		%x = %obj.zGrid.currSquare % $Round.grid.width;
		%y = (%obj.zGrid.currSquare-%x) / $Round.grid.width;

		%c2 = %x SPC %y;
		%len = VectorLen(VectorSub(%c2, %obj.zGrid.center));
		if(%len <= %ceil)
		{
			%idx = %y*$Round.grid.width + %x;
			%obj.zGrid.squareF[%idx] = %obj.zGrid.squareF[%idx] | %team;
			$Round.newgrid.squareF[%idx] = $Round.newgrid.squareF[%idx] | %team;	
			if(%len > %ceil-1 || %x == 0 || %x == $Round.grid.width-1
			|| %y == 0 || %y == $Round.grid.height-1)
			{
				%obj.zGrid.tmpSquare[%obj.zGrid.numTmpSquares] = %idx;
				%obj.zGrid.numTmpSquares++;
			}
			%obj.zGrid.usedSquare[%obj.zGrid.numUsedSquares] = %idx;
			%obj.zGrid.numUsedSquares++;
		}

		if(%x == %obj.zGrid.maxX)
		{
			%x = %minx;
			%y = %y + 1;
			%obj.zGrid.currSquare = %y*$Round.grid.width + %x;
		}	
		else
			%obj.zGrid.currSquare++;

		if(%n-- < 0) 
			return false;
	}

	return true;
}

function GameGrid_mergeNewGrid()
{
	//echo("GameGrid_mergeNewGrid()");

	if($Round.newgrid.currSquare == $Round.grid.numSquares)
		return true;

	%n = 64;
	while($Round.newgrid.currSquare < $Round.grid.numSquares)
	{
		%mask = ($Round.grid.Red | $Round.grid.Green | $Round.grid.Blue);

		%oldflags = $Round.grid.squareF[$Round.newgrid.currSquare] & %mask;
		%newflags = $Round.newgrid.squareF[$Round.newgrid.currSquare] & %mask;

		if(%oldflags != %newflags)
		{
			//error("Change: " @ $Round.newgrid.currSquare @ ": " @ 
			//	%oldflags SPC "->" SPC %newflags);
			$Round.modgrid.squareF[$Round.newgrid.currSquare] = 1;
		}

		$Round.grid.squareF[$Round.newgrid.currSquare] = %newflags;
		
		$Round.newgrid.currSquare++;

		if(%n-- < 0)
			return false;
	}

	return true;
}

function GameGrid_addBeacon(%obj)
{
	%pos = %obj.getPosition();
	%x = getWord(%pos, 0) / $Round.grid.squareSize;
	%x = mFloor(%x);
	%y = getWord(%pos, 1) / $Round.grid.squareSize;
	%y = -%y;
	%y = mFloor(%y);
	%x = mFloatLength(%x, 0) + $Round.grid.width/2;
	%y = mFloatLength(%y, 0) + $Round.grid.width/2;
	%radius = %obj.gridRadius;
	%team = %obj.getTeamId();

	%c1 = %x SPC %y;
	%minx = mFloor(%x - %radius);
	if(%minx < 0) %minx = 0;
	%miny = mFloor(%y - %radius);
	if(%miny < 0) %miny = 0;
	%maxx = mCeil(%x + %radius);
	if(%maxx > $Round.grid.width-1) %maxx = $Round.grid.width-1;
	%maxy = mCeil(%y + %radius);
	if(%maxy > $Round.grid.height-1) %maxy = $Round.grid.height-1;

	if($Round.grid.currChunkRight < %minx) return;
	if($Round.grid.currChunkBottom < %miny) return;
	if($Round.grid.currChunkLeft > %maxx) return;
	if($Round.grid.currChunkTop > %maxy) return;

	if($Round.grid.currChunkRight < %maxx)
		%maxx = $Round.grid.currChunkRight;
	if($Round.grid.currChunkBottom < %maxy)
		%maxy = $Round.grid.currChunkBottom;
	if($Round.grid.currChunkLeft > %minx)
		%minx = $Round.grid.currChunkLeft;
	if($Round.grid.currChunkTop > %miny)
		%miny = $Round.grid.currChunkTop;

	%ceil = mCeil(%radius);
	%rest = 1 - (%ceil-%radius);

	for(%i = %miny; %i <= %maxy; %i++)
	{
		for(%j = %minx; %j <= %maxx; %j++)
		{	
			%c2 = %j SPC %i;
			%len = VectorLen(VectorSub(%c2, %c1));
			if(%len <= %ceil)
			{
				%idx = %i*$Round.grid.width + %j;
				%flags = $Round.grid.squareF[%idx];
				%flags = %flags | %team;		
				if(%len > %ceil-1 || %j == 0 || %j == $Round.grid.width-1
				|| %i == 0 || %i == $Round.grid.height-1)
				{
					%obj.zGrid.squareF[%idx] = %obj.zGrid.squareF[%idx] | 2;
					%flags = %flags | $Round.grid.Edge;
					%obj.zGrid.tmpSquare[%obj.zGrid.numTmpSquares] = %idx;
					%obj.zGrid.numTmpSquares++;
				}
				%c = (%flags & ($Round.grid.Red | $Round.grid.Green | $Round.grid.Blue));
				$Round.grid.squareC[%idx] = %c;
				$Round.grid.squareF[%idx] = %flags;
				%obj.zGrid.squareF[%idx] = %obj.zGrid.squareF[%idx] | 1;
			}
		}
	}
}


function GameGrid_addCircle(%x, %y, %radius, %team)
{
	%c1 = %x SPC %y;
	%minx = mFloor(%x - %radius);
	if(%minx < 0) %minx = 0;
	%miny = mFloor(%y - %radius);
	if(%miny < 0) %miny = 0;
	%maxx = mCeil(%x + %radius);
	if(%maxx > $Round.grid.width-1) %maxx = $Round.grid.width-1;
	%maxy = mCeil(%y + %radius);
	if(%maxy > $Round.grid.height-1) %maxy = $Round.grid.height-1;

	%ceil = mCeil(%radius);
	%rest = 1 - (%ceil-%radius);

	for(%i = %miny; %i <= %maxy; %i++)
	{
		for(%j = %minx; %j <= %maxx; %j++)
		{	
			%c2 = %j SPC %i;
			%len = VectorLen(VectorSub(%c2, %c1));
			if(%len <= %ceil)
			{
				%idx = %i*$Round.grid.width + %j;
				%flags = $Round.grid.squareF[%idx];
				%flags = %flags | %team;		
				if(true && %len > %ceil-1)
				{
					%flags = %flags | $Round.grid.Edge;
					%val = %rest;
if(false) {
					if(%team == 0) {
						if(%val > $Round.grid.squareGC[%idx])
							$Round.grid.squareGC[%idx] = %val;
					} else if(%team == 1) {
						if(%val > $Round.grid.squareRC[%idx])
							$Round.grid.squareRC[%idx] = %val;
					} else if(%team == 2) {
						if(%val > $Round.grid.squareBC[%idx])
							$Round.grid.squareBC[%idx] = %val;
					}
					if(%j < %x+0.5) {
						%flags = %flags | $Round.grid.BorderL;
					} else {
						%flags = %flags | $Round.grid.BorderR;
					}
					if(%i < %y+0.5) {
						%flags = %flags | $Round.grid.BorderT;
					} else {
						%flags = %flags | $Round.grid.BorderB;
					}
}
				}
				%c = (%flags & ($Round.grid.Red | $Round.grid.Green | $Round.grid.Blue));
				$Round.grid.squareC[%idx] = %c;
				$Round.grid.squareF[%idx] = %flags;
			}
		}
	}
}

