using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fitlinetodata
{
    class Program
    {
        static double[,] Matrix;
        static double[,] transpose(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            double[,] matrixCopy = new double[columns,rows];
            for(int i = 0; i< rows;i++)
            {
                for (int j = 0; j< columns;j++)
                {
                    matrixCopy[j, i] = matrix[i, j];
                }
            }
            return matrixCopy;
        }
        /*static double[,] multiply(double[,] matrix, double [] vector)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            double[,] matrixRes = new double[rows, 1];
            if (columns != vector.Length)
            {
                Console.WriteLine("\n Number of columns in First Matrix should be equal to Number of rows in Second Matrix.");
                Console.WriteLine("\n Please re-enter correct dimensions.");
                Environment.Exit(-1);
            }
            for (int i = 0; i < rowsC; i++)
            {
                matrixRes[i, j] = 0;
                for (int j = 0; j < columnsC; j++)
                {
                    
                    
                        matrixRes[i, j] = matrixRes[i, j] + matrix[i, k] * matrixB[k, j];
                }
            }
            
            return matrixRes;
        }*/
        static double[,] multiply(double[,] matrixA,double[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int columnsA = matrixA.GetLength(1);
            int rowsB = matrixB.GetLength(0);
            int columnsB = matrixB.GetLength(1);
            
            int rowsC = rowsA;
            int columnsC = columnsB;
            double[,] matrixC = new double[rowsC, columnsC];
            if (columnsA!= rowsB)
            {
                Console.WriteLine("\n Number of columns in First Matrix should be equal to Number of rows in Second Matrix.");
                Console.WriteLine("\n Please re-enter correct dimensions.");
                Environment.Exit(-1);
            }
            else
            {
                for (int i = 0; i < rowsC; i++)
                {
                    for (int j = 0; j < columnsC; j++)
                    {
                        matrixC[i, j] = 0;
                        for (int k = 0; k < columnsA; k++)
                            matrixC[i, j] = matrixC[i, j] + matrixA[i, k] * matrixB[k, j];
                    }
                }
            }
            return matrixC;
        }
        //static void inverse(A)
        //static void gaussJordan(A)
        static void Main(string[] args)
        {
            const int rows = 43;//unix wc -l sourceData.csv
            const int columns = 4;
            Matrix = new double[rows, columns] {
                {25600,160,1,60},
                {28224,168,1,60},
                {28224,168,1,58},
                {28561,169,1,96},
{28900,170,1,70},
{28900,170,1,62},
{29584,172,1,61},
{29584,172,1,79},
{30625,175,1,72},
{31329,177,1,62},
{32400,180,1,86},
{32400,180,1,60},
{32400,180,1,75},
{32400,180,1,70},
{32761,181,1,76},
{33489,183,1,65},
{33489,183,1,77},
{33489,183,1,87},
{33489,183,1,76},
{33856,184,1,65},
{34225,185,1,90},
{34225,185,1,65},
{34225,185,1,80},
{34225,185,1,72},
{34225,185,1,85},
{34596,186,1,110},
{35721,189,1,85},
{36100,190,1,75},
{36100,190,1,70},
{36481,191,1,70},
{37249,193,1,71},
{37636,194,1,87},
{38025,195,1,95},
{31684,178,1,73},
{34969,187,1,85},
{38025,195,1,88},
{27889,167,1,63},
{38025,195,1,79},
{31329,177,1,80},
{30625,175,1,70},
{36100,190,1,92},
{34596,186,1,75},
{36100,190,1,85},
                    };

            double[,] tMatrix = new double[columns,rows];
            tMatrix = transpose(Matrix);
            for (int i = 0; i < columns; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < rows; j++)
                {
                    Console.Write(tMatrix[i, j] + " ");
                }
                
            }
            Console.ReadKey();
        }
    }
}
