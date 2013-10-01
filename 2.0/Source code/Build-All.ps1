param (
    [Parameter(Position = 2, Mandatory = $False,  HelpMessage = "Key file used to strong name sign assembly.")]
    [string] $AssemblyKey     = (Resolve-Path "..\..\SigningKey.pfx"),

    [Parameter(Position = 3, Mandatory = $False,  HelpMessage = "Key file used to digital signature sign assembly.")]
    [string] $SignatureKey    = (Resolve-Path "..\..\SigningKey.pfx"),

    [Parameter(Position = 4, Mandatory = $False,  HelpMessage = "Assembly digital signature parameters.")]
    [string] $SignatureParams = "/t http://timestamp.comodoca.com/authenticode",

    [Parameter(Position = 5, Mandatory = $False, HelpMessage = "Number of working threads (recommended one thread per logical core). Default is 2.")]
    [byte]   $Threads         = 8,

    [Parameter(Position = 6, Mandatory = $False, HelpMessage = "Switch, if you try build mapped code.")]
    [bool]   $Checkout        = $True
)

$Location = Split-Path -Parent $MyInvocation.MyCommand.Path
$Path = Join-Path -Path $Location -ChildPath Build.ps1

"Started building in background..."
$BuildNETFX4 = Start-Job -ScriptBlock {
    & $args[0] -Version 2012 -Framework NETFX4 -AssemblyKey $args[1] -SignatureKey $args[2] -SignatureParams $args[3] -Threads $args[4] -Checkout $args[5]
    Try-Exit
} -ArgumentList @($Path, $AssemblyKey, $SignatureKey, $SignatureParams, $Threads, $Checkout)
Wait-Job $BuildNETFX4
Receive-Job $BuildNETFX4
"Completed building in background..."

"Started building in background..."
$BuildNETFX45 = Start-Job -ScriptBlock {
    & $args[0] -Version 2012 -Framework NETFX45 -AssemblyKey $args[1] -SignatureKey $args[2] -SignatureParams $args[3] -Threads $args[4] -Checkout $args[5]
    Try-Exit
} -ArgumentList @($Path, $AssemblyKey, $SignatureKey, $SignatureParams, $Threads, $Checkout)
Wait-Job $BuildNETFX45
Receive-Job $BuildNETFX45
"Completed building in background..."
# SIG # Begin signature block
# MIIGAwYJKoZIhvcNAQcCoIIF9DCCBfACAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUfEytDn0CviKqg4dws2PzIBWj
# yLegggN8MIIDeDCCAmCgAwIBAgIQOmW6/axc969KEPAZJomtbzANBgkqhkiG9w0B
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
# PCn/mdqj1bby6BT6Fugvzo/J9x4wDQYJKoZIhvcNAQEBBQAEggEAIVQ0LUNyVuNO
# 0JTKKF2kX4dLmq/SEx5JYVMqxlpE6Dw2R6UXjoNikvK//cvjKThdHFM+WVrq9qQO
# vJas+rSg4+gRWeSGUc8SSv2nEzucMMBx+gqH48nUze5XDbh2Vzk0E6xeATDibj1O
# 0K2Ryc/ZSCyIojPueoBkX0IhhRdocItPpuXUoP0kzst/DylcjG4yiRBFWRqXhRnL
# blrlD1MsUFh40rKF5B8IpiGp2JtII2dUOUm3KWEBHTJTJj0janCqIJ6sIch15wpd
# dZcQK5eg/FiXpcSgxSC7bbhDNoP1XJHOwm2Nx3vNe48fvY8SbdZ3cB2sOKSzTFhY
# M4OkziRGgQ==
# SIG # End signature block
