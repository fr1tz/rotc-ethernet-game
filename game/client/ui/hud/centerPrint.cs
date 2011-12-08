//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

$centerPrintActive = 0;
$bottomPrintActive = 0;

// time is specified in seconds
function clientCmdCenterPrint( %message, %time, %append )
{
	// if centerprint already visible, reset text and time.
	if ($centerPrintActive) {	
		if (CenterPrint.removePrint !$= "")
			cancel(CenterPrint.removePrint);
	}
	else {
		CenterPrint.visible = 1;
		$centerPrintActive = 1;
	}
	
	if(%append)
		%text = CenterPrintText.getText() @ %message;
	else
		%text = "<just:center>" @ %message;
		
	CenterPrintText.setText(replaceBindVars(%text));		

	if (%time > 0)
		CenterPrint.removePrint = schedule( ( %time * 1000 ), 0, "clientCmdClearCenterPrint" );
}

// time is specified in seconds
function clientCmdBottomPrint( %message, %time )
{
	if($bottomPrintActive)
	{
		if( BottomPrint.removePrint !$= "")
			cancel(BottomPrint.removePrint);
	}
	else
	{
		BottomPrint.setVisible(true);
		$bottomPrintActive = 1;
	}

	// if bottomprint already visible, change text and reset time...
	if($bottomPrintActive && bottomPrintText.getText() !$= "" )
		%text = "<just:center>" @ %message @ "\n\n" @ bottomPrintText.getText();
	else
		%text = "<just:center>" @ %message;
		
	bottomPrintText.setText(replaceBindVars(%text));				

	if(%time > 0)
		BottomPrint.removePrint = schedule( %time*1000, 0, "clientCmdClearbottomPrint" );
}

function CenterPrintText::onResize(%this, %width, %height)
{
	%this.position = "20 15";
}

//-------------------------------------------------------------------------------------------------------

function clientCmdClearCenterPrint()
{
	$centerPrintActive = 0;
	CenterPrint.visible = 0;
	CenterPrint.removePrint = "";
}

function clientCmdClearBottomPrint()
{
	$bottomPrintActive = 0;
	BottomPrintText.setText("");
	BottomPrint.visible = 0;
	BottomPrint.removePrint = "";
}
