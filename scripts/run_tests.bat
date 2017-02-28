setlocal EnableDelayedExpansion
echo Test discovery started...
dir *.tests.dll /b /s | findstr /v obj > __tmp.txt
echo Saved
set dirs=
echo Iterating
FOR /F %%i IN (__tmp.txt) DO set dirs=!dirs! %%i
echo Removing tmp
del __tmp.txt
echo Tests found: %dirs%
vstest.console /logger:Appveyor %dirs% /Platform:x64