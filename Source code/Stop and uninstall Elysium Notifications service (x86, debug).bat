@echo off
set OriginalPATH=%PATH%
set PATH=%OriginalPATH%;%windir%\Microsoft.NET\Framework\v4.0.30319
chdir /d %~dp0
cd ..\
@echo on
net stop ElysiumNotifications1.5.14.0
installutil /u "%CD%\Binary\Debug\x86\Elysium.Notifications.Server.exe"
pause