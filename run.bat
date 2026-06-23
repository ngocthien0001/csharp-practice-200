@echo off
chcp 65001 >nul
cd /d "%~dp0"

if not exist "src\CSharpPractice.App\CSharpPractice.App.csproj" (
    echo [LOI] Khong tim thay src\CSharpPractice.App\CSharpPractice.App.csproj
    echo Hay bam run.bat o thu muc goc cua repo.
    pause
    exit /b 1
)

where dotnet >nul 2>nul
if errorlevel 1 (
    echo [LOI] May chua cai .NET SDK 8 hoac dotnet chua nam trong PATH.
    pause
    exit /b 1
)

dotnet run --project src\CSharpPractice.App
if errorlevel 1 pause
