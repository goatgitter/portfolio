@echo off
echo ---------------------------------
echo           START PRE-BUILD
echo ---------------------------------
echo Working Directory: %CD%
echo Script Dir: %~dp0
echo ---------------------------------
cd %~dp0\..
echo Contents of deploy directory are ....
dir deploy\*
echo Deleting contents of deploy directory ....
del /F /Q deploy
echo Contents of deploy directory are ....
dir deploy\*
echo ---------------------------------
echo           END PRE-BUILD
echo ---------------------------------
