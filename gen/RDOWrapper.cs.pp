using kCura.Relativity.Client.DTOs;
using ApplicationParser;
using System;
using Relativity.Services.Objects.DataContracts;
using System.Collections.Generic;
using System.Linq;

namespace $rootnamespace$
{
    public class RDOWrapper
    {
		private RDO _rdo;
        public RDO Artifact { get { return _rdo ?? (_rdo = new RDO()); } set { _rdo = value; } }
		public int ArtifactId { get { return this.Artifact.ArtifactID; } }
    }

	public class DocumentWrapper 
    {
		private kCura.Relativity.Client.DTOs.Document _rdo;
        public kCura.Relativity.Client.DTOs.Document Artifact { get { return _rdo ?? (_rdo = new kCura.Relativity.Client.DTOs.Document()); } set { _rdo = value; } }
		public int ArtifactId { get { return Artifact.ArtifactID; } }
    }

    public class FieldValuePairTracker : FieldValuePair
    {
        public FieldValuePairTracker(bool updated)
        {
            this.Updated = updated;
        }
        public bool Updated { get; set; }
    }

    public class RelativityObjectWrapper
    {
        public FieldValuePairTracker this[Guid g]
        {
            get
            {
                var field = this.FieldValues.FirstOrDefault(x => x.Field.Guids.Contains(g)) ?? throw new ApplicationException($"Field with Guid {g} cannot be found on the artifact collection");
                return field;
            }
        }
        public FieldValuePairTracker this[string name]
        {
            get
            {
                var field = this.FieldValues.FirstOrDefault(x => x.Field.Name == name) ?? throw new ApplicationException($"Field with Name {name} cannot be found on the artifact collection");
                return field;
            }
        }

        public FieldValuePairTracker this[int fieldId]
        {
            get
            {
                var field = this.FieldValues.FirstOrDefault(x => x.Field.ArtifactID == fieldId) ?? throw new ApplicationException($"Field with Id {fieldId} cannot be found on the artifact collection");
                return field;
            }
        }

        public bool FieldValueExists(Guid g)
        {
            return this.FieldValues.Any(x => x.Field.Guids.Contains(g));
        }

        public List<FieldValuePairTracker> FieldValues { get; set; } = new List<FieldValuePairTracker>();
        public IReadOnlyList<FieldValuePair> UpdatedFields { get { return this.FieldValues?.Where(x => x.Updated)?.ToList() ?? new List<FieldValuePairTracker>(); } }
        public RelativityObjectWrapper() { }
        public int ArtifactId { get; set; }
        public int? ParentId { get; set; }
        public string TextIdentifier { get; set; }

        public Guid ObjectTypeGuid { get;set; }
        public RelativityObjectWrapper(Relativity.Services.Objects.DataContracts.RelativityObject artifact, bool updated = false)
        {
            if (artifact != null)
            {
                this.FieldValues = artifact.FieldValues?.Select(x => new FieldValuePairTracker(updated)
                {
                    Field = x.Field,
                    Value = x.Value,
                }).ToList() ?? new List<FieldValuePairTracker>();
                this.ArtifactId = artifact.ArtifactID;
                this.ParentId = artifact.ParentObject?.ArtifactID;
                this.TextIdentifier = artifact.Name;
            }
        }
    }

    public class OMObjectWrapper
    {
        private RelativityObjectWrapper _rdo;
        public RelativityObjectWrapper Artifact { get { return _rdo ?? (_rdo = new RelativityObjectWrapper()); } set { _rdo = value; } }
        public int ArtifactId { get { return this.Artifact.ArtifactId; } }
    }

}
