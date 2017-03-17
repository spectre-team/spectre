Install-Product node 6.10.0 x64
npm install -g npm
$env:path = $env:appdata + "\npm;" + $env:path
cd .\src\Spectre.Angular2Client
npm install | Out-Null
cd ..\..