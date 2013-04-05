param (
    [Parameter(Position = 2, Mandatory = $False,  HelpMessage = "Key file used to strong name sign assembly.")]
    [string] $AssemblyKey     = (Resolve-Path "SigningKey.pfx"),

    [Parameter(Position = 3, Mandatory = $False,  HelpMessage = "Key file used to digital signature sign assembly.")]
    [string] $SignatureKey    = (Resolve-Path "SigningKey.pfx"),

    [Parameter(Position = 4, Mandatory = $False,  HelpMessage = "Assembly digital signature parameters.")]
    [string] $SignatureParams = "/t http://timestamp.comodoca.com/authenticode",

    [Parameter(Position = 5, Mandatory = $False, HelpMessage = "Number of working threads (recommended one thread per logical core). Default is 2.")]
    [byte]   $Threads         = 2,

    [Parameter(Position = 6, Mandatory = $False, HelpMessage = "Switch, if you try build mapped code.")]
    [bool]   $Checkout        = $True
)

$Location = Split-Path -Parent $MyInvocation.MyCommand.Path
$Path = Join-Path -Path $Location -ChildPath Build.ps1

& $Path -Version 2012 -Framework NETFX4 -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Threads $Threads -Checkout $Checkout
Try-Exit
& $Path -Version 2012 -Framework NETFX45 -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Threads $Threads -Checkout $Checkout
Try-Exit