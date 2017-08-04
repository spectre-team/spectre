cd C:\projects\spectre
7z a %1-%CONFIGURATION%.zip "%APPVEYOR_BUILD_FOLDER%\src\%2\bin\x64\%CONFIGURATION%\*.dll" >nul 2>&1
7z a %1-%CONFIGURATION%.zip "%APPVEYOR_BUILD_FOLDER%\src\%2\bin\x64\%CONFIGURATION%\%2.exe" >nul 2>&1
7z a %1-%CONFIGURATION%.zip "%APPVEYOR_BUILD_FOLDER%\docs\%2.pdf" >nul 2>&1
