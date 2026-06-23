@echo off
chcp 65001 >nul
cd /d "%~dp0"
echo Starting CSharp Practice 200 GUI...
dotnet run --project src\CSharpPractice.App\CSharpPractice.App.csproj
if errorlevel 1 (
    echo.
    echo Khong mo duoc giao dien. Hay kiem tra da cai .NET SDK 8 chua.
    echo Chay lenh: dotnet --version
    pause
)
