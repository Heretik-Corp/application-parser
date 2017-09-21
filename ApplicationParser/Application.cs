using System.Collections;
using System.Collections.Generic;

namespace ApplicationParser
{
    public class Application : ArtifactDef
    {
        public IEnumerable<ObjectDef> Objects { get; set; }
        public IEnumerable<Tab> Tabs { get; set; }
        public IEnumerable<Script> Scripts { get; set; }
    }
}
