@echo off

set VCEssentialExist=0

set currentProgramFiles="%ProgramFiles%"
IF NOT ""%1"" EQU """" set currentProgramFiles=%1

rem set the directory where the batch file is located
set cmdFileFolder=%~dp0

set outputFolder=%cmdFileFolder%Output\AnyCPU
set mainExeFile=VisualComponents.Engine.exe

set VCEssentialsFolderName=Visual Components Essentials 4.0
set VCPremiumFolderName=Visual Components Premium 4.0
set VCProfessionalFolderName=Visual Components Professional 4.0
set VCEssentialsDevFolderName=Visual Components Essentials 4.0 (DevBuild)

rem Check development version first and take this version
call :CopyVCEssential "%VCEssentialsFolderName%" "%cmdFileFolder%..\Output\Debug\win32"     Yes
call :CopyVCEssential "%VCEssentialsFolderName%" "%cmdFileFolder%..\Output\Debug\x64"       Yes
call :CopyVCEssential "%VCEssentialsFolderName%" "%cmdFileFolder%..\Output\Release\win32"   Yes
call :CopyVCEssential "%VCEssentialsFolderName%" "%cmdFileFolder%..\Output\Release\x64"     Yes

rem Check installed versions
call :CopyVCEssential "%VCPremiumFolderName%"        %currentProgramFiles%\"Visual Components\%VCPremiumFolderName%"
call :CopyVCEssential "%VCProfessionalFolderName%"   %currentProgramFiles%\"Visual Components\%VCProfessionalFolderName%"
call :CopyVCEssential "%VCEssentialsFolderName%"     %currentProgramFiles%\"Visual Components\%VCEssentialsFolderName%"
call :CopyVCEssential "%VCEssentialsDevdFolderName%" %currentProgramFiles%\"Visual Components\%VCEssentialsDevFolderName%"


goto :END

:CopyVCEssential %1 %2
set folderName=%1
set sourcePath=%2
IF %VCEssentialExist%==1 goto :EOF
IF NOT EXIST "%outputFolder%" mkdir "%outputFolder%"
IF NOT EXIST "%outputFolder%\%mainExeFile%" (
    IF EXIST %sourcePath%\%mainExeFile% (
        set VCEssentialExist=1
        echo.
        echo Copying %folderName% version
        echo from %sourcePath% folder
        echo to   "%outputFolder%" folder:
        echo.
        rem copy the exe file first
        copy /Y %sourcePath%\%mainExeFile% "%outputFolder%"
        rem copy all rest files
        xcopy %sourcePath% "%outputFolder%\*.*" /S /Q /Y
    )
) ELSE (
    echo Output executable present, copying %folderName% is not needed.
    set VCEssentialExist=1
)
goto :EOF


:END
rem pause

:EOF
