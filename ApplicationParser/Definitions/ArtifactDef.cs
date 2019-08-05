using System.Text.RegularExpressions;

namespace ApplicationParser
{
    public class ArtifactDef
    {
        private string _name;
        public string Name
        {
            get { return EncodeName(_name?.Replace(" ", string.Empty) ?? string.Empty); }
            set { _name = value; }
        }
        public string RawName { get { return _name; } }

        public string Guid { get; set; }

        //move to utils class?
        public static string EncodeName(string text)
        {
            text = Regex.Replace(text, "^#", "h_");
            text = Regex.Replace(text, "#$", "_h");
            return text.Replace("%", "Percent")
                .Replace("::", "_")
                .Replace("-", "_")
                .Replace("&", "And")
                .Replace("^", "carrot")
                .Replace("#", "_")
                .Replace(")", string.Empty)
                .Replace("(", string.Empty)
                .Replace(".", string.Empty)
                .Replace(",", string.Empty)
                .Replace("'", string.Empty)
                .Replace("/", string.Empty);
        }
    }
}
