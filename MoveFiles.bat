@echo off
:: Force script to run in the current folder
cd /d "%~dp0"

echo ==========================================
echo      FIXING PROJECT STRUCTURE...
echo ==========================================
echo Working Directory: %CD%
echo.

:: 1. Check if we are in the right place
if not exist "RentalSystemUI.csproj" (
    echo [ERROR] WRONG FOLDER! 
    echo Please move this script into the folder containing 'RentalSystemUI.csproj'
    pause
    exit
)

:: 2. Create Folders
echo [1/3] Creating Folders...
if not exist "Forms" mkdir "Forms"
if not exist "Assets" mkdir "Assets"
if not exist "Classes" mkdir "Classes"
if not exist "Controllers" mkdir "Controllers"

:: 3. Move Files (Forcefully)
echo [2/3] Moving Form Files...

if exist "Form1.cs" (
    move /Y "Form1.cs" "Forms\"
    echo Moved Form1.cs
) else (
    echo NOTICE: Form1.cs not found in root (Maybe already moved?)
)

if exist "Form1.Designer.cs" (
    move /Y "Form1.Designer.cs" "Forms\"
    echo Moved Form1.Designer.cs
)

if exist "Form1.resx" (
    move /Y "Form1.resx" "Forms\"
    echo Moved Form1.resx
)

:: 4. Clean up typos
if exist "Froms" (
    echo Removing typo folder 'Froms'...
    rd /s /q "Froms"
)

echo.
echo ==========================================
echo      DONE! FILES MOVED.
echo ==========================================
echo.
echo NEXT STEPS (IMPORTANT):
echo 1. Open Visual Studio.
echo 2. Open 'Forms/Form1.cs' and change:
echo    "namespace RentalSystemUI" -> "namespace RentalSystemUI.Forms"
echo 3. Open 'Program.cs' and add at the top:
echo    "using RentalSystemUI.Forms;"
echo.
pause