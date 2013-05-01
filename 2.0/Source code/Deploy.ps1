param (
    [Parameter(Position = 0, Mandatory = $False, HelpMessage = "Specifies target framework (NETFX4 or NETFX45). Default is NETFX45.")]
    [ValidateSet("NETFX4", "NETFX45")]
    [string] $Framework       = "NETFX45"
)

$Location = Split-Path -Parent $MyInvocation.MyCommand.Path
$Path = Join-Path -Path $Location -ChildPath Elysium.psm1

Import-Module $Path

Deploy -Root $Location -Framework $Framework