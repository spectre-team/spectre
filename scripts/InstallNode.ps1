Install-Product node 8.9.4 x64
Write-Host "Installing npm..." -ForegroundColor Yellow
npm install -g npm@5.6.0 | Out-Null
$env:path = $env:appdata + "\npm;" + $env:path
Write-Host "Installing angular-cli..." -ForegroundColor Yellow
npm install -g @angular/cli@1.7.1 --loglevel=error | Out-Null
Write-Host "Installing dependencies..." -ForegroundColor Yellow
npm install --loglevel=error | Out-Null
