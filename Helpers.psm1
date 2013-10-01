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
# SIG # Begin signature block
# MIIGAwYJKoZIhvcNAQcCoIIF9DCCBfACAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUW2nyInTCgtixTnQnZd1fdeX+
# fp+gggN8MIIDeDCCAmCgAwIBAgIQOmW6/axc969KEPAZJomtbzANBgkqhkiG9w0B
# AQ0FADA8MTowOAYDVQQDEzFBbGVrc2FuZHIgVmlzaG55YWtvdiAtIFRlbXBvcmFy
# eSByb290IGNlcnRpZmljYXRlMB4XDTEyMDkxMDA3MjgzMVoXDTM5MTIzMTIzNTk1
# OVowKjEoMCYGA1UEAxMfRWx5c2l1bSAtIFRlbXBvcmFyeSBjZXJ0aWZpY2F0ZTCC
# ASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBANKG/OMKgZrEcVvIgRSMjSij
# sfG9wTCwi2RFlphVJtuWfJ09caUcQw8xIr95yyMdyyOeFNLcovmqLLX9GFt7/qKc
# Jh2m+EVvU4RvPs+YjfQio/T8u+ZI8dndgwegKcs3B0VLIiX35JHe5SSyWUwnAqjH
# NdW0fBO18k0xjRGN30TCdqSRUjdNu6zOvOF6GEsBvnW677vY2mIic3wFq1Q5//WQ
# GmCjOkv75spzsI95MNOaj0CTj5PE0bfm4OIz55U6jT+2SOxFxDVp0y+lAAn7L1i2
# ymeyfBzM3w53gIQrOKl2CT/fVldeNQy1/cYWWBgHo+3EZKk7TnfkE8++YPrBUb0C
# AwEAAaOBhzCBhDATBgNVHSUEDDAKBggrBgEFBQcDAzBtBgNVHQEEZjBkgBBnVlWU
# Fuyg6Mb0/essbehVoT4wPDE6MDgGA1UEAxMxQWxla3NhbmRyIFZpc2hueWFrb3Yg
# LSBUZW1wb3Jhcnkgcm9vdCBjZXJ0aWZpY2F0ZYIQPtMhNrOL6YVORyl7mbhbxDAN
# BgkqhkiG9w0BAQ0FAAOCAQEAKYr6EQqKHNMP8kYTTdEjG8S0ixiTEcVE01lzA0Cu
# XS1I5GkpAU66qlu/u+Vc72hwxV8XkTICM2bYdqhz/7PmH8hJaYJfLww2rOoqsATi
# 9UVznlmT0b6BLojUBLQnTD2TKPHg9xIsfbFVyKYMuXmOSA+IXaaExWQOMcVAw7pc
# paL3ZjrjbwLHQlo8bhS01KgL78Mu4ShDhCf2Y/U80kooUWYE5SLmx5XS38ZE+Bgw
# HCp04tQrvW9uYGJ+viDVpq1JPXJ/xTtsSR5xz9kPVC5iI6Hlo62NG3MND/P4yXFe
# MkQGW1uxIX+i7kegMemoGksP+otBcwhFteyFbAn0QawuvjGCAfEwggHtAgEBMFAw
# PDE6MDgGA1UEAxMxQWxla3NhbmRyIFZpc2hueWFrb3YgLSBUZW1wb3Jhcnkgcm9v
# dCBjZXJ0aWZpY2F0ZQIQOmW6/axc969KEPAZJomtbzAJBgUrDgMCGgUAoHgwGAYK
# KwYBBAGCNwIBDDEKMAigAoAAoQKAADAZBgkqhkiG9w0BCQMxDAYKKwYBBAGCNwIB
# BDAcBgorBgEEAYI3AgELMQ4wDAYKKwYBBAGCNwIBFTAjBgkqhkiG9w0BCQQxFgQU
# L39xALpTHyTqbe6DRKR03SZJhEgwDQYJKoZIhvcNAQEBBQAEggEAI6sebGnTpwmZ
# gY10L3UXX/LSq2mXDmsMf1LuebJeNSAQZsneXVLueRw8cHhrWNJMvK3yTS9zg4f8
# CdTxHuWP1Onjc3yGg0kHgNQ5MVcJs1fLY/AmmuaXZl54s0stpH7XBMgxpBOdM+1d
# 1SHV5Km2VbQqoNrVaf3RiffFIaTKNPcHnOs4jw3X1oKo19RLCeOBhY9HMPF8YtTB
# hp4RqNZXw79tXg6+D1xQotBEtJOLaaYg6OGB7/Py+eRFq2ktXOtrRpWh8/CsoSzw
# 0UcItY6rW8usC9xpdy13Wu5Qod9J4gTI1LZCiBNHor7PGeetJpJCbHb4WlVnWR/z
# fGLMCDtdAQ==
# SIG # End signature block
