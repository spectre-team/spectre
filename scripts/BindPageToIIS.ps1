Import-Module "WebAdministration"
New-Item iis:Sites\Spectre -bindings @{protocol="http";bindingInformation="*:440:"} -physicalPath c:\projects\spectre\src\Spectre.Angular2Client\dist | Out-Null
# spawn IIS worker
Invoke-WebRequest https://localhost:440 -TimeoutSec 120