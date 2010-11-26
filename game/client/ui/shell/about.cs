//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

function AboutWindow::onWake(%this)
{
	AboutFileList.entryCount = 0;
	AboutFileList.clear();

	%files[0] = "README";
	%files[1] = "AUTHORS";
	%files[2] = "COPYING";

	for(%i = 0; %i < 3; %i++)
	{
		%file = %files[%i];
		AboutFileList.fileName[AboutFileList.entryCount] = %file;
		AboutFileList.addRow(AboutFileList.entryCount, %file);
		AboutFileList.entryCount++;
	}
	AboutFileList.setSelectedRow(0);
}

function AboutFileList::onSelect(%this, %row)
{
	%fo = new FileObject();
	%fo.openForRead(%this.fileName[%row]);
	%text = "";
	while(!%fo.isEOF())
		%text = %text @ %fo.readLine() @ "\n";

	%fo.delete();

	AboutText.setText("<font:monospace:12>" @ %text);
}

