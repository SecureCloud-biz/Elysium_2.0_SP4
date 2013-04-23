Add-PSSnapin Microsoft.TeamFoundation.PowerShell

# Try exit if error occurred
function Try-Exit
{
    param (
        [Parameter(Position = 0, Mandatory = $False)]
        [int]$ExitCode = $LASTEXITCODE
    )
    
    if ($ExitCode -ne 0)
    {
        Write-Host "Error occurred"
        Read-Host -Prompt "Press any key to continue..."
        exit $ExitCode
    }
}

# Run program and capture stdout and stderr
function Invoke-Program
{
    param (
        [Parameter(Position = 0, Mandatory = $True)]
        [string]$Path,

        [Parameter(Position = 1, Mandatory = $False)]
        [string]$Arguments
    )
    Write-Host "$Path $Arguments"
    $processInfo = New-Object System.Diagnostics.ProcessStartInfo
    $processInfo.FileName = $Path
    $processInfo.Arguments = $Arguments
    $processInfo.WorkingDirectory = Get-Location
    Write-Host $processInfo.WorkingDirectory
    $processInfo.RedirectStandardError = $True
    $processInfo.RedirectStandardOutput = $True
    $processInfo.WindowStyle = "Hidden"
    $processInfo.UseShellExecute = $False
    $process = New-Object System.Diagnostics.Process
    $process.StartInfo = $processInfo
    $process.Start() | Out-Null
    $process.WaitForExit()
    $stdout = $process.StandardOutput.ReadToEnd()
    $stderr = $process.StandardError.ReadToEnd()
    Write-Host $stdout
    Write-Host $stderr
    return $process.ExitCode
}

# Import environment variables from specified environment
function Invoke-Environment
{
    param (
        [Parameter(Position = 0, Mandatory = $True)]
        [string]$Command,

        [Parameter(Position = 1, Mandatory = $False)]
        [switch]$Force
    )
    
    $stream = ($temp = [IO.Path]::GetTempFileName())
    $operator = if ($Force) {"'&'"} else {"'&&'"}
    
    
    Invoke-Expression -Command "$env:ComSpec /c $Command > `"$stream`" 2>&1 $operator set"
    foreach($_ in Get-Content -LiteralPath $temp) {
        if ($_ -match '^([^=]+)=(.*)') {
            [System.Environment]::SetEnvironmentVariable($matches[1], $matches[2])
        }
    }
    
    Remove-Item -LiteralPath $temp
}

# Import Visual Studio environment variables
function Import-Variables
{
    param (
        [Parameter(Mandatory = $True, HelpMessage = "Specifies your Visual Studio version (2010 or 2012). Default is 2012.")]
        [ValidateSet("2010", "2012")]
        [string] $Version
    )

    switch ($Version)
    {
        "2010"  { $Variables = Join-Path $env:VS100COMNTOOLS -ChildPath "..\..\VC\vcvarsall.bat" -Resolve }
        "2012" { $Variables = Join-Path $env:VS110COMNTOOLS -ChildPath "..\..\VC\vcvarsall.bat" -Resolve }
    }

    Invoke-Environment -Command "`"$Variables`" $env:PROCESSOR_ARCHITECTURE"
}

# Run MSBuild.exe with specified arguments
function Build
{
    param (
        [Parameter(Position = 0, Mandatory = $True,  HelpMessage = "Specifies the project to build.")]
        [string] $Project,

        [Parameter(Position = 1, Mandatory = $False, HelpMessage = "Specifies build action (build or rebuild).")]
        [ValidateSet("Build", "Rebuild", "Transform")]
        [string] $Action = "Build",
    
        [Parameter(Position = 2, Mandatory = $True,  HelpMessage = "Specifies build configuration (Debug or Release).")]
        [ValidateSet("Debug", "Release")]
        [string] $Configuration,
        
        [Parameter(Position = 3, Mandatory = $True,  HelpMessage = "Specifies target platform.")]
        [ValidateSet("AnyCPU", "x86", "x64")]
        [string] $Platform,

        [Parameter(Position = 4, Mandatory = $True,  HelpMessage = "True to transform T4 templates, otherwise False.")]
        [bool]   $Transform,

        [Parameter(Position = 5, Mandatory = $False,  HelpMessage = "Key file used to strong name sign assembly.")]
        [string] $AssemblyKey,

        [Parameter(Position = 6, Mandatory = $False,  HelpMessage = "Key file used to digital signature sign assembly.")]
        [string] $SignatureKey,

        [Parameter(Position = 7, Mandatory = $False,  HelpMessage = "Assembly digital signature parameters.")]
        [string] $SignatureParams,

        [Parameter(Position = 8, Mandatory = $True,  HelpMessage = "Number of working threads (recommended one thread per logical core).")]
        [byte]   $Threads
    )

    $TransformArgs = "/property:TransformOnBuild=True;TransformFile=*.tt"
    $SignArgs = "/property:AssemblyOriginatorKeyFile=`"$AssemblyKey`" /property:DigitalSignatureKeyFile=`"$SignatureKey`" /property:DigitalSignatureParams=`"$SignatureParams`""

    & msbuild.exe "`"$Project`" /target:$Action /property:Configuration=$Configuration;Platform=$Platform $TransformArgs /property:BuildProjectReferences=False $SignArgs /maxcpucount:$Threads /verbosity:minimal"
    
    Try-Exit
}

# Make relative path absolute
function Resolve-Path
{
    param (
        [Parameter(Mandatory = $True, HelpMessage = "Specifies the relative path.")]
        [string] $RelativePath
    )

    $Location = Split-Path -Parent $script:MyInvocation.MyCommand.Path
    return [System.IO.Path]::GetFullPath((Join-Path -Path $Location -ChildPath $RelativePath))
}

# Install keys
function Install-Keys
{
    param (
        [Parameter(Mandatory = $True, HelpMessage = "Specifies your Visual Studio version (2010 or 2012). Default is 2012.")]
        [ValidateSet("2010", "2012")]
        [string] $Version = "2012"
    )

    Import-Variables -Version $Version

    & certmgr.exe -add -c (Resolve-Path "RootCertificate.cer") -s -r localMachine root
    Try-Exit

    & sn -i (Resolve-Path "SigningKey.pfx") VS_KEY_495CE44A959FD928
    Try-Exit
}

# Reinstall keys
function Remove-Keys
{
    param (
        [Parameter(Mandatory = $True, HelpMessage = "Specifies your Visual Studio version (2010 or 2012). Default is 2012.")]
        [ValidateSet("2010", "2012")]
        [string] $Version = "2012"
    )

    Import-Variables -Version $Version

    & sn -d VS_KEY_495CE44A959FD928
    Try-Exit
}

# Build Elysium projects
function Build-Projects
{
    param (
        [Parameter(Position = 0, Mandatory = $False, HelpMessage = "Specifies your Visual Studio version (2010 or 2012). Default is 2012.")]
        [ValidateSet("2010", "2012")]
        [string] $Version       = "2012",

        [Parameter(Position = 1, Mandatory = $False, HelpMessage = "Specifies target framework (NETFX4 or NETFX45). Default is NETFX45.")]
        [ValidateSet("NETFX4", "NETFX45")]
        [string] $Framework     = "NETFX45",

        [Parameter(Position = 2, Mandatory = $False, HelpMessage = "Specifies build configuration (Debug or Release). Default is Release.")]
        [ValidateSet("Debug", "Release")]
        [string] $Configuration = "Release",

        [Parameter(Position = 3, Mandatory = $False, HelpMessage = "True to transform T4 templates, otherwise False. Default is True.")]
        [bool]   $Transform     = $True,

        [Parameter(Position = 4, Mandatory = $False,  HelpMessage = "Key file used to strong name sign assembly.")]
        [string] $AssemblyKey = (Resolve-Path "SigningKey.pfx"),

        [Parameter(Position = 5, Mandatory = $False,  HelpMessage = "Key file used to digital signature sign assembly.")]
        [string] $SignatureKey = (Resolve-Path "SigningKey.pfx"),

        [Parameter(Position = 6, Mandatory = $False,  HelpMessage = "Assembly digital signature parameters.")]
        [string] $SignatureParams = "/t http://timestamp.comodoca.com/authenticode",

        [Parameter(Position = 7, Mandatory = $False, HelpMessage = "Number of working threads (recommended one thread per logical core). Default is 2.")]
        [byte]   $Threads       = 2,

        [Parameter(Position = 8, Mandatory = $False, HelpMessage = "Switch, if you try build mapped code.")]
        [bool]   $Checkout      = $True
    )

    switch ($Framework)
    {
        "NETFX4"  { $FrameworkName = ".NET Framework 4"   }
        "NETFX45" { $FrameworkName = ".NET Framework 4.5" }
    }

    Import-Variables $Version

    function Build-Project
    {
        param (
            [Parameter(Position = 0, Mandatory = $True, HelpMessage = "Specifies the project to build.")]
            [string] $Project,
            
            [Parameter(Position = 1, Mandatory = $True, HelpMessage = "Specifies target platform.")]
            [ValidateSet("AnyCPU", "x86", "x64")]
            [string] $Platform
        )

        Build -Project $Project -Configuration $Configuration -Platform $Platform -Transform $Transform -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Threads $Threads
    }

    # Build Elysium assembly
    if ($Checkout)
    {
        Add-TfsPendingChange -Edit (Resolve-Path "Elysium\Properties\AssemblyInfo.cs") -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path "Elysium\Documentation\ru\Elysium.xml") -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path "Elysium\Documentation\en\Elysium.xml") -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path "$FrameworkName\Elysium\Themes\Generic.xaml") -Lock none
    }
    Build-Project -Project (Resolve-Path "Elysium\Elysium.$Framework.csproj") -Platform AnyCPU

    # Build design-time support assemblies
    if ($Checkout)
    {
        Add-TfsPendingChange -Edit (Resolve-Path "Elysium.Design\Properties\AssemblyInfo.cs") -Lock none
    }
    if ($Framework -eq "NETFX4")
    {
        Build-Project -Project (Resolve-Path "Elysium.Design\Elysium.Design.10.0.csproj") -Platform AnyCPU
    }
    Build-Project -Project (Resolve-Path "Elysium.Design\Elysium.Design.11.0.$Framework.csproj") -Platform x86

    # Build Notifications core assembly
    if ($Checkout)
    {
        Add-TfsPendingChange -Edit (Resolve-Path "Elysium.Notifications\Properties\AssemblyInfo.cs") -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path "Elysium.Notifications\Documentation\ru\Elysium.Notifications.xml") -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path "Elysium.Notifications\Documentation\en\Elysium.Notifications.xml") -Lock none
    }
    Build-Project -Project (Resolve-Path "Elysium.Notifications\Elysium.Notifications.$Framework.csproj") -Platform AnyCPU

    # Build Notifications support service assembly
    if ($Checkout)
    {
        Add-TfsPendingChange -Edit (Resolve-Path "Elysium.Notifications.Server\Properties\AssemblyInfo.cs") -Lock none
        $InstallAndRunScript = "$FrameworkName\Elysium.Notifications.Server\Tools\{0}\Install and run Elysium Notifications service ({1}).bat"
        $StopAndUninstallScript = "$FrameworkName\Elysium.Notifications.Server\Tools\{0}\Stop and uninstall Elysium Notifications service ({1}).bat"
        Add-TfsPendingChange -Edit (Resolve-Path ([string]::Format($InstallAndRunScript, "x86", "debug"))) -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path ([string]::Format($InstallAndRunScript, "x86", "release"))) -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path ([string]::Format($StopAndUninstallScript, "x86", "debug"))) -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path ([string]::Format($StopAndUninstallScript, "x86", "release"))) -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path ([string]::Format($InstallAndRunScript, "x64", "debug"))) -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path ([string]::Format($InstallAndRunScript, "x64", "release"))) -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path ([string]::Format($StopAndUninstallScript, "x64", "debug"))) -Lock none
        Add-TfsPendingChange -Edit (Resolve-Path ([string]::Format($StopAndUninstallScript, "x64", "release"))) -Lock none
    }
    Build-Project -Project (Resolve-Path "Elysium.Notifications.Server\Elysium.Notifications.Server.$Framework.csproj") -Platform x86
    Build-Project -Project (Resolve-Path "Elysium.Notifications.Server\Elysium.Notifications.Server.$Framework.csproj") -Platform x64

    # Build sample project
    if ($Checkout)
    {
        Add-TfsPendingChange -Edit (Resolve-Path "Elysium.Test\Properties\AssemblyInfo.cs") -Lock none
    }
    Build-Project -Project (Resolve-Path "Elysium.Test\Elysium.Test.$Framework.csproj") -Platform x86
    Build-Project -Project (Resolve-Path "Elysium.Test\Elysium.Test.$Framework.csproj") -Platform x64
}

# Build project and item templates for Visual Studio
function Build-Templates
{
    param (
        [Parameter(Position = 0, Mandatory = $False, HelpMessage = "Specifies your Visual Studio version (2010 or 2012). Default is 2012.")]
        [ValidateSet("2010", "2012")]
        [string] $Version   = "2012",

        [Parameter(Position = 1, Mandatory = $False, HelpMessage = "Specifies target framework (NETFX4 or NETFX45). Default is NETFX45.")]
        [ValidateSet("NETFX4", "NETFX45")]
        [string] $Framework = "NETFX45",

        [Parameter(Position = 2, Mandatory = $True, HelpMessage = "Specifies target language by LCID: English (1033) or Russian (1049). Default is English (1033).")]
        [ValidateSet("1033", "1049")]
        [string] $LCID,

        [Parameter(Position = 4, Mandatory = $False, HelpMessage = "Number of working threads (recommended one thread per logical core). Default is 2.")]
        [byte]   $Threads   = 2,

        [Parameter(Position = 5, Mandatory = $False, HelpMessage = "Switch, if you try build mapped code.")]
        [bool]   $Checkout  = $True
    )

    switch ($Framework)
    {
        "NETFX4"  { $FrameworkName = ".NET Framework 4"   }
        "NETFX45" { $FrameworkName = ".NET Framework 4.5" }
    }

    Import-Variables $Version

    function Build-Template
    {
        param (
            [Parameter(Position = 0, Mandatory = $True, HelpMessage = "Specifies type of template (Item or Project).")]
            [ValidateSet("Item", "Project")]
            [string] $Type,

            [Parameter(Position = 1, Mandatory = $True, HelpMessage = "Specifies version of Visual Studio (2010 or 2012).")]
            [ValidateSet("2010", "2012")]
            [string] $Version
        )
        
        if ($Checkout)
        {
            Add-TfsPendingChange -Edit (Resolve-Path ("SDK\MSI\" + $Type + "Templates\Visual Studio $Version\CSharp\$LCID\$FrameworkName\")) -Recurse -Lock none
        }
        $Project = Resolve-Path ("SDK\MSI\" + $Type + "Templates\Visual Studio $Version\CSharp\$LCID\$FrameworkName\VS" + $Version + "_CSharp_" + $LCID + "_" + $Type + "Template.csproj")
        Build -Project $Project -Action Transform -Configuration Release -Platform AnyCPU -Transform $True -Threads $Threads
    }

    # Build item templates
    if ($Framework -eq "NETFX4")
    {
        Build-Template -Type Item -Version 2010
    }    
    Build-Template -Type Item -Version 2012

    # Build project templates
    if ($Framework -eq "NETFX4")
    {
        Build-Template -Type Project -Version 2010
    }    
    Build-Template -Type Project -Version 2012
}

# Zip project and item templates for Visual Studio
function Zip-Templates
{
    param (
        [Parameter(Position = 0, Mandatory = $False, HelpMessage = "Specifies your Visual Studio version (2010 or 2012). Default is 2012.")]
        [ValidateSet("2010", "2012")]
        [string] $Version   = "2012",

        [Parameter(Position = 1, Mandatory = $False, HelpMessage = "Specifies target framework (NETFX4 or NETFX45). Default is NETFX45.")]
        [ValidateSet("NETFX4", "NETFX45")]
        [string] $Framework = "NETFX45",

        [Parameter(Position = 2, Mandatory = $True, HelpMessage = "Specifies target language by LCID: English (1033) or Russian (1049). Default is English (1033).")]
        [ValidateSet("1033", "1049")]
        [string] $LCID,

        [Parameter(Position = 3, Mandatory = $False, HelpMessage = "Number of working threads (recommended one thread per logical core). Default is 2.")]
        [byte]   $Threads   = 2,

        [Parameter(Position = 4, Mandatory = $False, HelpMessage = "Switch, if you try build mapped code.")]
        [bool]   $Checkout  = $True
    )

    switch ($Framework)
    {
        "NETFX4"  { $FrameworkName = ".NET Framework 4"   }
        "NETFX45" { $FrameworkName = ".NET Framework 4.5" }
    }

    Import-Variables $Version

    function Zip-Template
    {
        param (
            [Parameter(Position = 0, Mandatory = $True, HelpMessage = "Specifies type of template (Item or Project).")]
            [ValidateSet("Item", "Project")]
            [string] $Type,

            [Parameter(Position = 1, Mandatory = $True, HelpMessage = "Specifies version of Visual Studio (2010 or 2012).")]
            [ValidateSet("2010", "2012")]
            [string] $Version

        )

        # Resolve paths
        $Folder  = Resolve-Path ("SDK\MSI\" + $Type + "Templates\Visual Studio $Version\CSharp\$LCID\$FrameworkName\")
        $Archive = Resolve-Path ("SDK\MSI\" + $Type + "Templates\Visual Studio $Version\CSharp\$LCID\$FrameworkName.zip")
        
        # Check-out and zip archive
        if ($Checkout)
        {
            Add-TfsPendingChange -Edit $Folder -Recurse -Lock none
            Add-TfsPendingChange -Edit $Archive         -Lock none
        }
        Remove-Item $Archive -Force
        $7za = Resolve-Path "..\Tools and Resources\Utilities\7za\7za.exe"
        $7zaArgs = "a `"$Archive`" `"$Folder*`" -x!*" + $Type + "Template.csproj -x!*.vspscc -x!*\ -x!*.tt"
        Try-Exit (Invoke-Program -Path $7za -Arguments $7zaArgs)
    }

    # Zip item templates
    if ($Framework -eq "NETFX4")
    {
        ZIP-Template -Type Item -Version 2010
    }
    ZIP-Template -Type Item -Version 2012

    # Zip project templates
    if ($Framework -eq "NETFX4")
    {
        ZIP-Template -Type Project -Version 2010
    }    
    ZIP-Template -Type Project -Version 2012

    # Wait background tasks and display results
}

# Build Elysium documentation
function Build-Documentation
{
    param (
        [Parameter(Position = 0, Mandatory = $False, HelpMessage = "Specifies your Visual Studio version (2010 or 2012). Default is 2012.")]
        [ValidateSet("2010", "2012")]
        [string] $Version = "2012",

        [Parameter(Position = 1, Mandatory = $False, HelpMessage = "Specifies target framework (NETFX4 or NETFX45). Default is NETFX45.")]
        [ValidateSet("NETFX4", "NETFX45")]
        [string] $Framework     = "NETFX45",

        [Parameter(Position = 3, Mandatory = $True, HelpMessage = "Specifies target language by short name: English (en) or Russian (ru). Default is English (en).")]
        [ValidateSet("en", "ru")]
        [string] $Language,

        [Parameter(Position = 4, Mandatory = $False, HelpMessage = "Number of working threads (recommended one thread per logical core). Default is 2.")]
        [byte]   $Threads       = 2
    )

    switch ($Framework)
    {
        "NETFX4"  { $FrameworkName = ".NET Framework 4"   }
        "NETFX45" { $FrameworkName = ".NET Framework 4.5" }
    }

    Import-Variables $Version

    $LanguagesCount = 2
    $Threads = $Threads / $LanguagesCount

    function Build-Documentation-Project
    {
        param (
            [Parameter(Position = 0, Mandatory = $True, HelpMessage = "Specifies the project to build.")]
            [string] $Project
        )

        Build -Project $Project -Configuration Release -Platform AnyCPU -Transform $False -Threads $Threads
    }

    Build-Documentation-Project -Project (Resolve-Path "Documentation\$Language\Documentation.$Framework.shfbproj")
}

# Build installation projects
function Build-Installation
{
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

        [Parameter(Position = 3, Mandatory = $False, HelpMessage = "True to transform T4 templates, otherwise False. Default is True.")]
        [bool]   $Transform     = $True,

        [Parameter(Position = 4, Mandatory = $False,  HelpMessage = "Key file used to strong name sign assembly.")]
        [string] $AssemblyKey = (Resolve-Path "SigningKey.pfx"),

        [Parameter(Position = 5, Mandatory = $False,  HelpMessage = "Key file used to digital signature sign assembly.")]
        [string] $SignatureKey = (Resolve-Path "SigningKey.pfx"),

        [Parameter(Position = 6, Mandatory = $False,  HelpMessage = "Assembly digital signature parameters.")]
        [string] $SignatureParams = "/t http://timestamp.comodoca.com/authenticode",

        [Parameter(Position = 7, Mandatory = $False, HelpMessage = "Number of working threads (recommended one thread per logical core). Default is 2.")]
        [byte]   $Threads       = 2,

        [Parameter(Position = 8, Mandatory = $False, HelpMessage = "Switch, if you try build mapped code.")]
        [bool]   $Checkout      = $True
    )

    switch ($Framework)
    {
        "NETFX4"  { $FrameworkName = ".NET Framework 4"   }
        "NETFX45" { $FrameworkName = ".NET Framework 4.5" }
    }

    Import-Variables $Version

    function Build-Project
    {
        param (
            [Parameter(Position = 0, Mandatory = $True, HelpMessage = "Specifies the project to build.")]
            [string] $Project,
            
            [Parameter(Position = 1, Mandatory = $True, HelpMessage = "Specifies target platform.")]
            [ValidateSet("AnyCPU", "x86", "x64")]
            [string] $Platform
        )

        Build -Project $Project -Action Build   -Configuration $Configuration -Platform $Platform -Transform $Transform -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Threads $Threads
    }

    function Build-WiX-Project
    {
        param (
            [Parameter(Position = 0, Mandatory = $True, HelpMessage = "Specifies the project to build.")]
            [string] $Project,
            
            [Parameter(Position = 1, Mandatory = $True, HelpMessage = "Specifies target platform.")]
            [ValidateSet("x86", "x64")]
            [string] $Platform
        )

        Build -Project $Project -Action Rebuild -Configuration $Configuration -Platform $Platform -Transform $False -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Threads $Threads
    }

    # Build SDK bootstrapper UI assembly
    if ($Checkout)
    {
        Add-TfsPendingChange -Edit (Resolve-Path "SDK\MSI\UI\Properties\AssemblyInfo.cs") -Lock none
    }
    Build-Project -Project (Resolve-Path "SDK\MSI\UI\Elysium.SDK.MSI.UI.$Framework.csproj") -Platform AnyCPU

    # Build SDK MSI installer
    Build-WiX-Project -Project (Resolve-Path "SDK\MSI\Elysium.SDK.MSI.$Framework.wixproj") -Platform x86
    Build-WiX-Project -Project (Resolve-Path "SDK\MSI\Elysium.SDK.MSI.$Framework.wixproj") -Platform x64

    # Build SDK bootstrapper
    Build-WiX-Project -Project (Resolve-Path "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.$Framework.wixproj") -Platform x86
    Build-WiX-Project -Project (Resolve-Path "SDK\MSI\Bootstrapper\Elysium.SDK.MSI.Bootstrapper.$Framework.wixproj") -Platform x64

    # Build Runtime
    Build-WiX-Project -Project (Resolve-Path "Runtime\MSI\Elysium.Runtime.MSI.$Framework.wixproj") -Platform x86
    Build-WiX-Project -Project (Resolve-Path "Runtime\MSI\Elysium.Runtime.MSI.$Framework.wixproj") -Platform x64
}

# Deploy
function Deploy
{

    param (
        [Parameter(Position = 0, Mandatory = $True, HelpMessage = "Specifies the root path.")]
        [string] $Root,

        [Parameter(Position = 1, Mandatory = $False, HelpMessage = "Specifies target framework (NETFX4 or NETFX45). Default is NETFX45.")]
        [ValidateSet("NETFX4", "NETFX45")]
        [string] $Framework     = "NETFX45"
    )

    switch ($Framework)
    {
        "NETFX4"  { $FrameworkName = ".NET Framework 4"   }
        "NETFX45" { $FrameworkName = ".NET Framework 4.5" }
    }

    function Zip-Files
    {
        param (
            [Parameter(Position = 0, Mandatory = $True, HelpMessage = "7-zip arguments.")]
            [string] $Args
        )

        $7za = Resolve-Path "..\Tools and Resources\Utilities\7za\7za.exe";
        Try-Exit (Invoke-Program -Path $7za -Arguments $Args)
    }
    
    $DebugAnyCPU = "..\Binary\$FrameworkName\Debug\AnyCPU"
    $ReleaseAnyCPU = "..\Binary\$FrameworkName\Release\AnyCPU"

    $ElysiumDebug = "$DebugAnyCPU\Elysium.dll";
    $ElysiumDebugPDB = "$DebugAnyCPU\Elysium.pdb";
    $ElysiumRelease = "$ReleaseAnyCPU\Elysium.dll";

    $Design10 = "$ReleaseAnyCPU\Elysium.Design.10.0.dll"
    $Design11 = "$ReleaseAnyCPU\Elysium.Design.11.0.dll";

    $NotificationsDebug = "$DebugAnyCPU\Elysium.Notifications.dll";
    $NotificationsDebugPDB = "$DebugAnyCPU\Elysium.Notifications.pdb";
    $NotificationsRelease = "$ReleaseAnyCPU\Elysium.Notifications.dll";

    $NotificationsServer32 = "..\Binary\$FrameworkName\Release\x86\Elysium.Notifications.Server.exe";
    $NotificationsServer64 = "..\Binary\$FrameworkName\Release\x64\Elysium.Notifications.Server.exe";
    $NotificationsServer32Config = "..\Binary\$FrameworkName\Release\x86\Elysium.Notifications.Server.exe.config";
    $NotificationsServer64Config = "..\Binary\$FrameworkName\Release\x64\Elysium.Notifications.Server.exe.config";

    $RunNotificationsService = ".\SDK\ZIP\Tools\$FrameworkName\Run Elysium Notifications service.bat";
    $StopNotificationsService = ".\SDK\ZIP\Tools\$FrameworkName\Stop Elysium Notifications service.bat";
    $InstallNotificationsService32 = ".\SDK\ZIP\Tools\$FrameworkName\x86\Install Elysium Notifications service.bat";
    $InstallNotificationsService64 = ".\SDK\ZIP\Tools\$FrameworkName\x64\Install Elysium Notifications service.bat";
    $UninstallNotificationsService32 = ".\SDK\ZIP\Tools\$FrameworkName\x86\Uninstall Elysium Notifications service.bat"
    $UninstallNotificationsService64 = ".\SDK\ZIP\Tools\$FrameworkName\x64\Uninstall Elysium Notifications service.bat"

    $Test32 = "..\Binary\$FrameworkName\Release\x86\Elysium.Test.exe";
    $Test64 = "..\Binary\$FrameworkName\Release\x64\Elysium.Test.exe";
    $Test32Config = "..\Binary\$FrameworkName\Release\x86\Elysium.Test.exe.config"
    $Test64Config = "..\Binary\$FrameworkName\Release\x64\Elysium.Test.exe.config"

    $Documentation = "..\Binary\$FrameworkName\Documentation\";

    $Dependencies = "..\Tools and Resources\Assembly dependencies\$FrameworkName";
    
    ############################################################################
    #                          SDK - ZIP - 32-bit                              #
    ############################################################################

    cd $Root

    $ArchivePath = Resolve-Path "..\Deploy\$FrameworkName\SDK\Elysium SDK (x86).zip";
    Remove-Item $ArchivePath -ErrorAction Ignore
    $Assemblies = "`"$ElysiumDebug`" `"$ElysiumDebugPDB`" `"$Design11`" `"$NotificationsDebug`" `"$NotificationsDebugPDB`" `"$NotificationsServer32`" `"$NotificationsServer32Config`" `"$Test32`""
    $Scripts = "`"$RunNotificationsService`" `"$StopNotificationsService`" `"$InstallNotificationsService32`" `"$UninstallNotificationsService32`""
    Zip-Files -Args "a `"$ArchivePath`" $Assemblies $Scripts"
    if ($Framework -eq "NETFX4")
    {
        Zip-Files -Args "u `"$ArchivePath`" `"$Design10`""
    }
    if ($Framework -eq "NETFX45")
    {
        Zip-Files -Args "u `"$ArchivePath`" `"$Test32Config`""
    }
    
    ############################################################################
    
    cd $Root
    cd $Documentation

    Zip-Files -Args "u `"$ArchivePath`" `"en\*.*`" `"ru\*.*`""

    ############################################################################
    
    cd $Root
    cd $Dependencies

    Zip-Files -Args "u `"$ArchivePath`" `"Microsoft.Expression.Drawing.dll`" `"Microsoft.Expression.Drawing.xml`" `"Design\*`" `"en\*`" -r"
    if ($Framework -eq "NETFX4")
    {
        Zip-Files -Args "u `"$ArchivePath`" `"Microsoft.Windows.Shell.dll`" `"Microsoft.Windows.Shell.pdb`" `"Microsoft.Windows.Shell.xml`""
    }
    if ($Framework -eq "NETFX45")
    {
        Zip-Files -Args "u `"$ArchivePath`" `"ru\*`""
    }
    
    ############################################################################
    #                          SDK - ZIP - 64-bit                              #
    ############################################################################

    cd $Root

    $ArchivePath = Resolve-Path "..\Deploy\$FrameworkName\SDK\Elysium SDK (x64).zip";
    Remove-Item $ArchivePath -ErrorAction Ignore
    $Assemblies = "`"$ElysiumDebug`" `"$ElysiumDebugPDB`" `"$Design11`" `"$NotificationsDebug`" `"$NotificationsDebugPDB`" `"$NotificationsServer64`" `"$NotificationsServer64Config`" `"$Test64`""
    $Scripts = "`"$RunNotificationsService`" `"$StopNotificationsService`" `"$InstallNotificationsService64`" `"$UninstallNotificationsService64`""
    Zip-Files -Args "a `"$ArchivePath`" $Assemblies $Scripts"
    if ($Framework -eq "NETFX4")
    {
        Zip-Files -Args "u `"$ArchivePath`" `"$Design10`""
    }
    if ($Framework -eq "NETFX45")
    {
        Zip-Files -Args "u `"$ArchivePath`" `"$Test64Config`""
    }

    ############################################################################
    
    cd $Root
    cd $Documentation

    Zip-Files -Args "u `"$ArchivePath`" `"en\*.*`" `"ru\*.*`""

    ############################################################################
    
    cd $Root
    cd $Dependencies

    Zip-Files -Args "u `"$ArchivePath`" `"Microsoft.Expression.Drawing.dll`" `"Microsoft.Expression.Drawing.xml`" `"Design\*`" `"en\*`" -r"
    if ($Framework -eq "NETFX4")
    {
        Zip-Files -Args "u `"$ArchivePath`" `"Microsoft.Windows.Shell.dll`" `"Microsoft.Windows.Shell.pdb`" `"Microsoft.Windows.Shell.xml`""
    }
    if ($Framework -eq "NETFX45")
    {
        Zip-Files -Args "u `"$ArchivePath`" `"ru\*`""
    }

    ############################################################################
    #                        Runtime - ZIP - 32-bit                            #
    ############################################################################

    cd $Root

    $ArchivePath = Resolve-Path "..\Deploy\$FrameworkName\Runtime\Elysium Runtime (x86).zip";
    Remove-Item $ArchivePath -ErrorAction Ignore
    Zip-Files -Args "a `"$ArchivePath`" `"$ElysiumRelease`" `"$NotificationsRelease`" `"$NotificationsServer32`" `"$NotificationsServer32Config`""

    ############################################################################
    
    cd $Root
    cd $Dependencies

    Zip-Files -Args "u `"$ArchivePath`" `"Microsoft.Expression.Drawing.dll`""
    if ($Framework -eq "NETFX4")
    {
        Zip-Files -Args "u `"$ArchivePath`" `"Microsoft.Windows.Shell.dll`""
    }


    ############################################################################
    #                        Runtime - ZIP - 64-bit                            #
    ############################################################################

    cd $Root

    $ArchivePath = Resolve-Path "..\Deploy\$FrameworkName\Runtime\Elysium Runtime (x64).zip";
    Remove-Item $ArchivePath -ErrorAction Ignore
    Zip-Files -Args "a `"$ArchivePath`" `"$ElysiumRelease`" `"$NotificationsRelease`" `"$NotificationsServer64`" `"$NotificationsServer64Config`""

    ############################################################################
    
    cd $Root
    cd $Dependencies

    Zip-Files -Args "u `"$ArchivePath`" `"Microsoft.Expression.Drawing.dll`""
    if ($Framework -eq "NETFX4")
    {
        Zip-Files -Args "u `"$ArchivePath`" `"Microsoft.Windows.Shell.dll`""
    }

    ############################################################################
    #                             Installers                                   #
    ############################################################################

    cd $Root

    Remove-Item "..\Deploy\$FrameworkName\SDK\Setup (x86).exe" -ErrorAction Ignore
    Copy-Item "..\Binary\$FrameworkName\Release\x86\SDK\MSI\Setup.exe" "..\Deploy\$FrameworkName\SDK\Setup (x86).exe"
    Remove-Item "..\Deploy\$FrameworkName\SDK\Installer (en-us, x86).msi" -ErrorAction Ignore
    Copy-Item "..\Binary\$FrameworkName\Release\x86\SDK\MSI\en-us\Installer.msi" "..\Deploy\$FrameworkName\SDK\Installer (en-us, x86).msi"
    Remove-Item "..\Deploy\$FrameworkName\SDK\Installer (ru-ru, x86).msi" -ErrorAction Ignore
    Copy-Item "..\Binary\$FrameworkName\Release\x86\SDK\MSI\ru-ru\Installer.msi" "..\Deploy\$FrameworkName\SDK\Installer (ru-ru, x86).msi"
    Remove-Item "..\Deploy\$FrameworkName\SDK\Setup (x64).exe" -ErrorAction Ignore
    Copy-Item "..\Binary\$FrameworkName\Release\x64\SDK\MSI\Setup.exe" "..\Deploy\$FrameworkName\SDK\Setup (x64).exe"
    Remove-Item "..\Deploy\$FrameworkName\SDK\Installer (en-us, x64).msi" -ErrorAction Ignore
    Copy-Item "..\Binary\$FrameworkName\Release\x64\SDK\MSI\en-us\Installer.msi" "..\Deploy\$FrameworkName\SDK\Installer (en-us, x64).msi"
    Remove-Item "..\Deploy\$FrameworkName\SDK\Installer (ru-ru, x64).msi" -ErrorAction Ignore
    Copy-Item "..\Binary\$FrameworkName\Release\x64\SDK\MSI\ru-ru\Installer.msi" "..\Deploy\$FrameworkName\SDK\Installer (ru-ru, x64).msi"
    Remove-Item "..\Deploy\$FrameworkName\Runtime\Installer (x86).msi" -ErrorAction Ignore
    Copy-Item "..\Binary\$FrameworkName\Release\x86\Runtime\MSI\Installer.msi" "..\Deploy\$FrameworkName\Runtime\Installer (x86).msi"
    Remove-Item "..\Deploy\$FrameworkName\Runtime\Installer (x64).msi" -ErrorAction Ignore
    Copy-Item "..\Binary\$FrameworkName\Release\x64\Runtime\MSI\Installer.msi" "..\Deploy\$FrameworkName\Runtime\Installer (x64).msi"
}