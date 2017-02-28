$registryPath = "HKLM:\Software\Microsoft\Fusion"
$name = "EnableLog"
$value = "1"
New-ItemProperty -Path $registryPath -Name $name -Value $value -PropertyType DWORD
Write-Host "Assembly resolving logs enabled." -ForegroundColor Yellow