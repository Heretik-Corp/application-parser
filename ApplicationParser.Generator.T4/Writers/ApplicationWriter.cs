using ApplicationParser;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heretik.ApplicationParser.Writers
{
    public class LayoutWriter
    {
        public string WriteLayoutDefinitions(IEnumerable<ObjectDef> objs)
        {
            var sb = new StringBuilder();
            foreach (var obj in objs.Where(x => (x.Layouts?.ToList() ?? new List<Layout>()).Any()))
            {
                sb.AppendLine($"\t{WriterUtils.GetClass(obj.Name)}Layouts");
                sb.AppendLine("\t{");
                foreach (var rule in obj.Layouts)
                {
                    sb.AppendLine($"\t\t{WriterUtils.GetString(rule)}");
                }
                sb.AppendLine("\t}");

            }
            return sb.ToString();
        }
    }
}
