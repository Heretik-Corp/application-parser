using ApplicationParser;
using System.Linq;
using System.Text;

namespace Heretik.ApplicationParser.Writers
{
    public class ChoiceWriter
    {
        public string WriteChoiceGuids(Application app)
        {
            var str = new StringBuilder();
            foreach (var obj in app.Objects)
            {
                foreach (var field in obj.Fields.Where(x => x.Choices.Any()))
                {
                    str.AppendLine($"\t{WriterUtils.GetClass(obj.Name + field.Name + "ChoiceGuids")}");
                    str.AppendLine("\t{");
                    foreach (var choice in field.Choices)
                    {
                        str.AppendLine($"\t\t{WriterUtils.GetString(choice)}");
                    }
                    str.AppendLine("\t}");
                }
            }
            return str.ToString();
        }
    }
}
