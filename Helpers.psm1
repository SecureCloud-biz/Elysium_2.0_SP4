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
# MIIM6AYJKoZIhvcNAQcCoIIM2TCCDNUCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUW2nyInTCgtixTnQnZd1fdeX+
# fp+gggokMIIENjCCAx6gAwIBAgIDBHpTMA0GCSqGSIb3DQEBBQUAMD4xCzAJBgNV
# BAYTAlBMMRswGQYDVQQKExJVbml6ZXRvIFNwLiB6IG8uby4xEjAQBgNVBAMTCUNl
# cnR1bSBDQTAeFw0wOTAzMDMxMjUzNTZaFw0yNDAzMDMxMjUzNTZaMHgxCzAJBgNV
# BAYTAlBMMSIwIAYDVQQKExlVbml6ZXRvIFRlY2hub2xvZ2llcyBTLkEuMScwJQYD
# VQQLEx5DZXJ0dW0gQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkxHDAaBgNVBAMTE0Nl
# cnR1bSBMZXZlbCBJSUkgQ0EwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIB
# AQCfUZZcS3wuSUcINT8L7UkdKmpeWGhNCNc/eJdyMUTcYZT1lOnTzZ0drfHk+QeR
# +f6kCZz7x54x4xsD3Pz1xUsiqa26p+GVZWOsK+KA/WF2Z+jEpDz+dOh2eB5JpRR5
# 3HSmn7YSiq4NWfxagCWYwEic28sPd+eG9bLH1k67h1AGTnb1t4wof1/i2uowieRE
# hu5V95V57wyIyn//XyUS7ymkw9/IUZ6LEJVX+urdN71Kpl9qlUXXvPOVUrMU8w6J
# OhO7gEA8y6D6jtKmRHLcN/4Ug+0Ag/GQEfwO8UPsbfBzA8sMfteClhw3zufuKGSr
# tW8GWqAESrYNe1Wce2sYwlrHAgMBAAGjggEBMIH+MA8GA1UdEwEB/wQFMAMBAf8w
# DgYDVR0PAQH/BAQDAgEGMB0GA1UdDgQWBBQEydqa3EpJd68wAwRmLsfO8vgXfTBS
# BgNVHSMESzBJoUKkQDA+MQswCQYDVQQGEwJQTDEbMBkGA1UEChMSVW5pemV0byBT
# cC4geiBvLm8uMRIwEAYDVQQDEwlDZXJ0dW0gQ0GCAwEAIDAsBgNVHR8EJTAjMCGg
# H6AdhhtodHRwOi8vY3JsLmNlcnR1bS5wbC9jYS5jcmwwOgYDVR0gBDMwMTAvBgRV
# HSAAMCcwJQYIKwYBBQUHAgEWGWh0dHBzOi8vd3d3LmNlcnR1bS5wbC9DUFMwDQYJ
# KoZIhvcNAQEFBQADggEBAIvCzDjOR2ApbA5IvG47OAoN4BefeTwRspwdkMm9vwOi
# WfKwVOI7kh+pb2MiF5xYpEEdYeuZJCjwcMcqzOgZ4CiQXOQ0kdFQaPxuxX9kijCP
# hm0sWVRimGGiXSs7KLBx/vRcaFjm/NNhlwQ6z+yx3XIfc26Zc8hqpF993Z2ei4x7
# 6sXsd/dkDu3u5a1GzBplTq9EHW5nZENquQxv1gQfX+Ua4Dmp9a/9tchmbDMPc+VD
# IaT99SO1cfHS7OyzUX0Ew7mZfEyeRo3N9GP8To60q8eCyJNuBEySttNcHmGKKiM2
# bjjSPqSvHnXaJTMwWP7o0/krJu183xKbIVOaDLEafn4wggXmMIIEzqADAgECAhBy
# Ns+bzOD8lq3+eRECdJKyMA0GCSqGSIb3DQEBBQUAMHgxCzAJBgNVBAYTAlBMMSIw
# IAYDVQQKExlVbml6ZXRvIFRlY2hub2xvZ2llcyBTLkEuMScwJQYDVQQLEx5DZXJ0
# dW0gQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkxHDAaBgNVBAMTE0NlcnR1bSBMZXZl
# bCBJSUkgQ0EwHhcNMTMwOTA2MDg0MTE2WhcNMTQwOTA2MDg0MTE2WjCBjDELMAkG
# A1UEBhMCUlUxHjAcBgNVBAoMFU9wZW4gU291cmNlIERldmVsb3BlcjE0MDIGA1UE
# AwwrT3BlbiBTb3VyY2UgRGV2ZWxvcGVyLCBBbGVrc2FuZHIgVmlzaG55YWtvdjEn
# MCUGCSqGSIb3DQEJARYYYXN2aXNobnlha292QGhvdG1haWwuY29tMIIBIjANBgkq
# hkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxViU/8XgIGzIYzK35FPMPgV+plbOuj7c
# J2lv61DHLih78rPRC0pHyJwyfwpml1LPcTo7IPaLrDvUYyRj7d8PT4G/9ov5NO4l
# J5HZFZyoUFbHzveA0LU6ZXeDD3TeG4byjnenbaK6pPeuVevLabEd6qqfvoNsruBU
# nJLnVGoDMnGa24EV9WyJNhmF8M8BcHIF/3+bkvs/GpJ6GJyb6W/Fz/lASk6YC4Xd
# 1G0Fxtwy7TJhBZNJnwyzOLwRLJfdoWFpamywdYvARWihYFEQzml/acRRXsbH7jFp
# cq2vSit6Avbgv7WRhvdo3epDe6PcGkgZQx7TyMiKX97rSzcjRehQiwIDAQABo4IC
# VTCCAlEwDAYDVR0TAQH/BAIwADAsBgNVHR8EJTAjMCGgH6AdhhtodHRwOi8vY3Js
# LmNlcnR1bS5wbC9sMy5jcmwwWgYIKwYBBQUHAQEETjBMMCEGCCsGAQUFBzABhhVo
# dHRwOi8vb2NzcC5jZXJ0dW0ucGwwJwYIKwYBBQUHMAKGG2h0dHA6Ly93d3cuY2Vy
# dHVtLnBsL2wzLmNlcjAfBgNVHSMEGDAWgBQEydqa3EpJd68wAwRmLsfO8vgXfTAd
# BgNVHQ4EFgQUfwKeHfqapXNoIJqLdQP3q/OBHSIwDgYDVR0PAQH/BAQDAgeAMIIB
# PQYDVR0gBIIBNDCCATAwggEsBgoqhGgBhvZ3AgIDMIIBHDAlBggrBgEFBQcCARYZ
# aHR0cHM6Ly93d3cuY2VydHVtLnBsL0NQUzCB8gYIKwYBBQUHAgIwgeUwIBYZVW5p
# emV0byBUZWNobm9sb2dpZXMgUy5BLjADAgEHGoHAVXNhZ2Ugb2YgdGhpcyBjZXJ0
# aWZpY2F0ZSBpcyBzdHJpY3RseSBzdWJqZWN0ZWQgdG8gdGhlIENFUlRVTSBDZXJ0
# aWZpY2F0aW9uIFByYWN0aWNlIFN0YXRlbWVudCAoQ1BTKSBpbmNvcnBvcmF0ZWQg
# YnkgcmVmZXJlbmNlIGhlcmVpbiBhbmQgaW4gdGhlIHJlcG9zaXRvcnkgYXQgaHR0
# cHM6Ly93d3cuY2VydHVtLnBsL3JlcG9zaXRvcnkuMBMGA1UdJQQMMAoGCCsGAQUF
# BwMDMBEGCWCGSAGG+EIBAQQEAwIEEDANBgkqhkiG9w0BAQUFAAOCAQEAeK/PxHKK
# xjFN2chB0DFDdLwVGsPr1qjISdR5Sty/ieUkE6i//dHDc/wgVMY9fzEXeZB1t+gA
# azFYYf0MTSdPK/iPOFRtaUGG0uZEFEW0poZrZvyI/VL0DqX6HkBQSfhbHxwtoqEH
# VehfmCuU+nRphRaqrCmg74GS0eXgH7WnBfikAPgYsZ2KQuD9xd8qz7L+8Oz801en
# XuDxoB7bR4HDGerFbhNz34odeXakNMLntDXIm929vdwwfqQEb4lx5oFPHV4aNpKI
# vWGFDYLlX+fxW5xKSRIBR5h+jBQyVrRJiq6/xgypv7iM+4+2JHYQcm5LmZg0PmDf
# R/zvI5JSzIaqZDGCAi4wggIqAgEBMIGMMHgxCzAJBgNVBAYTAlBMMSIwIAYDVQQK
# ExlVbml6ZXRvIFRlY2hub2xvZ2llcyBTLkEuMScwJQYDVQQLEx5DZXJ0dW0gQ2Vy
# dGlmaWNhdGlvbiBBdXRob3JpdHkxHDAaBgNVBAMTE0NlcnR1bSBMZXZlbCBJSUkg
# Q0ECEHI2z5vM4PyWrf55EQJ0krIwCQYFKw4DAhoFAKB4MBgGCisGAQQBgjcCAQwx
# CjAIoAKAAKECgAAwGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQwHAYKKwYBBAGC
# NwIBCzEOMAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFC9/cQC6Ux8k6m3u
# g0SkdN0mSYRIMA0GCSqGSIb3DQEBAQUABIIBAF1AKhyVUS8A84Dvs15j0rt+AuXk
# PuoGZLl5i55AFwdBzj1iXudK3Cw0I/X37Sixxj64O8+L565aNyxW2igENv9zyBPA
# wjsxTpLXp49yK0Tue5XjaKz38Gf2D2/cBXJV6pwYUuGzzqUcNzXTqaT33BFPAT0A
# 6EHNOls6AV3JVe8gJUEYbNTfOkc8sgPSx7a/TBBzPJQDYTS7SNry5j/QsguhFDq3
# YZ/6Og3/pV1bP9SztBdQlp+qlJRdarJY3avdQQhcwcR2imNYB38UOUPoU2BDyIwH
# I0rMkJkq7ZXdn/N2lPfYc/Un2Wb/KseaCO94gMldJKaloXRFi1zFolRhwlQ=
# SIG # End signature block
