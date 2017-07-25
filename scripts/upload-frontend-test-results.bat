@echo off
cd C:\projects\spectre\src\Spectre.Angular2Client
IF EXIST "C:\projects\spectre\src\Spectre.Angular2Client\karma-tests.xml" (
    powershell C:\projects\spectre\scripts\Upload-TestResult.ps1 -fileName C:\projects\spectre\src\Spectre.Angular2Client\karma-tests.xml
)
