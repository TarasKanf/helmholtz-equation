using System;

namespace HelmholtzEquation.LinearSystem
{
    public static class SLAE
    {
        private const double epsilon = 0.000001;
        public static double[] JacobiMethodSolving(double[,] a, double[] b, double eps)
        {
            double[] xk1 = new double[b.Length];
            double[] xk2 = new double[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                xk1[i] = 0;
                xk2[i] = 1;
            }

            double temp = 0;
            int iteration = 0;
            while ((LoopContinue(xk1, xk2, eps)) && (iteration < 10000))
            {
                iteration++;
                for (int i = 0; i < xk1.Length; i++)
                    xk1[i] = xk2[i];
                for (int i = 0; i < b.Length; i++)
                {
                    temp = 0;
                    for (int j = 0; j < b.Length; j++)
                    {
                        if (j == i) continue;
                        temp += a[i, j] * xk1[j];
                    }
                    temp -= b[i];
                    xk2[i] = -temp / a[i, i];
                }
            }
            if (iteration > 9999) throw new Exception("Jacobi method does not converge to solution.");
            return xk2;
        }
        private static bool LoopContinue(double[] x1, double[] x2, double eps)
        {
            bool ans = true;
            double maxdif = Math.Abs(x1[0] - x2[0]);
            for (int i = 1; i < x1.Length; i++)
                if (maxdif < Math.Abs(x1[i] - x2[i])) maxdif = Math.Abs(x1[i] - x2[i]);
            if (maxdif < eps) ans = false;
            return ans;
        }
        static public double[] LU_methodSolving(double[,] A, double[] F, int n)
        {
            // розклад матриці A
            double[,] lu = new double[n, n];
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    sum = 0;
                    for (int k = 0; k < i; k++)
                        sum += lu[i, k] * lu[k, j];
                    lu[i, j] = A[i, j] - sum;
                }
                for (int j = i + 1; j < n; j++)
                {
                    sum = 0;
                    for (int k = 0; k < i; k++)
                        sum += lu[j, k] * lu[k, i];
                    lu[j, i] = (1 / lu[i, i]) * (A[j, i] - sum);
                }
            }
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                sum = 0;
                for (int k = 0; k < i; k++)
                    sum += lu[i, k] * y[k];
                y[i] = F[i] - sum;
            }
            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                sum = 0;
                for (int k = i + 1; k < n; k++)
                    sum += lu[i, k] * x[k];
                x[i] = (1 / lu[i, i]) * (y[i] - sum);
            }
            return x;
        }
        public static double[] SolveWithQRmethod(double[,] matr, double[] b, int n)
        {
            double[,] M = matr;
            double[] M1;
            double[] M2;
            //bool IsDegenerate;            
            M1 = new double[n];
            M2 = new double[n];
            bool sing = Rosklad(ref M, ref M1, ref M2, n); // is deganarated => sing == true
            QRSolve(ref M, ref M1, ref M2, n, ref b);
            return b;
        }
        private static bool Rosklad(ref double[,] M, ref double[] M1, ref double[] M2, int N)
        {
            bool sing = false; // матриця невироджена
            for (int k = 0; k < N - 1; k++)
            {
                double max = Math.Abs(M[k, k]);
                for (int i = k; i < N; i++)
                    if (max < Math.Abs(M[i, k])) max = Math.Abs(M[i, k]);
                if (Math.Abs(max) < epsilon)
                {
                    M1[k] = 0;
                    M2[k] = 0;
                    sing = true;
                }
                else // сформувати Qk і помножити М на неї зліва
                {
                    for (int i = k; i < N; i++)
                    {
                        M[i, k] = M[i, k] / max;
                    }
                    double sigma = 0;
                    for (int i = k; i < N; i++)
                    {
                        sigma += M[i, k] * M[i, k];
                    }
                    sigma = ((Math.Sign(M[k, k]) == 0) ? Math.Sqrt(sigma) : Math.Sign(M[k, k])) * Math.Sqrt(sigma);
                    M[k, k] = M[k, k] + sigma;
                    M1[k] = M[k, k] * sigma;
                    M2[k] = -max * sigma;
                    double t = 0;
                    for (int j = k + 1; j < N; j++)
                    {
                        t = 0;
                        for (int i = k; i < N; i++)
                        {
                            t += M[i, k] * M[i, j];
                        }
                        t /= M1[k];
                        for (int i = k; i < N; i++)
                        {
                            M[i, j] -= t * M[i, k];
                        }
                    }
                }
            }
            if (Math.Abs(M[N - 1, N - 1]) < epsilon) sing = true;
            M2[N - 1] = M[N - 1, N - 1];
            return sing;
        }
        private static void QRSolve(ref double[,] M, ref double[] M1, ref double[] M2, int N, ref double[] b)
        {
            // b <= QT*b
            double t;
            for (int j = 0; j < N - 1; j++)
            {
                t = 0;
                for (int i = j; i < N; i++)
                {
                    t += M[i, j] * b[i];
                }
                t /= M1[j];
                for (int i = j; i < N; i++)
                {
                    b[i] -= t * M[i, j];
                }
            }
            RSolve(ref M, ref M1, ref M2, N, ref b);
        }
        private static void RSolve(ref double[,] M, ref double[] M1, ref double[] M2, int N, ref double[] b) //  розвязок зберігають в векторі b
        {
            // Rx = b  -> x
            b[N - 1] /= M2[N - 1];
            double sum = 0;
            for (int i = N - 2; i >= 0; i--)
            {
                sum = 0;
                for (int j = i + 1; j < N; j++)
                {
                    sum += M[i, j] * b[j];
                }
                b[i] = (b[i] - sum) / M2[i];
            }
        }
    }
}
