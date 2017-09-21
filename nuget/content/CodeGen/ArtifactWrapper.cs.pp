using kCura.Relativity.Client.DTOs;
using ApplicationParser;
using System;

namespace $rootnamespace$
{
    public class ArtifactWrapper
    {
		private Artifact _rdo;
        public Artifact RDO { get { return _rdo ?? (_rdo = new Artifact()); } set { _rdo = value; } }
		public int ArtifactId { get { return RDO.ArtifactID; } }
        public void SetValue<T>(Guid fieldGuid, T value)
        {
            if (!this.RDO.Fields.Any(x => x.Guids.Any(y => y.Equals(fieldGuid))))
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
