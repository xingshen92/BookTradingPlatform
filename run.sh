#!/bin/bash

#變數
APP_PATH="/home/user/myapp"
APP_DLL="YourApi.dll"
LOG_FILE="/var/log/mywebapi.log"

# 查看目前已有的API進程
PID=$(pgrep -f $APP_DLL)

if [ -n "$PID" ]; then
    echo "[$(date)] Web API is already running (PID: $PID). Restarting..." | tee -a $LOG_FILE
    kill -9 $PID  
    sleep 2  
fi

# 進入API目錄並且啟動
echo "[$(date)] Starting Web API..." | tee -a $LOG_FILE
cd $APP_PATH
nohup dotnet $APP_DLL > $LOG_FILE 2>&1 &

echo "[$(date)] Web API started successfully." | tee -a $LOG_FILE