param (
    [Parameter(Position = 0, Mandatory = $False, HelpMessage = "Specifies your Visual Studio version (2010 or 2012). Default is 2012.")]
    [ValidateSet("2010", "2012")]
    [string] $Version = "2012",

    [Parameter(Position = 1, Mandatory = $False, HelpMessage = "Specifies target framework (NETFX4 or NETFX45). Default is NETFX45.")]
    [ValidateSet("NETFX4", "NETFX45")]
    [string] $Framework     = "NETFX45",

    [Parameter(Position = 2, Mandatory = $False, HelpMessage = "Number of working threads (recommended one thread per logical core). Default is 2.")]
    [byte]   $Threads       = 2
)

$Location = Split-Path -Parent $MyInvocation.MyCommand.Path
$Path = Join-Path -Path $Location -ChildPath Elysium.psm1

Import-Module $Path

# Build Elysium projects
Build-Projects -Version $Version -Framework $Framework -Configuration Debug -Transform $True -Parallel $False -Threads $Threads
Build-Projects -Version $Version -Framework $Framework -Configuration Release -Transform $True -Parallel $False -Threads $Threads

# Build project and item templates for Visual Studio
Build-Templates -Version $Version -Framework $Framework -LCID 1033 -Threads $Threads
Build-Templates -Version $Version -Framework $Framework -LCID 1049 -Threads $Threads

# Zip project and item templates for Visual Studio
Zip-Templates -Version $Version -Framework $Framework -LCID 1033 -Threads $Threads
Zip-Templates -Version $Version -Framework $Framework -LCID 1049 -Threads $Threads

# Build Elysium documentation
Build-Documentation -Version $Version -Framework $Framework -Language en -Parallel $False -Threads $Threads
Build-Documentation -Version $Version -Framework $Framework -Language ru -Parallel $False -Threads $Threads

# Build installation projects
Build-Installation -Version $Version -Framework $Framework -Configuration Debug -Transform $True -Parallel $False -Threads $Threads
Build-Installation -Version $Version -Framework $Framework -Configuration Release -Transform $True -Parallel $False -Threads $Threads

Read-Host -Prompt "Press any key to continue..."