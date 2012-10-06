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

chdir /d %~dp0

certmgr.exe -add -c "RootCertificate.cer" -s -r localMachine root 
sn -i "SigningKey.pfx" VS_KEY_495CE44A959FD928

pause