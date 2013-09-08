
@echo off
set OriginalPATH=%PATH%
set PATH=%OriginalPATH%;%windir%\Microsoft.NET\Framework\v4.0.30319
chdir /d %~dp0
cd ..\..\..\..\
@echo on
installutil "%CD%\Binary\.NET Framework 4\Debug\x86\Elysium.Notifications.Server.exe"
net start ElysiumNotifications-v2.0.58.3-v4.0
pause