using ApplicationParser;
using System.Linq;
using System.Text;

namespace Heretik.ApplicationParser.Writers
{
    public class ChoiceWriter
    {
        /// <summary>
        /// Writes out all choice guids for all fields and object types
        /// output for Document with field 'Choice Type' would look similar to:
        /// public class DocumentChoiceTypeChoiceGuids
        /// {
        ///     public const string Choice1 = "7b742f38-5d09-49a7-983c-b563ae7a07d2";
        /// }
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
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
