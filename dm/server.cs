//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
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
