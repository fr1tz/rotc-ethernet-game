//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - profiles.cs
// GuiControlProfiles for Revenge Of The Cats' shell
//------------------------------------------------------------------------------

new GuiControlProfile(GuiDefaultProfile)
{
	tab = false;
	canKeyFocus = false;
	hasBitmapArray = false;
	mouseOverSelected = false;

	// fill color
	opaque = false;
	fillColor = "130 130 130";
	fillColorHL = "50 50 50";
	fillColorNA = "221 202 173";

	// border color
	border = false;
	borderColor	= "100 100 100";
	borderColorHL = "100 100 100";
	borderColorNA = "100 100 100";

	// font
	fontType = "NovaSquare";
	fontSize = 14;

	fontColor = "0 0 0";
	fontColorHL = "100 100 100";
	fontColorNA = "100 100 100";
	fontColorSEL= "200 200 200";

	// bitmap information
	bitmap = "./pixmaps/mg_window4";
	bitmapBase = "";
	textOffset = "0 0";

	// used by guiTextControl
	modal = true;
	justify = "left";
	autoSizeWidth = false;
	autoSizeHeight = false;
	returnTab = false;
	numbersOnly = false;
	cursorColor = "0 0 0 255";

	// sounds
	soundButtonDown = "";
	soundButtonOver = "";
};

//--------------------------------------------------------------------------
// Console Window
//
new GuiControlProfile(GuiConsoleProfile : GuiDefaultProfile)
{
	//fontType = "Lucida Console";
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
	opaque = true;
	border = true;
	fontColor = "0 0 0";
	fontColorHL = "255 255 255";
	borderColor = "100 100 100";
	fixedExtent = true;
	justify = "center";
	canKeyFocus = false;
};

new GuiControlProfile(GuiRootMenuButtonProfile : GuiDefaultProfile)
{
	opaque = true;
	border = true;
	fontColor = "0 0 0";
	fontColorHL = "255 255 255";
	borderColor = "255 255 255";
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
	fillColorHL = "255 150 0";
	border = 4;
	fontColor = "0 0 0";
	fontColorHL = "255 255 255";
	fontColorNA = "64 64 64";
	fixedExtent = true;
	justify = "center";
	canKeyFocus = false;
	mouseOverSelected = true;
	bitmap = ($platform $= "macos") ? "./pixmaps/osxMenu" : "./pixmaps/torqueMenu";
	hasBitmapArray = true;
};

new GuiControlProfile(GuiTextProfile : GuiDefaultProfile)
{
	fontColor = "0 0 0";
	fontColorLink = "255 96 96";
	fontColorLinkHL = "0 0 255";
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
	fillColor = "255 255 255";
	fillColorHL = "128 128 128";
	border = 3;
	borderThickness = 2;
	borderColor = "0 0 0";
	fontColor = "0 0 0";
	fontColorHL = "255 255 255";
	fontColorNA = "128 128 128";
	textOffset = "0 2";
	autoSizeWidth = false;
	autoSizeHeight = true;
	tab = true;
	canKeyFocus = true;
};

new GuiControlProfile(GuiControlListPopupProfile : GuiDefaultProfile)
{
	opaque = true;
	fillColor = "255 255 255";
	fillColorHL = "128 128 128";
	border = true;
	borderColor = "0 0 0";
	fontColor = "0 0 0";
	fontColorHL = "255 255 255";
	fontColorNA = "128 128 128";
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
	fontColorHL = "32 100 100";
	fillColorHL = "200 200 200";
};

new GuiControlProfile(GuiTextListProfile : GuiTextProfile) ;

new GuiControlProfile(GuiTreeViewProfile : GuiDefaultProfile)
{
	fontSize = 13;  // dhc - trying a better fit...
	fontColor = "0 0 0";
	fontColorHL = "64 150 150";
	bitmap = "common/ui/shll_treeView";
};

new GuiControlProfile(GuiPopUpMenuProfile : GuiDefaultProfile)
{
	opaque = true;
	mouseOverSelected = true;

	border = 4;
	borderThickness = 2;
	borderColor = "0 0 0";
	fontColor = "0 0 0";
	fontColorHL = "32 100 100";
	fontColorSEL = "32 100 100";
	fixedExtent = true;
	justify = "center";
	bitmap = ($platform $= "macos") ? "./pixmaps/osxScroll" : "./pixmaps/darkScroll";
	hasBitmapArray = true;
};


new GuiControlProfile(LoadTextProfile : GuiDefaultProfile)
{
	fontColor = "66 219 234";
	autoSizeWidth = true;
	autoSizeHeight = true;
};


new GuiControlProfile(GuiMLTextProfile : GuiDefaultProfile)
{
	fontColorLink = "200 0 200";
	fontColorLinkHL = "255 0 255";
};

new GuiControlProfile(GuiMLTextNoSelectProfile : GuiDefaultProfile)
{
	fontColorLink = "220 220 220";
	fontColorLinkHL = "255 255 255";
	modal = false;
};

new GuiControlProfile(GuiMLTextEditProfile : GuiDefaultProfile)
{
	fontColorLink = "255 96 96";
	fontColorLinkHL = "0 0 255";

	fillColor = "255 255 255";
	fillColorHL = "128 128 128";

	fontColor = "0 0 0";
	fontColorHL = "255 255 255";
	fontColorNA = "128 128 128";

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
	borderColor	= "78 88 120";
};

new GuiControlProfile(GuiProgressTextProfile : GuiDefaultProfile)
{
	fontColor = "0 0 0";
	justify = "center";
};

new GuiControlProfile(GuiBitmapBorderProfile : GuiDefaultProfile)
{
	bitmap = "./darkBorder";
	hasBitmapArray = true;
};

//-----------------------------------------------------------------------

new GuiControlProfile(GuiWindowProfile : GuiDefaultProfile)
{
	opaque = true;
	border = 2;
	fillColor = "130 130 130";
	fillColorHL = "221 202 173";
	fillColorNA = "221 202 173";
	fontColor = "255 255 255";
	fontColorHL = "255 255 255";
	text = "GuiWindowCtrl test";
	bitmap = "./pixmaps/mg_window4";
	textOffset = "6 6";
	hasBitmapArray = true;
	justify = "center";
};

new GuiControlProfile(GuiTransparentWindowProfile : GuiDefaultProfile)
{
	opaque = true;
	border = 0;
	fillColor = "130 130 130 230";
	fillColorHL = "221 202 173 230";
	fillColorNA = "221 202 173 230";
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
	opaque = true;
	fillColor = "255 255 255";
	border = 3;
	borderThickness = 2;
	borderColor = "0 0 0";
	bitmap = "./pixmaps/simpleScroll";
	hasBitmapArray = true;
};

new GuiControlProfile(GuiCheckBoxProfile : GuiDefaultProfile)
{
	opaque = false;
	fillColor = "232 232 232";
	border = false;
	borderColor = "0 0 0";
	fontColor = "0 0 0";
	fontColorHL = "32 100 100";
	fixedExtent = true;
	justify = "left";
	bitmap = "./pixmaps/simpleCheck";
	hasBitmapArray = true;
};

new GuiControlProfile(GuiRadioProfile : GuiDefaultProfile)
{
	fillColor = "232 232 232";
	fontColorHL = "32 100 100";
	fixedExtent = true;
	bitmap = "./pixmaps/simpleRadio";
	hasBitmapArray = true;
};

