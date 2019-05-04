using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Fitlinetodata
{
    class Program
    {   
        
        static double[,] Transpose(double[,] matrix)
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
        public static double[] MultiplyVector(double[,] matrix, double [] vector)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            double[] vectorRes = new double[rows];
            if (columns != vector.Length)
            {
                Console.WriteLine("\n Number of columns in First Matrix should be equal to Number of rows in Second Matrix.");
                Console.WriteLine("\n Please re-enter correct dimensions.");
                Environment.Exit(-1);
            }
            for (int i = 0; i < rows; i++)
            {
                vectorRes[i] = 0;
                for (int j = 0; j < columns; j++)
                {              
                        vectorRes[i] = vectorRes[i] + matrix[i, j] * vector[j];
                }
            }
            
            return vectorRes;
        }
        public static double[,] MultiplyMatrix(double[,] matrixA,double[,] matrixB)
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
        static double[,] Inverse(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            double[,] inverse = new double[columns, rows * 2];
            double[,] resInverse = new double[columns, rows];
            //init
            for (int i = 0; i<rows;i++)
            {
                for(int j = 0; j < columns;j++)
                {
                    inverse[i, j] = matrix[i, j];
                    if (i==j)
                    {
                        inverse[i, j + columns] = 1;
                    }
                    else
                    {
                        inverse[i, j + columns] = 0;
                    }                   
                }
            }
            //gaussjordan
            int completeRows = 0;
            for (int j = 0; j<columns;j++)
            {

                for (int i = completeRows; i < rows; i++)
                {
                    if (inverse[i,j] != 0)
                    {
                        // pivot is 1
                        for (int k=completeRows+1; k<columns*2 ; k++)
                        {
                            inverse [i,k] = inverse[i,k] / inverse [i, j];
                        }
                        inverse[i, j] = 1;
                        // swap to its throne
                        if (i != completeRows)
                        {
                            for (int k = completeRows; k < columns * 2; k++)
                            {
                                double swap = inverse[i, k];
                                inverse[i, k] = inverse[completeRows, k];
                                inverse[completeRows, k] = swap;
                            }
                        }                       
                        // decrease column by pivot row so it contains 0
                        for (int m=0; m < rows; m++)
                        {
                            if (m != completeRows)
                            {
                                for (int k = completeRows+1; k < columns * 2; k++)
                                {
                               
                                    inverse[m, k] = inverse[m, k] - inverse[m, completeRows] * inverse[completeRows, k];
                                }
                                inverse[m, completeRows] = 0;
                            }
                        }                       
                        /* // zaokruhlenie
                        for (int y = 0; y < rows; y++)
                        {
                            
                            for (int x = 0; x < columns*2; x++)
                            {
                                inverse[y, x] = Math.Round(inverse[y, x],4);
                            }

                        }
                        */
                        completeRows++;
                        //test
                        /*
                        for (int y = 0; y < rows; y++)
                        {
                            Console.WriteLine();
                            for (int x = 0; x < columns * 2; x++)
                            {
                                Console.Write(inverse[y, x] + " |");
                            }
                        }
                        Console.WriteLine();*/
                    }                   
                }
            }

            //cut answer
            for (int y = 0; y < rows; y++)
            {

                for (int x = 0; x < columns; x++)
                {
                    resInverse[y, x] = inverse[y, x+columns];
                }

            }
            return resInverse;
        }
        static void Main(string[] args)
        {
           
            double[,] Matrix;
            double[] vector;
            double[] resVector;
            double[,] tMatrix;
            const int columns = 3;
            
            var path = Path.Combine(Directory.GetCurrentDirectory(), "sourceData.txt");
           
            using (StreamReader table = new StreamReader(path))
            {
                int rows = int.Parse(table.ReadLine());

                Matrix = new double[rows, columns];
                vector = new double[rows];
                resVector = new double[columns];
                tMatrix = new double[columns, rows];

                for (int i = 0; i < rows; i++)
                {
                    string[] a = table.ReadLine().Split();

                    int height = int.Parse(a[0]);
                    int weight = int.Parse(a[1]);               
                    Matrix[i, 0] = height * height;
                    Matrix[i, 1] = height;
                    Matrix[i, 2] = 1;
                    vector[i] = weight;
                }

            }
            
            tMatrix = Transpose(Matrix);
            resVector = MultiplyVector(Inverse(MultiplyMatrix(tMatrix, Matrix)),MultiplyVector(tMatrix, vector));

            Console.WriteLine("Weight= alpha*Height*Height + beta*Height + gama");
            Console.WriteLine("alpha = " + resVector[0]);
            Console.WriteLine("beta  = " + resVector[1]);
            Console.WriteLine("gama  = " + resVector[2]);
            Console.WriteLine("Press a key to end");
            Console.ReadKey();
        }
    }
}
