//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function GameConnection::getEtherformDataBlock(%this)
{
	if(strstr(strlwr(getTaggedString(%this.name)),"nyan") != -1)
	{
		if( %this.team == $Team1 )
			return RedInfantryEtherform;
		else
			return BlueInfantryEtherform;
	}
	else
	{
		if( %this.team == $Team1 )
			return RedInfantryEtherform;
		else
			return BlueInfantryEtherform;
	}
}

function GameConnection::getPlayerFormDataBlock(%this)
{
	if( %this.team == $Team1 )
		return RedInfantryCat;
	else
		return BlueInfantryCat;
}