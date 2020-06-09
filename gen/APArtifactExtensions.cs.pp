using kCura.Relativity.Client.DTOs;
using ApplicationParser;
using System;
using System.Linq;
using Relativity.Services.Objects.DataContracts;
using System.Collections.Generic;

namespace $rootnamespace$
{
    public static class APArtifactExtensions
    {
        public static void SetValue<T>(this kCura.Relativity.Client.DTOs.Artifact artifact, Guid fieldGuid, T value)
        {
            if (artifact == null)
            {
                throw new ArgumentNullException(nameof(artifact));
            }
            if (!artifact.Fields.Any(x => x.Guids.Any(y => y.Equals(fieldGuid))))
            {
                artifact.Fields.Add(new FieldValue(fieldGuid, value));
            }
            else
            {
                artifact[fieldGuid].Value = value;
            }
        }
        public static T GetValue<T>(this kCura.Relativity.Client.DTOs.Artifact artifact, Guid fieldGuid)
        {
            if (artifact == null)
            {
                throw new ArgumentNullException(nameof(artifact));
            }
            if (artifact[fieldGuid] == null)
            {
                throw new FieldNotLoadedException($"Field with guid {fieldGuid} is not loaded on this object.");
            }
            return (T)artifact[fieldGuid]?.Value;
        }
        public static bool TryGetValue<T>(this kCura.Relativity.Client.DTOs.Artifact artifact, Guid fieldGuid, out T value)
        {
            if (artifact == null)
            {
                throw new ArgumentNullException(nameof(artifact));
            }
            value = default(T);
            if (artifact[fieldGuid] == null)
            {
                return false;
            }
            value = GetValue<T>(artifact, fieldGuid);
            return true;
        }

        public static T GetValue<T>(this RelativityObjectWrapper obj, Guid fieldGuid)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (obj.FieldValues == null)
            {
                obj.FieldValues = new List<FieldValuePairTracker>();
            }
            if (!obj.FieldValueExists(fieldGuid))
            {
                throw new Exception($"Field with guid {fieldGuid} is not loaded on this object.");
            }
            var value = obj[fieldGuid].Value;
            return (T)value;
        }

        public static void SetValue<T>(this RelativityObjectWrapper obj, Guid fieldGuid, T value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (obj.FieldValues == null)
            {
                obj.FieldValues = new List<FieldValuePairTracker>();
            }
            if (!obj.FieldValueExists(fieldGuid))
            {
                obj.FieldValues.Add(new FieldValuePairTracker(true)
                {
                    Field = new Relativity.Services.Objects.DataContracts.Field
                    {
                        Guids = new List<Guid> { fieldGuid },
                    },
                    Value = value,
                });
            }
            else
            {
                obj[fieldGuid].Value = value;
                obj[fieldGuid].Updated= true;
            }
        }
    }
}
