//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function srdi_pad(%t, %num)
{
	%pad = "";
	%n = %num - strlen(%t);
	while(%n > 0)
	{
		%pad = %pad @ " ";
		%n--;
	}
	return %t @ %pad;
}

function srdi_h1(%t)
{
	return "<font:NovaSquare:12>" @ srdi_pad(%t, 20) @ "\n";
}

function srdi_h2(%t)
{
	return "<font:NovaSquare:20>" @ srdi_pad(%t, 10) @ "\n";
}

function srdi_h3(%t)
{
	return "<font:NovaSquare:20>" @ srdi_pad(%t, 20) @ "\n";
}

function GameConnection::showReceivedDamageInfo(%this, %died)
{
	%t = "";
	%a = %this.stats.lastReceivedDamage;

	%n = "?";
	if(%a.damageName $= "blaster")
		%n = "Blaster hit";
	else if(%a.damageName $= "br")
		%n = "Battle Rifle explosion";
	else if(%a.damageName $= "sniper")
		%n = "Sniper ROFL hit";
	else if(%a.damageName $= "minigun")
		%n = "Minigun hit";
	else if(%a.damageName $= "gl")
		%n = "Mine explosion";
	else if(%a.damageName $= "grenade")
		%n = "Grenade explosion";
	else if(%a.damageName $= "explosivedisc")
		%n = "Explosive Disc";
	else if(%a.damageName $= "Force")
		%n = "Falling damage";
	else if(%a.damageName $= "Bounce")
		%n = "B.O.U.N.C.E.";
	else if(%a.damageName $= "Other")
		%n = "Being out of zone";

	%t = %t @ "<just:right>";
	if(%died)
		%t = %t @ srdi_h1("Fatal damage inflicted by");
	else
		%t = %t @ srdi_h1("Last damage inflicted by");
	%t = %t @ "\n";
	if(%a.sourceClientName !$= "")
		%t = %t @ srdi_h2(%a.sourceClientName @ "'s " @ %a.damageName);
	else
		%t = %t @ srdi_h2(%n);
	%t = %t @ "\n";

	%t = %t @ srdi_h1("Damage applied");
	%t = %t @ srdi_h2(mFloatLength(%a.damageApplied,0));
	%t = %t @ "\n";
	
	if(%a.handicapDifference > 0)
	{
		%t = %t @ srdi_h1("Attacker has higher handicap");
	}
	else
	{
		%i = mFloatLength(%a.handicapDifference*100,0);
		%t = %t @ srdi_h1("Handicap difference (" @ %i @ "%)");
	}
	%t = %t @ srdi_h2("-" SPC mFloatLength(%a.handicapSlice,0));
	%t = %t @ "\n";

	%i = mFloatLength(%a.damperEnergy*100,0);
	%t = %t @ srdi_h1("Damper (powered by " @ %i @ "% energy)");
	%t = %t @ srdi_h2("-" SPC mFloatLength(%a.damperSlice,0));
	%t = %t @ "\n";

	%i = mFloatLength(%a.shield,0);
	if(%a.shield > 0 && %a.shieldSlice == 0)
	{
		%t = %t @ srdi_h1("Damage bypasses shield of " @ %i);
	}
	else
	{
		%t = %t @ srdi_h1("Shield of " @ %i);
	}
	%t = %t @ srdi_h2("-" SPC mFloatLength(%a.shieldSlice,0));
	%t = %t @ "\n";

	%t = %t @ srdi_h1("-------------");
	%i = mFloatLength(%a.damageCaused,0);
	if(%i == 0) %i = 1;
	%t = %t @ srdi_h2("=" SPC %i);
	%t = %t @ srdi_h2("health damage");
	%i = mFloatLength(%a.health,0);
	if(%i == 0) %i = 1;
	%t = %t @ srdi_h1("(to " @ %i @ " remaining health)");
	
	%l = strlen(%t); %n = 0; %slot = 0;
	while(%n < %l)
	{
		%chunk = getSubStr(%t, %n, 255);
		%this.setHudMenuR(%slot, %chunk, 1, 1);
			%n += 255;
		%slot++;
	}
	for(%i = %slot; %i <= 9; %i++)
		%this.setHudMenuR(%i, "", 1, 0);
}
