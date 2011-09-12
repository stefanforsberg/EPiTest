$framework = '4.0'

properties {
  $base_dir = Resolve-Path .
  $sln_file = "$base_dir\src\EPiTest.sln"
  
  $mspec_dir = "$base_dir\src\packages\Machine.Specifications.0.4.24.0\tools\"
  
  $nuget_dir = "$base_dir\tools"
  
  # Build mode of the integration test project. If you're running the tests as release you need to change the value to release
  $buildMode = 'debug'
  
  # The name of the database to create snapshots from.
  $databaseName = 'dbEPiIntegrationTests'
  
  # Point this to where you want to store the snapshot. The path below is the standard location of Sql Server 2008 databases.
  $databaseSnapshotPath = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA'
  
  # The relative path from the folder this file is located in to where the web project is located.
  $dbWebProjectRelativePath = '..\..\EPiIntegrationTests.Web'
  
  $env:Path += ";$mspec_dir;$nuget_dir"
}

task default -depends Compile, Specs, Nuget

task Compile {
    msbuild "$sln_file"
}

task Specs {
    exec { mspec-clr4.exe "$base_dir\src\EPiTest.WebDriverExtension.Specs\bin\$buildMode\EPiTest.WebDriverExtension.Specs.dll" }
}

task Nuget {
    cd "$base_dir\src\EPiTest.UI"
    exec { nuget.exe pack EPiTest.UI.csproj -OutputDirectory ..\..\build }
    cd "$base_dir"
}

task CopyConfigFiles {
    $webRootPath = Resolve-Path $dbWebProjectRelativePath
    $destinationPath = Resolve-Path '..\'
    
    Copy-Item $webRootPath\connectionStrings.config $destinationPath
    Copy-Item $webRootPath\episerver.config $destinationPath
    Copy-Item $webRootPath\episerverFramework.config $destinationPath
    Copy-Item $webRootPath\license.config $destinationPath
    Copy-Item $webRootPath\web.config $destinationPath\app.config
}

task CopyEpiServerBinaries {
    $webRootPath = Resolve-Path $dbWebProjectRelativePath
    $destinationPath = Resolve-Path ('..\bin\' + $buildMode)
    
    Copy-Item $webRootPath\bin\*.dll $destinationPath
}

task CreateSnapshot {
  $query = "IF EXISTS (SELECT * FROM sys.databases WHERE NAME='IntegrationSnapshot') DROP DATABASE IntegrationSnapshot;
            CREATE DATABASE IntegrationSnapshot ON
            ( NAME = $databaseName, FILENAME = 
            '$databaseSnapshotPath\dbEPiIntegrationTests.ss')
            AS SNAPSHOT OF $databaseName"

  $rootPath = Resolve-Path '..\connectionStrings.config'

  $connectionString = ([xml](cat $rootPath)).connectionStrings.add.connectionString

  $SqlConnection = New-Object System.Data.SqlClient.SqlConnection
  $SqlConnection.ConnectionString = $connectionString

  $SqlCmd             = New-Object System.Data.SqlClient.SqlCommand
  $SqlCmd.CommandText = $query
  $SqlCmd.Connection  = $SqlConnection

  $SqlConnection.Open()
  $SqlCmd.ExecuteNonQuery()
  $SqlConnection.Close()  

  $SqlCmd.Dispose()
}

task ? -Description "Helper to display task info" {
	Write-Documentation
}