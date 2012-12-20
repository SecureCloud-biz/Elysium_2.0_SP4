@echo off

IF /I "%1" == "NETFX4" (
  set Framework=NETFX4
) ELSE (
  IF /I "%1" == "NETFX45" (
    set Framework=NETFX45
) ELSE goto showerror
)
IF NOT "%2" == "" goto usage

IF %PROCESSOR_ARCHITECTURE% == AMD64 goto x64
ELSE goto x86

:x86
IF EXIST "%ProgramFiles%\Microsoft Visual Studio 11.0" (
  call "%ProgramFiles%\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" x86
) ELSE IF EXIST "%ProgramFiles%\Microsoft Visual Studio 10.0" (
         IF /I "%Framework%" == "NETFX45" goto showerror
         call "%ProgramFiles%\Microsoft Visual Studio 10.0\VC\vcvarsall.bat" x86
     ) ELSE goto showerror
goto body

:x64
IF EXIST "%ProgramFiles(x86)%\Microsoft Visual Studio 11.0" (
  call "%ProgramFiles(x86)%\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" x64
) ELSE IF EXIST "%ProgramFiles(x86)%\Microsoft Visual Studio 10.0" (
         IF /I "%Framework%" == "NETFX45" goto showerror
         call "%ProgramFiles(x86)%\Microsoft Visual Studio 10.0\VC\vcvarsall.bat" x64
     ) ELSE goto showerror
goto body

:body
goto :eof

:usage
echo Error in script usage. The correct usage is:
echo     %0 [framework]
echo where [framework] is: NETFX4 ^| NETFX45
echo:
echo For example:
echo     %0 NETFX45
goto :eof

:showerror
echo Build error occurred
exit /b 1