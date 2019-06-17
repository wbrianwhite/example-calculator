Properties {
    $source       = Join-Path -Path $psake.build_script_dir -ChildPath 'source'
    $artifacts    = Join-Path -Path $source -ChildPath 'artifacts'
    $packages     = Join-Path -Path $source -ChildPath 'packages'
    $solution     = 'FractionCalculator.sln'
    $project      = 'FractionCalculator'
    $testproject  = 'FractionCalculatorTests'
    $projectpath  = Join-Path -Path $source -ChildPath $project
    $testpath     = Join-Path -Path $source -ChildPath $testproject
    $buildPath    = Join-Path -Path $psake.build_script_dir -ChildPath 'build'
}

# What is this psakefile?  PSake is a PowerShell Make system I like to use.  When used in combination with Jenkins, all the logic is in the psake file.  Developers run the
# psakefile locally so there are no surprises when it's Jenkins build time.  JenkinsFiles end up skinny wrappers like below, where the only real groovy functions are for uploading 
# artifacts to artifactory using a service account with access to do so. 
# JenkinsFile example:
# stage('Build') {
#    steps {
#        powershell "psake psakefile.ps1 -taskList 'Build'"
#    }
#}
# I like to use the dotnet cli in psake, it goes well with build automation.  

Task Default -depends Restore, Clean, Build, Pack
Task Full -depends Restore, Clean, Build, Test, Pack
Task Release -depends Restore, Clean, Build, Pack

Task Restore {
    "Pushing " + (Join-Path -Path $source -ChildPath $project) | Write-Output
    Push-Location -Path  (Join-Path -Path $source -ChildPath $project)    
    Get-Location | Write-Output
    dotnet restore
    "Pushing " + (Join-Path -Path $source -ChildPath $testproject) | Write-Output
    Push-Location -Path  (Join-Path -Path $source -ChildPath $testproject)  
    Get-Location | Write-Output
    Pop-Location
    Pop-Location
    Get-Location| Write-Output
    if($LASTEXITCODE -ne 0) { throw "Restore failed" }
}

Task Build {
    Push-Location -Path  $source
    dotnet build $solution --configuration Release
    dotnet build $solution --configuration Debug
    Pop-Location
    if($LASTEXITCODE -ne 0) { throw "Build failed" }
}

Task Test {      
    #TODO: Add code around supported versions to run tests on different dlls.
    $tmp = (Join-Path -Path $testpath -ChildPath "bin\Release")
    $tmp | Write-Output
    Push-Location -Path (Join-Path -Path $testpath -ChildPath "bin\Release")
    Get-Location | Write-Output
    $dlls = Get-ChildItem "$testproject.dll" -Recurse
    $dlls | Write-Output
    foreach ($dll in $dlls) {
        $dll | Write-Output
        $dll.FullName | Write-Output
        dotnet vstest $dll.FullName
        if ($LASTEXITCODE -ne 0) { throw "Tests failed with exit code $LASTEXITCODE" }
    }
    Pop-Location
    if ($LASTEXITCODE -ne 0) { throw "Tests failed with exit code $LASTEXITCODE" }
    
}

Task CleanArtifacts {
    If (Test-Path -Path $artifacts) {
		Remove-Item -Path $artifacts -Recurse -Force -ErrorAction Stop
	}
    New-Item -Path $artifacts -ItemType Directory -ErrorAction Stop
}

Task Clean -depends CleanArtifacts {
    Push-Location -Path  $source
    dotnet clean $solution --configuration Release --verbosity quiet
    dotnet clean $solution --configuration Debug --verbosity quiet
    Pop-Location
    if($LASTEXITCODE -ne 0) { throw "Clean failed" }
}

Task Pack -depends CleanArtifacts {    
    $output = Resolve-Path -Path $artifacts
    choco pack $buildPath\FractionCalculator.nuspec --outputdirectory $output
    if($LASTEXITCODE -ne 0) { throw "Pack failed" }
}