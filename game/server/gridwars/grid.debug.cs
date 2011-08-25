//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

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




