param (
    [Parameter(Position = 0, Mandatory = $False, HelpMessage = "Specifies your Visual Studio version (2010 or 2012). Default is 2012.")]
    [ValidateSet("2010", "2012")]
    [string] $Version         = "2012",

    [Parameter(Position = 1, Mandatory = $False, HelpMessage = "Specifies target framework (NETFX4 or NETFX45). Default is NETFX45.")]
    [ValidateSet("NETFX4", "NETFX45")]
    [string] $Framework       = "NETFX45",

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
$Path = Join-Path -Path $Location -ChildPath Elysium.psm1

Import-Module $Path

# Build Elysium projects
Build-Projects -Version $Version -Framework $Framework -Configuration Debug -Transform $True -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Parallel $False -Threads $Threads -Checkout $Checkout
Build-Projects -Version $Version -Framework $Framework -Configuration Release -Transform $True -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Parallel $False -Threads $Threads -Checkout $Checkout

# Build project and item templates for Visual Studio
Build-Templates -Version $Version -Framework $Framework -LCID 1033 -Threads $Threads -Checkout $Checkout
Build-Templates -Version $Version -Framework $Framework -LCID 1049 -Threads $Threads -Checkout $Checkout

# Zip project and item templates for Visual Studio
Zip-Templates -Version $Version -Framework $Framework -LCID 1033 -Threads $Threads -Checkout $Checkout
Zip-Templates -Version $Version -Framework $Framework -LCID 1049 -Threads $Threads -Checkout $Checkout

# Build Elysium documentation
#Build-Documentation -Version $Version -Framework $Framework -Language en -Parallel $False -Threads $Threads
#Build-Documentation -Version $Version -Framework $Framework -Language ru -Parallel $False -Threads $Threads

# Build installation projects
Build-Installation -Version $Version -Framework $Framework -Configuration Debug -Transform $True -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Parallel $False -Threads $Threads -Checkout $Checkout
Build-Installation -Version $Version -Framework $Framework -Configuration Release -Transform $True -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Parallel $False -Threads $Threads -Checkout $Checkout

Read-Host -Prompt "Press any key to continue..."