$Location = Split-Path -Parent $MyInvocation.MyCommand.Path
$Path = Join-Path -Path $Location -ChildPath Elysium.psm1

Import-Module $Path

Deploy -Root $Location -Framework NETFX4
Deploy -Root $Location -Framework NETFX45