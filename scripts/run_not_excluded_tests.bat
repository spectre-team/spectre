@echo off
setlocal EnableDelayedExpansion
echo Test discovery started...
dir *.tests.dll /b /s | findstr /v obj %excluded% > __tmp.txt

REM EXCLUSIONS
:exclusions
if "%1"=="" goto test
echo Excluding %1
type __tmp.txt | findstr /v %1 > __tmp1.txt
move __tmp1.txt __tmp.txt > nul 2>&1
shift
goto exclusions

:test
set dirs=
FOR /F %%i IN (__tmp.txt) DO set dirs=!dirs! %%i
del __tmp.txt
echo Tests found: %dirs%
vstest.console /logger:Appveyor %dirs% /Platform:x64 /inIsolation