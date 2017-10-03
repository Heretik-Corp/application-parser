using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        public static IEnumerable<PropertyDeclarationSyntax> GetProperties(string text)
        {
            return ((CompilationUnitSyntax)ParseTestHelper.GetClassDefinition(text).Parent)
                .Members
                .Where(x => x is PropertyDeclarationSyntax)
                .Cast<PropertyDeclarationSyntax>();
        }
    }
}
