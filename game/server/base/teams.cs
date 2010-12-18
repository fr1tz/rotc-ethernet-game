//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - teams.cs
// Stuff concerning teams
// Thanks to Xavier "ExoDuS" Amado
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// scripted team objects...
//-----------------------------------------------------------------------------

// Team0 = Observers
// Team1 = Red
// Team2 = Blue

function createTeams()
{
	if(!isObject($Team0))
	{
		$Team0 = new ScriptObject()
		{
			teamId = 0;
			name = "Observers";
			numPlayers = 0;
		};
		MissionCleanup.add($Team0);
	}

	if(!isObject($Team1))
	{
		$Team1 = new ScriptObject()
		{
			teamId = 1;
			name = "Reds";
			score = 0;
			numPlayers = 0;
			numTerritoryZones = 0;
			numCATs = 0;
            repairSpeed = 0.5;
		};
		MissionCleanup.add($Team1);
		
		$Team1.repairObjects = new SimGroup();
		MissionCleanup.add($Team1.repairObjects);
	}

	if(!isObject($Team2))
	{
		$Team2 = new ScriptObject()
		{
			teamId = 2;
			name = "Blues";
			score = 0;
			numPlayers = 0;
			numTerritoryZones = 0;
			numCATs = 0;
            repairSpeed = 0.5;
		};
		MissionCleanup.add($Team2);

		$Team2.repairObjects = new SimGroup();
		MissionCleanup.add($Team2.repairObjects);
	}
}
