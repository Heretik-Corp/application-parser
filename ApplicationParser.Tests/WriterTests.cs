using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Xunit;

namespace ApplicationParser.Tests
{
    public class WriterTests
    {
        [Fact]
        public void WriteClasses_PassInRDOObject_ReturnsCorrectClassName()
        {
            //ARRANGE
            var text = Writer.WriteClasses(new Application
            {
                Objects = new List<ObjectDef>
                {
                    new ObjectDef
                    {
                        Name ="hello",
                        Fields = new List<Field>()
                    }
                }
            });
            //ACT
            var classDefinition = ParseTestHelper.GetClassDefinition(text);

            //ASSERT
            Assert.Equal("hello", classDefinition.Identifier.Text);
        }

        [Fact]
        public void WriteClasses_PassInRDOObject_ReturnsInheritRDOWrapper()
        {
            //ARRANGE
            var text = Writer.WriteClasses(new Application
            {
                Objects = new List<ObjectDef>
                {
                    new ObjectDef
                    {
                        Name ="hello",
                        Fields = new List<Field>()
                    }
                }
            });
            //ACT
            var classDefinition = ParseTestHelper.GetClassDefinition(text);

            //ASSERT
            var classDef = ((IdentifierNameSyntax)classDefinition.BaseList.Types[0].Type);
            Assert.Equal("RDOWrapper", classDef.Identifier.Text);

        }

        [Fact]
        public void WriteClasses_PassInDocumentObject_ReturnsInhertikFromDocumentWrapper()
        {
            //ARRANGE
            var text = Writer.WriteClasses(new Application
            {
                Objects = new List<ObjectDef>
                {
                    new ObjectDef
                    {
                        Name ="Document",
                        Fields = new List<Field>()
                    }
                }
            });
            //ACT
            var classDefinition = ParseTestHelper.GetClassDefinition(text);

            //ASSERT
            var classDef = ((IdentifierNameSyntax)classDefinition.BaseList.Types[0].Type);
            Assert.Equal("DocumentWrapper", classDef.Identifier.Text);
        }

        [Theory]
        [InlineData("field")]
        [InlineData("Field")]
        public void WriteClasses_PassInBlackListObject_SkipsBlackListObject(string objectName)
        {
            //ARRANGE

            //ACT
            var text = Writer.WriteClasses(new Application
            {
                Objects = new List<ObjectDef>
                {
                    new ObjectDef
                    {
                        Name =objectName,
                        Fields = new List<Field>()
                    }
                }
            });
            //ASSERT
            Assert.Empty(text);
        }
    }
}
