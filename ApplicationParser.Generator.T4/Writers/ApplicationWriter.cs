using ApplicationParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heretik.ApplicationParser.Writers
{
    public class ApplicationWriter
    {
        public string WriteApplicationDefinition(Application app)
        {
            var sb = new StringBuilder();
            sb.AppendLine("\tpublic static class Application");
            sb.AppendLine("\t{");
            sb.AppendLine($"\t\tpublic const string Name = \"{app.Name}\";");
            sb.AppendLine($"\t\tpublic const string Guid = \"{app.Guid}\";");
            sb.AppendLine("\t}");
            return sb.ToString();
        }
    }
}
