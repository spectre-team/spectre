Install-Product node 6.10.0 x64
npm install -g npm | Out-Null
$env:path = $env:appdata + "\npm;" + $env:path
npm install -g @angular/cli | Out-Null
cd .\src\Spectre.Angular2Client
npm install | Out-Null
cd ..\..