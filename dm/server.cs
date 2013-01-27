//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

package Deathmatch {

function onServerCreated()
{
    Parent::onServerCreated();

    if($Server::ModString $= "-")
        $Server::ModString = "dm";
    else
        $Server::ModString = $Server::ModString @ "/dm";

   	exec("./game.cs");
}

}; // package Deathmatch
