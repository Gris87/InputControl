@echo off
set APP_NAME=InputControl

set UNITY="C:\Program Files\Unity\Editor\Unity.exe"

if not exist %UNITY% (
    set UNITY="C:\Program Files (x86)\Unity\Editor\Unity.exe"
)

if exist out (
    rmdir /S /Q out
)



echo ---------------------------------------------------------------
echo Path to unity = %UNITY%
echo Prepare binaries for %APP_NAME%
echo ---------------------------------------------------------------
echo.

mkdir out

echo Windows x86
%UNITY% -batchmode -projectPath %CD% -buildWindowsPlayer out\Windows32\%APP_NAME%.exe -quit

echo Windows x64
%UNITY% -batchmode -projectPath %CD% -buildWindows64Player out\Windows64\%APP_NAME%.exe -quit

echo Linux x86
%UNITY% -batchmode -projectPath %CD% -buildLinux32Player out\Linux32\%APP_NAME%.run -quit

echo Linux x64
%UNITY% -batchmode -projectPath %CD% -buildLinux64Player out\Linux64\%APP_NAME%.run -quit

echo Mac OSX x86
mkdir out\MacOSX32
%UNITY% -batchmode -projectPath %CD% -buildOSXPlayer out\MacOSX32\%APP_NAME%.app -quit

echo Mac OSX x64
mkdir out\MacOSX64
%UNITY% -batchmode -projectPath %CD% -buildOSX64Player out\MacOSX64\%APP_NAME%.app -quit

echo Web
%UNITY% -batchmode -projectPath %CD% -buildWebPlayer out\Web\%APP_NAME% -quit

if [%1]==[] (
pause
)