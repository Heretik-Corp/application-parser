﻿using Heretik.ApplicationParser.Writers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationParser.Tests.Writers
{
    public class ClassWriterTests
    {
        [Fact]
        public void GetProperties_PassInArtifactID_skips()
        {
            //ARRANGE
            var def = new ObjectDef();
            def.Fields = new List<Field>()
            {
                new Field
                {
                    Name = "ArtifactID"
                }
            };

            //ACT
            var writer = new ClassWriter();
            var text = writer.WriteClasses(new Application
            {
                Objects = new List<ObjectDef> { def }
            });

            //ASSERT
            var members = ParseTestHelper.GetProperties(text).Select(x => x.Identifier.Text);

            Assert.DoesNotContain(members, x=>x == "ArtifactID");
        }
        [Theory]
        [InlineData("SystemCreatedBy", FieldTypes.User, "User")]
        [InlineData("SystemCreatedOn", FieldTypes.Date, nameof(DateTime) + "?")]
        [InlineData("SystemLastModifiedBy", FieldTypes.User, "User")]
        [InlineData("SystemLastModifiedOn", FieldTypes.Date, nameof(DateTime) + "?")]
        public void GetProperties_SystemFieldsAreAdded(string fieldName, FieldTypes fieldType, string fieldTypeName)
        {
            //ARRANGE
            var def = new ObjectDef();
            def.Fields = new List<Field>
            {
                new Field(fieldName, fieldType, true)
            };

            //ACT
            var writer = new ClassWriter();
            var text = writer.WriteClasses(new Application
            {
                Objects = new List<ObjectDef> { def }
            });

            var members = ParseTestHelper.GetProperties(text);

            //ASSERT
            Assert.Contains(members, x=>x.Identifier.Text == fieldName && x.Type.ToString() == fieldTypeName);
        }

        [Fact]
        public void GetProperties_SystemFieldsAreFormedCorrectly()
        {
            //ARRANGE
            var def = new ObjectDef();
            def.Fields = new List<Field> {
                new Field("SystemCreatedBy", FieldTypes.User, true)
            };

            //ACT
            var writer = new ClassWriter();
            var text = writer.WriteClasses(new Application
            {
                Objects = new List<ObjectDef> { def }
            });

            var members = ParseTestHelper.GetProperties(text);

            //ASSERT
            Assert.Contains(members, 
            x => x.ToString().EqualsIgnoreWhitespace("public User SystemCreatedBy { get { return base.Artifact.SystemCreatedBy; } set { base.Artifact.SystemCreatedBy = value; } }"));

        }
    }
}
