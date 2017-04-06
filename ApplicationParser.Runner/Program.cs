using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = @"";
            var file = File.ReadAllText(path);
            var app = new Parser().Parse(file);

        }
    }
}
