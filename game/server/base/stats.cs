//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
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
	
	%a = %s.dmgDealtApplied;
	%p.discDmgApplied = arrayGetValue(%a, "seeker");
	%p.grenadeDmgApplied = arrayGetValue(%a, "grenade");
	%p.blasterDmgApplied = arrayGetValue(%a, "blaster");
	%p.blaster4DmgApplied = arrayGetValue(%a, "blaster4");
	%p.blaster5DmgApplied = arrayGetValue(%a, "blaster5");
	%p.brDmgApplied = arrayGetValue(%a, "br");
	%p.minigunDmgApplied = arrayGetValue(%a, "minigun");
	%p.sniperDmgApplied = arrayGetValue(%a, "sniper");
	%p.sniper2DmgApplied = arrayGetValue(%a, "sniper2");
	%p.glDmgApplied = arrayGetValue(%a, "gl");
	%p.totalDmgApplied = arrayGetValue(%a, "All");	

	%a = %s.dmgDealtCaused;
	%p.discDmgCaused = arrayGetValue(%a, "seeker");
	%p.grenadeDmgCaused = arrayGetValue(%a, "grenade");
	%p.blasterDmgCaused = arrayGetValue(%a, "blaster");
	%p.blaster4DmgCaused = arrayGetValue(%a, "blaster4");
	%p.blaster5DmgCaused = arrayGetValue(%a, "blaster5");
	%p.brDmgCaused = arrayGetValue(%a, "br");
	%p.minigunDmgCaused = arrayGetValue(%a, "minigun");
	%p.sniperDmgCaused = arrayGetValue(%a, "sniper");
	%p.sniper2DmgCaused = arrayGetValue(%a, "sniper2");
	%p.glDmgCaused = arrayGetValue(%a, "gl");
	%p.totalDmgCaused = arrayGetValue(%a, "All");

	%a = %s.dmgReceivedCaused;
	%p.discDmgTaken = arrayGetValue(%a, "seeker");
	%p.grenadeDmgTaken = arrayGetValue(%a, "grenade");
	%p.blasterDmgTaken = arrayGetValue(%a, "blaster");
	%p.blaster4DmgTaken = arrayGetValue(%a, "blaster4");
	%p.blaster5DmgTaken = arrayGetValue(%a, "blaster5");
	%p.brDmgTaken = arrayGetValue(%a, "br");
	%p.minigunDmgTaken = arrayGetValue(%a, "minigun");
	%p.sniperDmgTaken = arrayGetValue(%a, "sniper");
	%p.sniper2DmgTaken = arrayGetValue(%a, "sniper2");
	%p.glDmgTaken = arrayGetValue(%a, "gl");
	%p.forceDmgTaken = arrayGetValue(%a, "Force");
	%p.totalDmgTaken = arrayGetValue(%a, "All");

	%a = %s.healthLost;
	%p.discHealthLost = arrayGetValue(%a, "seeker");
	%p.grenadeHealthLost = arrayGetValue(%a, "grenade");
	%p.blasterHealthLost = arrayGetValue(%a, "blaster");
	%p.blaster4HealthLost = arrayGetValue(%a, "blaster4");
	%p.blaster5HealthLost = arrayGetValue(%a, "blaster5");
	%p.brHealthLost = arrayGetValue(%a, "br");
	%p.minigunHealthLost = arrayGetValue(%a, "minigun");
	%p.sniperHealthLost = arrayGetValue(%a, "sniper");
	%p.sniper2HealthLost = arrayGetValue(%a, "sniper2");
	%p.glHealthLost = arrayGetValue(%a, "gl");		
	%p.forceHealthLost = arrayGetValue(%a, "Force");
	%p.totalHealthLost = arrayGetValue(%a, "All");

	%p.totalHealthRegained = arrayGetValue(%s.healthRegained, "All");

	%a = %s.fired;
	%p.discFired = arrayGetValue(%a, "seeker");
	%p.grenadeFired = arrayGetValue(%a, "grenade");
	%p.blasterFired = arrayGetValue(%a, "blaster") * 9;
	%p.blasterFired4 = arrayGetValue(%a, "blaster4") * 9;
	%p.blasterFired5 = arrayGetValue(%a, "blaster5");
	%p.brFired = arrayGetValue(%a, "br");
	%p.minigunFired = arrayGetValue(%a, "minigun");
	%p.sniperFired = arrayGetValue(%a, "sniper");
	%p.sniper2Fired = arrayGetValue(%a, "sniper2");
	%p.glFired = arrayGetValue(%a, "gl");			
	%p.totalFired = %p.discFired +
      %p.grenadeFired +
      %p.blasterFired +
      %p.blaster4Fired +
      %p.blaster5Fired +
      %p.brFired +
      %p.minigunFired +
      %p.sniperFired +
      %p.sniper2Fired +
      %p.glFired;

	%a = %s.dmgDealtApplied;
	%p.discDmgApplied = arrayGetValue(%a, "seeker");
	%p.grenadeDmgApplied = arrayGetValue(%a, "grenade");
	%p.blasterDmgApplied = arrayGetValue(%a, "blaster");
	%p.blaster4DmgApplied = arrayGetValue(%a, "blaster4");
	%p.blaster5DmgApplied = arrayGetValue(%a, "blaster5");
	%p.brDmgApplied = arrayGetValue(%a, "br");
	%p.minigunDmgApplied = arrayGetValue(%a, "minigun");
	%p.sniperDmgApplied = arrayGetValue(%a, "sniper");
	%p.sniper2DmgApplied = arrayGetValue(%a, "sniper2");
	%p.glDmgApplied = arrayGetValue(%a, "gl");				
	%p.totalDmgApplied = %p.discDmgApplied +
      %p.grenadeDmgApplied +
      %p.blasterDmgApplied +
		%p.blaster4DmgApplied +
      %p.blaster5DmgApplied +
      %p.brDmgApplied +
      %p.minigunDmgApplied +
      %p.sniperDmgApplied +
      %p.sniper2DmgApplied +
      %p.glDmgApplied;

	%p.discDmgPossible = RedSeekerDisc.impactDamage * %p.discFired;
	%p.grenadeDmgPossible = RedGrenade.splashDamage * %p.grenadeFired;
	%p.blasterDmgPossible = RedBlasterProjectile.impactDamage * %p.blasterFired;
	%p.blaster4DmgPossible = RedBlaster4Projectile.impactDamage * %p.blaster4Fired;
	%p.blaster5DmgPossible = RedBlaster5Projectile.impactDamage * %p.blaster5Fired;
	%p.brDmgPossible = RedAssaultRifleProjectile1.splashDamage * %p.brFired;
	%p.minigunDmgPossible = RedMinigunProjectile.impactDamage * %p.minigunFired;
	%p.sniperDmgPossible = RedSniperProjectile.impactDamage * %p.sniperFired;
	%p.sniper2DmgPossible = RedSniper2Projectile.impactDamage * %p.sniper2Fired;
	%p.glDmgPossible = RedGrenadeLauncherProjectile.splashDamage * %p.glFired;
	%p.totalDmgPossible = %p.discDmgPossible +
      %p.grenadeDmgPossible +
      %p.blasterDmgPossible +
		%p.blaster4DmgPossible +
      %p.blaster5DmgPossible +
      %p.brDmgPossible +
      %p.minigunDmgPossible +
      %p.sniperDmgPossible +
      %p.sniper2DmgPossible +
      %p.glDmgPossible;

	%p.discEffectiveness = (%p.discDmgApplied / %p.discDmgPossible) * 100;
	%p.grenadeEffectiveness = (%p.grenadeDmgApplied / %p.grenadeDmgPossible) * 100;
	%p.blasterEffectiveness = (%p.blasterDmgApplied / %p.blasterDmgPossible) * 100;
	%p.blaster4Effectiveness = (%p.blaster4DmgApplied / %p.blaster4DmgPossible) * 100;
	%p.blaster5Effectiveness = (%p.blaster5DmgApplied / %p.blaster5DmgPossible) * 100;
	%p.brEffectiveness = (%p.brDmgApplied / %p.brDmgPossible) * 100;
	%p.minigunEffectiveness = (%p.minigunDmgApplied / %p.minigunDmgPossible) * 100;
	%p.sniperEffectiveness = (%p.sniperDmgApplied / %p.sniperDmgPossible) * 100;
	%p.sniper2Effectiveness = (%p.sniper2DmgApplied / %p.sniper2DmgPossible) * 100;
	%p.glEffectiveness = (%p.glDmgApplied / %p.glDmgPossible) * 100;
	%p.totalEffectiveness = (%p.totalDmgApplied / %p.totalDmgPossible) * 100;
	
	if(%this.menuVisible)
	{
		if(%this.menu $= "playerlist")
			serverCmdShowPlayerList(%this, %this.menuArgs);
		else if(%this.menu $= "playerinfo")
			serverCmdShowPlayerInfo(%this, %this.menuArgs);			
	}
		
	%this.schedule(3000, "processPlayerStats");
}
