//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

package Duel {

function onServerCreated()
{
    Parent::onServerCreated();
    $Server::ModString = "duel";
   	exec("./game.cs");
}

}; // package Duel
