using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMultiplier.FileInteraction
{
    public class DefaultDialogInteraction : IDialogInteraction
    {
        public string Path { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Path = openFileDialog.FileName;
                return true;
            }

            return false;
        }
    }
}
