@echo off
set PATH=%ProgramFiles%\Microsoft SDKs\Windows\v8.0A\Bin\NETFX 4.0 Tools
@echo on
gacutil /u "Microsoft.Expression.Drawing"
gacutil /u "Microsoft.Expression.Interactions"
gacutil /u "System.Windows.Interactivity"
gacutil /u "Microsoft.Windows.Shell"
gacutil /u "JetBrains.Annotations"
pause