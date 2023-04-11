using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class MatrixMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 square matrices in an efficient way [Strassen's Method]
        /// </summary>
        /// <param name="M1">First square matrix</param>
        /// <param name="M2">Second square matrix</param>
        /// <param name="N">Dimension (power of 2)</param>
        /// <returns>Resulting square matrix</returns>
        static public int[,] MatrixMultiply(int[,] M1, int[,] M2, int N)
        {
            int[,] result = new int[N, N];
            if (N == 2)
            {
                result[0, 0] = M1[0, 0] * M2[0, 0] + M1[0, 1] * M2[1, 0];
                result[0, 1] = M1[0, 0] * M2[0, 1] + M1[0, 1] * M2[1, 1];
                result[1, 0] = M1[1, 0] * M2[0, 0] + M1[1, 1] * M2[1, 0];
                result[1, 1] = M1[1, 0] * M2[0, 1] + M1[1, 1] * M2[1, 1];

                return result;
            }
            else if (N == 4)
            {
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        result[i, j] = M1[i, 0] * M2[0, j] + M1[i, 1] * M2[1, j] + M1[i, 2] * M2[2, j] + M1[i, 3] * M2[3, j];

                return result;
            }
            else if (N < 128)
            {
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        for (int k = 0; k < N; k++)
                            result[i, j] += M1[i, k] * M2[k, j];
 
                return result;
            }

            int size_over_2 = N / 2;

            int[,] A22 = new int[size_over_2, size_over_2];
            int[,] B22 = new int[size_over_2, size_over_2];

            int[,] S1 = new int[size_over_2, size_over_2];
            int[,] S2 = new int[size_over_2, size_over_2];
            int[,] S3 = new int[size_over_2, size_over_2];
            int[,] S4 = new int[size_over_2, size_over_2];
            int[,] S5 = new int[size_over_2, size_over_2];
            int[,] S6 = new int[size_over_2, size_over_2];
            int[,] S7 = new int[size_over_2, size_over_2];
            int[,] S8 = new int[size_over_2, size_over_2];
            int[,] S9 = new int[size_over_2, size_over_2];
            int[,] S10 = new int[size_over_2, size_over_2];

            // Divide
            for (int i = 0; i < size_over_2; i++)
                for (int j = 0; j < size_over_2; j++)
                {
                    S1[i, j] = M2[i, j + size_over_2] - M2[i + size_over_2, j + size_over_2];    // f - h
                    
                    S2[i, j] = M1[i, j] + M1[i, j + size_over_2];                                // a + b
                    
                    S3[i, j] = M1[i + size_over_2, j] + M1[i + size_over_2, j + size_over_2];    // c + d
                    
                    S4[i, j] = M2[i + size_over_2, j] - M2[i, j];                                // g - e
                    
                    S5[i, j] = M1[i, j] + M1[i + size_over_2, j + size_over_2];                  // a + d
                    
                    S6[i, j] = M2[i, j] + M2[i + size_over_2, j + size_over_2];                  // e + h
                    
                    S7[i, j] = M1[i, j + size_over_2] - M1[i + size_over_2, j + size_over_2];    // b - d
                    
                    S8[i, j] = M2[i + size_over_2, j] + M2[i + size_over_2, j + size_over_2];    // g + h
                    
                    S9[i, j] = M1[i, j] - M1[i + size_over_2, j];                                // a - c
                    
                    S10[i, j] = M2[i, j] + M2[i, j + size_over_2];                               // e + f

                    A22[i, j] = M1[i + size_over_2, j + size_over_2];
                    B22[i, j] = M2[i + size_over_2, j + size_over_2];            
                }

                           
            // Conquer
            var t1 = Task.Factory.StartNew(() => MatrixMultiply(M1, S1, size_over_2));
            var t2 = Task.Factory.StartNew(() => MatrixMultiply(S2, B22, size_over_2));
            var t3 = Task.Factory.StartNew(() => MatrixMultiply(S3, M2, size_over_2));
            var t4 = Task.Factory.StartNew(() => MatrixMultiply(A22, S4, size_over_2));
            var t5 = Task.Factory.StartNew(() => MatrixMultiply(S5, S6, size_over_2));
            var t6 = Task.Factory.StartNew(() => MatrixMultiply(S7, S8, size_over_2));
            var t7 = Task.Factory.StartNew(() => MatrixMultiply(S9, S10, size_over_2));

            Task.WaitAll(t1, t2, t3, t4, t5, t6, t7);

            int[,] P1 = t1.Result;
            int[,] P2 = t2.Result;
            int[,] P3 = t3.Result;
            int[,] P4 = t4.Result;
            int[,] P5 = t5.Result;
            int[,] P6 = t6.Result;
            int[,] P7 = t7.Result;

            // Combine
            for (int i = 0; i < size_over_2; i++)
                for (int j = 0; j < size_over_2; j++)
                {
                    result[i, j] = P5[i, j] + P4[i, j] - P2[i, j] + P6[i, j];
                    result[i, j + size_over_2] = P1[i, j] + P2[i, j];
                    result[i + size_over_2, j] = P3[i, j] + P4[i, j];
                    result[i + size_over_2, j + size_over_2] = P5[i, j] + P1[i, j] - P3[i, j] - P7[i, j];
                }

            return result;
        }
        #endregion
    }
}
