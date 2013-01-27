//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - profiles.cs
// GuiControlProfiles for Revenge Of The Cats' shell
//------------------------------------------------------------------------------

new AudioProfile(GuiSoundButtonDown)
{
	filename = "share/sounds/rotc/damage1.wav";
	description = AudioGui;
	preload = true;
};

new AudioProfile(GuiSoundButtonOver)
{
	filename = "share/sounds/rotc/weaponEmpty.wav";
	description = AudioGui;
	preload = true;
};

new GuiControlProfile(GuiDefaultProfile)
{
	tab = false;
	canKeyFocus = false;
	hasBitmapArray = false;
	mouseOverSelected = false;

	// fill color
	opaque = false;
	fillColor = "0 0 0 220";
	fillColorHL = "50 50 50 220";
	fillColorNA = "221 202 173 220";

	// border color
	border = false;
	borderColor	= "100 100 100 220";
	borderColorHL = "100 100 100 220";
	borderColorNA = "100 100 100 220";

	// font
	fontType = "NovaSquare";
	fontSize = 14;

	fontColor = "200 200 200";
	fontColorHL = "0 255 0";
	fontColorNA = "100 100 100";
	fontColorSEL= "200 200 200";
	fontColors[4] = "255 96 96"; // aka fontColorLink
	fontColors[5] = "0 0 255"; // aka fontColorLinkHL
	fontColors[6] = "0 200 0 255";
	fontColors[7] = "200 0 200 255";
	fontColors[8] = "255 200 0 255";
	fontColors[9] = "0 200 255 255";

	// bitmap information
	bitmap = "./pixmaps/mg_window6";
	bitmapBase = "";
	textOffset = "0 0";

	// used by guiTextControl
	modal = true;
	justify = "left";
	autoSizeWidth = false;
	autoSizeHeight = false;
	returnTab = false;
	numbersOnly = false;
	cursorColor = "200 200 200";

	// sounds
	soundButtonDown = GuiSoundButtonDown;
	soundButtonOver = GuiSoundButtonOver;
};

//--------------------------------------------------------------------------
// Console Window
//
new GuiControlProfile(GuiConsoleProfile : GuiDefaultProfile)
{
	fontType = "Lucida Console";
	fontSize = 12;
	fontColor = "0 0 255";
	fontColorHL = "130 130 130";
	fontColorNA = "255 0 0";
	fontColors[6] = "50 50 50";
	fontColors[7] = "50 50 0";
	fontColors[8] = "0 0 50";
	fontColors[9] = "0 50 0";
};
//--------------------------------------------------------------------------

new GuiControlProfile(GuiButtonProfile : GuiDefaultProfile)
{
	opaque = false;
	border = true;
	fixedExtent = true;
	justify = "center";
	canKeyFocus = false;
	bitmap = "./pixmaps/mg_button6";
};

new GuiControlProfile(GuiHilightButtonProfile : GuiButtonProfile)
{
	bitmap = "./pixmaps/mg_button6hilight";
};

new GuiControlProfile(GuiTitleButtonProfile : GuiButtonProfile)
{
	bitmap = "./pixmaps/mg_button6title";
};

new GuiControlProfile(GuiRootMenuButtonProfile : GuiDefaultProfile)
{
	opaque = false;
	border = true;
	fixedExtent = true;
	justify = "center";
	canKeyFocus = false;
};

new GuiControlProfile(GuiBorderButtonProfile : GuiDefaultProfile)
{
	fontColorHL = "0 0 0";
};

new GuiControlProfile(GuiMenuBarProfile : GuiDefaultProfile)
{
	opaque = true;
	border = 4;
	fixedExtent = true;
	justify = "center";
	canKeyFocus = false;
	mouseOverSelected = true;
	bitmap = ($platform $= "macos") ? "./pixmaps/osxMenu" : "./pixmaps/torqueMenu";
	hasBitmapArray = true;
};

new GuiControlProfile(GuiTextProfile : GuiDefaultProfile)
{
	autoSizeWidth = true;
	autoSizeHeight = true;
};

new GuiControlProfile(GuiMediumTextProfile : GuiTextProfile)
{
	fontSize = 24;
};

new GuiControlProfile(GuiBigTextProfile : GuiTextProfile)
{
	fontSize = 36;
};

new GuiControlProfile(GuiCenterTextProfile : GuiTextProfile)
{
	justify = "center";
};


new GuiControlProfile(GuiTextEditProfile : GuiDefaultProfile)
{
	opaque = true;
	border = 3;
	borderThickness = 2;
	borderColor = "0 0 0";
	textOffset = "0 2";
	autoSizeWidth = false;
	autoSizeHeight = true;
	tab = true;
	canKeyFocus = true;
};

new GuiControlProfile(GuiControlListPopupProfile : GuiDefaultProfile)
{
	opaque = true;
	border = true;
	borderColor = "0 0 0";
	textOffset = "0 2";
	autoSizeWidth = false;
	autoSizeHeight = true;
	tab = true;
	canKeyFocus = true;
	bitmap = ($platform $= "macos") ? "./pixmaps/osxScroll" : "./pixmaps/darkScroll";
	hasBitmapArray = true;
};

new GuiControlProfile(GuiTextArrayProfile : GuiTextProfile)
{
	dummyField = true;
};

new GuiControlProfile(GuiTextListProfile : GuiTextProfile) ;

new GuiControlProfile(GuiTreeViewProfile : GuiDefaultProfile)
{
	fontSize = 13;  // dhc - trying a better fit...
	bitmap = "common/ui/shll_treeView";
};

new GuiControlProfile(GuiPaneProfile : GuiDefaultProfile)
{
	bitmap = "./pixmaps/simplePane";
	hasBitmapArray = true;
};

new GuiControlProfile(GuiPopUpMenuProfile : GuiDefaultProfile)
{
	opaque = true;
	mouseOverSelected = true;

	border = 4;
	borderThickness = 2;
	borderColor = "0 0 0";
	fixedExtent = true;
	justify = "center";
	bitmap = ($platform $= "macos") ? "./pixmaps/osxScroll" : "./pixmaps/darkScroll";
	hasBitmapArray = true;
};


new GuiControlProfile(LoadTextProfile : GuiDefaultProfile)
{
	autoSizeWidth = true;
	autoSizeHeight = true;
};


new GuiControlProfile(GuiMLTextProfile : GuiDefaultProfile)
{
	fillColor = "0 0 0 0";
	fontColorLink = "0 150 0";
	fontColorLinkHL = "100 255 100";
};

new GuiControlProfile(GuiMLTextNoSelectProfile : GuiDefaultProfile)
{
	fillColor = "0 0 0 0";
	modal = false;
};

new GuiControlProfile(GuiMLTextEditProfile : GuiDefaultProfile)
{
	fillColor = "0 0 0 0";
	fontColorLink = "0 150 0";
	fontColorLinkHL = "100 255 100";
	autoSizeWidth = true;
	autoSizeHeight = true;
	tab = true;
	canKeyFocus = true;
};

new GuiControlProfile(GuiProgressProfile : GuiDefaultProfile)
{
	opaque = false;
	fillColor = "44 152 162 100";
	border = true;
};

new GuiControlProfile(GuiProgressTextProfile : GuiDefaultProfile)
{
	justify = "center";
};

new GuiControlProfile(GuiBitmapBorderProfile : GuiDefaultProfile)
{
	bitmap = "./darkBorder";
	hasBitmapArray = true;
};

new GuiControlProfile ( GuiDirectoryTreeProfile : GuiTreeViewProfile )
{
	bitmap = "common/ui/shll_treeView";
};

new GuiControlProfile ( GuiDirectoryFileListProfile : GuiTreeViewProfile )
{
	fillColor = "50 50 50 220";
};


//-----------------------------------------------------------------------

new GuiControlProfile(GuiWindowProfile : GuiDefaultProfile)
{
	canKeyFocus = true;
	opaque = false;
	border = 8;
	fillColor = "0 0 0 220";
	fillColorHL = "0 0 0 220";
	fillColorNA = "0 0 0 220";
	fontColor = "100 255 100";
	fontColorHL = "100 255 100";
	text = "GuiWindowCtrl test";
	bitmap = "./pixmaps/mg_window6";
	textOffset = "12 6";
	hasBitmapArray = true;
	justify = "left";
};

new GuiControlProfile(GuiInactiveWindowProfile : GuiDefaultProfile)
{
	canKeyFocus = true;
	opaque = false;
	border = 8;
	fillColor = "0 0 0 220";
	fillColorHL = "0 0 0 220";
	fillColorNA = "0 0 0 220";
	fontColor = "170 170 170";
	fontColorHL = "170 170 170";
	text = "GuiWindowCtrl test";
	bitmap = "./pixmaps/mg_window6inactive";
	textOffset = "12 6";
	hasBitmapArray = true;
	justify = "left";
};

new GuiControlProfile(GuiTransparentWindowProfile : GuiDefaultProfile)
{
	opaque = true;
	border = 0;
	fillColor = "0 0 0 220";
	fillColorHL = "0 0 0 220";
	fillColorNA = "0 0 0 220";
	fontColor = "255 255 255";
	fontColorHL = "255 255 255";
	text = "GuiWindowCtrl test";
	bitmap = "./pixmaps/mg_window4";
	textOffset = "6 6";
	hasBitmapArray = true;
	justify = "center";
};

new GuiControlProfile(GuiScrollProfile : GuiDefaultProfile)
{
	opaque = false;
	border = 3;
	borderThickness = 3;
	bitmap = "./pixmaps/simpleScrollTransparent";
	hasBitmapArray = true;
	fillColor = "0 0 0 0";
};

new GuiControlProfile(MissionWindowQuickbarScrollProfile : GuiScrollProfile)
{
	border = 0;
	borderThickness = 0;
};

new GuiControlProfile(MissionWindowMenuScrollProfile : GuiScrollProfile)
{
	border = 1;
	borderThickness = 5;
};

new GuiControlProfile(GuiCheckBoxProfile : GuiDefaultProfile)
{
	opaque = false;
	border = false;
	fixedExtent = true;
	justify = "left";
	bitmap = "./pixmaps/simpleCheckTransparent";
	hasBitmapArray = true;
};

new GuiControlProfile(GuiRadioProfile : GuiDefaultProfile)
{
	fixedExtent = true;
	bitmap = "./pixmaps/simpleRadioTransparent";
	hasBitmapArray = true;
};

