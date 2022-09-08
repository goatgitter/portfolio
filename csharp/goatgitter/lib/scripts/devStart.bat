@echo off
echo ---------------------------------
echo           DEV Shell
echo ---------------------------------
echo Working Directory: %CD%
echo Script Dir: %~dp0
cd %~dp0\..
echo ---------------------------------
echo        OPEN PROCESSES
echo ---------------------------------
powershell OpenFiles /local on
powershell Set-ExecutionPolicy RemoteSigned
echo ---------------------------------
dir
echo ---------------------------------
