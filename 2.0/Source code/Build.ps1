param (
    [Parameter(Position = 0, Mandatory = $False, HelpMessage = "Specifies your Visual Studio version (2010 or 2012). Default is 2012.")]
    [ValidateSet("2010", "2012")]
    [string] $Version         = "2012",

    [Parameter(Position = 1, Mandatory = $False, HelpMessage = "Specifies target framework (NETFX4 or NETFX45). Default is NETFX45.")]
    [ValidateSet("NETFX4", "NETFX45")]
    [string] $Framework       = "NETFX45",

    [Parameter(Position = 2, Mandatory = $False,  HelpMessage = "Key file used to strong name sign assembly.")]
    [string] $AssemblyKey     = (Resolve-Path "..\..\SigningKey.pfx"),

    [Parameter(Position = 3, Mandatory = $False,  HelpMessage = "Key file used to digital signature sign assembly.")]
    [string] $SignatureKey    = (Resolve-Path "..\..\SigningKey.pfx"),

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
Build-Projects -Version $Version -Framework $Framework -Configuration Debug -Transform $True -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Threads $Threads -Checkout $Checkout
Build-Projects -Version $Version -Framework $Framework -Configuration Release -Transform $True -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Threads $Threads -Checkout $Checkout

# Build project and item templates for Visual Studio
Build-Templates -Version $Version -Framework $Framework -LCID 1033 -Threads $Threads -Checkout $Checkout
Build-Templates -Version $Version -Framework $Framework -LCID 1049 -Threads $Threads -Checkout $Checkout

# Zip project and item templates for Visual Studio
Zip-Templates -Version $Version -Framework $Framework -LCID 1033 -Threads $Threads -Checkout $Checkout
Zip-Templates -Version $Version -Framework $Framework -LCID 1049 -Threads $Threads -Checkout $Checkout

# Build Elysium documentation
Build-Documentation -Version $Version -Framework $Framework -Language en -Threads $Threads
Build-Documentation -Version $Version -Framework $Framework -Language ru -Threads $Threads

# Build installation projects
Build-Installation -Version $Version -Framework $Framework -Configuration Debug -Transform $True -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Threads $Threads -Checkout $Checkout
Build-Installation -Version $Version -Framework $Framework -Configuration Release -Transform $True -AssemblyKey $AssemblyKey -SignatureKey $SignatureKey -SignatureParams $SignatureParams -Threads $Threads -Checkout $Checkout
# SIG # Begin signature block
# MIIM6AYJKoZIhvcNAQcCoIIM2TCCDNUCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUrQYSMLz2YXDPO5Nzp0oOxbVb
# x06gggokMIIENjCCAx6gAwIBAgIDBHpTMA0GCSqGSIb3DQEBBQUAMD4xCzAJBgNV
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
# NwIBCzEOMAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFATeqM6oeve2dSpu
# YYqnhsAkXAYqMA0GCSqGSIb3DQEBAQUABIIBAHfP8lL+a0AiFMaIuiwNtCcwyE7R
# M9vXbOcGKRAX1TkAr44y8Hq+BnUYzZkEhdBBLGaJu8EdlRTmYhzgaP9kohzA+MCl
# H7hxfqjsnaRpZwCnr/x3N945fzBJDHmkpWBIJc4rhl/O9U4twatENMJw+WtBTIgp
# hWghkrw5eCa78Sut+VRtjWpusmX/xl+VGooKixcwr1MGl2GD+MfRjhHDuKNEgfBH
# dOc3m019suWPoRQ3AMEitdvgm5pRVpKPLnxKZ598QLjIXXe5EeQ76LSWa29NbeC/
# 5G0rSIAaWJkcNMv/m1GpPCXXifdr1bXGMxgaoSsiPxyXIhnKmgBXyuW1f2s=
# SIG # End signature block
