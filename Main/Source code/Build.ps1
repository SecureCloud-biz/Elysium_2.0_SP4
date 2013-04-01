param (
    [Parameter(Position = 0, Mandatory = $False, HelpMessage = "Specifies your Visual Studio version (2010 or 2012). Default is 2012.")]
    [ValidateSet("2010", "2012")]
    [string] $Version = "2012",

    [Parameter(Position = 1, Mandatory = $False, HelpMessage = "Specifies target framework (NETFX4 or NETFX45). Default is NETFX45.")]
    [ValidateSet("NETFX4", "NETFX45")]
    [string] $Framework     = "NETFX45",

    [Parameter(Position = 2, Mandatory = $False, HelpMessage = "Specifies build configuration (Debug or Release). Default is Release.")]
    [ValidateSet("Debug", "Release")]
    [string] $Configuration = "Release",

    [Parameter(Position = 3, Mandatory = $False, HelpMessage = "Number of working threads (recommended one thread per logical core). Default is 2.")]
    [byte]   $Threads       = 2
)

$Location = Split-Path -Parent $MyInvocation.MyCommand.Path
$Path = Join-Path -Path $Location -ChildPath Elysium.psm1

Import-Module $Path

# Build Elysium projects
Build-Projects -Version $Version -Framework $Framework -Configuration $Configuration -Transform $True -Parallel $False -Threads $Threads

# Build project and item templates for Visual Studio
Build-Templates -Version $Version -Framework $Framework -LCID 1033 -Parallel $False -Threads $Threads
Build-Templates -Version $Version -Framework $Framework -LCID 1049 -Parallel $False -Threads $Threads

# Zip project and item templates for Visual Studio
$ZipEnglishTemplates = Start-Job -ScriptBlock { Zip-Templates -Version $Version -Framework $Framework -LCID 1033 -Threads $Threads }
$ZipRussianTemplates = Start-Job -ScriptBlock { Zip-Templates -Version $Version -Framework $Framework -LCID 1049 -Threads $Threads }
Wait-Job $ZipEnglishTemplates
Wait-Job $ZipRussianTemplates
Receive-Job $ZipEnglishTemplates
Receive-Job $ZipRussianTemplates

# Build Elysium documentation
$BuildEnglishDocumentation = Start-Job -ScriptBlock { Build-Documentation -Version $Version -Framework $Framework -Language en -Parallel $False -Threads $Threads }
$BuildRussianDocumentation = Start-Job -ScriptBlock { Build-Documentation -Version $Version -Framework $Framework -Language ru -Parallel $False -Threads $Threads }
Wait-Job $BuildEnglishDocumentation
Wait-Job $BuildRussianDocumentation
Receive-Job $BuildEnglishDocumentation
Receive-Job $BuildRussianDocumentation

# Build installation projects
Build-Installation -Version $Version -Framework $Framework -Configuration $Configuration -Transform $True -Parallel $False -Threads $Threads