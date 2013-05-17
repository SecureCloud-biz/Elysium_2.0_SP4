param (
    [Parameter(Mandatory = $True, HelpMessage = "Specifies your Visual Studio version (2010 or 2012).")]
    [ValidateSet("2010", "2012")]
    [string] $Version
)

$Location = Split-Path -Parent $MyInvocation.MyCommand.Path
$Path = Join-Path -Path $Location -ChildPath Helpers.psm1

Import-Module $Path

Import-Variables -Version $Version

& sn -d VS_KEY_495CE44A959FD928
Try-Exit

Read-Host -Prompt "Press any key to continue..."