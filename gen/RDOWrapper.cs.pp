using kCura.Relativity.Client.DTOs;
using ApplicationParser;
using System;

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

    public class OMObjectWrapper
    {
        private Relativity.Services.Objects.DataContracts.RelativityObject _rdo;
        public Relativity.Services.Objects.DataContracts.RelativityObject Artifact { get { return _rdo ?? (_rdo = new Relativity.Services.Objects.DataContracts.RelativityObject()); } set { _rdo = value; } }
		public int ArtifactId { get { return this.Artifact.ArtifactID; } }
    }

}
