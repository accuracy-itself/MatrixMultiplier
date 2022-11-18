using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MatrixMultiplier
{
    public static class MatrixCalculator
    {
        private static int[,] matrixA, matrixB, matrixResult;



        private struct Element
        {
            public int i, j;
        }

        private static void CalculateElement(object obj)
        {
            Element element = (Element)obj;
            int i = element.i;
            int j = element.j;
            int sum = 0;
            int n = matrixA.GetLength(1);
            for (int k = 0; k < n; k++)
                sum += matrixA[i, k] * matrixB[k, j];

            matrixResult[i, j] = sum;
        }

        public static int[,] Multiply(int[,] a, int[,] b)
        {
            int aRows = a.GetLength(0);
            int aColumns = a.GetLength(1);
            int bRows = b.GetLength(0);
            int bColumns = b.GetLength(1);

            matrixA = a;
            matrixB = b;
            matrixResult = new int[aRows, bColumns];

            if (aColumns != bRows)
                return null;
            
            Thread[,] threads = new Thread[aRows, bColumns];
            for (int i = 0; i < aRows; i++)
                for (int j = 0; j < bColumns; j++)
                {
                    threads[i, j] = new Thread(new ParameterizedThreadStart(CalculateElement));
                    Element element = new Element();
                    element.i = i;
                    element.j = j;
                    threads[i, j].IsBackground = true;
                    threads[i, j].Start((object)element);
                }

            for (int i = 0; i < aRows; i++)
                for (int j = 0; j < bColumns; j++)
                    threads[i, j].Join();

            return matrixResult;
        }
    }
}

