using ApplicationParser.Writers;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApplicationParser.Tests.Writers
{
    public class ObjectRuleWriterTests
    {
        private readonly ObjectRuleWriter _writer;

        public ObjectRuleWriterTests()
        {
            _writer = new ObjectRuleWriter();
        }
        [Fact]
        public void WriteObjectRules_ObjectRuleNull_ReturnsEmptyString()
        {
            // ARRANGE
            var rule = new ObjectRule
            {
                Name = "hello",
                Guid = Guid.NewGuid().ToString()
            };

            // ACT
            var result = _writer.WriteObjectRules(new List<ObjectDef>
            {
                new ObjectDef
                {
                    Name = "obj",
                    ObjectRules = null
                }
            });

            // ASSERT
            Assert.Empty(result);
        }

        [Fact]
        public void WriteObjectRules_ObjectRuleEmpty_ReturnsEmptyString()
        {
            // ARRANGE
            var rule = new ObjectRule
            {
                Name = "hello",
                Guid = Guid.NewGuid().ToString()
            };

            // ACT
            var result = _writer.WriteObjectRules(new List<ObjectDef>
            {
                new ObjectDef
                {
                    Name = "obj",
                    ObjectRules = new List<ObjectRule>()
                }
            });

            // ASSERT
            Assert.Empty(result);
        }


        [Fact]
        public void WriteObjectRules_ObjectRulePassedIn_ReturnsCorrectClassName()
        {
            // ARRANGE
            var rule = new ObjectRule
            {
                Name = "hello",
                Guid = Guid.NewGuid().ToString()
            };

            // ACT
            var result = _writer.WriteObjectRules(new List<ObjectDef>
            {
                new ObjectDef
                {
                    Name = "obj",
                    ObjectRules = new List<ObjectRule>{ rule}
                }
            });

            // ASSERT
            Assert.Contains("public partial class objObjectRules", result);
        }

        [Fact]
        public void WriteObjectRules_ObjectRulePassedIn_ReturnsCorrectProperty()
        {
            // ARRANGE
            var rule = new ObjectRule
            {
                Name = "hello",
                Guid = Guid.NewGuid().ToString()
            };

            // ACT
            var result = _writer.WriteObjectRules(new List<ObjectDef>
            {
                new ObjectDef
                {
                    Name = "obj",
                    ObjectRules = new List<ObjectRule>{ rule}
                }
            });

            // ASSERT
            Assert.Contains($"public const string hello = \"{rule.Guid}\";", result);
        }
    }
}
