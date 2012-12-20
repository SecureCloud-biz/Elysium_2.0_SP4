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

tf checkout "Elysium\Properties\AssemblyInfo.cs" /lock:none
tf checkout "Elysium\Documentation\ru\Elysium.xml" "Elysium\Documentation\en\Elysium.xml" /lock:none
tf checkout "%FrameworkName%\Elysium\Themes\Generic.xaml" /lock:none
msbuild "Elysium\Elysium.%Framework%.csproj" /target:Build /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium\Elysium.%Framework%.csproj" /target:Build /property:Configuration=Release;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

tf checkout "Elysium.Design\Properties\AssemblyInfo.cs" /lock:none
IF /I "%Framework%" == "NETFX4" (
msbuild "Elysium.Design\Elysium.Design.10.0.csproj" /target:Build /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Design\Elysium.Design.10.0.csproj" /target:Build /property:Configuration=Release;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
)
msbuild "Elysium.Design\Elysium.Design.11.0.%Framework%.csproj" /target:Build /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Design\Elysium.Design.11.0.%Framework%.csproj" /target:Build /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

tf checkout "Elysium.Notifications\Properties\AssemblyInfo.cs" /lock:none
tf checkout "Elysium.Notifications\Documentation\ru\Elysium.Notifications.xml" "Elysium.Notifications\Documentation\en\Elysium.Notifications.xml" /lock:none
msbuild "Elysium.Notifications\Elysium.Notifications.%Framework%.csproj" /target:Build /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications\Elysium.Notifications.%Framework%.csproj" /target:Build /property:Configuration=Release;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

tf checkout "Elysium.Notifications.Server\Properties\AssemblyInfo.cs" /lock:none
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.%Framework%.csproj" /target:Build /property:Configuration=Debug;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.%Framework%.csproj" /target:Build /property:Configuration=Release;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.%Framework%.csproj" /target:Build /property:Configuration=Debug;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.%Framework%.csproj" /target:Build /property:Configuration=Release;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

tf checkout "Elysium.Test\Properties\AssemblyInfo.cs" /lock:none
msbuild "Elysium.Test\Elysium.Test.%Framework%.csproj" /target:Build /property:Configuration=Debug;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.%Framework%.csproj" /target:Build /property:Configuration=Release;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.%Framework%.csproj" /target:Build /property:Configuration=Debug;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.%Framework%.csproj" /target:Build /property:Configuration=Release;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

IF /I "%Framework%" == "NETFX4" (
tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2010\CSharp\1033\%FrameworkName%\" /lock:none /recursive
tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2010\CSharp\1033\%FrameworkName%.zip" /lock:none
msbuild "SDK\MSI\ItemTemplates\Visual Studio 2010\CSharp\1033\%FrameworkName%\VS2010_CSharp_1033_ItemTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2010\CSharp\1049\%FrameworkName%\" /lock:none /recursive
tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2010\CSharp\1049\%FrameworkName%.zip" /lock:none
msbuild "SDK\MSI\ItemTemplates\Visual Studio 2010\CSharp\1049\%FrameworkName%\VS2010_CSharp_1049_ItemTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
)
tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1033\%FrameworkName%\" /lock:none /recursive
tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1033\%FrameworkName%.zip" /lock:none
msbuild "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1033\%FrameworkName%\VS2012_CSharp_1033_ItemTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1049\%FrameworkName%\" /lock:none /recursive
tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1049\%FrameworkName%.zip" /lock:none
msbuild "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1049\%FrameworkName%\VS2012_CSharp_1049_ItemTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

IF /I "%Framework%" == "NETFX4" (
tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2010\CSharp\1033\%FrameworkName%\" /lock:none /recursive
tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2010\CSharp\1033\%FrameworkName%.zip" /lock:none
msbuild "SDK\MSI\ProjectTemplates\Visual Studio 2010\CSharp\1033\%FrameworkName%\VS2010_CSharp_1033_ProjectTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2010\CSharp\1049\%FrameworkName%\" /lock:none /recursive
tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2010\CSharp\1049\%FrameworkName%.zip" /lock:none
msbuild "SDK\MSI\ProjectTemplates\Visual Studio 2010\CSharp\1049\%FrameworkName%\VS2010_CSharp_1049_ProjectTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
)
tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1033\%FrameworkName%\" /lock:none /recursive
tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1033\%FrameworkName%.zip" /lock:none
msbuild "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1033\%FrameworkName%\VS2012_CSharp_1033_ProjectTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1049\%FrameworkName%\" /lock:none /recursive
tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1049\%FrameworkName%.zip" /lock:none
msbuild "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1049\%FrameworkName%\VS2012_CSharp_1049_ProjectTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

call "SDK\MSI\Zip %FrameworkName% templates.bat"
IF ERRORLEVEL 1 goto showerror

chdir /d %~dp0

msbuild "Documentation\ru\Documentation.%Framework%.shfbproj" /target:Build /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Documentation\en\Documentation.%Framework%.shfbproj" /target:Build /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

tf checkout "SDK\MSI\UI\Properties\AssemblyInfo.cs" /lock:none
msbuild "SDK\MSI\UI\Elysium.SDK.MSI.UI.%Framework%.csproj" /target:Build /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\UI\Elysium.SDK.MSI.UI.%Framework%.csproj" /target:Build /property:Configuration=Release;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "SDK\MSI\Elysium.SDK.MSI.%Framework%.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.%Framework%.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.%Framework%.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.%Framework%.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.%Framework%.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.%Framework%.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.%Framework%.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.%Framework%.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Runtime\MSI\Elysium.Runtime.MSI.%Framework%.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.%Framework%.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.%Framework%.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.%Framework%.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x64 /verbosity:minimal
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