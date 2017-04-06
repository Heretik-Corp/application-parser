<#@ template debug="false" hostspecific="true" language="C#" #>
<#@ assembly name="System.Core" #>
<#@ import namespace="System.Linq" #>
<#@ import namespace="System.Text" #>
<#@ import namespace="System.Collections.Generic" #>
<#@ import namespace="ApplicationParser" #>
<#@ assembly name="$(SolutionDir)\packages\Heretik.ApplicationParser.1.0.5\lib\net45\ApplicationParser.dll" #>
<# 
	string solutionsPath = Host.ResolveAssemblyReference("$(SolutionDir)");
	string filePath = solutionsPath + "application/application.xml"; 
#>
<# string fileContent = System.IO.File.ReadAllText(filePath); #>
<# var app = new ApplicationParser.Parser().Parse(fileContent); #>
<#@ output extension=".cs" #>
namespace $rootnamespace$
{

	public static class Application 
	{
		public const string Name = "<#= app.Name #>";
		public const string Guid = "<#= app.Guid #>";
	}
<#= app.WriteObjectTypeGuids() #>
<#= app.WriteObjectGuids() #>
<#= app.WriteChoiceGuids() #>
<#= app.WriteTabGuids() #>

}
