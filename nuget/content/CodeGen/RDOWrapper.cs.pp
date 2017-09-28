using kCura.Relativity.Client.DTOs;
using ApplicationParser;
using System;

namespace $rootnamespace$
{
    public class RDOWrapper
    {
		private RDO _rdo;
        public RDO Artifact { get { return _rdo ?? (_rdo = new Artifact()); } set { _rdo = value; } }
		public int ArtifactId { get { return RDO.ArtifactID; } }
    }

	public class DocumentWrapper : kCura.Relativity.Client.DTOs.Document
    {
		private kCura.Relativity.Client.DTOs.Document _rdo;
        public kCura.Relativity.Client.DTOs.Document Artifact { get { return _rdo ?? (_rdo = new kCura.Relativity.Client.DTOs.Document()); } set { _rdo = value; } }
		public int ArtifactId { get { return Artifact.ArtifactID; } }
    }
}
