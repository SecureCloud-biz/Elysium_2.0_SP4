@echo off

IF /I "%1" == "NETFX4" (
  set Framework=NETFX4
  set FrameworkName=.NET Framework 4
) ELSE (
  IF /I "%1" == "NETFX45" (
    set Framework=NETFX45
    set FrameworkName=.NET Framework 4.5
) ELSE goto showerror
)
IF NOT "%2" == "" goto usage

call "Load tools.bat" %Framework%
IF ERRORLEVEL 1 goto showerror

chdir /d %~dp0
del "..\Deploy\%FrameworkName%\SDK\Elysium SDK (x86).zip"
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\%FrameworkName%\SDK\Elysium SDK (x86).zip" "..\Binary\%FrameworkName%\Debug\AnyCPU\Elysium.dll" "..\Binary\%FrameworkName%\Debug\AnyCPU\Elysium.pdb" "..\Binary\%FrameworkName%\Release\AnyCPU\Elysium.Design.11.0.dll" "..\Binary\%FrameworkName%\Debug\AnyCPU\Elysium.Notifications.dll" "..\Binary\%FrameworkName%\Debug\AnyCPU\Elysium.Notifications.pdb" "..\Binary\%FrameworkName%\Release\x86\Elysium.Notifications.Server.exe" "..\Binary\%FrameworkName%\Release\x86\Elysium.Notifications.Server.exe.config" "..\Binary\%FrameworkName%\Release\x86\Elysium.Test.exe" ".\SDK\ZIP\Tools\%FrameworkName%\Run Elysium Notifications service.bat" ".\SDK\ZIP\Tools\%FrameworkName%\Stop Elysium Notifications service.bat" ".\SDK\ZIP\Tools\%FrameworkName%\x86\Install Elysium Notifications service.bat" ".\SDK\ZIP\Tools\%FrameworkName%\x86\Uninstall Elysium Notifications service.bat"
IF ERRORLEVEL 1 goto showerror
IF /I "%FrameworkName%" == "NETFX4" (
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\%FrameworkName%\SDK\Elysium SDK (x86).zip" "..\Binary\%FrameworkName%\Release\AnyCPU\Elysium.Design.10.0.dll"
)
IF /I "%FrameworkName%" == "NETFX45" (
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\%FrameworkName%\SDK\Elysium SDK (x86).zip" "..\Binary\Release\x86\Elysium.Test.exe.config"
)
IF ERRORLEVEL 1 goto showerror

cd "..\Binary\%FrameworkName%\Documentation"
"..\..\..\Tools and Resources\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\SDK\Elysium SDK (x86).zip" "en\*" "ru\*"
IF ERRORLEVEL 1 goto showerror

chdir /d %~dp0
cd "..\Tools and Resources\Assembly dependencies\%FrameworkName%\"
"..\..\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\SDK\Elysium SDK (x86).zip" "Microsoft.Expression.Drawing.dll" "Microsoft.Expression.Drawing.xml" "Design\*" "en\*"
IF ERRORLEVEL 1 goto showerror
IF /I "%FrameworkName%" == "NETFX4" (
"..\..\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\SDK\Elysium SDK (x86).zip" "Microsoft.Windows.Shell.dll" "Microsoft.Windows.Shell.pdb" "Microsoft.Windows.Shell.xml"
) ELSE IF "%FrameworkName%" == "NETFX45" (
"..\..\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\SDK\Elysium SDK (x86).zip" "ru\*"
)
IF ERRORLEVEL 1 goto showerror

chdir /d %~dp0
del "..\Deploy\%FrameworkName%\SDK\Elysium SDK (x64).zip"
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\%FrameworkName%\SDK\Elysium SDK (x64).zip" "..\Binary\%FrameworkName%\Debug\AnyCPU\Elysium.dll" "..\Binary\%FrameworkName%\Debug\AnyCPU\Elysium.pdb" "..\Binary\%FrameworkName%\Release\AnyCPU\Elysium.Design.11.0.dll" "..\Binary\%FrameworkName%\Debug\AnyCPU\Elysium.Notifications.dll" "..\Binary\%FrameworkName%\Debug\AnyCPU\Elysium.Notifications.pdb" "..\Binary\%FrameworkName%\Release\x64\Elysium.Notifications.Server.exe" "..\Binary\%FrameworkName%\Release\x64\Elysium.Notifications.Server.exe.config" "..\Binary\%FrameworkName%\Release\x64\Elysium.Test.exe" ".\SDK\ZIP\Tools\%FrameworkName%\Run Elysium Notifications service.bat" ".\SDK\ZIP\Tools\%FrameworkName%\Stop Elysium Notifications service.bat" ".\SDK\ZIP\Tools\%FrameworkName%\x64\Install Elysium Notifications service.bat" ".\SDK\ZIP\Tools\%FrameworkName%\x64\Uninstall Elysium Notifications service.bat"
IF ERRORLEVEL 1 goto showerror
IF /I "%FrameworkName%" == "NETFX4" (
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\%FrameworkName%\SDK\Elysium SDK (x64).zip" "..\Binary\%FrameworkName%\Release\AnyCPU\Elysium.Design.10.0.dll"
)
IF /I "%FrameworkName%" == "NETFX45" (
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\%FrameworkName%\SDK\Elysium SDK (x64).zip" "..\Binary\Release\x64\Elysium.Test.exe.config"
)
IF ERRORLEVEL 1 goto showerror

cd "..\Binary\%FrameworkName%\Documentation"
"..\..\..\Tools and Resources\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\SDK\Elysium SDK (x64).zip" "en\*" "ru\*"
IF ERRORLEVEL 1 goto showerror

chdir /d %~dp0
cd "..\Tools and Resources\Assembly dependencies\%FrameworkName%\"
"..\..\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\SDK\Elysium SDK (x64).zip" "Microsoft.Expression.Drawing.dll" "Microsoft.Expression.Drawing.xml" "Design\*" "en\*"
IF ERRORLEVEL 1 goto showerror
IF /I "%FrameworkName%" == "NETFX4" (
"..\..\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\SDK\Elysium SDK (x64).zip" "Microsoft.Windows.Shell.dll" "Microsoft.Windows.Shell.pdb" "Microsoft.Windows.Shell.xml"
) ELSE IF "%FrameworkName%" == "NETFX45" (
"..\..\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\SDK\Elysium SDK (x64).zip" "ru\*"
)
IF ERRORLEVEL 1 goto showerror

chdir /d %~dp0
del "..\Deploy\%FrameworkName%\Runtime\Elysium Runtime (x86).zip"
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\%FrameworkName%\Runtime\Elysium Runtime (x86).zip" "..\Binary\%FrameworkName%\Release\AnyCPU\Elysium.dll" "..\Binary\%FrameworkName%\Release\AnyCPU\Elysium.Notifications.dll" "..\Binary\%FrameworkName%\Release\x86\Elysium.Notifications.Server.exe" "..\Binary\%FrameworkName%\Release\x86\Elysium.Notifications.Server.exe.config"
IF ERRORLEVEL 1 goto showerror

cd "..\Tools and Resources\Assembly dependencies\%FrameworkName%\"
"..\..\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\Runtime\Elysium Runtime (x86).zip" "Microsoft.Expression.Drawing.dll"
IF ERRORLEVEL 1 goto showerror
IF /I "%FrameworkName%" == "NETFX4" (
"..\..\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\Runtime\Elysium Runtime (x86).zip" "Microsoft.Windows.Shell.dll"
)
IF ERRORLEVEL 1 goto showerror

chdir /d %~dp0
del "..\Deploy\%FrameworkName%\Runtime\Elysium Runtime (x64).zip"
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\%FrameworkName%\Runtime\Elysium Runtime (x64).zip" "..\Binary\%FrameworkName%\Release\AnyCPU\Elysium.dll" "..\Binary\%FrameworkName%\Release\AnyCPU\Elysium.Notifications.dll" "..\Binary\%FrameworkName%\Release\x64\Elysium.Notifications.Server.exe" "..\Binary\%FrameworkName%\Release\x64\Elysium.Notifications.Server.exe.config"
IF ERRORLEVEL 1 goto showerror

cd "..\Tools and Resources\Assembly dependencies\%FrameworkName%\"
"..\..\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\Runtime\Elysium Runtime (x64).zip" "Microsoft.Expression.Drawing.dll"
IF ERRORLEVEL 1 goto showerror
IF /I "%FrameworkName%" == "NETFX4" (
"..\..\Utilities\7za\7za.exe" u "..\..\..\Deploy\%FrameworkName%\Runtime\Elysium Runtime (x64).zip" "Microsoft.Windows.Shell.dll"
)
IF ERRORLEVEL 1 goto showerror

chdir /d %~dp0
del "..\Deploy\%FrameworkName%\SDK\Setup (x86).exe"
copy "..\Binary\%FrameworkName%\Release\x86\SDK\MSI\Setup.exe" "..\Deploy\%FrameworkName%\SDK\Setup (x86).exe"
del "..\Deploy\%FrameworkName%\SDK\Installer (en-us, x86).msi"
copy "..\Binary\%FrameworkName%\Release\x86\SDK\MSI\en-us\Installer.msi" "..\Deploy\%FrameworkName%\SDK\Installer (en-us, x86).msi"
del "..\Deploy\%FrameworkName%\SDK\Installer (ru-ru, x86).msi"
copy "..\Binary\%FrameworkName%\Release\x86\SDK\MSI\ru-ru\Installer.msi" "..\Deploy\%FrameworkName%\SDK\Installer (ru-ru, x86).msi"
del "..\Deploy\%FrameworkName%\SDK\Setup (x64).exe"
copy "..\Binary\%FrameworkName%\Release\x64\SDK\MSI\Setup.exe" "..\Deploy\%FrameworkName%\SDK\Setup (x64).exe"
del "..\Deploy\%FrameworkName%\SDK\Installer (en-us, x64).msi"
copy "..\Binary\%FrameworkName%\Release\x64\SDK\MSI\en-us\Installer.msi" "..\Deploy\%FrameworkName%\SDK\Installer (en-us, x64).msi"
del "..\Deploy\%FrameworkName%\SDK\Installer (ru-ru, x64).msi"
copy "..\Binary\%FrameworkName%\Release\x64\SDK\MSI\ru-ru\Installer.msi" "..\Deploy\%FrameworkName%\SDK\Installer (ru-ru, x64).msi"
del "..\Deploy\%FrameworkName%\Runtime\Installer (x86).msi"
copy "..\Binary\%FrameworkName%\Release\x86\Runtime\MSI\Installer.msi" "..\Deploy\%FrameworkName%\Runtime\Installer (x86).msi"
del "..\Deploy\%FrameworkName%\Runtime\Installer (x64).msi"
copy "..\Binary\%FrameworkName%\Release\x64\Runtime\MSI\Installer.msi" "..\Deploy\%FrameworkName%\Runtime\Installer (x64).msi"

goto :eof

:usage
echo Error in script usage. The correct usage is:
echo     %0 [framework]
echo where [framework] is: NETFX4 ^| NETFX45
echo:
echo For example:
echo     %0 NETFX45
pause
goto :eof

:showerror
echo Build error occurred
pause