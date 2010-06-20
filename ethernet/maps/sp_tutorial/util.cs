function map_displayCenter(%message)
{
	centerPrintAll("", 0, 0 );
	%l = strlen(%message); %n = 0;
	while(%n < %l)
	{
		%chunk = getSubStr(%message, %n, 255);
		centerPrintAll(%chunk, 0, 1);
		%n += 255;
	}
}

function map_clearCenter()
{
	clearCenterPrintAll();
}

