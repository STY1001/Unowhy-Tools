@echo off
echo ===Registering the REG file...===
cd "C:\Program Files (x86)\Unowhy-Tools\"
reg import ".\shell.reg"
echo ===Done===
exit
