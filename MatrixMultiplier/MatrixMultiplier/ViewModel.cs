using MatrixMultiplier.Commands;
using MatrixMultiplier.DataTransformation;
using MatrixMultiplier.FileInteraction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatrixMultiplier
{
    public class ViewModel : INotifyPropertyChanged
    {
        private IFileInteraction fileInteraction;
        private IDialogInteraction dialogInteraction;
        
        private List<string>[] Matrixes { get; set; }
        public List<string> MatrixA { get { return Matrixes[FirstIndex]; } set { } }
        public List<string> MatrixB { get { return Matrixes[SecondIndex]; } set { } }
        public List<string> MatrixC { get { return Matrixes[ResultIndex]; } set { } }

        public int FirstIndex { get { return 0; } }
        public int SecondIndex { get { return 1; } }
        public int ResultIndex { get { return 2; } }

        private Command openCommand;
        public Command OpenCommand
        {
            get 
            {
                return openCommand ??= new Command(obj =>
                    {
                        try
                        {
                            if (dialogInteraction.OpenFileDialog() == true)
                            {
                                int index = (int)obj;
                                Matrixes[index] = fileInteraction.Open(dialogInteraction.Path);
                                OnPropertyChanged(nameof(MatrixA));
                                OnPropertyChanged(nameof(MatrixB));
                            }
                        }
                        catch
                        {
                            
                        }
                    });
            }
            
            set { openCommand = value; }
        }

        private Command multiplyCommand;
        public Command MultiplyCommand
        {
            get
            {
                return multiplyCommand ??= new Command(obj =>
                    {
                        try
                        {
                            int[,] matrixA = MatrixTransformer.CreateMatrixFromList(Matrixes[FirstIndex]);
                            int[,] matrixB = MatrixTransformer.CreateMatrixFromList(Matrixes[SecondIndex]);
                            int[,] resultMatrix = MatrixCalculator.Multiply(matrixA, matrixB);

                            if (resultMatrix == null)
                                throw new Exception("There's something wrong with your matrixes!");

                            Matrixes[ResultIndex] = MatrixTransformer.CreateListFromMatrix(resultMatrix);
                            OnPropertyChanged(nameof(MatrixC));
                        }
                        catch (Exception) 
                        {
                            MessageBox.Show("There's something wrong with your matrixes!");
                        }
                    });
            }
        }

        public ViewModel(IFileInteraction fileInteraction, IDialogInteraction dialogInteraction)
        {
            Matrixes = new List<string>[3] { new List<string>(), new List<string>(), new List<string>()};
            this.fileInteraction = fileInteraction;
            this.dialogInteraction = dialogInteraction;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
