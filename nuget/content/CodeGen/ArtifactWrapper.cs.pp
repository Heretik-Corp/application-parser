using kCura.Relativity.Client.DTOs;
using System;

namespace $rootnamespace$
{
    public class ArtifactWrapper
    {
        public RDO RDO { get; set; }

        public void SetValue<T>(Guid fieldGuid, T value)
        {
            if (this.RDO[fieldGuid] == null)
            {
                this.RDO.Fields.Add(new FieldValue(fieldGuid, value));
            }
            else
            {
                this.RDO[fieldGuid].Value = value;
            }
        }

        public T GetValue<T>(Guid fieldGuid)
        {
            if (this.RDO[fieldGuid] == null)
            {
                throw new FieldNotLoadedException($"Field with guid {fieldGuid} is not loaded on this object.");
            }
            return (T)this.RDO[fieldGuid]?.Value;
        }

        public bool TryGetValue<T>(Guid fieldGuid, out T value)
        {
            value = default(T);
            if (this.RDO[fieldGuid] == null)
            {
                return false;
            }
            value = GetValue<T>(fieldGuid);
            return true;
        }
    }
}
