@echo off
cd C:\projects\spectre\src\Spectre.AngularClient
ng test --single-run true --progress false
set fail=FAILED
ng test --single-run true --progress false | findstr %fail%  > __test.txt
set /p failed=<__test.txt
if not "%failed%"=="" appveyor AddMessage "JS tests failed." -Category Error
del __test.txt
