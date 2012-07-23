@echo off
set OriginalPATH=%PATH%
set PATH=%OriginalPATH%;%windir%\Microsoft.NET\Framework64\v4.0.30319
chdir /d %~dp0
cd ..\
@echo on
installutil "%CD%\Binary\Elysium for .NET 4\Debug\x64\Elysium.Notifications.Server.exe"
net start ElysiumNotifications
pause