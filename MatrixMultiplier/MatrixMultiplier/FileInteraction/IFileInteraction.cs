using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMultiplier.FileInteraction
{
    public interface IFileInteraction
    {
        List<string> Open(string filename);

        //for future saving the result matrix to file
        void Save(string filename, List<string> strings);
    }
}
