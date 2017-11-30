param(
    [Parameter(Mandatory = $true)]
    [version]
    $Version
)
Push-Location $PSScriptRoot

Remove-Item ".\content\" -Force -Recurse -ErrorAction SilentlyContinue | out-null
Remove-Item .\lib -Force -Recurse -ErrorAction SilentlyContinue | Out-Null


"Generating Core Package"
#copy Dll
"Copying ..\ApplicationParser\bin\Release\ApplicationParser.dll => .\lib\net45"

New-Item -ItemType File -Path .\lib\net45\ApplicationParser.dll -Force | out-null
Copy-Item ..\ApplicationParser\bin\Release\ApplicationParser.dll .\lib\net45


nuget pack .\ApplicationParser.Core.nuspec -Version $Version

"Generating T4 Package"

Remove-Item .\lib -Force -Recurse | Out-Null
New-Item -ItemType File -Path .\lib\net45\ApplicationParser.Generator.T4.dll -Force | out-null
Copy-Item ..\ApplicationParser.Generator.T4\bin\Release\ApplicationParser.Generator.T4.dll .\lib\net45

Get-ChildItem ..\gen\*.pp | % {
    $destFile = ".\content\CodeGen\$($_.Name)";

    Write-Output "Replacing tokens $($_.Name) => $($destFile)"
	New-Item -ItemType File -Path $destFile -Force | out-null
    (Get-Content $_.FullName).Replace("{{nuget-version}}", $Version) | Set-Content $destFile | out-null
}

(Get-Content .\ApplicationParser.nuspec.gen).Replace("{{version}}", $version) | Set-Content .\ApplicationParser.nuspec | out-null

nuget pack .\ApplicationParser.nuspec -Version $Version

Remove-Item .\ApplicationParser.nuspec
Remove-Item ".\content\" -Force -Recurse -ErrorAction SilentlyContinue | out-null
Remove-Item .\lib -Force -Recurse -ErrorAction SilentlyContinue | Out-Null

Pop-Location