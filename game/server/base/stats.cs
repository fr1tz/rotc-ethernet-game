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
	%p.discDmgCaused = arrayGetValue(%a, "seeker");
	%p.grenadeDmgCaused = arrayGetValue(%a, "grenade");
	%p.blasterDmgCaused = arrayGetValue(%a, "blaster");
	%p.brDmgCaused = arrayGetValue(%a, "br");
	%p.minigunDmgCaused = arrayGetValue(%a, "minigun");
	%p.sniperDmgCaused = arrayGetValue(%a, "sniper");
	%p.glDmgCaused = arrayGetValue(%a, "gl");
	%p.totalDmgCaused = arrayGetValue(%a, "All");

	%a = %s.dmgReceivedCaused;
	%p.discDmgTaken = arrayGetValue(%a, "seeker");
	%p.grenadeDmgTaken = arrayGetValue(%a, "grenade");
	%p.blasterDmgTaken = arrayGetValue(%a, "blaster");
	%p.brDmgTaken = arrayGetValue(%a, "br");
	%p.minigunDmgTaken = arrayGetValue(%a, "minigun");
	%p.sniperDmgTaken = arrayGetValue(%a, "sniper");
	%p.glDmgTaken = arrayGetValue(%a, "gl");	
	%p.forceDmgTaken = arrayGetValue(%a, "Force");
	%p.totalDmgTaken = arrayGetValue(%a, "All");

	%a = %s.healthLost;
	%p.discHealthLost = arrayGetValue(%a, "seeker");
	%p.grenadeHealthLost = arrayGetValue(%a, "grenade");
	%p.blasterHealthLost = arrayGetValue(%a, "blaster");
	%p.brHealthLost = arrayGetValue(%a, "br");
	%p.minigunHealthLost = arrayGetValue(%a, "minigun");
	%p.sniperHealthLost = arrayGetValue(%a, "sniper");
	%p.glHealthLost = arrayGetValue(%a, "gl");		
	%p.forceHealthLost = arrayGetValue(%a, "Force");
	%p.totalHealthLost = arrayGetValue(%a, "All");

	%p.totalHealthRegained = arrayGetValue(%s.healthRegained, "All");

	%a = %s.fired;
	%p.discFired = arrayGetValue(%a, "seeker");
	%p.grenadeFired = arrayGetValue(%a, "grenade");
	%p.blasterFired = arrayGetValue(%a, "blaster") * 9;
	%p.brFired = arrayGetValue(%a, "br");
	%p.minigunFired = arrayGetValue(%a, "minigun");
	%p.sniperFired = arrayGetValue(%a, "sniper");
	%p.glFired = arrayGetValue(%a, "gl");			
	%p.totalFired = %p.discFired + %p.grenadeFired + %p.blasterFired +
		 %p.brFired + %p.minigunFired + %p.sniperFired + %p.glFired;

	%a = %s.dmgDealtApplied;
	%p.discDmgApplied = arrayGetValue(%a, "seeker");
	%p.grenadeDmgApplied = arrayGetValue(%a, "grenade");
	%p.blasterDmgApplied = arrayGetValue(%a, "blaster") * 9;
	%p.brDmgApplied = arrayGetValue(%a, "br");
	%p.minigunDmgApplied = arrayGetValue(%a, "minigun");
	%p.sniperDmgApplied = arrayGetValue(%a, "sniper");
	%p.glDmgApplied = arrayGetValue(%a, "gl");				
	%p.totalDmgApplied = %p.discDmgApplied + %p.grenadeDmgApplied + %p.blasterDmgApplied + 
		%p.brDmgApplied + %p.minigunDmgApplied + %p.sniperDmgApplied + %p.glDmgApplied;

	%p.discDmgPossible = RedSeekerDisc.impactDamage * %p.discFired;
	%p.grenadeDmgPossible = RedGrenade.splashDamage * %p.grenadeFired;
	%p.blasterDmgPossible = RedBlasterProjectile.impactDamage * %p.blasterFired;
	%p.brDmgPossible = RedAssaultRifleProjectile1.splashDamage * %p.brFired;
	%p.minigunDmgPossible = RedMinigunProjectile.impactDamage * %p.minigunFired;
	%p.sniperDmgPossible = RedSniperProjectile.impactDamage * %p.sniperFired;
	%p.glDmgPossible = RedGrenadeLauncherProjectile.splashDamage * %p.glFired;
	%p.totalDmgPossible = %p.discDmgPossible + %p.grenadeDmgPossible + %p.blasterDmgPossible +
		%p.brDmgPossible + %p.minigunDmgPossible + %p.sniperDmgPossible + %p.glDmgPossible;

	%p.discEffectiveness = (%p.discDmgApplied / %p.discDmgPossible) * 100;
	%p.grenadeEffectiveness = (%p.grenadeDmgApplied / %p.grenadeDmgPossible) * 100;
	%p.blasterEffectiveness = (%p.blasterDmgApplied / %p.blasterDmgPossible) * 100;
	%p.brEffectiveness = (%p.brDmgApplied / %p.brDmgPossible) * 100;
	%p.minigunEffectiveness = (%p.minigunDmgApplied / %p.minigunDmgPossible) * 100;
	%p.sniperEffectiveness = (%p.sniperDmgApplied / %p.sniperDmgPossible) * 100;
	%p.glEffectiveness = (%p.glDmgApplied / %p.glDmgPossible) * 100;
	%p.totalEffectiveness = (%p.totalDmgApplied / %p.totalDmgPossible) * 100;

	%this.schedule(3000, "processPlayerStats");
}
