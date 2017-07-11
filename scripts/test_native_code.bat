@echo off
setlocal EnableDelayedExpansion
echo Test discovery started...
dir C:\projects\spectre\*Tests.exe /b /s | findstr /v obj > __tmp_gtest.txt

echo Testing (Google Test)...

FOR /F %%i IN (__tmp_gtest.txt) DO (
	echo %%i
	%%i
)
del __tmp_gtest.txt
