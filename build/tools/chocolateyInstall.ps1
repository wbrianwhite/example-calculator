$toolsDir = "$(Split-Path -Path $MyInvocation.MyCommand.Definition)"
$destDir="C:\Utilities\FractionCalculator"

#Remove existing installed app if still present
if ([System.IO.Directory]::Exists($destDir))
{
	"Removing directory $destDir" | Write-Output
	Remove-Item -Path $destDir -Recurse -Force
}

"Creating directory $destDir" | Write-Output
New-Item -Path $destDir -ItemType Directory -Verbose

#TODO: check which dotnet is installed, and install correct version !!
Copy-Item $toolsDir\bin\* -Recurse -Exclude chocolatey* -Destination $destDir

"Successfully Installed $packageName utility, try FractionCalculator.exe -h for more info" | Write-Output
