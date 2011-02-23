
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

function map_displayObjective(%message)
{
	if(%message !$= "")
		$Map::Objective = %message;

	clearBottomPrintAll();
	%l = strlen($Map::Objective); %n = 0;
	while(%n < %l)
	{
		%chunk = getSubStr($Map::Objective, %n, 255);
		bottomPrintAll(%chunk);
		%n += 255;
	}
}

function map_clearObjective()
{
	$Map::Objective = "";
	clearBottomPrintAll();
}

