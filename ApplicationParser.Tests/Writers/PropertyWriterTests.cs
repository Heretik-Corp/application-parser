using Heretik.ApplicationParser.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationParser.Tests.Writers
{
    public class PropertyWriterTests
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
            var writer = new PropertyWriter();
            var text = writer.GetProperties(def);

            //ASSERT
            Assert.Equal(string.Empty, text);
        }
    }
}
