@echo off
chcp 65001 >nul
cd /d "%~dp0"
dotnet run --project src\CSharpPractice.Judge\CSharpPractice.Judge.csproj -- %*
if errorlevel 1 pause
