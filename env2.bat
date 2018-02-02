@echo OFF
title gacutil regasm prompt

goto check_Permissions

:check_Permissions
    net session >nul 2>&1
    if %errorLevel% == 0 (
        REM echo Success: Administrative permissions confirmed.
        goto go
    ) else (
        echo Please run as Administrator.
    )

    pause >nul

:go


.\gacutil.exe /if SampleBars.dll
.\gacutil.exe /if BandObjectLib.dll
.\gacutil.exe /if GoogleApi.dll
.\gacutil.exe /if Newtonsoft.Json.dll
.\gacutil.exe /if BouncyCastle.Crypto.dll

"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe" SampleBars.dll

C:\Windows\System32\taskkill.exe /im explorer.exe /f



