using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMultiplier.FileInteraction
{
    public class TextFileInteraction : IFileInteraction
    {
        public List<string> Open(string filename)
        { 
            var fileText = File.ReadAllLines(filename);
            List<string> result = new List<string>(fileText);
            return result;
        }

        public void Save(string filename, List<string> strings)
        {
            File.WriteAllLines(filename, strings);
        }
    }
}
