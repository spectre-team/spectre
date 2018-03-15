@echo off
cd C:\projects\spectre\src\Spectre.AngularClient
IF EXIST "C:\projects\spectre\karma-tests.xml" (
    powershell C:\projects\spectre\scripts\Upload-TestResult.ps1 -fileName C:\projects\spectre\karma-tests.xml
)
IF EXIST "C:\projects\spectre\protractor-tests.xml" (
    powershell C:\projects\spectre\scripts\Upload-TestResult.ps1 -fileName C:\projects\spectre\protractor-tests.xml
)
