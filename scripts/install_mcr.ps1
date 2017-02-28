Write-Host "Gathering MCR installer..." -ForegroundColor Yellow
(new-object net.webclient).DownloadFile('https://www.mathworks.com/supportfiles/downloads/R2016b/deployment_files/R2016b/installers/win64/MCR_R2016b_win64_installer.exe', 'mcr_setup.exe')
Write-Host "MCR installer downloaded. Installation..." -ForegroundColor Yellow
$job = Start-Job { mcr_setup.exe -mode silent -agreeToLicense yes }
Wait-Job $job
Receive-Job $job
Write-Host "MCR installed." -ForegroundColor Yellow