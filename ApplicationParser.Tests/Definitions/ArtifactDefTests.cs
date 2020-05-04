
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationParser.Tests.Definitions
{
    public class ArtifactDefTests
    {

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] { "#field", "h_field"},
                new object[] { "field#", "field_h" },
                new object[] { "field%", "fieldPercent" },
                new object[] { "field::field", "field_field" },
                new object[] { "field-field", "field_field" },
                new object[] { "field&field", "fieldAndfield" },
                new object[] { "field^field", "fieldcarrotfield" },
                new object[] { "field#field", "field_field" },
                new object[] { "field)field", "fieldfield" },
                new object[] { "field.field", "fieldfield" },
                new object[] { "field,field", "fieldfield" },
                new object[] { "field'field", "fieldfield" },
                new object[] { "field/field", "fieldfield" },
                new object[] { "1.field", "field" },
                new object[] { "field.1.field", "field1field" },
            };
            return allData;
        }


        [Theory]
        [MemberData(nameof(GetData))]
        public void EncodeName_PassInValue_TrimsCorrectResults(string input, string expected)
        {
            // ARRANGE

            // ACT
            var result = ArtifactDef.EncodeName(input);

            // ASSERT
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void GetName_PassInValue_TrimsCorrectResults(string input, string expected)
        {
            // ARRANGE
            var a = new ArtifactDef();
            a.Name = input;
            // ACT
            var result = a.Name;

            // ASSERT
            Assert.Equal(expected, result);
        }
    }
}
