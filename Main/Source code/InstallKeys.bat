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

certmgr.exe -add -c "RootCertificate.cer" -s -r localMachine root 
IF ERRORLEVEL 1 goto showerror
sn -i "SigningKey.pfx" VS_KEY_495CE44A959FD928
IF ERRORLEVEL 1 goto showerror

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
pause