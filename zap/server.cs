//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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
