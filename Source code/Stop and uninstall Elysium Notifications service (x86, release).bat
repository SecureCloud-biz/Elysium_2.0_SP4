@echo off
set OriginalPATH=%PATH%
set PATH=%OriginalPATH%;%windir%\Microsoft.NET\Framework\v4.0.30319
chdir /d %~dp0
cd ..\
@echo on
net stop ElysiumNotifications
installutil /u "%CD%\Binary\Elysium for .NET 4\Release\x86\Elysium.Notifications.Server.exe"
pause