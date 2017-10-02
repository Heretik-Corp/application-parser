param(
    [Parameter(Mandatory = $true)]
    [version]
    $Version
)
Push-Location $PSScriptRoot

#copy Dll
"Copying ..\ApplicationParser\bin\Release\ApplicationParser.dll => .\lib\net45"

New-Item -ItemType File -Path .\lib\net45\ApplicationParser.dll -Force | out-null
Copy-Item ..\ApplicationParser\bin\Release\ApplicationParser.dll .\lib\net45

Remove-Item ".\content\CodeGen\*" -ErrorAction SilentlyContinue | out-null
Get-ChildItem ..\gen\*.pp | % {
    $destFile = ".\content\CodeGen\$($_.Name)";

    Write-Output "Replacing tokens $($_.Name) => $($destFile)"
	New-Item -ItemType File -Path $destFile -Force | out-null
    (Get-Content $_.FullName).Replace("{{nuget-version}}", $Version) | Set-Content $destFile | out-null
}

nuget pack -Version $Version
Pop-Location