@echo off
title Unowhy Tools by STY1001
echo =============================
echo    Unowhy Tools by STY1001   
echo
echo        Please wait...
echo      Veuillez patienter...
echo 
echo         Do not close
echo         Ne pas fermer
echo =============================
MODE CON: COLS=30 LINES=10
powershell -windows minimize -command ""
cd "C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup"
del /q /f "silent_*.*"