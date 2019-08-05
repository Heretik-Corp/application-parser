using ApplicationParser;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heretik.ApplicationParser.Writers
{
    public class ObjectRuleWriter
    {
        public string WriteObjectRules(IEnumerable<ObjectDef> objs)
        {
            var sb = new StringBuilder();
            foreach (var obj in objs.Where(x => (x.ObjectRules?.ToList() ?? new List<ObjectRule>()).Any()))
            {
                sb.AppendLine($"\t{WriterUtils.GetClass(obj.Name)}ObjectRules");
                sb.AppendLine("\t{");
                foreach (var rule in obj.ObjectRules)
                {
                    sb.AppendLine($"\t\t{WriterUtils.GetString(rule)}");
                }
                sb.AppendLine("\t}");

            }
            return sb.ToString();
        }
    }
}
