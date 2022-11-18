using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMultiplier.FileInteraction
{
    public interface IDialogInteraction
    {
        string Path { get; set; }
        bool OpenFileDialog();
    }
}
