//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function showNews(%client)
{
	%newtxt = om_init();
	%client.beginMenuText();

	if(%page $= "")
		%page = 1;

	%newtxt = %newtxt @ om_head(%client, "", "MainMenu");

	%filename = "NEWS";

	%file = new FileObject();
	%file.openForRead(%fileName);
	while(!%file.isEOF())
		%newtxt = %newtxt @ strreplace(%file.readLine(), "<br>", "\n") @ "\n";
	%file.delete();

	%client.addMenuText(%newtxt);
	%client.endMenuText();
}