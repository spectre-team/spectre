@echo off
cd C:\projects\spectre\src\Spectre.AngularClient
if "%CONFIGURATION%" == "Release" (
    ng e2e --progress false -prod --aot=false
) else (
    ng e2e --progress false
)
