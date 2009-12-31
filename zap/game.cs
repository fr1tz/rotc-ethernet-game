//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

exec("./zapper/zapper.cs");

function EtherformData::useWeapon(%this, %obj, %nr)
{
    messageClient(%obj.client, 'MsgWeaponUsed', "", -1);
}

function StandardCat::useWeapon(%this, %obj, %nr)
{
    %obj.unmountImage(1); // unmount disc
    %obj.setDiscs(0);

	if(%obj.getTeamId() == 1)
		%obj.mountImage(RedZapperImage, 0, -1, true);
	else
		%obj.mountImage(BlueZapperImage, 0, -1, true);
}
