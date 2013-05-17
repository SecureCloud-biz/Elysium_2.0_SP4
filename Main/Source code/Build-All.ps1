param (
    [Parameter(Position = 2, Mandatory = $False,  HelpMessage = "Key file used to strong name sign assembly.")]
    [string] $AssemblyKey     = (Resolve-Path "..\..\SigningKey.pfx"),

    [Parameter(Position = 3, Mandatory = $False,  HelpMessage = "Key file used to digital signature sign assembly.")]
    [string] $SignatureKey    = (Resolve-Path "..\..\SigningKey.pfx"),

    [Parameter(Position = 4, Mandatory = $False,  HelpMessage = "Assembly digital signature parameters.")]
    [string] $SignatureParams = "/t http://timestamp.comodoca.com/authenticode",

    [Parameter(Position = 5, Mandatory = $False, HelpMessage = "Number of working threads (recommended one thread per logical core). Default is 2.")]
    [byte]   $Threads         = 8,

    [Parameter(Position = 6, Mandatory = $False, HelpMessage = "Switch, if you try build mapped code.")]
    [bool]   $Checkout        = $True
)

$Location = Split-Path -Parent $MyInvocation.MyCommand.Path
$Path = Join-Path -Path $Location -ChildPath Build.ps1

"Started building in background..."
$BuildNETFX4 = Start-Job -ScriptBlock {
    & $args[0] -Version 2012 -Framework NETFX4 -AssemblyKey $args[1] -SignatureKey $args[2] -SignatureParams $args[3] -Threads $args[4] -Checkout $args[5]
    Try-Exit
} -ArgumentList @($Path, $AssemblyKey, $SignatureKey, $SignatureParams, $Threads, $Checkout)
Wait-Job $BuildNETFX4
Receive-Job $BuildNETFX4
"Completed building in background..."

"Started building in background..."
$BuildNETFX45 = Start-Job -ScriptBlock {
    & $args[0] -Version 2012 -Framework NETFX45 -AssemblyKey $args[1] -SignatureKey $args[2] -SignatureParams $args[3] -Threads $args[4] -Checkout $args[5]
    Try-Exit
} -ArgumentList @($Path, $AssemblyKey, $SignatureKey, $SignatureParams, $Threads, $Checkout)
Wait-Job $BuildNETFX45
Receive-Job $BuildNETFX45
"Completed building in background..."