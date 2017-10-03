using System.Collections.Generic;

namespace ApplicationParser
{
    public class Field : ArtifactDef
    {
        public IEnumerable<ArtifactDef> Choices { get; set; }
        public FieldTypes FieldType { get; set; }
        public bool IsSystem { get; set; }
        public Field() { }
        public Field(string name, FieldTypes fieldType, bool systemField) 
        {
            this.Name = name;
            this.FieldType = fieldType;
            this.IsSystem = systemField;
        }
    }
}
