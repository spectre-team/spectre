@echo off
cd C:\projects\spectre\src\Spectre.Angular2Client
ng e2e --progress false
set fail="*                    Failures                    *"
ng e2e --progress false | findstr %fail% > __failed.txt
ng e2e --progress false > __e2e.txt
set /p failed=<__failed.txt
set /p report=<__e2e.txt
if not "%failed%"=="" appveyor AddMessage "e2e JS tests failed." -Category Error -Details "%report%"
del __failed.txt
del __e2e.txt
