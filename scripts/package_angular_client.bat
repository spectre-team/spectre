cd C:\projects\spectre
7z a WebClient-%CONFIGURATION%.zip "%APPVEYOR_BUILD_FOLDER%\src\Spectre.AngularClient\dist\*" >nul 2>&1
