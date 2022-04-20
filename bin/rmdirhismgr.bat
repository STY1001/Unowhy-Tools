@echo off
echo ===Stoping HiSqool Manager Services...===
net stop HiSqoolManager
echo ===Disabling HiSqool Manager Services...===
sc config hisqoolmanager start=disabled
echo ===Removing Unowhy HiSqool Manager Folder...===
rmdir "C:\Program Files\Unowhy\HiSqool Manager"
echo ===Done===
exit