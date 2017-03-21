Install-Product node 6.10.0 x64
npm install -g npm | Out-Null
$env:path = $env:appdata + "\npm;" + $env:path
cmd /C "npm install -g @angular/cli --loglevel=error"
cd .\src\Spectre.Angular2Client
npm install --loglevel=error | Out-Null
cd ..\..
