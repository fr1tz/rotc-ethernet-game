//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Player statistics stuff
//------------------------------------------------------------------------------

function trimStat(%stat)
{
    %dotPos = strpos(%stat, ".");
    if(%dotPos == -1)
        return %stat;
    else
        return getSubStr(%stat, 0, %dotPos+2);
}

function GameConnection::updateStats(%this)
{
    %this.stats.kills = %this.stats.healthTaken / 75;
    %this.stats.deaths = %this.stats.healthLost / 75;
	%this.score = trimStat(%this.stats.kills) SPC "/" SPC trimStat(%this.stats.deaths);
}

function GameConnection::updateScoreOnClients(%this)
{
    if(%this.score !$= %this.lastSentScore)
    {
    	messageAll('MsgClientScoreChanged', "", %this.score, %this);
     
        %this.lastSentScore = %this.score;
    }

    %this.schedule(1000, "updateScoreOnClients");
}
