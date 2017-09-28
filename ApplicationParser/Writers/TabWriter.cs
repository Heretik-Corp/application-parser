using ApplicationParser;
using System.Text;

namespace Heretik.ApplicationParser.Writers
{
    public class TabWriter
    {
        public string WriteTabGuids(Application app)
        {
            var str = new StringBuilder();
            str.AppendLine($"\t{WriterUtils.GetClass("TabGuids")}");
            str.AppendLine("\t{");
            foreach (var tab in app.Tabs)
            {
                str.AppendLine($"\t\t{WriterUtils.GetString(tab)}");
            }
            str.AppendLine("\t}");
            return str.ToString();
        }
    }
}
