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

tf checkout "Elysium\Properties\AssemblyInfo.cs" /lock:none
tf checkout "Elysium\Documentation\ru\Elysium.xml" "Elysium\Documentation\en\Elysium.xml" /lock:none
tf checkout "Elysium\Themes\Generic.xaml" /lock:none
msbuild "Elysium\Elysium.csproj" /target:Build /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium\Elysium.csproj" /target:Build /property:Configuration=Release;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

tf checkout "Elysium.Notifications\Properties\AssemblyInfo.cs" /lock:none
tf checkout "Elysium.Notifications\Documentation\ru\Elysium.Notifications.xml" "Elysium.Notifications\Documentation\en\Elysium.Notifications.xml" /lock:none
msbuild "Elysium.Notifications\Elysium.Notifications.csproj" /target:Build /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications\Elysium.Notifications.csproj" /target:Build /property:Configuration=Release;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

tf checkout "Elysium.Notifications.Server\Properties\AssemblyInfo.cs" /lock:none
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.csproj" /target:Build /property:Configuration=Debug;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.csproj" /target:Build /property:Configuration=Release;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.csproj" /target:Build /property:Configuration=Debug;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Notifications.Server\Elysium.Notifications.Server.csproj" /target:Build /property:Configuration=Release;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

tf checkout "Elysium.Test\Properties\AssemblyInfo.cs" /lock:none
msbuild "Elysium.Test\Elysium.Test.csproj" /target:Build /property:Configuration=Debug;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.csproj" /target:Build /property:Configuration=Release;Platform=x86 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.csproj" /target:Build /property:Configuration=Debug;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Elysium.Test\Elysium.Test.csproj" /target:Build /property:Configuration=Release;Platform=x64 /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1033\" /lock:none /recursive
tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1033.zip" /lock:none
msbuild "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1033\VS2012_CSharp_1033_ItemTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1049\" /lock:none /recursive
tf checkout "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1049.zip" /lock:none
msbuild "SDK\MSI\ItemTemplates\Visual Studio 2012\CSharp\1049\VS2012_CSharp_1049_ItemTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1033\" /lock:none /recursive
tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1033.zip" /lock:none
msbuild "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1033\VS2012_CSharp_1033_ProjectTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1049\" /lock:none /recursive
tf checkout "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1049.zip" /lock:none
msbuild "SDK\MSI\ProjectTemplates\Visual Studio 2012\CSharp\1049\VS2012_CSharp_1049_ProjectTemplate.csproj" /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

call "SDK\MSI\Zip templates.bat"

chdir /d %~dp0

msbuild "Documentation\ru\Elysium.shfbproj" /target:Build /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Documentation\en\Elysium.shfbproj" /target:Build /property:Configuration=Debug;Platform=AnyCPU /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

tf checkout "SDK\MSI\UI\Properties\AssemblyInfo.cs" /lock:none
msbuild "SDK\MSI\UI\Elysium.SDK.MSI.UI.csproj" /target:Build /property:Configuration=Debug;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\UI\Elysium.SDK.MSI.UI.csproj" /target:Build /property:Configuration=Release;Platform=AnyCPU /property:BuildProjectReferences=false /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "SDK\MSI\Elysium.SDK.MSI.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Elysium.SDK.MSI.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

msbuild "Runtime\MSI\Elysium.Runtime.MSI.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.wixproj" /target:Rebuild /property:Configuration=Debug;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x86 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror
msbuild "Runtime\MSI\Elysium.Runtime.MSI.wixproj" /target:Rebuild /property:Configuration=Release;Platform=x64 /verbosity:minimal
IF ERRORLEVEL 1 goto showerror

pause

goto: EOF

:showerror
echo Build error occurred
pause
exit %ERRORLEVEL%