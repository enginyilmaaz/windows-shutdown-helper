@echo off
REM =============================================
REM  Windows Shutdown Helper - Build Script
REM  Wrapper for create-build.ps1
REM =============================================
REM
REM Usage:
REM   create-build.bat                    (Release build)
REM   create-build.bat -Configuration Debug
REM   create-build.bat -Clean
REM   create-build.bat -Clean -Configuration Debug
REM

pushd "%~dp0"
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0create-build.ps1" %*
set EXIT_CODE=%ERRORLEVEL%
popd
exit /b %EXIT_CODE%
