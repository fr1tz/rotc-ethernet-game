//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./grid.debug.cs");
exec("./grid.zones.cs");
exec("./grid.edgezones.cs");

function GameGrid_reset()
{
	%gridSquareSize = 32;
	%w = getWord(MissionArea.area, 2) / %gridSquareSize;
	%h = getWord(MissionArea.area, 3) / %gridSquareSize;
	%w = mFloatLength(%w, 0);
	%h = mFloatLength(%h, 0);

	if(isObject($Round.grid))
		$Round.grid.delete();
	$Round.grid = new ScriptObject();
	MissionCleanup.add($Round.grid);
	$Round.grid.squareSize = %gridSquareSize;
	$Round.grid.numSquares = %w * %h;
	$Round.grid.width = %w;
	$Round.grid.height = %h;

	if(isObject($Round.chunkgrid))
	{
		for(%i = 0; %i < $Round.chunkgrid.numSquares; %i++)
		{
			%zoneSet = $Round.chunkgrid.zoneSet[%i];
			if(isObject(%zoneSet))
				%zoneSet.delete();

			%beaconSet = $Round.chunkgrid.beaconSet[%i];
			if(isObject(%beaconSet))
				%beaconSet.delete();
		}	
		$Round.chunkgrid.delete();
	}
	$Round.chunkgrid = new ScriptObject();
	MissionCleanup.add($Round.chunkgrid);
	$Round.chunkgrid.squareSize = 8; // in grid squares
	$Round.chunkgrid.width = mCeil($Round.grid.width/$Round.chunkgrid.squareSize);
	$Round.chunkgrid.height = mCeil($Round.grid.height/$Round.chunkgrid.squareSize);
	$Round.chunkgrid.numSquares = $Round.chunkgrid.width * $Round.chunkgrid.height;
	for(%i = 0; %i < $Round.chunkgrid.numSquares; %i++ )
	{
		// SimSet that contains all the TacticalZones in the chunk
		%zoneSet = new SimSet();
		MissionCleanup.add(%zoneSet);
		%zoneSet.gridchunk = %i;
		$Round.chunkgrid.zoneSet[%i] = %zoneSet;

		// SimSet that contains all the beacons that have squares in the chunk
		%beaconSet = new SimSet();
		MissionCleanup.add(%beaconSet);
		%beaconSet.gridchunk = %i;
		$Round.chunkgrid.beaconSet[%i] = %beaconSet;
	}	

	if(isObject($Round.tmpset))
		$Round.tmpset.delete();
	$Round.tmpset = new SimSet();
	MissionCleanup.add($Round.tmpset);

	GameGrid_deleteZones();

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

	return;

	for(%i = 0; %i < $Round.grid.numSquares; %i++ )
	{
		%gx = %i % $Round.grid.width;
		%gy = (%i-%gx) / $Round.grid.width;

		%x = (%gx - $Round.grid.width/2) * $Round.grid.squareSize + $Round.grid.squareSize/2;
		%y = (%gy - $Round.grid.height/2) * $Round.grid.squareSize + $Round.grid.squareSize/2;
		%y = -%y;
		%z = getTerrainHeight(%x SPC %y);

		%pos = %x SPC %y SPC %z;

		//%muzzleTransform = createOrientFromDir(VectorNormalize(%muzzleVector));

		%obj = new StaticShape() {
			dataBlock = GridMarker;
			scale = "5 5 5";
			teamId = 3;
		};	
		MissionCleanup.add(%obj);
		%obj.setTransform(%pos);
	}
}

function GameGrid_updateGridThread()
{
	//echo("GameGrid_updateGridThread()");

	if($Round.updateGridThread !$= "")
		cancel($Round.updateGridThread);

	%pause = 32;

	if($Round.grid.state $= "")
	{
		$Round.tmpset.clear();
		for(%i = 0; %i < $Round.beacons.getCount(); %i++ )
		{	
			%obj = $Round.beacons.getObject(%i);
			%obj.zTickGridRadius = %obj.gridRadius;
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
	
		if(isObject($Round.chunkmodgrid))	
			$Round.chunkmodgrid.delete();
		$Round.chunkmodgrid = new ScriptObject();
		MissionCleanup.add($Round.chunkmodgrid);
		$Round.chunkmodgrid.squareSize = $Round.chunkgrid.squareSize;
		$Round.chunkmodgrid.width = $Round.chunkgrid.width;
		$Round.chunkmodgrid.height = $Round.chunkgrid.height;
		$Round.chunkmodgrid.numSquares = $Round.chunkgrid.numSquares;

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
			$Round.grid.state = 2;
		}
	}
	else if($Round.grid.state == 2)
	{
		%done = GameGrid_mergeNewGrid();
		if(%done)
			$Round.grid.state = 3;
	}
	else if($Round.grid.state == 3)
	{
		if($Round.tmpset.getCount() == 0)
		{
			$Round.grid.state = "";
			%pause = 8000;
		}
		else
		{
			$Round.chunkmodgrid.currZoneSet = $Round.tmpset.getObject(0);

			%idx = $Round.chunkmodgrid.currZoneSet.gridchunk;
			%x = %idx % $Round.chunkgrid.width;
			%y = (%idx-%x) / $Round.chunkgrid.width;
			%min_x = %x * $Round.chunkgrid.squareSize;
			%min_y = %y * $Round.chunkgrid.squareSize;
			%max_x = %min_x + $Round.chunkgrid.squareSize-1;
			%max_y = %min_y + $Round.chunkgrid.squareSize-1;

			$Round.grid.currChunkIndex = %idx;
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

			while($Round.chunkmodgrid.currZoneSet.getCount() > 0)
				$Round.chunkmodgrid.currZoneSet.getObject(0).delete();

			$Round.grid.state = 4;

			%pause = 4;
		}
	}
	else if($Round.grid.state == 4)
	{

		%done = GameGrid_createCurrChunkZones();	
		if(%done)
			$Round.grid.state = 5;
		%pause = 4;
	}
	else if($Round.grid.state == 5)
	{
		%idx = $Round.grid.currChunkIndex;
		%beaconSet = $Round.chunkgrid.beaconSet[%idx];
		if(%beaconSet.getCount() == 0)
		{
			// Done with the chunk.
			$Round.tmpset.remove($Round.chunkmodgrid.currZoneSet);
			$Round.grid.state = 3;
		}
		else
		{
			%beacon = %beaconSet.getObject(0);
			%done = GameGrid_createCurrChunkEdgeZones(%beacon);
			if(%done)
				%beaconSet.remove(%beacon);
		}
		%pause = 4;
	}

	$Round.updateGridThread = schedule(%pause, $Round, "GameGrid_updateGridThread");
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

	for(%i = 0; %i < $Round.chunkgrid.numSquares; %i++ )
	{
		//$Round.chunkgrid.beaconSet[%i].remove(%obj);

		%edgeSquares = %obj.zEdgeSquares[%i];
		if(isObject(%edgeSquares))
			%edgeSquares.delete();
		%edgeSquares = new ScriptObject();
		MissionCleanup.add(%edgeSquares);
		%edgeSquares.gridchunk = %i;
		%edgeSquares.numSquares = 0;
		%obj.zEdgeSquares[%i] = %edgeSquares;
	}	
}

function GameGrid_updateBeaconGrid(%obj)
{
	//echo("GameGrid_updateBeaconGrid():" SPC %obj.getId());

	%team = %obj.getTeamId();
	%radius = %obj.zTickGridRadius;
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
			%chunkx = mFloor(%x / $Round.chunkgrid.squareSize);
			%chunky = mFloor(%y / $Round.chunkgrid.squareSize);
			%chunkidx = %chunky*$Round.chunkgrid.width + %chunkx;

			// Add beacon to the square's chunk's beacon SimSet.
			%beaconSet = $Round.chunkgrid.beaconSet[%chunkidx];
			%beaconSet.add(%obj);

			// Update the new grid and beacon's own grid.
			$Round.newgrid.squareF[%idx] |= %team;
			%obj.zGrid.squareF[%idx] |= 1;

			if(%len > %ceil-1 || %x == 0 || %x == $Round.grid.width-1
			|| %y == 0 || %y == $Round.grid.height-1)
			{
				$Round.newgrid.squareF[%idx] |= $Round.grid.Edge;
				%obj.zGrid.squareF[%idx] |= 2;
				%edgeSquares = %obj.zEdgeSquares[%chunkidx];
				%edgeSquares.squareidx[%edgeSquares.numSquares] = %idx;
				%edgeSquares.numSquares++;
			}
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
		%mask = ($Round.grid.ColorMask | $Round.grid.Edge);

		%oldflags = $Round.grid.squareF[$Round.newgrid.currSquare] & %mask;
		%newflags = $Round.newgrid.squareF[$Round.newgrid.currSquare] & %mask;

		if(%oldflags != %newflags)
		{
			//error("Change: " @ $Round.newgrid.currSquare @ ": " @ 
			//	%oldflags SPC "->" SPC %newflags);
			$Round.modgrid.squareF[$Round.newgrid.currSquare] = 1;
			%x = $Round.newgrid.currSquare % $Round.grid.width;
			%y = ($Round.newgrid.currSquare-%x) / $Round.grid.width;
			%x = mFloor(%x / $Round.chunkgrid.squareSize);
			%y = mFloor(%y / $Round.chunkgrid.squareSize);
			%idx = %y*$Round.chunkgrid.width + %x;
			//error(%x SPC %y SPC %idx);
			%zoneSet = $Round.chunkgrid.zoneSet[%idx];
			$Round.tmpset.add(%zoneSet);
		}

		$Round.grid.squareF[$Round.newgrid.currSquare] = %newflags;
		
		$Round.newgrid.currSquare++;

		if(%n-- < 0)
			return false;
	}

	return true;
}


