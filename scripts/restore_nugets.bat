@echo off
cd src
nuget restore >nul 2>&1
if errorlevel 1 (
	echo NuGet failed to restore packages.
	EXIT /B 1
)
cd ..