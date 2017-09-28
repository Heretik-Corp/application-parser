using Heretik.ApplicationParser.Writers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationParser.Tests.Writers
{
    public class ScriptWriterTests
    {
        [Fact]
        public void WriteScripts_ScriptGuidCorrect()
        {
            //ARRANGE
            var app = new Application();
            app.Scripts = new List<Script> {
                new Script
                {
                    Name = "testScript",
                    Guid ="ac4a4f4c-b9af-4c87-8fb4-35ca0f4a9b63"
                }
            };

            //ACT
            var writer = new ScriptWriter();
            var text = writer.WriteScripts(app);

            //ASSERT
            var classDefinition = ParseTestHelper.GetClassDefinition(text);
            var properties = (FieldDeclarationSyntax)classDefinition.Members[0];
            Assert.Equal("public const string testScript = \"ac4a4f4c-b9af-4c87-8fb4-35ca0f4a9b63\";", properties.ToString(), ignoreWhiteSpaceDifferences: true);
        }
    }
}
