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
        static public int[,] MatrixMultiply(int[,] A, int[,] B, int N)
        {
            //int[,] result = new int[N, N];

            //if (N == 1)
            //{
            //    result[0, 0] = M1[0, 0] * M2[0, 0];
            //    return result;
            //}

            //int[,] a = new int[N / 2, N / 2];
            //int[,] b = new int[N / 2, N / 2];
            //int[,] c = new int[N / 2, N / 2];
            //int[,] d = new int[N / 2, N / 2];

            //int[,] e = new int[N / 2, N / 2];
            //int[,] f = new int[N / 2, N / 2];
            //int[,] g = new int[N / 2, N / 2];
            //int[,] h = new int[N / 2, N / 2];

            //// partition M1 and M2
            //for (int i = 0; i < N / 2; i++)
            //    for (int j = 0; j < N / 2; j++)
            //    {
            //        a[i, j] = M1[i, j];
            //        b[i, j] = M1[i, j + N / 2];
            //        c[i, j] = M1[i + N / 2, j];
            //        d[i, j] = M1[i + N / 2, j + N / 2];

            //        e[i, j] = M2[i, j];
            //        f[i, j] = M2[i, j + N / 2];
            //        g[i, j] = M2[i + N / 2, j];
            //        h[i, j] = M2[i + N / 2, j + N / 2];
            //    }

            //// compute intermediate matrices
            //int[,] s1 = MatrixSubtraction(f, h);
            //int[,] s2 = MatrixAddition(a, b);
            //int[,] s3 = MatrixAddition(b, d);
            //int[,] s4 = MatrixSubtraction(g, e);
            //int[,] s5 = MatrixAddition(a, d);
            //int[,] s6 = MatrixAddition(e, h);
            //int[,] s7 = MatrixSubtraction(b, d);
            //int[,] s8 = MatrixAddition(g, h);
            //int[,] s9 = MatrixSubtraction(a, c);
            //int[,] s10 = MatrixAddition(e, f);


            //int[,] p1 = MatrixMultiply(a, s1, N / 2);
            //int[,] p2 = MatrixMultiply(s2, h, N / 2);
            //int[,] p3 = MatrixMultiply(s3 , e, N / 2);
            //int[,] p4 = MatrixMultiply(d, s4, N / 2);
            //int[,] p5 = MatrixMultiply(s5, s6, N / 2);
            //int[,] p6 = MatrixMultiply(s7, s8, N / 2);
            //int[,] p7 = MatrixMultiply(s9, s10, N / 2);


            //int[,] r = MatrixAddition(MatrixAddition(p5, p4), MatrixSubtraction(p6, p2));
            //int[,] s = MatrixAddition(p1, p2);
            //int[,] t = MatrixAddition(p3, p4);
            //int[,] u = MatrixSubtraction(MatrixAddition(p5, p1),MatrixAddition(p3, p7));

            //// combine intermediate matrices into result matrix
            //for (int i = 0; i < N / 2; i++)
            //{
            //    for (int j = 0; j < N / 2; j++)
            //    {
            //        result[i, j] = r[i, j];
            //        result[i, j + N / 2] = s[i, j];
            //        result[i + N / 2, j] = t[i, j];
            //        result[i + N / 2, j + N / 2] = u[i, j];
            //    }
            //}

            //return result;

            int[,] result = new int[N, N];
            int size_over_2 = N / 2;
            if (N == 1)
            {
                result[0, 0] = A[0, 0] * B[0, 0];
                return result;
            }

            int[,] A11 = new int[size_over_2, size_over_2];
            int[,] A12 = new int[size_over_2, size_over_2];
            int[,] A21 = new int[size_over_2, size_over_2];
            int[,] A22 = new int[size_over_2, size_over_2];

            int[,] B11 = new int[size_over_2, size_over_2];
            int[,] B12 = new int[size_over_2, size_over_2];
            int[,] B21 = new int[size_over_2, size_over_2];
            int[,] B22 = new int[size_over_2, size_over_2];

            //Divide
            for (int i = 0; i < size_over_2; i++)
                for (int j = 0; j < size_over_2; j++)
                {
                    A11[i, j] = A[i, j];
                    A12[i, j] = A[i, j + size_over_2];
                    A21[i, j] = A[i + size_over_2, j];
                    A22[i, j] = A[i + size_over_2, j + size_over_2];    

                    B11[i, j] = B[i, j];
                    B12[i, j] = B[i, j + size_over_2];
                    B21[i, j] = B[i + size_over_2, j];
                    B22[i, j] = B[i + size_over_2, j + size_over_2];
                }
            //Divide(B, B11, B12, B21, B22);

            //int[,] S1 = Subtract(B12, B22);
            //int[,] S2 = Add(A11, A12);
            //int[,] S3 = Add(A21, A22);
            //int[,] S4 = Subtract(B21, B11);
            //int[,] S5 = Add(A11, A22);
            //int[,] S6 = Add(B11, B22);
            //int[,] S7 = Subtract(A12, A22);
            //int[,] S8 = Add(B21, B22);
            //int[,] S9 = Subtract(A11, A21);
            //int[,] S10 = Add(B11, B12);   
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

            for (int i = 0; i < size_over_2; i++)
                for (int j = 0; j < size_over_2; j++)
                {
                    S1[i, j] = B12[i, j] - B22[i, j];
                    S2[i, j] = A11[i, j] + A12[i, j];
                    S3[i, j] = A21[i, j] + A22[i, j];
                    S4[i, j] = B21[i, j] - B11[i, j];
                    S5[i, j] = A11[i, j] + A22[i, j];
                    S6[i, j] = B11[i, j] + B22[i, j];
                    S7[i, j] = A12[i, j] - A22[i, j];
                    S8[i, j] = B21[i, j] + B22[i, j];
                    S9[i, j] = A11[i, j] - A21[i, j];
                    S10[i, j] = B11[i, j] + B12[i, j];
                }

            int[,] P1 = MatrixMultiply(A11, S1, size_over_2);
            int[,] P2 = MatrixMultiply(S2, B22, size_over_2);
            int[,] P3 = MatrixMultiply(S3, B11, size_over_2);
            int[,] P4 = MatrixMultiply(A22, S4, size_over_2);
            int[,] P5 = MatrixMultiply(S5, S6, size_over_2);
            int[,] P6 = MatrixMultiply(S7, S8, size_over_2);
            int[,] P7 = MatrixMultiply(S9, S10, size_over_2);

            //int[,] C11 = Subtract(Add(Add(P5, P4), P6), P2);
            //int[,] C12 = Add(P1, P2);
            //int[,] C21 = Add(P3, P4);  
            //int[,] C22 = Subtract(Subtract(Add(P5, P1), P3), P7);

            int[,] C12 = new int[size_over_2, size_over_2];
            int[,] C11 = new int[size_over_2, size_over_2];
            int[,] C21 = new int[size_over_2, size_over_2];
            int[,] C22 = new int[size_over_2, size_over_2];

            for (int i = 0; i < size_over_2; i++)
                for (int j = 0; j < size_over_2; j++)
                {
                    result[i, j] = P5[i, j] + P4[i, j] - P2[i, j] + P6[i, j];
                    result[i, j + size_over_2] = P1[i, j] + P2[i, j];
                    result[i + size_over_2, j] = P3[i, j] + P4[i, j];
                    result[i + size_over_2, j + size_over_2] = P5[i, j] + P1[i, j] - P3[i, j] - P7[i, j];
                }


            //// Combine
            //for (int i = 0; i < size_over_2; i++)
            //    for (int j = 0; j < size_over_2; j++)
            //    {
            //        result[i, j] = C11[i, j];
            //        result[i, j + size_over_2] = C12[i, j];
            //        result[i + size_over_2, j] = C21[i, j];
            //        result[i + size_over_2, j + size_over_2] = C22[i, j];
            //    }


            return result;
        }

        #endregion
    }
}
