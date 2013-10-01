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
# MIIGAwYJKoZIhvcNAQcCoIIF9DCCBfACAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUrQYSMLz2YXDPO5Nzp0oOxbVb
# x06gggN8MIIDeDCCAmCgAwIBAgIQOmW6/axc969KEPAZJomtbzANBgkqhkiG9w0B
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
# BN6ozqh697Z1Km5hiqeGwCRcBiowDQYJKoZIhvcNAQEBBQAEggEAl14My7N+dGfe
# l7PvHt/FeZ0tMadHFSzBQBzEKwqWp3hKUkkV3RYQ/Jpv/Ghrlyt77g8hiFzBIMJh
# XZUKy6TwR5Ko+D377xlMahngzoRSxtPPLU6/wRJdGX7e1zr9oQFVFx8gCFE7vxFo
# RyDd/i15hp9/kwCRbJTQgd7kMU7bMeaaVYoLSZXa0QdsyLGV7FoO13vfb1Uh+2sI
# iMsaON2skVfcRXNPVp/uZOWMqjOXt+WueQloQqGiPZBw+gPQKo6MA9qldIiP6ptF
# fv9wCSpMi3p1eYJ8VMNFzSnbFdHt70nOW5H5/SyfgOAUNvVo25Shrxg9PPhrJ5vs
# rnF5IuPpkg==
# SIG # End signature block
