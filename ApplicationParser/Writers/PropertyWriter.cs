using ApplicationParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heretik.ApplicationParser.Writers
{
    public class PropertyWriter
    {

        public string GetProperties(ObjectDef objDef)
        {
            var sb = new StringBuilder();
            foreach (var field in objDef.Fields)
            {
                //TODO: does relativity's fields support ArtifactId?
                if(field.Name == "ArtifactID")
                {
                    continue;
                }
                var parseString = $"Guid.Parse({WriterUtils.GetFieldGuidClass(objDef)}.{field.Name})";
                var fieldType = GetFieldType(field);
                sb.Append($"\t\tpublic {fieldType} {field.Name}");
                sb.Append(" {");
                sb.Append($" get {{ return base.Artifact.GetValue<{fieldType}>({parseString}); }}");
                sb.Append($" set {{ base.Artifact.SetValue({parseString}, value); }}");
                sb.Append(" }");
                sb.AppendLine();
            }
            return sb.ToString();
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
    }
}
