cd C:\projects\spectre
7z a %1-%CONFIGURATION%.zip "%APPVEYOR_BUILD_FOLDER%\Deployments\%2\*" >nul 2>&1
