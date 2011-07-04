$allCsFiles = Get-ChildItem -recurse  | where {$_.extension -eq ".cs"}

$allCsFiles | ForEach-Object {
    Get-Content $_.FullName | Foreach-Object {
        $_ -replace "AlloytTech.Tests.Integration", ("$" + "rootnamespace" + "$")
    } | Set-Content ($_.FullName + ".pp")
    
    Remove-Item $_.FullName
}