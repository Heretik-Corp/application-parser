using Heretik.ApplicationParser.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationParser.Tests.Writers
{
    public class ApplicationWriterTests
    {
        [Fact]
        public void WriteApplicationDefinition_EnsureNameGetsWrittenCorrectly()
        {
            //ARRANGE
            var application = new Application();
            application.Name = "appName";
            application.Guid = "635c6cbe-e370-49e8-be45-059fa5979ad5";

            //ACT
            var writer = new ApplicationWriter();
            var text = writer.WriteApplicationDefinition(application);

            //ASSERT
            var props = ParseTestHelper.GetClassDefinition(text).Members.ToArray();
            Assert.Equal("public const string Name = \"appName\";", props[0].ToString(), ignoreWhiteSpaceDifferences: true);
            Assert.Equal("public const string Guid = \"635c6cbe-e370-49e8-be45-059fa5979ad5\";", props[1].ToString(), ignoreWhiteSpaceDifferences: true);
        }
    }
}
