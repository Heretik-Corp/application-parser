using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ApplicationParser.Tests
{
    public static class ParseTestHelper
    {
        public static ClassDeclarationSyntax GetClassDefinition(string text)
        {
            var tree = CSharpSyntaxTree.ParseText(text);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var firstMember = root.Members[0];
            var classDefinition = (ClassDeclarationSyntax)firstMember;
            return classDefinition;
        }
    }
}
