using System;

namespace ApplicationParser
{
    public class FieldNotLoadedException : Exception
    {
        public FieldNotLoadedException(string message) : base(message) { }
    }
}
