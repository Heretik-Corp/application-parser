using Microsoft.CodeAnalysis;
using System.Linq;
using System.Xml;
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
            Assert.Single(objects);
            Assert.Equal("15c36703-74ea-4ff8-9dfb-ad30ece7530d", objects[0].Guid);
            Assert.Equal("A custom Object", objects[0].RawName);
        }

        [Fact]
        public void ParseObjects_ParsesObjectRule()
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
      <ObjectRules>
        <ObjectRule>
          <Name>Show Segmentation Layout</Name>
          <Action>&lt;action&gt;&lt;selectedLayoutID&gt;1071483&lt;/selectedLayoutID&gt;&lt;allowChange&gt;N&lt;/allowChange&gt;&lt;/action&gt;</Action>
          <ArtifactTypeID>1000054</ArtifactTypeID>
          <FieldArtifactID>1071337</FieldArtifactID>
          <Guid>80b68dcc-8d7c-465c-a142-154cc0472445</Guid>
          <ObjectRuleID>1071486</ObjectRuleID>
          <Operator />
          <Type>DefaultLayout</Type>
          <Value>&lt;value&gt;&lt;selectedCodeID&gt;1071339&lt;/selectedCodeID&gt;&lt;/value&gt;</Value>
          <ApplicationVersion>1.4.149.59</ApplicationVersion>
        </ObjectRule>
</ObjectRules>
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
            Assert.Single(objects);
            Assert.Equal("80b68dcc-8d7c-465c-a142-154cc0472445", objects.Single().ObjectRules.Single().Guid);
            Assert.Equal("ShowSegmentationLayout", objects.Single().ObjectRules.Single().Name);
        }


        [Fact]
        public void ParseFields_NonSystemFieldsPassIn_ReturnsFalseForFieldDef()
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
        <Fields>
<Field>
        <DisplayName>test field name</DisplayName>
        <Guid>ce3f1380-7535-49f1-b45b-ce40dc9d0742</Guid>
        <FieldTypeId>1</FieldTypeId>
</Field>
        </Fields>
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
            var field = objects.First().Fields.First();

            Assert.Equal("test field name", field.RawName);
            Assert.Equal("ce3f1380-7535-49f1-b45b-ce40dc9d0742", field.Guid);
            Assert.Equal((FieldTypes)1, field.FieldType);
            Assert.False(field.IsSystem);

        }
    }
}
