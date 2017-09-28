using System.Collections.Generic;

namespace ApplicationParser
{
    public class ObjectDef : ArtifactDef
    {
        public IEnumerable<Field> Fields { get; set; }
    }
}