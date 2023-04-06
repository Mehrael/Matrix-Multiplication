using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            if (N < 64)
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
            int[,] P1 = MatrixMultiply(M1, S1, size_over_2);
            int[,] P2 = MatrixMultiply(S2, B22, size_over_2);
            int[,] P3 = MatrixMultiply(S3, M2, size_over_2);
            int[,] P4 = MatrixMultiply(A22, S4, size_over_2);
            int[,] P5 = MatrixMultiply(S5, S6, size_over_2);
            int[,] P6 = MatrixMultiply(S7, S8, size_over_2);
            int[,] P7 = MatrixMultiply(S9, S10, size_over_2);

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
