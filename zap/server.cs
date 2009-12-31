//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

package Zap {

function onServerCreated()
{
    Parent::onServerCreated();
    
    if($Server::ModString $= "-")
        $Server::ModString = "Zap";
    else
        $Server::ModString = $Server::ModString @ "/Zap";
    
   	exec("./game.cs");
}

}; // package Zap
