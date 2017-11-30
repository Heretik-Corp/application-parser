using Heretik.ApplicationParser.Writers;
using System.Collections.Generic;
using Xunit;

namespace ApplicationParser.Tests.Writers
{
    public class TabWriterTests
    {
        [Fact]
        public void WriteScripts_ScriptGuidCorrect()
        {
            //ARRANGE
            var app = new Application();
            app.Tabs = new List<Tab> {
                new Tab
                {
                    Name = "testTab",
                    Guid = "ac4a4f4c-b9af-4c87-8fb4-35ca0f4a9b63"
                }
            };

            //ACT
            var writer = new TabWriter();
            var text = writer.WriteTabGuids(app);

            //ASSERT
            var properties = ParseTestHelper.GetClassDefinition(text).Members.First();
            Assert.Equal("public const string testTab = \"ac4a4f4c-b9af-4c87-8fb4-35ca0f4a9b63\";", properties.ToString(), ignoreWhiteSpaceDifferences: true);
        }
    }
}
