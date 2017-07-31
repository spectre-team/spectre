@echo off
cd src
nuget sources add -Name GMrukwaAppVeyorFeed -Source https://ci.appveyor.com/nuget/gmrukwa-xfn7vhwq20u6 -UserName %nuget_user% -Password %nuget_password%
if errorlevel 1 (
	echo Connection to GMrukwaAppVeyorFeed failed.
	EXIT /B 1
)
nuget restore >nul 2>&1
if errorlevel 1 (
	echo NuGet failed to restore packages.
	EXIT /B 1
)
cd ..