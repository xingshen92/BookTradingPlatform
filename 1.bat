@echo off
cd /d "%~dp0"
echo start ASP.NET Core Web API...
start /b dotnet run --urls "http://localhost:5134"

:: 等待 API 启动
timeout /t 5 /nobreak >nul

:: 打印 Swagger 网址
echo start: http://localhost:5134/swagger/index.html

:: 自动打开 Swagger 页面（使用默认浏览器）
start "" "http://localhost:5134/swagger/index.html"

:: 保持 CMD 窗口不关闭
pause