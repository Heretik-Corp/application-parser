using System.Collections.Generic;

namespace ApplicationParser
{
    public class Field : ArtifactDef
    {
        public IEnumerable<ArtifactDef> Choices { get; set; }
        public int FieldTypeId { get; set; }
    }
}
