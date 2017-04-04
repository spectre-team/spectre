@echo off
for /f %%I in ('dir /b .\docs\*.md') do (
    pandoc docs\%%I -o docs\%%~nI.pdf
    echo %%~nI rendered...
)
