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