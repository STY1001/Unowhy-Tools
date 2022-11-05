::[Bat To Exe Converter]
::
::YAwzoRdxOk+EWAjk
::fBw5plQjdCyDJGyX8VAjFDN9aCW7GV/0KpQs3MfLorzThR0+A8ASRKvU2aGDJe4H+XnRQcdjhkZzm8QCMB1IMBuoYW8=
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
::Zh4grVQjdCyDJGyX8VAjFDN9aCW7GV/0KpQs3MfLorzThR0+A8ASRKvU2aGDJe4H+XnRQcdjhkZ8kcUNACdadxyXeQY6oWtOumuAOdPSthfkKg==
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
reg add "HKLM\SOFTWARE\Policies\Microsoft\PassportForWork" /v "EnablePinRecovery" /t REG_DWORD /d "1" /f
reg add "HKLM\SOFTWARE\Policies\Microsoft\PassportForWork" /v "RequireSecurityDevice" /t REG_DWORD /d "1" /f
reg add "HKLM\SOFTWARE\Policies\Microsoft\PassportForWork" /v "Enabled" /t REG_DWORD /d "1" /f
reg add "HKLM\SOFTWARE\Policies\Microsoft\PassportForWork" /v "DisablePostLogonProvisioning" /t REG_DWORD /d "0" /f
reg add "HKLM\SOFTWARE\Policies\Microsoft\PassportForWork\DynamicLock" /v "DynamicLock" /t REG_DWORD /d "1" /f
reg add "HKLM\SOFTWARE\Policies\Microsoft\PassportForWork\DynamicLock" /v "Plugins" /t REG_SZ /d "<rule schemaVersion=\"1.0\"> <signal type=\"bluetooth\" scenario=\"Dynamic Lock\" classOfDevice=\"512\" rssiMin=\"-10\" rssiMaxDelta=\"-10\"/> </rule> " /f
reg add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\WinBio\Credential Provider" /v "Domain Accounts" /t REG_DWORD /d "1" /f
