using ApplicationParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heretik.ApplicationParser.Writers
{
    public class ClassWriter
    {

        public virtual string WriteClasses(Application app)
        {
            var sb = new StringBuilder();
            foreach (var obj in app.Objects)
            {
                WriteClassName(obj, sb);
                sb.AppendLine("\t{");
                sb.Append(this.GetProperties(obj));
                sb.AppendLine("\t}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        protected virtual void WriteClassName(ObjectDef obj, StringBuilder sb)
        {
            if (obj.Name.Equals("Document"))
            {
                sb.AppendLine($"\t{WriterUtils.GetClass(obj.Name)} : DocumentWrapper");
            }
            else
            {
                sb.AppendLine($"\t{WriterUtils.GetClass(obj.Name)} : RDOWrapper");
            }
        }

        protected virtual string GetProperties(ObjectDef objDef)
        {
            var sb = new StringBuilder();
            var fields = (objDef.Fields ?? new List<Field>()).ToList();
            objDef.Fields = fields;
            foreach (var field in objDef.Fields)
            {
                if(field.Name == "ArtifactID")
                {
                    continue;
                }
                WriteField(objDef, field, sb);
            }
            return sb.ToString();
        }

        protected virtual void WriteField(ObjectDef objDef, Field field, StringBuilder sb)
        {
            var fieldType = GetFieldType(field);
            //Fields causing problems:
            //ControlNumber reason => is system but not on document object
            //ExtractedText reason => is system but not on document object
            //Name reason => Maps to textIdentifier
            //Following fields are "system fields" but not on the Artifact Property:
            //ModelFileIcon
            //ModelFileSize
            //ModelText
            //HeretikAnalysisSet on object HeretikAnalysisDocument
            //HeretikProfile on object HeretikTrainingDocument

            //We also need to support ParentArtifact of type Artifact isSystem:true
            sb.Append($"\t\tpublic {fieldType} {field.Name}");
            sb.Append(" {");
            if (field.IsSystem)
            {
                sb.Append($" get {{ return base.Artifact.{field.Name}; }}");
                sb.Append($" set {{ base.Artifact.{field.Name} = value; }}");
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
            if(field.IsSystem && field.Name.EndsWith("By", StringComparison.CurrentCultureIgnoreCase))
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

