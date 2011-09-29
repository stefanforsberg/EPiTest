param($installPath, $toolsPath, $package, $project)

function ShowDialogBox {
    param($title, $message)
    
    [Windows.Forms.MessageBox]::Show($message, $title, [Windows.Forms.MessageBoxButtons]::YesNo, [System.Windows.Forms.MessageBoxIcon]::Information)
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
 

$epiProjectData = GetEpiServerProjectData $project

$epiProject = $epiProjectData[0]
$epiProjectFolder = $epiProjectData[1]
 
 if($epiProjectFolder -ne "") {
    $shouldCopy = ShowDialogBox "Copy values from sites" "I've found an EPiServer project in the solution. Want me to copy the relevant URL-data from that site?"
    
    if($shouldCopy -eq "Yes") {
        
        $epiconfig = [xml](get-content "$epiProjectFolder\episerver.config")    
        
        $uiConfigPath = ([System.IO.FileInfo]$project.FullName).DirectoryName
        
        $uiTestConfig = [xml](get-content "$uiConfigPath\app.config")    
        
        $uiTestConfig.configuration.appSettings.add[0].value = $epiconfig.episerver.sites.site.siteSettings.siteUrl
        
        $uiUrl = $epiconfig.episerver.sites.site.siteSettings.uiUrl
        
        if($uiUrl.StartsWith("~/")) {
            $uiUrl = $uiUrl.Substring(2)
        }
        
        $uiTestConfig.configuration.appSettings.add[1].value = $uiUrl
        
        $uiTestConfig.Save("$uiConfigPath\app.config")
    }
}
else {
    write-host "Welcome to EPiTest.UI Remember to update the variables in app.config to reflect the site you're testing."  -foreground "magenta"
}

notepad "$toolsPath\Instructions.txt"