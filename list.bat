@echo off
chcp 65001 >nul
cd /d "%~dp0"

where dotnet >nul 2>nul
if errorlevel 1 (
    echo [LOI] May chua cai .NET SDK 8 hoac dotnet chua nam trong PATH.
    pause
    exit /b 1
)

dotnet run --project src\CSharpPractice.Judge -- list
pause
