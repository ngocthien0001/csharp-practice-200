@echo off
cd /d "%~dp0\.."
git init
git add .
git commit -m "Initial C# practice repository with offline judge"
echo Done.
pause
