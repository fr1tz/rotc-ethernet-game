//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
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
