param(
    [Parameter(Mandatory = $true)]
    [version]
    $Version
)
cd $PSScriptRoot

#copy Dll
Copy-Item ..\ApplicationParser\bin\Release\ApplicationParser.dll .\lib\net45

Remove-Item ".\content\CodeGen\*"
Get-ChildItem ..\gen\*.pp | % {
    $destFile = ".\content\CodeGen\$($_.Name)";

    Write-Output "Replacing tokens $($_.Name) => $($destFile)"
    (Get-Content $_.FullName).Replace("{{nuget-version}}", $Version) | Set-Content $destFile
}

nuget pack -Version $Version