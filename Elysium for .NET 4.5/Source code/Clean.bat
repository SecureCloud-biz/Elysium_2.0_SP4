@echo off

IF %PROCESSOR_ARCHITECTURE% == AMD64 goto x64
ELSE goto x86

:x86
IF EXIST "%ProgramFiles%\Microsoft Visual Studio 11.0" (
  call "%ProgramFiles%\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" x86
)

:x64
IF EXIST "%ProgramFiles(x86)%\Microsoft Visual Studio 11.0" (
  call "%ProgramFiles(x86)%\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" x64
)

call %vcvarsall%

chdir /d %~dp0

msbuild "Elysium\Elysium.csproj" /target:Clean /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium\Elysium.csproj" /target:Clean /property:Configuration=Release;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Elysium.Notifications\Elysium.Notifications.csproj" /target:Clean /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications\Elysium.Notifications.csproj" /target:Clean /property:Configuration=Release;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.csproj" /target:Clean /property:Configuration=Debug;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.csproj" /target:Clean /property:Configuration=Release;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.csproj" /target:Clean /property:Configuration=Debug;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.csproj" /target:Clean /property:Configuration=Release;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Elysium.Test\Elysium.Test.csproj" /target:Clean /property:Configuration=Debug;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.csproj" /target:Clean /property:Configuration=Release;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.csproj" /target:Clean /property:Configuration=Debug;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.csproj" /target:Clean /property:Configuration=Release;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Documentation\ru\Elysium.shfbproj" /target:Clean /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Documentation\en\Elysium.shfbproj" /target:Clean /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "SDK\MSI\UI\Elysium.SDK.MSI.UI.csproj" /target:Clean /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\UI\Elysium.SDK.MSI.UI.csproj" /target:Clean /property:Configuration=Release;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "SDK\MSI\Elysium.SDK.MSI.wixproj" /target:Clean /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.wixproj" /target:Clean /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.wixproj" /target:Clean /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.wixproj" /target:Clean /property:Configuration=Release;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.wixproj" /target:Clean /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.wixproj" /target:Clean /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.wixproj" /target:Clean /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.wixproj" /target:Clean /property:Configuration=Release;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Runtime\MSI\Elysium.Runtime.MSI.wixproj" /target:Clean /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.wixproj" /target:Clean /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.wixproj" /target:Clean /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.wixproj" /target:Clean /property:Configuration=Release;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

pause

goto: EOF

:showerror
echo Build error occurred
pause
exit %ERRORLEVEL%