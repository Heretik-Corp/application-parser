using Heretik.ApplicationParser.Writers;
using System;
using System.Linq;
using System.Text;

namespace ApplicationParser
{
    public static partial class Writer
    {
        private static ClassWriter classWriter = new ClassWriter();
        private static ScriptWriter scriptWriter = new ScriptWriter();
        private static TabWriter tabWriter = new TabWriter();
        private static ApplicationWriter applicationWriter = new ApplicationWriter();
        private static ChoiceWriter choiceWriter = new ChoiceWriter();

        public static string WriteObjectTypeGuids(this Application app)
        {
            var str = new StringBuilder();
            str.AppendLine($"\t{WriterUtils.GetClass("ObjectTypeGuids")}");
            str.AppendLine("\t{");
            foreach (var obj in app.Objects)
            {
                str.AppendLine($"\t\t{WriterUtils.GetString(obj)}");
            }
            str.AppendLine("\t}");
            return str.ToString();
        }

        public static string WriteObjectGuids(this Application app)
        {
            var str = new StringBuilder();
            foreach (var obj in app.Objects)
            {
                str.AppendLine($"\t{WriterUtils.GetClass(WriterUtils.GetFieldGuidClass(obj))}");
                str.AppendLine("\t{");
                foreach (var field in obj.Fields)
                {
                    str.AppendLine($"\t\t{WriterUtils.GetString(field)}");
                }
                str.AppendLine("\t}");
            }
            return str.ToString();
        }

        public static string WriteApplicationDefinition(this Application app)
        {
            return applicationWriter.WriteApplicationDefinition(app);
        }

        public static string WriteChoiceGuids(this Application app)
        {
            return choiceWriter.WriteChoiceGuids(app);
        }

        public static string WriteTabGuids(this Application app)
        {
            return tabWriter.WriteTabGuids(app);
        }

        public static string WriteClasses(this Application app)
        {
            return classWriter.WriteClasses(app);
        }

        public static string WriteScripts(this Application app)
        {
            return scriptWriter.WriteScripts(app);
        }

    }
}
