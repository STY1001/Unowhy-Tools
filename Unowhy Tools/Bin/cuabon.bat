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
reg add "HKCU\Software\STY1001\Unowhy Tools" /v UpdateStart /d 1 /t REG_SZ /f