@echo off

chdir /d %~dp0

call "..\..\Load tools.bat" NETFX45
IF ERRORLEVEL 1 goto showerror

tf checkout "ProjectTemplates\Visual Studio 2012\CSharp\1033\.NET Framework 4.5.zip" /lock:none
del "ProjectTemplates\Visual Studio 2012\CSharp\1033\.NET Framework 4.5.zip"
IF ERRORLEVEL 1 goto showerror
tf checkout "ProjectTemplates\Visual Studio 2012\CSharp\1049\.NET Framework 4.5.zip" /lock:none
del "ProjectTemplates\Visual Studio 2012\CSharp\1049\.NET Framework 4.5.zip"
IF ERRORLEVEL 1 goto showerror


tf checkout "ItemTemplates\Visual Studio 2012\CSharp\1033\.NET Framework 4.5.zip" /lock:none
del "ItemTemplates\Visual Studio 2012\CSharp\1033\.NET Framework 4.5.zip"
IF ERRORLEVEL 1 goto showerror
tf checkout "ItemTemplates\Visual Studio 2012\CSharp\1049\.NET Framework 4.5.zip" /lock:none
del "ItemTemplates\Visual Studio 2012\CSharp\1049\.NET Framework 4.5.zip"
IF ERRORLEVEL 1 goto showerror


"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ProjectTemplates\Visual Studio 2012\CSharp\1033\.NET Framework 4.5.zip" ".\ProjectTemplates\Visual Studio 2012\CSharp\1033\.NET Framework 4.5\*" -x!*ProjectTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt
IF ERRORLEVEL 1 goto showerror
"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ProjectTemplates\Visual Studio 2012\CSharp\1049\.NET Framework 4.5.zip" ".\ProjectTemplates\Visual Studio 2012\CSharp\1049\.NET Framework 4.5\*" -x!*ProjectTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt
IF ERRORLEVEL 1 goto showerror


"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ItemTemplates\Visual Studio 2012\CSharp\1033\.NET Framework 4.5.zip" ".\ItemTemplates\Visual Studio 2012\CSharp\1033\.NET Framework 4.5\*" -x!*ItemTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt
IF ERRORLEVEL 1 goto showerror
"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ItemTemplates\Visual Studio 2012\CSharp\1049\.NET Framework 4.5.zip" ".\ItemTemplates\Visual Studio 2012\CSharp\1049\.NET Framework 4.5\*" -x!*ItemTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt
IF ERRORLEVEL 1 goto showerror

goto :eof

:showerror
echo Build error occurred
exit /b %ERRORLEVEL%