#!/bin/bash

# 設定 API 的路徑
API_PATH="/home/book/API"

# 設定 tmux 分頁名稱（您可以自定義名稱）
SESSION_NAME="my_api_session"
WINDOW_NAME="api_window"

# 如果 tmux session 不存在，創建一個新的 session
if ! tmux has-session -t $SESSION_NAME 2>/dev/null; then
    tmux new-session -d -s $SESSION_NAME -n $WINDOW_NAME
fi

# 在指定的 tmux 窗口中執行指令
tmux send-keys -t $SESSION_NAME:$WINDOW_NAME "cd $API_PATH && dotnet BookTradingPlatform.dll" C-m

# 顯示狀態訊息
echo "ASP.NET API 已啟動並在 tmux 分頁中執行！（Session 名稱：$SESSION_NAME，Window 名稱：$WINDOW_NAME）"