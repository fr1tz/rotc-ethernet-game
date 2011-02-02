//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Player statistics stuff
//------------------------------------------------------------------------------

function trimStat(%stat)
{
    %dotPos = strpos(%stat, ".");
    if(%dotPos == -1)
        return %stat;
    else
        return getSubStr(%stat, 0, %dotPos+2);
}

function removeDecimals(%stat)
{
    %dotPos = strpos(%stat, ".");
    if(%dotPos == -1)
        return %stat;
    else
        return getSubStr(%stat, 0, %dotPos);
}

function GameConnection::processPlayerStats(%this)
{
	%s = %this.stats;
	%p = %this.pstats;

	%p.lastUpdate = $Sim::Time;

	%p.timePlayed = ($Sim::Time-%s.joinTime)/60;

	%a = %s.dmgDealtCaused;
	%p.discDmgCaused = arrayGetValue(%a, "RedSeekerDisc") + 
		arrayGetValue(%a, "BlueSeekerDisc");
	%p.grenadeDmgCaused = arrayGetValue(%a, "RedGrenade") + 
		arrayGetValue(%a, "BlueGrenade");
	%p.tridentDmgCaused = arrayGetValue(%a, "RedBlasterProjectile") + 
		arrayGetValue(%a, "BlueBlasterProjectile");
	%p.mgDmgCaused = arrayGetValue(%a, "RedAssaultRifleProjectile") + 
		arrayGetValue(%a, "BlueAssaultRifleProjectile");
	%p.sniperDmgCaused = arrayGetValue(%a, "RedSniperProjectile") + 
		arrayGetValue(%a, "BlueSniperProjectile");
	%p.glDmgCaused = arrayGetValue(%a, "RedGrenadeLauncherProjectile") + 
		arrayGetValue(%a, "BlueGrenadeLauncherProjectile");
	%p.totalDmgCaused = arrayGetValue(%a, "All");

	%a = %s.dmgReceivedCaused;
	%p.discDmgTaken = arrayGetValue(%a, "RedSeekerDisc") + 
		arrayGetValue(%a, "BlueSeekerDisc");
	%p.grenadeDmgTaken = arrayGetValue(%a, "RedGrenade") + 
		arrayGetValue(%a, "BlueGrenade");
	%p.tridentDmgTaken = arrayGetValue(%a, "RedBlasterProjectile") + 
		arrayGetValue(%a, "BlueBlasterProjectile");
	%p.mgDmgTaken = arrayGetValue(%a, "RedAssaultRifleProjectile") + 
		arrayGetValue(%a, "BlueAssaultRifleProjectile");
	%p.sniperDmgTaken = arrayGetValue(%a, "RedSniperProjectile") + 
		arrayGetValue(%a, "BlueSniperProjectile");
	%p.glDmgTaken = arrayGetValue(%a, "RedGrenadeLauncherProjectile") + 
		arrayGetValue(%a, "BlueGrenadeLauncherProjectile");
	%p.forceDmgTaken = arrayGetValue(%a, "Force");
	%p.totalDmgTaken = arrayGetValue(%a, "All");

	%a = %s.healthLost;
	%p.discHealthLost = arrayGetValue(%a, "RedSeekerDisc") + 
		arrayGetValue(%a, "BlueSeekerDisc");
	%p.grenadeHealthLost = arrayGetValue(%a, "RedGrenade") + 
		arrayGetValue(%a, "BlueGrenade");
	%p.tridentHealthLost = arrayGetValue(%a, "RedBlasterProjectile") + 
		arrayGetValue(%a, "BlueBlasterProjectile");
	%p.mgHealthLost = arrayGetValue(%a, "RedAssaultRifleProjectile") + 
		arrayGetValue(%a, "BlueAssaultRifleProjectile");
	%p.sniperHealthLost = arrayGetValue(%a, "RedSniperProjectile") + 
		arrayGetValue(%a, "BlueSniperProjectile");
	%p.glHealthLost = arrayGetValue(%a, "RedGrenadeLauncherProjectile") + 
		arrayGetValue(%a, "BlueGrenadeLauncherProjectile");
	%p.forceHealthLost = arrayGetValue(%a, "Force");
	%p.totalHealthLost = arrayGetValue(%a, "All");

	%p.totalHealthRegained = arrayGetValue(%s.healthRegained, "All");

	%a = %s.fired;
	%p.discFired = arrayGetValue(%a, "RedSeekerDisc") + arrayGetValue(%a, "BlueSeekerDisc");
	%p.grenadeFired = arrayGetValue(%a, "RedGrenade") + arrayGetValue(%a, "BlueGrenade");
	%p.tridentFired = arrayGetValue(%a, "RedBlasterProjectile")*9 + arrayGetValue(%a, "BlueBlasterProjectile")*9;
	%p.mgFired = arrayGetValue(%a, "RedAssaultRifleProjectile") + arrayGetValue(%a, "BlueAssaultRifleProjectile");
	%p.sniperFired = arrayGetValue(%a, "RedSniperProjectile") + arrayGetValue(%a, "BlueSniperProjectile");
	%p.glFired = arrayGetValue(%a, "RedGrenadeLauncherProjectile") + arrayGetValue(%a, "BlueGrenadeLauncherProjectile");
	%p.totalFired = %p.discFired + %p.grenadeFired + %p.tridentFired +
		 %p.mgFired + %p.sniperFired + %p.glFired;

	%a = %s.dmgDealtApplied;
	%p.discDmgApplied = arrayGetValue(%a, "RedSeekerDisc") + arrayGetValue(%a, "BlueSeekerDisc");
	%p.grenadeDmgApplied = arrayGetValue(%a, "RedGrenade") + arrayGetValue(%a, "BlueGrenade");
	%p.tridentDmgApplied = arrayGetValue(%a, "RedBlasterProjectile") + arrayGetValue(%a, "BlueBlasterProjectile");
	%p.mgDmgApplied = arrayGetValue(%a, "RedAssaultRifleProjectile") + arrayGetValue(%a, "BlueAssaultRifleProjectile");
	%p.sniperDmgApplied = arrayGetValue(%a, "RedSniperProjectile") + arrayGetValue(%a, "BlueSniperProjectile");
	%p.glDmgApplied = arrayGetValue(%a, "RedGrenadeLauncherProjectile") + arrayGetValue(%a, "BlueGrenadeLauncherProjectile");
	%p.totalDmgApplied = %p.discDmgApplied + %p.grenadeDmgApplied + %p.tridentDmgApplied + 
		%p.mgDmgApplied + %p.sniperDmgApplied + %p.glDmgApplied;

	%p.discDmgPossible = RedSeekerDisc.impactDamage * %p.discFired;
	%p.grenadeDmgPossible = RedGrenade.splashDamage * %p.grenadeFired;
	%p.tridentDmgPossible = RedBlasterProjectile.impactDamage * %p.tridentFired;
	%p.mgDmgPossible = RedAssaultRifleProjectile.splashDamage * %p.mgFired;
	%p.sniperDmgPossible = RedSniperProjectile.impactDamage * %p.sniperFired;
	%p.glDmgPossible = RedGrenadeLauncherProjectile.splashDamage * %p.glFired;
	%p.totalDmgPossible = %p.discDmgPossible + %p.grenadeDmgPossible + %p.tridentDmgPossible +
		%p.mgDmgPossible + %p.sniperDmgPossible + %p.glDmgPossible;

	%p.discEffectiveness = (%p.discDmgApplied / %p.discDmgPossible) * 100;
	%p.grenadeEffectiveness = (%p.grenadeDmgApplied / %p.grenadeDmgPossible) * 100;
	%p.tridentEffectiveness = (%p.tridentDmgApplied / %p.tridentDmgPossible) * 100;
	%p.mgEffectiveness = (%p.mgDmgApplied / %p.mgDmgPossible) * 100;
	%p.sniperEffectiveness = (%p.sniperDmgApplied / %p.sniperDmgPossible) * 100;
	%p.glEffectiveness = (%p.glDmgApplied / %p.glDmgPossible) * 100;
	%p.totalEffectiveness = (%p.totalDmgApplied / %p.totalDmgPossible) * 100;


	%this.schedule(3000, "processPlayerStats");
}
