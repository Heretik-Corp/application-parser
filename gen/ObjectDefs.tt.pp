<#@ template debug="false" hostspecific="true" language="C#" #>
<#@ assembly name="System.Core" #>
<#@ import namespace="System.Linq" #>
<#@ import namespace="System.Text" #>
<#@ import namespace="System.Collections.Generic" #>
<#@ import namespace="ApplicationParser" #>
<#@ assembly name="$(SolutionDir)\packages\Heretik.ApplicationParser.{{nuget-version}}\lib\net45\ApplicationParser.dll" #>
<#@ assembly name="$(SolutionDir)\packages\Heretik.ApplicationParser.{{nuget-version}}\lib\net45\ApplicationParser.Generator.T4.dll" #>
<# 
	string solutionsPath = Host.ResolveAssemblyReference("$(SolutionDir)");
	string filePath = solutionsPath + "application/application.xml"; 
#>
<# string fileContent = System.IO.File.ReadAllText(filePath); #>
<# var app = new ApplicationParser.Parser().Parse(fileContent); #>
<#@ output extension=".cs" #>
using System;
using kCura.Relativity.Client.DTOs;
using $rootnamespace$;

namespace $rootnamespace$
{

<#= app.WriteClasses() #>

}
