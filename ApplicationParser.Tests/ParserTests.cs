using System;
using System.Xml.Linq;
using System.Xml;
using System.Linq;
using Xunit;

namespace ApplicationParser.Tests
{
    public class ParserTests
    {
        [Fact]
        public void ParseScripts_SanityCheck()
        {
            //ARRANGE
            var xmlTemplate = @"<?xml version=""1.0"" encoding=""UTF - 8""?>
<Application xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
    <ApplicationScripts>
        <ScriptElement>
          <ArtifactId>1060974</ArtifactId>
          <Name>App Parser Script</Name>
          <Description />
          <Version />
          <Key />
          <Body>
            N/A
          </Body>
          <Category />
          <SystemScriptArtifactID xsi:nil=""true"" />
          <IsMasterScript>false</IsMasterScript>
          <Guid>9a070c2a-ff83-4d22-bdd9-a04d02ef7177</Guid>
          <ApplicationVersion>0.0.829.8</ApplicationVersion>
        </ScriptElement>
      </ApplicationScripts>
</Application>";
            //ACT
            var parser = new Parser();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlTemplate);
            var scripts = parser.ParseScripts(xmlDoc).ToList();

            //ASSERT
            Assert.Equal(1, scripts.Count());
            Assert.Equal("9a070c2a-ff83-4d22-bdd9-a04d02ef7177", scripts[0].Guid);
            Assert.Equal("App Parser Script", scripts[0].RawName);

        }

        [Fact]
        public void ParseObjects_SanityCheck()
        {
            //ARRANGE
            var xmlTemplate = @"<?xml version=""1.0"" encoding=""UTF - 8""?>
<Application xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <Objects>
    <Object>
      <ArtifactId>1035231</ArtifactId>
      <DescriptorArtifactTypeId>10</DescriptorArtifactTypeId>
      <Guid>15c36703-74ea-4ff8-9dfb-ad30ece7530d</Guid>
      <Name>A custom Object</Name>
        <Fields />
        <SystemFields />
    </Object>
</Objects>
</Application>
";
            //ACT
            var parser = new Parser();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlTemplate);
            var objects = parser.ParseObjects(xmlDoc).ToList();

            //ASSERT
            Assert.Equal(1, objects.Count());
            Assert.Equal("15c36703-74ea-4ff8-9dfb-ad30ece7530d", objects[0].Guid);
            Assert.Equal("A custom Object", objects[0].RawName);
        }
    }
}
