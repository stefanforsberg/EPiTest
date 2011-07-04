include default.ps1

properties {
  # Build mode of the integration test project. If you're running the tests as release you need to change the value to release
  $buildMode = 'debug'
  
  # The name of the database to create snapshots from.
  $databaseName = 'dbEPiIntegrationTests'
  
  # Point this to where you want to store the snapshot. The path below is the standard location of Sql Server 2008 databases.
  $databaseSnapshotPath = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA'
  
  # The relative path from the folder this file is located in to where the web project is located.
  $dbWebProjectRelativePath = '..\..\EPiIntegrationTests.Web'
}

task CreateTestReport {
	$rootPath = Resolve-Path '..\app.config'
	$reportPath = ([xml](cat (Resolve-Path '..\app.config') )).configuration.appSettings.add | where {$_.key -eq "ReportPath"} 

	if(Test-Path $reportPath.Value) {
		Remove-Item -Recurse $reportPath.Value
	}
	
	New-Item $reportPath.Value -type directory

	..\..\packages\Machine.Specifications-Signed.0.4.13.0\tools\mspec-clr4.exe ..\bin\Debug\AlloytTech.Tests.Integration.dll --html $reportPath.Value
}
