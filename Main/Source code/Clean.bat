@echo off

IF /I "%1" == "NETFX4" (
  set Framework=NETFX4
) ELSE (
  IF /I "%1" == "NETFX45" (
    set Framework=NETFX45
) ELSE goto showerror
)
IF NOT "%2" == "" goto usage

call "Load tools.bat" %Framework%
IF ERRORLEVEL 1 goto showerror

msbuild "Elysium\Elysium.%Framework%.csproj" /target:Clean /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium\Elysium.%Framework%.csproj" /target:Clean /property:Configuration=Release;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

IF /I "%Framework%" == "NETFX4" (
msbuild "Elysium.Design\Elysium.Design.10.0.csproj" /target:Clean /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Design\Elysium.Design.10.0.csproj" /target:Clean /property:Configuration=Release;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
)
msbuild "Elysium.Design\Elysium.Design.11.0.%Framework%.csproj" /target:Clean /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Design\Elysium.Design.11.0.%Framework%.csproj" /target:Clean /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Elysium.Notifications\Elysium.Notifications.%Framework%.csproj" /target:Clean /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications\Elysium.Notifications.%Framework%.csproj" /target:Clean /property:Configuration=Release;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.%Framework%.csproj" /target:Clean /property:Configuration=Debug;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.%Framework%.csproj" /target:Clean /property:Configuration=Release;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.%Framework%.csproj" /target:Clean /property:Configuration=Debug;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.%Framework%.csproj" /target:Clean /property:Configuration=Release;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Elysium.Test\Elysium.Test.%Framework%.csproj" /target:Clean /property:Configuration=Debug;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.%Framework%.csproj" /target:Clean /property:Configuration=Release;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.%Framework%.csproj" /target:Clean /property:Configuration=Debug;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.%Framework%.csproj" /target:Clean /property:Configuration=Release;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Documentation\ru\Documentation.%Framework%.shfbproj" /target:Clean /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Documentation\en\Documentation.%Framework%.shfbproj" /target:Clean /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "SDK\MSI\UI\Elysium.SDK.MSI.UI.%Framework%.csproj" /target:Clean /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\UI\Elysium.SDK.MSI.UI.%Framework%.csproj" /target:Clean /property:Configuration=Release;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "SDK\MSI\Elysium.SDK.MSI.%Framework%.wixproj" /target:Clean /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.%Framework%.wixproj" /target:Clean /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.%Framework%.wixproj" /target:Clean /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.%Framework%.wixproj" /target:Clean /property:Configuration=Release;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.%Framework%.wixproj" /target:Clean /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.%Framework%.wixproj" /target:Clean /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.%Framework%.wixproj" /target:Clean /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.%Framework%.wixproj" /target:Clean /property:Configuration=Release;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Runtime\MSI\Elysium.Runtime.MSI.%Framework%.wixproj" /target:Clean /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.%Framework%.wixproj" /target:Clean /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.%Framework%.wixproj" /target:Clean /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.%Framework%.wixproj" /target:Clean /property:Configuration=Release;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

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