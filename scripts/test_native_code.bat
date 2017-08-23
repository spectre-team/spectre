@echo off
setlocal EnableDelayedExpansion
echo Test discovery started...
dir C:\projects\spectre\*Tests.exe /b /s | findstr /v obj > __tmp_gtest.txt

echo Testing (Google Test)...

set failures=0

FOR /F %%i IN (__tmp_gtest.txt) DO (
	echo %%i
	%%i --gtest_output="xml:%%i.xml"
	powershell C:\projects\spectre\scripts\Upload-TestResult.ps1 -fileName %%i.xml
	IF %ERRORLEVEL% NEQ 0 (
		set /A failures=%failures%+1
	)
)
del __tmp_gtest.txt

EXIT /B %failures%
