# Fraction Calculator
--------------------

Sample project to solve during interview process.  The app uses the [Fractions package](https://github.com/danm-de/Fractions/blob/master/license.txt) to handle fractions.

The app parses the string[] args into a MathNode[] where a MathNode is either a Fraction or Operator type.  Then runs across that doing the divide operators, multiply, add, subtract.  

Basic validations:
* No trailing numbers or operators ("3 * 1_1/2 7") ("3 * 1_1/2 +") 
* No dividing by zero ("3 / 0")
* No incorrect ordering of numbers and operators ("3 * * 1_1/2")

## Source
----------

* FractionCalculator - main code, main classes are Parser and MathRunner and Program
* FractionCalculatorTests - xUnit tests

## Build
---------

To build this app, run `Install-Module psake` in a powershell console, then close/open and run `Invoke-psake psakeFile.ps1` to execute the Default task which runs Restore, Clean, Build, Pack. That will work if all you have is MSBuildTools installed.  Or you can always open it in Visual Studio 2017 and build there.  

The final pack step creates a chocolatey package for this app you'd install with `choco install FractionCalculator -y`
