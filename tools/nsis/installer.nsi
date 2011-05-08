!include MUI2.nsh ;include modern UI

!define VERSION    "prototype1.10"
!define SHORTNAME  "rotc-ethernet"
!define TECHNAME   "rotc-ethernet-${VERSION}"
!define PRETTYNAME "Revenge of the Cats: Ethernet"

Name "${PRETTYNAME}"
Outfile "${TECHNAME}-win32.exe"

InstallDir "$PROGRAMFILES\${SHORTNAME}"
InstallDirRegKey HKCU "Software\${SHORTNAME}" "installDir"

RequestExecutionLevel user

;--------------------------------
; Variables

  Var StartMenuFolder

;--------------------------------
;Interface Settings

  !define MUI_ABORTWARNING
;  !define MUI_ICON "rotc.ico"
  !define MUI_HEADERIMAGE_BITMAP "rotc_install.bmp"
  !define MUI_WELCOMEPAGE_TEXT "This will install ${PRETTYNAME} on your computer.$\nThis installer unpacks data to one folder, adds one registry entry and optionally adds an entry to your start menu.$\n$\nFor more information, visit our website at http://ethernet.wasted.ch"

  !define MUI_FINISHPAGE_RUN $INSTDIR\rotc.exe
  !define MUI_FINISHPAGE_LINK "http://ethernet.wasted.ch"

  !define MUI_UNCONFIRMPAGE_TEXT_TOP "!!!WARNING!!! THIS UNINSTALLER DELETES THE WHOLE GAME FOLDER. IT WILL NOT CHECK IF FILES ARE PRESENT THAT WERE ADDED LATER. If you have copied files into the game folder yourself, please do not use this installer, or backup the files before using this uninstaller."

;--------------------------------
;Pages

  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_DIRECTORY

  ;Start Menu Folder Page Configuration
  !define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU" 
  !define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\${SHORTNAME}"
  !define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "startMenuFolder"

  
  !insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder

  !insertmacro MUI_PAGE_INSTFILES
  !insertmacro MUI_PAGE_FINISH
  
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  
;--------------------------------
;Languages
 
  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "Dummy Section" SecDummy

  SetOutPath "$INSTDIR"
  
  ;ADD YOUR OWN FILES HERE...
  File /r "..\..\*"
  
  ;Store installation folder
  WriteRegStr HKCU "Software\rotc-ethernet" "installDir" $INSTDIR
  WriteRegStr HKCU "Software\rotc-ethernet" "version" "${VERSION}"
  
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    
    ;Create shortcuts
    CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\rotc-ethernet.lnk" "$INSTDIR\rotc.exe"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\manual.lnk" "$INSTDIR\manual.html"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\uninstall.lnk" "$INSTDIR\uninstall.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_END


  ;Create uninstaller
  WriteUninstaller "$INSTDIR\uninstall.exe"

SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} "A test section."

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section

Section "Uninstall"

  ;ADD YOUR OWN FILES HERE...

  RMDir /r $INSTDIR

  !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder
  Delete "$SMPROGRAMS\$StartMenuFolder\rotc-ethernet.lnk"
  Delete "$SMPROGRAMS\$StartMenuFolder\manual.lnk"
  Delete "$SMPROGRAMS\$StartMenuFolder\uninstall.lnk"
  RMDIR "$SMPROGRAMS\$StartMenuFolder"


  DeleteRegValue HKCU "Software\${SHOFTNAME}" "installDir"
  DeleteRegValue HKCU "Software\${SHOFTNAME}" "version"
  DeleteRegKey /ifempty HKCU "Software\${SHORTNAME}"

SectionEnd

