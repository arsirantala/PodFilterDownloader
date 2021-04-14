@echo off

if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat" (
    call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat"
) else (
    echo "Microsoft Visual Studio 2019 Community was not found"
    goto end
)

START /WAIT /B cmd /c msbuild ..\IxothPodFilterDownloader.sln /t:Build /p:Configuration=Release

if exist "%ProgramFiles%\InstallMate 9\BinX64\Tin.exe" (
    "%ProgramFiles%\InstallMate 9\BinX64\Tin.exe" "%cd%\Ixoth Pod Filter Downloader.im9" /build
) else (
    echo "Tin.exe could not be found"
    goto end
)

Tools\fnr.exe --cl --dir "%cd%" --fileMask "IxothPodFilterDownloader_Setup.txt" --find "InstallerPath=IxothPodFilterDownloader_Setup.exe" --replace "InstallerPath=https://raw.githubusercontent.com/arsirantala/IxothPoDFilterDownloader/installer/master/IxothPodFilterDownloader_Setup.exe"

:end
