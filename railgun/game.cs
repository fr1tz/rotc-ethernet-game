//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./railgun/railgun.cs");

function EtherformData::useWeapon(%this, %obj, %nr)
{
    messageClient(%obj.client, 'MsgWeaponUsed', "", $SpecialWeapon::SniperRifle);
}

function StandardCat::useWeapon(%this, %obj, %nr)
{
    %obj.unmountImage(1); // unmount disc
    %obj.setDiscs(0);

	if(%obj.getTeamId() == 1)
		%obj.mountImage(RedRailgunImage, 0, -1, true);
	else
		%obj.mountImage(BlueRailgunImage, 0, -1, true);
}
