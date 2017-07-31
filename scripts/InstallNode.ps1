Install-Product node 6.10.0 x64
Write-Host "Installing npm..." -ForegroundColor Yellow
npm install -g npm@4.4.1
$env:path = $env:appdata + "\npm;" + $env:path
Write-Host "Installing angular-cli..." -ForegroundColor Yellow
npm install -g @angular/cli@1.0.0-rc.2 --loglevel=error
cd .\src\Spectre.AngularClient
Write-Host "Installing dependencies..." -ForegroundColor Yellow
npm install --loglevel=error
cd ..\..
