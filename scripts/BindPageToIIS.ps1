Import-Module "WebAdministration"
# New-Item 'IIS:\Sites\Default Web Site\spectre' -physicalPath 'c:\projects\spectre\src\Spectre.Angular2Client' -type Application
New-Item iis:Sites\Spectre -bindings @{protocol="https";bindingInformation=":440"} -physicalPath c:\projects\spectre\src\Spectre.Angular2Client\dist | Out-Null
# spawn IIS worker
Invoke-WebRequest https://localhost:440 -TimeoutSec 120