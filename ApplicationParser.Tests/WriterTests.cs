using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Xunit;

namespace ApplicationParser.Tests
{
    public class WriterTests
    {
        #region WriteClasses
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
        public void WriteClasses_PassInRDOObjectWithTextField_ReturnsInheritRDOWrapper()
        {
            //ARRANGE
            var text = Writer.WriteClassMetaData(new Application
            {
                Objects = new List<ObjectDef>
                {
                    new ObjectDef
                    {
                        Name ="hello",
                        Fields = new List<Field>()
                        {
                            new Field
                            {
                                Name = "test",
                                FieldType = FieldTypes.FixedLength,
                                MaxLength = 30
                            }
                        }
                    }
                }
            });
            //ACT
            var classDefinition = ParseTestHelper.GetClassDefinition(text);

            //ASSERT
            var fieldDef = classDefinition.Members.First().ToString().Trim();
            Assert.Equal("public const int MAX_LENTH = 30;", fieldDef);
        }

        [Fact]
        public void WriteClasses_PassInRDOObjectWithNoTextField_ReturnsInheritRDOWrapper()
        {
            //ARRANGE
            var text = Writer.WriteClassMetaData(new Application
            {
                Objects = new List<ObjectDef>
                {
                    new ObjectDef
                    {
                        Name ="hello",
                        Fields = new List<Field>()
                        {
                            new Field
                            {
                                Name = "test",
                                FieldType = FieldTypes.Date,
                                MaxLength = 30
                            }
                        }
                    }
                }
            });
            //ACT

            //ASSERT
            Assert.True(string.IsNullOrWhiteSpace(text));
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

        #endregion


    }
}
