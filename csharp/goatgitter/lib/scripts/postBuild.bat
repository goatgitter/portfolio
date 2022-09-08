@echo off
echo ---------------------------------
echo           START POST-BUILD
echo ---------------------------------
echo Working Directory: %CD%
echo Script Dir: %~dp0
echo ---------------------------------
cd %~dp0\..
echo Deleting obj directory ....
RD /S /Q obj
echo Deleting contents of deploy directory ....
RD /S /Q debug
echo ---------------------------------
echo           END POST-BUILD
echo ---------------------------------