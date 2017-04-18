Param(
    [switch]$AppVeyor
)

if($AppVeyor -eq $True)
{
    $ProjectRoot = "c:\projects\spectre"
    Set-Location $ProjectRoot
}
else
{
    $ProjectRoot = Get-Location | Select-String -Pattern spectre -SimpleMatch
}

Write-Host "Using $($ProjectRoot) as project root."

Try
{
    Write-Host "Adding $($ProjectRoot)\src\Spectre as an application."
    Import-Module "WebAdministration"
    New-Item 'IIS:\Sites\Default Web Site\spectre_api' -physicalPath "$($ProjectRoot)\src\Spectre" -type Application
    #New-Item 'IIS:\Sites\Default Web Site\spectre_api' -physicalPath "$($ProjectRoot)\src\Spectre" -type VirtualDirectory

    Write-Host "Added API binding to IIS." -foregroundcolor green
}
Catch
{
    Write-Host $_.Exception.Message -foregroundcolor red -backgroundcolor black
    Write-Host "Failed to bind page to IIS." -foregroundcolor red
}

Try
{
    New-Item c:\spectre_data -type Directory
    $Acl = Get-Acl c:\spectre_data
    $Ar = New-Object System.Security.AccessControl.FileSystemAccessRule("IIS_IUSRS", "FullControl", "Allow")
    $Acl.SetAccessRule($Ar)
    Set-Acl c:\spectre_data $Acl
    Copy-Item "$($ProjectRoot)\test_files\*" c:\spectre_data

    Write-Host "Test data copied to default data directory." -foregroundcolor green
}
Catch
{
    Write-Host "Failed to copy data default directory." -foregroundcolor red
}
