if (!([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) { Start-Process powershell.exe "-NoProfile -ExecutionPolicy Bypass -File `"$PSCommandPath`"" -Verb RunAs; exit }

(New-Object Net.WebClient).DownloadFile('https://nodejs.org/dist/v6.10.0/node-v6.10.0-x64.msi', 'node.msi')
Start-Process msiexec.exe -ArgumentList "/i node.msi /quiet" -Wait | Wait-Process
Remove-Item node.msi
$env:Path = $env:Path + ";C:\Program Files\nodejs"
npm install -g npm
cmd /C "npm install -g @angular/cli --loglevel=error"
cd ..\src\Spectre.Angular2Client
npm install --loglevel=error | Out-Null
cd ..\..\scripts
Write-Host "Press any key..."
$host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")