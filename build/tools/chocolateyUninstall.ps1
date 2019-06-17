$destDir="C:\Utilities\FractionCalculator"

#Remove existing installed app if still present
if ([System.IO.Directory]::Exists($destDir))
{
	"Removing directory $destDir" | Write-Output
	Remove-Item -Path $destDir -Recurse -Force
}

"Successfully uninnstalled FractionCalculator utility" | Write-Output
