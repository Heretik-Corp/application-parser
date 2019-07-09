using ApplicationParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heretik.ApplicationParser.Writers
{
    public class ClassWriter
    {
        public virtual string WriteClassMetaData(Application app)
        {
            var sb = new StringBuilder();
            foreach (var obj in FilterClassNames(app.Objects))
            {
                foreach (var field in obj.Fields)
                {
                    WriteClassName(obj, sb, $"{field.Name}FieldMetaData", false);
                    sb.AppendLine("\t{");
                    sb.Append(this.GetPropertyMetaData(field));
                    sb.AppendLine("\t}");
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
        public virtual string WriteClasses(Application app)
        {
            var sb = new StringBuilder();
            foreach (var obj in FilterClassNames(app.Objects))
            {
                WriteClassName(obj, sb, string.Empty, true);
                sb.AppendLine("\t{");
                sb.Append(this.GetProperties(obj));
                sb.AppendLine("\t}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public virtual IEnumerable<ObjectDef> FilterClassNames(IEnumerable<ObjectDef> objects)
        {
            return objects.Where(x => !x.Name.Equals("field", StringComparison.InvariantCultureIgnoreCase));
        }

        protected virtual void WriteClassName(ObjectDef obj, StringBuilder sb, string suffix, bool includeInheritance)
        {
            if (obj.Name.Equals("Document"))
            {
                sb.AppendLine($"\t{WriterUtils.GetClass($"{obj.Name}{suffix}")} {(includeInheritance ? ": DocumentWrapper" : "")}");
            }
            else
            {
                sb.AppendLine($"\t{WriterUtils.GetClass($"{obj.Name}{suffix}")} {(includeInheritance ? ": RDOWrapper" : "")}");
            }
        }

        protected virtual string GetProperties(ObjectDef objDef)
        {
            var sb = new StringBuilder();
            var fields = (objDef.Fields ?? new List<Field>()).ToList();
            objDef.Fields = fields;
            foreach (var field in objDef.Fields)
            {
                if (field.Name == "ArtifactID")
                {
                    continue;
                }
                WriteField(objDef, field, sb);
            }
            return sb.ToString();
        }

        protected virtual string GetPropertyMetaData(Field field)
        {
            var sb = new StringBuilder();
            if (field.FieldType == FieldTypes.FixedLength || field.FieldType == FieldTypes.LongText)
            {
                sb.AppendLine($"\t\tpublic const int MAX_LENTH = {field.MaxLength};");
            }
            return sb.ToString();
        }


        protected virtual void WriteField(ObjectDef objDef, Field field, StringBuilder sb)
        {
            var fieldType = GetFieldType(field);
            //We also need to support ParentArtifact of type Artifact isSystem:true
            sb.Append($"\t\tpublic {fieldType} {field.Name}");
            sb.Append(" {");
            if (field.IsSystem && field.Name.StartsWith("System")) //this might cause a problem but for now I'm lazy
            {
                sb.Append($" get {{ return base.Artifact.{field.Name}; }}");
            }
            else if (field.IsSystem && field.Name.Equals("Name"))
            {
                sb.Append($" get {{ return base.Artifact.TextIdentifier; }}");
                sb.Append($" set {{ base.Artifact.TextIdentifier = value; }}");
            }
            else
            {
                var parseString = $"Guid.Parse({WriterUtils.GetFieldGuidClass(objDef)}.{field.Name})";
                sb.Append($" get {{ return base.Artifact.GetValue<{fieldType}>({parseString}); }}");
                sb.Append($" set {{ base.Artifact.SetValue({parseString}, value); }}");
            }
            sb.AppendLine("\t\t}");
        }

        protected string GetFieldType(Field field)
        {
            //This is actually a relativity bug
            if (field.IsSystem && (field.Name.Equals("SystemLastModifiedBy", StringComparison.CurrentCultureIgnoreCase) ||
                                   field.Name.Equals("SystemCreatedBy", StringComparison.CurrentCultureIgnoreCase)))
            {
                field.FieldType = FieldTypes.User;
            }

            switch (field.FieldType)
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
                case FieldTypes.MultiChoice:
                    return "MultiChoiceFieldValueList";
                case FieldTypes.File:
                    return "string";
                default:
                    return "object";
            }
        }
    }
}

