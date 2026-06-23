@echo off
chcp 65001 >nul
cd /d "%~dp0"

if "%~1"=="" (
    echo CSharp Practice 200 - Judge
    echo.
    echo Cach dung:
    echo   judge.bat P001
    echo   judge.bat P001 --sample
    echo   judge.bat list
    echo.
    pause
    exit /b 0
)

where dotnet >nul 2>nul
if errorlevel 1 (
    echo [LOI] May chua cai .NET SDK 8 hoac dotnet chua nam trong PATH.
    pause
    exit /b 1
)

dotnet run --project src\CSharpPractice.Judge -- %*
if errorlevel 1 pause
