//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function GameConnection::showHelpMenu(%this)
{
	%L3 = om_init();
	%L3 = %L3 @ om_head(%this, "Help");
	%L3 = %L3 @ "<lmargin:24><font:NovaSquare:36>";

	%spc = " <spush><font:NovaSquare:12>\n\n<spop>";

	%L4 = %L3;

	%msk = "      ";
	%L3 = %L3 @ "<B:1:cmd Manual C>"@%msk@"Controls</a>";
	%L3 = %L3 @ %spc;
	%L3 = %L3 @ "<B:2:cmd Manual 1>"@%msk@"Ethernet Basics</a>";
	%L3 = %L3 @ %spc;
	%L3 = %L3 @ "<B:3:cmd Manual 2>"@%msk@"The HUD</a>";
	%L3 = %L3 @ %spc;
	%L3 = %L3 @ "<B:4:cmd Manual 5>"@%msk@"Firing & Deflecting Discs</a>";
	%L3 = %L3 @ %spc;
	%L3 = %L3 @ "<B:5:cmd Manual 0>"@%msk@"Manual Index</a>";

	%L4 = %L4 @ "<bitmap:share/ui/rotc/icon.controls>";
	%L4 = %L4 @ %spc;
	%L4 = %L4 @ "<bitmap:share/ui/rotc/icon.basics>";
	%L4 = %L4 @ %spc;
	%L4 = %L4 @ "<bitmap:share/ui/rotc/icon.hud>";
	%L4 = %L4 @ %spc;
	%L4 = %L4 @ "<bitmap:share/ui/rotc/icon.discs>";
	%L4 = %L4 @ %spc;
	%L4 = %L4 @ "<bitmap:share/ui/rotc/icon.manual>";

	%this.beginMenuText(false);
	if(%L1 !$= "") %this.addMenuText(%L1, 1);
	if(%L2 !$= "") %this.addMenuText(%L2, 2);
	if(%L3 !$= "") %this.addMenuText(%L3, 4);
	if(%L4 !$= "") %this.addMenuText(%L4, 8);
	%this.endMenuText();

}
