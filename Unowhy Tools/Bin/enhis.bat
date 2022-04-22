@echo off
echo ===Enabling HiSqool Manager Services...===
sc config hisqoolmanager start=auto
echo ===Starting HiSqool Manager Services...===
net start HiSqoolManager
echo ===Done===
exit