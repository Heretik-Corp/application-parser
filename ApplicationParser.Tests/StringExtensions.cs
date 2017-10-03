using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationParser.Tests
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreWhitespace(this string str, string other, StringComparison stringComparison)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (string.IsNullOrEmpty(other))
            {
                throw new ArgumentNullException(nameof(other));
            }
            string normalized1 = Regex.Replace(str, @"\s", "");
            string normalized2 = Regex.Replace(other, @"\s", "");
            bool stringEquals = String.Equals(normalized1, normalized2, stringComparison);
            return stringEquals;
        }
        public static bool EqualsIgnoreWhitespace(this string str, string other)
        {
            return EqualsIgnoreWhitespace(str, other, StringComparison.OrdinalIgnoreCase);
        }
    }
}
