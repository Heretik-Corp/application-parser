using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationParser.Writers
{

    public class RDOFieldTypeGeneratorFactory
    {
        public static RDOFieldTypeGenerator GetFielGenerator(ObjectDef obj)
        {
            if (obj.ShouldUseOMModel)
            {
                return new OMFieldTypeGenerator();
            }
            return new RDOFieldTypeGenerator();
        }
    }

    public class RDOFieldTypeGenerator
    {
        public virtual string GetFieldType(Field field)
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

    public class OMFieldTypeGenerator : RDOFieldTypeGenerator
    {
        public override string GetFieldType(Field field)
        {
            var b = base.GetFieldType(field);

            switch (field.FieldType)
            {
                case FieldTypes.SingleObject:
                    throw new NotImplementedException();
                case FieldTypes.MultiChoice:
                    throw new NotImplementedException();
                case FieldTypes.SingleChoice:
                    return "Relativity.Services.Objects.DataContracts.Choice";
                case FieldTypes.MultiObject:
                    throw new NotImplementedException();
                case FieldTypes.User:
                    throw new NotImplementedException();
                default:
                    return b;

            }
        }

    }
}
