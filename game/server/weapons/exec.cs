//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./weapons.cs");
exec("./disc.cs");
exec("./grenade2/exec.cs");
exec("./sniperrifle.cs");
exec("./assaultrifle.cs");
exec("./grenadelauncher.cs");
exec("./blaster.cs");
if($Server::Game.superblaster)
	exec("./blaster3/exec.cs");
else
	exec("./blaster2/exec.cs");
exec("./minigun.cs");
exec("./repelgun.cs");
exec("./repel.cs");
