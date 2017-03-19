@echo off
setlocal EnableDelayedExpansion
echo Test discovery started...
dir C:\projects\spectre\*.tests.dll /b /s | findstr /v obj > __tmp.txt

set isnt=^^!=
set conditions=
if "%2"=="" goto test
set conditions="cat !isnt! %2
:parse_next
shift
if "%2"=="" (
    set conditions=!conditions!"
    goto test
)
set conditions=!conditions! && cat !isnt! %2
goto parse_next

:test
set dirs=
FOR /F %%i IN (__tmp.txt) DO set dirs=!dirs! %%i
del __tmp.txt

echo Testing (NUnit3)...
nunit3-console %dirs% --where !conditions! --result=myresults.xml;format=AppVeyor
