param($installPath, $toolsPath, $package, $project)
 
$epiProjectFolder = ""
$project.Globals.DTE.Solution.Projects | ForEach-Object {
    $dir = ([System.IO.FileInfo]$_.FullName).DirectoryName
    if(test-path "$dir\EPiServer.config") { $epiProjectFolder = $dir }
}
 
 if($epiProjectFolder -ne "") {
    Write-Host "Found an EPiServer project in the solution. Trying to set site url and UI-url." -ForegroundColor magenta
    
    $epiconfig = [xml](get-content "$epiProjectFolder\episerver.config")    
    
    $uiConfigPath = ([System.IO.FileInfo]$project.FullName).DirectoryName
    
    $uiTestConfig = [xml](get-content "$uiConfigPath\app.config")    
    
    $uiTestConfig.configuration.appSettings.add[0].value = $webConfigFile.episerver.sites.site.siteSettings.siteUrl
    
    $uiUrl = $webConfigFile.episerver.sites.site.siteSettings.uiUrl
    
    if($uiUrl.StartsWith("~/")) {
        $uiUrl = $uiUrl.Substring(2)
    }
    
    $uiTestConfig.configuration.appSettings.add[1].value = $uiUrl
    
    $uiTestConfig.Save("$uiConfigPath\app.config")
}
else {
    write-host "Welcome to EPiTest.UI Remember to update the variables in app.config to reflect the site you're testing."  -foreground "magenta"
}