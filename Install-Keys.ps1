param (
    [Parameter(Mandatory = $True, HelpMessage = "Specifies your Visual Studio version (2010 or 2012).")]
    [ValidateSet("2010", "2012")]
    [string] $Version
)

$Location = Split-Path -Parent $MyInvocation.MyCommand.Path
$Path = Join-Path -Path $Location -ChildPath Helpers.psm1

Import-Module $Path

Import-Variables -Version $Version

& certmgr.exe -add -c (Join-Path -Path $Location -ChildPath "RootCertificate.cer") -s -r localMachine root
Try-Exit

& sn -i (Join-Path -Path $Location -ChildPath "SigningKey.pfx") VS_KEY_495CE44A959FD928
Try-Exit

Read-Host -Prompt "Press any key to continue..."
# SIG # Begin signature block
# MIIGAwYJKoZIhvcNAQcCoIIF9DCCBfACAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUESkulw4z1Lx7LK5kJGdGAQHC
# nL2gggN8MIIDeDCCAmCgAwIBAgIQOmW6/axc969KEPAZJomtbzANBgkqhkiG9w0B
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
# Oe/WmAnXheudWrsCoZ2iiTyjP8kwDQYJKoZIhvcNAQEBBQAEggEAA3jajcS3Wczj
# d20qHtB4s/VAlxI8o6KkH4A7p3E9zRWVUvPS9lmrLEOIn94djcIg2WxXmZ3C6+QX
# ZLr+zKX6SZY4gqEoteGmW9WeW8cpZO0JI7byhDewJH42AmWzKkJrNjLYe5bntopi
# ai4xPxpfWtddz9KntVJFVwE69RFNQfu6NGHGcuIDRx5Hr0TiUTbUoKKrcIZ+8U2+
# 4xcQ24DoSTqFVR+zT6IZLiIYDgMiOylljcCgfjN7XCAcL4irLrp50vAEkOQCiZQT
# ZEzlJZqmbztaC4zNLtSFLvHIK3idjmQ2Uy+NKqj5EeSKQgCkupHhJnfCVpwhVMbD
# 0T2EiOrW9A==
# SIG # End signature block
