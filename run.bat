@echo off
cd /d "%~dp0"
echo start ASP.NET Core Web API...
start cmd /k "dotnet run --urls http://localhost:5134"
timeout /t 5 /nobreak >nul
echo API start: http://localhost:5134/swagger/index.html
start "" "http://localhost:5134/swagger/index.html"
pause