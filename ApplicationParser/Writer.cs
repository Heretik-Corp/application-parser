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
            str.AppendLine($"\t{GetStaticClass("ObjectTypeGuids")}");
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
                str.AppendLine($"\t{GetStaticClass(GetFieldGuidClass(obj))}");
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

        public static string WriteChoiceGuids(this Application app)
        {
            var str = new StringBuilder();
            foreach (var obj in app.Objects)
            {
                foreach (var field in obj.Fields.Where(x => x.Choices.Any()))
                {
                    str.AppendLine($"\t{GetStaticClass(field.Name + "ChoiceGuids")}");
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
            str.AppendLine($"\t{GetStaticClass("TabGuids")}");
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
                sb.AppendLine($"\t{GetClass(obj.Name)} : ArtifactWrapper");
                sb.AppendLine("\t{");
                GetProperties(obj, sb);
                sb.AppendLine("\t}");
                sb.AppendLine();
            }
            return sb.ToString();
        }
        private static void GetProperties(ObjectDef obj, StringBuilder str)
        {
            foreach (var field in obj.Fields)
            {
                var parseString = $"Guid.Parse({GetFieldGuidClass(obj)}.{field.Name})";
                var fieldTypeTuple = GetFieldType(field);
                str.Append($"\t\tpublic {fieldTypeTuple.Item1} {field.Name}");
                str.Append(" {");
                str.Append($" get {{ return base.GetValue<{fieldTypeTuple.Item1}>({parseString}); }}");
                str.Append($" set {{ base.SetValue({parseString}, value); }}");
                str.Append(" }");
                str.AppendLine();
            }
            return;
        }

        private static Tuple<string, string> GetFieldType(Field field)
        {
            switch ((FieldTypes)field.FieldTypeId)
            {
                case FieldTypes.FixedLength:
                case FieldTypes.LongText:
                    return new Tuple<string, string>("string", "ValueAsFixedLengthText");
                case FieldTypes.Decimal:
                case FieldTypes.Currency:
                    return new Tuple<string, string>("decimal?", "ValueAsDecimal");
                case FieldTypes.WholeNumber:
                    return new Tuple<string, string>("int?", "ValueAsWholeNumber");
                case FieldTypes.SingleChoice:
                    return new Tuple<string, string>("Choice", "ValueAsSingleChoice");
                case FieldTypes.SingleObject:
                    return new Tuple<string, string>("Artifact", "ValueAsSingleObject");
                case FieldTypes.User:
                    return new Tuple<string, string>("User", "ValueAsUser");
                case FieldTypes.YesNo:
                    return new Tuple<string, string>("bool?", "ValueAsYesNo");
                case FieldTypes.Date:
                    return new Tuple<string, string>("DateTime?", "ValueAsDate");
                case FieldTypes.File:
                case FieldTypes.MultiChoice:
                case FieldTypes.MultiObject:
                default:
                    return new Tuple<string, string>("object", "Value");
            }
        }
        #region private parts
        private static string GetString(ArtifactDef obj)
        {
            return $"public const string {obj.Name} = \"{obj.Guid}\";";
        }
        private static string GetStaticClass(string name)
        {
            return $"public static class {name}";
        }

        private static string GetClass(string name)
        {
            return $"public class {name}";
        }
        #endregion

    }
}
