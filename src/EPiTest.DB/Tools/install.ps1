param($installPath, $toolsPath, $package, $project)

function Copy-And-Include-Config-File {
    param([string]$sourcePath, [string]$destinationPath, [string]$sourceFileName, [string]$destinationFileName)
    
    if(test-path "$sourcePath\$sourceFileName") {
        Copy-Item "$sourcePath\$sourceFileName" "$destinationPath\$destinationFileName"
    
        $addedFile = $project.ProjectItems.AddFromFileCopy("$destinationPath\$destinationFileName")
        $addedFile.Properties.Item("CopyToOutputDirectory").Value = 1
    }
}

function Copy-EPiServer-Binaries {
    param([string]$epiSitePath, [string]$destinationPath)
    
    Copy-Item $epiSitePath\bin\*.dll $destinationPath\bin\debug
    Copy-Item $epiSitePath\bin\*.dll $destinationPath\bin\release
}

$projectDir = (Get-Item $project.FullName).Directory

$solutionDir = (Get-Item $project.Globals.DTE.Solution.FullName).Directory

Write-Host "Trying to find an EPiServer website in the solution." -ForegroundColor magenta

$epiProjectFolder = ""
$project.Globals.DTE.Solution.Projects | ForEach-Object {
    $dir = ([System.IO.FileInfo]$_.FullName).DirectoryName
    if(test-path "$dir\EPiServer.config") { $epiProjectFolder = $dir }
}

if($epiProjectFolder -ne "") {
    Write-Host "Trying to copy config files and includes them in project." -ForegroundColor magenta

    Copy-And-Include-Config-File $epiProjectFolder $projectDir "web.config" "app.config"
    Copy-And-Include-Config-File $epiProjectFolder $projectDir "connectionStrings.config" "connectionStrings.config"
    Copy-And-Include-Config-File $epiProjectFolder $projectDir "episerver.config" "episerver.config"
    Copy-And-Include-Config-File $epiProjectFolder $projectDir "episerverFramework.config" "episerverFramework.config"
    Copy-And-Include-Config-File $epiProjectFolder $projectDir "license.config" "license.config"

    Write-Host "Trying to copy EPiServer binaries." -ForegroundColor magenta
    
    Copy-EPiServer-Binaries $epiProjectFolder $projectDir
}
else {
    Write-Host "Could not find an EPiServer site web project."
}