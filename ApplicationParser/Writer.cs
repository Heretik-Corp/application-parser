using System;
using System.Linq;
using System.Text;

namespace ApplicationParser
{
    public static partial class Writer
    {
        public static string WriteObjectTypeGuids(this Application app)
        {
            var str = new StringBuilder();
            str.AppendLine($"\t{GetClass("ObjectTypeGuids")}");
            str.AppendLine("\t{");
            foreach (var obj in app.Objects)
            {
                str.AppendLine($"\t\t{GetString(obj)}");
            }
            str.AppendLine("\t}");
            return str.ToString();
        }
        public static string WriteObjectGuids(this Application app)
        {
            var str = new StringBuilder();
            foreach (var obj in app.Objects)
            {
                str.AppendLine($"\t{GetClass(GetFieldGuidClass(obj))}");
                str.AppendLine("\t{");
                foreach (var field in obj.Fields)
                {
                    str.AppendLine($"\t\t{GetString(field)}");
                }
                str.AppendLine("\t}");
            }
            return str.ToString();
        }
        private static string GetFieldGuidClass(ObjectDef obj)
        {
            return $"{obj.Name}FieldGuids";
        }

        public static string WriteApplicationDefinition(this Application app)
        {
            var sb = new StringBuilder();
            sb.AppendLine("\tpublic static class Application");
            sb.AppendLine("\t{");
            sb.AppendLine($"\t\tpublic const string Name = \"{app.Name}\";");
            sb.AppendLine($"\t\tpublic const string Guid= \"{app.Guid}\";");
            sb.AppendLine("\t}");

            return sb.ToString();
        }
        public static string WriteChoiceGuids(this Application app)
        {
            var str = new StringBuilder();
            foreach (var obj in app.Objects)
            {
                foreach (var field in obj.Fields.Where(x => x.Choices.Any()))
                {
                    //todo this needs More than just fieldName
                    str.AppendLine($"\t{GetClass(obj.Name + field.Name + "ChoiceGuids")}");
                    str.AppendLine("\t{");
                    foreach (var choice in field.Choices)
                    {
                        str.AppendLine($"\t\t{GetString(choice)}");
                    }
                    str.AppendLine("\t}");
                }
            }
            return str.ToString();
        }
        public static string WriteTabGuids(this Application app)
        {
            var str = new StringBuilder();
            //str.AppendLine($"\t{GetClass("TabGuids")}");
            str.AppendLine("\t{");
            foreach (var tab in app.Tabs)
            {
                str.AppendLine($"\t\t{GetString(tab)}");
            }
            str.AppendLine("\t}");
            return str.ToString();
        }

        public static string WriteClasses(this Application app)
        {
            var sb = new StringBuilder();
            foreach (var obj in app.Objects)
            {
                //TODO: this needs Artifact Type Guids
                sb.AppendLine($"\t{GetClass(obj.Name)} : ArtifactWrapper");
                sb.AppendLine("\t{");
                GetProperties(obj, sb);
                sb.AppendLine("\t}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static string WriteScripts(this Application app)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"\t {GetClass("RelativityScripts")}");
            sb.AppendLine("\t{");
            foreach (var obj in app.Scripts)
            {
                sb.AppendLine(GetString(obj));
            }
            sb.AppendLine("\t}");
            sb.AppendLine();
            return sb.ToString();
        }


        #region private parts
        private static void GetProperties(ObjectDef obj, StringBuilder str)
        {
            foreach (var field in obj.Fields)
            {
                var parseString = $"Guid.Parse({GetFieldGuidClass(obj)}.{field.Name})";
                var fieldType = GetFieldType(field);
                str.Append($"\t\tpublic {fieldType} {field.Name}");
                str.Append(" {");
                str.Append($" get {{ return base.GetValue<{fieldType}>({parseString}); }}");
                str.Append($" set {{ base.SetValue({parseString}, value); }}");
                str.Append(" }");
                str.AppendLine();
            }
            return;
        }


        private static string GetFieldType(Field field)
        {
            switch ((FieldTypes)field.FieldTypeId)
            {
                case FieldTypes.FixedLength:
                case FieldTypes.LongText:
                    return "string";
                case FieldTypes.Decimal:
                case FieldTypes.Currency:
                    return "decimal?";
                case FieldTypes.WholeNumber:
                    return "int?";
                case FieldTypes.SingleChoice:
                    return "Choice";
                case FieldTypes.SingleObject:
                    return "Artifact";
                case FieldTypes.User:
                    return "User";
                case FieldTypes.YesNo:
                    return "bool?";
                case FieldTypes.Date:
                    return "DateTime?";
                case FieldTypes.MultiObject:
                    return "FieldValueList<Artifact>";
                case FieldTypes.File:
                case FieldTypes.MultiChoice:
                default:
                    return "object";
            }
        }
        private static string GetString(ArtifactDef obj)
        {
            return $"public const string {obj.Name} = \"{obj.Guid}\";";
        }

        private static string GetClass(string name)
        {
            return $"public partial class {name}";
        }
        #endregion

    }
}
