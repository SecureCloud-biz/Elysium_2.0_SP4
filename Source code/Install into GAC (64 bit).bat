@echo off
set PATH=%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools
chdir /d %~dp0
cd ..\
@echo on
gacutil /i "%CD%\Tools and Resources\Assembly dependencies\Microsoft.Expression.Drawing.dll"
gacutil /i "%CD%\Tools and Resources\Assembly dependencies\Microsoft.Expression.Interactions.dll"
gacutil /i "%CD%\Tools and Resources\Assembly dependencies\Microsoft.Windows.Shell.dll"
gacutil /i "%CD%\Tools and Resources\Assembly dependencies\System.Windows.Interactivity.dll"
pause