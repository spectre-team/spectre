cd C:\projects\spectre
if "%CONFIGURATION%" == "Release" (
    7z a WebClient-Bit.zip "%APPVEYOR_BUILD_FOLDER%\src\Spectre.AngularClient\dist\*" >nul 2>&1
)

