using kCura.Relativity.Client.DTOs;
using ApplicationParser;
using System;
using System.Linq;

namespace $rootnamespace$
{
    public static class APArtifactExtensions
    {
        public static void SetValue<T>(this kCura.Relativity.Client.DTOs.Artifact artifact, Guid fieldGuid, T value)
        {
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
            if (artifact[fieldGuid] == null)
            {
                throw new FieldNotLoadedException($"Field with guid {fieldGuid} is not loaded on this object.");
            }
            return (T)artifact[fieldGuid]?.Value;
        }
        public static bool TryGetValue<T>(this kCura.Relativity.Client.DTOs.Artifact artifact, Guid fieldGuid, out T value)
        {
            value = default(T);
            if (artifact[fieldGuid] == null)
            {
                return false;
            }
            value = GetValue<T>(fieldGuid);
            return true;
        }
    }
}
