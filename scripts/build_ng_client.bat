cd C:\projects\spectre\src\Spectre.AngularClient
if "%1" == "Release" (
    ng build -prod --aot=false >nul 2>&1
) else ( if "%1" == "Bit" (
    ng build -env=bit --aot=false >nul 2>&1
)  else (
    ng build >nul 2>&1
) )
