using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMultiplier.DataTransformation
{
    public static class MatrixTransformer
    {
        public static List<string> CreateListFromMatrix(int[,] matrix) 
        {
            int[][] matrixConverted = new int[matrix.GetLength(0)][];
            for (int k = 0; k < matrix.GetLength(0); k++)
            {
                matrixConverted[k] = new int[matrix.GetLength(1)];
                for (int l = 0; l < matrix.GetLength(1); l++)
                    matrixConverted[k][l] = matrix[k, l];
            }

            List<string> result = new List<string>();
            for (int k = 0; k < matrix.GetLength(0); k++)
                result.Add(String.Join("  ", matrixConverted[k]));

            return result;
        }

        public static int[,] CreateMatrixFromList(List<string> list)
        {
            int columns;
            columns = list[0].Split("  ").Select(int.Parse).ToArray().Length;           
            int[,] matrixA = new int[list.Count, columns];
            int i = 0, j = 0;
            foreach (var s in list)
            {
                int[] row = s.Split("  ").Select(int.Parse).ToArray();
                for (int k = 0; k < columns; k++)
                    matrixA[i, k] = row[k];
                i++;
            }

            return matrixA;
        }
    }
}
