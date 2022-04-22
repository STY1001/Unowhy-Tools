@echo off
echo ===Registering the REG file...===
cd "C:\Program Files\Unowhy-Tools\bin"
reg import ".\shell.reg"
echo ===Done===
exit