//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

package Railgun {

function onServerCreated()
{
    Parent::onServerCreated();
    
    if($Server::ModString $= "-")
        $Server::ModString = "Railgun";
    else
        $Server::ModString = $Server::ModString @ "/Railgun";
    
   	exec("./game.cs");
}

}; // package Railgun
