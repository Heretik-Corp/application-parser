using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ApplicationParser
{
    public class Parser
    {
        public Application Parse(string xml)
        {
            var xmlDoc = new XmlDocument(); // Create an XML document object
            xmlDoc.LoadXml(xml);
            var app = new Application();
            app.Guid = xmlDoc.SelectSingleNode("Application").SelectSingleNode("Guid").InnerText;
            app.Name = xmlDoc.SelectSingleNode("Application").SelectSingleNode("Name").InnerText;

            app.Objects = ParseObjects(xmlDoc);

            var tabList = new List<Tab>();
            var tabs = xmlDoc.GetElementsByTagName("Tab");
            foreach (XmlNode obj in tabs)
            {
                var objDef = ParseNode<Tab>(obj);
                tabList.Add(objDef);
            }
            app.Tabs = tabList;
            app.Scripts = ParseScripts(xmlDoc).ToList();
            return app;
        }

        public IEnumerable<ObjectDef> ParseObjects(XmlDocument xmlDoc)
        {
            var objects = xmlDoc.GetElementsByTagName("Object");
            foreach (XmlNode obj in objects)
            {
                var objDef = ParseObject(obj);
                yield return objDef;
            }
        }

        public IEnumerable<Script> ParseScripts(XmlDocument xmlDoc)
        {
            var scriptsList = new List<Script>();
            var scripts = xmlDoc.GetElementsByTagName("ApplicationScripts").Item(0).SelectNodes("ScriptElement");

            foreach (XmlNode obj in scripts)
            {
                var objDef = ParseNode<Script>(obj);
                yield return objDef;
            }
        }

        private ObjectDef ParseObject(XmlNode node)
        {
            var obj = new ObjectDef();
            obj.Name = node.SelectSingleNode("Name").InnerText;
            obj.Guid = node.SelectSingleNode("Guid").InnerText;
            var fields = node.SelectSingleNode("Fields").SelectNodes("Field");
            var systemFields = node.SelectSingleNode("SystemFields").SelectNodes("SystemField");
            var fieldList = new List<Field>();
            foreach (XmlNode field in fields)
            {
                var fieldDef = ParseField(field);
                if (fieldDef != null)
                {
                    fieldList.Add(fieldDef);
                }
            }
            foreach (XmlNode field in systemFields)
            {
                var fieldDef = ParseField(field, true);
                if (fieldDef != null)
                {
                    fieldList.Add(fieldDef);
                }
            }
            obj.Fields = fieldList;
            return obj;
        }

        private Field ParseField(XmlNode field, bool system = false)
        {
            var guid = field.SelectSingleNode("Guid");
            var name = field.SelectSingleNode("DisplayName");
            var fieldId = (FieldTypes)int.Parse(field.SelectSingleNode("FieldTypeId").InnerText);
            var artifact = new Field
            {
                Guid = guid.InnerText,
                Name = name.InnerText,
                FieldType = fieldId,
                IsSystem = system

            };
            var choiceList = new List<ArtifactDef>();
            if (field.SelectNodes("Codes").Count > 0)
            {
                var codes = field.SelectSingleNode("Codes").SelectNodes("Code");
                foreach (XmlNode code in codes)
                {
                    var choiceDef = ParseNode<Field>(code);
                    choiceList.Add(choiceDef);
                }
            }
            artifact.Choices = choiceList;
            return artifact;
        }
        private T ParseNode<T>(XmlNode node) where T : ArtifactDef, new()
        {
            var guid = node.SelectNodes("Guid").Item(0);
            var name = node.SelectNodes("Name").Item(0);
            var artifact = new T { Guid = guid.InnerText, Name = name.InnerText };
            return artifact;
        }
    }
}
