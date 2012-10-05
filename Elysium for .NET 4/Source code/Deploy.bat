@echo off

IF %PROCESSOR_ARCHITECTURE% == AMD64 goto x64
ELSE goto x86

:x86
IF EXIST "%ProgramFiles%\Microsoft Visual Studio 11.0" (
  call "%ProgramFiles%\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" x86
) ELSE IF EXIST "%ProgramFiles%\Microsoft Visual Studio 10.0" (
  call "%ProgramFiles%\Microsoft Visual Studio 10.0\VC\vcvarsall.bat" x86
)

:x64
IF EXIST "%ProgramFiles(x86)%\Microsoft Visual Studio 11.0" (
  call "%ProgramFiles(x86)%\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" x64
) ELSE IF EXIST "%ProgramFiles%\Microsoft Visual Studio 10.0" (
  call "%ProgramFiles(x86)%\Microsoft Visual Studio 10.0\VC\vcvarsall.bat" x64
)

chdir /d %~dp0

tf checkout "..\Deploy\" /lock:none /recursive

chdir /d %~dp0
del "..\Deploy\SDK\Elysium SDK (x86).zip"
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\SDK\Elysium SDK (x86).zip" "..\Binary\Debug\AnyCPU\Elysium.dll" "..\Binary\Debug\AnyCPU\Elysium.pdb" "..\Binary\Debug\AnyCPU\Elysium.Notifications.dll" "..\Binary\Debug\AnyCPU\Elysium.Notifications.pdb" "..\Binary\Release\x86\Elysium.Notifications.Server.exe" "..\Binary\Release\x86\Elysium.Notifications.Server.exe.config" "..\Binary\Release\x86\Elysium.Test.exe" ".\SDK\ZIP\Tools\Run Elysium Notifications service.bat" ".\SDK\ZIP\Tools\Stop Elysium Notifications service.bat" ".\SDK\ZIP\Tools\x86\Install Elysium Notifications service.bat" ".\SDK\ZIP\Tools\x86\Uninstall Elysium Notifications service.bat"

cd "..\Binary\Documentation"
"..\..\Tools and Resources\Utilities\7za\7za.exe" u "..\..\Deploy\SDK\Elysium SDK (x86).zip" "en\*" "ru\*"

chdir /d %~dp0
cd "..\Tools and Resources\Assembly dependencies\"
"..\Utilities\7za\7za.exe" u "..\..\Deploy\SDK\Elysium SDK (x86).zip" "Microsoft.Expression.Drawing.dll" "Microsoft.Expression.Drawing.xml" "Microsoft.Windows.Shell.dll" "Microsoft.Windows.Shell.pdb" "Microsoft.Windows.Shell.xml" "Design\*" "en\*"

chdir /d %~dp0
del "..\Deploy\SDK\Elysium SDK (x64).zip"
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\SDK\Elysium SDK (x64).zip" "..\Binary\Debug\AnyCPU\Elysium.dll" "..\Binary\Debug\AnyCPU\Elysium.pdb" "..\Binary\Debug\AnyCPU\Elysium.Notifications.dll" "..\Binary\Debug\AnyCPU\Elysium.Notifications.pdb" "..\Binary\Release\x64\Elysium.Notifications.Server.exe" "..\Binary\Release\x64\Elysium.Notifications.Server.exe.config" "..\Binary\Release\x64\Elysium.Test.exe" ".\SDK\ZIP\Tools\Run Elysium Notifications service.bat" ".\SDK\ZIP\Tools\Stop Elysium Notifications service.bat" ".\SDK\ZIP\Tools\x64\Install Elysium Notifications service.bat" ".\SDK\ZIP\Tools\x64\Uninstall Elysium Notifications service.bat"

cd "..\Binary\Documentation"
"..\..\Tools and Resources\Utilities\7za\7za.exe" u "..\..\Deploy\SDK\Elysium SDK (x64).zip" "en\*" "ru\*"

chdir /d %~dp0
cd "..\Tools and Resources\Assembly dependencies\"
"..\Utilities\7za\7za.exe" u "..\..\Deploy\SDK\Elysium SDK (x64).zip" "Microsoft.Expression.Drawing.dll" "Microsoft.Expression.Drawing.xml" "Microsoft.Windows.Shell.dll" "Microsoft.Windows.Shell.pdb" "Microsoft.Windows.Shell.xml" "Design\*" "en\*"

chdir /d %~dp0
del "..\Deploy\Runtime\Elysium Runtime (x86).zip"
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\Runtime\Elysium Runtime (x86).zip" "..\Binary\Release\AnyCPU\Elysium.dll" "..\Binary\Release\AnyCPU\Elysium.Notifications.dll" "..\Binary\Release\x86\Elysium.Notifications.Server.exe" "..\Binary\Release\x86\Elysium.Notifications.Server.exe.config"

cd "..\Tools and Resources\Assembly dependencies\"
"..\Utilities\7za\7za.exe" u "..\..\Deploy\Runtime\Elysium Runtime (x86).zip" "Microsoft.Expression.Drawing.dll" "Microsoft.Windows.Shell.dll"

chdir /d %~dp0
del "..\Deploy\Runtime\Elysium Runtime (x64).zip"
"..\Tools and Resources\Utilities\7za\7za.exe" a "..\Deploy\Runtime\Elysium Runtime (x64).zip" "..\Binary\Release\AnyCPU\Elysium.dll" "..\Binary\Release\AnyCPU\Elysium.Notifications.dll" "..\Binary\Release\x64\Elysium.Notifications.Server.exe" "..\Binary\Release\x64\Elysium.Notifications.Server.exe.config"

cd "..\Tools and Resources\Assembly dependencies\"
"..\Utilities\7za\7za.exe" u "..\..\Deploy\Runtime\Elysium Runtime (x64).zip" "Microsoft.Expression.Drawing.dll" "Microsoft.Windows.Shell.dll"

chdir /d %~dp0
del "..\Deploy\SDK\Setup (x86).exe"
copy "..\Binary\Debug\x86\SDK\MSI\Setup.exe" "..\Deploy\SDK\Setup (x86).exe"
del "..\Deploy\SDK\Installer (en-us, x86).msi"
copy "..\Binary\Debug\x86\SDK\MSI\en-us\Installer.msi" "..\Deploy\SDK\Installer (en-us, x86).msi"
del "..\Deploy\SDK\Installer (ru-ru, x86).msi"
copy "..\Binary\Debug\x86\SDK\MSI\ru-ru\Installer.msi" "..\Deploy\SDK\Installer (ru-ru, x86).msi"
del "..\Deploy\SDK\Setup (x64).exe"
copy "..\Binary\Debug\x64\SDK\MSI\Setup.exe" "..\Deploy\SDK\Setup (x64).exe"
del "..\Deploy\SDK\Installer (en-us, x64).msi"
copy "..\Binary\Debug\x64\SDK\MSI\en-us\Installer.msi" "..\Deploy\SDK\Installer (en-us, x64).msi"
del "..\Deploy\SDK\Installer (ru-ru, x64).msi"
copy "..\Binary\Debug\x64\SDK\MSI\ru-ru\Installer.msi" "..\Deploy\SDK\Installer (ru-ru, x64).msi"
del "..\Deploy\Runtime\Installer (x86).msi"
copy "..\Binary\Debug\x86\Runtime\MSI\Installer.msi" "..\Deploy\Runtime\Installer (x86).msi"
del "..\Deploy\Runtime\Installer (x64).msi"
copy "..\Binary\Debug\x64\Runtime\MSI\Installer.msi" "..\Deploy\Runtime\Installer (x64).msi"

pause