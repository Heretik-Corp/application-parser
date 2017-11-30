using ApplicationParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heretik.ApplicationParser.Writers
{
    public class ScriptWriter
    {
        public string WriteScripts(Application app)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"\t {WriterUtils.GetClass("RelativityScripts")}");
            sb.AppendLine("\t{");
            foreach (var obj in app.Scripts)
            {
                sb.AppendLine("\t\t" + WriterUtils.GetString(obj));
            }
            sb.AppendLine("\t}");
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
