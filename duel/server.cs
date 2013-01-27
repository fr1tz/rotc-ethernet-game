//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

package Duel {

function onServerCreated()
{
    Parent::onServerCreated();
    $Server::ModString = "duel";
   	exec("./game.cs");
}

}; // package Duel
