if (!([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) { Start-Process powershell.exe "-NoProfile -ExecutionPolicy Bypass -File `"$PSCommandPath`"" -Verb RunAs; exit }

Invoke-WebRequest https://nodejs.org/dist/v6.10.0/node-v6.10.0-x64.msi -OutFile node.msi
Start-Process msiexec.exe -ArgumentList "/i node.msi /quiet" -Wait | Wait-Process
Remove-Item node.msi
$env:Path = $env:Path + ";C:\Program Files\nodejs"
npm install -g npm
npm install -g @angular/cli
Write-Host "Press any key..."
$host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")