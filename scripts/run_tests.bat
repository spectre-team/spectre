@echo off
setlocal EnableDelayedExpansion
dir *.tests.dll /b /s | findstr /v obj > __tmp.txt
set dirs=
FOR /F %%i IN (__tmp.txt) DO set dirs=!dirs! %%i
del __tmp.txt
vstest.console /logger:Appveyor %dirs% /Platform:x64