using System.Collections.Generic;

namespace ApplicationParser
{
    public class ObjectDef : ArtifactDef
    {
        public IEnumerable<Field> Fields { get; set; }
        public IEnumerable<ObjectRule> ObjectRules { get; set; } = new List<ObjectRule>();
        public IEnumerable<Layout> Layouts { get; set; } = new List<Layout>();
    }
}