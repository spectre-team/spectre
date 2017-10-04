cd C:\projects\spectre
7z a WebClient-%CONFIGURATION%.zip "%APPVEYOR_BUILD_FOLDER%\src\Spectre.AngularClient\dist\*" >nul 2>&1
if %CONFIGURATION% == "Release" (
    CALL C:\spectre\scripts\build_ng_client.bat Bit
    7z a WebClient-Bit.zip "%APPVEYOR_BUILD_FOLDER%\src\Spectre.AngularClient\dist\*" >nul 2>&1
)

