using ApplicationParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heretik.ApplicationParser.Writers
{
    class WriterUtils
    {
        public static string GetString(ArtifactDef obj)
        {
            return $"public const string {obj.Name} = \"{obj.Guid}\";";
        }

        public static string GetClass(string name)
        {
            return $"public partial class {name}";
        }
        public static string GetFieldGuidClass(ObjectDef obj)
        {
            return $"{obj.Name}FieldGuids";
        }
    }
}
