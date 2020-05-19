using System.Collections.Generic;

namespace ApplicationParser
{
    public class ObjectDef : ArtifactDef
    {
        public bool ShouldUseOMModel { get; set; } = false;
        public IEnumerable<Field> Fields { get; set; }
        public IEnumerable<ObjectRule> ObjectRules { get; set; } = new List<ObjectRule>();
        public IEnumerable<Layout> Layouts { get; set; } = new List<Layout>();
    }
}