//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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
