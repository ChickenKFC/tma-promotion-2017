@echo off
setlocal

rem DB設定
if not defined PGDATABASE       set PGDATABASE=search_operator
if not defined PGUSER_LIV       set PGUSER_LIV=search_operator
if not defined PGPASSWORD_LIV   set PGPASSWORD_LIV=NhatAnh#0110
if not defined PGUSER_ADMIN     set PGUSER_ADMIN=postgres
if not defined PGPASSWORD_ADMIN set PGPASSWORD_ADMIN=postgres
if not defined INIT_DIR         set INIT_DIR=%~dp0init

rem Reading command line option
if "%1" == "-f" (
    set FORCE=TRUE
    shift /1
)

rem Environment setting
call :SET_ENVIROMENT
if errorlevel 1 (
    if not defined FORCE pause > nul
    exit /b 1
)

rem Get initialize data
if "%1" == "" (
    call :LAUNCHER
) else (
    set DATA=%1
)

rem File existing checking
call :CHECK_FILES
if errorlevel 1 (
    if not defined FORCE pause > nul
    exit /b 1
)

rem Session disconnect
call :TERMINATE_SESSION

rem Create log.
set BATDIR=%~dp0
set LOG=%BATDIR%log.txt
echo.> %LOG%

rem データベースのクリア
call :CLEAR_DATABASE

rem Initialize
set PGUSER=%PGUSER_LIV%
set PGPASSWORD=%PGPASSWORD_LIV%
for /f "usebackq tokens=* eol=#" %%i in ("%INIT_DIR%\%DATA%") do (
    echo %%i
    echo %%i >> %LOG%
    %BATDIR%tools\ByteOrderMarkRemover\ByteOrderMarkRemover.exe < "%INIT_DIR%\%%i" | psql >> %LOG% 2>&1
)

rem Confirm log
for /f "tokens=3 usebackq" %%i in (`find /C "ERROR:" %LOG%`) do set SUM_ERROR=%%i
for /f "tokens=3 usebackq" %%i in (`find /C "FATAL:" %LOG%`) do set SUM_FATAL=%%i
set /a result=SUM_ERROR + SUM_FATAL - SUM_IGNORE
if "%RESULT%" == "0" (
    echo.
    echo Successful completion.
    if not defined FORCE set /P DUMMY=
    exit /b 0
) else (
    echo.
    echo ---------- %LOG%
    findstr "ERROR: FATAL:" %LOG%
    echo.
    echo Error occured.
    echo Please confirm %LOG% for more detail.
    if not defined FORCE set /P DUMMY=
    
    exit /b 2
)


rem ----------------------------------------------------------------------------
:SET_ENVIROMENT
set POSTGRE_SQL=C:\Program Files\PostgreSQL\9.6\bin
if not exist "%POSTGRE_SQL%" (
    rem Environment where PostgreSQL 9.6 is installed
    set POSTGRE_SQL=C:\Program Files\PostgreSQL\9.6\bin
)
if not exist "%POSTGRE_SQL%" (
    rem Environment where pgAdmin is installed alone
    set POSTGRE_SQL=C:\Program Files\pgAdmin 4
)
if not exist "%POSTGRE_SQL%" (
    echo Error: Can not find the installation location of psql.
    exit /b 1
)
PATH=%PATH%;%POSTGRE_SQL%
exit /b 0


rem ----------------------------------------------------------------------------
:LAUNCHER
echo Warning: installed database will be deleted.
echo.
echo ----- Modes -----
set CHARS=ABCDEFGHIJKLMNOPQRSTUVWXYZ
set COUNT=0
set OPTIONS=.
for %%i in (%INIT_DIR%\*.dat) do (
    call call echo [%%%%CHARS:~%%COUNT%%,1%%%%] %%%\%~ni
    call set OPTIONS=%%OPTIONS%%;%%%\%~nxi
    set /a COUNT+=1
)
echo.

echo which mode do you want to initialize?
call choice /C:Z%%CHARS:~0,%COUNT%^%% /N /M "> "
set SELECT=%ERRORLEVEL%
for /f "delims=; tokens=%SELECT%" %%i in ("%OPTIONS%") do set DATA=%%i
cls

exit /b 0


rem ----------------------------------------------------------------------------
:CHECK_FILES
rem Initialization data exists check
if not exist "%INIT_DIR%\%DATA%" (
    echo Error: Can not find [%DATA%].
    exit /b 1
)

rem SQL exist checking
set FAILED=0
for /f "usebackq tokens=* eol=#" %%i in ("%INIT_DIR%\%DATA%") do (
    if not exist %INIT_DIR%\%%i (
        echo Error： Can not find [%%i].
        set FAILED=1
    )
)

exit /b %FAILED%


rem ----------------------------------------------------------------------------
:TERMINATE_SESSION
set PGUSER=%PGUSER_ADMIN%
set PGPASSWORD=%PGPASSWORD_ADMIN%
set INPUT=
for /f "DELIMS=" %%A in ('psql -c "SELECT COUNT(*) FROM pg_stat_activity WHERE datname = '%PGDATABASE%'" --dbname=postgres -w -t -A') do set SESSION=%%A
if not %SESSION% == 0 (
    echo %PGDATABASE% へのコネクションが残っています。
    if not defined FORCE (
        echo LitIViewServer, Tomcat, PgAdmin などをすべて終了してください。
        echo.
        echo 「Enterキー」で続行します。
        echo （コネクションが残っていた場合、強制切断を試みます）
        set /P INPUT=
    )
    
    rem 切断と待機
    echo しばらくお待ちください...
    psql -c "SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '%PGDATABASE%'" --dbname=postgres -w > nul
    ping localhost -n 4 > nul
    cls
    goto TERMINATE_SESSION
)
exit /b 0

rem ----------------------------------------------------------------------------
:CLEAR_DATABASE
rem Delete and generate databases run as "administrator user"
set PGUSER=%PGUSER_ADMIN%
set PGPASSWORD=%PGPASSWORD_ADMIN%

echo DROP DATABASE
echo DROP DATABASE >> %LOG%
psql -c "DROP DATABASE IF EXISTS %PGDATABASE%;" --dbname=postgres >> %LOG% 2>&1

echo CREATE DATABASE
echo CREATE DATABASE >> %LOG%
psql -c "CREATE DATABASE %PGDATABASE% WITH ENCODING='UTF8' OWNER=%PGUSER_LIV% TEMPLATE=template0 LC_COLLATE='C' LC_CTYPE='C' CONNECTION LIMIT=-1;" --dbname=postgres -U %PGUSER_ADMIN% >> %LOG% 2>&1
