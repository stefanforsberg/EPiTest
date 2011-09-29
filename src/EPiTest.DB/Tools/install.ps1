param($installPath, $toolsPath, $package, $project)

[void] [Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms")

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

function AddReferenceFromEPiServerProject {
    param($epiServerproject, $epiProjectFolder, $assemblyName)
    
    $pathToAssembly = (Get-ChildItem $epiProjectFolder -include "$assemblyName.dll" -Recurse).FullName
    
    if($pathToAssembly -ne "") {
        $project.Object.References.Add($pathToAssembly)
    }
}

function GetEpiServerProjectData {
    $epiProject = ""
    $epiProjectFolder = ""
    $project.Globals.DTE.Solution.Projects | ForEach-Object {
        if($_.FullName -ne "")
        {
            Write-Host $_.FullName
        
            $dir = ([System.IO.FileInfo]$_.FullName).DirectoryName
            if(test-path "$dir\EPiServer.config") { 
                $epiProjectFolder = $dir 
                $epiProject = $_
            }
        }
    }

    Write-Output $epiProject
    Write-Output $epiProjectFolder
}

function ShowDialogBox {
    param($title, $message)
    
    [Windows.Forms.MessageBox]::Show($message, $title, [Windows.Forms.MessageBoxButtons]::YesNo, [System.Windows.Forms.MessageBoxIcon]::Information)
}

$projectDir = (Get-Item $project.FullName).Directory

$solutionDir = (Get-Item $project.Globals.DTE.Solution.FullName).Directory

Write-Host "Trying to find an EPiServer website in the solution." -ForegroundColor magenta

$epiProjectData = GetEpiServerProjectData $project

$epiProject = $epiProjectData[0]
$epiProjectFolder = $epiProjectData[1]

notepad "$toolsPath\Instructions.txt"


if($epiProjectFolder -ne "") {
    Write-Host "Trying to copy config files and includes them in project." -ForegroundColor magenta

    Copy-And-Include-Config-File $epiProjectFolder $projectDir "web.config" "app.config"
    Copy-And-Include-Config-File $epiProjectFolder $projectDir "connectionStrings.config" "connectionStrings.config"
    Copy-And-Include-Config-File $epiProjectFolder $projectDir "episerver.config" "episerver.config"
    Copy-And-Include-Config-File $epiProjectFolder $projectDir "episerverFramework.config" "episerverFramework.config"
    Copy-And-Include-Config-File $epiProjectFolder $projectDir "license.config" "license.config"

    Write-Host "Trying to copy EPiServer binaries." -ForegroundColor magenta
    
    Copy-EPiServer-Binaries $epiProjectFolder $projectDir
    
    $tryAndAddReferences = [Windows.Forms.MessageBox]::Show("Want to add the neccesary references from the EPiServer project? This will add the references from GAC. If you answer no you can add them yourself manually.", "Try to add references", [Windows.Forms.MessageBoxButtons]::YesNo, [System.Windows.Forms.MessageBoxIcon]::Information)
    
    if($tryAndAddReferences -eq "Yes")
    {
        Write-Host "Trying to add references to EPiServer assemblies"  -ForegroundColor magenta
        AddReferenceFromEPiServerProject $epiProject $epiProjectFolder "EPiServer"
        AddReferenceFromEPiServerProject $epiProject $epiProjectFolder "EPiServer.BaseLibrary"
        AddReferenceFromEPiServerProject $epiProject $epiProjectFolder "EPiServer.Configuration"
        AddReferenceFromEPiServerProject $epiProject $epiProjectFolder "EPiServer.Framework"
        AddReferenceFromEPiServerProject $epiProject $epiProjectFolder "EPiServer.Implementation"   
    }
    
}
else {
    Write-Host "Could not find an EPiServer site web project."
}

