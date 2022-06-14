::[Bat To Exe Converter]
::
::YAwzoRdxOk+EWAjk
::fBw5plQjdCyDJGyX8VAjFDN9aCW7GV/0KpQs3MfLorzThR0+A8ASRKvU2aGDJe4H+XnRQcdjhkZzm8QCMAtbdxytYUE9qmEi
::YAwzuBVtJxjWCl3EqQJgSA==
::ZR4luwNxJguZRRnk
::Yhs/ulQjdF+5
::cxAkpRVqdFKZSDk=
::cBs/ulQjdF+5
::ZR41oxFsdFKZSDk=
::eBoioBt6dFKZSDk=
::cRo6pxp7LAbNWATEpCI=
::egkzugNsPRvcWATEpCI=
::dAsiuh18IRvcCxnZtBJQ
::cRYluBh/LU+EWAnk
::YxY4rhs+aU+JeA==
::cxY6rQJ7JhzQF1fEqQJQ
::ZQ05rAF9IBncCkqN+0xwdVs0
::ZQ05rAF9IAHYFVzEqQJQ
::eg0/rx1wNQPfEVWB+kM9LVsJDGQ=
::fBEirQZwNQPfEVWB+kM9LVsJDGQ=
::cRolqwZ3JBvQF1fEqQJQ
::dhA7uBVwLU+EWDk=
::YQ03rBFzNR3SWATElA==
::dhAmsQZ3MwfNWATElA==
::ZQ0/vhVqMQ3MEVWAtB9wSA==
::Zg8zqx1/OA3MEVWAtB9wSA==
::dhA7pRFwIByZRRnk
::Zh4grVQjdCyDJGyX8VAjFDN9aCW7GV/0KpQs3MfLorzThR0+A8ASRKvU2aGDJe4H+XnRQcdjhkZ8kcUNACdadxyXaQoguW1LuGKKecKEtm8=
::YB416Ek+ZW8=
::
::
::978f952a14a936cc963da21a135fa983
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
hostname > pcname.txt
whoami > username.txt
wmic /output:mf.txt computersystem get manufacturer
wmic /output:model.txt computersystem get model
wmic /output:os.txt os get caption
wmic /output:ene.txt bios get smbiosbiosversion
wmic /output:ifp.txt bios get serialnumber
powershell Get-ItemPropertyValue -path 'HKLM:SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon' -name 'Shell' > shell.txt