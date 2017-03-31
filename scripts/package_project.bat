cd C:\projects\spectre
7z a %1 "%APPVEYOR_BUILD_FOLDER%\src\%2\bin\x64\%CONFIGURATION%\*.dll"
7z a %1 "%APPVEYOR_BUILD_FOLDER%\src\%2\bin\x64\%CONFIGURATION%\%2.exe"
7z a %1 "%APPVEYOR_BUILD_FOLDER%\docs\%2.pdf"
