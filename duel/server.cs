//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

package Duel {

function onServerCreated()
{
    Parent::onServerCreated();
    $Server::ModString = "duel";
   	exec("./game.cs");
}

}; // package Duel
