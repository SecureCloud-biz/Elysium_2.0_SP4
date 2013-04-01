param (
    [Parameter(Mandatory = $True, HelpMessage = "Specifies your Visual Studio version (2010 or 2012).")]
    [ValidateSet("2010", "2012")]
    [string] $Version
)

$Location = Split-Path -Parent $MyInvocation.MyCommand.Path
$Path = Join-Path -Path $Location -ChildPath Elysium.psm1

Import-Module $Path

Install-Keys -Version $Version