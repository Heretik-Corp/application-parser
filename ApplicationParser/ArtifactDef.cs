namespace ApplicationParser
{
    public class ArtifactDef
    {
        private string _name;
        public string Name
        {
            get { return _name?.Replace(" ", string.Empty) ?? string.Empty; }
            set { _name = value; }
        }
        public string Guid { get; set; }

    }
}
