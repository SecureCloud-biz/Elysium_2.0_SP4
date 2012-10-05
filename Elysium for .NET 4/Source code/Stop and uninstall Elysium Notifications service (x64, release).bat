@echo off
set OriginalPATH=%PATH%
set PATH=%OriginalPATH%;%windir%\Microsoft.NET\Framework64\v4.0.30319
chdir /d %~dp0
cd ..\
@echo on
net stop ElysiumNotifications-v1.5.17.0-v4.0
installutil /u "%CD%\Binary\Release\x64\Elysium.Notifications.Server.exe"
pause