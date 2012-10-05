@echo off
set OriginalPATH=%PATH%
set PATH=%OriginalPATH%;%windir%\Microsoft.NET\Framework64\v4.0.30319
chdir /d %~dp0
cd ..\
@echo on
installutil "%CD%\Binary\Debug\x64\Elysium.Notifications.Server.exe"
net start ElysiumNotifications1.5.16.0
pause