if (!([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) { Start-Process powershell.exe "-NoProfile -ExecutionPolicy Bypass -File `"$PSCommandPath`"" -Verb RunAs; exit }

(New-Object Net.WebClient).DownloadFile('https://nodejs.org/dist/v8.9.4/node-v8.9.4-x64.msi', 'node.msi')
Start-Process msiexec.exe -ArgumentList "/i node.msi /quiet" -Wait | Wait-Process
Remove-Item node.msi
$env:Path = $env:Path + ";C:\Program Files\nodejs"
Write-Host "Installing npm..." -ForegroundColor Yellow
npm install -g npm@5.6.0
Write-Host "Installing angular-cli..." -ForegroundColor Yellow
cmd /C "npm install -g @angular/cli@1.7.1 --loglevel=error"
cd ..
Write-Host "Installing dependencies..." -ForegroundColor Yellow
npm install --loglevel=error | Out-Null
cd scripts
Write-Host "Press any key..."
$host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
