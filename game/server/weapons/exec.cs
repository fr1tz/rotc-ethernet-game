//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./weapons.cs");
exec("./disc.cs");
exec("./grenade2/exec.cs");
if($Server::Game.arena $= "supersniper")
{
   exec("./sniperrifle3/exec.cs");
}
else
{
   exec("./sniperrifle.cs");
   exec("./sniperrifle2/exec.cs");
}
exec("./assaultrifle.cs");
exec("./grenadelauncher.cs");
exec("./blaster4/exec.cs");
exec("./blaster5/exec.cs");
exec("./minigun.cs");
exec("./repelgun.cs");
exec("./repel4/exec.cs");
