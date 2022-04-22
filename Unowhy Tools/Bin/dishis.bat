@echo off
echo ===Stoping HiSqool Manager Services...===
net stop HiSqoolManager
echo ===Disabling HiSqool Manager Services...===
sc config hisqoolmanager start=disabled
echo ===Done===
exit